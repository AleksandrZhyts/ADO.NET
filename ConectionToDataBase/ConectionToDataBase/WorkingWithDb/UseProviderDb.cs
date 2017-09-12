using ConectionToDataBase.Provider_DB;
using System;
using System.Configuration;
using ConectionToDataBase.ListQueries;

namespace ConectionToDataBase.WorkingWithDb
{
    static class UseProviderDb
    {
        public static void CreateQueryToDb()
        {
            try
            {
                ProviderDb provider;
                switch (MenuTypeDb())
                {
                    case 1:
                        provider = new ProviderDb("Sql");
                        break;
                    case 2:
                        provider = new ProviderDb("Ole");
                        break;
                    default:
                        return;
                }
                
                string query;
                while (true)
                {                       
                    switch (MenuQuery())
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
                            query = Queries.query9;
                            break;
                        case 10:
                            query = Queries.query10;
                            break;
                        case 11:
                            query = Queries.query11;
                            break;
                        case 12:
                            query = Queries.query12;
                            break;
                        case 13:
                            Console.WriteLine("Введите запрос на выборку: ");
                            query = Console.ReadLine();                               
                            break;
                        default:
                            return;
                    }
                    Console.Clear();
                    provider.ShowResultQuery(query);
                    Console.WriteLine("\nPress any key...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }
        public static int MenuQuery()
        {
            Console.WriteLine("\t1 - Query №1 DB=med.db\n" +
                            "\t2 - Query №2 DB=med.db\n" +
                            "\t3 - Query №3 DB=med.db\n" +
                            "\t4 - Query №4 DB=med.db\n" +
                            "\t5 - Query №5 DB=med.db\n" +
                            "\t6 - Query №6 DB=med.db\n" +
                            "\t7 - Query №7 DB=med.db\n" +
                            "\t8 - Query №8 DB=med.db\n" +
                            "\t9 - Query №9 DB=Northwind.db\n" +
                            "\t10 - Query №10 DB=Northwind.db\n" +
                            "\t11 - Query №11 DB=Northwind.db\n" +
                            "\t12 - Query №12 DB=Northwind.db\n" +
                            "\t13 - Your query\n" +
                            "\tq - Exit");
            int choise;
            var answer = Console.ReadLine();
            if (!int.TryParse(answer, out choise))
                choise = 0;
            return choise;
        }

        public static int MenuTypeDb()
        {
            Console.WriteLine("\t1 - Sql Db\n" +
                              "\t2 - Ole Db\n" +
                              "\tq - Exit");
            int choise;
            var answer = Console.ReadLine();
            if (!int.TryParse(answer, out choise))
                choise = 0;
            return choise;
        }
    }
}
