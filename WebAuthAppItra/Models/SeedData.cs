using Microsoft.EntityFrameworkCore;
using WebAuthAppItra.Data;

namespace WebAuthAppItra.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebAuthAppItraContext(
                serviceProvider.GetRequiredService<DbContextOptions<WebAuthAppItraContext>>()))
            {
                if (context.User.Any())
                {
                    return;
                }

                context.User.AddRange(
                    new User
                    {
                        Name = "Dima",
                        Email = "abc@mail.ru",
                        Password = "password",
                        //UserName = "Xxx",
                        RegistrationDate = DateTime.Now,
                        LastLoginTime = DateTime.Now,
                        IsBlocked = false,
                        IsChecked = false
                    },

                    new User
                    {
                        Name = "Andrey",
                        Email = "andrey@mail.ru",
                        Password = "password",
                        //UserName= "username",
                        RegistrationDate = DateTime.Now,
                        LastLoginTime = DateTime.Now,
                        IsBlocked = false,
                        IsChecked = false
                    }

                    );;
                context.SaveChanges();
            }
        }
    }
}
