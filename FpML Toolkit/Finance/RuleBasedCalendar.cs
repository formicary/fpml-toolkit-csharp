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
	/// A <b>RuleBasedCalendar</b> uses a set of <see cref="CalendarRule"/>
	/// instances to derive the dates on which holidays will occur either in the
	/// past or the future.
	/// </summary>
	public class RuleBasedCalendar : Calendar
	{
		/// <summary>
		/// Constructs a <b>RuleBasedCalendar</b> with the given name and
		/// <see cref="Weekend"/> rule.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="weekend"></param>
		public RuleBasedCalendar (string name, Weekend weekend)
			: base (name)
		{
			this.weekend = weekend;
		}

		/// <summary>
		/// Determines if the <see cref="Date"/> provided falls on a business
		/// day in this <b>Calendar</b> (e.g. not a holiday or weekend).
		/// </summary>
		/// <param name="date">The <see cref="Date"/> to be tested.</param>
		/// <returns><c>true</c> if the date is a business day, <c>false</c>
		/// otherwise.</returns>
		public override bool IsBusinessDay (Date date)
		{
			if (weekend.IsWeekend (date)) return (false);

			int year = date.Year;

			if ((holidays == null) || (year < minYear) || (year > maxYear))
				lock (this) {
					if (holidays == null) {
						holidays = new Dictionary<Date, CalendarRule> ();

						Generate (minYear = maxYear = year, year);
					}
					else {
						if (year < minYear) {
							int oldLimit = minYear;
							Generate (minYear = year, oldLimit - 1);
						}
						else {
							int oldLimit = maxYear;
							Generate (oldLimit + 1, maxYear = year);
						}
					}
				}

			return (!holidays.ContainsKey (date));
		}

		/// <summary>
		/// Adds a <see cref="CalendarRule"/> instance to the set maintained by the
		/// current instance.
		/// </summary>
		/// <param name="rule">The <see cref="CalendarRule"/> to be added.</param>
		public void AddRule (CalendarRule rule)
		{
			rules.Add (rule);
		}

		/// <summary>
		/// The <see cref="Weekend"/> instance to used for recurring weekly
		/// non-business days.
		/// </summary>
		private readonly Weekend weekend;

		/// <summary>
		/// The set of <see cref="CalendarRule"/> instances used to define
		/// holidays.
		/// </summary>
		private List<CalendarRule>	rules		= new List<CalendarRule> ();

		/// <summary>
		/// The oldest year for which holiday dates have been calculated.
		/// </summary>
		private int				minYear;

		/// <summary>
		/// The most future year for which holiday dates have been calculated.
		/// </summary>
		private int				maxYear;

		/// <summary>
		/// The set of all holiday dates determined so far.
		/// </summary>
		private Dictionary<Date, CalendarRule>	holidays	= null;

		/// <summary>
		/// Uses the <see cref="CalendarRule"/> instances to extend the holiday
		/// set for the years specified.
		/// </summary>
		/// <param name="min">The first year in the period required.</param>
		/// <param name="max">The last year in the period required.</param>
		private void Generate (int min, int max)
		{
			foreach (CalendarRule rule in rules)
				for (int year = min; year <= max; ++year)
					holidays.Add (rule.Generate (this, year), rule);
		}
	}
}