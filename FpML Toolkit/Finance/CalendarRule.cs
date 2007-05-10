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
	/// A <b>CalendarRule</b> instance contains the data and algorithmic rule
	/// necessary to compute the date when a holiday will fall in any appropriate
	/// year.
	/// </summary>
	public abstract class CalendarRule
	{
		/// <summary>
		/// The <b>Fixed</b> class extends <see cref="Calendar"/> with an
		/// understanding of how to handle holidays than fall on fixed dates and
		/// for which the associate public holiday date may be rolled.
		/// </summary>
		public sealed class Fixed : CalendarRule
		{
			/// <summary>
			/// Constructs a <b>Fixed</b> date calender rule.
			/// </summary>
			/// <param name="name">The name of the holiday.</param>
			/// <param name="from">The first year in which the holiday occurs.</param>
			/// <param name="until">The last year in which the holiday occurs.</param>
			/// <param name="month">The month of the year where the holidays falls.</param>
			/// <param name="dayOfMonth">The day of the month where the holiday falls.</param>
			/// <param name="dateRoll">The <see cref="DateRoll"/> used to adjust dates.</param>
			public Fixed (string name, int from, int until, int month, int dayOfMonth, DateRoll dateRoll)
				: base (name, from, until)
			{
				this.month		= month;
				this.dayOfMonth	= dayOfMonth;
				this.dateRoll	= dateRoll;
			}

			/// <summary>
			/// Contains the month the holiday falls in.
			/// </summary>
			public int Month {
				get {
					return (month);
				}
			}

			/// <summary>
			/// Contains the day of the month.
			/// </summary>
			public int DayOfMonth {
				get {
					return (dayOfMonth);
				}
			}

			/// <summary>
			/// Contains the <see cref="DateRoll"/>.
			/// </summary>
			public DateRoll DateRoll {
				get {
					return (dateRoll);
				}
			}

			/// <summary>
			/// Applies the algorithm defined by the <see cref="CalendarRule"/> to calculate
			/// the date that a holiday falls in the given year using the given
			/// <see cref="Calendar"/> to avoid other non-business dates.
			/// </summary>
			/// <param name="calendar">The <see cref="Calendar"/> used to identify non-business days.</param>
			/// <param name="year">The year to generate the holiday for.</param>
			/// <returns>The adjusted <see cref="Date"/> that the holiday falls on.</returns>
			public override Date Generate (Calendar calendar, int year)
			{
				return (dateRoll.Adjust (calendar, new Date (dayOfMonth, month, year)));
			}

			/// <summary>
			/// The month in holiday falls in.
			/// </summary>
			private readonly int		month;

			/// <summary>
			///  The day of the month the holiday falls on.
			/// </summary>
			private readonly int		dayOfMonth;

			/// <summary>
			/// The <see cref="DateRoll"/> used to adjust the date.
			/// </summary>
			private readonly DateRoll	dateRoll;
		}

		/// <summary>
		/// The <b>Offset</b> class extends <see cref="CalendarRule"/> with an
		/// understanding of holidays that fall on a day offset from the start of
		/// a month (e.g. Thanksgiving in the US).
		/// </summary>
		public sealed class Offset : CalendarRule
		{
			/// <summary>
			/// An constant value indicating the first occurance of a day.
			/// </summary>
			public const int			FIRST	= 1;

			/// <summary>
			/// An constant value indicating the second occurance of a day.
			/// </summary>
			public const int			SECOND	= 2;

			/// <summary>
			/// An constant value indicating the third occurance of a day.
			/// </summary>
			public const int			THIRD	= 3;

			/// <summary>
			/// An constant value indicating the fourth occurance of a day.
			/// </summary>
			public const int			FOURTH	= 4;

			/// <summary>
			/// An constant value indicating the last occurance of a day.
			/// </summary>
			public const int			LAST	= -1;

			/// <summary>
			/// Constructs an <b>Offset</b> instance that describes a holiday that
			/// occurs on a given occurance of a weekday in the specified month.
			/// </summary>
			/// <param name="name">The name of the holiday.</param>
			/// <param name="from">The first year in which the holiday occurs.</param>
			/// <param name="until">The last year in which the holiday occurs.</param>
			/// <param name="when">The occurance of within the month (e.g. first, last).</param>
			/// <param name="day">The weekday the holiday falls on.</param>
			/// <param name="month">The month the holiday falls in.</param>
			public Offset (string name, int from, int until, int when, int day, int month)
				: base (name, from, until)
			{
				this.when	= when;
				this.day	= day;
				this.month	= month;
			}

			/// <summary>
			/// Contains the occurance value.
			/// </summary>
			public int When {
				get {
					return (when);
				}
			}

			/// <summary>
			/// Contains the weekday the holiday falls on.
			/// </summary>
			public int Day {
				get {
					return (day);
				}
			}

			/// <summary>
			/// Contains the month when the holiday falls.
			/// </summary>
			public int Month {
				get {
					return (month);
				}
			}

			/// <summary>
			/// Applies the algorithm defined by the <see cref="CalendarRule"/> to calculate
			/// the date that a holiday falls in the given year using the given
			/// <see cref="Calendar"/> to avoid other non-business dates.
			/// </summary>
			/// <param name="calendar">The <see cref="Calendar"/> used to identify non-business days.</param>
			/// <param name="year">The year to generate the holiday for.</param>
			/// <returns>The adjusted <see cref="Date"/> that the holiday falls on.</returns>
			public override Date Generate (Calendar calendar, int year)
			{
				int 	limit = Date.MonthLength (month, year);
				
				if (when == LAST) {	
					for (int index = limit - 6; index <= limit; ++index) {
						Date date = new Date (index, month, year);
					
						if (date.Weekday == day) return (date);
					}
				}
				else {
					int count = 0;
		
					for (int index = 1; index <= limit; ++index) {
						Date date = new Date (index, month, year);
						
						if ((date.Weekday == day) && (++count == when))
							return (date);
					}
				}
				return (null);
			}

			/// <summary>
			/// The occurance of the day.
			/// </summary>
			private readonly int		when;

			/// <summary>
			/// The day of the week.
			/// </summary>
			private readonly int		day;

			/// <summary>
			/// The month of the year.
			/// </summary>
			private readonly int		month;
		}

		/// <summary>
		/// The <b>Easter</b> class extends <see cref="CalendarRule"/> with an
		/// understanding of Easter related holidays.
		/// </summary>
		public sealed class Easter : CalendarRule
		{
			/// <summary>
			/// Constructs an <b>Easter</b> related holiday definition.
			/// </summary>
			/// <param name="name">The name of the holiday.</param>
			/// <param name="from">The first year in which the holiday occurs.</param>
			/// <param name="until">The last year in which the holiday occurs.</param>
			/// <param name="offset">The number of days relative to Easter Monday.</param>
			public Easter (string name, int from, int until, int offset)
				: base (name, from, until)
			{
				this.offset = offset;
			}

			/// <summary>
			/// Contains the offset of the holiday from Easter Monday.
			/// </summary>
			public new int Offset {
				get {
					return (offset);
				}
			}

			/// <summary>
			/// Applies the algorithm defined by the <see cref="CalendarRule"/> to calculate
			/// the date that a holiday falls in the given year using the given
			/// <see cref="Calendar"/> to avoid other non-business dates.
			/// </summary>
			/// <param name="calendar">The <see cref="Calendar"/> used to identify non-business days.</param>
			/// <param name="year">The year to generate the holiday for.</param>
			/// <returns>The adjusted <see cref="Date"/> that the holiday falls on.</returns>
			public override Date Generate (Calendar calendar, int year)
			{
				return (new Date (1, 1, year).PlusDays (easterMonday [year - 1900] + offset - 1));
			}

			/// <summary>
			/// A lookup table of Easter Monday date offsets for the years 1900
			/// to 2099.
			/// </summary>
			private static int []		easterMonday = {
	            107,  98,  90, 103,  95, 114, 106,  91, 111, 102,   // 1900-1909
	             87, 107,  99,  83, 103,  95, 115,  99,  91, 111,   // 1910-1919
	             96,  87, 107,  92, 112, 103,  95, 108, 100,  91,   // 1920-1929
	            111,  96,  88, 107,  92, 112, 104,  88, 108, 100,   // 1930-1939
	             85, 104,  96, 116, 101,  92, 112,  97,  89, 108,   // 1940-1949
	            100,  85, 105,  96, 109, 101,  93, 112,  97,  89,   // 1950-1959
	            109,  93, 113, 105,  90, 109, 101,  86, 106,  97,   // 1960-1969
	             89, 102,  94, 113, 105,  90, 110, 101,  86, 106,   // 1970-1979
	             98, 110, 102,  94, 114,  98,  90, 110,  95,  86,   // 1980-1989
	            106,  91, 111, 102,  94, 107,  99,  90, 103,  95,   // 1990-1999
	            115, 106,  91, 111, 103,  87, 107,  99,  84, 103,   // 2000-2009
	             95, 115, 100,  91, 111,  96,  88, 107,  92, 112,   // 2010-2019
	            104,  95, 108, 100,  92, 111,  96,  88, 108,  92,   // 2020-2029
	            112, 104,  89, 108, 100,  85, 105,  96, 116, 101,   // 2030-2039
	             93, 112,  97,  89, 109, 100,  85, 105,  97, 109,   // 2040-2049
	            101,  93, 113,  97,  89, 109,  94, 113, 105,  90,   // 2050-2059
	            110, 101,  86, 106,  98,  89, 102,  94, 114, 105,   // 2060-2069
	             90, 110, 102,  86, 106,  98, 111, 102,  94, 107,   // 2070-2079
	             99,  90, 110,  95,  87, 106,  91, 111, 103,  94,   // 2080-2089
	            107,  99,  91, 103,  95, 115, 107,  91, 111, 103    // 2090-2099
	    	};

			/// <summary>
			/// The offset of the holiday from Easter Monday.
			/// </summary>
			private readonly int		offset;
		}

		/// <summary>
		/// Contains the name of the holiday.
		/// </summary>
		public string Name {
			get {
				return (name);
			}
		}

		/// <summary>
		/// Contains the first year that the rule is applicable.
		/// </summary>
		public int From {
			get {
				return (from);
			}
		}

		/// <summary>
		/// Contains the last year that the rule is applicable.
		/// </summary>
		public int Until {
			get {
				return (until);
			}
		}

		/// <summary>
		/// Determines if the <b>CalendarRule</b> is applicable to the given
		/// year.
		/// </summary>
		/// <param name="year">The year to be tested.</param>
		/// <returns><c>true</c> if this <b>CalendarRule</b> is applicable in
		/// the given year, <c>false</c> otherwise.</returns>
		public bool IsApplicable (int year)
		{
			return ((from <= year) && (year <= until));
		}

		/// <summary>
		/// Applies the algorithm defined by the <b>CalendarRule</b> to calculate
		/// the date that a holiday falls in the given year using the given
		/// <see cref="Calendar"/> to avoid other non-business dates.
		/// </summary>
		/// <param name="calendar">The <see cref="Calendar"/> used to identify non-business days.</param>
		/// <param name="year">The year to generate the holiday for.</param>
		/// <returns>The adjusted <see cref="Date"/> that the holiday falls on.</returns>
		public abstract Date Generate (Calendar calendar, int year);

		/// <summary>
		/// Constructs a <b>CalendarRule</b> with a given name and applicable
		/// year range.
		/// </summary>
		/// <param name="name">The name of the holiday.</param>
		/// <param name="from">The first year in which the holiday occurs.</param>
		/// <param name="until">The last year in which the holiday occurs.</param>
		protected CalendarRule (string name, int from, int until)
		{
			this.name	= name;
			this.from	= from;
			this.until  = until;
		}

		/// <summary>
		/// The name of the holiday.
		/// </summary>
		private readonly string		name;

		/// <summary>
		/// The first year the holiday is applicable.
		/// </summary>
		private readonly int		from;

		/// <summary>
		/// The last year the holiday is applicable.
		/// </summary>
		private readonly int		until;
	}
}