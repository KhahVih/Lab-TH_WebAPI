namespace LAB_TH_API.Models
{
    public class Publishers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Books> Book { get; set; }
    }
}
