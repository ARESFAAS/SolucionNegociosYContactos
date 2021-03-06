﻿using NegociosYContactos.Models;

namespace NegociosYContactos.Data.Classes
{
    public interface IData
    {
        #region businessData

        BusinessWeb GetBusinessData(User user);

        BusinessWeb SaveBusinessWeb(BusinessWeb businesWeb);

        #endregion

        #region userData

        User SaveUser(User user);

        User GetUser(User user);

        User GetUserForLogin(User user);

        User GetUserForLoginExternal(User user);

        User EditUser(User user);

        bool ValidateUserName(string userName);

        #endregion

        #region search

        object GetCategory();

        int TypeTerm_Get(string term);

        SearchListViewModel BusinessList_Get(int idCategory, int tamPage, int page);

        BusinessWeb Business_Get(int id, string name);

        ProductOrderWeb ProductOrderGet(ProductOrderWeb order);

        ProductOrderWeb ProductOrderSave(ProductOrderWeb order);

        User GetUserForMail(int productId);

        #endregion
    }
}
