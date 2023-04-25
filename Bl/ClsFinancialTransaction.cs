using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public class ClsFinancialTransaction : IBusinessLayer<TbFinancialTransaction>
    {
        MoneyTransferContext context;
        public ClsFinancialTransaction(MoneyTransferContext ctx)
        {
            context = ctx;
        }

        #region Get Transaction By ID:
        public TbFinancialTransaction GetById(int id)
        {
            try
            {

                var contract = context.TbFinancialTransactions.FirstOrDefault(a => a.TransactionId == id);
                return contract;
            }
            catch
            {
                return new TbFinancialTransaction();
            }
        }
        #endregion

        #region Transaction Function
        public bool Trans(TbFinancialTransaction transaction)
        {
            try
            {
                // Verify that there is enough credit in the sender's account
                var senderAccount = context.TbBankAccounts.FirstOrDefault(a => a.AccountNumber == transaction.SenderAccountNumber);

                if (senderAccount == null || senderAccount.AccountBalance < transaction.TransactionAmount)
                {
                    return false;
                }

                // Update the balance of the sending account and the receiving account
                senderAccount.AccountBalance -= transaction.TransactionAmount;

                var receiverAccount = context.TbBankAccounts.FirstOrDefault(a => a.AccountNumber == transaction.ReceiverAccountNumber);
                if (receiverAccount != null)
                {
                    receiverAccount.AccountBalance += transaction.TransactionAmount;
                }

                // Update the foreign key references in the transaction object
                transaction.SenderAccountNumberNavigation = senderAccount;
                transaction.ReceiverAccountNumberNavigation = receiverAccount;

                // Add the transaction object to the FinancialTransactions table
                context.TbFinancialTransactions.Add(transaction);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        } 
        #endregion

        #region Save function
        public bool Save(TbUser user)
        {
            try
            {
                if (user.UserId == 0)
                {
                    user.CurrentState = 1;
                    context.TbUsers.Add(user);
                }
                else
                {
                    user.CurrentState = 1;
                    context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        } 
        #endregion

        #region Hashed Functions
        TbFinancialTransaction IBusinessLayer<TbFinancialTransaction>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(TbFinancialTransaction table)
        {
            throw new NotImplementedException();
        }
        public bool Delete(TbFinancialTransaction transaction)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<TbFinancialTransaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public TbFinancialTransaction AuthorizeUser(TbFinancialTransaction table)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
