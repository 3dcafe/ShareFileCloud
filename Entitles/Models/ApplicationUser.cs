using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CafeExtensions.SimpleModels;
using CafeExtensions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Entitles.Models
{
    /// <summary>
    /// Model Application User on system
    /// </summary>
    public class ApplicationUser : IdentityUser, IBase, IUser
    {
        public ApplicationUser() { }
        /// <summary>
        /// Surname
        /// </summary>
        [StringLength(48)]
        public string? Surname { get; set; }
        /// <summary>
        /// FirstName
        /// </summary>
        [StringLength(40, MinimumLength = 2)]
        public string? FirstName { get; set; }
        /// <summary>
        /// Patronymic
        /// </summary>
        public string? Patronymic { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        public bool? Sex { get; set; }
        /// <summary>
        /// Date Birth
        /// </summary>
        public DateTime? DateBirth { get; set; }
        /// <summary>
        /// Description profile
        /// </summary>
        [StringLength(800)]
        public string? Description { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// Date register
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTimeOffset DateAdd { get; set; } = DateTime.Now;
        /// <summary>
        /// Date update profile or login
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTimeOffset DateUpdate { get; set; } = DateTime.Now;
        /// <summary>
        /// If user delete or blocked ?
        /// </summary>
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// Last login user DateTime
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTimeOffset LastLogin { get; set; } = DateTime.Now;
        /// <summary>
        /// is online user ?
        /// </summary>
        public bool IsOnline { get; set; } = false;
        /// <summary>
        /// Role name
        /// </summary>
        public string? RoleName { get; set; }
        /// <summary>
        /// Reason delete user
        /// </summary>
        public string? ReasonDelete { get; set; }
        /// <summary>
        /// Type system register
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public TypeRegister TypeRegister { get; set; }
        /// <summary>
        /// file id on Avatar
        /// </summary>
        public int? PhotoId { get; set; }
        /// <summary>
        /// Avatar user
        /// </summary>
        [ForeignKey("PhotoId")]
        public virtual AppFile? Photo { get; set; }
        /// <summary>
        /// Provider name
        /// </summary>
        public ProviderName? ProviderName { get; set; }
        #region hiden override fields
        /// <summary>
        /// Is the user blocked or not blocked
        /// </summary>
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }
        /// <summary>
        /// Email
        /// </summary>
        public override string? Email { get => base.Email; set => base.Email = value; }
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override string Id { get => base.Id; set => base.Id = value; }
        /// <summary>
        /// Normalized Email
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        /// <summary>
        /// Normalized UserName
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        /// <summary>
        /// Email Confirmed
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        /// <summary>
        /// Password Hash
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        /// <summary>
        /// Security Stamp
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        /// <summary>
        /// Concurrency Stamp
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        /// <summary>
        /// Телефон подтверждени
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }
        /// <summary>
        /// Двух факторная авториция
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }
        /// <summary>
        /// Access Failed Count
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }
        /// <summary>
        /// Lockout End
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }
        /// <summary>
        /// User Name
        /// </summary>
        [MaxLength(64)]
        public override string? UserName { get => base.UserName; set => base.UserName = value; }
        #endregion
        /// <summary>
        /// Override info for user prepare
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string data = $"{Surname} {FirstName} {Patronymic} {PhoneNumber} {Email}";
            return data;
        }
    }
}