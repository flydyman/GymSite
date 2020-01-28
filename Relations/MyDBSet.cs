using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Data.Common;
using GymSite;
using GymSite.Models;
using System.Collections;
using System.Collections.Generic;

namespace GymSite.Relations
{
    public class MyDBSet <T> : IEnumerable where T: new ()
    {
        List<T> Items = new List<T>();
        public List<T> GetList {
            get{
                FillItems();
                return Items;
            }
        }
        string TableName;
        List<string> Fields = new List<string>();
        public string GetFields {
            get{
                string s = "";
                for (int i = 0; i< Fields.Count; i++)
                {
                    s += Fields[i];
                    if (i != Fields.Count - 1 ) s += ", ";
                }
                return s;
            }
        }
        MyDBUse db;
        public string SelectString{ 
            get{
                return $"SELECT * FROM {TableName}";
            }
        }


        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Парсит DbDataReader в обьект
        /// </summary>
        public T ParseRow(DbDataReader row)
        {
            T res = new T();
            var pi = typeof(T).GetProperties();
            foreach (string f in Fields)
            {
                var p = pi.FirstOrDefault(x =>
                    x.Name == f);
                p.SetValue(res, row[f], default);
            }
            return res;
        }

        public string ParseToDelete(string fieldName, string fieldValue)
        {
            return $"DELETE FROM {TableName} WHERE {fieldName} = {fieldValue};";
        }
        public string ParseToDelete(string expression)
        {
            return $"DELETE FROM {TableName} WHERE {expression};";
        }

        public string ParseToUpdate(T obj, string idName = "")
        {
            var pi = typeof(T).GetProperties();
            string pairs = "";
            int startAt = idName == "" ? 1 : 0;
            for (int i = startAt; i< pi.Count(); i++)
            {
                if (startAt ==0 && pi[i].Name != idName)
                {
                    if (pi[i].Name!="DateTime")
                        pairs += pi[i].Name + " = '" + pi[i].GetValue(obj, null).ToString() + "'";
                    else
                        pairs += pi[i].Name + " = '" + Convert.ToDateTime(pi[i].GetValue(obj, null)).Date.ToString("yyyyMMdd") + "'";
                }
                if (i != pi.Count() - 1) pairs += ", ";
            }
            string where = idName == "" ? $"{pi[0].Name} = {pi[0].GetValue(obj).ToString()}" : $"{idName} = {pi[Fields.IndexOf(idName)].GetValue(obj).ToString()}";
            return $"UPDATE {TableName} SET ({pairs}) WHERE ({where});";
        }

        public string ParseToInstert(T obj)
        {
            var pi = typeof(T).GetProperties();
            string res = "";
            for (int i = 0; i< pi.Count(); i++)
            {
                if (pi[i].Name!="DateTime")
                    res += "'" + pi[i].GetValue(obj, null).ToString() + "'";
                else
                    res += "'" + Convert.ToDateTime(pi[i].GetValue(obj)).Date.ToString("yyyyMMdd") + "'";
                if (i != pi.Count() - 1) res += ", ";
            }
            return $"INSERT INTO {TableName} ({GetFields}) VALUES ({res}); SELECT LAST_INSERT_ID();";
        }
        
        public void FillItems()
        {
            DbDataReader reader = db.GetReader(SelectString);
            if (Items.Count>0) Items.Clear();
            while (reader.Read())
            {
                Items.Add(ParseRow(reader));
            }
            reader.Close();
            //db.Close();
        }
        
        public int Update (string query)
        {
            FillItems();
            int res =  db.GetCommand(query).ExecuteNonQuery(); 
            //db.Close();
            FillItems();
            return res;
        }

        public MyDBSet(MyDBUse db)
        {
            this.db = db;
            PropertyInfo[] pi = typeof(T).GetProperties();
            TableName = typeof(T).Name+"s";
            Fields.Clear();
            foreach (PropertyInfo p in pi)
            {
                Fields.Add(p.Name);
            }
        }
    }
}