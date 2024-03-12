using Afs.SearchTerms.Web.Models;
using Afs.SearchTerms.Web.Models.Requests;
using Afs.SearchTerms.Web.Models.Responses;
using AfsSearchTerms.Web.Tests.Components;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;

namespace AfsSearchTerms.Web.Tests;

public class TranslatorServiceTests : IClassFixture<DiFixture>
{

    private readonly DiFixture _fixture;
    public TranslatorServiceTests(DiFixture fixture)
    {
        _fixture = fixture;
    }

    #region TranslateAndCreateTextAsync
        
    [Fact]
    public async Task TranslateAndCreateTextAsync_Should_Return_Success_And_Unsuccessful_When_When_Translator_Response_Is_Not_Successful()
    {
        // Arrange
        //BusinessInformation = GenFu.GenFu.New<BusinessInfoDetails>()
        var sut = _fixture.TranslatorService();
        var translatorRequest = new FunTranslatorRequest
        {
            Text = "hello"
        };
        
        var translatorResponse =   new ApiResponse<TranslatorResponse>
        {
            Message = "bad request",
            Code = StatusCodes.Status400BadRequest,
            IsSuccessful = false,
            Data = new TranslatorResponse()
        }; 
        _fixture.HttpServices.GetFunTranslatorAsync(translatorRequest).Returns(translatorResponse);
    
        // Act
        var apiResponse = await sut.TranslateAndCreateTextAsync(translatorRequest);

        // Assert
        Assert.NotNull(apiResponse);
        Assert.Equal(StatusCodes.Status200OK, apiResponse.Code);
        Assert.Equal(false, apiResponse.IsSuccessful);
        Assert.Equal("Could not perform translation kindly try later", apiResponse.Message);
        Assert.NotNull(apiResponse.Data);
    }
    
    
    [Fact]
    public async Task TranslateAndCreateTextAsync_Should_Return_Success_And_Successful_When_When_Translator_Response_Is_Successful()
    {
        // Arrange
        //BusinessInformation = GenFu.GenFu.New<BusinessInfoDetails>()
        var sut = _fixture.TranslatorService();
        var translatorRequest = new FunTranslatorRequest
        {
            Text = "hello"
        };
        
        var translatorResponse =   new ApiResponse<TranslatorResponse>
        {
            Message = "success",
            Code = StatusCodes.Status200OK,
            IsSuccessful = true,
            Data = new TranslatorResponse
            {
                Success = new Success(),
                Contents = new Contents
                {
                    Translated = "rjr445",
                    Text =translatorRequest.Text,
                    Translation = "LeetSpeak"
                }
            }
        };

        var translatorData = new TranslationSearch
        {
            CreatedAt = DateTime.UtcNow,
            Translation = translatorResponse.Data.Contents.Translation,
            Text = translatorResponse.Data.Contents.Text,
            Translated = translatorResponse.Data.Contents.Translated
        };
        _fixture.HttpServices.GetFunTranslatorAsync(translatorRequest).Returns(translatorResponse);
         await  _fixture.TranslatorDbRepository.CreatTranslatedDetailsAsync(translatorData);
    
        // Act
        var apiResponse = await sut.TranslateAndCreateTextAsync(translatorRequest);

        // Assert
        Assert.NotNull(apiResponse);
        Assert.Equal(StatusCodes.Status200OK, apiResponse.Code);
        Assert.Equal(true, apiResponse.IsSuccessful);
        Assert.Equal("success", apiResponse.Message);
        Assert.NotNull(apiResponse.Data);
    }

    
    

    

    #endregion
   
    
}