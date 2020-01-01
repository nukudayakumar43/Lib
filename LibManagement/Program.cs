using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibManagementModel;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();

        }

        private static void CreateDbIfNotExists(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<LibManagementModel.LibManagementContext>();
                    context.Database.EnsureCreated();
                    //DbContext dbContext = new DbContext()

                    
                    Intialize(context);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static void Intialize(LibManagementContext context)
        {
            // Look for any students.
            if (context.BookDetails.Any())
            {
                return;   // DB has been seeded
            }
            var boook = new BookDetail[]
            {
                new BookDetail{BookName="sample"},
                new BookDetail{ BookName ="test"},

            };
            foreach (BookDetail s in boook)
            {
                context.BookDetails.Add(s);
            }

            

            var user = new UserDetail[]
            {
                new UserDetail{UserName = "Admin",Password="P@ssword!", EmailID="sample@gmail.com",UserDetailId=1,CreatedDateTime=DateTime.UtcNow},
                new UserDetail{UserName = "user1",Password="P@ssword!", EmailID="user1@gmail.com",UserDetailId=2,CreatedDateTime=DateTime.UtcNow},

            };

            foreach (UserDetail s in user)
            {
                context.UserDetails.Add(s);
            }

            context.SaveChanges();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
