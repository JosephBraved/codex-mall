using Bulky.Models.Models;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.iRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void update(Product obj);
    }
}
