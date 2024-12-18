using BookStore.Models.Domain;
using System.Net;

namespace BookStore.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BookImage> Upload(IFormFile file, BookImage bookImage);

      Task<IEnumerable<BookImage>>  GetAll();
    }
}
