using Afs.SearchTerms.Web.Models;

namespace Afs.SearchTerms.Web.DataContext;

public interface ITranslatorDbRepository
{
    public IQueryable<TranslationSearch> GetQueryable(); 
    public Task<int> CreatTranslatedDetailsAsync(TranslationSearch request);
     public   Task<List<TranslationSearch>> ListTranslationSearches();
}