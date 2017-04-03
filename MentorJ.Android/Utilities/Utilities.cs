using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace AndroidApp
{

    
    public class Serializer
    {

        public static void Clone<T>(T fromObj, T toObj)
        {
            try
            {
                PropertyInfo[] properties1 = fromObj.GetType().GetProperties();
                PropertyInfo[] properties2 = toObj.GetType().GetProperties();
                foreach (PropertyInfo p1 in properties1)
                {
                    if (p1.Name.ToUpper() == "ID") continue;
                    if (p1.Name.StartsWith("n_")) continue;
                    foreach (PropertyInfo p2 in properties2)
                    {
                        if (p1.Name == p2.Name)
                        {
                            if (p1.CanRead && p2.CanWrite)
                            {
                                p2.SetValue(toObj, p1.GetValue(fromObj, null), null);
                            }
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}