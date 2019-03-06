using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreDemo.Pages
{
    public class FileUploadModel : PageModel
    {
        IHostingEnvironment _environment;
        IFileProvider _fileProvider;

        [BindProperty]
        public IFormFile FileUpload { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty]
        public string SuccessMessage { get; set; }
        [BindProperty]
        public Dictionary<string, string> DirectoryContents { get; set; }

        public FileUploadModel(IHostingEnvironment env, IFileProvider fileProvider)
        {
            _environment = env;
            _fileProvider = fileProvider;
            ClearMessages();
        }

        public void OnGet()
        {
            DirectoryContents = new Dictionary<string, string>();
            foreach(var item in _fileProvider.GetDirectoryContents("Uploads").ToList())
            {
                DirectoryContents.Add(item.Name, Path.Combine(_environment.ContentRootPath, "Uploads", item.Name));
            }            
        }

        public async Task<IActionResult> OnPostUploadFile()
        {
            ClearMessages();
            try
            {
                string filePath = Path.Combine(_environment.ContentRootPath, "Uploads", FileUpload.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);
                }
                SuccessMessage = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ErrorMessage = "Unable to upload file.";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> OnGetDownloadFile(string fileName)
        {
            if(fileName == null)
            {
                ErrorMessage = "Something went wrong. Please try again later.";
                return RedirectToAction("Index");
            }

            string path = Path.Combine(_environment.ContentRootPath, "Uploads", fileName);
            MemoryStream memory = new MemoryStream();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, GetContentType(path), Path.GetFileName(path));

        }

        private void ClearMessages()
        {
            ErrorMessage = "";
            SuccessMessage = "";
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}