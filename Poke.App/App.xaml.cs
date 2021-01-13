using System.Windows;
using Poke.Web;

namespace Poke.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Fuckup Point
            var pokemonService = new PokemonServiceAsync();
            var dataContext = new PokemonViewModel(pokemonService);
            
            MainWindow = new MainWindow { DataContext = dataContext };
            MainWindow.Show();
        }
    }
}
