using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poke.App.LazyLoading;
using Poke.App.Mvvm;
using Poke.Entities;
using Poke.Web;

namespace Poke.App
{
    class PokemonViewModel : ViewModelBase, ILazyLoadAsync
    {
        private readonly IPokemonServiceAsync _service;
        private readonly ObservableCollection<Pokemon> _pokemon;

        public ICollection<Pokemon> Pokemon => _pokemon;

        public PokemonViewModel(IPokemonServiceAsync service)
        {
            _service = service;
            _pokemon = new ObservableCollection<Pokemon>();

            Title = "Pokemon";
        }

        public async Task InitializeAsync(int pageSize)
        {
            var initialItems = (await _service.GetPokemonAsync(pageSize)).ToList();

            initialItems.ForEach(p => _pokemon.Add(p));
        }

        public async Task LoadNextAsync(int pageSize)
        {
            var nextItems = (await _service.GetPokemonAsync(_pokemon.Count, pageSize)).ToList();

            nextItems.ToList().ForEach(p => _pokemon.Add(p));
        }
    }
}
