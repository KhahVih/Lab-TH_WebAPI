namespace LAB_TH_API.Models.DTO
{
    public class AddAuthorDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }

        public List<int> BookIds { get; set; }
    }
}
