using Bulky.DataAccess.Repository.iRepository;
using Bulky.Models.Models;
using BulkyDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    internal class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(ShoppingCart cart)
        {
            _db.ShoppingCarts.Update(cart);
        }
    }
}
