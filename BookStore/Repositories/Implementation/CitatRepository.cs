using BookStore.Data;
using BookStore.Models.Domain;
using BookStore.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class CitatRepository : ICitatRepository
    {
        private readonly AppDbContext appDbContext;

        public CitatRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Citats> CreateAsync(Citats citat)
        {
            await appDbContext.Citats.AddAsync(citat);
            await appDbContext.SaveChangesAsync();
            return citat;   
        }

        public async Task<IEnumerable<Citats>> GetAllAsync()
        {
          return  await appDbContext.Citats.ToListAsync();    
        }
    }
}
