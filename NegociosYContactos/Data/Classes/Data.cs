using System;
using System.Linq;
using NegociosYContactos.Models;
using System.Collections.Generic;

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
                        EndDate = x.EndDate,
                        Id = x.Id,
                        InitDate = x.InitDate,
                        Name = x.Name,
                        Premium = x.Premium,
                        Products = products,
                        Style = x.Style,
                        UrlImage = x.UrlImage,
                        User = new BusinessUserWeb { IdBusiness = x.Id, IdUser = user.Id },
                        Address = x.Address,
                        Category = new BusinessCategory { Id = x.IdCategory, Description = x.DescriptionCategory }
                    }).FirstOrDefault();

                    if (data == null)
                    {
                        data = new BusinessWeb
                        {
                            Active = false,
                            EndDate = DateTime.MinValue,
                            Id = 0,
                            InitDate = DateTime.MinValue,
                            Name = string.Empty,
                            Premium = false,
                            Products = products,
                            Style = string.Empty,
                            UrlImage = string.Empty,
                            User = new BusinessUserWeb { IdBusiness = 0, IdUser = string.Empty },
                            Address = string.Empty,
                            Category = new BusinessCategory { Id = 0, Description = string.Empty }
                        };
                    }
                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BusinessWeb SaveBusinessWeb(BusinessWeb businesWeb)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    int idBusinessTemp = 0;
                    if (businesWeb.Id == 0)
                    {
                        var endDateTemp = DateTime.Now.AddYears(1);
                        var actualBusiness = new Business
                        {
                            Name = businesWeb.Name,
                            UrlImage = businesWeb.UrlImage,
                            Style = businesWeb.Style,
                            InitDate = DateTime.Now,
                            EndDate = endDateTemp,
                            Premium = businesWeb.Premium,
                            Active = true,
                            Address = businesWeb.Address,
                            IdCategory = businesWeb.Category.Id
                        };

                        context.AspNetUsers.FirstOrDefault(x => x.Id.Equals(businesWeb.User.IdUser)).Business.Add(actualBusiness);
                        context.SaveChanges();
                        idBusinessTemp = context.Business.Where(x => x.AspNetUsers.Any(y => y.Id.Equals(businesWeb.User.IdUser))).FirstOrDefault().Id;

                        foreach (var item in businesWeb.Products)
                        {
                            context.BusinessProduct.Add(new BusinessProduct
                            {
                                Name = item.Name,
                                Description = item.Description,
                                UrlImage = item.UrlImage,
                                Value = item.Value,
                                IdBusiness = idBusinessTemp
                            });
                        }
                    }
                    else
                    {
                        idBusinessTemp = businesWeb.Id;
                        var actualBusiness = context.Business.FirstOrDefault(x => x.Id == businesWeb.Id);
                        actualBusiness.Name = businesWeb.Name;
                        actualBusiness.UrlImage = businesWeb.UrlImage;
                        actualBusiness.Style = businesWeb.Style;
                        actualBusiness.Address = businesWeb.Address;
                        actualBusiness.IdCategory = businesWeb.Category.Id;

                        foreach (var item in context.BusinessProduct.Where(x => x.IdBusiness == idBusinessTemp))
                        {
                            context.BusinessProduct.Remove(item);
                        }

                        foreach (var item in businesWeb.Products)
                        {
                            context.BusinessProduct.Add(new BusinessProduct
                            {
                                Name = item.Name,
                                Description = item.Description,
                                IdBusiness = idBusinessTemp,
                                UrlImage = item.UrlImage,
                                Value = item.Value
                            });
                        }
                    }
                    context.SaveChanges();
                }
                return businesWeb;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetCategory()
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    return context.Category.Select(x => new
                    {
                        id = x.Id,
                        label = x.Description,
                        category = "Categoria"
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int TypeTerm_Get(string term)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    var result = context.TypeTerm_Get(term);
                    return result.FirstOrDefault().Value;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SearchListViewModel BusinessList_Get(int idCategory, int tamPage, int page)
        {
            try
            {
                SearchListViewModel result = new SearchListViewModel();
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    result.SearchList = context.Business
                        .Where(x => x.IdCategory == idCategory && x.Active == true)
                        .Select(x => new SearchViewModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            UrlImage = string.Concat("../../", x.UrlImage),
                            Address = x.Address,
                            Active = x.Active,
                            InitDate = x.InitDate,
                            EndDate = x.EndDate,
                            Premium = x.Premium,
                            Style = x.Style,
                            IdCategory = x.IdCategory != null ? x.IdCategory.Value : 0
                        }).OrderBy(x => x.Id).Skip(tamPage * page).Take(tamPage).ToList();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BusinessWeb Business_Get(int id, string name)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    List<BusinessProductWeb> products = new List<BusinessProductWeb>();
                    BusinessWeb data = new BusinessWeb();
                    data.Products = products;

                    if (id == 0)
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            var businessTemp = context.Business
                                .Where(x => x.Name.ToLower().Equals(name.ToLower()) && x.Active == true)
                                .FirstOrDefault();
                            if (businessTemp != null)
                            {
                                id = businessTemp.Id;
                            }
                        }
                        if (id == 0)
                        {
                            return data;
                        }
                        else
                        {
                            products = context.BusinessProduct
                                .Where(x => x.IdBusiness.Value == id)
                                .Select(x => new BusinessProductWeb
                                {
                                    Id = x.Id,
                                    IdBusiness = x.IdBusiness != null ? x.IdBusiness.Value : 0,
                                    Description = x.Description,
                                    Name = x.Name,
                                    UrlImage = string.Concat("../../", x.UrlImage),
                                    Value = x.Value
                                }).ToList();

                            data = context.Business
                                .Where(x => x.Id == id)
                                .Select(x => new BusinessWeb
                                {
                                    Active = x.Active,
                                    EndDate = x.EndDate,
                                    Id = x.Id,
                                    InitDate = x.InitDate,
                                    Name = x.Name,
                                    Premium = x.Premium,
                                    Style = x.Style,
                                    UrlImage = string.Concat("../../", x.UrlImage),
                                    Address = x.Address,
                                    Category = new BusinessCategory { Id = x.Category.Id, Description = x.Category.Description }
                                }).FirstOrDefault();

                            data.Products = products;

                            return data;
                        }
                    }
                    else
                    {
                        products = context.BusinessProduct
                            .Where(y => y.IdBusiness.Value == id)
                            .Select(x => new BusinessProductWeb
                            {
                                Id = x.Id,
                                IdBusiness = x.IdBusiness != null ? x.IdBusiness.Value : 0,
                                Description = x.Description,
                                Name = x.Name,
                                UrlImage = string.Concat("../../", x.UrlImage),
                                Value = x.Value
                            }).ToList();

                        data = context.Business
                                .Where(x => x.Id == id)
                                .Select(x => new BusinessWeb
                                {
                                    Active = x.Active,
                                    EndDate = x.EndDate,
                                    Id = x.Id,
                                    InitDate = x.InitDate,
                                    Name = x.Name,
                                    Premium = x.Premium,
                                    Style = x.Style,
                                    UrlImage = string.Concat("../../", x.UrlImage),
                                    Address = x.Address,
                                    Category = new BusinessCategory { Id = x.Category.Id, Description = x.Category.Description }
                                }).FirstOrDefault();

                        data.Products = products;

                        return data;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidateUserName(string userName)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    var result = context.AspNetUsers.Any(x => x.UserName.ToLower().Trim().Equals(userName.Trim()));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductOrderWeb ProductOrderGet(ProductOrderWeb order)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    if (order.Id != 0)
                    {
                        return context.ProductOrder
                            .Where(x => x.Id == order.Id)
                            .Select(x => new ProductOrderWeb
                            {
                                Id = x.Id,
                                Product = context.BusinessProduct
                                 .Where(y => y.Id == order.Product.Id)
                                 .Select(y => new BusinessProductWeb
                                 {
                                     Id = y.Id,
                                     Description = y.Description,
                                     IdBusiness = y.IdBusiness.Value,
                                     Name = y.Name,
                                     UrlImage = string.Concat("../../", y.UrlImage),
                                     Value = y.Value
                                 }).FirstOrDefault(),
                                ContactEmail = x.ContactEmail,
                                ContactPhone = x.ContactPhone,
                                OrderType = x.OrderType,
                                BusinessName = order.BusinessName
                            }).FirstOrDefault();

                    }
                    else
                    {
                        return new ProductOrderWeb
                        {
                            Product = context.BusinessProduct
                                 .Where(y => y.Id == order.Product.Id)
                                 .Select(y => new BusinessProductWeb
                                 {
                                     Id = y.Id,
                                     Description = y.Description,
                                     IdBusiness = y.IdBusiness.Value,
                                     Name = y.Name,
                                     UrlImage = string.Concat("../../", y.UrlImage),
                                     Value = y.Value
                                 }).FirstOrDefault(),
                            ContactEmail = order.ContactEmail,
                            ContactPhone = order.ContactPhone,
                            OrderType = order.OrderType,
                            Id = 0,
                            BusinessName = order.BusinessName
                        };
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductOrderWeb ProductOrderSave(ProductOrderWeb order)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    ProductOrder productOrder = new ProductOrder
                    {
                        ProductName = order.Product.Name,
                        OrderType = order.OrderType,
                        ContactEmail = order.ContactEmail,
                        ContactPhone = order.ContactPhone,
                        DateEvent = DateTime.Now
                    };
                    context.ProductOrder.Add(productOrder);
                    context.SaveChanges();
                    order.Product.Name = context.BusinessProduct.Where(x => x.Id == order.Product.Id).FirstOrDefault().Name;
                    return order;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserForMail(int productId)
        {
            try
            {
                using (ContactosyNegociosEntities context = new ContactosyNegociosEntities())
                {
                    var businessId = context.BusinessProduct.Where(x => x.Id == productId).FirstOrDefault().IdBusiness;                    
                    var result = context.Business.Where(x => x.Id == businessId)
                        .FirstOrDefault()
                        .AspNetUsers
                        .Select(x => new User
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
                        LoginProvider = string.Empty,
                        IsAuthenticated = false,
                        Message = string.Empty,
                        IsTermsAccepted = x.AcceptTerms != null ? x.AcceptTerms.Value : false
                    }).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
