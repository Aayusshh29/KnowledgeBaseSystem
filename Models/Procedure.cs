namespace Backend.Models
{
    public class Procedure
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Steps { get; set; }
        public string? Department { get; set; }
        public string? CreatedAt { get; set; }   // changed to string
        public string? LastUpdatedAt { get; set; }   // changed to string
        public string? CreatedBy { get; set; }
    }
}
