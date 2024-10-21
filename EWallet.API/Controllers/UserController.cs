using Microsoft.AspNetCore.Mvc;
using EWalletBusinessLogic;
using EWalletModels;
using EWalletDataLayer;
using System.Data.SqlTypes;
using System.Transactions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;

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
                users.Add(new EWallet.API.User { userName = item.userName, pinNumber = item.pinNumber, accountNumber = item.accountNumber, money = item.money, email = item.email});
            }

            return users;
        }

        [HttpGet("GetUserByAccountNumber")]
        public ActionResult<User> GetUserByAccountNum(string accountNumber)
        {
            var user = userServices.GetUserByAccNum(accountNumber);

            if (user != null)
            {
                return Ok(new User
                {
                    userName = user.userName,
                    pinNumber = user.pinNumber,
                    accountNumber = user.accountNumber,
                    money = user.money,
                    email = user.email
                });
            }

            return NotFound("User not found"); 
        }





        [HttpPost("RegisterUser")]
        public JsonResult AddUser(User User)
        {
            var result = userServices.AddUser(User.accountNumber, User.userName, User.pinNumber, User.email);
            return new JsonResult(result);
        }

        [HttpPost("VerifyUser")]
        public JsonResult VerifyUser(Login user) 
        {
            var result = userServices.verifyUser(user.accountNumber,user.pinNumber);

            return new JsonResult(result);
        
        }



        [HttpPatch("UpdatePassword")]
        public void UpdatePassword(User user)
        {
            userServices.UpdateUserPassword(user.accountNumber, user.pinNumber);
           
        }

        [HttpPatch("RenameUserName")]
        public void RenameUserName(User user)
        {
            userServices.UpdateUsername(user.accountNumber,user.userName);
            
        }

        [HttpDelete("deleteUser")]
        public JsonResult DeleteUser(User user)
        {
            var result = userServices.DeactivateUser(user.accountNumber, user.pinNumber);
            return new JsonResult(result);
        }

        // Cash Service

        [HttpPost("cashIn")]
        public JsonResult CashIn(Transaction request)
        {
            
            var result = cashServices.CashIn(request.accountNumber, request.Amount);
            return new JsonResult(result);
        }

        [HttpPost("cashOut")]
        public JsonResult CashOut(Transaction request)
        {
            var result = cashServices.CashOut(request.accountNumber, request.Amount);
            return new JsonResult(result);
        }

        [HttpPost("Transfer")]
        public JsonResult Transfer(Transfer request)
        {
            var result = cashServices.TransferMoney(request.accountNumber, request.Amount, request.receiver);
            return new JsonResult(result);
        }



    }
}
