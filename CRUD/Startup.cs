using Contracts.Repositories;
using Contracts.Service;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Repositories;
using Services;

namespace UsersCRUD
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // agregar servicios aquí

            services.AddDbContext<UserContext>(option => option.UseSqlServer(_configuration.GetConnectionString("CRUD_SQL")));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD_SQL", Version = "v1" });

                // We define the Security for authorization

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Header using Bearer Scheme"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement

            {

                {

                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }

            }

    });

            });

        }

        public void Configure(IApplicationBuilder app)
        {
            // configurar la aplicación aquí
        }
    }
}
