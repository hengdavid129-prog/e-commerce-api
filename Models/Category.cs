namespace E_Commerce_api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; } 
        public DateTime Updated_At { get; set; }
    }
}
