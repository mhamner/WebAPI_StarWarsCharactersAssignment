using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DTOs
{
    public class StarWarsCharacterDTO
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Allegiance { get; set; }
        public bool IsJedi { get; set; }
        public string TrilogyName { get; set; }
    }
}
