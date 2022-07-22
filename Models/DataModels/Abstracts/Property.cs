﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Metadata;

namespace AccountsData.Models.DataModels.Abstracts
{
    public enum ReferenceEquality
    {
        SameObjects= 0,
        OneIsNull = 1,
        DifferentObjects = 2
    }
    public abstract class Property : ICloneable
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public static string Name { get; set; } = "DefaultPropertyName";

        //MAKE A COMBINATION SHOULD RETURN THE PREFERRED (HIGHER) VALUE, IT MUSTN'T MODIFY THE PROPERTY ITSELF, JUST MAKE A COMBINATION
        public abstract Property MakeACombination(Property parentProperty);
        
        public abstract void SetDefaultBannedValue();
        
        public abstract void SetDefaultAdminValue();

        public static ReferenceEquality CheckReferenceEquality(Property a, Property b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return ReferenceEquality.SameObjects;
            } else if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return ReferenceEquality.OneIsNull;
            }

            return ReferenceEquality.DifferentObjects;
        }
        public static Property operator+(Property a, Property b)
        {
            a = (Property) a.Clone();
            
            Property c = a.MakeACombination(b);

            return c;
        }

        //requires further testing after all properties are implemented
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}