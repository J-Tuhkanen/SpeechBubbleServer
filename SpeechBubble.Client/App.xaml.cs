using SpeechBubble.Client.Startup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SpeechBubble.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Bootstrapper _bootstrapper;

        public App()
        {
            _bootstrapper = new Bootstrapper();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _bootstrapper.OnStart();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Error: " + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
    }
}
