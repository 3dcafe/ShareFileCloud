using Domain.Interfaces;
using System.Text.Json.Serialization;

namespace DTO.Models;
public class ApplicationUserDTO : IUserDomain
{
    /// <summary>
    /// Id
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Email user
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Должность
    /// </summary>
    public string? Job { get; set; }
    /// <summary>
    /// Отдел
    /// </summary>
    public string? Department { get; set; }
    /// <summary>
    /// Number
    /// </summary>
    public int? Number { get; set; }
    /// <summary>
    /// username пользователя
    /// </summary>
    public string? UserName { get; set; }
    public DateTimeOffset? DateBirth { get; set; }
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? RoleName { get; set; }
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? Surname { get; set; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }
    /// <summary>
    /// User's avatar or color
    /// </summary>
    public string? Avatar { get; set; }
    /// <summary>
    /// For OneSignal
    /// </summary>
    [JsonIgnore]
    public string? PushChannel { get; set; }
    public string? PhoneNumber { get; set; }
    [JsonIgnore]
    public bool IsDeleted { get; set; }
    [JsonIgnore]
    public string? UserCode { get; set; }
    /// <summary>
    /// Is user lock on administration
    /// </summary>
    [JsonIgnore]
    public bool LockoutEnabled { get; set; } = false;
    /// <summary>
    /// PasswordHash user
    /// </summary>
    [JsonIgnore]
    public string? PasswordHash { get; set; }
}