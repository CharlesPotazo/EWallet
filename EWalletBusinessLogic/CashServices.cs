using System;
using EWalletModels;
using EWalletDataLayer;

namespace EWalletBusinessLogic
{
    public class CashServices
    {
        private SqlDbData sqlDbData = new SqlDbData();

        public void CashIn(string accountNumber, decimal amount)
        {
            var account = sqlDbData.GetUserByAccNum(accountNumber);
            if (account != null)
            {
                account.money = account.money + amount;
                sqlDbData.UpdateMoney(account);
            }
        }

        public bool CashOut(string accountNumber, decimal amount)
        {
            var account = sqlDbData.GetUserByAccNum(accountNumber);
            if (account != null && account.money >= amount)
            {
                account.money = account.money - amount;
                sqlDbData.UpdateMoney(account);
                return true;
            }
            return false;
        }

        public bool TransferMoney(string sender, decimal amount, string receiver)
        {
            var senderAccount = sqlDbData.GetUserByAccNum(sender);
            var receiverAccount = sqlDbData.GetUserByAccNum(receiver);

            if (senderAccount != null && receiverAccount != null && senderAccount.money >= amount)
            {
                senderAccount.money = senderAccount.money - amount;
                receiverAccount.money = receiverAccount.money + amount;

                sqlDbData.UpdateMoney(senderAccount);
                sqlDbData.UpdateMoney(receiverAccount);

                return true;
            }
            return false;
        }
    }
}
