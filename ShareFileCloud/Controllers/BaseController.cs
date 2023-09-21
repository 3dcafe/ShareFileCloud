using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Base class for web api controllers
/// workspace with userId
/// </summary>
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Identity user
    /// </summary>
    protected string? UserId => User.Claims.Any() ? User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value : null;
}