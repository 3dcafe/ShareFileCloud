namespace Entites.Models.Interfaces;

/// <summary>
/// Interface for all models
/// </summary>
public interface IBase
{
    /// <summary>
    /// Дата добавления
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public DateTimeOffset DateAdd { get; set; }
    /// <summary>
    /// Дата добавления
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public DateTimeOffset DateUpdate { get; set; }
    /// <summary>
    /// Удалена ли запись
    /// </summary>
    public bool IsDeleted { get; set; }
}
