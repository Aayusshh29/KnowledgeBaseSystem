namespace Backend.Dtos
{
    public class ProcedureUpdateDto
    {
        public string? Title { get; set; }
        public string? Steps { get; set; }
        public string? Department { get; set; }
        // LastUpdatedAt is automatically tracked on the server, not sent by client
    }
}
