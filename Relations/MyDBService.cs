using Microsoft.Extensions.DependencyInjection;

namespace GymSite.Relations
{
    public static class MyDBService
    {
        public static void AddMyDBService(this IServiceCollection services, MyDBType dbType, string conn)
        {
            services.AddTransient<MyDBContext>(x => new MyDBContext(new MyDBUse(dbType,
                conn)));
        }
    }
}