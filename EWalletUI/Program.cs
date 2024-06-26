using EWalletBusinessLogic;
using EWalletDataLayer;
using EWalletModels;
using System;
using System.Security.Principal;

namespace EWalletUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            GreetingPage();
        }

        public static void GreetingPage() {
            Console.WriteLine("------------Welcome to (C)-cash----------");
            Console.WriteLine("Choose a number: \n1.Login\n2.Register Account");
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

        public static void Login() {
            UserServices loginService = new UserServices();

            Console.WriteLine("Enter Account num: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Pin Num: ");
            string pinNumber = Console.ReadLine();

            if(loginService.verifyUser(accountNumber, pinNumber))
            {
                Console.WriteLine("Success");
                Menu(accountNumber);
            }
            else
            {
                Console.WriteLine("Error! Do you want to Retry?[Yes/No]");
                string answer =Console.ReadLine();

                if (answer == "Yes")
                {
                    Login();
                }
                else {
                    GreetingPage();
                }
            }
        }

        public static void Menu(int accountNumber) {

            while (true)
            {
                UserServices userService = new UserServices();
                CashServices cashService = new CashServices();
                var user = userService.GetUserByAccNum(accountNumber);

                Console.WriteLine("----------(C)-cash----------");
                Console.WriteLine($"Welcome: {user.userName}, Balance: {user.money}");
                Console.Write("Choose a number: \n1. Cash In\n2. Cash Out\n3. Exit");
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
                        cashService.CashOut(accountNumber, CashOutAmount);
                        Console.WriteLine("Succefully");
                        break;
                    case 3:
                        Login(); break;
                    default:
                        break;

                }
            } 
        
        }

        public static void Register() {
            Console.WriteLine("Welcome to the E-Wallet system!");

            Console.Write("Enter your username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter your 6 Digit PIN: ");
            string pinNumber = Console.ReadLine();

            Console.Write("Enter your account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            UserServices userService = new UserServices();
            userService.RegisterUser(accountNumber, userName,pinNumber);

            Console.WriteLine("User registered successfully!");


        }

    }
}
