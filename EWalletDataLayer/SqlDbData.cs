using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EWalletModels;
using System.Security.Principal;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Data.SqlTypes;

namespace EWalletDataLayer
{
    public class SqlDbData
    {
        string connectionString
      = "Data Source =LAPTOP-QH3G5ET7\\SQLEXPRESS; Initial Catalog = EWallet; Integrated Security = True;";

        SqlConnection sqlConnection;

        public SqlDbData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<User> GetUsers()
        {
            string selectStatement = "SELECT accountNumber, pinNumber FROM Records";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            List<User> users = new List<User>();

            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                int accountNumber = Convert.ToInt32(reader["accountNumber"]);
                string pinNumber = reader["pinNumber"].ToString();

                User readUser = new User();
                readUser.accountNumber = accountNumber;
                readUser.pinNumber = pinNumber;

                users.Add(readUser);
            }

            sqlConnection.Close();
            return users;
        }

        public User GetUserByAccNum(int accountNumber)
        {
            sqlConnection.Open();

            SqlCommand findCommand = new SqlCommand("SELECT * FROM Records WHERE accountNumber = @accountNumber", sqlConnection);
            findCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

            SqlDataReader reader = findCommand.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    accountNumber = Convert.ToInt32(reader["accountNumber"]),
                    userName = reader["userName"].ToString(),
                    money = Convert.ToDecimal(reader["money"]),
                    pinNumber = reader["pinNumber"].ToString(),
                };
            }
            sqlConnection.Close();
            return null;
        }



        public void UpdateMoney(User user)
        {
            string updateStatement = $"UPDATE Records SET money = @money WHERE accountNumber = @accountNumber";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            sqlConnection.Open();

            updateCommand.Parameters.AddWithValue("@money", user.money);
            updateCommand.Parameters.AddWithValue("@accountNumber", user.accountNumber);

            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void AddUser(int accountNumber, string userName, string pinNumber)
        {
            decimal money = 0;

            string insertStatement = "INSERT INTO Records (AccountNumber, UserName, PinNumber, money) VALUES (@accountNumber, @userName, @pinNumber, @money)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
            insertCommand.Parameters.AddWithValue("@userName", userName);
            insertCommand.Parameters.AddWithValue("@pinNumber", pinNumber);
            insertCommand.Parameters.AddWithValue("@money", money);


            sqlConnection.Open();

            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

        }
        
        public void UpdateUserPassword(int accountNumber, string pinNumber)
        {
            string updateStatement = $"UPDATE Records SET pinNumber = @pinNumber WHERE accountNumber = @accountNumber";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            sqlConnection.Open();

            updateCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
            updateCommand.Parameters.AddWithValue("@pinNumber", pinNumber);

            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();

        }
        public void UpdateUsername(int accountNumber, string userName)
        {
            string updateStatement = $"UPDATE Records SET userName = @userName WHERE accountNumber = @accountNumber";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            sqlConnection.Open();

            updateCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
            updateCommand.Parameters.AddWithValue("@userName", userName);

            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void DeleteUser(int accountNumber)
        {
            string deleteStatement = $"DELETE FROM Records WHERE accountNumber = @accountNumber";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            sqlConnection.Open();

            deleteCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

            deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();

        }
    }
}