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
	/// The <b>WeekendDelegate</b> delegate is used to access a function that
	/// tests a <see cref="Date"/> to see if it falls on a weekend.
	/// </summary>
	public delegate bool WeekendDelegate (Date date);

	/// <summary>
	/// The <b>DelegatedWeekend</b> class provides a implementation of the
	/// abstract <see cref="Weekend"/> class that allows the use of
	/// delegates.
	/// </summary>
	public sealed class DelegatedWeekend : Weekend
	{
		/// <summary>
		/// Constructs a <b>DelegatedWeekend</b> instance that will use the
		/// indicated <b>WeekendDelegate</b> to test dates. 
		/// </summary>
		/// <param name="name">The name used to reference this instance.</param>
		/// <param name="function">A <see cref="WeekendDelegate"/> for the test function.</param>
		public DelegatedWeekend (string name, WeekendDelegate function)
			: base (name)
		{
			this.function = function;
		}

		/// <summary>
		/// Determines if the given <see cref="Date"/> falls on a weekend.
		/// </summary>
		/// <param name="date">The <see cref="Date"/> to check.</param>
		/// <returns><c>true</c> if the date falls on a weekend, <c>false</c>
		/// otherwise.</returns>
		public override bool IsWeekend (Date date)
		{
			return (function (date));
		}

		/// <summary>
		/// The delegate used to test a name.
		/// </summary>
		private readonly WeekendDelegate function;
	}
}