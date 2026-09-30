using DAL.Database;
using DTO.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.BusinessLogic.Interface;
using UCAIDataBase.DataBase;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Invoice_Processing_POC.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IUploadFile _uploadfile;
        private readonly ILogger<UploadFileController> _logger;
        private readonly InvoiceProcessingDbContext _context;
        public UploadFileController(IUploadFile uploadfile, ILogger<UploadFileController> logger, InvoiceProcessingDbContext context)
        {
            _context = context;
            _logger = logger;
            _uploadfile = uploadfile;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
        {
            UploadFileStatusDTO result = new UploadFileStatusDTO();
            try
            {
                var data = new UploadDocument();
                result = await _uploadfile.Uploadpdf(pdfFile);
                if(result.Success)
                {

                    data.FileRealName = result.FileName;
                    data.FileName = result.uniqueFileName;
                    data.UploadDate = DateTime.Now;
                    data.source = "upload";
                }
                if (!result.Success)
                {
                    result.Success = false;
                    return Ok(result);
                }
                var extractedText = await _uploadfile.UploadedFileProcess(result);

                data.ExtractedInfo= extractedText;
                await _context.UploadDocuments.AddAsync(data);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "File uploaded successfully.",
                    success = true,
                });
            }

            catch(Exception e)
            {
                _logger.LogError(e, "Error in uploading invoice");
                throw;
            }
            
        }
        public async Task<IActionResult> UploadedFile()
        {
            var uploadedFiles = await _uploadfile.GetAllUploadedFiles();

            return View(uploadedFiles);
        }
    } 
}
