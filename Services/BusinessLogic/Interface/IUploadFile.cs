using DTO.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessLogic.Interface
{
    public interface IUploadFile
    {
        public Task<UploadFileStatusDTO> Uploadpdf(IFormFile pdfFile);
        public Task<string> UploadedFileProcess(UploadFileStatusDTO data);
    }
}
