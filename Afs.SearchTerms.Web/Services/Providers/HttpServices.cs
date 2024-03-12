using Afs.SearchTerms.Web.Extensions;
using Afs.SearchTerms.Web.Models.Requests;
using Afs.SearchTerms.Web.Models.Responses;
using Afs.SearchTerms.Web.Options;
using Afs.SearchTerms.Web.Services.Interfaces;
using Flurl.Http;
using Humanizer;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace Afs.SearchTerms.Web.Services.Providers;

public class HttpServices : IHttpServices
{
    private readonly ILogger<HttpServices> _logger;

    private readonly ExternalApiConfigs _externalApiConfigs;

    public HttpServices(ILogger<HttpServices> logger, 
        IOptions<ExternalApiConfigs> externalApiConfigs)
    {
        _externalApiConfigs = externalApiConfigs.Value;
        _logger = logger;
    }

    public async Task<ApiResponse<TranslatorResponse>> GetFunTranslatorAsync(FunTranslatorRequest request)
    {
        try
        {
            _logger.LogDebug("GetFunTranslatorAsync Raw response after sending notification {request}", request);
            // Build the URL with the parameters
            var requestData = new { text = request.Text };
            var url = await _externalApiConfigs.FunTranslatorApi.AllowAnyHttpStatus().PostJsonAsync(requestData);

            // Make a post request
            var rawResponse = await url.GetStringAsync();
            _logger.LogDebug(
                "[GetFunTranslatorAsync] Raw response after sending translator request {res} request {request} : ",
                request, rawResponse);

            if (!url.StatusCode.Equals(StatusCodes.Status200OK))
            {
                return new ApiResponse<TranslatorResponse>
                {
                    Message = url.ResponseMessage.ReasonPhrase,
                    Code = url.StatusCode,
                    IsSuccessful = false
                };
            }

            var responseData = rawResponse.Deserialize<TranslatorResponse>();
            return new ApiResponse<TranslatorResponse>
            {
                Message = "success",
                Code = StatusCodes.Status200OK,
                IsSuccessful = true,
                Data = responseData
            };
          
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[LendScoreSMSNotification] Error occured for {res}",
                JsonConvert.SerializeObject(request, Formatting.Indented));
            return new ApiResponse<TranslatorResponse>
            {
                Message = ex.Message,
                Code = StatusCodes.Status500InternalServerError,
                IsSuccessful = false,
            };
        }
    }

}