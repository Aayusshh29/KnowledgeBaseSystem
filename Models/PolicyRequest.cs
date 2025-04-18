namespace Backend.Models
{
    public class PolicyRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime RequestedAt { get; set; }
        public string? Status { get; set; }
        public int RequestedById { get; set; }  // Keep this field

        // Removed the RequestedBy navigation property
        // public User? RequestedBy { get; set; }
    }
}
