using Afs.SearchTerms.Web.DataContext;
using Afs.SearchTerms.Web.Services.Interfaces;
using Afs.SearchTerms.Web.Services.Providers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace AfsSearchTerms.Web.Tests.Components;

public class DiFixture
{
    public ITranslatorDbRepository TranslatorDbRepository { get; set; }
    public  IHttpServices HttpServices {get; set; }
    public  ILogger<TranslatorService> Logger {get; set; }

    public DiFixture()
    {
        TranslatorDbRepository = Substitute.For<ITranslatorDbRepository>();
        HttpServices = Substitute.For<IHttpServices>();
        Logger = Substitute.For< ILogger<TranslatorService>>();
    }
    
   

   public  TranslatorService TranslatorService()
   {
       return new TranslatorService(TranslatorDbRepository,HttpServices,Logger);

   }
   
   
   
   
}