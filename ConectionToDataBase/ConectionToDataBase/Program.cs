using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ConectionToDataBase
{
class Program
{
    static void Main(string[] args)
    {
        //string connectionString = @"Data Source=.\SQLEXPRESS14; Initial Catalog=Internet Shop; Integrated Security=True";
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string query = "Select * From Clients";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand (query, connection);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows) // если есть данные
            {
                Console.WriteLine($"{dr.GetName(0),2}{dr.GetName(1),15}{dr.GetName(2),30}{dr.GetName(3),15}");
                while (dr.Read())
                {
                    Console.WriteLine($"{dr.GetValue(0),2}{dr.GetValue(1),33}{dr.GetValue(2),20}{dr.GetValue(3),33}");
                }
                    Console.WriteLine("Подключение открыто\n" +
                                      "Свойства подключения:" +
                                      $"\n\tСтрока подключения: {connection.ConnectionString}" +
                                      $"\n\tБаза данных: {connection.Database}" +
                                      $"\n\tСервер: {connection.DataSource}" +
                                      $"\n\tВерсия сервера: {connection.ServerVersion}" +
                                      $"\n\tСостояние: {connection.State}" +
                                      $"\n\tWorkstationld: {connection.WorkstationId}");
                }
        }
        Console.WriteLine("Подключение закрыто...");
        Console.Read();
    }
}
}
