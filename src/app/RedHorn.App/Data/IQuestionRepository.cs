using RedHorn.App.Models;

namespace RedHorn.App.Data;

public interface IQuestionRepository
{
    Task AddAsync(Question question, CancellationToken ct = default);
    Task<Question?> GetByIdAsync(Guid id, CancellationToken ct = default);
}
