using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_StarWarsCharactersAssignment.Models;
using DataLibrary.DTOs;
using DataLibrary.Repositories;

namespace WebAPI_StarWarsCharactersAssignment.BusinessLogic
{
    public class CharacterBL
    {
        public List<StarWarsCharacterModel> PopulateDummyCharacterData()
        {
            List<StarWarsCharacterModel> starWarsCharacters = new List<StarWarsCharacterModel>();

            StarWarsCharacterModel character = new StarWarsCharacterModel()
            {
                Id = 1,
                Name = "Luke Skywalker",
                Allegiance = "Rebellion",
                IsJedi = true,
                IntroducedInTrilogy = "Original"
            };
            starWarsCharacters.Add(character);           
          
            character = new StarWarsCharacterModel()
            {
                Id = 2,
                Name = "Obi-Wan Kenobi",
                Allegiance = "Rebellion",
                IsJedi = true,
                IntroducedInTrilogy = "Original"
            };
            starWarsCharacters.Add(character);

            character = new StarWarsCharacterModel()
            {
                Id = 3,
                Name = "Jar Jar Binks",
                Allegiance = "None",
                IsJedi = false,
                IntroducedInTrilogy = "Prequel"
            };
            starWarsCharacters.Add(character);

            character = new StarWarsCharacterModel()
            {
                Id = 4,
                Name = "Poe Dameron",
                Allegiance = "Rebellion",
                IsJedi = false,
                IntroducedInTrilogy = "Sequel"
            };
            starWarsCharacters.Add(character);

            character = new StarWarsCharacterModel()
            {
                Id = 5,
                Name = "Finn",
                Allegiance = "Rebellion",
                IsJedi = false,
                IntroducedInTrilogy = "Sequel"
            };
            starWarsCharacters.Add(character);

            character = new StarWarsCharacterModel()
            {
                Id = 6,
                Name = "Rey Skywalker",
                Allegiance = "Rebellion",
                IsJedi = true,
                IntroducedInTrilogy = "Sequel"
            };
            starWarsCharacters.Add(character);

            character = new StarWarsCharacterModel()
            {
                Id = 7,
                Name = "C-3PO",
                Allegiance = "Rebellion",
                IsJedi = false,
                IntroducedInTrilogy = "Original"
            };
            starWarsCharacters.Add(character);

            character = new StarWarsCharacterModel()
            {
                Id = 8,
                Name = "R2-D2",
                Allegiance = "Rebellion",
                IsJedi = false,
                IntroducedInTrilogy = "Original"
            };
            starWarsCharacters.Add(character);

            return starWarsCharacters;
        }

        public List<StarWarsCharacterModel> GetAllStarWarsCharacters()
        {
            List<StarWarsCharacterModel> starWarsCharacterModels = new List<StarWarsCharacterModel>();

            StarWarsCharacterRepository starWarsCharacterRepository = new StarWarsCharacterRepository();
            List<StarWarsCharacterDTO> starWarsCharacterDTOs = starWarsCharacterRepository.GetAllCharacterInfo();

            //Convert the DTOs to the Models and add them to the List<> of models and return the List<> of models       
            return ConvertCharacterDtoListToModelList(starWarsCharacterDTOs);
        }

        public StarWarsCharacterModel GetStarWarsCharacterByName(string name)
        {
            List<StarWarsCharacterModel> starWarsCharacterModels = new List<StarWarsCharacterModel>();

            StarWarsCharacterRepository starWarsCharacterRepository = new StarWarsCharacterRepository();
            StarWarsCharacterDTO starWarsCharacterDTO = starWarsCharacterRepository.GetCharacterInfoByName(name);

            //Convert the DTO to the Model and return the model
            return ConvertCharacterDtoToModel(starWarsCharacterDTO);
        }

        public List<StarWarsCharacterModel> GetAllStarWarsCharactersByAllegiance(string allegiance)
        {
            List<StarWarsCharacterModel> starWarsCharacterModels = new List<StarWarsCharacterModel>();

            StarWarsCharacterRepository starWarsCharacterRepository = new StarWarsCharacterRepository();
            List<StarWarsCharacterDTO> starWarsCharacterDTOs = starWarsCharacterRepository.GetCharacterInfoByAllegiance(allegiance);

            //Convert the DTOs to the Models and add them to the List<> of models and return the List<> of models       
            return ConvertCharacterDtoListToModelList(starWarsCharacterDTOs);
        }

        public List<StarWarsCharacterModel> GetAllStarWarsCharactersByTrilogy(string trilogy)
        {
            List<StarWarsCharacterModel> starWarsCharacterModels = new List<StarWarsCharacterModel>();

            StarWarsCharacterRepository starWarsCharacterRepository = new StarWarsCharacterRepository();
            List<StarWarsCharacterDTO> starWarsCharacterDTOs = starWarsCharacterRepository.GetCharactersByTrilogy(trilogy);

            //Convert the DTOs to the Models and add them to the List<> of models and return the List<> of models       
            return ConvertCharacterDtoListToModelList(starWarsCharacterDTOs);
        }

        public void AddNewStarWarsCharacter(StarWarsCharacterModel sm)
        {
            StarWarsCharacterRepository starWarsCharacterRepository = new StarWarsCharacterRepository();

            //Note:  I'm passing the fields here rather than the model because I don't want to make the Repository "know" about the Models, only the DTOs
            //  and the DTO in this instance mirrors the data, so it has the Trilogy as the ID int, so I have a method in the repository that goes and gets that
            //  int from the DB using the Trilogy Name
            starWarsCharacterRepository.InsertNewCharacter(sm.Name, sm.Allegiance, sm.IsJedi, sm.IntroducedInTrilogy);
        }

        private StarWarsCharacterModel ConvertCharacterDtoToModel(StarWarsCharacterDTO starWarsCharacterDTO)
        {
            StarWarsCharacterModel sm = new StarWarsCharacterModel();
            sm.Id = starWarsCharacterDTO.CharacterId;
            sm.Name = starWarsCharacterDTO.Name;
            sm.Allegiance = starWarsCharacterDTO.Allegiance;
            sm.IsJedi = starWarsCharacterDTO.IsJedi;
            sm.IntroducedInTrilogy = starWarsCharacterDTO.TrilogyName;

            return sm;
        }

        private List<StarWarsCharacterModel> ConvertCharacterDtoListToModelList(List<StarWarsCharacterDTO> starWarsCharacterDTOs)
        {
            List<StarWarsCharacterModel> starWarsCharacterModels = new List<StarWarsCharacterModel>();

            //Convert the DTOs to the Models and add them to the List<> of models         
            foreach (StarWarsCharacterDTO starWarsCharacterDTO in starWarsCharacterDTOs)
            {
                starWarsCharacterModels.Add(ConvertCharacterDtoToModel(starWarsCharacterDTO));
            }

            return starWarsCharacterModels;
        }
    }
}