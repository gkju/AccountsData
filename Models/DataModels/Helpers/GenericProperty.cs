using System;
using System.Collections.Generic;
using AccountsData.Models.DataModels.Abstracts;

namespace AccountsData.DataModels.Helpers
{
    public abstract class GenericProperty<T> : Property where T : IEquatable<T>
    {
        public T Data { get; }

        public GenericProperty(GenericProperty<T> property)
        {
            Data = property.Data;
        }

        public static bool operator ==(GenericProperty<T> a, GenericProperty<T> b)
        {
            if (CheckReferenceEquality(a, b) == ReferenceEquality.SameObjects)
            {
                return true;
            } else if (CheckReferenceEquality(a, b) == ReferenceEquality.OneIsNull)
            {
                return false;
            }

            return Comparer<T>.Default.Compare(a.Data, b.Data) >= 0 && a.GetType() == b.GetType();
        }

        public static bool operator !=(GenericProperty<T> a, GenericProperty<T> b)
        {
            return !(a == b);
        }
        
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            try
            {
                return Comparer<T>.Default.Compare(Data, ((GenericProperty<T>) obj).Data) >= 0  && GetType() == obj.GetType();
            }
            catch
            {
                return false;
            }
        }

        public bool Equals(GenericProperty<T> obj)
        {
            return Equals((object) obj);
        }
    }
}