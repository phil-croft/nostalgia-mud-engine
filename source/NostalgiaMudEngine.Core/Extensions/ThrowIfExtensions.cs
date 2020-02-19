using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Extensions
{
    public static class ThrowIfExtensions
    {
        public static T ThrowIfArgumentNull<T>(this T obj, string parameterName)
        {
            if (obj == null)
                throw new ArgumentNullException(parameterName);

            return obj;
        }
    }
}
