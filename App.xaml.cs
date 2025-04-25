using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using ProfessorsSSU.Data;
using ProfessorsSSU.Interfaces;
using ProfessorsSSU.Services;
using Microsoft.EntityFrameworkCore;

namespace ProfessorsSSU
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IServiceProvider _serviceProvider;

        protected void OnStartup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(e);
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configure Logging
            services.AddLogging();

            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=professors.db"));
            // Register Services
            services.AddSingleton<IProfessorService, ProfessorService>();

            //// Register ViewModels
            //services.AddSingleton<IMainViewModel, MainViewModel>();
            // Register Views
            services.AddSingleton<MainWindow>();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            // Dispose of services if needed
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

    }

}
