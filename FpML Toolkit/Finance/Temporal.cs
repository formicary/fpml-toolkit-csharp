// Copyright (C),2005-2007 HandCoded Software Ltd.
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
using System.Text;

namespace HandCoded.Finance
{
	/// <summary>
	/// The <b>Temporal</b> class provides storage for the optional timezone
	/// offset portion of <see cref="Date"/>, <see cref="DateTime"/> and
	/// <see cref="Time"/> instances.
	/// </summary>
	public abstract class Temporal
	{
		/// <summary>
		/// Contains the implict <see cref="TimeZone"/>.
		/// </summary>
		public static TimeZone ImplicitTimeZone {
			get {
				return (implicitTimeZone);
			}
			set {
				implicitTimeZone = value;
			}
		}

		/// <summary>
		/// Provides access to the timezone offset if any was specified.
		/// </summary>
		public TimeZone TimeZone {
			get {
				return (timeZone);
			}
		}

		/// <summary>
		/// Holds the <see cref="TimeZone"/> if present.
		/// </summary>
		protected readonly TimeZone	timeZone;

		/// <summary>
		/// Constructs a <b>Temporal</b> instance having either a UTC
		/// time or no timezone depending on the argument.
		/// </summary>
		/// <param name="timeZone">The <see cref="TimeZone"/> value or <b>null</b>.</param>
		protected Temporal (TimeZone timeZone)
		{
			this.timeZone = timeZone;
		}

		/// <summary>
		/// Determines if a character is a digit (e.g. 0..9). Used by parsing
		/// methods in derived classes.
		/// </summary>
		/// <param name="ch">The character to be tested.</param>
		/// <returns><b>true</b> if the character is a decimal digit.</returns>
		protected static bool IsDigit (char ch)
		{
			return (('0' <= ch) && (ch <= '9'));
		}
				
		/// <summary>
		/// Holds the default <see cref="TimeZone"/> for the executing application.
		/// </summary>
		private static TimeZone		implicitTimeZone = new TimeZone ();
	}
}