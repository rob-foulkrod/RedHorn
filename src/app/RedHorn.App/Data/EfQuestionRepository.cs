using Microsoft.EntityFrameworkCore;
using RedHorn.App.Models;

namespace RedHorn.App.Data;

public class EfQuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _db;

    public EfQuestionRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Question question, CancellationToken ct = default)
    {
        _db.Questions.Add(question);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<Question?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Questions.FindAsync(new object[] { id }, ct);
    }
}
