using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EWalletModels;

namespace EWalletDataLayer
{
    public class SqlDbData
    {
         string connectionString = "Data Source=LAPTOP-NV0945RI\\SQLEXPRESS;Initial Catalog=EWallet;Integrated Security=True;"; //Local host ssms
           //= "Server = tcp:20.6.32.91,1433;Database =EWallet;User Id = sa; Password = Password1234"; //Azure Virtual machine
        SqlConnection sqlConnection; 

        public SqlDbData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }


        public List<User> GetUsers()
        {
            string selectStatement = "SELECT * FROM Records";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            List<User> users = new List<User>();
            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                User readUser = new User
                {
                    accountNumber = reader["accountNumber"].ToString(),
                    pinNumber = reader["pinNumber"].ToString(),
                    userName = reader["userName"].ToString(),
                    money = Convert.ToDecimal(reader["money"]),
                    email = reader["email"].ToString()
                };
                users.Add(readUser);
            }

            sqlConnection.Close();
            return users;
        }

        public User GetUserByAccNum(string accountNumber)
        {
            sqlConnection.Open();
            SqlCommand findCommand = new SqlCommand("SELECT * FROM Records WHERE accountNumber = @accountNumber", sqlConnection);
            findCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
            SqlDataReader reader = findCommand.ExecuteReader();

            if (reader.Read())
            {
                User user = new User
                {
                    accountNumber = reader["accountNumber"].ToString(),
                    userName = reader["userName"].ToString(),
                    money = Convert.ToDecimal(reader["money"]),
                    pinNumber = reader["pinNumber"].ToString(),
                    email = reader["email"].ToString()
                };
                sqlConnection.Close();
                return user;
            }

            sqlConnection.Close();
            return null;
        }

        public void UpdateMoney(User user)
        {
            string updateStatement = "UPDATE Records SET money = @money WHERE accountNumber = @accountNumber";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@money", user.money);
            updateCommand.Parameters.AddWithValue("@accountNumber", user.accountNumber);

            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void AddUser(string accountNumber, string userName, string pinNumber, string email)
        {
            decimal money = 0;
            string insertStatement = "INSERT INTO Records (accountNumber, userName, pinNumber, money, email) VALUES (@accountNumber, @userName, @pinNumber, @money, @email)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
            insertCommand.Parameters.AddWithValue("@userName", userName);
            insertCommand.Parameters.AddWithValue("@pinNumber", pinNumber);
            insertCommand.Parameters.AddWithValue("@money", money);
            insertCommand.Parameters.AddWithValue("@email", email);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateUserPassword(string accountNumber, string pinNumber)
        {
            string updateStatement = "UPDATE Records SET pinNumber = @pinNumber WHERE accountNumber = @accountNumber";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
            updateCommand.Parameters.AddWithValue("@pinNumber", pinNumber);

            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateUsername(string accountNumber, string userName)
        {
            string updateStatement = "UPDATE Records SET userName = @userName WHERE accountNumber = @accountNumber";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@accountNumber", accountNumber);
            updateCommand.Parameters.AddWithValue("@userName", userName);

            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void DeleteUser(string accountNumber)
        {
            string deleteStatement = "DELETE FROM Records WHERE accountNumber = @accountNumber";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            sqlConnection.Open();

            deleteCommand.Parameters.AddWithValue("@accountNumber", accountNumber);

            deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

    }
}
