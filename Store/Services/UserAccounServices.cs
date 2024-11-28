using Store.Authentication;
using Store.Data;
using Store.Models;

namespace Store.Services
{
	public class UserAccounServices : IServices<UserAccount>
	{
		AuthenticationDbContext _context;
		public UserAccounServices(AuthenticationDbContext context)
		{
			_context = context;
		}
		public UserAccount Create(UserAccount account)
		{
			_context.UserAccounts.Add(account);
			_context.SaveChanges();
			return account;
		}

		public bool Update(int id, UserAccount account)
		{
			var acc = GetById(id);
			if (acc != null)
			{
				acc.Name = account.Name;
				acc.UserName = account.UserName;
				acc.Password = account.Password;
				acc.Email = account.Email;
			
				_context.SaveChanges();
				return true;
			}

			return false;
		}

		public List<UserAccount> GetAll()
		{
			List<UserAccount> accounts = _context.UserAccounts.ToList();

			return accounts;
		}

		public UserAccount GetById(int id)
		{
			var account = _context.UserAccounts.FirstOrDefault(x => x.Id == id);

			return account;
		}

		public bool DeleteById(int id)
		{
			var prod = GetById(id);
			if (prod != null)
			{
				_context.UserAccounts.Remove(prod);
				_context.SaveChanges();
				return true;
			}

			return false;
		}
	}
}
