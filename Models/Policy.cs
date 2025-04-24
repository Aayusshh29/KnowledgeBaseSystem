namespace Backend.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Content { get; set; }
        public string? CreatedAt { get; set; }  // Store formatted date as string
        public string? LastUpdatedAt { get; set; }  // Store formatted date as string
        public int CreatedById { get; set; }
    }
}
