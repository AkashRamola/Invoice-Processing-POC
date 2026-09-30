using System;
using System.Collections.Generic;

namespace DTO.Data
{
    public partial class UploadDocumentDTO
    {

        public string FileRealName { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public DateTime UploadDate { get; set; }

        public bool IsProcessed { get; set; }

        public string? ExtractedInfo { get; set; }

        public string? Source { get; set; }

    }
}
