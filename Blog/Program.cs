using Blog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {

            //seed admin
            var scope = host.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //handles all user accounts
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //handles all roles for users
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            dbContext.Database.EnsureCreated();

            //create admin
            var adminRole = new IdentityRole("Admin");

            if (!dbContext.Roles.Any())
            {
                //create a role
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if(!dbContext.Users.Any(u => u.UserName == "admin"))
            {
                //create an admin
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                //add admin role to user
                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
