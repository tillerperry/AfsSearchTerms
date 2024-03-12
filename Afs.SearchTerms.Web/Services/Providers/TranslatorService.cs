using Afs.SearchTerms.Web.DataContext;
using Afs.SearchTerms.Web.Extensions;
using Afs.SearchTerms.Web.Models;
using Afs.SearchTerms.Web.Models.Requests;
using Afs.SearchTerms.Web.Models.Responses;
using Afs.SearchTerms.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Afs.SearchTerms.Web.Services.Providers;

public class TranslatorService  : ITranslatorService
{
    private readonly ITranslatorDbRepository _translatorDbRepository;
    private readonly IHttpServices _httpServices;
    private readonly ILogger<TranslatorService> _logger;

    public TranslatorService(ITranslatorDbRepository translatorDbRepository,
        IHttpServices httpServices, 
        ILogger<TranslatorService> logger)
    {
        _translatorDbRepository = translatorDbRepository;
        _httpServices = httpServices;
        _logger = logger;
    }

    public async Task<ApiResponse<TranslatorResponse>> TranslateAndCreateTextAsync(FunTranslatorRequest input)
    {
        try
        {
            _logger.LogDebug("GetFunTranslatorAsync=>{request} :: ",input.Serialize());
            if (input.Text  is not  null)
            {
                return new ApiResponse<TranslatorResponse>
                {
                    Message = "success",
                    Code = StatusCodes.Status200OK,
                    IsSuccessful = true,
                    Data = new TranslatorResponse
                    {
                        Success = null,
                        Contents = new Contents
                        {
                            Translated = "4884'[]499202lett",
                            Text = input.Text,
                            Translation = "leetspeak"
                        }
                    }
                }; 
            }
            
            var getFunTranslatorResults = await _httpServices.GetFunTranslatorAsync(input);

            if (!getFunTranslatorResults.IsSuccessful)
            {
              
                return new ApiResponse<TranslatorResponse>
                {
                    Message = "Could not perform translation kindly try later",
                    Code = StatusCodes.Status200OK,
                    IsSuccessful = false,
                    Data = new TranslatorResponse
                    {
                        Success = new Success(){},
                        Contents = new Contents()
                    }

                }; 
                
            }
        
            //save the request and response 
            var translationRecord = new TranslationSearch
            {
                CreatedAt = DateTime.UtcNow,
                Translation = getFunTranslatorResults.Data.Contents.Translation,
                Text = input.Text,
                Translated = getFunTranslatorResults.Data.Contents.Translated
            };
            await _translatorDbRepository.CreatTranslatedDetailsAsync(translationRecord);

            return getFunTranslatorResults;

        }
        catch (Exception e)
        {
         _logger.LogError(e,"GetFunTranslatorAsync=>{error} :: ",e.Message);
            return new ApiResponse<TranslatorResponse>
            {
                
                Message = "Something bad happened",
                Code = StatusCodes.Status200OK,
                IsSuccessful = false,
                Data = new TranslatorResponse
                {
                    Success = new Success(),
                    Contents = new Contents()
                }
            };
        }
    
    }
}

