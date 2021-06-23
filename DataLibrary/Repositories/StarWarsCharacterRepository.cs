using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using DataLibrary.DataAccessLayer;
using DataLibrary.DTOs;

namespace DataLibrary.Repositories
{
    public class StarWarsCharacterRepository
    {
        public List<StarWarsCharacterDTO> GetAllCharacterInfo()
        {           
            //Create a new instance of our DAL, passing in the connection string
            DataAccess dal = new DataAccess(ConfigurationManager.ConnectionStrings["StarWarsConnection"].ConnectionString);

            //Create a Dictionary (key, value pairs) of paramaters and values for our stored proc to use in our DAL
            //This one will be empty because we are getting everything
            Dictionary<string, object> paramDictionary = new Dictionary<string, object>();

            //Call our DAL method       
            DataTable characterTable = dal.PopulateDataTableViaStoredProcedure("spGetAllCharacterInfo", paramDictionary);

            //Loop through our data table and populate the Star Wars DTO, return the resulting List of DTOs
            return PopulateDTOListFromDataTable(characterTable);
        }

        public StarWarsCharacterDTO GetCharacterInfoByName(string name)
        {
            //Create a new instance of our DAL, passing in the connection string
            DataAccess dal = new DataAccess(ConfigurationManager.ConnectionStrings["StarWarsConnection"].ConnectionString);

            //Create a Dictionary (key, value pairs) of paramaters and values for our stored proc to use in our DAL
            Dictionary<string, object> paramDictionary = new Dictionary<string, object>();
            paramDictionary.Add("@Name", name);

            //Call our DAL method        
            DataTable characterTable = dal.PopulateDataTableViaStoredProcedure("spGetCharacterInfoByName", paramDictionary);

            //Loop through our data table and populate the Star Wars DTO, returning the result (will only have one, this the [0])
            return PopulateDTOListFromDataTable(characterTable)[0];
        }

        public List<StarWarsCharacterDTO> GetCharacterInfoByAllegiance(string allegiance)
        {
            //Create a new instance of our DAL, passing in the connection string
            DataAccess dal = new DataAccess(ConfigurationManager.ConnectionStrings["StarWarsConnection"].ConnectionString);

            //Create a Dictionary (key, value pairs) of paramaters and values for our stored proc to use in our DAL
            Dictionary<string, object> paramDictionary = new Dictionary<string, object>();
            paramDictionary.Add("@Allegiance", allegiance);

            //Call our DAL method           
            DataTable characterTable = dal.PopulateDataTableViaStoredProcedure("spGetCharacterInfoByAllegiance", paramDictionary);

            //Loop through our data table and populate the Star Wars DTO, returning the result (will only have one, this the [0])
            return PopulateDTOListFromDataTable(characterTable);
        }

        public List<StarWarsCharacterDTO> GetCharactersByTrilogy(string trilogy)
        {
            //Create a new instance of our DAL, passing in the connection string
            DataAccess dal = new DataAccess(ConfigurationManager.ConnectionStrings["StarWarsConnection"].ConnectionString);

            //Create a Dictionary (key, value pairs) of paramaters and values for our stored proc to use in our DAL
            Dictionary<string, object> paramDictionary = new Dictionary<string, object>();
            paramDictionary.Add("@Trilogy", trilogy);

            //Call our DAL method to get a list of books by author           
            DataTable characterTable = dal.PopulateDataTableViaStoredProcedure("spGetCharacterInfoByTrilogy", paramDictionary);

            //Loop through our data table and populate the Star Wars DTO, returning the results
            return PopulateDTOListFromDataTable(characterTable);
        }

        public void InsertNewCharacter(string name, string allegiance, bool isJedi, string introducedInTrilogy)
        {
            //Create a new instance of our DAL, passing in the connection string
            DataAccess dal = new DataAccess(ConfigurationManager.ConnectionStrings["StarWarsConnection"].ConnectionString);

            //Take the Trilogy Name and get the ID for the insert into the Character table
            int trilogyId = GetTrilogyIdByTrilogyName(introducedInTrilogy);

            //Create a Dictionary (key, value pairs) of paramaters and values for our stored proc to use in our DAL
            Dictionary<string, object> paramDictionary = new Dictionary<string, object>();
            paramDictionary.Add("@Name", name);
            paramDictionary.Add("@Allegiance", allegiance);
            paramDictionary.Add("@IsJedi", isJedi);
            paramDictionary.Add("@IntroducedInTrilogy_Id", trilogyId);

            //Call our DAL method to get a list of books by author           
            dal.CreateDataViaStoredProcedure("spAddCharacter", paramDictionary);          
        }

        private List<StarWarsCharacterDTO> PopulateDTOListFromDataTable(DataTable dt)
        {
            List<StarWarsCharacterDTO> starWarsCharacterDTOs = new List<StarWarsCharacterDTO>();

            //Loop through our data table and populate the Star Wars DTO
            foreach (DataRow row in dt.Rows)
            {
                StarWarsCharacterDTO starWarsCharacterDTO = new StarWarsCharacterDTO();
                starWarsCharacterDTO.CharacterId = Convert.ToInt32(row["CharacterId"]);
                starWarsCharacterDTO.Name = row["Name"].ToString();
                starWarsCharacterDTO.Allegiance = row["Allegiance"].ToString();
                starWarsCharacterDTO.IsJedi = Convert.ToBoolean(row["IsJedi"]);
                starWarsCharacterDTO.TrilogyName = row["TrilogyName"].ToString();

                starWarsCharacterDTOs.Add(starWarsCharacterDTO);
            }

            return starWarsCharacterDTOs;
        }

        private int GetTrilogyIdByTrilogyName(string trilogyName)
        {
            //Create a new instance of our DAL, passing in the connection string
            DataAccess dal = new DataAccess(ConfigurationManager.ConnectionStrings["StarWarsConnection"].ConnectionString);

            //Create a Dictionary (key, value pairs) of paramaters and values for our stored proc to use in our DAL
            Dictionary<string, object> paramDictionary = new Dictionary<string, object>();
            paramDictionary.Add("@Trilogy", trilogyName);

            //Call our DAL method to get a list of books by author           
            DataTable trilogyTable = dal.PopulateDataTableViaStoredProcedure("spGetTrilogyIdByTrilogyName", paramDictionary);

            //Loop through our data table and populate the Star Wars DTO, returning the results - we can use [0] because we'll only have one result
            return Convert.ToInt32(trilogyTable.Rows[0]["TrilogyId"]);
        }

    }
}
