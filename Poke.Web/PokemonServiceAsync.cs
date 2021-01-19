using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Poke.Entities;
using RestSharp;

namespace Poke.Web
{
    public sealed class PokemonServiceAsync : IPokemonServiceAsync
    {
        private static readonly RestClient Client;

        static PokemonServiceAsync()
        {
            Client = new RestClient(@"https://pokeapi.co/api/v2/");
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
            var request = new RestRequest($"pokemon?offset={offset}&limit={count}");
            var response = await Client.GetAsync<PokemonPage>(request);
             
            return response.Results;
        }
    }
}
