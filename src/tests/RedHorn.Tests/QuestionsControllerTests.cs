using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using RedHorn.App.Controllers;
using RedHorn.App.Data;
using RedHorn.App.Models;

namespace RedHorn.Tests;

public class QuestionsControllerTests
{
    private readonly Mock<ILogger<QuestionsController>> _loggerMock;
    private readonly Mock<IQuestionRepository> _repositoryMock;

    public QuestionsControllerTests()
    {
        _loggerMock = new Mock<ILogger<QuestionsController>>();
        _repositoryMock = new Mock<IQuestionRepository>();
    }

    private QuestionsController CreateControllerWithTempData()
    {
        var controller = new QuestionsController(_loggerMock.Object, _repositoryMock.Object);
        var httpContext = new DefaultHttpContext();
        var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        controller.TempData = tempData;
        return controller;
    }

    [Fact]
    public void Ask_Get_ReturnsView()
    {
        // Arrange
        var controller = new QuestionsController(_loggerMock.Object, _repositoryMock.Object);

        // Act
        var result = controller.Ask();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Ask_Post_WithValidModel_RedirectsToSuccess()
    {
        // Arrange
        var controller = CreateControllerWithTempData();
        var model = new QuestionViewModel
        {
            Email = "test@example.com",
            QuestionText = "This is a test question?"
        };

        // Act
        var result = await controller.Ask(model);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Success", redirectResult.ActionName);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Question>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Ask_Post_WithInvalidModel_ReturnsViewWithModel()
    {
        // Arrange
        var controller = new QuestionsController(_loggerMock.Object, _repositoryMock.Object);
        controller.ModelState.AddModelError("Email", "Email is required");
        var model = new QuestionViewModel
        {
            Email = "",
            QuestionText = "This is a test question?"
        };

        // Act
        var result = await controller.Ask(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(model, viewResult.Model);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Question>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Ask_Post_WhenRepositoryThrows_ReturnsViewWithError()
    {
        // Arrange
        var controller = new QuestionsController(_loggerMock.Object, _repositoryMock.Object);
        var model = new QuestionViewModel
        {
            Email = "test@example.com",
            QuestionText = "This is a test question?"
        };
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Question>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await controller.Ask(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(model, viewResult.Model);
        Assert.False(controller.ModelState.IsValid);
    }

    [Fact]
    public void Success_ReturnsView()
    {
        // Arrange
        var controller = new QuestionsController(_loggerMock.Object, _repositoryMock.Object);

        // Act
        var result = controller.Success();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
}
