using BookStore.Models.Domain;

namespace BookStore.Repositories.Interface
{
    public interface ICitatRepository
    {
        Task<Citats> CreateAsync(Citats citat);

        Task<IEnumerable<Citats>> GetAllAsync();
    }
}
