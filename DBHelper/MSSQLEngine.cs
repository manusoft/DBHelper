using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBHelper
{
    public class MSSQLEngine
    {
        private String ConnString = string.Empty;
        private SqlConnection ConnectionObj;
        private SqlTransaction TransactionObj;

        /// <summary>
        /// Used to open a connection with the database. 
        /// If successful returns True, otherwise it returns False.
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public bool Connect(string ConnectionString)
        {
            try
            {
                // First get the connection string
                ConnString = ConnectionString;

                // Now create a database connection object
                ConnectionObj = new SqlConnection(ConnString);
                ConnectionObj.Open();

                TransactionObj = ConnectionObj.BeginTransaction();

                return true;
            }
            catch (Exception)
            {             
                return false;
            }
        }

        /// <summary>
        /// Used to close the existing connection to the database.
        /// If successful returns True, otherwise it returns False.
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                this.Rollback();
                ConnectionObj.Close();
                ConnectionObj.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to execute the SQL statements or commands.
        /// </summary>
        /// <param name="Query">SQL query or stored procedure</param>
        /// <param name="Values">Values (optional)</param>
        /// <returns></returns>
        public DataTable Execute(string Statement, params object[] Parameters)
        {
            var CMD = new SqlCommand
            {
                Connection = ConnectionObj,
                Transaction = TransactionObj,
                CommandText = Statement
            };

            if (CMD.CommandText.Contains(" "))
            {
                CMD.CommandType = CommandType.Text;
            }
            else
            {
                CMD.CommandType = CommandType.StoredProcedure;
            }

            for (int i = 0; i < Parameters.Count(); i++)
            {
                CMD.Parameters.AddWithValue("@" + i.ToString(), Parameters[i]);
            }

            var DA = new SqlDataAdapter();
            var DT = new DataTable();
            DA.SelectCommand = CMD;
            DA.Fill(DT);

            CMD.Dispose();
            DA.Dispose();
            return DT;
        }

        /// <summary>
        /// Used to get records from the database.
        /// </summary>
        /// <param name="Statement"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public DataTable Fill(string Statement, params object[] Parameters)
        {
            var CMD = new SqlCommand
            {
                Connection = ConnectionObj,
                Transaction = TransactionObj,
                CommandText = Statement
            };

            if (CMD.CommandText.Contains(" "))
            {
                CMD.CommandType = CommandType.Text;
            }
            else
            {
                CMD.CommandType = CommandType.StoredProcedure;
            }

            for (int i = 0; i < Parameters.Count(); i++)
            {
                CMD.Parameters.AddWithValue("@" + i.ToString(), Parameters[i]);
            }

            var DA = new SqlDataAdapter();
            var DT = new DataTable();
            DA.SelectCommand = CMD;
            DA.Fill(DT);

            CMD.Dispose();
            DA.Dispose();
            return DT;
        }

        /// <summary>
        /// Used to commit a transaction.
        /// </summary>
        public void Commit()
        {
            TransactionObj.Commit();
            TransactionObj.Dispose();
            TransactionObj = ConnectionObj.BeginTransaction();
        }

        /// <summary>
        ///  Used to rollback transactions.
        /// </summary>
        public void Rollback()
        {
            TransactionObj.Rollback();
            TransactionObj.Dispose();
            TransactionObj = ConnectionObj.BeginTransaction();
        }

    }
}
