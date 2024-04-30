using InventoryManagementCore.Application.Interfaces;
using InventoryManagementCore.Application.Repos;
using InventoryManagementCore.Infrastructure.Services;

namespace InventoryManagementCore
{
    public static class ProjectDepandancyCollection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IDbContext, DbContext>()
                .AddSingleton(typeof(IAuditedCollection<>), typeof(AuditedCollection<>));

        }
        public static IServiceCollection AddProjectRepository(this IServiceCollection services)
        {
            return services
                .AddTransient<IUserRepo, UserRepo>()
                ;
            //.AddTransient<IUserRepository, UserRepository>();
        }
        public static IServiceCollection AddProjectQueries(this IServiceCollection services)
        {
            return services;
        }
    }
}
