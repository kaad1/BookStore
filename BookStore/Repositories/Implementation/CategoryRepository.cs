using BookStore.Data;
using BookStore.Models.Domain;
using BookStore.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
          var existingCategory=  await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null) { return null; }
            appDbContext.Categories.Remove(existingCategory);
            await appDbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await appDbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
           return await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsnync(Category category)
        {
         var existingCategory= await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id== category.Id);
            if (existingCategory != null)
            {
                appDbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await appDbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
}
