using GrantRequests.WEB.Models;
using System;
using System.Web;

namespace GrantRequests.Common
{
    public static class ServerExtensions
    {
        const string systemPath = @"~/App_Files/";
        const string systemType = @"application/";
        const string systemPostfix = @"File";
        public static string SetFileOnServer(this HttpServerUtilityBase Server, HttpPostedFileBase file)
        {
            string result = null;
            if (file != null)
            {
                string fileExtension = System.IO.Path.GetExtension(file.FileName);
                result = Guid.NewGuid() + fileExtension;
                file.SaveAs(Server.MapPath(systemPath + result));
            }
            return result;
        }

        public static string GetFilePathFromServer(this HttpServerUtilityBase Server, string filePath)
        {
            return Server.MapPath(systemPath + filePath);
        }
        public static string GetFileContentTypeFromServer(this HttpServerUtilityBase Server, string filePath)
        {
            return systemType + System.IO.Path.GetExtension(filePath).Substring(1);
        }

        public static void SetAllFileOnServer(this HttpServerUtilityBase Server, InformationViewModelBase model)
        {
            var properties = model.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (item.PropertyType.IsClass && item.PropertyType.IsAssignableFrom(typeof(HttpPostedFileBase)) && item.GetValue(model) != null)
                {
                    var fileName = item.Name.Substring(0, item.Name.IndexOf(systemPostfix));
                    foreach (var item2 in properties)
                        if (item2.Name == fileName)
                            item2.SetValue(model, Server.SetFileOnServer((HttpPostedFileBase)item.GetValue(model)));
                }
            }
        }
    }
}