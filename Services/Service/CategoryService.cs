using BusinessObjects.Models;
using Repositories.Interfaces;
using Repositories.Repository;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CategoryService : ICatergoryService
    {
        private readonly ICatergoryRepository icatergoryRepository;
        public CategoryService()
        {
            icatergoryRepository = new CatergoryRepository();
        }
        public List<Category> GetCategories()
        {
            return icatergoryRepository.GetCategories();
        }
    }
}
