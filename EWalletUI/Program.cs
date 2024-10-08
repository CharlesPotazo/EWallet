﻿using EWalletBusinessLogic;
using System;

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
            while (true) 
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("|           Welcome to (C)-cash          |");
                Console.WriteLine("|         *endorser BINI PICTURE*        |");
                Console.WriteLine("|     *with Salamin, Salamin BG music*    |");
                Console.WriteLine("|Choose a number:                        |");
                Console.WriteLine("|1. Login                                 |");
                Console.WriteLine("|2. Register                              |");

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
                            Console.WriteLine("Invalid choice. Please try again.");
                            break; 
                    }
                }
                catch (FormatException) 
                {
                    Console.WriteLine("That is not a number! Please try again.");
                }
            }
        }

        public static void Login()
        {
            UserServices loginService = new UserServices();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Account Number: ");
                    string accountNumber = Console.ReadLine();

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
                        if (answer != "YES")
                        {
                            GreetingPage();
                            break;
                        }
                    }
                }
                catch { 
                Console.WriteLine("The inputted Value is in wrong format ");
                Login();
                }
            }
        }


        public static void Menu(string accountNumber)
        {

            while (true)
            {
                EmailServices emailService = new EmailServices();
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
                            if (cashService.CashIn(accountNumber, CashInAmount) && emailService.emailCashin(user.userName, user.email, CashInAmount, " (C)-Cash System"))
                            {
                                Console.WriteLine("Successful");
                                Console.WriteLine("Email sent successfully through Mailtrap.");
                            }
                            else {
                                Console.WriteLine("Unsuccessful");
                            }
                            break;
                        case 2:
                            Console.Write("Enter amount to withdraw: ");
                            int CashOutAmount = Convert.ToInt32(Console.ReadLine());
                            if (cashService.CashOut(accountNumber, CashOutAmount) && emailService.emailCashout(user.userName, accountNumber, user.email, CashOutAmount))
                            {
                                Console.WriteLine($"You successfully withdrawed {CashOutAmount} to your account");
                                Console.WriteLine("Email sent successfully through Mailtrap.");
                            }
                            else
                            {
                                Console.WriteLine("The amount you are trying to cash out is not valid");
                            }
                            break;
                        case 3:
                            Console.Write("Send to(input account number): ");
                            string receiverAccNum = Console.ReadLine();
                            Console.Write("Amount: ");
                            int amount = Convert.ToInt32(Console.ReadLine());
                            var receiver = userService.GetUserByAccNum(receiverAccNum);
                        
                            if (cashService.TransferMoney(accountNumber, amount, receiverAccNum) &&
                                emailService.emailTransferMoney(user.userName, accountNumber, user.email, receiver.userName, receiverAccNum, receiver.email, amount))
                            {
                                Console.Write($"Succefully Transferred {amount} to {receiverAccNum} ");
                                Console.WriteLine("Email sent successfully through Mailtrap.");
                            }
                            else
                            {
                                Console.WriteLine("Transfer failed. Either Account Number is not valid or your balance is not enough\n");
                            }

                            break;
                        case 4:
                            GreetingPage();
                            break;
                        case 5:
                            Settings(accountNumber);
                            break;
                            }
                }
                catch
                {
                    Console.WriteLine("Invalid inputted format");

                }
            }

        }

        public static void Settings(string accountNumber) {

            UserServices userService = new UserServices();
            while (true)
            {
                Console.WriteLine("Settings\n Choose a number\n(1.Rename Username) (2.Update Password) (3. Delete Account) (4.Back to Menu)");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    Console.WriteLine("What is your new username?");
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
                        string pinNumber = Console.ReadLine();

                        if (userService.DeleteUser(accountNumber, pinNumber))
                        {
                            Console.WriteLine("User Deleted Successfully! Bye Bye Until we meet again ka c-casher");
                            GreetingPage();
                        }
                        else
                        {
                            Console.WriteLine("Pin Number incorrect or  you need to withdraw all the money first.");
                        }
                    }
                }
                else if (option == 4)
                {
                    break;
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
            string accountNumber = Console.ReadLine();

            Console.Write("|Enter your email: ");
            string email = Console.ReadLine();

            UserServices userService = new UserServices();

            EmailServices emailServices = new EmailServices();

            if (userService.RegisterUser(accountNumber, userName, pinNumber, email) && emailServices.emailNewUser(userName, email) )
            {
                Console.WriteLine("You are now Registered! " + userName);
                Console.WriteLine("Email sent successfully through Mailtrap.");
            }
            else {
                Console.WriteLine("Either AccountNumber is Taken or Invalid Inputs ");
            }

            

            GreetingPage();


        }
    }
}