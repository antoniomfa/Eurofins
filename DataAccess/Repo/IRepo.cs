using DataAccess.Entities;

namespace DataAccess.Repo
{
    // TODO Implement DTOs and Mapper
    public interface IRepo
    {
        Task<bool> Authenticate(User user);
        Task Register(User user);
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void Create(Customer platform);
        bool SaveChanges();
    }
}
