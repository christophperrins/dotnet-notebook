using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;

using src.Data;
using Microsoft.EntityFrameworkCore;
using src.Dto;
using src.Persistence.Repository;
using src.Services;
using src.Persistence.Model;
using src.Configuation;

namespace src
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

            var mapper = new ApplicationConfiguration().CreateMapper();
            services.AddTransient<IMapper>(sp => mapper);
            services.AddTransient<IRepository<Note>, Repository<Note>>();
            services.AddTransient<IRepository<NoteBook>, Repository<NoteBook>>();
            services.AddTransient<NoteService>();
            services.AddTransient<NoteBookService>();

            services.AddDbContext<ApplicationContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql")));
            services.AddControllers();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
