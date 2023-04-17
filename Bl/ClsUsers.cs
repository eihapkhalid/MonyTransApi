using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public class ClsUsers : IBusinessLayer<TbUser>
    {
        MoneyTransferContext context;
        public ClsUsers(MoneyTransferContext ctx)
        {
            context = ctx;
        }

        #region AuthorizeUser function:
       /* public TbUser AuthorizeUser(TbUser table)
        {
            TbUser data = new TbUser();
            if (table.Username == data.Username && table.Password == data.Password)
            {
                return new TbUser()
                {
                    Username = data.Username,
                    Email = data.Email
                };
            }
            return null;*/
            #region Another way for test :
             public TbUser AuthorizeUser(TbUser table)
                 {
                   // Perform user authorization logic based on your business requirements
                   // For example, query the database to verify username and password
                   TbUser user = context.TbUsers.FirstOrDefault(u => u.Username == table.Username && u.Password == table.Password);

                   if (user != null)
                   {
                       return new TbUser()
                       {
                           Username = user.Username,
                           Email = user.Email
                       };
                   }

                   return null;
                 }
            #endregion
       // } 
        #endregion

        #region Delete user Function By Id
        public bool Delete(int id)
        {
            try
            {

                var user = GetById(id);
                user.CurrentState = 0;
                context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Delete user Function By object
        public bool Delete(TbUser user)
        {
            try
            {

                var newuser = GetById(user.UserId);
                newuser.CurrentState = 0;
                context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Get All User Data
        public List<TbUser> GetAll()
        {
            try
            {
                var lstUsers = context.TbUsers.Where(a => a.CurrentState == 1).ToList();
                return lstUsers;
            }
            catch
            {
                return new List<TbUser>();
            }

        }
        #endregion

        #region Get user Function By Id
        public TbUser GetById(int id)
        {
            try
            {

                var contract = context.TbUsers.Where(a => a.UserId == id && a.CurrentState == 1).FirstOrDefault();
                return contract;
            }
            catch
            {
                return new TbUser();
            }
        }
        #endregion

        #region Save or Edit user Data
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
        public void Trans(TbUser table)
        {
            throw new NotImplementedException();
        }

        bool IBusinessLayer<TbUser>.Trans(TbUser table)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
