using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CasketStatusWebSite.Models;

namespace CasketStatusWebSite
{
    // Менеджер юзеров (аккаунтов)
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            // Создаем и настраиваем менеджер юзеров

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationIdentityDbContext>()));
            
            // Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = true
            };

            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1
            };

            // Configure user lockout defaults
            userManager.UserLockoutEnabledByDefault = false;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            // Создаем пользователя супер-админа, если такого еще нет
            // ToDo: Незнаю, насколько правильно делать это тут, так как метод вызывается очень часто. Возможно стоит найти место сразу после запуска
            // веб-приложения, как только ApplicationUserManager станет доступен
            Logic.SuperAdminCreator.CreateSuperAdmin(userManager);

            return userManager;
        }
    }

    // Менеджер для входа под аккаунтом
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
