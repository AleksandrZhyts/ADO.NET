using System;
using System.Data.OleDb;

namespace ConectionToDataBase.OLE_DB
{
    class ProviderOleDb : IDisposable   
    {
        #region Fields
        private OleDbConnection _connection;
        private bool _disposed;
        #endregion

        public ProviderOleDb(string connectionString)
        {
            this._connection = new OleDbConnection(connectionString);
            _connection.Open();
            this._disposed = false;
        }

        public void ShowResultQuery(string query)
        {
            if (_disposed)
                throw new ObjectDisposedException("Ресурс был освобожден.");
            OleDbCommand command = new OleDbCommand(query, _connection);
            OleDbDataReader reader = command.ExecuteReader();
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

        #region IDisposable Members
        public void Dispose()
        {
            this.Dispose(true);
            // чтобы не выполнять финализацию после явного освобождения ресурсов
            GC.SuppressFinalize(this); 
        }
        #endregion

        ~ProviderOleDb()   
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                        _connection.Dispose();
                    Console.WriteLine("Ресурс освобожден.");
                }
                // Индикация, что ресурс уже был освобожден
                this._connection = null;
                this._disposed = true;
            }
        }
    }
}
