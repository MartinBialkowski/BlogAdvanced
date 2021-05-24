using Autofac;
using BlogPost.Core.Entities;
using BlogPost.WebApi.Types;
using BlogPost.WebApi.Types.Student;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BlogPost.Services;
using BlogPost.Infrastructure;

namespace BlogPost.WebApi
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
            services.AddDbContextPool<BlogPostContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog Post - Advanced", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. To use, put the following phrase: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.AddAutoMapper(typeof(StudentMapping).GetTypeInfo().Assembly);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<StudentService>().AsImplementedInterfaces();
            builder.RegisterType<StudentRepository>().AsImplementedInterfaces();
            builder.RegisterType<CreateStudentValidator>().As<IValidator<CreateStudentRequest>>();
            builder.RegisterType<UpdateStudentValidator>().As<IValidator<UpdateStudentRequest>>();
            builder.RegisterType<ValidatorFactory>().As<IValidatorFactory>().SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                DatabaseInitializer.Initialize(app);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Post - Basic v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
