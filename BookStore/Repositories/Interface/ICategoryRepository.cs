﻿using BookStore.Models.Domain;

namespace BookStore.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
       Task<Category?> GetById(Guid id);

        Task<Category?>UpdateAsnync(Category category);

        Task<Category?> DeleteAsync( Guid id);
    }
}
