using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poke.Entities;

namespace Poke.Web
{
    public interface IPokemonServiceAsync
    {
        Task<IEnumerable<PokemonReference>> GetPokemonAsync(int count);
        Task<IEnumerable<PokemonReference>> GetPokemonAsync(int offset, int count);
    }
}