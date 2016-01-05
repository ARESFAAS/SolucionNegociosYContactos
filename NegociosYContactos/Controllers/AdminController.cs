using NegociosYContactos.CustomAttributes;
using NegociosYContactos.Data.Classes;
using NegociosYContactos.Models;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    [BasicAuth]
    public class AdminController : BaseController
    {
        private string _folderTemplate = "../ClientImages/{0}";
        private string _folderLogoTemplate = "../ClientImages/{0}/Portada";
        // GET: Admin
        public ActionResult Index()
        {
            Data.Classes.Data data = new Data.Classes.Data();

            if (UserAutenticated != null)
            {
                if (BusinessWeb.Id == 0)
                {
                    // data Base
                    BusinessWeb = data.GetBusinessData(UserAutenticated);
                    BusinessWeb.User.IdUser = UserAutenticated.Id;
                }
            }

            return View(BusinessWeb);
        }

        [HttpPost]
        public ContentResult UploadFiles()
        {
            int idImageTmp = 0;
            var totalImage = 0;
            if (!BusinessWeb.Premium)
            {
                totalImage = int.Parse(System.Configuration.ConfigurationManager.AppSettings["totalImageFree"]);
            }
            else
            {
                totalImage = int.Parse(System.Configuration.ConfigurationManager.AppSettings["totalImagePremium"]);
            }

            if (BusinessWeb.Products.Count < totalImage)
            {
                foreach (var item in BusinessWeb.Products)
                {
                    if (item.Id >= idImageTmp)
                    {
                        idImageTmp = item.Id;
                    }
                }

                idImageTmp += 1;
                var fileList = new List<UploadFile>();
                var pathFolder = string.Format(Server.MapPath(_folderTemplate), UserAutenticated.Id);
                var pathImage = string.Format(_folderTemplate, UserAutenticated.Id);

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    Directory.CreateDirectory(pathFolder);

                    string savedFileName = Path.Combine(pathFolder, Path.GetFileName(hpf.FileName));
                    string savedFileImage = string.Concat(pathImage, "/", Path.GetFileName(hpf.FileName));

                    hpf.SaveAs(savedFileName); // Save the file

                    // save temporal images                
                    BusinessWeb.Products.Add(new BusinessProductWeb
                    {
                        UrlImage = savedFileImage,
                        Id = idImageTmp,
                        Description = string.Empty,
                        Name = string.Empty,
                        Value = string.Empty,
                        IdBusiness = BusinessWeb.Id
                    });

                    fileList.Add(new UploadFile()
                    {
                        Name = hpf.FileName,
                        Size = hpf.ContentLength,
                        Type = hpf.ContentType,
                        Url = savedFileImage,
                        Id = idImageTmp
                    });
                }

                // Returns json
                return Content(
                    "{\"files\": [{\"name\":\"" + fileList[0].Name +
                    "\",\"type\":\"" + fileList[0].Type +
                    "\",\"url\":\"" + fileList[0].Url +
                     "\",\"thumbnailUrl\":\"" + fileList[0].ThumbnailUrl +
                     "\",\"deleteUrl\":\"" + fileList[0].DeleteUrl +
                     "\",\"deleteType\":\"" + "DELETE" +
                     "\",\"size\":\"" + string.Format("{0} bytes", fileList[0].Size) +
                     "\",\"id\":\"" + fileList[0].Id +
                     "\"}]}", "application/json");
            }
            else
            {
                return Content(
                    "{\"files\": [{\"name\":\"" + "limitSize" +
                    "\",\"type\":\"" + "limitSize" +
                    "\",\"url\":\"" + "limitSize" +
                    "\",\"thumbnailUrl\":\"" + "limitSize" +
                    "\",\"deleteUrl\":\"" + "limitSize" +
                    "\",\"deleteType\":\"" + "DELETE" +
                    "\",\"size\":\"" + string.Format("{0} bytes", "0") +
                    "\"}]}", "application/json");
            }
        }

        [HttpPost]
        public ContentResult UploadLogo()
        {
            var fileList = new List<UploadFile>();
            var pathFolder = string.Format(Server.MapPath(_folderLogoTemplate), UserAutenticated.Id);
            var pathImage = string.Format(_folderLogoTemplate, UserAutenticated.Id);
            string savedFileImage = string.Empty;

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                Directory.CreateDirectory(pathFolder);

                string savedFileName = Path.Combine(pathFolder, string.Concat("_Portada_", Path.GetFileName(hpf.FileName)));
                savedFileImage = string.Concat(pathImage, "/", "_Portada_", Path.GetFileName(hpf.FileName));

                hpf.SaveAs(savedFileName); // Save the file

                //Save Temporal Logo
                BusinessWeb.UrlImage = savedFileImage;

                fileList.Add(new UploadFile()
                {
                    Name = hpf.FileName,
                    Size = hpf.ContentLength,
                    Type = hpf.ContentType
                });
            }

            // Returns json
            return Content("{\"name\":\"" + fileList[0].Name +
                "\",\"type\":\"" + fileList[0].Type +
                "\",\"size\":\"" + string.Format("{0} bytes", fileList[0].Size) +
                "\",\"savedFileImage\":\"" + savedFileImage +
                "\"}", "application/json");
        }

        public ActionResult SaveData()
        {
            // maintenance functions
            DeleteFiles();
            DeleteLogo();

            IData data = new Data.Classes.Data();
            data.SaveBusinessWeb(BusinessWeb);
            return View("Index", BusinessWeb);
        }

        public ActionResult GetData()
        {

            Data.Classes.Data data = new Data.Classes.Data();
            BusinessWeb businessWeb = new BusinessWeb();
            if (this.UserAutenticated != null)
            {
                businessWeb = data.GetBusinessData(UserAutenticated);
            }

            return View("Index");
        }

        public JsonResult UpdateNameTemp(string newName)
        {
            BusinessWeb.Name = newName;
            return Json(new { Message = "ok" });
        }

        public JsonResult UpdateStyleTemp(string newStyle)
        {
            BusinessWeb.Style = newStyle;
            return Json(new { Message = "ok" });
        }

        public JsonResult UpdateProductInformation(List<BusinessProductWeb> businessProductList)
        {
            BusinessWeb.Products = businessProductList;
            return Json(new { Message = "ok" });
        }

        public JsonResult GetProductTemp()
        {
            return Json(BusinessWeb.Products);
        }

        public JsonResult DeleteImageTemp(string id)
        {
            BusinessProductWeb productTemp = BusinessWeb.Products.Find(x => x.Id.ToString().Equals(id));
            BusinessWeb.Products.Remove(productTemp);
            return Json(new { Message = "ok" });
        }

        public JsonResult UpdateAddressTemp(string newAddress)
        {
            BusinessWeb.Address = newAddress;
            return Json(new { Message = "ok" });
        }

        public ActionResult CancelChange()
        {

            Data.Classes.Data data = new Data.Classes.Data();
            BusinessWeb = data.GetBusinessData(UserAutenticated);
            BusinessWeb.User.IdUser = UserAutenticated.Id;

            // maintenance functions
            DeleteFiles();
            DeleteLogo();

            return View("Index", BusinessWeb);
        }

        private void DeleteFiles()
        {
            var pathFolder = string.Format(Server.MapPath(_folderTemplate), UserAutenticated.Id);
            string[] fileList = Directory.GetFiles(pathFolder, "*.*");
            foreach (var file in fileList)
            {
                var fileNameTemp = Path.GetFileName(file);
                var deleteFile = true;
                foreach (var product in BusinessWeb.Products)
                {
                    var productImageTemp = Path.GetFileName(product.UrlImage);
                    if (productImageTemp.Equals(fileNameTemp))
                    {
                        deleteFile = false;
                        break;
                    }
                }
                if (deleteFile)
                {
                    System.IO.File.Delete(file);
                }
            }

        }

        private void DeleteLogo()
        {
            var pathFolder = string.Format(Server.MapPath(_folderLogoTemplate), UserAutenticated.Id);
            string[] fileList = Directory.GetFiles(pathFolder, "*.*");
            foreach (var file in fileList)
            {
                var fileNameTemp = Path.GetFileName(file);
                var deleteFile = true;
                var logoImageTemp = Path.GetFileName(BusinessWeb.UrlImage);

                if (logoImageTemp.Equals(fileNameTemp))
                {
                    deleteFile = false;
                }

                if (deleteFile)
                {
                    System.IO.File.Delete(file);
                }
            }

        }
    }
}