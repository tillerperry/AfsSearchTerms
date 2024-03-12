using Afs.SearchTerms.Web.DataContext;
using Afs.SearchTerms.Web.Services.Interfaces;
using Afs.SearchTerms.Web.Services.Providers;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace AfsSearchTerms.Web.Tests.Components;

public class DiFixture
{
    public ITranslatorDbRepository TranslatorDbRepository { get; set; }
    public  IHttpServices HttpServices {get; set; }

    public DiFixture()
    {
        TranslatorDbRepository = Substitute.For<ITranslatorDbRepository>();
        HttpServices = Substitute.For<IHttpServices>();
    }
    
   

   public  TranslatorService TranslatorService()
   {
       return new TranslatorService(TranslatorDbRepository,HttpServices);

   }
   
   
   
   
}