﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using open_life_server.V1;
using open_life_server.V1.Goals;
using open_life_server.V1.Goals.HabitGoals;
using open_life_server.V1.Goals.ListGoals;
using open_life_server.V1.Goals.NumberGoals;
using open_life_server.V1.Users;
using Swashbuckle.AspNetCore.Swagger;

namespace open_life_server
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
            //Database
            services.AddDbContext<OpenLifeContext>(options => options.UseNpgsql(GetRdsConnectionString()));

            //Validators
            services.AddTransient<IUserValidator, UserValidator>();
            services.AddTransient<IGoalValidator, GoalValidator>();
            services.AddTransient<IHabitGoalValidator, HabitGoalValidator>();
            services.AddTransient<IListGoalValidator, ListGoalValidator>();
            services.AddTransient<INumberGoalValidator, NumberGoalValidator>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info{ Title = "Open Life API", Version = "v1" });
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddCors(options =>
            {
                options.AddPolicy("dev",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                    });

                options.AddPolicy("prod",
                    builder =>
                    {
                        builder.WithOrigins("https://beta.myopen.life").AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }

        private string GetRdsConnectionString()
        {
            var dbname = Environment.GetEnvironmentVariable("RDS_DB_NAME");

            if (string.IsNullOrEmpty(dbname)) return null;

            var username = Environment.GetEnvironmentVariable("RDS_USERNAME");
            var password = Environment.GetEnvironmentVariable("RDS_PASSWORD");
            var hostname = Environment.GetEnvironmentVariable("RDS_HOSTNAME");

            return "Host=" + hostname + "; Database=" + dbname + "; Username=" + username + "; Password=" + password + ";";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("dev");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseCors("prod");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
