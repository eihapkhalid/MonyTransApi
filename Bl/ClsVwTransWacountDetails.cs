using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public class ClsVwTransWacountDetails : IBusinessLayer<VwTransWacountDetail>
    {
        MoneyTransferContext context;
        public ClsVwTransWacountDetails(MoneyTransferContext ctx)
        {
            context = ctx;
        }


        #region Get All Transactions
        public List<VwTransWacountDetail> GetAll()
        {
            try
            {
                var lstTransactions = context.VwTransWacountDetails.ToList();
                return lstTransactions;
            }
            catch
            {
                return new List<VwTransWacountDetail>();
            }
        }
        #endregion

        #region Get Transaction By ID:
        public VwTransWacountDetail GetById(int id)
        {
            try
            {

                var contract = context.VwTransWacountDetails.FirstOrDefault(a => a.TransactionId == id);
                return contract;
            }
            catch
            {
                return new VwTransWacountDetail();
            }
        } 
        #endregion

        #region Hashed Functions
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(VwTransWacountDetail table)
        {
            throw new NotImplementedException();
        }
        public VwTransWacountDetail GetVwById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(VwTransWacountDetail table)
        {
            throw new NotImplementedException();
        }
        public bool Trans(VwTransWacountDetail table)
        {
            throw new NotImplementedException();
        }

        public VwTransWacountDetail AuthorizeUser(VwTransWacountDetail table)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
