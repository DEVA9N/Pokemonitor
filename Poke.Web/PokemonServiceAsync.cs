using System;
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
                // Extension method from the Microsoft.AspNet.WebApi.Client nuget package
                var content = await response.Content.ReadAsAsync<PokemonGetResult>();

                return content.Results;
            }

            return await Task.FromResult(new List<Pokemon>());
        }
    }
}
