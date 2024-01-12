using CafeExtensions.Exceptions;
using CafeExtensions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ShareFileCloud.Controllers;
/// <summary>
/// Main file upload controller
/// </summary>
[ApiController]
public class AppFilesController : BaseController
{
    /*
    /// <summary>
    /// Database layer
    /// </summary>
    readonly ApplicationContext _db;
    /// <summary>
    /// Basic Constructor
    /// </summary>
    /// <param name="db"></param>
    public AppFilesController
        (
            ApplicationContext db
        )
    {
        _db = db;
    }
    /// <summary>
    /// Acceptable file upload formats
    /// </summary>
    private readonly string[] _validExtensions = new string[] { ".png", ".jpg", ".gif", ".jpeg", ".mp4", ".mov", ".xlsx", ".wav", ".pdf", ".doc", ".docx", ".odt", ".xls", ".xlsx", ".ppt", ".pptx", ".apk", ".zip" };
    /// <summary>
    /// Method upload file
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("api/appfiles")]
    [Authorize]
    [RequestSizeLimit(int.MaxValue)]
    public async Task<ActionResult<IFormFile>> Post(IFormFile[] uploadedFiles)
    {
        if (UserId == null) return Unauthorized();
        List<AppFile> appFiles = new List<AppFile>();
        if (uploadedFiles.Length <= 0) return Ok(appFiles);
        foreach (var item in uploadedFiles)
        {
            var extension = Path.GetExtension(item.FileName).ToLower();

            if (_validExtensions.Contains(extension) == false)
                return BadRequest("INVALID_EXTENSIONS");
            if (_db == null)
                throw new AccessErrorException("Error - connect to database");
            if (_db.Users == null)
                throw new AccessErrorException("Error - table Users not found");
            if (_db.AppFiles == null)
                throw new AccessErrorException("Error - table AppFiles not found");

            var newNameFile = Guid.NewGuid();
            var fileType = 1;
            switch (extension)
            {
                case ".xlsx":
                    fileType = 2;
                    break;
                case ".mp4":
                case ".mov":
                    fileType = 5;
                    break;
            }

            const string folderUpload = "/var/www/cdn.qird.ru/upload";
            if (!Directory.Exists(folderUpload))
                Directory.CreateDirectory(folderUpload);

            var filePath = Path.Combine(folderUpload, newNameFile.ToString() + extension);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await item.CopyToAsync(fileStream);
            }
            var user = _db.Users.Where(x => x.Id == UserId).FirstOrDefault();
            if (user == null)
                throw new AccessErrorException("Error - user not found");

            var appFile = new AppFile()
            {
                FileName = newNameFile + extension,
                FileSize = item.Length,
                Name = item.FileName.Replace(extension, string.Empty),
                FileType = fileType,
                UserId = user.Id
            };
            await _db.AppFiles.AddAsync(appFile);
            await _db.SaveChangesAsync();
            appFiles.Add(appFile);
        }
        return Ok(appFiles);
    }

    
    private async Task<AppFile?> UploadFileAsync(string? fileUrl)
    {
        string TOKENUSER = this.HttpContext.Request.Headers["authorization"];
        #region Photo sync
        if (fileUrl != null && fileUrl.Length != 0)
        {
            var clientRepository = new HttpSimpleClientRepository();
            var fileData = await clientRepository.GetFile(fileUrl);
            if (fileData != null)
            {
                if (fileData != null && fileData.Length > 0)
                {
                    var uri = new Uri(fileUrl);
                    clientRepository.SetHeaders
                    (
                        new Dictionary<string, string>()
                        {
                                {"Authorization", TOKENUSER}
                        }
                    );
                    var dataReadRest = await clientRepository.FileUplopadAsync<AppFile[]>("https://cdn.qird.ru/api/appfiles", "1.png", fileData);
                    if (dataReadRest != null && dataReadRest.StatusCode == 200 && dataReadRest.Response != null && dataReadRest.Response.Length > 0)
                    {
                        return dataReadRest.Response?.FirstOrDefault();
                    }
                }
            }

        }
        #endregion
        return null;
    }
    */
}