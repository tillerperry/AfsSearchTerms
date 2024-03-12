using Afs.SearchTerms.Web.DataContext;
using Afs.SearchTerms.Web.Models;
using Afs.SearchTerms.Web.Models.Requests;
using Afs.SearchTerms.Web.Models.Responses;
using Afs.SearchTerms.Web.Services.Interfaces;

namespace Afs.SearchTerms.Web.Services.Providers;

public class TranslatorService  : ITranslatorService
{
    private readonly ITranslatorDbRepository _translatorDbRepository;
    private readonly IHttpServices _httpServices;

    public TranslatorService(ITranslatorDbRepository translatorDbRepository,
        IHttpServices httpServices)
    {
        _translatorDbRepository = translatorDbRepository;
        _httpServices = httpServices;
    }

    public async Task<ApiResponse<TranslatorResponse>> TranslateAndCreateTextAsync(FunTranslatorRequest input)
    {
        try
        {
            // if (input.Text  is not  null)
            // {
            //     return new ApiResponse<TranslatorResponse>
            //     {
            //         Message = "success",
            //         Code = StatusCodes.Status200OK,
            //         IsSuccessful = false,
            //         Data = new TranslatorResponse
            //         {
            //             Success = null,
            //             Contents = new Contents
            //             {
            //                 Translated = "rjr445",
            //                 Text = input.Text,
            //                 Translation = "leetspeak"
            //             }
            //         }
            //     }; 
            // }
           
            var getFunTranslatorResults = await _httpServices.GetFunTranslatorAsync(input);

            if (!getFunTranslatorResults.IsSuccessful)
            {
              
                return new ApiResponse<TranslatorResponse>
                {
                    Message = "Could not perform translation kindly try later",
                    Code = StatusCodes.Status200OK,
                    IsSuccessful = false,
                    Data = new TranslatorResponse()
                    
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
            Console.WriteLine(e);
            return new ApiResponse<TranslatorResponse>
            {
                Message = "Something bad happened",
                Code = StatusCodes.Status200OK,
                IsSuccessful = false
            };
        }
    
    }
}

