using System.Windows;
using Poke.Web;

namespace Poke.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var service = new PokemonServiceAsync();
            var viewModel = new PokemonViewModel(service);
            
            MainWindow = new MainWindow { DataContext = viewModel };
            MainWindow.Show();
        }
    }
}
