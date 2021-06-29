using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, 
            Action<TContext, IServiceProvider> seeder, 
            int? retry = 0) where TContext: DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database...");

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Database migration finished.");
                }
                catch (SqlException)
                {
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(5000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                    throw;
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, 
            TContext context, 
            IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
