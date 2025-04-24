namespace Backend.Dtos
{
    public class PolicyRequestReadDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? RequestedAt { get; set; }  // The formatted string date
        public string? Status { get; set; }
        public int RequestedById { get; set; }
    }
}
