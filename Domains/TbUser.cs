using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bl;

public partial class TbUser
{
    [Key]
    [ValidateNever]
    public int UserId { get; set; }

    [Required(ErrorMessage = "plz enter employee name")]
    [StringLength(100, ErrorMessage = "name must be less than 100")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a password.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "plz enter employee Email")]
    [StringLength(100, ErrorMessage = "Email must be less than 100")]
    public string Email { get; set; } = null!;

    [ValidateNever]
    public int CurrentState { get; set; }

    public virtual ICollection<TbBankAccount> TbBankAccounts { get; set; } = new List<TbBankAccount>();
}
