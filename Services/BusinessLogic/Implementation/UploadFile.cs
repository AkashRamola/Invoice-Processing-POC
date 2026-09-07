using DTO.Data;
using Microsoft.AspNetCore.Http;
using Services.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessLogic.Implementation
{
    public class UploadFile : IUploadFile
    {
        public async Task<UploadFileStatusDTO> Uploadpdf(IFormFile pdfFile)
        {
            UploadFileStatusDTO result = new UploadFileStatusDTO();
            try
            {
                if (pdfFile == null || pdfFile.Length == 0)
                {
                    result.Success = false;
                    result.Message = "Please select a PDF file.";
                    return result;
                    //return Json(new
                    //{
                    //    success = false,
                    //    message = "Please select a PDF file."
                    //});
                }

                // Create upload folder if it doesn't exist
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Create unique file name
                string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Path.GetExtension(pdfFile.FileName)}";

                // Full file path
                string filePath = Path.Combine(uploadFolder, fileName);

                // Save file
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await pdfFile.CopyToAsync(stream);
                }
                result.Success = true;
                result.Message = "PDF uploaded successfully.";
                result.FileName = fileName;
                return result;
                //return Json(new
                //{
                //    success = true,
                //    message = "PDF uploaded successfully.",
                //    fileName = fileName
                //});
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
                //return Json(new
                //{
                //    success = false,
                //    message = ex.Message
                //});
            }
        }
    }
}
