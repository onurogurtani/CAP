using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Sample.Kafka.PostgreSql
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCap(x =>
            {
                //docker run --name postgres -p 5432:5432 -e POSTGRES_PASSWORD=mysecretpassword -d postgres
                x.UsePostgreSql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres;");
                
                //docker run --name kafka -p 9092:9092 -d bashj79/kafka-kraft
                x.UseKafka("localhost:9092");
                x.UseDashboard(opt => { opt.PathMatch = "/eventbus"; });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Title v1");
            });
        }
    }
}