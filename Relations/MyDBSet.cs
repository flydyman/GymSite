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
                return Items;
            }
        }
        public T GetLast {
            get{
                return GetList.Last();
            }
        }
        string TableName;
        List<string> Fields = new List<string>();
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
        public T ParseRow(DataRow row)
        {
            T res = new T();
            var pi = typeof(T).GetProperties();
            foreach (string f in Fields)
            {
                var p = pi.FirstOrDefault(x =>
                    x.Name == f);
                //var row = reader.
                p.SetValue(res, row[f], default);
            }
            return res;
        }

        /// <summary>
        /// Парсит обьект в object[]
        /// </summary>
        public object[] ParseObject(T obj)
        {
            var pi = typeof(T).GetProperties();
            object[] row = new object[pi.Length];
            for (int i = 0; i< row.Length; i++)
            {
                row[i] = pi[i].GetValue(obj, null);
            }
            return row;
        }
        public int Add (T obj)
        {   
            return 0;
        }
        public int GetID(T obj)
        {
            return 0;
        }
        public T GetCurrent()
        {
            return Items.Last();
        }
        public T GetObject(int id)
        {
            return Items[id];
        }
        public int Remove (T obj)
        {
            return 0;
        }
        public int Update( T obj)
        {
            foreach (string f in Fields)
            {
                var p = typeof(T).GetProperties().FirstOrDefault(x =>
                    x.Name == f);
                //tmp[f] = p.GetValue(obj, null);
                
            }
            
            return 0;
        }
        private void FillItems()
        {
            // 1. Fill fields
            PropertyInfo[] pi = typeof(T).GetProperties();
            Fields.Clear();
            foreach (PropertyInfo p in pi)
            {
                Fields.Add(p.Name);
            }
            // 2. Fill items
            try
            {
                DbDataReader reader = db.GetReader(SelectString);
                DataTable table = reader.GetSchemaTable();
                foreach (DataRow row in table.Rows)
                {
                    Items.Add(ParseRow(row));
                }
                reader.Close();
            } finally {
                db.Close();
            }
        }
        private void FillDB()
        {
            try{
                DbDataReader reader = db.GetReader(SelectString);

                reader.Close();
            } finally {
                db.Close();
            }
        }
        public MyDBSet(MyDBUse DataBase)
        {
            db = DataBase;
            
        }
        private void Test()
        {
            Console.WriteLine($"Table Name: {TableName}");
            if (Fields.Count>0)
            {
                foreach (string f in Fields)
                {
                    Console.Write($"\t {f}");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Rows: {Items.Count}");
        }
    }
}