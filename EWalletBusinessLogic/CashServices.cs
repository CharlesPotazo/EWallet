using System;
using EWalletModels;
using EWalletDataLayer;

namespace EWalletBusinessLogic
{
    public class CashServices
    {
        private UserData data = new UserData();
        private UserServices userServices = new UserServices();

        public bool CashIn(string accountNumber, decimal amount)
        {

            var account = userServices.GetUserByAccNum(accountNumber);
            if (account != null)
            {
                account.money = account.money + amount;
                data.UpdateMoney(account);
                return true;
            }
            return false;
        }

        public bool CashOut(string accountNumber, decimal amount)
        {
            var account = userServices.GetUserByAccNum(accountNumber);
            if (account != null && account.money >= amount)
            {
                account.money = account.money - amount;
                data.UpdateMoney(account);
                return true;
            }
            return false;
        }

        public bool TransferMoney(string sender, decimal amount, string receiver)
        {
            var senderAccount = userServices.GetUserByAccNum(sender);
            var receiverAccount = userServices.GetUserByAccNum(receiver);

            if (senderAccount != null && receiverAccount != null && senderAccount.money >= amount && sender != receiver)
            {
                senderAccount.money = senderAccount.money - amount;
                receiverAccount.money = receiverAccount.money + amount;

                data.UpdateMoney(senderAccount);
                data.UpdateMoney(receiverAccount);

                return true;
            }
            return false;
        }
    }
}
