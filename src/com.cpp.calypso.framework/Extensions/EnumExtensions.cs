using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework.Extensions
{
	 

	public static class EnumExtensions
	{
		/// <summary>
		/// Converts string to enum value (opposite to Enum.ToString()).
		/// </summary>
		/// <typeparam name="T">Type of the enum to convert the string into.</typeparam>
		/// <param name="s">string to convert to enum value.</param>
		public static T ToEnum<T>(this string s) where T : struct
		{
			T newValue;
			return Enum.TryParse(s, out newValue) ? newValue : default(T);
		}
	}
}
