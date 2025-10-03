using Microsoft.EntityFrameworkCore;
using RedHorn.App.Data;
using RedHorn.App.Models;

namespace RedHorn.Tests;

public class EfQuestionRepositoryTests
{
    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_SavesQuestionToDatabase()
    {
        using var context = CreateDbContext();
        var repository = new EfQuestionRepository(context);

        var question = new Question
        {
            Email = "test@example.com",
            QuestionText = "This is a test question"
        };

        await repository.AddAsync(question);

        var savedQuestion = await context.Questions.FirstOrDefaultAsync();
        Assert.NotNull(savedQuestion);
        Assert.Equal("test@example.com", savedQuestion.Email);
        Assert.Equal("This is a test question", savedQuestion.QuestionText);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsQuestionWhenFound()
    {
        using var context = CreateDbContext();
        var repository = new EfQuestionRepository(context);

        var question = new Question
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            QuestionText = "This is a test question"
        };

        await repository.AddAsync(question);

        var retrievedQuestion = await repository.GetByIdAsync(question.Id);

        Assert.NotNull(retrievedQuestion);
        Assert.Equal(question.Id, retrievedQuestion.Id);
        Assert.Equal("test@example.com", retrievedQuestion.Email);
        Assert.Equal("This is a test question", retrievedQuestion.QuestionText);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNullWhenNotFound()
    {
        using var context = CreateDbContext();
        var repository = new EfQuestionRepository(context);

        var retrievedQuestion = await repository.GetByIdAsync(Guid.NewGuid());

        Assert.Null(retrievedQuestion);
    }
}
