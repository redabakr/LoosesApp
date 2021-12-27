namespace Looses.Domain.Repositories;

public interface ILooseRepository
{
    Task<IEnumerable<Entities.Looses?>> GetAllAsync();
    Task<Entities.Looses?> GetAsync(int id);
    Task AddAsync(Entities.Looses looses);
    Task UpdateAsync(Entities.Looses looses);
    Task DeleteAsync(Entities.Looses looses);

    Task SaveChangesAsync();
}