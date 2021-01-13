using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poke.Entities;

namespace Poke.Web
{
    public interface IPokemonServiceAsync
    {
        Task<IEnumerable<Pokemon>> GetPokemonAsync(int count);
        Task<IEnumerable<Pokemon>> GetPokemonAsync(int offset, int count);
    }
}