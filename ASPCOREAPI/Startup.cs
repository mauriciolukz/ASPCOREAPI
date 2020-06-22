using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCOREAPI.Middleware;
using ASPCOREAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;


namespace ASPCOREAPI
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
            services.AddControllers();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // Add database context
            services
                .AddDbContext<DynamicsContext>(options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseSqlServer(
                            Configuration.GetConnectionString("DynamicsContext"),
                            connectionOptions => connectionOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                        );
                    }
                );

            services
                .AddDbContext<DudeContext>(options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseSqlServer(
                            Configuration.GetConnectionString("DudeContext"),
                            connectionOptions => connectionOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                        );
                    }
                );

            // Add authentication schema
            services.AddTokenAuthentication(Configuration);
            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = IISDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    }
            //    )
            //    .AddJwtBearer("Bearer", options =>
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ClockSkew = TimeSpan.FromMinutes(5),
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"])),
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = true,
            //            ValidIssuer = Configuration["JWT:issuer"],
            //            ValidAudience = Configuration["JWT:audience"]
            //        }
            //    );
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseRouting();

            //JWT
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
