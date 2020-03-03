using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;

namespace GymSite
{
    public enum MyDBType{
        mysql,
        sqlite
    }
    public class MyDBUse : IDisposable
    {
        DbConnection Connection;
        MyDBType dbtype;

        public MyDBUse(MyDBType dBType, string conn)
        {
            dbtype = dBType;
            switch (dbtype)
            {
                case(MyDBType.mysql):
                    Connection = new MySqlConnection(conn);
                    break;
                case(MyDBType.sqlite):
                    Connection = new SQLiteConnection(conn);
                    break;
            }
            Connection.Open();
        }


        public DbCommand GetCommand(string query)
        {
            /*
            switch (dbtype)
            {
                case (MyDBType.mysql):
                    return new MySqlCommand(query, (MySqlConnection)Connection);
                case (MyDBType.sqlite):
                    return new SQLiteCommand(query, (SQLiteConnection)Connection);
            }
            */
            DbCommand comm = Connection.CreateCommand();
            comm.CommandText = query;
            return comm;
        }
        public DbDataReader GetReader (string query)
        {
            // For direct access
            //Connection.Open();
            return GetCommand(query).ExecuteReader();
        }
        public void Close()
        {
            Connection.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}