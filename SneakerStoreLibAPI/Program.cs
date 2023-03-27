using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.ServiceModel;
using System;
using SneakerStoreAPI.Data;
using SneakerStoreAPI.Repositories;

namespace SneakerStoreAPI
{
    public class Program
    {
        private static string _cs = String.Empty;

        public static string WebAPITag = String.Empty;
        public static string WebAPIVersao = String.Empty;
        public static string Secret = String.Empty;
        public static string Audience = String.Empty;
        public static string Issuer = String.Empty;
        public static bool IsIISExpress;
        public static string AmbienteConnect = String.Empty;
        public static string KeyConnect = String.Empty;



        public IConfiguration Configuration { get; }


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Lê configurações
            //Código que obtém o nome real da aplicação
            WebAPITag = AppDomain.CurrentDomain.FriendlyName;
            //Tenta definir esse nome como fonte de dados
            WebAPITag = Model.Logs.LogErros.CreateEventSource(WebAPITag);
            //Versionamento
            WebAPIVersao = builder.Configuration["Versionamento:Versao"];
            AmbienteConnect = builder.Configuration["Connect:Ambiente"];
            KeyConnect = builder.Configuration["ConnectionStrings:SneakerStoreDB"];
            Secret = builder.Configuration["Token:Secret"];
            Audience = builder.Configuration["Token:Audience"];
            Issuer = builder.Configuration["Token:Issuer"];
            string nomeProcesso = Process.GetCurrentProcess().ProcessName;
            IsIISExpress = string.Compare(nomeProcesso, "iisexpress") == 0;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCors();

            builder.Services.AddScoped<DbSession>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            //Ativando o suporte a documentação XML
            builder.Services.AddSwaggerGen(c =>
            {
                //c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API SneakerStore", Version = "v1" });

                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "SneakerStore.xml");
                //c.IncludeXmlComments(filePath);

                //Ativando o envio do cabeçalho de autorização em métodos protegidos
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.  Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
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
                            new string[] {}
                    }
                });

            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidAudience = builder.Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Token:Secret"])),
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "API SneakerStore"));
            //}

            app.UseHttpsRedirection();

            //Ativação do suporte a CORS
            app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }

        
    }
}