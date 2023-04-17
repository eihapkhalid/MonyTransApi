using Bl;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace MoneyTransfer.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UsersController : Controller
    {
        IBusinessLayer<TbUser> oClsUsers;
        public UsersController(IBusinessLayer<TbUser> Users)
        {
            oClsUsers = Users;
        }
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region List of Users
        public IActionResult List()
        {
            var lstUsers = oClsUsers.GetAll();
            return View(lstUsers);
        }
        #endregion

        #region Edit by User id
        public IActionResult Edit(int? userId)
        {
            var contract = new TbUser();

            if (userId != null)
            {
                contract = oClsUsers.GetById(Convert.ToInt32(userId));
            }
            return View(contract);
        }
        #endregion

        #region Save by User object
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbUser user)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", user);
            }
            oClsUsers.Save(user);
            return RedirectToAction("List");
        }
        #endregion

        #region Delete By User Id
        public IActionResult Delete(int userId)
        {
            oClsUsers.Delete(userId);
            return RedirectToAction("List");
        } 
        #endregion
    }
}
