using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Poke.Entities;

namespace Poke.Web
{
    public sealed class PokemonServiceAsync : IPokemonServiceAsync
    {
        private static readonly HttpClient Client = new HttpClient();

        public async Task<IEnumerable<Pokemon>> GetPokemonAsync(int count = 20)
        {
            return await GetPokemonAsync(0, count);
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonAsync(int offset, int count)
        {
            var response = await Client.GetAsync($"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit={count}");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content. ReadAsAsync<Product>();
            }
            return product;

        }
    }
}
