using System;
using System.Collections.Generic;
using System.Text;

namespace Poke.Entities
{
    public class PokemonPage
    {
        // JSON deserialization is case insensitive - we could stick to the .NET naming convention
        public int Count { get; set; }
        public String Next { get; set; }
        public String Previous { get; set; }
        public List<PokemonReference> Results { get; set; }

    }
}
