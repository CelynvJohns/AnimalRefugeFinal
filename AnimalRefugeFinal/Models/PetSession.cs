using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace AnimalRefugeFinal.Models
{
    public class PetSession
    {
        // Set private constants for key names
        private const string PetListKey = "PetList";
        private const string CountKey = "PetCount";
        private const string FavoritesKey = "UserFavorites";

        // Define a property to store the session
        private ISession session { get; set; }

        // Constructor that accepts an argument of the ISession type
        public PetSession(ISession session)
        {
            this.session = session;
        }

        // Extension method to set the list of pets in the session
        public void SetPetList(List<Pet> petList)
        {
            // Use SetObject() extension method of the SessionExtensions class to store the list of pets in session state
            session.SetObject(PetListKey, petList);

            // Store count of pets in session state
            session.SetInt32(CountKey, petList.Count);
        }

        //get data store by SetPetList() method, gets a list of the pets 
        public List<Pet> GetMyPets() => session.GetObject<List<Pet>>(PetListKey) ?? new List<Pet>();


        

        //get count of pets
        public int GetMyPetCount() => session.GetInt32(CountKey) ?? 0;

        //method to remove session country and count keys
        public void RemoveMyPets()
        {
            session.Remove(PetListKey);
            session.Remove(CountKey);
        }

    }
}
