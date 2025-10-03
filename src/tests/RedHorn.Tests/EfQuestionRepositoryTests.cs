using Microsoft.EntityFrameworkCore;
using RedHorn.App.Data;
using RedHorn.App.Models;

namespace RedHorn.Tests;

public class EfQuestionRepositoryTests
{
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_AddsQuestionToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EfQuestionRepository(context);
        var question = new Question
        {
            Email = "test@example.com",
            QuestionText = "This is a test question?"
        };

        // Act
        await repository.AddAsync(question);

        // Assert
        var savedQuestion = await context.Questions.FirstOrDefaultAsync();
        Assert.NotNull(savedQuestion);
        Assert.Equal("test@example.com", savedQuestion.Email);
        Assert.Equal("This is a test question?", savedQuestion.QuestionText);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsQuestion_WhenQuestionExists()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EfQuestionRepository(context);
        var question = new Question
        {
            Email = "test@example.com",
            QuestionText = "This is a test question?"
        };
        context.Questions.Add(question);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(question.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(question.Id, result.Id);
        Assert.Equal("test@example.com", result.Email);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenQuestionDoesNotExist()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EfQuestionRepository(context);
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await repository.GetByIdAsync(nonExistentId);

        // Assert
        Assert.Null(result);
    }
}
