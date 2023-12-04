using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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

        // Extension method to get the list of pets from the session
        public List<Pet> GetPetList()
        {
            // Use GetObject() extension method of the SessionExtensions class to retrieve the list of pets from session state
            var petList = session.GetObject<List<Pet>>(PetListKey);
            return petList ?? new List<Pet>();
        }

        // Extension method to set the user's favorite pets in the session
        public void SetUserFavorites(List<Pet> favoritePets)
        {
            // Use SetObject() extension method to store the user's favorite pets in session state
            session.SetObject(FavoritesKey, favoritePets);
        }

        // Extension method to get the user's favorite pets from the session
        public List<Pet> GetUserFavorites()
        {
            // Use GetObject() extension method to retrieve the user's favorite pets from session state
            var favoritePets = session.GetObject<List<Pet>>(FavoritesKey);
            return favoritePets ?? new List<Pet>();
        }

        // Extension method to get the count of pets from the session
        public int GetPetCount()
        {
            // Use GetInt32() method to retrieve the count of pets from session state
            return session.GetInt32(CountKey) ?? 0;
        }
    }
}



