namespace Afs.SearchTerms.Web.Models.Responses;

#nullable disable

#nullable disable

public class ApiResponse<T>
{
        public ApiResponse(string message, T data, bool isSuccessful)
        {
                Message = message;
                Data = data;
                IsSuccessful = isSuccessful;
        }

        public ApiResponse()
        {
             
        }


        public string Message { get; set; }
        public int Code { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
}