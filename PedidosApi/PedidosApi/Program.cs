
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PedidosApi.Aplicacion.Interfaces;
using PedidosApi.Aplicacion.Servicios;
using PedidosApi.Dominio.Interfaces;
using PedidosApi.Dominio.Persistencia.DbContextMigraciones;
using PedidosApi.Dominio.Persistencia.Interfaces;
using PedidosApi.Infraestructura.Repositorios;
using System.Text;
using System.Text.Json;

namespace PedidosApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema de Pedidos", Version = "v1" });

                // Configuracion para la autenticacion con JWT en Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new List<string>()
        }
    });
            });



    //        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //.AddJwtBearer(options =>
    //{
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuerSigningKey = true,
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
    //        ValidateIssuer = false,
    //        ValidateAudience = false
    //    };
    //});

            builder.Services.AddAuthorization(options => {
                options.AddPolicy("SuperAdmin", policy => policy.RequireClaim("AdminType", "Admin"));
            });


            builder.Services.AddSqlServer<PedidoDbContext>(builder.Configuration.GetConnectionString("Dev"));

            builder.Services.AddScoped<IPedidoDbContext, PedidoDbContext>();

            builder.Services.AddScoped<IClienteService, ClienteService>();

            builder.Services.AddScoped<IPedidoService, PedidoService>();
            builder.Services.AddScoped<IProductoService, ProductoService>();

            builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>(); 




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Use CORS
                app.UseCors("AllowSwaggerUI");

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contabilidad");
                    c.OAuthClientId("swagger");
                    c.OAuthClientSecret("secret");
                    c.OAuthRealm("realm");
                    c.OAuthAppName("Mi API");
                });
            }
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (DbUpdateException ex)
                {
                    context.Response.StatusCode = 400;
                    var result = JsonSerializer.Serialize(new
                    {
                        Mensaje = ex.Message
                    });
                    await context.Response.WriteAsync(result);
                }

                catch (Exception ex)
                {

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new
                    {
                        Mensaje = $"Ah ocurrido un error inesperado en el servidor, por favor contactar al administrador del sistema. ({ex.Message})"
                    });

                    await context.Response.WriteAsync(result);
                }


                if (context.Response.StatusCode == 401)
                {
                    var result = JsonSerializer.Serialize(new { Mensaje = "No se ha autenticado para realizar este proceso." });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }
                else if (context.Response.StatusCode == 403)
                {
                    var result = JsonSerializer.Serialize(new { Mensaje = "No tienes permiso para realizar esta acción." });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }
            });


            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
