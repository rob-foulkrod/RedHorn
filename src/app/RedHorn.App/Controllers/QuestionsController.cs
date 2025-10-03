using Microsoft.AspNetCore.Mvc;
using RedHorn.App.Models;

namespace RedHorn.App.Controllers;

public class QuestionsController : Controller
{
    private readonly ILogger<QuestionsController> _logger;

    public QuestionsController(ILogger<QuestionsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Ask()
    {
        return View(new QuestionViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Ask(QuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // TODO: Add business logic to save question
        _logger.LogInformation("Question received from {Email}: {Question}", 
            model.Email, model.QuestionText);

        TempData["SuccessMessage"] = "Thank you! Your question has been submitted successfully.";
        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success()
    {
        return View();
    }
}
