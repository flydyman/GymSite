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
        string ConnectionString;
        DbConnection Connection;
        MyDBType dbtype;
        public DataSet dataSet {get;set;}

        public MyDBUse(MyDBType dBType, string conn, string name)
        {
            dbtype = dBType;
            ConnectionString = conn;
            switch (dbtype)
            {
                case(MyDBType.mysql):
                    Connection = new MySqlConnection(ConnectionString);
                    break;
                case(MyDBType.sqlite):
                    Connection = new SQLiteConnection(ConnectionString);
                    break;
            }
            dataSet = new DataSet(name);
        }

        private DbCommand GetCommand(string query)
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

        public DbDataReader GetReader (string query)
        {
            Connection.Open();
            return GetCommand(query).ExecuteReader();
        }

        public int Update(DataSet dataSet)
        {
            int res = 0;
            try
            {
                if (!dataSet.IsInitialized)
                    throw new Exception("DATASET IS NOT INITIALIZED!");
                Connection.Open();
            }
            finally 
            {
                Connection.Close();
            }
            return res;
        }
        public string GetSQL(string Expression, object O)
        {
            string res = Expression;
            List<ObjField> fields= Commons.GetObjFields(O);
            foreach (ObjField f in fields)
            {
                string s = " "+f.FieldName+" = '"+ f.FieldValue+"'";
                res += s;
            }
            return res;
        }
        public string GetSQL(string Expression, List<object> Os)
        {
            string res = Expression;
            foreach (object o in Os)
            {
                List<ObjField> fields = Commons.GetObjFields(o);
                foreach (ObjField f in fields)
                {
                    string s = " "+f.FieldName+" = '"+ f.FieldValue+"'";
                    res += s;
                }
            }
            return res;
        }
        public void Close()
        {
            Connection.Close();
        }
    }
}