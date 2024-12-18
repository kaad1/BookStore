using BookStore.Models.Domain;
using BookStore.Models.DTO;

namespace BookStore.Repositories.Interface
{
    public interface IBookPostRepository
    {
       Task<Books> CreateAsync(Books books);

       Task<IEnumerable<Books>> GetAllAsync();

        Task<Books>GetByIdAsync(Guid id);
        Task<Books> GetByUrlHandleAsync(string urlHandle);



        Task<Books?> UpdateAsync(Books books);

       Task<Books?> DeleteAsync(Guid id);
    }
}
