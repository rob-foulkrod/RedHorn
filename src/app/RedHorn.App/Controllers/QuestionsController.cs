using Microsoft.AspNetCore.Mvc;
using RedHorn.App.Data;
using RedHorn.App.Models;

namespace RedHorn.App.Controllers;

public class QuestionsController : Controller
{
    private readonly ILogger<QuestionsController> _logger;
    private readonly IQuestionRepository _repo;

    public QuestionsController(ILogger<QuestionsController> logger, IQuestionRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Ask()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ask(QuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var question = new Question
        {
            Email = model.Email,
            QuestionText = model.QuestionText
        };

        try
        {
            await _repo.AddAsync(question);
            _logger.LogInformation("Question received from {Email}: {Question}", model.Email, model.QuestionText);
            TempData["SuccessMessage"] = "Thank you! Your question has been submitted successfully.";
            return RedirectToAction(nameof(Success));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save question from {Email}", model.Email);
            ModelState.AddModelError("", "We couldn't save your question right now. Please try again later.");
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }
}
