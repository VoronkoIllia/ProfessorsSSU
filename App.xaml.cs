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
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<InfoProfessorForm>();
            //var loginForm = _serviceProvider.GetRequiredService<LogInForm>();
            //var editProfessorForm = _serviceProvider.GetRequiredService<EditProfessorForm>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configure Logging
            services.AddLogging();

            // Register DB Context
            services.AddDbContext<AppDbContext>();

            // Register Services
            services.AddSingleton<IProfessorService, ProfessorService>();
            services.AddSingleton<IAuthService, AuthService>();

            // Register Views
            services.AddSingleton<InfoProfessorForm>();
            //services.AddSingleton<LogInForm>();
            //services.AddSingleton<EditProfessorForm>();
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
