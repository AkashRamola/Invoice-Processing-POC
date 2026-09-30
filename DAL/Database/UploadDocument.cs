using System;
using System.Collections.Generic;

namespace UCAIDataBase.DataBase;

public partial class UploadDocument
{
    public int DocId { get; set; }

    public string FileRealName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public DateTime UploadDate { get; set; }

    public bool IsProcessed { get; set; }

    public string? ExtractedInfo { get; set; }
    public string? MatchedResponse { get; set; }

    public bool? IsSafe { get; set; }

    public string? source { get; set; }

}
