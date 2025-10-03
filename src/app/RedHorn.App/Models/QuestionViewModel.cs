using System.ComponentModel.DataAnnotations;

namespace RedHorn.App.Models;

public class QuestionViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Question is required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Question must be between 10 and 1000 characters")]
    public string QuestionText { get; set; } = default!;
}
