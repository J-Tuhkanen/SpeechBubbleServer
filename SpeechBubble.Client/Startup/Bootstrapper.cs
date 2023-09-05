using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using SpeechBubble.Client.Helpers;
using SpeechBubble.Client.ViewModels;
using System;
using System.Linq;
using System.Reflection;

namespace SpeechBubble.Client.Startup
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterAsImplementedInterfaces<TService>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var interfaces = typeof(TService).GetTypeInfo().ImplementedInterfaces.Where(_ => _ != typeof(IDisposable) && _.IsPublic);

            foreach (Type interfaceType in interfaces)
            {
                services.Add(new ServiceDescriptor(interfaceType, typeof(TService), lifetime));
            }
        }

        public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            services.AddSingleton<Func<TService>>(_ => () => _.GetService<TService>());
        }

        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            //services.RegisterAsImplementedInterfaces<LookupDataService>(ServiceLifetime.Transient);
            //services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddUserInterfaceServices(this IServiceCollection services)
        {
            // Singleton services
            services.AddSingleton<IEventAggregator, EventAggregator>();

            // Transitient services
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<ChatViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainWindow>();

            return services;
        }
    }

    public class Bootstrapper
    {
        public Bootstrapper()
        {
            DispatcherHelper.Initialize();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; }

        public void OnStart()
        {
            // Migrate the database.
            //ServiceProvider.GetService<ProductsDBContext>().Database.Migrate();

            // Show the main window.
            ServiceProvider.GetService<MainWindow>().Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDataAccessServices();
            services.AddDataServices();
            services.AddUserInterfaceServices();
        }
    }
}
