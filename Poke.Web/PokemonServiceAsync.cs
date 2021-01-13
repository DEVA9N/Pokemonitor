using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Poke.Entities;

namespace Poke.Web
{
    public sealed class PokemonServiceAsync : IPokemonServiceAsync
    {
        private static readonly HttpClient Client = new HttpClient();
        private static String BaseUri = "https://pokeapi.co/api/v2/";

        static PokemonServiceAsync()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
        }

        public async Task<IEnumerable<PokemonReference>> GetPokemonAsync(int count = 20)
        {
            return await GetPokemonAsync(0, count);
        }

        public async Task<IEnumerable<PokemonReference>> GetPokemonAsync(int offset, int count)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return await GetPokemonAsyncImpl(offset, count);
        }

        private static async Task<IEnumerable<PokemonReference>> GetPokemonAsyncImpl(int offset, int count)
        {
            var request = $"pokemon?offset={offset}&limit={count}";
            var response = await Client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Log request and response
                return await Task.FromResult(Enumerable.Empty<PokemonReference>());
            }

            // Extension method from the Microsoft.AspNet.WebApi.Client nuget package
            var content = await response.Content.ReadAsAsync<PokemonPage>();

            return content.Results;
        }
    }
}
