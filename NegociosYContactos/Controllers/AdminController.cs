using NegociosYContactos.Data;
using NegociosYContactos.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class AdminController : BaseController
    {
        private string _folderTemplate = "~/App_Data/ClientImages/{0}";
        // GET: Admin
        public ActionResult Index()
        {
            Data.Classes.Data data = new Data.Classes.Data();
            BusinessWeb businessWeb = new BusinessWeb();
            if (this.UserAutenticated != null)
            {
                businessWeb = data.GetBusinessData(this.UserAutenticated);
               
            }
            return View(businessWeb);
        }

        [HttpPost]
        public ContentResult UploadFiles()
        {
            var fileList = new List<UploadFile>();
            var pathFolder = string.Format(Server.MapPath(_folderTemplate), "AAAA");

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                System.IO.Directory.CreateDirectory(pathFolder);

                string savedFileName = Path.Combine(pathFolder, Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName); // Save the file

                fileList.Add(new UploadFile()
                {
                    Name = hpf.FileName,
                    Size = hpf.ContentLength,
                    Type = hpf.ContentType
                });
            }

            // Returns json
            return Content("{\"files\": [{\"name\":\"" + fileList[0].Name + "\",\"type\":\"" + fileList[0].Type + "\",\"url\":\"" + fileList[0].Url +
                "\",\"thumbnailUrl\":\"" + fileList[0].ThumbnailUrl + "\",\"deleteUrl\":\"" + fileList[0].DeleteUrl + "\",\"deleteType\":\"" +
                "DELETE" + "\",\"size\":\"" + string.Format("{0} bytes", fileList[0].Size) + "\"}]}",
                "application/json");
        }

        [HttpPost]
        public ContentResult UploadLogo()
        {
            var fileList = new List<UploadFile>();
            var pathFolder = string.Format(Server.MapPath(_folderTemplate), "AAAA");

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                System.IO.Directory.CreateDirectory(pathFolder);

                string savedFileName = Path.Combine(pathFolder, string.Concat("_Portada_", Path.GetFileName(hpf.FileName)));
                hpf.SaveAs(savedFileName); // Save the file

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

        public ActionResult SaveData(string business, string businessProduct)
        {

            ContactosyNegociosEntities db = new ContactosyNegociosEntities();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Business));
            MemoryStream stream1 = new MemoryStream(Encoding.UTF8.GetBytes(business));
            stream1.Position = 0;
            Business businessObject = (Business)serializer.ReadObject(stream1);
            db.Business.Add(businessObject);

            serializer = new DataContractJsonSerializer(typeof(BusinessProduct));
            stream1 = new MemoryStream(Encoding.UTF8.GetBytes(businessProduct));
            stream1.Position = 0;
            BusinessProduct businessProductObject = (BusinessProduct)serializer.ReadObject(stream1);
            db.BusinessProduct.Add(businessProductObject);

            return View("Index");
        }

        public ActionResult GetData()
        {

            Data.Classes.Data data = new Data.Classes.Data();
            BusinessWeb businessWeb = new BusinessWeb();
            if (this.UserAutenticated != null)
            {
                businessWeb = data.GetBusinessData(this.UserAutenticated);

            }

            return View("Index");
        }
    }
}