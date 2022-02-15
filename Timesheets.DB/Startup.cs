using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Timesheets.DB.DAL.Context;
using Timesheets.DB.DAL.Implementation;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.DB
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSingleton<IPersonRepo, PersonRepo>();
            var connectionString = Configuration.GetConnectionString("MyDbConnection");
            services.AddDbContext<MyDbContext>(options => options.UseSqlite(connectionString));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timesheets.DB", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timesheets.DB v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
