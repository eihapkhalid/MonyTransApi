using System;
using System.Collections.Generic;

namespace Bl;

public partial class TbUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
    public int CurrentState { get; set; }

    public virtual ICollection<TbBankAccount> TbBankAccounts { get; set; } = new List<TbBankAccount>();
}
