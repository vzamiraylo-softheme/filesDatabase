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
        public string username;

        [Authorize]
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "Name", autoCreateTables: true);
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            username = GetUsername();

            var files = (from x in _db.filesTables
                         where x.userName == username
                         select x).OrderByDescending(x => x.id).ToList();

            return View(GenerateUserFileModels(files));
        }

        public string GetUsername()
        {
            try
            {
                return FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            }
            catch
            {
                return null;
            }
        }

        public int GetUserId()
        {
            try
            {
                return _db.Users.FirstOrDefault(x => x.userName == GetUsername()).userID;
            }
            catch
            {
                return 0;
            }
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
            List<filesTable> list = new List<filesTable>();

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        if (filename != null)
                        {
                            var physicalPath = Path.Combine(Server.MapPath("~/uploads/files"), filename);

                            file.SaveAs(physicalPath);

                            filesTable newModel = new filesTable
                                {
                                    fileName = fileName == "" ? "Unknown" : fileName,
                                    fileDescription = fileDescription == "" ? "Unknown" : fileDescription,
                                    filePath = physicalPath,
                                    userName = GetUsername()
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
                        var newFile = (from x in _db.filesTables
                                       where x.id == newFileId
                                       select x).FirstOrDefault();
                        list.Add(newFile);
                    }
                }
            }


            return PartialView("_FilesList", GenerateUserFileModels(list));
            
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


        public FileContentResult GetAvatar(string avatarType, string nameOfUser)
        {
            var defaultAvatar =
                    "D:/Prjcts/ASP.NET MVC4 DocsDatabase/filesDatabase/filesDatabase/uploads/avatars/defaultAvatar.png";
            byte[] array = { };

            try
            {

                if (avatarType == "currentUser")
                {
                    var user = _db.Users.FirstOrDefault(x => x.userName == GetUsername());

                    if (user.avatar != null)
                    {
                        array = System.IO.File.ReadAllBytes(user.avatar);
                    }
                    else if (user.avatar == null)
                    {
                        array = System.IO.File.ReadAllBytes(defaultAvatar);
                    }

                    return new FileContentResult(array, "image/jpeg");
                }
                if (avatarType == "share")
                {
                    var user = _db.Users.FirstOrDefault(x => x.userName == nameOfUser);

                    if (user.avatar != null)
                    {
                        array = System.IO.File.ReadAllBytes(user.avatar);
                    }
                    else if (user.avatar == null)
                    {
                        array = System.IO.File.ReadAllBytes(defaultAvatar);
                    }

                    return new FileContentResult(array, "image/jpeg");
                }

            }
            catch
            {
                array = System.IO.File.ReadAllBytes(defaultAvatar);
                return new FileContentResult(array, "image/jpeg"); 
            }
            
            array = System.IO.File.ReadAllBytes(defaultAvatar);
            return new FileContentResult(array, "image/jpeg");
        }

    [HttpPost]
    public ActionResult DeleteAvatar()
    {
        var defaultAvatar =
                    "../uploads/avatars/defaultAvatar.png";

        try
        {
            var userAvatar = (from x in _db.Users
                             where x.userName == GetUsername()
                             select x).FirstOrDefault();
            userAvatar.avatar = null;
            _db.SubmitChanges();

            return Json(new { message = "Deleted", result = true, avatar = defaultAvatar });
        }
        catch
        {
            return Json(new{message = "Something went wrong!", result = false});
        }
    }

        [HttpPost]
        public ActionResult UploadAvatar(IEnumerable<HttpPostedFileBase> avatar_input)
        {
            try
            {
                var avatar = avatar_input.First();
                var user = _db.Users.FirstOrDefault(x => x.userName == GetUsername());
                var filename = Path.GetFileName(avatar.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/uploads/avatars"), filename);
                avatar.SaveAs(physicalPath);

                user.avatar = physicalPath;
                _db.SubmitChanges();

                byte[] array = System.IO.File.ReadAllBytes(physicalPath);

                return Json(new {result = true, avatar = "../uploads/avatars/" + filename});
            }
            catch
            {
                return Json(new { result = false, message = "Something went wrong!"});
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult GetCategories()
        {
            var categories = _db.Categories.Where(x => x.UserName == GetUsername()).ToList();
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
                        where x.filesTable.userName == GetUsername()
                        select x.filesTable).ToList();
            }
            else
            {
                files = (from x in _db.filesTables
                         where x.userName == GetUsername()
                            select x).ToList(); 
            }

            files.OrderByDescending(x => x.id);

            return PartialView("_FilesList", GenerateUserFileModels(files));
        }

        [HttpPost]
        public ActionResult GetThumbsForShare()
        {
            List<filesTable> files;

                files = (from x in _db.filesTables
                         where x.userName == GetUsername()
                         select x).ToList();

            files.OrderByDescending(x => x.id);

            return PartialView("_shareFileTpl", GenerateUserFileModels(files));
        }

        [HttpPost]
        public ActionResult UserSearch(string type, string str)
        {
            /*TODO*/
            /*if (type == "share")
            {*/
                List<User> users = (from x in _db.Users
                            where x.userName.Contains(str)
                                    select x).OrderBy(x => x.userName).ToList();

                return PartialView("_userSearchShare", users);
            //}
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddNewCategory(string catName)
        {
            var categories = from x in _db.Categories
                             where x.UserName == GetUsername()
                             select x;
            foreach (var category in categories)
            {
                if(category.CatName == catName)
                    return Json(new { message = "You have the same category name!", result = false });
            }

            Category model = new Category
                {
                    CatName = catName,
                    UserName = GetUsername()
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
                    bool file = _db.FileToCategories.Where(x => x.Category.UserName == GetUsername()).Any(x => x.CatId == Int32.Parse(fileCat) && x.FileId == file_id);
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
                                where x.UserName == GetUsername()
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

                var sharedRecords = _db.sharedContents.Where(x => x.fileId == file.id).ToList();
                foreach (var item in sharedRecords)
                {
                    _db.sharedContents.DeleteOnSubmit(item);
                }

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
        public ActionResult DeleteSharedFile(int id)
        {
            var file = _db.sharedContents.FirstOrDefault(x => x.userId == GetUserId() && x.fileId == id);
            _db.sharedContents.DeleteOnSubmit(file);

            _db.SubmitChanges();

            return Json(new { message = " File deleted! ", result = true });
            
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            var category = (from x in _db.Categories
                        where x.Id == id
                        select x).FirstOrDefault();
            if (category != null)
            {
                _db.Categories.DeleteOnSubmit(category);
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
                var file = (from x in _db.FileToCategories
                           where x.CatId == cat_id
                           where x.FileId == doc_id
                           select x).FirstOrDefault();
                    _db.FileToCategories.DeleteOnSubmit(file);

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
                   _db.FileToCategories.DeleteOnSubmit(file);
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
            var currentUserId = _db.Users.Where(x => x.userName == GetUsername()).Select(x => x.userID).FirstOrDefault();

            var files = (from x in _db.sharedContents
                         where x.userId == currentUserId
                         select x).OrderByDescending(x => x.Id).ToList();

            return PartialView("_SharedList", files);
        }

        [HttpPost]
        public ActionResult deleteAllFiles()
        {
            try
            {
                var files = _db.filesTables.Where(x => x.userName == GetUsername()).ToList();
                var categories = _db.Categories.Where(x => x.UserName == GetUsername()).ToList();
                foreach (var category in categories)
                {
                    _db.Categories.DeleteOnSubmit(category);
                }
                foreach (var file in files)
                {
                    var intersectionRecords = _db.FileToCategories.Where(x => x.FileId == file.id).ToList();
                    var sharedRecords = _db.sharedContents.Where(x => x.fileId == file.id).ToList();
                    foreach (var field in sharedRecords)
                    {
                        _db.sharedContents.DeleteOnSubmit(field);
                    }
                    foreach (var intersectionRecord in intersectionRecords)
                    {
                        _db.FileToCategories.DeleteOnSubmit(intersectionRecord);
                    }
                    _db.filesTables.DeleteOnSubmit(file);
                }
                _db.SubmitChanges();

                return Json(new {result = true, message = "All your files deleted!"});
            }
            catch
            {
                return Json(new { result = false, message = "Somwthing went wrong!" });
            }
        }

        [HttpPost]
        public ActionResult deleteAllSharedFiles()
        {
            try
            {
                var files = _db.sharedContents.Where(x => x.userId == GetUserId()).ToList();
                foreach (var file in files)
                {
                    _db.sharedContents.DeleteOnSubmit(file);
                }
                _db.SubmitChanges();
                return Json(new {result = true, message = "All your files deleted!"});
            }
            catch
            {
                return Json(new { result = false, message = "Somwthing went wrong!" });
            }
        }

    [HttpPost]
    public ActionResult ShareContent(string files, string users)
    {
        var filesArray = files.Split(',').Select(x => Int32.Parse(x)).ToArray();
        var usersArray = users.Split(',').Select(x => Int32.Parse(x)).ToArray();

        try
        {
            foreach (var user in usersArray)
            {
                foreach (var file in filesArray)
                {
                    sharedContent item = new sharedContent
                        {
                            userId = user,
                            fileId = file
                        };
                    _db.sharedContents.InsertOnSubmit(item);
                }
            }
            _db.SubmitChanges();

            return Json(new {result = true, message = "Files have been shared!"});
        }
        catch
        {
            return Json(new { result = false, message = "Something went wrong!" });
        }
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
