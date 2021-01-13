using System;

namespace Poke.Entities
{
    public class Pokemon
    {
        // {"name":"ivysaur","url":"https://pokeapi.co/api/v2/pokemon/2/"}

        public String Name { get; set; }
        public String Url { get; set; }

        public override string ToString()
        {
            return Name ?? "UNDEFINED";
        }
    }
}