using System;
using System.Collections.Generic;

namespace Bl;

public partial class TbFinancialTransaction
{
    public int TransactionId { get; set; }

    public string SenderAccountNumber { get; set; } = null!;

    public string ReceiverAccountNumber { get; set; } = null!;

    public decimal TransactionAmount { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual TbBankAccount ReceiverAccountNumberNavigation { get; set; } = null!;

    public virtual TbBankAccount SenderAccountNumberNavigation { get; set; } = null!;
}
