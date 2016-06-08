using Microsoft.AspNet.Identity;
using CasketStatusWebSite.Models;

namespace CasketStatusWebSite.Logic
{
    // Создавалка супер-админа
    public static class SuperAdminCreator
    {
        // Имя (логин) супер админа (начальный пароль задается в web.config)
        private const string saUserName = "sa";
        // Ключ с начальным паролем супер админа в web.config (внутри тега <appSettings>)
        private const string saPasswordConfigKey = "SuperAdminPassword";

        // Создает аккаунт супер-администратора, в случае, если его еще не зарегистрировано
        public static void CreateSuperAdmin(ApplicationUserManager userManager)
        {
            // Если аккаунт супер-админа уже есть - выходим
            if (userManager.FindByName(saUserName) != null)
                return;

            // Читаем пароль супер-админа из web.config

            var webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var saPassword = webConfig.AppSettings.Settings[saPasswordConfigKey];

            if (saPassword == null)
                throw new System.Exception($"\"{saPasswordConfigKey}\" is not defined in Web.config under <appSettings>");

            // Создадим аккаунт суперадмина
            var creationResult = userManager.Create(new ApplicationUser() { UserName = saUserName }, saPassword.Value);
            if (!creationResult.Succeeded)
                throw new System.Exception($"Cannot create super-user.\nErrors: {string.Join(",", creationResult.Errors)}");
        }
    }
}
