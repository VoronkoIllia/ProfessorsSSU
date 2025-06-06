﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using ProfessorsSSU.Data;
using ProfessorsSSU.Interfaces;
using ProfessorsSSU.Services;

namespace ProfessorsSSU
{
    public partial class App : Application
    {

        private IServiceProvider _serviceProvider;

        protected void OnStartup(object sender, StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<InfoProfessorForm>();
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
            services.AddSingleton<IWordService, WordService>();

            // Register Views
            services.AddSingleton<InfoProfessorForm>();
            
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
