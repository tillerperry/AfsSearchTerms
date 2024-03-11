using Afs.SearchTerms.Web.Models.Requests;
using Afs.SearchTerms.Web.Models.Responses;

namespace Afs.SearchTerms.Web.Services.Interfaces;

public interface IHttpServices
{
 Task<ApiResponse<TranslatorResponse>> GetFunTranslatorAsync(FunTranslatorRequest request);
} 