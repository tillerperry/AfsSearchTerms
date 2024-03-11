using Afs.SearchTerms.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Afs.SearchTerms.Web.DataContext;

public class TranslatorDbRepository : ITranslatorDbRepository 
{
    private readonly ApplicationDbContext _translatorDbRepository;

    public TranslatorDbRepository(ApplicationDbContext translatorDbRepository)
    {
        _translatorDbRepository = translatorDbRepository;
    }
    
    public IQueryable<TranslationSearch> GetQueryable()
    {
        return _translatorDbRepository.TranslationSearch.AsNoTracking();
    }
    
    public async Task<List<TranslationSearch>> ListTranslationSearches()
    {
        return await GetQueryable().ToListAsync();
    }

    
    public async Task<int> CreatTranslatedDetailsAsync(TranslationSearch request)
    {
        await _translatorDbRepository.TranslationSearch.AddAsync(request);
        var saveResponse = await _translatorDbRepository.SaveChangesAsync();
        return saveResponse;
    }

}