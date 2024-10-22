namespace Api_RickAndMorty_DotNet.Model
{
    public class CharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public DateTime Created { get; set; }
        public Origin origin { get; set; }
        public Location location { get; set; } 

        public class Origin
        {
            public string Name { get; set; } 
        }

        public class Location
        {
            public string Name { get; set; } 
        }
    }
}
