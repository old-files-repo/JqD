using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JqD.Infrustruct.Enums;
using JqueryDapper.ViewModels;

namespace JqueryDapper.Controllers
{
    public class InfrustructController : Controller
    {
        [HttpPost]
        public JsonResult AddImages()
        {
            var files = Request.Files;
            var urlList = new List<string>();
            if (files.Count <= 0)
            {
                return Json(new { errno = 1, data = urlList });
            }
            var file = files[0];
            var path = Server.MapPath("~/images/LoadImages/"); //存储图片的文件夹
            if (file == null)
            {
                return Json(new { errno = 1, data = urlList });
            }
            var originalFileName = file.FileName;
            var fileExtension = originalFileName.Substring(originalFileName.LastIndexOf('.'),
                originalFileName.Length - originalFileName.LastIndexOf('.'));
            var currentFileName = new Random().Next() + fileExtension; //文件名中不要带中文，否则会出错
            var imagePath = path + currentFileName;  //生成文件路径
            //保存文件
            file.SaveAs(imagePath);
            //获取图片url地址
            var imgUrl = "/images/LoadImages/" + currentFileName;
            urlList.Add(imgUrl);
            return Json(new { errno = 0, data = urlList });
        }

        [HttpPost]
        public JsonResult GetCategory()
        {
            var list = new List<EnumViewModel>();
            foreach (var value in Enum.GetValues(typeof(Enums.Category)))
            {
                var s = new EnumViewModel
                {
                    EnumString = value.ToString(),
                    Id = Convert.ToInt32(value)
                };
                list.Add(s);
            }
            return Json(list);
        }
    }
}