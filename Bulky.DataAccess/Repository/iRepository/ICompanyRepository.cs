using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.iRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void update(Company obj);
    }
}
