﻿using NegociosYContactos.CustomAttributes;
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
                    if (item.Id >= idImageTmp) {
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
                    string savedFileImage = string.Concat(pathImage, "/" ,Path.GetFileName(hpf.FileName));

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
                return Content("{\"files\": [{\"name\":\"" + fileList[0].Name + "\",\"type\":\"" + fileList[0].Type + "\",\"url\":\"" + fileList[0].Url 
                     + "\",\"thumbnailUrl\":\"" + fileList[0].Id + "\",\"deleteUrl\":\"" +  fileList[0].DeleteUrl + 
                     "\",\"deleteType\":\"" + "DELETE" + "\",\"size\":\"" + string.Format("{0} bytes", fileList[0].Size) + "\"}]}", "application/json");
            }
            else
            {
                return Content("{\"files\": [{\"name\":\"" + "limitSize" + "\",\"type\":\"" + "limitSize" + "\",\"url\":\"" + "limitSize" +
                    "\",\"thumbnailUrl\":\"" + "limitSize" + "\",\"deleteUrl\":\"" + "limitSize" + "\",\"deleteType\":\"" +
                    "DELETE" + "\",\"size\":\"" + string.Format("{0} bytes", "0") + "\"}]}",
                    "application/json");
            }
        }

        [HttpPost]
        public ContentResult UploadLogo()
        {
            var fileList = new List<UploadFile>();
            var pathFolder = string.Format(Server.MapPath(_folderTemplate), UserAutenticated.Id);
            var pathImage = string.Format(_folderTemplate, UserAutenticated.Id);

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                Directory.CreateDirectory(pathFolder);

                string savedFileName = Path.Combine(pathFolder, string.Concat("_Portada_", Path.GetFileName(hpf.FileName)));
                string savedFileImage = Path.Combine(pathImage, string.Concat("_Portada_", Path.GetFileName(hpf.FileName)));

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
            return Content("{\"name\":\"" + fileList[0].Name + "\",\"type\":\"" + fileList[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", fileList[0].Size) + "\"}", "application/json");
        }

        public ActionResult SaveData()
        {
            IData data = new Data.Classes.Data();
            data.SaveBusinessWeb(BusinessWeb);
            return View("Index",BusinessWeb);
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
    }
}