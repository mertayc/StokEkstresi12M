using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StokEkstresi.Business.Abstacts;
using StokEkstresi.Business.Concretes;
using StokEkstresi.DataAccess.Abstracts;
using StokEkstresi.DataAccess.Concretes.Contexts;
using StokEkstresi.DataAccess.Concretes.Repositories;
using System.IO;
using System.Windows;

namespace StokEkstresi.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            // Yeni bir ConfigurationBuilder nesnesi oluşturdum.
            // Uygulamanın çalıştığı dizinden appsettings.json dosyasını kullanacağını belirttim.
            // optional: false ile dosya bulunamazsa hata fırlatır zorunlu dosya.
            // reloadOnChange true ile dosya değiştiğinde otomatik olarak yapılandırmayı günceller.
            // Environmment'e göre farklı apsettingsler kullanıyorum.Test projesi olduğu için tek bir appsetting kullandım.
            // Çünkü her environment'e göre Database Connection String vs. farklı olabilir.
            // Bu nedenle appsettings.Development.json,apppsettings.Staging.json gibi farklı dosyalarla çalışıyorum.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<DapperContext>();
            services.AddScoped<IStokEkstresiService, StokEkstresiService>();
            services.AddScoped<IStiRepository, StiRepository>();
            services.AddScoped<IStkRepository, StkRepository>();
            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);



        }

    }
}