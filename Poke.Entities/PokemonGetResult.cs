using System;
using System.Collections.Generic;
using System.Text;

namespace Poke.Entities
{
    public class PokemonGetResult
    {
        public int Count { get; set; }
        public String Next { get; set; }
        public String Previous { get; set; }
        public List<Pokemon> Results { get; set; }

    }
}
