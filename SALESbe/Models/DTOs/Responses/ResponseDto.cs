namespace Api.Sales.Models.DTOs.Responses
{
    public class ResponseDto
    {
        public int? RowsAffected { get; set; }
        public string? Message { get; set; }
        public string? Errors { get; set; }
    }
}
