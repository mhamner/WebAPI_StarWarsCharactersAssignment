using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI_StarWarsCharactersAssignment.Models;
using WebAPI_StarWarsCharactersAssignment.BusinessLogic;


namespace WebAPI_StarWarsCharactersAssignment.Controllers
{
    /// <summary>
    /// Performs various actions on Star Wars Characters
    /// </summary>
    public class StarWarsCharactersController : ApiController
    {
        List<StarWarsCharacterModel> starWarsCharacters = new List<StarWarsCharacterModel>();
        /// <summary>
        ///  Populate some dummy data
        /// </summary>
        public StarWarsCharactersController()
        {
            //Uncomment the 2 lines below to populate dummy data
            ////CharacterBL characterBL = new CharacterBL();
            ////starWarsCharacters = characterBL.PopulateDummyCharacterData();
        }
        /// <summary>
        /// Returns a list of all Star Wars Characters
        /// </summary>
        /// <returns>List of Star Wars Characters</returns>
        public List<StarWarsCharacterModel> Get()        {

            CharacterBL characterBL = new CharacterBL();
            return characterBL.GetAllStarWarsCharacters();
        }

        /// <summary>
        /// Returns a specific Star Wars Character by ID
        /// </summary>
        /// <param name="id">ID of the Star Wars Character</param>
        /// <returns> Star Wars Character</returns>
        public StarWarsCharacterModel Get(int id)
        {
            return starWarsCharacters.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Accepts the  Star Wars Character name and returns their allegiance
        /// </summary>
        /// <param name="characterName"> Star Wars Character Name</param>
        /// <returns>Allegiance of the character</returns>
        [Route("api/StarWarsCharacters/GetAllegianceByCharacterName")]
        public string GetAllegianceByCharacterName(string characterName)
        {
            StarWarsCharacterModel starWarsCharacterModel = starWarsCharacters.Where(x => x.Name == characterName).FirstOrDefault();
            return starWarsCharacterModel.Allegiance;
        }

        /// <summary>
        /// Returns all  Star Wars Characters that are Jedi
        /// </summary>
        /// <returns>List of  Star Wars Characters</returns>
        [Route("api/StarWarsCharacters/GetAllJedi")]
        public List<StarWarsCharacterModel> GetAllJedi()
        {
            //Uncomment the line below to return by searching the dummy data
            return starWarsCharacters.Where(x => x.IsJedi == true).ToList();
        }

        /// <summary>
        /// Accepts a Trilogy Name and returns a list of  Star Wars Characters introduced in that Trilogy
        /// </summary>
        /// <param name="trilogy"></param>
        /// <returns>List of  Star Wars Characters</returns>
        [Route("api/StarWarsCharacters/GetCharactersByTrilogy")]
        public List<StarWarsCharacterModel> GetCharactersByTrilogy(string trilogy)
        {
            CharacterBL characterBL = new CharacterBL();
            return characterBL.GetAllStarWarsCharactersByTrilogy(trilogy);
            //Uncomment the line below to get the info. from the dummy data
            ////return starWarsCharacters.Where(x => x.IntroducedInTrilogy == trilogy).ToList();
        }

        /// <summary>
        /// Accepts a Character Name and returns the info. on that Character
        /// </summary>
        /// <param name="name">Character Name</param>
        /// <returns>Star Wars Character</returns>
        [Route("api/StarWarsCharacters/GetCharacterByName")]
        public StarWarsCharacterModel GetCharacterInfoByName(string name)
        {
            CharacterBL characterBL = new CharacterBL();
            return characterBL.GetStarWarsCharacterByName(name);
        }

        /// <summary>
        /// Accepts an Allegiance and returns all the characters with that Allegiance
        /// </summary>
        /// <param name="allegiance">Allegiance of the character</param>
        /// <returns>List of Star Wars Characters</returns>
        [Route("api/StarWarsCharacters/GetCharactersByAllegiance")]
        public List<StarWarsCharacterModel> GetCharactersByAllegiance(string allegiance)
        {
            CharacterBL characterBL = new CharacterBL();
            return characterBL.GetAllStarWarsCharactersByAllegiance(allegiance);
        }

        /// <summary>
        /// Adds a new  Star Wars Character
        /// </summary>
        /// <param name="starWarsCharacter">Star Wars Character Info.</param>
        public void Post(StarWarsCharacterModel starWarsCharacter)
        {
            CharacterBL characterBL = new CharacterBL();
            characterBL.AddNewStarWarsCharacter(starWarsCharacter);

            //Umcomment the line below to add the character to the dummy data
            ////starWarsCharacters.Add(starWarsCharacter);
        }

    }
}