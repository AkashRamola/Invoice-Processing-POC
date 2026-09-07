using Microsoft.AspNetCore.Mvc;
using Services.BusinessLogic.Interface;

namespace Invoice_Processing_POC.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IUploadFile _uploadfile;
        public IActionResult Index()
        {
            return View();
        }
        public UploadFileController(IUploadFile uploadfile)
        {
            _uploadfile=uploadfile;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
        {
            var result= await _uploadfile.Uploadpdf(pdfFile);
            //return Json(result);
            return Ok(result);
        }
            
    } 
}
