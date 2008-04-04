// Copyright (C),2005-2008 HandCoded Software Ltd.
// All rights reserved.
//
// This software is licensed in accordance with the terms of the 'Open Source
// License (OSL) Version 3.0'. Please see 'license.txt' for the details.
//
// HANDCODED SOFTWARE LTD MAKES NO REPRESENTATIONS OR WARRANTIES ABOUT THE
// SUITABILITY OF THE SOFTWARE, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE, OR NON-INFRINGEMENT. HANDCODED SOFTWARE LTD SHALL NOT BE
// LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING
// OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.

using System.Text;
using System.Xml;

using HandCoded.Finance;

namespace HandCoded.Xml
{
	/// <summary>
	/// The <b>Types</b> class contains a set of functions for extracting
	/// native type values from XML <see cref="XmlElement"/> content text strings.
	/// </summary>
	/// <remarks>Much of this code has been refactored from the <see cref="Logic"/>
	/// class.</remarks>
	public abstract class Types
	{
		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a string.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static string ToString (XmlNode node)
		{
			return ((node != null) ? node.InnerText.Trim () : null);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a boolean.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static bool ToBool (XmlNode node)
		{
			try {
				return (bool.Parse (node.InnerText.Trim ()));
			}
			catch (System.Exception) {
				;
			}
			return (false);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as an integer.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static int ToInteger (XmlNode node)
		{
			try {
				return (int.Parse (node.InnerText.Trim ()));
			}
			catch (System.Exception) {
				;
			}
			return (0);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as an Double.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static double ToDouble (XmlNode node)
		{
			try {
				return (double.Parse (node.InnerText.Trim ()));
			}
			catch (System.Exception) {
				;
			}
			return (0.0);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a decimal.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static decimal ToDecimal (XmlNode node)
		{
			try {
                return (decimal.Parse (node.InnerText.Trim ()));
			}
			catch (System.Exception) {
				;
			}
			return (0.0M);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a <see cref="Date"/>.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static Date ToDate (XmlNode node)
		{
			try {
                return (Date.Parse (node.InnerText.Trim ()));
			}
			catch (System.Exception) {
				;
			}
			return (null);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a <see cref="DateTime"/>.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static DateTime ToDateTime (XmlNode node)
		{
			try {
                return (DateTime.Parse (node.InnerText.Trim ()));
			}
			catch (System.Exception) {
				;
			}
			return (null);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a <see cref="Time"/>.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static Time ToTime (XmlNode node)
		{
			try {
                return (Time.Parse (node.InnerText.Trim ()));
			}
			catch (System.Exception) {
				;
			}
			return (null);
		}

		/// <summary>
		/// Rounds a monetary decimal value to a given number of places. 
		/// </summary>
		/// <param name="value">The <see cref="decimal"/> to round.</param>
		/// <param name="places">The number of places required.</param>
		/// <returns>The rounded value.</returns>
		public static decimal Round (decimal value, int places)
		{
			return (decimal.Round (value, places));
		}

		/// <summary>
		/// Converts the value of the indicated <see cref="XmlNode"/> into a
		/// <see cref="decimal"/> value and then determines if it is positive.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be tested.</param>
		/// <returns><b>true</b> if the value is positive, <b>false</b> otherwise.</returns>
		public static bool IsPositive (XmlNode node)
		{
			return (IsPositive (ToDecimal (node)));
		}

		/// <summary>
		/// Determines if the supplied <see cref="decimal"/> value is positive.
		/// </summary>
		/// <param name="value">The <see cref="decimal"/> to test.</param>
		/// <returns><b>true</b> if the value is positive, <b>false</b> otherwise.</returns>
		public static bool IsPositive (decimal value)
		{
			return (value > 0);
		}

		/// <summary>
		/// Converts the value of the indicated <see cref="XmlNode"/> into a
		/// <see cref="decimal"/> value and then determines if it is negative.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be tested.</param>
		/// <returns><b>true</b> if the value is negative, <b>false</b> otherwise.</returns>
		public static bool IsNegative (XmlNode node)
		{
			return (IsNegative (ToDecimal (node)));
		}

		/// <summary>
		/// Determines if the supplied <see cref="decimal"/> value is negative.
		/// </summary>
		/// <param name="value">The <see cref="decimal"/> to test.</param>
		/// <returns><b>true</b> if the value is negative, <b>false</b> otherwise.</returns>
		public static bool IsNegative (decimal value)
		{
			return (value < 0);
		}

		/// <summary>
		/// Converts the value of the indicated <see cref="XmlNode"/> into a
		/// <see cref="decimal"/> value and then determines if it is non-negative.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be tested.</param>
		/// <returns><b>true</b> if the value is non-negative, <b>false</b> otherwise.</returns>
		public static bool IsNonNegative (XmlNode node)
		{
			return (IsNonNegative (ToDecimal (node)));
		}

		/// <summary>
		/// Determines if the supplied <see cref="decimal"/> value is non-negative.
		/// </summary>
		/// <param name="value">The <see cref="decimal"/> to test.</param>
		/// <returns><b>true</b> if the value is non-negative, <b>false</b> otherwise.</returns>
		public static bool IsNonNegative (decimal value)
		{
			return (value >= 0);
		}

		/// <summary>
		/// Constructs a <b>Types</b> instance.
		/// </summary>
		protected Types ()
		{ }
	}
}
