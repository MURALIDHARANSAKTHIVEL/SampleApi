using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;

namespace ProjectName.Contract.Classes
{
    public static class Extension
    {

        public static List<T> ToCustomList<T>(this DataTable dataTable) where T : class, new()
        {
            try
            {
                List<T> entitiesList = new List<T>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var row = dataTable.Rows[i];

                    T entity = new T();
                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = entity.GetType().GetProperty(prop.Name);
                            bool isNullable = (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null);
                            if (row[prop.Name] != DBNull.Value) // Don't copy over DBNull
                            {
                                row[prop.Name] = isNullable ? (row[prop.Name] is DBNull) ? null : Convert.ChangeType(row[prop.Name], Nullable.GetUnderlyingType(propertyInfo.PropertyType)) :
                                    Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType);
                                // Set the value of the property with the value from the db
                                propertyInfo.SetValue(entity, row[prop.Name], null);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    entitiesList.Add(entity);
                }
                return entitiesList;
            }
            catch
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns>List with generic objects</returns>

        public static T ToCustomEntity<T>(this DataTable dataTable) where T : class, new()
        {
            try
            {

                T entity = null;

                if (dataTable.Rows.Count > 0)
                {
                    entity = new T();
                    var row = dataTable.Rows[0];
                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = entity.GetType().GetProperty(prop.Name);
                            bool isNullable = (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null);
                            if (row[prop.Name] != DBNull.Value) // Don't copy over DBNull
                            {
                                row[prop.Name] = isNullable ? (row[prop.Name] is DBNull) ? null : Convert.ChangeType(row[prop.Name], Nullable.GetUnderlyingType(propertyInfo.PropertyType)) :
                                    Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType);
                                // Set the value of the property with the value from the db
                                propertyInfo.SetValue(entity, row[prop.Name], null);

                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public static List<T> ToCustomList<T>(this DbDataReader dr) where T : new()
        {
            List<T> entitiesList = null;
            var entity = typeof(T);
            var propDict = new Dictionary<string, PropertyInfo>();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    entitiesList = new List<T>();
                    var property = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    propDict = property.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (dr.Read())
                    {
                        T newObject = new T();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            if (propDict.ContainsKey(dr.GetName(i).ToUpper()))
                            {
                                var Info = propDict[dr.GetName(i).ToUpper()];
                                if ((Info != null) && Info.CanWrite)
                                {
                                    var val = dr.GetValue(i);
                                    Info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                                }
                            }
                        }
                        entitiesList.Add(newObject);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entitiesList;
        }
        /// <Summary>
        /// Map data from DataReader to an object
        /// </Summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="dr">Data Reader</param>
        /// <returns>Object having data from Data Reader</returns>

        public static T ToCustomEntity<T>(this DbDataReader dr) where T : class, new()
        {

            var entity = typeof(T);
            T retVal = null;
            var propDict = new Dictionary<string, PropertyInfo>();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    retVal = new T();
                    var property = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    propDict = property.ToDictionary(p => p.Name.ToUpper(), p => p);
                    dr.Read();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (propDict.ContainsKey(dr.GetName(i).ToUpper()))
                        {
                            var Info = propDict[dr.GetName(i).ToUpper()];
                            if ((Info != null) && Info.CanWrite)
                            {
                                var val = dr.GetValue(i);
                                Info.SetValue(retVal, (val == DBNull.Value) ? null : val, null);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retVal;
        }

        /// <Summary>
        /// Map data from OracleDataReader to GenericList
        /// </Summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="dr">Oracle Data Reader</param>
        /// <returns>Object having data from Data Reader</returns>
        public static List<T> OracleToCustomList<T>(this OracleDataReader dr) where T : new()
        {
            List<T> entitiesList = null;
            var entity = typeof(T);
            var propDict = new Dictionary<string, PropertyInfo>();
            try
            {
                if (dr != null && dr.HasRows)
                {
                    entitiesList = new List<T>();
                    var property = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    propDict = property.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (dr.Read())
                    {
                        T newObject = new T();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            if (propDict.ContainsKey(dr.GetName(i).ToUpper()))
                            {
                                var Info = propDict[dr.GetName(i).ToUpper()];
                                if ((Info != null) && Info.CanWrite)
                                {
                                    var val = dr.GetValue(i);
                                    Info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                                }
                            }
                        }
                        entitiesList.Add(newObject);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entitiesList;
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return string.Empty; // could also return string.Empty
        }

    }
}