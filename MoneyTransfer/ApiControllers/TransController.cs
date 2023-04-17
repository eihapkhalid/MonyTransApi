using Bl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyTransfer.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransController : ControllerBase
    {
        // all  dependency injection definition :
        #region dependency injection region
        IBusinessLayer<TbUser> oClsUsers;
        IBusinessLayer<TbFinancialTransaction> oClsFinancialTransaction;
        IBusinessLayer<VwTransWacountDetail> oClsVwTransWacountDetails;
        public TransController(IBusinessLayer<TbUser> Users, IBusinessLayer<TbFinancialTransaction> financialTransaction, IBusinessLayer<VwTransWacountDetail> vwTransWacountDetails)
        {
            oClsUsers = Users;
            oClsFinancialTransaction = financialTransaction;
            oClsVwTransWacountDetails = vwTransWacountDetails;
        } 
        #endregion

        // all  Get functions :
        #region GET All Users: api/<TransController>/Get
        [HttpGet]
        [Route("Get")]
        public List<TbUser> Get()
        {
            return oClsUsers.GetAll();
        }
        #endregion

        #region GET User By Id: api/<TransController>/Get/5
        [HttpGet("{id}")]
        [Route("Get/{id}")]
        public TbUser Get(int id)
        {
            return oClsUsers.GetById(id);
        }
        #endregion

        #region Get All Trans: api/<TransController>/GetAllTrans
        [HttpGet]
        [Route("GetAllTrans")]
        public List<VwTransWacountDetail> GetAllTrans()
        {
            return oClsVwTransWacountDetails.GetAll();
        }
        #endregion

        #region Get All Trans By Id: api/<TransController>/GetAllTrans/2
        [HttpGet("{id}")]
        [Route("GetTransID/{id}")]
        public VwTransWacountDetail GetTransID(int id)
        {
            return oClsVwTransWacountDetails.GetById(id);
        }
        #endregion

        // all  post functions :
        #region POST New or Edit user: api/<TransController>
        [HttpPost]
        public void Post([FromBody] TbUser user)
        {
            oClsUsers.Save(user);
        }
        #endregion

        #region POST Delte user: api/<TransController>/Delete
        [HttpPost]
        [Route("Delete")]
        public void Delete([FromBody] TbUser user)
        {
            oClsUsers.Delete(user);
        }
        #endregion

        #region POST Create Transction: api/<TransController>/Create
        [HttpPost]
        [Route("Create")]
        public void Create([FromBody] TbFinancialTransaction transaction)
        {
            oClsFinancialTransaction.Trans(transaction);

        }
        #endregion

    }
}
