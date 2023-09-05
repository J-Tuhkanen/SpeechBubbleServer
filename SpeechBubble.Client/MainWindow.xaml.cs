using MahApps.Metro.Controls;
using SpeechBubble.Client.ViewModels;
using System.ComponentModel;

namespace SpeechBubble.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _viewModel.OnClosing(e);

            base.OnClosing(e);
        }
    }
}
