using E_Commerse2.Data;
using E_Commerse2.Models;
using E_Commerse2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse2.Repository
{
    public class CategoryRepository:Repository<Category> ,ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

    }
}
