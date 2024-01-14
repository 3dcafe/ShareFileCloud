namespace Domain.Interfaces;
/// <summary>
/// Base interface user
/// </summary>
public interface IUserDomain
{
    /// <summary>
    /// UserId
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Email user
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Surname user
    /// </summary>
    public string? Surname { get; set; }
    /// <summary>
    /// FirstName
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// Patronymic
    /// </summary>
    public string? Patronymic { get; set; }
    /// <summary>
    /// Job title
    /// </summary>
    public string? Job { get; set; }
    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTimeOffset? DateBirth { get; set; }
}
