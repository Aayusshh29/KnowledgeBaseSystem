namespace Backend.Dtos
{
    public class PolicyReadDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CreatedAt { get; set; }
        public string? LastUpdatedAt { get; set; }
        public int CreatedById { get; set; }
    }
}
