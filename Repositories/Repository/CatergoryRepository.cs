using BusinessObjects.Models;
using DataAccessObjects;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class CatergoryRepository : ICatergoryRepository
    {
        public List<Category> GetCategories() => CategoryDAO.GetCategories();   

    }
}
