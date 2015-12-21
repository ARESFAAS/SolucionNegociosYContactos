using NegociosYContactos.Models;

namespace NegociosYContactos.Data.Classes
{
    public interface IData
    {
        BusinessWeb GetBusinessData();

        User SaveUser(User user);

        User GetUser(User user);
    }
}
