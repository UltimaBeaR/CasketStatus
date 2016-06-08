using System;
using System.Data.Entity;

namespace CasketStatusWebSite.Logic
{
    // Контекст базы данных - статус casket
    public class CasketStatusDbContext : DbContext
    {
        public CasketStatusDbContext()
            // Будем использовать ту же БД, что и для identity (Строка подключения сидит в web.config)
            : base("DefaultConnection")
        {
            // Do nothing
        }

        // Таблица CurrentStatus - содержит только одну запись - текущий статус сервиса ларец (casket)
        public DbSet<CurrentStatus> CurrentStatus { get; set; }
    }
}
