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
    public class Properties : IEnumerable<Property>, IEquatable<Properties>, ICollection<Property>
    {

        //never write directly, always use objectInstance[string] = 
        protected Dictionary<string, Property> _propertyDictionary
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
            foreach (var property in _propertyDictionary)
            {
                yield return property.Value;
            }
        }

        public Property this[string propertyName]
        {
            get
            {
                if (_propertyDictionary.TryGetValue(propertyName, out var val))
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

                bool propertiesContainsBanned = _propertyDictionary.ContainsKey(BannedProperty.Name) &&
                                                _propertyDictionary.ContainsValue(new BannedProperty());
                
                if (propertiesContainsBanned)
                {
                    value.SetDefaultBannedValue();
                } else if (value.GetType() == typeof(BannedProperty))
                {
                    foreach (var property in _propertyDictionary)
                    {
                        property.Value.SetDefaultBannedValue();
                    }
                }

                bool propertiesContainsAdmin = _propertyDictionary.ContainsKey(AdminProperty.Name) &&
                                               _propertyDictionary.ContainsValue(new AdminProperty(true));
                
                if (propertiesContainsAdmin && !propertiesContainsBanned)
                {
                    value.SetDefaultAdminValue();
                } else if (value.GetType() == typeof(AdminProperty))
                {
                    foreach (var property in _propertyDictionary)
                    {
                        property.Value.SetDefaultAdminValue();
                    }
                }

                if (!_propertyDictionary.ContainsKey(propertyName))
                {
                    ++Count;
                }
                
                _propertyDictionary[propertyName] = value;
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

            if (_propertyDictionary.ContainsKey(propertyName))
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

            if (_propertyDictionary.ContainsKey(propertyName))
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
                foreach (Property property in role.properties)
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
            return new List<string>(_propertyDictionary.Keys);
        }

        public bool Equals(Properties? other)
        {
            if (other == null)
            {
                return false;
            }
            
            try
            {
                foreach (var property in _propertyDictionary)
                {
                    if (!other._propertyDictionary.Contains(property))
                    {
                        return false;
                    }
                }
                
                foreach (var property in other._propertyDictionary)
                {
                    if (!_propertyDictionary.Contains(property))
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
            _propertyDictionary = new Dictionary<string, Property>();
        }

        public bool Contains(Property item)
        {
            foreach (var prop in _propertyDictionary)
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
            return _propertyDictionary.ContainsKey(itemName);
        }

        public bool ContainsPropertyCalled(string itemName)
        {
            return _propertyDictionary.ContainsKey(itemName);
        }

        public int Count { get; set; }

        public bool Remove(Property item)
        {
            if (item == null)
            {
                return false;
            }

            --Count;

            return _propertyDictionary.Remove(GetNameFromInstance(item));
        }

        public void CopyTo(Property[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach (var property in _propertyDictionary)
            {
                array[i] = property.Value;
                ++i;
            }
        }

        public bool IsReadOnly { get; } = false;
    }
}