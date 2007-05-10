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

namespace HandCoded.Finance
{
	/// <summary>
	/// The <b>DateRollDelegate</b> delegate is used to access a function that
	/// adjusts a <see cref="Date"/> to see if it falls on a non-business day.
	/// </summary>
	public delegate Date DateRollDelegate (Calendar calendar, Date date);

	/// <summary>
	/// The <b>DelegatedDateRoll</b> class provides a implementation of the
	/// abstract <see cref="DateRoll"/> class that allows the use of 
	/// delegates.
	/// </summary>
	public class DelegatedDateRoll : DateRoll
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="function"></param>
		public DelegatedDateRoll (string name, DateRollDelegate function)
			: base (name)
		{
			this.function = function;
		}

		/// <summary>
		/// Adjusts a <see cref="Date"/> which falls on a holiday within the
		/// indicated <see cref="Calendar"/> to an appropriate business day.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		public override Date Adjust (Calendar calendar, Date date)
		{
			return (function (calendar, date));
		}

		/// <summary>
		/// The delegate used to adjust a date.
		/// </summary>
		private readonly DateRollDelegate	function;
	}
}