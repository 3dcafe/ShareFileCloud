using CafeExtensions.SimpleModels;

namespace ShareFileCloud.SimpleModels.Auth;

public class LoginSimpleAnswer: SimpleAnswer
{
    /// <summary>
    /// JWT Token for header
    /// </summary>
    public string? JWT { get; set; }
    /// <summary>
    /// Role user
    /// </summary>
    public string? Role { get; set; }
}
