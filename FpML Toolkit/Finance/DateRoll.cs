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

using System;
using System.Collections.Generic;

namespace HandCoded.Finance
{
	/// <summary>
	/// Instances of the <b>DateRoll</b> class carry out financial date
	/// adjustments. A <b>DateRoll</b> instance will apply a particular
	/// adjustment rule to a given <see cref="Date"/> using a business
	/// day <see cref="Calendar"/> to skip non-working days.
	/// </summary>
	public abstract class DateRoll
	{
		/// <summary>
		/// The set of all existing <b>DateRoll</b> instances.
		/// </summary>
		private static Dictionary<string, DateRoll>	extent
            = new Dictionary<string, DateRoll> ();

		/// <summary>
		/// A <b>DateRoll</b> that performs no adjustment.
		/// </summary>
		public static readonly DateRoll NONE
			= new DelegatedDateRoll ("NONE", new DateRollDelegate (None));

		/// <summary>
		/// A <b>DateRoll</b> that performs no adjustment.
		/// </summary>
		public static readonly DateRoll FOLLOWING
			= new DelegatedDateRoll ("FOLLOWING", new DateRollDelegate (Following));

		/// <summary>
		/// A <b>DateRoll</b> that performs no adjustment.
		/// </summary>
		public static readonly DateRoll PRECEDING
			= new DelegatedDateRoll ("PRECEDING", new DateRollDelegate (Preceding));

		/// <summary>
		/// A <b>DateRoll</b> that performs no adjustment.
		/// </summary>
		public static readonly DateRoll MODFOLLOWING
			= new DelegatedDateRoll ("MODFOLLOWING", new DateRollDelegate (ModFollowing));

		/// <summary>
		/// A <b>DateRoll</b> that performs no adjustment.
		/// </summary>
		public static readonly DateRoll MODPRECEDING
			= new DelegatedDateRoll ("MODPRECEDING", new DateRollDelegate (ModPreceding));

		/// <summary>
		/// A <b>DateRoll</b> that performs no adjustment.
		/// </summary>
		public static readonly DateRoll WEEKEND
			= new DelegatedDateRoll ("WEEKEND", new DateRollDelegate (Weekend));

		/// <summary>
		/// The symbolic name for the <b>DateRoll</b>.
		/// </summary>
		public string Name {
			get {
				return (name);
			}
		}

		/// <summary>
		/// Attempts to locate a <b>DateRoll</b> instance with the given name.
		/// </summary>
		/// <param name="name">The required <b>DateRoll</b> name.</param>
		/// <returns>A reference to the corresponding <b>DateRoll</b> instance or
		/// <c>null</c> if no match was found.</returns>
		public static DateRoll ForName (string name)
		{
			return (extent.ContainsKey (name) ? extent [name] : null);
		}

		/// <summary>
		/// Adjusts a <see cref="Date"/> which falls on a holiday within the
		/// indicated <see cref="Calendar"/> to an appropriate business day.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		public abstract Date Adjust (Calendar calendar, Date date);

		/// <summary>
		/// Constructs a <b>DateRoll</b> instance and adds it to the extent
		/// set indexed by its symbolic name.
		/// </summary>
		/// <param name="name">The symbolic name for this instance.</param>
		protected DateRoll (string name)
		{
			extent [this.name = name] = this;
		}

		/// <summary>
		/// The symbolic name for this instance.
		/// </summary>
		private readonly string		name;

		/// <summary>
		/// A dummy adjustment that performs no change to the date.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		private static Date None (Calendar calendar, Date date)
		{
			return (date);
		}

		/// <summary>
		/// Adjusts a <see cref="Date"/> to the next business day if it falls on
		/// a holiday.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		private static Date Following (Calendar calendar, Date date)
		{
			while (!calendar.IsBusinessDay (date))
				date = date.PlusDays (+1);
			return (date);
		}

		/// <summary>
		/// Adjusts a <see cref="Date"/> to the preceding business day if it falls
		/// on a holiday.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		private static Date Preceding (Calendar calendar, Date date)
		{
			while (!calendar.IsBusinessDay (date))
				date = date.PlusDays (-1);
			return (date);
		}

		/// <summary>
		/// Adjusts a <see cref="Date"/> to the next business day if it falls
		/// on a holiday unless that would move the date into the next month
		/// in which case it is rolled to a preceding date.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		private static Date ModFollowing (Calendar calendar, Date date)
		{
			Date			result = Following (calendar, date);

			if (date.Month != result.Month)
				result = Preceding (calendar, date);
			return (result);
		}

		/// <summary>
		///	Adjusts a <see cref="Date"/> to the previous business day if it
		///	falls on a holiday unless that would move the date into the
		///	previous month in which case it is rolled to a following date.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		private static Date ModPreceding (Calendar calendar, Date date)
		{
			Date			result = Preceding (calendar, date);

			if (date.Month != result.Month)
				result = Following (calendar, date);
			return (result);
		}

		/// <summary>
		/// Adjusts dates which fall on a Saturday to the preceding Friday, and
		/// those falling on a Sunday to the following Monday. This convention
		/// is used by some national holidays, for example Christmas Day in the
		/// USA.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> to be used.</param>
		/// <param name="date">The <see cref="Date"/> to adjust.</param>
		/// <returns>A (possibly) adjusted <see cref="Date"/> instance.</returns>
		private static Date Weekend (Calendar calendar, Date date)
		{
			switch (date.Weekday) {
			case Date.SATURDAY:		return (date.PlusDays (-1));
			case Date.SUNDAY:		return (date.PlusDays (+1));
			}
			return (date);
		}
	}
}