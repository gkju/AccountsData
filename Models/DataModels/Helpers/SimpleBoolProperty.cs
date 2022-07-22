﻿using System;
using System.ComponentModel;
using AccountsData.Models.DataModels.Abstracts;

namespace AccountsData.Models.DataModels.Helpers
{
    public class SimpleBoolProperty : Property, IEquatable<SimpleBoolProperty>
    {
        public bool Data { get; set; }

        public override void SetDefaultBannedValue()
        {
            Data = false;
        }
        
        public override void SetDefaultAdminValue()
        {
            
        }
        
        public SimpleBoolProperty()
        {
            Data = false;
        }
        
        public SimpleBoolProperty(Property property )
        {
            try
            {
                Data = ((SimpleBoolProperty) property).Data;
            }
            catch
            {
                Data = false;
            }
        }

        public SimpleBoolProperty(SimpleBoolProperty property)
        {
            Data = property.Data;
        }
        
        public override Property MakeACombination(Property _parentProperty)
        {
            SimpleBoolProperty parentProperty = new SimpleBoolProperty((SimpleBoolProperty) _parentProperty);
            //DO NOT CHANGE TO A COPY CONSTRUCTOR TO AVOID CHANGING TYPE
            var copyOfSelf = (SimpleBoolProperty) Clone();
            
            if (parentProperty.Data)
            {
                copyOfSelf.Data = parentProperty.Data;
            }

            return copyOfSelf;
        }

        public static implicit operator bool(SimpleBoolProperty property)
        {
            if (ReferenceEquals(property, null))
            {
                return false;
            }
            
            return property.Data;
        }

        public static bool operator ==(SimpleBoolProperty a, SimpleBoolProperty b)
        {
            if (CheckReferenceEquality(a, b) == ReferenceEquality.SameObjects)
            {
                return true;
            } else if (CheckReferenceEquality(a, b) == ReferenceEquality.OneIsNull)
            {
                return false;
            } 
            
            return a.Data == b.Data && a.GetType() == b.GetType();
        }

        public static bool operator !=(SimpleBoolProperty a, SimpleBoolProperty b)
        {
            return !(a == b);
        }
        
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            Type t = obj.GetType();
            
            try
            {
                return Data == ((SimpleBoolProperty) obj).Data && GetType() == obj.GetType();
            }
            catch
            {
                return false;
            }
        }

        public bool Equals(SimpleBoolProperty? obj)
        {
            return Equals((object?) obj);
        }
    }
}