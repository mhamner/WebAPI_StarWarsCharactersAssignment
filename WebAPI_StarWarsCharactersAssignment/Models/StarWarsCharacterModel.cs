using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_StarWarsCharactersAssignment.Models
{
    public class StarWarsCharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Allegiance { get; set; }
        public bool IsJedi { get; set; }
        public string IntroducedInTrilogy { get; set; }
    }
}