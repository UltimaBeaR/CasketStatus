using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasketStatusWebSite.Tests
{
    // Тут ничего нет, не успеваю.
    // Но вообще, если воткнуть DI контейнер (NInject), сделать соответствие ICasketStatusRepository -> CasketStatusEFRepository для основного проекта и
    // ICasketStatusRepository -> CasketStatusRepositoryMock для юнит тестов, сделать инъекцию ICasketStatusRepository через конструктор контроллеров
    // Home и RestfulApi, то можно их протестировать по аналогии с sample приложением (т.к. они вроде больше не имеют внешних зависимостей). Остальные контроллеры имеют зависимости от Identity (А она от БД), тут надо что-то
    // похожее видимо придумывать. Кроме контроллеров тестировать тут особо нечего на мой взгляд.
}
