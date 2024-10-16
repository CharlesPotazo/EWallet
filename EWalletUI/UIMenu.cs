using EWalletBusinessLogic;
using System;

namespace EWalletUI
{
    public class UIMenu
    {
        EmailServices emailService = new EmailServices();
        UserServices userService = new UserServices();
        CashServices cashService = new CashServices();

        public void Menu(string accountNumber)
        {
            while (true)
            {
                var user = userService.GetUserByAccNum(accountNumber);
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("|                   Welcome to (C)-cash              |");
                Console.WriteLine("|                 *endorser BINI PICTURE*            |");
                Console.WriteLine("|             *with Salamin,Salamin BG music*        |");
                Console.WriteLine($"|\n|Welcome: {user.userName}, Balance: {user.money}");
                Console.Write("|\n| Choose a number:                                   |\n|(1.Cash In) (2.Cash Out) (3.Transfer Money) (4.Settings) (5.Exit)                                        |\n");

                int choice = Convert.ToInt16(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Cash in \nEnter amount:");
                        int cashInAmount = Convert.ToInt32(Console.ReadLine());
                        Cashin(accountNumber, cashInAmount);
                        break;
                    case 2:
                        Console.WriteLine("Cash out \nEnter amount:");
                        int cashOutAmount = Convert.ToInt32(Console.ReadLine());
                        Cashout(accountNumber, cashOutAmount);
                        break;
                    case 3:
                        Console.WriteLine("Transfer Money \nEnter amount:");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the Account Number of Receiver:");
                        string receiverAccNum = Console.ReadLine();

                        var receiver = userService.GetUserByAccNum(receiverAccNum);
                        if (receiver != null)
                        {
                            Transfer(accountNumber, receiverAccNum, amount, receiver.userName, receiver.email);
                        }
                        else
                        {
                            Console.WriteLine("Receiver account number not found.");
                        }
                        break;
                    case 4:
                        UISettings settings = new UISettings();
                        settings.Settings(accountNumber);
                        break;
                    case 5:
                        Console.WriteLine("Log out");
                        Program.GreetingPage();
                        return;

                }



            }

        }

        public bool Cashin(string accountNumber, int amount)
        {
            var user = userService.GetUserByAccNum(accountNumber);
            if (cashService.CashIn(accountNumber, amount) && emailService.emailCashin(accountNumber, user.email, amount, "(c)-cash system"))
            {
                Console.WriteLine("Successful");
                Console.WriteLine("Email sent successfully through Mailtrap.");
                return true;
            }
            else
            {
                Console.WriteLine("Unsuccessful");
            }
            return false;
        }

        public void Cashout(string accountNumber, int amount)
        {
            var user = userService.GetUserByAccNum(accountNumber);
            if (cashService.CashOut(accountNumber, amount) && emailService.emailCashout(user.userName, accountNumber, user.email, amount))
            {
                Console.WriteLine($"You successfully withdrawed {amount} to your account");
                Console.WriteLine("Email sent successfully through Mailtrap.");
            }
            else
            {
                Console.WriteLine("The amount you are trying to cash out is not valid");
            }
        }

        public bool Transfer(string Sender, string receiver, int amount, string receiverUsername, string receiverEmail)
        {
            var user = userService.GetUserByAccNum(Sender);
            if (cashService.TransferMoney(Sender, amount, receiver) && emailService.emailTransferMoney(user.userName, Sender, user.email, receiverUsername, receiver, receiverEmail, amount))
            {
                Console.Write($"Succefully Transferred {amount} to {receiverUsername}");
                Console.WriteLine("Email sent successfully through Mailtrap.");
                return true;
            }
            else
            {
                Console.WriteLine("Transfer failed. Either Account Number is not valid or your balance is not enough\n");
            }
            return false;

        }
    }
}
