// Copyright (C),2005-2006 HandCoded Software Ltd.
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

using System;
using System.Xml;

using HandCoded.Xml;

namespace HandCoded.Xml
{
	/// <summary>
	/// The <b>Logic</b> class contains functions the make converting Clix
	/// based rules to C# easier. Rule set classes may be derived from this class
	/// to make access to the members more efficient.
	/// </summary>
	/// <remarks>Not every possible combination of arguments types is provided
	/// by all the functions - only those that are currently used in the validation
	/// code.</remarks>
	public abstract class Logic
	{
		/// <summary>
		/// Calculates the logical-not of the given predicate result value.
		/// </summary>
		/// <param name="value">The predicate value.</param>
		/// <returns>The logical-not of the predicate value.</returns>
		public static bool Not (bool value)
		{
			return (!value);
		}

		/// <summary>
		/// Calculates the logical-or of the given predicate result values.
		/// </summary>
		/// <param name="lhs">The first predicate value.</param>
		/// <param name="rhs">The second predicate value.</param>
		/// <returns>The logical-or of the predicate values.</returns>
		public static bool Or (bool lhs, bool rhs)
		{
			return (lhs || rhs);
		}

		/// <summary>
		/// Calculates the logical-and of the given predicate result values.
		/// </summary>
		/// <param name="lhs">The first predicate value.</param>
		/// <param name="rhs">The second predicate value.</param>
		/// <returns>The logical-and of the predicate values.</returns>
		public static bool And (bool lhs, bool rhs)
		{
			return (lhs && rhs);
		}

		/// <summary>
		/// Calculates the logical-implication of the given predicate result values.
		/// </summary>
		/// <param name="lhs">The first predicate value.</param>
		/// <param name="rhs">The second predicate value.</param>
		/// <returns>The logical-implication of the predicate values.</returns>
		public static bool Implies (bool lhs, bool rhs)
		{
			return (lhs ? rhs : true); 
		}

		/// <summary>
		/// Calculates the "if and only if" of the given predicate result values.
		/// </summary>
		/// <param name="lhs">The first predicate value.</param>
		/// <param name="rhs">The second predicate value.</param>
		/// <returns>The "if and only if" of the predicate values.</returns>
		public static bool Iff (bool lhs, bool rhs)
		{
			return (lhs == rhs);
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Determines if a referenced <see cref="XmlNode"/> exists.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> or <c>null</c>.</param>
		/// <returns><c>true</c> if the <see cref="XmlNode"/> exists.</returns>
		public static bool Exists (XmlNode node)
		{
			return (node != null);
		}

		/// <summary>
		/// Determines if a <see cref="XmlNodeList"/> is not empty.
		/// </summary>
		/// <param name="list">The <see cref="XmlNodeList"/> to examine.</param>
		/// <returns><c>true</c> if the <see cref="XmlNodeList"/> is not empty.</returns>
		public static bool Exists (XmlNodeList list)
		{
			return (list.Count > 0);
		}

		/// <summary>
		/// Determines the number of nodes in a <see cref="XmlNodeList"/>.
		/// </summary>
		/// <param name="list">The <see cref="XmlNodeList"/> to examine.</param>
		/// <returns>The number of nodes in the list.</returns>
		public static int Count (XmlNodeList list)
		{
			return (list.Count);
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a string.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static string String (XmlNode node)
		{
			return ((node != null) ? node.InnerText.Trim () : null);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as an integer.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static int Integer (XmlNode node)
		{
			try {
				return (int.Parse (node.InnerText));
			}
			catch (Exception) {
				;
			}
			return (0);
		}

		/// <summary>
		/// Returns the value of the given <see cref="XmlNode"/> as a decimal.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> containing the value.</param>
		/// <returns>The value of the node as a C# datatype.</returns>
		public static decimal Decimal (XmlNode node)
		{
			try {
                return (decimal.Parse (node.InnerText));
			}
			catch (Exception) {
				;
			}
			return (0.0M);
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

		// --------------------------------------------------------------------

		/// <summary>
		/// Determines if two nodes contain the same value.
		/// </summary>
		/// <param name="lhs">The first node to compare.</param>
		/// <param name="rhs">The second node to compare.</param>
		/// <returns><b>true</b> if the nodes contain the same value.</returns>
		public static bool Equal (XmlNode lhs, XmlNode rhs)
		{
			if ((lhs != null) && (rhs != null))
				return (Equal (String (lhs), String (rhs)));
			return (false);
		}

		/// <summary>
		/// Determines if the value of a <see cref="XmlNode"/> is the same as a
		/// given <see cref="string"/>.
		/// </summary>
		/// <param name="lhs">The <see cref="XmlNode"/> to compare.</param>
		/// <param name="rhs">The <see cref="string"/> value.</param>
		/// <returns><c>true</c> if the two values are equal.</returns>
		public static bool Equal (XmlNode lhs, string rhs)
		{
			return ((lhs != null) ? Equal (String (lhs), rhs) : false);
		}

		/// <summary>
		/// Determines if two <see cref="string"/> values have the same contents.
		/// </summary>
		/// <param name="lhs">The <see cref="string"/> to compare.</param>
		/// <param name="rhs">The <see cref="string"/> to compare with.</param>
		/// <returns><c>true</c> if the <see cref="string"/> values are equal.</returns>
		public static bool Equal (string lhs, string rhs)
		{
			return (lhs.Equals (rhs));
		}

		/// <summary>
		/// Determines if the value of a <see cref="XmlNode"/> is the same as a
		/// given <see cref="int"/>.
		/// </summary>
		/// <param name="lhs">The <see cref="XmlNode"/> to compare.</param>
		/// <param name="rhs">The <see cref="int"/> value.</param>
		/// <returns><c>true</c> if the two values are equal.</returns>
		public static bool Equal (XmlNode lhs, int rhs)
		{
			if (lhs != null) {
				try {
					return (Integer (lhs) == rhs);
				}
				catch (Exception) {
					return (false);
				}
			}
			return (false);
		}

		/// <summary>
		/// Compares two integer values.
		/// </summary>
		/// <param name="lhs">The first value.</param>
		/// <param name="rhs">The second value</param>
		/// <returns><c>true</c> if the two values are equal.</returns>
		public static bool Equal (int lhs, int rhs)
		{
			return (lhs == rhs);
		}

		/// <summary>
		/// Determines if two <see cref="decimal"/> values have the same contents.
		/// </summary>
		/// <param name="lhs">The <see cref="decimal"/> to compare.</param>
		/// <param name="rhs">The <see cref="decimal"/> to compare with.</param>
		/// <returns><c>true</c> if the <see cref="decimal"/> values are equal.</returns>
		public static bool Equal (decimal lhs, decimal rhs)
		{
			return (lhs.Equals (rhs));
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Determines if two nodes contain the different values.
		/// </summary>
		/// <param name="lhs">The first node to compare.</param>
		/// <param name="rhs">The second node to compare.</param>
		/// <returns><b>true</b> if the nodes contain the different values.</returns>
		public static bool NotEqual (XmlNode lhs, XmlNode rhs)
		{
			if ((lhs != null) && (rhs != null))
				return (NotEqual (lhs.InnerText.Trim (), rhs.InnerText.Trim ()));
			return (false);
		}

		/// <summary>
		/// Determines if the value of a <see cref="XmlNode"/> is not the same as a
		/// given <see cref="string"/>.
		/// </summary>
		/// <param name="lhs">The <see cref="XmlNode"/> to compare.</param>
		/// <param name="rhs">The <see cref="string"/> value.</param>
		/// <returns><c>true</c> if the two values are different.</returns>
		public static bool NotEqual (XmlNode lhs, string rhs)
		{
			return ((lhs != null) ? NotEqual (lhs.InnerText.Trim (), rhs) : false);
		}

		/// <summary>
		/// Determines if two <see cref="string"/> values are different.
		/// </summary>
		/// <param name="lhs">The <see cref="string"/> to compare.</param>
		/// <param name="rhs">The <see cref="string"/> to compare to.</param>
		/// <returns><c>true</c> if the two <see cref="string"/> values are different.</returns>
		public static bool NotEqual (string lhs, string rhs)
		{
			return (!lhs.Equals (rhs));
		}

		/// <summary>
		/// Determines if the value of a <see cref="XmlNode"/> is not the same as a
		/// given <see cref="int"/>.
		/// </summary>
		/// <param name="lhs">The <see cref="XmlNode"/> to compare.</param>
		/// <param name="rhs">The <see cref="int"/> value.</param>
		/// <returns><c>true</c> if the two values are equal.</returns>
		public static bool NotEqual (XmlNode lhs, int rhs)
		{
			if (lhs != null) {
				try {
					return (Int32.Parse (lhs.InnerText.Trim ()) != rhs);
				}
				catch (Exception) {
					return (false);
				}
			}
			return (false);
		}

		/// <summary>
		/// Determines if the value of a <see cref="XmlNode"/> is not the same as a
		/// given <see cref="decimal"/>.
		/// </summary>
		/// <param name="lhs">The <see cref="XmlNode"/> to compare.</param>
		/// <param name="rhs">The <see cref="int"/> value.</param>
		/// <returns><c>true</c> if the two values are equal.</returns>
		public static bool NotEqual (XmlNode lhs, decimal rhs)
		{
			if (lhs != null) {
				try {
					return (decimal.Parse (lhs.InnerText.Trim ()) != rhs);
				}
				catch (Exception) {
					return (false);
				}
			}
			return (false);
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Determines if the value of one node is less than another. If either
		/// node is null then the result is <b>false</b>.
		/// </summary>
		/// <param name="lhs">The first node.</param>
		/// <param name="rhs">The second node.</param>
		/// <returns><b>true</b> if the text value of the first node is less than
		/// that of the second.</returns>
		public static bool Less (XmlNode lhs, XmlNode rhs)
		{
			if ((lhs != null) && (rhs != null))
				return (Less (lhs.InnerText.Trim (), rhs.InnerText.Trim ()));
			return (false);
		}

		/// <summary>
		/// Determines if the value of a node is less than a given string. If the
		/// node is null then the result is <b>false</b>.
		/// </summary>
		/// <param name="lhs">The node to be tested.</param>
		/// <param name="rhs">A string value to compare with.</param>
		/// <returns><b>true</b> if the text value of the first node is less than
		/// the given string.</returns>
		public static bool Less (XmlNode lhs, string rhs)
		{
			return ((lhs != null) ? Less (lhs.InnerText.Trim (), rhs) : false);
		}

		/// <summary>
		/// Determines if the value of a <see cref="string"/> is lexiographically
		/// less than the value of another.
		/// </summary>
		/// <param name="lhs">The <see cref="string"/> to compare.</param>
		/// <param name="rhs">The <see cref="string"/> to compare with.</param>
		/// <returns><c>true</c> if the first value is less than the second.</returns>
		public static bool Less (string lhs, string rhs)
		{
			return (lhs.CompareTo (rhs) < 0);
		}

		/// <summary>
		/// Determines if the value of a <see cref="decimal"/> is less than
		/// the value of another.
		/// </summary>
		/// <param name="lhs">The <see cref="decimal"/> to compare.</param>
		/// <param name="rhs">The <see cref="decimal"/> to compare with.</param>
		/// <returns><c>true</c> if the first value is less than the second.</returns>
		public static bool Less (decimal lhs, decimal rhs)
		{
			return (lhs.CompareTo (rhs) < 0);
		}
		
		// --------------------------------------------------------------------

		/// <summary>
		/// Compares two <see cref="XmlNode"/> instances to determine if the
		/// first is larger than the second.
		/// </summary>
		/// <param name="lhs">The first node.</param>
		/// <param name="rhs">The second node.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool Greater (XmlNode lhs, XmlNode rhs)
		{
			if ((lhs != null) && (rhs != null))
				return (Greater (lhs.InnerText.Trim (), rhs.InnerText.Trim ()));
			return (false);
		}

		/// <summary>
		/// Compares two <see cref="string"/> instances to determine if the
		/// first is larger than the second.
		/// </summary>
		/// <param name="lhs">The first string.</param>
		/// <param name="rhs">The second string.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool Greater (string lhs, string rhs)
		{
			return (lhs.CompareTo (rhs) > 0);
		}
		
		/// <summary>
		/// Compares two <see cref="Int32"/> instances to determine if the
		/// first is larger than the second.
		/// </summary>
		/// <param name="lhs">The first int.</param>
		/// <param name="rhs">The second int.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool Greater (int lhs, int rhs)
		{
			return (lhs > rhs);
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Compares two <see cref="XmlNode"/> instances to determine if the
		/// first is equal to or smaller than the second.
		/// </summary>
		/// <param name="lhs">The first node.</param>
		/// <param name="rhs">The second node.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool LessOrEqual (XmlNode lhs, XmlNode rhs)
		{
			if ((lhs != null) && (rhs != null))
				return (LessOrEqual (lhs.InnerText.Trim (), rhs.InnerText.Trim ()));
			return (false);
		}

		/// <summary>
		/// Compares a <see cref="XmlNode"/> instance with a double to determine
		/// if the value it holds is smaller or the same.
		/// </summary>
		/// <param name="lhs">The node holding the value.</param>
		/// <param name="rhs">The value to compare against.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool LessOrEqual (XmlNode lhs, double rhs)
		{
			try {
				return (Double.Parse (lhs.InnerText) <= rhs);
			}
			catch (Exception) {
				return (false);
			}
		}

		/// <summary>
		/// Compares two <see cref="string"/> instances to determine if the
		/// first is equal to or smaller than the second.
		/// </summary>
		/// <param name="lhs">The first string.</param>
		/// <param name="rhs">The second string.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool LessOrEqual (string lhs, string rhs)
		{
			return (lhs.CompareTo (rhs) <= 0);
		}
		
		/// <summary>
		/// Compares two <see cref="decimal"/> instances to determine if the
		/// first is equal to or smaller than the second.
		/// </summary>
		/// <param name="lhs">The first decimal.</param>
		/// <param name="rhs">The second decimal.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool LessOrEqual (decimal lhs, decimal rhs)
		{
			return (lhs.CompareTo (rhs) <= 0);
		}
		
		// --------------------------------------------------------------------

		/// <summary>
		/// Compares two <see cref="XmlNode"/> instances to determine if the
		/// first is equal to or larger than the second.
		/// </summary>
		/// <param name="lhs">The first node.</param>
		/// <param name="rhs">The second node.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool GreaterOrEqual (XmlNode lhs, XmlNode rhs)
		{
			if ((lhs != null) && (rhs != null))
				return (GreaterOrEqual (lhs.InnerText.Trim (), rhs.InnerText.Trim ()));
			return (false);
		}

		/// <summary>
		/// Compares two <see cref="string"/> instances to determine if the
		/// first is equal to or larger than the second.
		/// </summary>
		/// <param name="lhs">The first string.</param>
		/// <param name="rhs">The second string.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool GreaterOrEqual (string lhs, string rhs)
		{
			return (lhs.CompareTo (rhs) >= 0);
		}
		
		/// <summary>
		/// Compares a <see cref="XmlNode"/> instance with a double to determine
		/// if the value it holds is larger or the same.
		/// </summary>
		/// <param name="lhs">The node holding the value.</param>
		/// <param name="rhs">The value to compare against.</param>
		/// <returns><c>true</c> if the first value is greater than the second.</returns>
		public static bool GreaterOrEqual (XmlNode lhs, double rhs)
		{
			try {
				return (Double.Parse (lhs.InnerText) >= rhs);
			}
			catch (Exception) {
				return (false);
			}
		}

		/// <summary>
		/// Constructs a <b>Logic</b> instance.
		/// </summary>
		protected Logic ()
		{ }
	}
}