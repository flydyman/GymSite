using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace GymSite
{
    public struct ObjField{
        public string FieldName;
        public string FieldValue;
    }
    public static class Commons
    {
        public static bool IsWordExists(string Statement, string Word)
        {
            return Statement.ToUpper().Contains(Word.ToUpper());
        }

        public static List<ObjField> GetObjFields(object O)
        {
            List<ObjField> res = new List<ObjField>();
            FieldInfo[] fi = O.GetType().GetFields();
            foreach (FieldInfo i in fi)
            {
                res.Add(new ObjField{
                    FieldName = i.Name,
                    FieldValue = i.GetRawConstantValue().ToString()
                });
            }
            return res;
        }
    }
}