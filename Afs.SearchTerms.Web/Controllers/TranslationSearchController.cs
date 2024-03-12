using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afs.SearchTerms.Web.DataContext;
using Afs.SearchTerms.Web.Models.Requests;
using Afs.SearchTerms.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Afs.SearchTerms.Web.Controllers
{
    public class TranslationSearchController : Controller
    {
        private readonly ITranslatorService _translatorService;
        private readonly ITranslatorDbRepository _translatorDbRepository;

        public TranslationSearchController(ITranslatorService translatorService, 
            ITranslatorDbRepository translatorDbRepository)
        {
            _translatorService = translatorService;
            _translatorDbRepository = translatorDbRepository;
        }

        // GET: TranslationSearch
        public async Task<IActionResult> Index()
        {
            var translationsResults =  await _translatorDbRepository.ListTranslationSearches();
            return View(translationsResults);
        }

       
        // POST : TranslationSearch/Create
        [HttpPost]
        [Authorize(AuthenticationSchemes = "PrivateKey")]
        public async Task<IActionResult> Create([FromBody] FunTranslatorRequest input)
        {
            var response = await _translatorService.TranslateAndCreateTextAsync(input);
            return  StatusCode(response.Code,response);
        }

       
    }
}