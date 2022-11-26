using ControleContatos.Data;
using ControleContatos.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;

namespace ControleContatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;
            IServiceCollection services = builder.Services;
            // Add services to the container.

            builder.Services.AddControllersWithViews();
            //builder.Services.AddEntityFrameworkSqlServer()
            //    .AddDbContext<BancoContext>(w => w.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));


            services.AddDbContext<BancoContext>((DbContextOptionsBuilder options) =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DataBase"]);
            });

            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}