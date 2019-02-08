using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Terakoya.Data;
using Microsoft.AspNetCore.Http;

namespace Terakoya
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(config =>
            {
                config.AddProfile<ApplicationProfile>();
            });
            services.AddSingleton<IMapper, Mapper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("default"),
                    builder => builder.MigrationsAssembly(typeof(Startup).Assembly.FullName));
            }).AddUnitOfWork<ApplicationDbContext>();

            // Add Swagger setting
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(
                    "v1",
                    new Info()
                    {
                        Title = "Terakoya",
                        Version = "v1",
                        Description = "Terakoya client web api",
                        Contact = new Contact()
                        {
                            Email = "osamu.mitsuhashi224@gmail.com",
                            Name = "omitsuhashi"
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI(option =>
                {
                    option.SwaggerEndpoint("/swagger/v1/swagger.json", "Terakoya API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
