using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MarketPlacePraktuka.HeplClasses
{
    public class Filter<TSource, TProperty> : IFilter<TSource> where TProperty : class
                                                               where TSource : class
    {
        private readonly List<TProperty> _selectedValues = new List<TProperty>();
        private readonly Func<TSource, TProperty> _propertyGetter;

        public IEnumerable<object> PossibleValues => App.DB.Set<TProperty>().Local;

        public Filter(Func<TSource, TProperty> propertyGetter)
        {
            _propertyGetter = propertyGetter;

            App.DB.Set<TSource>().Load();
            App.DB.Set<TProperty>().Load();
        }

        public void Add(object value)
        {
            if (value is TProperty propertyValue)
            _selectedValues.Add(propertyValue);
        }

        public void Remove(object value)
        {
            if (value is TProperty propertyValue)
                _selectedValues.Remove(propertyValue);
        }

        public bool IsAccepted(TSource source) =>
            !_selectedValues.Any() || _selectedValues.Contains(_propertyGetter(source));
    }

    public interface IFilter<TSource>
    {
        IEnumerable<object> PossibleValues { get; }

        void Add(object value);
        void Remove(object value);

        bool IsAccepted(TSource source);
    }
}
