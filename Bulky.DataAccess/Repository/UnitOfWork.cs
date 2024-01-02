using Bulky.DataAccess.Repository.iRepository;
using BulkyDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWorkcs
	{
		public ICategoryRepository Category { get; private set; }

        public IProductRepository Product {  get; private set; }

        public ICompanyRepository Company { get; private set; }

        private readonly ApplicationDbContext _db;
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			Product = new ProductRepository(_db);
			Company = new CompanyRepository(_db);	

		}
		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
