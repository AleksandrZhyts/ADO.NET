using System;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ConectionToDataBase.Provider_DB
{
    class ProviderDb   
    {
        #region Fields
        private OleDbConnection _connectionOle;
        private SqlConnection _connectionSql;
        private string _typeDb;
        #endregion

        public ProviderDb(string typeDb)
        {
            if (!String.IsNullOrEmpty(typeDb))
                _typeDb = typeDb;
            else throw new Exception("An empty string was passed to the constructor");
            if (string.Compare(typeDb,"Ole") == 0)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                this._connectionOle = new OleDbConnection(connectionString);
            }
            else    
                if (string.Compare(typeDb, "Sql") == 0)
                {
                    this._connectionSql = new SqlConnection();
                    _connectionSql.ConnectionString = ConfigurationManager.ConnectionStrings["NorthWindConnecthionString"].ConnectionString;
                }
                else throw new Exception("An incorrect string was passed to the constructor");
        }

        public void ShowResultQuery(string query)
        {
            try
            {
                if (string.Compare(_typeDb, "Ole") == 0)
                {
                    _connectionOle.Open();
                    OleDbCommand command = new OleDbCommand(query, _connectionOle);
                    OleDbDataReader reader = command.ExecuteReader();
                    ReaderOle(reader);
                }
                else
                {
                    _connectionSql.Open();
                    SqlCommand cmdSelect = _connectionSql.CreateCommand();
                    cmdSelect.CommandText = query;
                    SqlDataReader reader = cmdSelect.ExecuteReader();
                    ReaderSql(reader);
                }
            }
            catch { throw; }
            finally
            {
                if (string.Compare(_typeDb, "Ole") == 0)
                    _connectionOle.Close();
                else
                    _connectionSql.Close();
                Console.WriteLine("Ресурс освобожден.");
            }           
        }

        private void ReaderOle(OleDbDataReader reader)
        {
            if (reader.HasRows)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write($"{reader.GetName(i),-15}\t");
                Console.WriteLine("\n");
                while (reader.Read() != false)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.Write($"{reader.GetValue(i),-15}\t");
                    Console.WriteLine();
                }
            }
        }

        private void ReaderSql(SqlDataReader reader)
        {
           if (reader.HasRows)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write($"{reader.GetName(i),-15}\t");
                Console.WriteLine("\n");
                //FileStream fs = new FileStream(@"temp.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                //StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                while (reader.Read() != false)
                {
                    //for (int i = 0; i < reader.FieldCount; i++)
                    //    sw.Write($"{reader.GetValue(i),-20}\t");
                    //sw.WriteLine();
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.Write($"{reader.GetValue(i),-15} ");
                    Console.WriteLine();
                }
                //sw.Dispose();
            }
        }
    }
}
