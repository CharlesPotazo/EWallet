using EWalletBusinessLogic;
using System;
using System.Linq;

namespace EWalletUI
{
    public class UIAuthetication
    {
        UserServices userServices = new UserServices();
        EmailServices emailServices = new EmailServices();
        public bool Login(string accountNum, string pin)
        {
            if (userServices.verifyUser(accountNum, pin))
            {
                Console.WriteLine("Welcome");
                return true;

            }
            Console.WriteLine("Incorrect account number or pin. Please try again");
            return false;
        }

        public bool CheckIfEmailExist(string email)
        {
            if (userServices.CheckIfEmailExist(email))
            {
                Console.WriteLine($"Welcome back {email}.\n Your Account is activated again check your Mailtrap to view your credentials.");
                emailServices.emailReactivate(email);
                userServices.ReactivateUser(email);
                return true;
            }
            return false;
        }

        public bool Register(string accountNum, string userName, string pin, string email)
        {
            if (pin.Length == 6 && pin.All(char.IsDigit) && email.Contains("@gmail"))
            {
                userServices.AddUser(accountNum, userName, pin, email);
                emailServices.emailNewUser(userName, email);
                Console.WriteLine($"Welcome to Gcash {userName}");
                return true;
            }
            else
            {
                Console.WriteLine("Check the format of your credential. Pin must be 6 NUMBERS");
            }

            return false;
        }
    }
}

