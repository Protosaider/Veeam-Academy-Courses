using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Other
{
	internal static class SExpressionHelper
    {
        #region Methods that helps as to pass property into function as fields with ref param.
        //So
        //              Method(ref _field) 
        //equals 
        //              Method(() => Property) 
        //where M is:  
        //              Method<T>(Expression<Func<T>> prop) 
        //              {
        //                  prop.GetPropertyValue()
        //              }

        /// <summary>
        /// Compiles an expression and gets the function return value
        /// </summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="lambda">Expression to compile</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }


        /// <summary>
        /// Sets the underlying properties value to the given value from an expression that contains the property
        /// </summary>
        /// <typeparam name="T">The type of value to set</typeparam>
        /// <param name="lambda">The expression</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            //Converts a lambda () => some.Property to some.Property
            var expression = (lambda as LambdaExpression).Body as MemberExpression;
            //Get the property information so we can set it
            var propertyInfo = (System.Reflection.PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();
            //Set the property value
            propertyInfo.SetValue(target, value);
        }
        #endregion
    }
}
