using System;

namespace Poke.Entities
{
    public class PokemonReference
    {
        // JSON deserialization is case insensitive - we could stick to the .NET naming convention
        public String Name { get; set; }
        public String Url { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}