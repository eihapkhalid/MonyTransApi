using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bl;

public partial class TbFinancialTransaction
{
    [Key]
    [ValidateNever]
    public int TransactionId { get; set; }

    [Required(ErrorMessage = "Please enter sender account number.")]
    [StringLength(100, ErrorMessage = "Sender account number must be less than 100 characters.")]
    public string SenderAccountNumber { get; set; } = null!;

    [Required(ErrorMessage = "Please enter receiver account number.")]
    [StringLength(100, ErrorMessage = "Receiver account number must be less than 100 characters.")]
    public string ReceiverAccountNumber { get; set; } = null!;

    [Required(ErrorMessage = "Please enter transaction amount.")]
    [Range(0.0000000001, 9999999.999, ErrorMessage = "Transaction amount must be between 0.0000000001 and 9999999.999.")]
    public decimal TransactionAmount { get; set; }

    [Required(ErrorMessage = "Please enter transaction date.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime TransactionDate { get; set; }

    public virtual TbBankAccount ReceiverAccountNumberNavigation { get; set; } = null!;

    public virtual TbBankAccount SenderAccountNumberNavigation { get; set; } = null!;
}
