using Entites.Models.Interfaces;

namespace Entites.Models;
/// <summary>
/// User Files upload
/// </summary>
public sealed class AppFile : IBase
{
    /// <summary>
    /// Id record
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// User Id uploaded file
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public string? UserId { get; init; }
    /// <summary>
    /// Generated file name inserted on link
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// Name file defult upload name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// File size
    /// </summary>
    public long FileSize { get; set; } = 0;
    /// <summary>
    /// File is preview
    /// </summary>
    public bool IsPreview { get; set; } = false;
    /// <summary>
    /// Parent for on source file
    /// </summary>
    public int? ParentId { get; set; }
    /// <summary>
    /// Type uploading file
    /// 1 - Image
    /// 2 - Document
    /// 3 - Archive
    /// 4 - Other
    /// 5 - Video
    /// 6 - Audio
    /// </summary>
    #warning change on enum or out links
    public int FileType { get; set; }
    /// <summary>
    /// Date upload
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public DateTimeOffset DateAdd { get; set; } = DateTime.Now;
    /// <summary>
    /// Date update
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public DateTimeOffset DateUpdate { get; set; } = DateTime.Now;
    /// <summary>
    /// is deleted file ?
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public bool IsDeleted { get; set; } = false;
}