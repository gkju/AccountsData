using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using AccountsData.Models.DataModels.Abstracts;
using AccountsData.Models.DataModels.Implementations.Properties;

namespace AccountsData.Models.DataModels
{
    public class Properties : IEquatable<Properties>, ICollection<Property>
    {
        
        public Dictionary<string, Property> PropertyDictionary
        {
            get;
            set;
        } = new Dictionary<string, Property>();

        public Properties()
        {
            
        }

        public Properties(params Property[] parameters)
        {
            foreach (var parameter in parameters)
            {
                InsertOrMerge(parameter);
            }
        }
        
        public Properties(List<Role> roles)
        {
            PopulateProperties(roles);
        }

        public Properties(Properties properties)
        {
            PopulateProperties(properties);
        }
        
        public IEnumerator<Property> GetEnumerator()
        {
            foreach (var property in PropertyDictionary)
            {
                yield return property.Value;
            }
        }

        public Property this[string propertyName]
        {
            get
            {
                if (PropertyDictionary.TryGetValue(propertyName, out var val))
                {
                    return val;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                value = (Property) value.Clone();
                //for safety reasons overrides the name
                propertyName = GetNameFromInstance(value);

                bool propertiesContainsBanned = PropertyDictionary.ContainsKey(BannedProperty.Name) &&
                                                PropertyDictionary.ContainsValue(new BannedProperty());
                
                if (propertiesContainsBanned)
                {
                    value.SetDefaultBannedValue();
                } else if (value.GetType() == typeof(BannedProperty))
                {
                    foreach (var property in PropertyDictionary)
                    {
                        property.Value.SetDefaultBannedValue();
                    }
                }

                bool propertiesContainsAdmin = PropertyDictionary.ContainsKey(AdminProperty.Name) &&
                                               PropertyDictionary.ContainsValue(new AdminProperty(true));
                
                if (propertiesContainsAdmin && !propertiesContainsBanned)
                {
                    value.SetDefaultAdminValue();
                } else if (value.GetType() == typeof(AdminProperty) && !propertiesContainsBanned)
                {
                    foreach (var property in PropertyDictionary)
                    {
                        property.Value.SetDefaultAdminValue();
                    }
                }

                if (!PropertyDictionary.ContainsKey(propertyName))
                {
                    ++Count;
                }
                
                PropertyDictionary[propertyName] = value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void InsertOrMerge(Property property)
        {
            property = (Property) property.Clone();

            string propertyName = GetNameFromInstance(property);

            if (ReferenceEquals(propertyName, null))
            {
                return;
            }

            if (PropertyDictionary.ContainsKey(propertyName))
            {
                this[propertyName] += property;
            }
            else
            {
                ++Count;
                this[propertyName] = property;
            }
        }

        public void Merge(Property property)
        {
            property = (Property) property.Clone();

            string propertyName = GetNameFromInstance(property);

            if (ReferenceEquals(propertyName, null))
            {
                return;
            }

            if (PropertyDictionary.ContainsKey(propertyName))
            {
                this[propertyName] += property;
            }
        }

        public static string GetNameFromInstance(Property property)
        {
            Type type = property.GetType();
            FieldInfo nameField = type.GetField("Name", BindingFlags.Public | BindingFlags.Static);
            return (string) nameField?.GetValue(null);
        }

        public void PopulateProperties(List<Role> roles)
        {
            foreach(var role in roles)
            {
                foreach (Property property in role.Properties)
                {
                    InsertOrMerge(property);
                }
            }
        }

        public void PopulateProperties(Properties properties)
        {
            foreach (Property property in properties)
            {
                InsertOrMerge(property);
            }
        }

        public List<string> GetKeys()
        {
            return new List<string>(PropertyDictionary.Keys);
        }

        public bool Equals(Properties? other)
        {
            if (other == null)
            {
                return false;
            }
            
            try
            {
                foreach (var property in PropertyDictionary)
                {
                    if (!other.PropertyDictionary.Contains(property))
                    {
                        return false;
                    }
                }
                
                foreach (var property in other.PropertyDictionary)
                {
                    if (!PropertyDictionary.Contains(property))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            Type t = obj.GetType();

            try
            {
                Properties otherObj = (Properties) obj;
                return Equals(otherObj);
            }
            catch
            {
                return false;
            }
        }

        public void Add(Property item)
        {
            if (item == null)
            {
                return;
            }
            InsertOrMerge(item);
        }

        public void Clear()
        {
            Count = 0;
            PropertyDictionary = new Dictionary<string, Property>();
        }

        public bool Contains(Property item)
        {
            foreach (var prop in PropertyDictionary)
            {
                if (prop.Value == item)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsPropertyCalled(Property item)
        {
            if (item == null)
            {
                return false;
            }

            string itemName = GetNameFromInstance(item);
            return PropertyDictionary.ContainsKey(itemName);
        }

        public bool ContainsPropertyCalled(string itemName)
        {
            return PropertyDictionary.ContainsKey(itemName);
        }

        public int Count { get; set; }

        public bool Remove(Property item)
        {
            if (item == null)
            {
                return false;
            }

            --Count;

            return PropertyDictionary.Remove(GetNameFromInstance(item));
        }

        public void CopyTo(Property[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach (var property in PropertyDictionary)
            {
                array[i] = property.Value;
                ++i;
            }
        }

        public bool IsReadOnly { get; } = false;
    }
}