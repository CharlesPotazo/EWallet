using EWalletBusinessLogic;
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
            UIAuthetication uIAuthetication = new UIAuthetication();
            string response = "";
            while (response != "STOP")
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("|Enter Account Number:");
                string AccountNum = Console.ReadLine();

                Console.WriteLine("Enter Pin Number");
                string pin = Console.ReadLine();

                if (uIAuthetication.Login(AccountNum, pin))
                {
                    UIMenu menu = new UIMenu();
                    menu.Menu(AccountNum);
                    break;
                }
                else
                {
                    Console.WriteLine("Restarting process...\nType STOP to stop the process");
                    response = Console.ReadLine().ToUpper();
                }
            }
        }


        public static void Register()
        {
            UIAuthetication uiAuthetication = new UIAuthetication();
            EmailServices emailServices = new EmailServices();
            string response = "";
            while (response != "STOP")
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("|Enter Email:");
                string email = Console.ReadLine().ToLower();

                if (uiAuthetication.CheckIfEmailExist(email))
                {
                    GreetingPage();
                }
                else
                {
                    Console.WriteLine("Enter Username: ");
                    string userName = Console.ReadLine();

                    Console.WriteLine("Enter Pin Number:");
                    string pin = Console.ReadLine();

                    Console.WriteLine("Enter Account Number:");
                    string AccountNum = Console.ReadLine();

                    if (uiAuthetication.Register(AccountNum, userName, pin, email))
                    {
                        GreetingPage();
                        break;

                    }
                    else
                    {
                        Console.WriteLine("Restarting process...\nType STOP to stop the process");
                        response = Console.ReadLine().ToUpper();
                    }
                }
            }
        }

    }
}