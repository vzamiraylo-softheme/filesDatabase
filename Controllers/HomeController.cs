using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;
using filesDatabase.Filters;
using filesDatabase.Models;
using System.Net;

namespace filesDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly FilesDatabaseClass1DataContext _db = new FilesDatabaseClass1DataContext();
        public int currentUserId;
        public string currentUserName;

        [Authorize]
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            currentUserId = WebSecurity.CurrentUserId;
            currentUserName = WebSecurity.CurrentUserName;

            dynamic users;
            using (var db = WebMatrix.Data.Database.Open("DefaultConnection"))
            {
                users = db.Query("SELECT* FROM UserProfile");
            }

            
            var files = (from x in _db.filesTables
                         where x.userId == currentUserId
                         select x).ToList();

            return View(GenerateUserFileModels(files));
        }

        public List<userFile> GenerateUserFileModels(List<filesTable> files)
        {
            
            List<userFile> filelist = (from file in files
                                       let categories = (from x in _db.FileToCategories
                                                         where x.FileId == file.id
                                                         select x.Category).ToList()
                                       select new userFile(file, categories)).ToList();

            return(filelist);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddFile(IEnumerable<HttpPostedFileBase> files, string fileName, string fileDescription, string categories)
        {
            int newFileId = 0;

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        if (filename != null)
                        {
                            var physicalPath = Path.Combine(Server.MapPath("~/uploads"), filename);

                            file.SaveAs(physicalPath);

                            filesTable newModel = new filesTable
                                {
                                    fileName = fileName == "" ? "Unknown" : fileName,
                                    fileDescription = fileDescription == "" ? "Unknown" : fileDescription,
                                    filePath = physicalPath,
                                    userId = WebSecurity.CurrentUserId,
                                    userName = WebSecurity.CurrentUserName
                                };
                            _db.filesTables.InsertOnSubmit(newModel);
                            _db.SubmitChanges();

                            /*get categories to save in*/
                            var fileCats = from s in categories.Split(',') orderby s descending select s;
                            newFileId = newModel.id;

                            if (fileCats != null)
                            {
                                foreach (var fileCat in fileCats)
                                {
                                    int currentId = 0;
                                    if (fileCat != "") currentId = Int32.Parse(fileCat);

                                    FileToCategory intersectModel = new FileToCategory
                                        {
                                            FileId = newModel.id,
                                            CatId = currentId
                                        };
                                    _db.FileToCategories.InsertOnSubmit(intersectModel);
                                }
                                _db.SubmitChanges();
                            }

                        }
                    }
                }
            }

                var newFile = (from x in _db.filesTables
                              where x.id == newFileId
                              select x).ToList();

            return PartialView("_FilesList", GenerateUserFileModels(newFile));
            
        }

        [Authorize]
        public ActionResult LargeView(int id)
        {
            var file = (from x in _db.filesTables
                       where x.id == id
                       select x).ToList();

            return View(GenerateUserFileModels(file));
        }

        public FileContentResult GetImage(int id)
        {
            filesTable file = _db.filesTables.FirstOrDefault(x => x.id == id);
            byte[] array = System.IO.File.ReadAllBytes(file.filePath);
            return new FileContentResult(array, "image/jpeg");
        }

        [Authorize]
        [HttpPost]
        public JsonResult GetCategories()
        {
            var categories = _db.Categories.ToList();
            return Json(categories.Select(x => new
                {
                    id = x.Id,
                    catName = x.CatName
                }));
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetChannelByCategory(int catId)
        {
            List<filesTable> files;

            if (catId != -1)
            {
                files = (from x in _db.FileToCategories
                        where x.CatId == catId
                        select x.filesTable).ToList();
            }
            else
            {
                files = (from x in _db.filesTables
                            select x).ToList(); 
            }

            files.OrderByDescending(x => x.id);

            return PartialView("_FilesList", GenerateUserFileModels(files));
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddNewCategory(string catName)
        {
            var categories = from x in _db.Categories
                             select x;
            foreach (var category in categories)
            {
                if(category.CatName == catName)
                    return Json(new { message = "You have the same category name!", result = false });
            }

            Category model = new Category
                {
                    CatName = catName
                };
            _db.Categories.InsertOnSubmit(model);
            _db.SubmitChanges();


            return Json(new {message = " ' " + catName + " ' category created.", result = true});
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCategoriesToFile(string cat_ids, int file_id)
        {
            if (cat_ids != "")
            {
                var fileCats = from s in cat_ids.Split(',') orderby s descending select s;
                foreach (var fileCat in fileCats)
                {
                    bool file = _db.FileToCategories.Any(x => x.CatId == Int32.Parse(fileCat) && x.FileId == file_id);
                    if(file)continue;

                    var item = new FileToCategory
                        {
                            CatId = Int32.Parse(fileCat),
                            FileId = file_id
                        };
                    _db.FileToCategories.InsertOnSubmit(item);
                }
                _db.SubmitChanges();

                return Json(new { message = "Added", result = true });
            }
            if (cat_ids == "")
            {
                return Json(new {message = "Select categories to add", result = false});
            }

            return Json(new { message = "Something went wrong!", result = false });
        }

        [Authorize]
        [HttpPost]
        public ActionResult RenameCategory(int id, string newName)
        {
            try
            {
                var category = (from x in _db.Categories
                                where x.Id == id
                                select x).FirstOrDefault();
                category.CatName = newName;
                _db.SubmitChanges();
                return Json(new { message = "Category name changed.", result = true });
            }
            catch
            {
                return Json(new { message = "Something went wrong!", result = false });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteFile(int id)
        {
            var file = (from x in _db.filesTables
                        where x.id == id
                        select x).FirstOrDefault();
            if (file != null)
            {
                _db.filesTables.DeleteOnSubmit(file);
                _db.SubmitChanges();

                var intersections = from x in _db.FileToCategories
                                    where x.FileId == id
                                    select x;
                foreach (var match in intersections)
                {
                    _db.FileToCategories.DeleteOnSubmit(match);
                }

                _db.SubmitChanges();

                return Json(new {message = " File deleted! ", result = true});
            }
            else{
                return Json(new { message = " Something went wrong! ", result = false });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            var file = (from x in _db.Categories
                        where x.Id == id
                        select x).FirstOrDefault();
            if (file != null)
            {
                _db.Categories.DeleteOnSubmit(file);
                _db.SubmitChanges();

                if (RefreshCategory(id))
                {
                    return Json(new { message = " Category deleted! ", result = true });
                }
                return Json(new { message = " Something went wrong! ", result = false });
                
            }
            else
            {
                return Json(new { message = " Something went wrong! ", result = false });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteFileFromCategory(int cat_id, int doc_id)
        {
            try
            {
                var file = from x in _db.FileToCategories
                           where x.CatId == cat_id
                           where x.FileId == doc_id
                           select x;
                foreach (var item in file)
                {
                    _db.FileToCategories.DeleteOnSubmit(item);

                }

                _db.SubmitChanges();

                return Json(new {message = " File deleted from this category! ", result = true});
            }
            catch
            {
                return Json(new {message = " Something went wrong! ", result = false});
            }

        }

        public bool RefreshCategory(int id)
        {
            try
            {
                var files = from x in _db.FileToCategories
                            where x.CatId == id
                            select x;
                foreach (var file in files)
                {
                   file.CatId = 0;
                }
                _db.SubmitChanges();
            }
            catch
            {
                return false;
            }
            return true;
            
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetSharedContent()
        {

            var files = (from x in _db.filesTables
                         select x).ToList();

            return PartialView("_SharedList", GenerateUserFileModels(files));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
