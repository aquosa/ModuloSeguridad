using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Based on code from http://geekswithblogs.net/Madman/archive/2008/06/27/faster-reflection-using-expression-trees.aspx
    /// Also check http://thecodeslinger.wordpress.com/2007/12/03/dynamic-object-instantiation-performance-part-ii/
    /// Modified to check for public getter and setter
    /// </summary>
    public class FastProperty
    {
        public PropertyInfo Property { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }

        public Func<object, object> GetDelegate;
        public Action<object, object> SetDelegate;

        public FastProperty(PropertyInfo property)
        {
            this.Property = property;
            InitializeGet();
            InitializeSet();
        }


        private void InitializeSet()
        {
            var setMethod = this.Property.GetSetMethod();
            CanWrite = (null != setMethod);
            if (!CanWrite) return;

            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");
            // value as T is slightly faster than (T)value, so if it's not a value type, use that
            UnaryExpression instanceCast = (!this.Property.DeclaringType.IsValueType) ? Expression.TypeAs(instance, this.Property.DeclaringType) : Expression.Convert(instance, this.Property.DeclaringType);
            UnaryExpression valueCast = (!this.Property.PropertyType.IsValueType) ? Expression.TypeAs(value, this.Property.PropertyType) : Expression.Convert(value, this.Property.PropertyType);
            this.SetDelegate = Expression.Lambda<Action<object, object>>(Expression.Call(instanceCast, setMethod, valueCast), new ParameterExpression[] { instance, value }).Compile();
        }


        private void InitializeGet()
        {
            var getMethod = this.Property.GetGetMethod();
            CanRead = (null != getMethod);
            if (!CanRead) return;

            var instance = Expression.Parameter(typeof(object), "instance");
            UnaryExpression instanceCast = (!this.Property.DeclaringType.IsValueType) ? Expression.TypeAs(instance, this.Property.DeclaringType) : Expression.Convert(instance, this.Property.DeclaringType);
            this.GetDelegate = Expression.Lambda<Func<object, object>>(Expression.TypeAs(Expression.Call(instanceCast, getMethod), typeof(object)), instance).Compile();
        }


        public object Get(object instance)
        {
            return this.GetDelegate(instance);
        }


        public void Set(object instance, object value)
        {
            this.SetDelegate(instance, value);
        }
    }
}
