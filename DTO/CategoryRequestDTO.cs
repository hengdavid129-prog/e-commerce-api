namespace E_Commerce_api.DTO
{
    public class CategoryRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
