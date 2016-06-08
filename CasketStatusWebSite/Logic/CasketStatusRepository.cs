using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasketStatusWebSite.Logic
{
    // Статус-код работы casket сервиса
    public enum StatusCode : int
    {
        // Все работает штатно, работы по обновлению не ведутся
        Available = 0,
        // Сервис недоступен, ведутся технические работы
        Unavailable = 1,
        // Сейчас все работает штатно, но на определенную дату запланированы работы
        WorkScheduled = 2
    }

    // Текущий статус (EF сущность)
    public class CurrentStatus
    {
        // Primary key. Просто, чтобы EF был доволен
        public int Id { get; set; }
        // Статус-код
        public StatusCode Code { get; set; }
        // Запланированная дата технических работ. Актуально только в случае Code = StatusCode.WorkScheduled, иначе не используется
        public DateTime ScheduledWorkDate { get; set; }
    }

    // Репозиторий статуса работы Ларца
    // Будем работать с интерфейсом, чтобы не привязываться сильно к реализации БД
    public interface ICasketStatusRepository : IDisposable
    {
        // Текущий статус
        CurrentStatus CurrentStatus { get; set; }
    }

    // Реализация ICasketStatusRepository для Entity Framework
    public class CasketStatusEFRepository : ICasketStatusRepository
    {
        // Контекст БД
        CasketStatusDbContext _dbContext;

        public CasketStatusEFRepository()
        {
            _dbContext = new CasketStatusDbContext();
        }

        // ICasketStatusRepository

        public CurrentStatus CurrentStatus
        {
            get
            {
                return GetCurrentStatus();
            }

            set
            {
                // Вызываем get, чтобы удостовериться, что запись существует
                var queriedStatus = GetCurrentStatus();

                // Обновляем запись

                value.Id = queriedStatus.Id;
                _dbContext.Entry(queriedStatus).CurrentValues.SetValues(value);
                _dbContext.SaveChanges();
            }
        }

        // Получает текущий статус. В случае если в БД такой записи нет - создаст запись со значением available
        private CurrentStatus GetCurrentStatus()
        {
            // Запрос на получение текущего статуса
            var getCurrentStatusQuery = _dbContext.CurrentStatus.Take(1);

            // Есть ли запись со статусом в таблице текущего статуса
            bool statusExists = getCurrentStatusQuery.Count() == 1;

            // Если статуса нет, добавим его
            if (!statusExists)
            {
                // Начинаем транзакцию
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        statusExists = getCurrentStatusQuery.Count() == 1;

                        // Если статуса все еще нет
                        if (!statusExists)
                        {
                            // Добавляем статус и сохраняем

                            _dbContext.CurrentStatus.Add(new CurrentStatus() { Code = StatusCode.Available, ScheduledWorkDate = DateTime.Now });
                            _dbContext.SaveChanges();

                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return getCurrentStatusQuery.First();
        }

        // IDisposable

        public void Dispose()
        {
            // Закрываем контекст БД
            _dbContext.Dispose();
        }
    }
}
