using System;
using System.Collections.Generic;
using System.Linq;
using CategoricalApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CategoricalApi.Services
{
    public class CategoryService
    {
        private readonly CategoricalContext dbContext;

        public CategoryService(CategoricalContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Category> GetAll()
        {
            return this.dbContext.Categories.ToList();
        }

        public Category Create(Category newCategory)
        {
            this.dbContext.Categories.Add(newCategory);
            this.dbContext.SaveChanges();
            return newCategory;
        }

        public Category GetById(int id)
        {
            return this.dbContext.Categories
                .Include(x => x.Items)
                .SingleOrDefault(x => x.Id == id);
        }

        public void Remove(Category category)
        {
            this.dbContext.Remove(category);
            this.dbContext.SaveChanges();
        }

        /* Calls Remove. You could do that yourself, also. */
        public void Delete(Category category)
        {
            this.Remove(category);
        }

        public void Update(Category category)
        {
            this.dbContext.Update(category);
            this.dbContext.SaveChanges();
        }
    }
}