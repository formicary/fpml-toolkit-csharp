// Copyright (C),2005 HandCoded Software Ltd.
// All rights reserved.
//
// This software is the confidential and proprietary information of HandCoded
// Software Ltd. ("Confidential Information").  You shall not disclose such
// Confidential Information and shall use it only in accordance with the terms
// of the license agreement you entered into with HandCoded Software.
//
// HANDCODED SOFTWARE MAKES NO REPRESENTATIONS OR WARRANTIES ABOUT THE
// SUITABILITY OF THE SOFTWARE, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE, OR NON-INFRINGEMENT. HANDCODED SOFTWARE SHALL NOT BE
// LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING
// OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.

using System;
using System.Collections;

namespace HandCoded.Finance
{
	/// <summary>
	/// <b>Period</b> defines a set of time units (day, week, month, etc.)
	/// </summary>
	public sealed class Period
	{
		/// <summary>
		/// The extent set of all <b>Period</b> instances.
		/// </summary>
		private static Hashtable	extent	= new Hashtable ();

		/// <summary>
		/// A <b>Period</b> instance representing a day.
		/// </summary>
		public static readonly Period	DAY			=	new Period ("D");

		/// <summary>
		/// A <b>Period</b> instance representing a week.
		/// </summary>
		public static readonly Period	WEEK		=	new Period ("W");

		/// <summary>
		/// A <b>Period</b> instance representing a month.
		/// </summary>
		public static readonly Period	MONTH		=	new Period ("M");

		/// <summary>
		/// A <b>Period</b> instance representing a year.
		/// </summary>
		public static readonly Period	YEAR		=	new Period ("Y");

		/// <summary>
		/// A <b>Period</b> instance representing a term.
		/// </summary>
		public static readonly Period	TERM		=	new Period ("T");

		/// <summary>
		/// Attempts to locate a <b>Period</b> instance having the given code
		/// value.
		/// </summary>
		/// <param name="code">The required code value.</param>
		/// <returns>The matching <b>Period</b> instance or <c>null</c> if none
		/// was found.</returns>
		public static Period ForCode (string code)
		{
			return (extent [code] as Period);
		}

		/// <summary>
		/// Contains the code value.
		/// </summary>
		public string Code {
			get {
				return (code);
			}
		}

		/// <summary>
		/// Determines the hash value for this instance based on the value of
		/// the code string.
		/// </summary>
		/// <returns>The hash value of the instance.</returns>
		public override int GetHashCode ()
		{
			return (code.GetHashCode ());	
		}

		/// <summary>
		/// Determines if the value of this instance equals that of another.
		/// </summary>
		/// <param name="other">The <see cref="Object"/> to compare with.</param>
		/// <returns>A <see cref="bool"/> indicating if the two instances had the
		/// same code value.</returns>
		public override bool Equals (object other)
		{
			return ((other is Period) && code.Equals ((other as Period).code));
		}

		/// <summary>
		/// Returns the value of the code.
		/// </summary>
		/// <returns>The code value.</returns>
		public override string ToString ()
		{
			return (code);
		}

		/// <summary>
		/// The period code.
		/// </summary>
		private readonly string		code;

		/// <summary>
		/// Constructs a <b>Period</b> with given code.
		/// </summary>
		/// <param name="code">The period code.</param>
		private Period (string code)
		{
			extent [this.code = code] = this;
		}	
	}
}