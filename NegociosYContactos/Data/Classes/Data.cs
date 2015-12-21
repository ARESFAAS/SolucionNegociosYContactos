using System;
using System.Linq;
using NegociosYContactos.Models;

namespace NegociosYContactos.Data.Classes
{
    public class Data : IData
    {
        public BusinessWeb GetBusinessData(User user)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    var products = context.BusinessData_Get(user.Id).Select(x => new BusinessProductWeb
                    {
                        Id = x.IdBusinessProduct,
                        IdBusiness = x.Id,
                        Description = x.ProductDescription,
                        Name = x.ProductName,
                        UrlImage = x.ProductUrlImage,
                        Value = x.ProductValue
                    }).ToList();

                    var data = context.BusinessData_Get(user.Id).Select(x => new BusinessWeb
                    {
                        Active = x.Active,
                        Description = x.Description,
                        EndDate = x.EndDate,
                        Id = x.Id,
                        InitDate = x.InitDate,
                        Name = x.Name,
                        Premium = x.Premium,
                        Products = products,
                        Style = x.Style,
                        UrlImage = x.UrlImage,
                        User = new BusinessUserWeb { IdBusiness = x.Id, IdUser = user.Id }
                    }).FirstOrDefault();
                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUser(User user)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    var result = context.AspNetUsers.Select(x => new User
                    {
                        Id = x.Id,
                        AccessFailedCount = x.AccessFailedCount,
                        Email = x.Email,
                        IdentificationNumber = x.IdentificationNumber,
                        IdentificationType = x.IdentificationType,
                        Locked = x.Locked,
                        Password = x.PasswordHash,
                        Phone = x.PhoneNumber,
                        UserName = x.UserName
                    }).Where(x => x.Email.Equals(user.Email)).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserForLogin(User user)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    var result = context.AspNetUsers.Select(x => new User
                    {
                        Id = x.Id,
                        AccessFailedCount = x.AccessFailedCount,
                        Email = x.Email,
                        IdentificationNumber = x.IdentificationNumber,
                        IdentificationType = x.IdentificationType,
                        Locked = x.Locked,
                        Password = x.PasswordHash,
                        Phone = x.PhoneNumber,
                        UserName = x.UserName
                    }).Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User SaveUser(User user)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    AspNetUsers userEntity = new AspNetUsers
                    {
                        Id = user.Id,
                        AccessFailedCount = user.AccessFailedCount,
                        Email = user.Email,
                        IdentificationNumber = user.IdentificationNumber,
                        IdentificationType = user.IdentificationType,
                        Locked = user.Locked,
                        PasswordHash = user.Password,
                        PhoneNumber = user.Phone,
                        UserName = user.UserName
                    };
                    context.AspNetUsers.Add(userEntity);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
