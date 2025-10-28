namespace Web_TagHelper;
    using Web_MehrTabellen.DAL;
using Web_MehrTabellen.DAL.Artikel;

    public class Program
    {
        public static void Main(string[] args)
        {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IArtikelRepository, ArtikelRepository>();
        builder.Services.AddScoped<IKategorieRepository, KategorieRepository>();

        builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
