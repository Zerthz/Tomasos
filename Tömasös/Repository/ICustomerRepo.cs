using Tömasös.Models;

namespace Tömasös.Repository
{
    public interface ICustomerRepo
    {
        AspNetUser GetUserById(string id);
        List<AspNetUser> GetUsers();
    }
}