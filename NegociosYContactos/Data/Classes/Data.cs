using System;
using System.Linq;
using NegociosYContactos.Models;

namespace NegociosYContactos.Data.Classes
{
    public class Data : IData
    {
        public User EditUser(User user)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    var userValidate = GetUser(user);
                    if (userValidate.Email.Equals(user.Email) && !userValidate.Id.Equals(user.Id))
                    {
                        user.Message = "emailExists";
                        return user;
                    }
                    else
                    {
                        var actualUser = context.AspNetUsers.FirstOrDefault(x => x.Id.Equals(user.Id));
                        actualUser.Id = user.Id;
                        actualUser.AccessFailedCount = user.AccessFailedCount;
                        actualUser.IdentificationNumber = user.IdentificationNumber;
                        actualUser.IdentificationType = user.IdentificationType;
                        actualUser.Locked = user.Locked;
                        actualUser.Email = user.Email;
                        actualUser.UserName = user.UserName;
                        actualUser.PasswordHash = user.Password;
                        actualUser.PhoneNumber = user.Phone;
                        actualUser.AcceptTerms = user.IsTermsAccepted;
                        user.LoginProvider = string.Empty;
                        user.IsAuthenticated = false;
                        user.Message = string.Empty;

                        context.SaveChanges();
                        return user;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

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
                        UserName = x.UserName,
                        LoginProvider = user.LoginProvider,
                        IsAuthenticated = false,
                        Message = string.Empty,
                        IsTermsAccepted = x.AcceptTerms != null ? x.AcceptTerms.Value : false                        
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
                        UserName = x.UserName,
                        LoginProvider = user.LoginProvider,
                        IsAuthenticated = false,
                        Message = string.Empty,
                        IsTermsAccepted = x.AcceptTerms != null ? x.AcceptTerms.Value : false
                    }).Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserForLoginExternal(User user)
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
                        UserName = x.UserName,
                        IsTermsAccepted = x.AcceptTerms != null ? x.AcceptTerms.Value : false,
                        LoginProvider = user.LoginProvider,
                        IsAuthenticated = false,
                        Message = string.Empty
                    }).Where(x => x.Email.Equals(user.Email)).FirstOrDefault();

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
                        UserName = user.UserName,
                        AcceptTerms = user.IsTermsAccepted
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
