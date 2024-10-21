using EWalletModels;
using EWalletDataLayer;
using System.Collections.Generic;

namespace EWalletBusinessLogic
{
    public class UserServices
    {
        UserData data;

        public UserServices()
        {
            data = new UserData();
        }

        public List<User> GetAllUser()
        {
            return data.GetAllUser();
        }

        public List<User> GetAllActiveStatus()
        {

            List<User> allUsers = GetAllUser();
            List<User> activeUser = new List<User>();

            foreach (User user in allUsers)
            {
                if (user.status == true)
                {
                    activeUser.Add(user);
                }
            }
            return activeUser;
        }

        public bool CheckIfEmailExist(string email)
        {
            return GetUserByEmail(email) != null;
        }

        public User GetUserByAccNum(string accountNumber)
        {
            List<User> activeUser = GetAllActiveStatus();

            foreach (User user in activeUser)
            {
                if (user.accountNumber == accountNumber)
                {
                    return user;
                }
            }
            return null;
        }

        public User GetUserByEmail(string email)
        {
            List<User> allUser = GetAllUser();

            foreach (User user in allUser)
            {
                if (user.email == email)
                {
                    return user;
                }

            }
            return null;
        }

        public bool verifyUser(string accountNumber, string pinNumber)
        {
            bool result = new bool();
            foreach (var users in GetAllActiveStatus())
            {
                if (users.accountNumber == accountNumber && users.pinNumber == pinNumber)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        //For now reserve i dont see the use pa
        //public bool IsActive(string accountNumber) {
        //    User user = GetUserByAccNumb(accountNumber);
        //    if (user != null && user.status != false)
        //    {
        //        return true;
        //    }
        //    return false;

        //}
        public bool AddUser(string accountNumber, string userName, string pinNumber, string email)
        {
            bool result = false;
            User userEmail = GetUserByEmail(email) ;
            User userAccountNum = GetUserByAccNum(accountNumber);
            if (userEmail == null && userAccountNum == null)
            {
                data.AddUser(accountNumber, userName, pinNumber, email);
                return true;
            }
            return false;
        }
        public bool ReactivateUser(string email)
        {
            User user = GetUserByEmail(email);
            if (user != null && user.status != true)
            {
                data.ActivateUser(user.email);
                return true;
            }
            return false;
        }

        public bool DeactivateUser(string accountNumber, string pinNumber)
        {
            User user = GetUserByAccNum(accountNumber);
            if (user != null && user.status && user.pinNumber == pinNumber && user.money <= 0)
            {
                data.DeactivateUser(accountNumber);
                return true;
            }
            return false;
        }

        public void UpdateUserPassword(string accountNumber, string pinNumber)
        {
             data.UpdateUserPassword(accountNumber, pinNumber);
        }

        public void UpdateUsername(string accountNumber, string userName)
        {
             data.UpdateUsername(accountNumber, userName);
        }

        public void  UpdateEmail(string accountNumber, string email)
        {
             data.UpdateEmail(accountNumber, email);
        }



    }
}