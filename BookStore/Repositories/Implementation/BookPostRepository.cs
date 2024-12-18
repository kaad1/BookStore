using BookStore.Data;
using BookStore.Models.Domain;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class BookPostRepository : IBookPostRepository
    {
        private readonly AppDbContext appContext;

        public BookPostRepository(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

      
        public async Task<Books> CreateAsync(Books books)
        {
           await appContext.Books.AddAsync(books);
           await appContext.SaveChangesAsync();
            return books;
        }

        public async Task<Books?> DeleteAsync(Guid id)
        {
           var existingBookPost= await appContext.Books.FirstOrDefaultAsync(x=> x.Id == id);
           
            if (existingBookPost != null)
            {
                appContext.Books.Remove(existingBookPost);
                await appContext.SaveChangesAsync();
                return existingBookPost;
            }
            return null;
        }

        public async Task<IEnumerable<Books>> GetAllAsync()
        {
           return await appContext.Books.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<Books> GetByIdAsync(Guid id)
        {
            return await appContext.Books.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public  async Task<Books> GetByUrlHandleAsync(string urlHandle)
        {
            return await appContext.Books.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<Books?> UpdateAsync(Books books)
        {
           var existingBookPost= await appContext.Books.Include(x=>x.Categories)
                .FirstOrDefaultAsync(x => x.Id == books.Id);
            if (existingBookPost == null)
            {
                return null;
            }
            //UpdateBookPost
            appContext.Entry(existingBookPost).CurrentValues.SetValues(books);

            //Update CAtegories
            existingBookPost.Categories = books.Categories;
            await appContext.SaveChangesAsync();
            return books;
        }

       
    }
}