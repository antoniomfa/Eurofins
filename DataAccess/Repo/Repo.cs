using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Helpers;

namespace DataAccess.Repo
{
    public class Repo : IRepo
    {
        private readonly AppDbContext _appDbContext;

        public Repo(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        #region CUSTOMER 
        public void Create(Customer customer)
        {
            if (customer == null) { throw new ArgumentNullException(nameof(customer)); }

            _appDbContext.Customers.Add(customer);

            _appDbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _appDbContext.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _appDbContext.Customers.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_appDbContext.SaveChanges() >= 0);
        }
        #endregion CUSTOMER

        #region LOGIN
        public async Task<bool> Authenticate(User user)
        {
            User authenticatedUser = new();

            try
            {
                if (user != null)
                {
                    authenticatedUser = await _appDbContext.Users
                        .FirstOrDefaultAsync(x => x.Username == user.Username);

                    if (!PwdHasher.VerifyPassword(user.Password, authenticatedUser.Password))
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return false;
        }
        
        // TODO Username, Email, Password validation + JWT token
        public async Task Register(User user)
        {
            user.Password = PwdHasher.HashPassword(user.Password);
            user.Role = "User";
            user.Token = "";

            await _appDbContext.Users.AddAsync(user);

            await _appDbContext.SaveChangesAsync();
        }
        #endregion LOGIN
    }
}
