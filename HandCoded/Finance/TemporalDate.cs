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
	/// The <b>TemporalDate</b> class defines a set of weekday constants
	/// needed by both the <see cref="Date"/> and <see cref="DateTime"/> classes.
	/// </summary>
	public abstract class TemporalDate : Temporal
	{
		/// <summary>
		/// A constant value indicating the weekday Sunday.
		/// </summary>
		public const int			SUNDAY		= 1;
	
		/// <summary>
		/// A constant value indicating the weekday Monday.
		/// </summary>
		public const int			MONDAY		= 2;
	
		/// <summary>
		/// A constant value indicating the weekday Tuesday.
		/// </summary>
		public const int			TUESDAY		= 3;
	
		/// <summary>
		/// A constant value indicating the weekday Wednesday.
		/// </summary>
		public const int			WEDNESDAY	= 4;
	
		/// <summary>
		/// A constant value indicating the weekday Thursday.
		/// </summary>
		public const int			THURSDAY	= 5;
	
		/// <summary>
		/// A constant value indicating the weekday Friday.
		/// </summary>
		public const int			FRIDAY		= 6;
	
		/// <summary>
		/// A constant value indicating the weekday Saturday.
		/// </summary>
		public const int			SATURDAY	= 7;

		/// <summary>
		/// Constructs a <b>TemporalDate</b> instance having either a UTC
		/// time or no timezone depending on the argument.
		/// </summary>
		/// <param name="timeZone">The <see cref="TimeZone"/> value or <b>null</b>.</param>
		protected TemporalDate (TimeZone timeZone)
			: base (timeZone)
		{ }
	}
}