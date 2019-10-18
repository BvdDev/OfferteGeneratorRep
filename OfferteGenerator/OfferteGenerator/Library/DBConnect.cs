using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using OfferteGenerator.Models;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace OfferteGenerator.Library
{
    public class DBConnect
    {
        public string ConnectionString;

        public DBConnect(string connectionsString)
        {
            ConnectionString = connectionsString;
        }

        public List<object> Select(string tablename, int rowStart, int rowCount)
        {
            using (var con = new MySqlConnection(CnnVal(ConnectionString)))
            {
                if(rowStart == 0 && rowCount == 0)
                {
                    return con.Query<dynamic>($"select * from {tablename}").ToList();
                }
                else
                {
                    return con.Query<dynamic>($"select * from {tablename} limit {rowStart.ToString()} , {rowCount.ToString()}").ToList();
                }
            }
        }

        public string CountRows(string tablename)
        {
            using (MySqlConnection conn = new MySqlConnection(CnnVal(ConnectionString))) 
            using (MySqlCommand cmd = new MySqlCommand($"select count(*) from {tablename}", conn))
            {
                conn.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}