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
    public class MyDBUse
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
            switch (dbtype)
            {
                case (MyDBType.mysql):
                    return new MySqlCommand(query, (MySqlConnection)Connection);
                case (MyDBType.sqlite):
                    return new SQLiteCommand(query, (SQLiteConnection)Connection);
            }
            return null;
        }
        private DbCommandBuilder GetCommBuilder(DataAdapter adapter)
        {
            switch (dbtype)
            {
                case (MyDBType.mysql):
                    return new MySqlCommandBuilder((MySqlDataAdapter)adapter);
                case (MyDBType.sqlite):
                    return new SQLiteCommandBuilder((SQLiteDataAdapter)adapter);
            }
            return null;
        }
        private DataAdapter GetDataAdapter(string query)
        {
            switch (dbtype)
            {
                case (MyDBType.mysql):
                    return new MySqlDataAdapter(query, (MySqlConnection)Connection);
                case (MyDBType.sqlite):
                    return new SQLiteDataAdapter(query, (SQLiteConnection)Connection);
            }
            return null;
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
    }
}