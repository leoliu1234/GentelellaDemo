using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Gentelella.Service
{
    public class Parameter : Dictionary<string, object>
    {
    }

    public class TransactionParameter
    {
        public string SQL { get; set; }

        public List<Parameter> Parameters { get; set; }
    }

    public static class SqlDataReaderExtension
    {
        public static int GetInt(this SqlDataReader reader, string key)
        {
            return Convert.ToInt32(reader[key]);
        }

        public static string GetString(this SqlDataReader reader, string key)
        {
            return reader[key].ToString();
        }

        public static long GetLong(this SqlDataReader reader, string key)
        {
            return Convert.ToInt64(reader[key]);
        }

        public static bool GetBoolean(this SqlDataReader reader, string key)
        {
            return Convert.ToBoolean(reader[key]);
        }
    }

    public static class DBHelper
    {
        private static string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public static bool ExecuteNonQuery(string sql, object[] parameters)
        {
            return Execute<bool>(sql, parameters, sqlCommand =>
            {
                return sqlCommand.ExecuteNonQuery() > 0;
            });
        }

        public static T ExecuteDataReader<T>(string sql, object[] parameters, Func<SqlDataReader, T> func)
        {
            return Execute<T>(sql, parameters, sqlCommand =>
            {
                using (var reader = sqlCommand.ExecuteReader())
                {
                    return func(reader);
                }
            });
        }

        public static T ExecuteScalar<T>(string sql, object[] parameters)
        {
            return Execute<T>(sql, parameters, sqlCommand =>
            {
                return (T)sqlCommand.ExecuteScalar();
            });
        }

        private static T Execute<T>(string sql, object[] parameters, Func<SqlCommand, T> func)
        {
            try
            {
                using (var conn = new SqlConnection(CONNECTION_STRING))
                {
                    using (var comm = new SqlCommand(sql, conn))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            var regex = new Regex("@[a-zA-Z0-0_]*");
                            var matchCollect = regex.Matches(sql);
                            var hashSet = new HashSet<string>();
                            for (var i = 0; i < matchCollect.Count; i++)
                            {
                                hashSet.Add(matchCollect[i].Value);
                            }

                            var j = 0;
                            foreach (var key in hashSet)
                            {
                                if (parameters[j] == null)
                                {
                                    var tempParameter = comm.Parameters.AddWithValue(key, DBNull.Value);
                                    tempParameter.IsNullable = true;
                                }
                                else
                                {
                                    comm.Parameters.AddWithValue(key, parameters[j]);
                                }
                                j++;
                            }
                        }
                        conn.Open();
                        return func(comm);
                    }
                }
            }
            catch (Exception e)
            {
                //TODO: log
                throw;
            }
        }

        private static void Execute(Action<SqlConnection, SqlTransaction> action)
        {
            Execute(conn =>
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (action != null)
                        {
                            action(conn, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        //TODO: log
                        throw;
                    }
                }
            });
        }

        private static void Execute(Action<SqlConnection> action)
        {
            using (var conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                try
                {
                    if (action != null)
                    {
                        action(conn);
                    }
                }
                catch (Exception e)
                {
                    //TODO: log
                    throw;
                }
            }
        }

        private static DataTable ExecuteAdapter(string sql, object[] parameters)
        {
            return Execute<DataTable>(sql, parameters, comm =>
            {
                using (var adapter = new SqlDataAdapter(comm))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            });
        }

        public static void ExecuteTransaction(List<TransactionParameter> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                //Execute((conn, tran) =>
                //{
                //    var comm = new SqlCommand();
                //    comm.Connection = conn;
                //    comm.Transaction = tran;
                //    parameters.ForEach(p =>
                //    {
                //        comm.CommandText = p.SQL;
                //        if (p.Parameters != null)
                //        {
                //            p.Parameters.ForEach(item =>
                //            {
                //                if (item.Value == null)
                //                {
                //                    var tempParameter = comm.Parameters.AddWithValue(item.Key, DBNull.Value);
                //                    tempParameter.IsNullable = true;
                //                }
                //                else
                //                {
                //                    comm.Parameters.AddWithValue(item.Key, item.Value);
                //                }
                //            });
                //            comm.ExecuteNonQuery();
                //            comm.Parameters.Clear();
                //        }
                //    });
                //});
            }
        }

        /// <summary>
        /// example : 
        ///void invoke()
        ///{
        ///    DBHelper.ExecuteBulkCopy("userinfo", table =>
        ///    {
        ///        for (int i = 0; i < 1000000; i++)
        ///        {
        ///            var row = table.NewRow();
        ///            row["name"] = "leo";
        ///            row["age"] = 25;
        ///            row["email"] = "leo@leo.com";
        ///            row["test"]=null
        ///            table.Rows.Add(row);
        ///        }
        ///    });
        ///}
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="action"></param>
        public static void ExecuteBulkCopy(string tableName, Action<DataTable> action)
        {
            var table = ExecuteAdapter("select top 0 * from " + tableName + " with (nolock)", null);

            if (action != null)
            {
                action(table);
            }

            Execute((conn, transaction) =>
            {
                using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BatchSize = 800;
                    bulkCopy.WriteToServer(table);
                }
            });
        }
    }
}