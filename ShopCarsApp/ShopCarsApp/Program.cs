using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCarsApp
{
class Program
{
    static void Main(string[] args)
    {
        SqlConnection connection = new SqlConnection();
        //1 way //connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ShopCars;Integrated Security=True;Timeout=5";

        //2 way  //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //builder.DataSource = @"(LocalDB)\MSSQLLocalDB";
        //builder.InitialCatalog = "ShopCars";
        //builder.IntegratedSecurity = true;
        //builder.ConnectTimeout = 5;
        //builder.Pooling = true;
        //connection.ConnectionString = builder.ConnectionString;

        connection.ConnectionString = ConfigurationManager.ConnectionStrings["CarsDbConnectionString"].ConnectionString;
        Console.WriteLine(ConfigurationManager.AppSettings["IpTest"]);


        //connection.StateChange += Connection_StateChange;
        try
        {
            connection.Open();
            //ShowData(connection);
            SqlCommand cmdInsert = connection.CreateCommand();
            cmdInsert.CommandText = @"INSERT INTO CARS (NAME,PRICE) VALUES (@NAME, @PRICE)";
            cmdInsert.Parameters.AddWithValue("@NAME", "KAMAZ");

            SqlParameter paramPrice = new SqlParameter()
            {
                ParameterName = "@PRICE",
                Value = 245000,
                Direction = ParameterDirection.Input
            };
            cmdInsert.Parameters.Add(paramPrice);

            SqlCommand cmdCountRows = connection.CreateCommand();
            cmdCountRows.CommandText = @"SELECT COUNT(*) FROM CARS";
            Console.WriteLine((int)cmdCountRows.ExecuteScalar());
            //ShowData(connection);

            int insertRows = cmdInsert.ExecuteNonQuery();
            Console.WriteLine(insertRows);
            ShowData(connection);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    private static void ShowData(SqlConnection connection)
    {
        SqlCommand cmdSelect = connection.CreateCommand();
        cmdSelect.CommandText = @"SELECT * FROM CARS";
        SqlDataReader reader = cmdSelect.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine("Name: {0}, Price:{1:N2}", reader[1], reader["Price"]);
        }
        reader.Close();
    }

    private static void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
    {
        SqlConnection connection = sender as SqlConnection;
        if(connection!=null)
        {
            Console.WriteLine(connection.DataSource);
            Console.WriteLine(connection.Database);
            Console.WriteLine(connection.ConnectionTimeout);
        }
        Console.WriteLine(e.CurrentState);
    }
}
}
