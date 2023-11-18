using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using SpeechBubble.Client.Helpers;
using SpeechBubble.Client.Services;
using SpeechBubble.Client.Services.Base;
using SpeechBubble.Client.ViewModels;
using System;
using System.Linq;
using System.Reflection;

namespace SpeechBubble.Client.Startup
{
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
            => ServiceProvider.GetService<MainWindow>().Show();
        
        private void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IChatService, ChatService>();

            // View Models
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<ChatViewModel>();
            services.AddTransient<RoomListViewModel>();
            services.AddTransient<LoginViewModel>();
            
            // Views
            services.AddTransient<MainWindow>();
        }
    }
}
