using Microsoft.AspNetCore.Mvc;
using EWalletBusinessLogic;
using EWalletModels;
using EWalletDataLayer;
using System.Data.SqlTypes;
using System.Transactions;

namespace EWallet.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        CashServices cashServices;
        UserServices userServices;

        public UserController() { 
        cashServices = new CashServices();
         userServices = new UserServices();
        }

        [HttpGet("getAllUsers")] 
        public IEnumerable<EWallet.API.User> GetUsers()
        {
            var activeusers = userServices.GetAllUser();

            List<EWallet.API.User> users = new List<User>();

            foreach (var item in activeusers)
            {
                users.Add(new EWallet.API.User { userName = item.userName, pinNumber = item.pinNumber, accountNumber = item.accountNumber , money = item.money  });
            }

            return users;
        }

        [HttpPost("RegisterUser")] 
        public JsonResult AddUser(User User)
        {
            var result = userServices.RegisterUser(User.accountNumber, User.userName, User.pinNumber);

            return new JsonResult(result);
        }


        [HttpPatch("UpdatePassword")]
        public JsonResult UpdatePassword(User user)
        {
            var result = userServices.UpdateUserPassword(user.accountNumber, user.pinNumber);
            return new JsonResult(result);
        }

        [HttpPatch("RenameUserName")]
        public JsonResult RenameUserName(User user)
        {
            var result = userServices.UpdateUsername(user.accountNumber,user.userName);
            return new JsonResult(result);
        }

        [HttpDelete("deleteUser")]
        public JsonResult DeleteUser(User user)
        {
            var result = userServices.DeleteUser(user.accountNumber, user.pinNumber);
            return new JsonResult(result);
        }

        // Cash Service

        [HttpPatch("cashIn")]
        public JsonResult CashIn(Transaction request)
        {
            var result = cashServices.CashIn(request.accountNumber, request.Amount);
            return new JsonResult(result);
        }

        [HttpPatch("cashOut")]
        public JsonResult CashOut(Transaction request)
        {
            var result = cashServices.CashOut(request.accountNumber, request.Amount);
            return new JsonResult(result);
        }

        [HttpPatch("Transfer")]
        public JsonResult Transfer(Transfer request)
        {
            var result = cashServices.TransferMoney(request.accountNumber, request.Amount, request.receiver);
            return new JsonResult(result);
        }



    }
}
