using System;
using System.Collections.Generic;

namespace Bl;

public partial class VwTransWacountDetail
{
    public string SenderAccountNumber { get; set; } = null!;

    public string ReceiverAccountNumber { get; set; } = null!;

    public decimal TransactionAmount { get; set; }

    public DateTime TransactionDate { get; set; }

    public int SuserId { get; set; }

    public int RuserId { get; set; }

    public int TransactionId { get; set; }

    public string SaccountNumber { get; set; } = null!;

    public string RaccountNumber { get; set; } = null!;

    public decimal SaccountBalance { get; set; }

    public decimal RaccountBalance { get; set; }
}
