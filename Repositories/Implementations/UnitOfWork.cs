using Lab_8___Carlos_Mamani.Models;
using Lab_8___Carlos_Mamani.Repositories.Interfaces;

namespace Lab_8___Carlos_Mamani.Services.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly LINQExampleContext _db;

    public UnitOfWork(LINQExampleContext db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        _db.SaveChangesAsync(ct);
}