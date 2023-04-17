using System;
using System.Collections.Generic;

namespace Bl;

public partial class TbBankAccount
{
    public string AccountNumber { get; set; } = null!;

    public int UserId { get; set; }

    public decimal AccountBalance { get; set; }

    public virtual ICollection<TbFinancialTransaction> TbFinancialTransactionReceiverAccountNumberNavigations { get; set; } = new List<TbFinancialTransaction>();

    public virtual ICollection<TbFinancialTransaction> TbFinancialTransactionSenderAccountNumberNavigations { get; set; } = new List<TbFinancialTransaction>();

    public virtual TbUser User { get; set; } = null!;
}
