using System;
using System.Collections.Generic;

namespace UCAIDataBase.DataBase;

public partial class UploadDocument
{
    public int DocId { get; set; }

    public string FileRealName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public DateTime UploadDate { get; set; }

    public string BlobUrl { get; set; } = null!;

    public bool IsProcessed { get; set; }

    public string? ExtractedInfo { get; set; }
    public string? MatchedResponse { get; set; }

    public bool? IsSafe { get; set; }

    public string? WhyNotUploaded { get; set; }

    public decimal? HateSeverity { get; set; }

    public decimal? SelfHarmSeverity { get; set; }

    public decimal? SexualSeverity { get; set; }

    public decimal? ViolenceSeverity { get; set; }

    public bool? isMatched { get; set; }
    public bool IsMatchCompleted { get; set; } = false;
    public bool? MatchingStatus { get; set; } = false;

    public string? source { get; set; }

}
