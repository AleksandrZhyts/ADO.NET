using ConectionToDataBase.OLE_DB;
using System;
using System.Configuration;
using ConectionToDataBase.ListQueries;

namespace ConectionToDataBase.WorkingWithOleDb
{
    static class UseProviderOleDb
    {
        public static void CreateQueryToDb()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (ProviderOleDb OleDb = new ProviderOleDb(connectionString))
                {
                    string query;
                    while (true)
                    {                       
                        switch (Menu())
                        {
                            case 1:
                                query = Queries.query1;
                                break;
                            case 2:
                                query = Queries.query2;
                                break;
                            case 3:
                                query = Queries.query3;
                                break;
                            case 4:
                                query = Queries.query4;
                                break;
                            case 5:
                                query = Queries.query5;
                                break;
                            case 6:
                                query = Queries.query6;
                                break;
                            case 7:
                                query = Queries.query7;
                                break;
                            case 8:
                                query = Queries.query8;
                                break;
                            case 9:
                                Console.WriteLine("Введите запрос на выборку: ");
                                query = Console.ReadLine();                               
                                break;
                            default:
                                return;
                        }
                        Console.Clear();
                        OleDb.ShowResultQuery(query);
                        Console.WriteLine("\nPress any key...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }
        public static int Menu()
        {
            Console.WriteLine("\t1 - Выполнить запрос №1 из Д/з\n" +
                            "\t2 - Выполнить запрос №2 из Д/з\n" +
                            "\t3 - Выполнить запрос №3 из Д/з\n" +
                            "\t4 - Выполнить запрос №4 из Д/з\n" +
                            "\t5 - Выполнить запрос №5 из Д/з\n" +
                            "\t6 - Выполнить запрос №6 из Д/з\n" +
                            "\t7 - Выполнить запрос №7 из Д/з\n" +
                            "\t8 - Выполнить запрос №8 из Д/з\n" +
                            "\t9 - Выполнить свой запрос\n" +
                            "\tq - Выход");
            int choise;
            var answer = Console.ReadLine();
            if (!int.TryParse(answer, out choise))
                choise = 0;
            return choise;
        }
    }
}
