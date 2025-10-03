using System.ComponentModel.DataAnnotations;

namespace RedHorn.App.Models;

public class QuestionViewModel
{
    [Required(ErrorMessage = "Please enter your name")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [Display(Name = "Your Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a category")]
    [Display(Name = "Question Category")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your question")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Question must be between 10 and 1000 characters")]
    [Display(Name = "Your Question")]
    public string QuestionText { get; set; } = string.Empty;

    [Display(Name = "Priority Level")]
    public string Priority { get; set; } = "Normal";
}
