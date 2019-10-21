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

        public List<object> Select(string tablename, int rowStart, int rowCount, int[] ids)
        {
            using (var con = new MySqlConnection(CnnVal(ConnectionString)))
            {
                if(rowStart == 0 && rowCount == 0 && ids == null)
                {
                    return con.Query<dynamic>($"select * from {tablename}").ToList();
                }
                else if(ids != null)
                {
                    string IdsString = "(";
                    for(int i = 0; i < ids.Count() - 1; ++i)
                    {
                        IdsString += "'" + ids[i] + "',";
                    }
                    IdsString += "'" + ids[ids.Count() - 1] + "')";
                    return con.Query<dynamic>($"select * from {tablename} where Id in {IdsString}").ToList();
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