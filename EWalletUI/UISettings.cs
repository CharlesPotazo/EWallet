using EWalletBusinessLogic;
using System;
using System.Linq;

namespace EWalletUI
{
    public class UISettings
    {
        UserServices userService = new UserServices();
        public void Settings(string accountNumber)
        {
            while (true)
            {
                Console.WriteLine("Settings\n Choose a number\n(1.Rename Username) (2.Update Pin) (3. Delete Account) (4.Change Email) (5.Back to Menu)");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter your new Username");
                        string newUserName = Console.ReadLine();

                        RenameUsername(accountNumber, newUserName);
                        break;
                    case 2:
                        Console.WriteLine("Enter New Pin");
                        string newPin = Console.ReadLine();

                        UpdatePassword(accountNumber, newPin);
                        break;
                    case 3:
                        Console.WriteLine("Enter pin");
                        string pin = Console.ReadLine();

                        if (DeleteAccount(accountNumber, pin))
                        {
                            Program.GreetingPage();
                            return;
                        }
                        break;
                    case 4:
                        Console.WriteLine("Enter New Email");
                        string email = Console.ReadLine();
                        ChangeEmail(accountNumber, email);
                        break;
                    case 5:
                        UIMenu menu = new UIMenu();
                        menu.Menu(accountNumber);
                        return;
                    default:
                        Console.WriteLine("Invalid Input.Try again");
                        break;
                }
            }
        }

        public bool RenameUsername(string accountNumber, string newUserName)
        {
            if (DoubleCheck())
            {
                userService.UpdateUsername(accountNumber, newUserName);
                Console.WriteLine($"You have successfully change your User Number to {newUserName}");
                return true;
            }
            else
            {
                Console.WriteLine("Unsucessful");
            }
            return false;
        }

        public bool UpdatePassword(string accountNumber, string newPin)
        {
            if (DoubleCheck() && newPin.Length == 6 && newPin.All(char.IsDigit))
            {
                userService.UpdateUserPassword(accountNumber, newPin);
                Console.WriteLine($"You have successfully change your Pin to {newPin}");
                return true;
            }
            else
            {
                Console.WriteLine("Unsucessful");
            }
            return false;
        }

        public bool DeleteAccount(string accountNumber, string pin)
        {
            var user = userService.GetUserByAccNum(accountNumber);

            if (DoubleCheck() && user.pinNumber == pin)
            {
                if (userService.DeactivateUser(accountNumber, pin))
                {
                    EmailServices emailServices = new EmailServices();
                    emailServices.emailDeleteAccount(user.email);
                    Console.WriteLine($"Thank You for using us. As we meet again!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Withdraw all money first");
                }
            }
            else
            {
                Console.WriteLine("Wrong Password");
            }
            return false;
        }

        public bool ChangeEmail(string accountNumber, string newEmail)
        {
            if (DoubleCheck() && newEmail.Contains("@gmail.com"))
            {
                userService.UpdateEmail(accountNumber, newEmail);
                Console.WriteLine($"Succesfully change email to {newEmail}");
                return true;
            }
            else
            {
                Console.WriteLine("Unsuccesful");
            }
            return false;
        }

        public bool DoubleCheck()
        {
            Console.WriteLine("are you sure?[Yes/No]");
            string answer = Console.ReadLine().Trim().ToUpper();
            if (answer == "YES")
            {
                return true;
            }
            return false;

        }
    }
}
