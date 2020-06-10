using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using examenapi.Contexts;
using examenapi.Entities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace examenapi
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
            #region [ Conexiones a las bases de datos ]
            // Cadena de conexi�n general
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("cnns")));
            // Cadena de conexi�n seguridad
            services.AddDbContext<SecurityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("scnn")));
            #endregion 

            // Esta confirguraci�n es para evitar los problemas de deserializaci�n por una referencia ciclica
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #region [ Seguridad y autenticaci�n ]
            // Configuraci�n de la autenticaci�n
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<SecurityDbContext>()
               .AddDefaultTokenProviders();

            //Agregar autenticaci�n por Jason Web Token
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(); // B�sica
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                     ClockSkew = TimeSpan.Zero
                 });
            #endregion

            services.AddResponseCaching();// Habilitar manejo de cache

            // Automapeo entre clases DTO y Entidades
            //services.AddAutoMapper(configuration =>
            //{
            //    configuration.CreateMap<Persona, PersonaDTO>();
            //    configuration.CreateMap<Persona, PersonaRFC_DTO>();
            //    configuration.CreateMap<PersonaCDTO, Persona>().ReverseMap();
            //}, typeof(Startup));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseResponseCaching();//Manejo de cache

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();

            // Configuraci�n del CORS a nivel aplicaci�n  
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               );


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
