namespace TodoList.PL.WebAPI.ResponseModels
{
    public class ErrorResponse
    {
        public string ErrorDescription { get; set; } = null!;
        public int ErrorCode { get; set; }
    }
}
