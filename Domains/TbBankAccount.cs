using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bl;

public partial class TbBankAccount
{
    [Key]
    [ValidateNever]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Please enter an account number.")]
    [StringLength(100, ErrorMessage = "Account number must be less than 100 characters.")]
    public string AccountNumber { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an account balance.")]
    [Range(0.0000000001, 9999999.999, ErrorMessage = "Account balance must be between 0.0000000001 and 9999999.999.")]
    public decimal AccountBalance { get; set; }

    public virtual ICollection<TbFinancialTransaction> TbFinancialTransactionReceiverAccountNumberNavigations { get; set; } = new List<TbFinancialTransaction>();

    public virtual ICollection<TbFinancialTransaction> TbFinancialTransactionSenderAccountNumberNavigations { get; set; } = new List<TbFinancialTransaction>();

    public virtual TbUser User { get; set; } = null!;
}
