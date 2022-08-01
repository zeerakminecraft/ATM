using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ATM
{
    internal class DBCrud
    {
        //private SqlConnection sqlConnection;
        //private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";

        //DBCrud()
        //{
        //    //connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";
        //    try
        //    {
        //        sqlConnection = new SqlConnection(connectionString);
        //        sqlConnection.Open();
        //        Console.WriteLine("Connection Established");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        static public int readPin(long cardNum)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";
            SqlDataReader dataReader;
            
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                //Console.WriteLine("connection est~~~~~~ GETTING PIN...");

          
                string sql = "SELECT Pin FROM Users WHERE CardNumber="+cardNum.ToString();
                var cmd = new SqlCommand(sql, sqlConnection);
                dataReader = cmd.ExecuteReader();
                
                while (dataReader.Read())
                {
                    return Convert.ToInt32(dataReader.GetValue(0));
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("readPin catch bloc");
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        static public int readBalance(long cardNum)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";
            SqlDataReader dataReader;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                
                string sql = "SELECT Balance FROM Users WHERE CardNumber=" + cardNum.ToString();
                var cmd = new SqlCommand(sql, sqlConnection);
                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    
                    return Convert.ToInt32(dataReader.GetValue(0));
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("readBalance catch bloc");
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        static public string readName(long cardNum)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";
            SqlDataReader dataReader;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open(); 
                string sql = "SELECT Name FROM Users WHERE CardNumber=" + cardNum.ToString();
                var cmd = new SqlCommand(sql, sqlConnection);
                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    //Console.WriteLine(dataReader.GetValue(0));
                    return dataReader.GetValue(0).ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("readName catch bloc");
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        static public Dictionary<string, object> SerializeRow(IEnumerable<string> cols, SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        static public SqlDataReader readTransaction(string tableName)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";
            SqlDataReader dataReader;
            string sqltrash = "SELECT Name FROM Users WHERE CardNumber=" + "000";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "SELECT * FROM Users WHERE CardNumber=" + tableName;
                var cmd = new SqlCommand(sql, sqlConnection);
                dataReader = cmd.ExecuteReader();
                var results = new List<Dictionary<string, object>>();
                var cols = new List<string>();
                for (var i = 0; i < dataReader.FieldCount; i++)
                    cols.Add(dataReader.GetName(i));

                while (dataReader.Read())
                    results.Add(SerializeRow(cols, dataReader));

                Console.WriteLine(results);
                //while (dataReader.Read())
                //{
                //    //Console.WriteLine(dataReader.GetValue(0));
                //    return dataReader.GetValue(0).ToString();
                //}
                return dataReader;
            }
            catch (Exception ex)
            {
                Console.WriteLine("readName catch bloc");
                Console.WriteLine(ex.Message);
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                var cmd = new SqlCommand(sqltrash, sqlConnection);
                return cmd.ExecuteReader();
            }
        }

        static public void updateBalance(long cardNum, int amt)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                Console.WriteLine("\n\n\n\t\t\t******~~~~~~  Processing transaction...   ~~~~~~*******");
                string sql = "UPDATE Users SET Balance = "+ amt.ToString()+ " WHERE CardNumber=" + cardNum.ToString();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.ExecuteNonQuery();
                //Console.WriteLine("\n\n\n\n\n\t\t\t\t^^^^~~~~~~ transaction successful ~~~~~~^^^^^");
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("updateBalance catch bloc");
                Console.WriteLine("\n\n\n\n\t\t\t\t!!!!!--- Transaction unsuccessful ---!!!!!!");
                Console.WriteLine(ex.Message);
            }
        }

        static public void writeTransaction(long cardNum, string tableName, int amt)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zeerak.Ibrahim\source\repos\ATM\ATM\Database1.mdf;Integrated Security=True";
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            string sql1 = "INSERT INTO "+ tableName +" (CardNumber, Amount, Transaction_Time) VALUES(" + cardNum + "," + amt + "," + DateTime.Now.ToString(format) + ")";
            Console.WriteLine(sql1);
            
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string n = DateTime.Now.ToString(format);
                //n = n.Replace(" ", "_time:");
                Console.WriteLine(n);

                string sql = "INSERT INTO " + tableName + "(CardNumber, Amount, Transaction_Time)" + " VALUES (" + cardNum.ToString() +", "+ amt.ToString() + ", "+n+ ")";

                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("writeTansaction catch bloc");
                Console.WriteLine("\n\t\t\t\t!!!!!--- Transaction unsuccessful ---!!!!!!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
