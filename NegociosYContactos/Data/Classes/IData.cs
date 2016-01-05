using NegociosYContactos.Models;

namespace NegociosYContactos.Data.Classes
{
    public interface IData
    {
        BusinessWeb GetBusinessData(User user);

        BusinessWeb SaveBusinessWeb(BusinessWeb businesWeb);

        User SaveUser(User user);

        User GetUser(User user);

        User GetUserForLogin(User user);

        User GetUserForLoginExternal(User user);

        User EditUser(User user);

        object GetCategoryAutoComplete();
    }
}
