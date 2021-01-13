using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Poke.Entities;

namespace Poke.Web
{
    public sealed class PokemonServiceAsync : IPokemonServiceAsync
    {
        private static readonly HttpClient Client = new HttpClient();
        private static String BaseUri = "https://pokeapi.co";

        public async Task<IEnumerable<PokemonReference>> GetPokemonAsync(int count = 20)
        {
            return await GetPokemonAsync(0, count);
        }

        public async Task<IEnumerable<PokemonReference>> GetPokemonAsync(int offset, int count)
        {
            var response = await Client.GetAsync($"{BaseUri}/api/v2/pokemon?offset={offset}&limit={count}");

            if (!response.IsSuccessStatusCode)
            {
                return await Task.FromResult(Enumerable.Empty<PokemonReference>());
            }

            // Extension method from the Microsoft.AspNet.WebApi.Client nuget package
            var content = await response.Content.ReadAsAsync<PokemonPage>();

            return content.Results;

        }
    }
}
