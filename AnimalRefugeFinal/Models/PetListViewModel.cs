namespace AnimalRefugeFinal.Models
{
    public class PetListViewModel : PetViewModel
    {
        public List<Pet> Pets { get; set; }

        // Make the next property a standard property so the setter can make the first item in the list "All"
        private List<string> species;
        public List<string> Species
        {
            get => species;
            set
            {
                species = value;
                species.Insert(0, "All");
            }
        }

        // Method to help the view determine the active link
        public string CheckActiveSpecies(string s) =>
            s.ToLower() == ActiveSpecies.ToLower() ? "active" : "";
    }
}