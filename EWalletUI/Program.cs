using EWalletBusinessLogic;
using EWalletModels;
using System;
using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EWalletUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            GreetingPage();
        }

        public static void GreetingPage()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("|           Welcome to (C)-cash          |");
            Console.WriteLine("|         *endorser BINI PICTURE*        |");
            Console.WriteLine("|     *with Salamin,Salamin BG music*    |");
            Console.WriteLine("|Choose a number:                        |");
            Console.WriteLine("|1.Login                                 |");
            Console.WriteLine("|2.Register                              |");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Register();
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        GreetingPage();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("That is not a number!");
                GreetingPage();
            }
        }

        public static void Login()
        {
            UserServices loginService = new UserServices();

            try
            {
                Console.WriteLine("Enter Account Number: ");
                int accountNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Pin Num: ");
                string pinNumber = Console.ReadLine();

                if (loginService.verifyUser(accountNumber, pinNumber))
                {
                    Console.WriteLine("Success");
                    Menu(accountNumber);
                }
                else
                {
                    Console.WriteLine("Either the account number or pin number is Wrong! Do you want to Retry?[Yes/No]");
                    string answer = Console.ReadLine().Trim().ToUpper();
                    if (answer == "YES")
                    {
                        Login();
                    }
                    else
                    {
                        GreetingPage();
                    }
                }
            }
            catch { }
            Console.WriteLine("The inputted Value is in wrong format ");
            Login();
        }

        public static void Menu(int accountNumber)
        {

            while (true)
            {
                UserServices userService = new UserServices();
                CashServices cashService = new CashServices();
                var user = userService.GetUserByAccNum(accountNumber);


                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("|                   Welcome to (C)-cash              |");
                Console.WriteLine("|                 *endorser BINI PICTURE*            |");
                Console.WriteLine("|             *with Salamin,Salamin BG music*        |");
                Console.WriteLine($"|\n|Welcome: {user.userName}, Balance: {user.money}");
                Console.Write("|\n| Choose a number:                                   |\n|(1.Cash In) (2.Cash Out) (3.Transfer Money) (4.Exit)|\n|(5.settings)                                        |\n");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Write("CASH-IN\nEnter amount: ");
                            int CashInAmount = Convert.ToInt32(Console.ReadLine());
                            cashService.CashIn(accountNumber, CashInAmount);
                            Console.WriteLine("Successful!!!!");
                            break;
                        case 2:
                            Console.Write("Enter amount to withdraw: ");
                            int CashOutAmount = Convert.ToInt32(Console.ReadLine());
                            bool result = cashService.CashOut(accountNumber, CashOutAmount);
                            if (result)
                            {
                                Console.WriteLine($"You successfully withdrawed {CashOutAmount} to your account");
                            }
                            else
                            {
                                Console.WriteLine("The amount you are trying to cash out is not valid");
                            }
                            break;
                        case 3:
                            Console.Write("Send to:(input account number) ");
                            int TransferTo = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Amount: ");
                            int amount = Convert.ToInt32(Console.ReadLine());

                            cashService.transferMoney( TransferTo, amount);
                            deductMoney(user, amount);
                            Console.Write($"Succefully Transferred {amount} to {TransferTo} ");
                            break;
                        case 4:
                            GreetingPage();
                            break;
                        case 5:

                            while (true)
                            {
                                Console.WriteLine("Settings\n Choose a number\n(1.Rename Username) (2.Update Password) (3. Delete Account) (4.Back to Menu)");
                                int option = Convert.ToInt32(Console.ReadLine());

                                if (option == 1)
                                {
                                    Console.WriteLine("What is your new username Number?");
                                    string newUserName = Console.ReadLine();

                                    Console.WriteLine("are you sure?[Yes/No]");
                                    string answer = Console.ReadLine().Trim().ToUpper();
                                    if (answer == "YES")
                                    {
                                        userService.UpdateUsername(accountNumber, newUserName);
                                        Console.WriteLine($"You have successfully change your User Number to {newUserName}");
                                    }
                                }
                                else if (option == 2)
                                {
                                    Console.WriteLine("What is your new Pin Number?");
                                    string newPinNumber = Console.ReadLine();

                                    Console.WriteLine("are you sure?[Yes/No]");
                                    string answer = Console.ReadLine().Trim().ToUpper();
                                    if (answer == "YES")
                                    {
                                        userService.UpdateUserPassword(accountNumber, newPinNumber);
                                        Console.WriteLine($"You have successfully change your Pin Number to {newPinNumber}");
                                    }
                                }
                                else if (option == 3)
                                {
                                    Console.WriteLine("Are you sure?:[Yes/No]");
                                    string answer = Console.ReadLine().ToLower().Trim(); ;

                                    if (answer == "yes")
                                    {
                                        Console.WriteLine("Enter Pin Number:");
                                        string PinNumber = Console.ReadLine();
                                        if (PinNumber == user.pinNumber)
                                        {Console.WriteLine("We hope we give you the best service! Until we meet again c-casher HAHA");
                                             GreetingPage();
                                             userService.DeleteUser(accountNumber); //Itu naman po di ko mapaga huhuh 
                                             
     
                                        }else
                                            {
                                                Console.WriteLine("Withdraw all the money first");
                                            }
                                    }
                                }
                                else if (option == 4)
                                {
                                    break;
                                }
                            }


                            break;
                            }
                }
                catch
                {
                    Console.WriteLine("Invalid inputted format");

                }
            }

        }

        public static void Register()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("|                Register                |");
            Console.Write("|Enter your username:  ");
            string userName = Console.ReadLine();

            Console.Write("|Enter your 6 Digit PIN:  ");
            string pinNumber = Console.ReadLine();

            Console.Write("|Enter your account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            UserServices userService = new UserServices();
            userService.RegisterUser(accountNumber, userName, pinNumber);

            Console.WriteLine("You are now Registered! " + userName);


        }



        private static void deductMoney(User user, int amount) //miss ito pa lang po yung way na napagana ko feel ko po sa mga constructors yung mali ... FIFIGURE OUT KO PA PO HUHU
        {
            CashServices cashServices = new CashServices();
            cashServices.CashOut(user.accountNumber, amount);
        }
    }
}