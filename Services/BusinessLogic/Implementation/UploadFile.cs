using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.Identity;
using DTO.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IConfiguration _config;
        private readonly ILogger<UploadFile> _logger;
        private readonly DefaultAzureCredential defaultAzureCredential;
        public UploadFile(IConfiguration config, ILogger<UploadFile> logger)
        {
            _logger = logger;
            _config = config;
            var managedIdentityClientId = config["ManagedIdentity:ClientId"];
            defaultAzureCredential = new DefaultAzureCredential(
              new DefaultAzureCredentialOptions
              {
                  ManagedIdentityClientId = managedIdentityClientId
              }
            );
        }
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
                    
                }

                // Create upload folder if it doesn't exist
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                string filerealname = Path.GetFileNameWithoutExtension(pdfFile.FileName);
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
                result.uniqueFileName = filerealname;
                return result;
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
                
            }
        }
        public async Task<string> UploadedFileProcess(UploadFileStatusDTO data)
        {
            try
            {
                var client = new DocumentAnalysisClient(
                    new Uri(_config["AzureAICredentials:FormRecognizerEndpoint"]),
                    defaultAzureCredential
                );
                if (data == null || string.IsNullOrEmpty(data.uniqueFileName))
                {
                    data.Success = false;
                    data.Message = "File name is missing.";
                    return data.ToString();
                }

                // Get uploads folder
                string uploadFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads"
                );

                // Get full PDF path
                string filePath = Path.Combine(
                    uploadFolder,
                    data.uniqueFileName
                );

                // Check file exists
                if (!File.Exists(filePath))
                {
                    data.Success = false;
                    data.Message = "PDF file not found.";
                    return data.ToString();
                }

                // Open PDF
                using FileStream stream = new FileStream(
                    filePath,
                    FileMode.Open,
                    FileAccess.Read
                );

                // Send PDF to Azure Document Intelligence
                AnalyzeDocumentOperation operation =
                    await client.AnalyzeDocumentAsync(
                        WaitUntil.Completed,
                        "prebuilt-layout",
                        stream
                    );

                AnalyzeResult result = operation.Value;

                // Extract text
                StringBuilder extractedText = new StringBuilder();

                foreach (DocumentPage page in result.Pages)
                {
                    foreach (DocumentLine line in page.Lines)
                    {
                        extractedText.AppendLine(line.Content);
                    }
                }

                // You can use this text later
                return extractedText.ToString();
                
            }
            catch (Exception ex)
            {
                data.Success = false;
                data.Message = ex.Message;

                return data.ToString();
            }
        }
    }
}
