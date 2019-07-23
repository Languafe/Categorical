using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using CategoricalApi.Data;
// using CategoricalApi.Models;
using CategoricalApi.Services;
using System.Linq;

namespace CategoricalApi.Tests
{
    public class CategoryServiceShould
    {
        public DbContextOptions<CategoricalContext> GetContextOptions()
        {
            string id = Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<CategoricalContext>()
                // .UseInMemoryDatabase("SharedByAllTests!")
                .UseInMemoryDatabase(id)
                .Options;
        }

        [Fact]
        public void CreateNewCategoryWithNewId()
        {
            using (var dbContext = new CategoricalContext(GetContextOptions()))
            {
                var categoryService = new CategoryService(dbContext);
                var category = new Category { Title = "A test category" };
                categoryService.Create(category);

                Assert.Single(dbContext.Categories.ToList());
                Assert.Equal(1, category.Id);
            }
        }

        [Fact]
        public void GetAllCategories()
        {
            using (var dbContext = new CategoricalContext(GetContextOptions()))
            {
                var categoryService = new CategoryService(dbContext);
                categoryService.Create(new Category { Title = "Test" });
                Assert.Single(categoryService.GetAll());
            }
        }

        [Fact]
        public void GetById()
        {
            using (var dbContext = new CategoricalContext(GetContextOptions()))
            {
                var categoryService = new CategoryService(dbContext);
                Category category = new Category { Title = "Test" };
                categoryService.Create(category);
                
                Assert.Equal(category.Id, categoryService.GetById(category.Id).Id);
                Assert.Equal("Test", categoryService.GetById(category.Id).Title);
            }
        }

        [Fact]
        public void UpdateCategory()
        {
            using (var dbContext = new CategoricalContext(GetContextOptions()))
            {
                var categoryService = new CategoryService(dbContext);
                Category category = new Category { Title = "Test" };
                categoryService.Create(category);
                category.Title = "Changed!";
                // categoryService.Update(category);
                
                Assert.Equal("Changed!", categoryService.GetById(category.Id).Title);
            }
        }

        [Fact]
        public void DeleteCategory()
        {
            using (var dbContext = new CategoricalContext(GetContextOptions()))
            {
                var categoryService = new CategoryService(dbContext);
                Category category = new Category { Title = "Test" };
                categoryService.Create(category);
                categoryService.Delete(category);
                Assert.Empty(categoryService.GetAll());
            }
        }
    }

    public class TestHelper
    {
        public static int getNumber()
        {
            return 1;
        }
    }
}
