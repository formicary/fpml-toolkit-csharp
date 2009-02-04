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
using System.Collections.Generic;
using System.Text;

namespace HandCoded.Finance
{
	/// <summary>
	/// Instances of the <b>DateValue</b> are used to hold the date portion
	/// of <see cref="Date"/> and <see cref="DateTime"/> values.
	/// </summary>
	[Serializable]
	sealed class DateValue : IImmutableDate, IComparable
	{
		/// <summary>
		/// The earliest possible date that can be correctly represented,
		/// </summary>
		public static readonly DateValue	MIN_VALUE	= new DateValue (367);

		/// <summary>
		/// The latest possible date that can be correctly represented.
		/// </summary>
		public static readonly DateValue	MAX_VALUE	= new DateValue (73050);

		/// <summary>
		/// Constructs a <b>DateValue</b> instance given a day, month and year.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <exception cref="ArgumentException">If the day, month or year
		/// values are outside the correct range.</exception>
		public DateValue (int day, int month, int year)
		{
			if ((year < 1900) || (year >= 2100))
				throw new ArgumentException ("Year value is out of range");
			if ((month < 1) || (month > 12))
				throw new ArgumentException ("Invalid month value");
			if ((day < 1) || (day > MonthLength (month, year)))
				throw new ArgumentException ("Invalid day value");
			
			date = day + MonthOffset (month, IsLeapYear (year)) + YearOffset (year);
		}

		/// <summary>
		/// Contains the weekday.
		/// </summary>
		public int Weekday {
			get {
				return (1 + (date + 6) % 7);
			}
		}

		/// <summary>
		/// Contains the day of the month (1 - 31).
		/// </summary>
		public int DayOfMonth {
			get {
				return (DayOfYear - MonthOffset (Month, IsLeapYear (Year)));
			}
		}

		/// <summary>
		/// Contains the last day of the month (28 - 31)
		/// </summary>
		public int LastDayOfMonth {
			get {
				return (MonthLength (Month, IsLeapYear (Year)));
			}
		}

		/// <summary>
		/// Contains <c>true</c> if the date falls at the end of the month.
		/// </summary>
		public bool IsEndOfMonth {
			get {
				return (DayOfMonth == LastDayOfMonth);
			}
		}

		/// <summary>
		/// Contains the day of the year (1 - 366)
		/// </summary>
		public int DayOfYear {
			get {
				return (date - YearOffset (Year));
			}
		}

		/// <summary>
		/// Contains the month of the year (1 - 12)
		/// </summary>
		public int Month {
			get {
				int			day 	= DayOfYear;
				int			month 	= day / 30 + 1;
				bool		leap	= IsLeapYear (Year);
		
				while (day <= MonthOffset (month, leap)) --month;
				while (day >  MonthOffset (month + 1, leap)) ++month;
		
				return (month);
			}
		}
	
		/// <summary>
		/// Contains the year (1900 - 2099)
		/// </summary>
		public int Year {
			get {
				int			year 	= (date / 365) + 1900;
		
				if (date <= YearOffset (year)) --year;

				return (year);
			}
		}

		/// <summary>
		/// Determines if a year is a leap year.
		/// </summary>
		/// <param name="year">The year to test (1900-2099)</param>
		/// <returns><c>true</c> if the year is a leap year, <c>false</c>
		/// otherwise.</returns>
		public static bool IsLeapYear (int year)
		{
			return (leapYears [year - 1900]);
		}

		/// <summary>
		/// Returns the number of days in the given month in a leap or non-leap
		/// year.
		/// </summary>
		/// <param name="month">The month number (1-12).</param>
		/// <param name="leapYear">Flag to indicate a leap year.</param>
		/// <returns>The length of the given month in the indicated type of
		/// year.</returns>
		public static int MonthLength (int month, bool leapYear)
		{
			return (((leapYear) ? leapMonthLength : monthLength)[month - 1]);
		}
	
		/// <summary>
		/// Returns the number of days in the given month for the specified
		/// year.
		/// </summary>
		/// <param name="month">The month number (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <returns>The length of the given month in the specified year.</returns>
		public static int MonthLength (int month, int year)
		{
			return (MonthLength (month, IsLeapYear (year)));
		}

		/// <summary>
		/// Creates a new <b>DateValue</b> based in the current instance and a given
		/// number of days adjustment.
		/// </summary>
		/// <param name="days">The number of days to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public DateValue PlusDays (int days)
		{
			return (new DateValue (date + days));
		}
	
		/// <summary>
		/// Creates a new <b>DateValue</b> based on the current instance and a given
		/// number of weeks adjustment.
		/// </summary>
		/// <param name="weeks">The number of weeks to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public DateValue PlusWeeks (int weeks)
		{
			return (new DateValue (date + 7 * weeks));
		}

		/// <summary>
		/// Creates a new <b>DateValue</b> based on the current instance and a given
		/// number of months adjustment.
		/// </summary>
		/// <param name="months">The number of months to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public DateValue PlusMonths (int months)
		{
			int			m		= Month + months;
			int			y		= Year;
			int			d		= DayOfMonth;

			while (m < 1) {
				m += 12;
				y -= 1;
			}
			while (m > 12) {
				m -= 12;
				y += 1;
			}
			
			int l = MonthLength (m, y);
			if (d > l) d = l;
			
			return (new DateValue (d, m, y));
		}
		
		/// <summary>
		/// Creates a new <b>DateValue</b> based on the current instance and a given
		/// number of years adjustment.
		/// </summary>
		/// <param name="years">The number of years to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public DateValue PlusYears (int years)
		{
			return (new DateValue (DayOfMonth, Month, Year + years));
		}
		
		/// <summary>
		/// Creates a new <b>DateValue</b> based on the current instance and a given
		/// <see cref="Interval"/>.
		/// </summary>
		/// <param name="interval">The time <see cref="Interval"/>.</param>
		/// <returns>The adjusted date.</returns>
		public DateValue Plus (Interval interval)
		{
			int			multiplier	= interval.Multiplier;
			Period		period 		= interval.Period;
			
			if (period == Period.DAY)
				return (PlusDays (multiplier));
			else if (period == Period.WEEK)
				return (PlusWeeks (multiplier));
			else if (period == Period.MONTH)
				return (PlusMonths (multiplier));
			else if (period == Period.YEAR)
				return (PlusYears (multiplier));
			else
				return (null);
		}

		public override int GetHashCode ()
		{
			return (date);
		}

		/// <summary>
		/// Determines if this <b>DateValue</b> instance and another object hold
		/// the same date.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns><b>true</b> if both instances represent the same date,
		/// <b>false</b> otherwise.</returns>
		public override bool Equals (object other)
		{
			return ((other is DateValue) && Equals (other as DateValue));
		}

		/// <summary>
		/// Determines if this <b>DateValue</b> instance and another instance hold
		/// the same date.
		/// </summary>
		/// <param name="other">The <b>DateValue</b> instance to compare with.</param>
		/// <returns><b>true</b> if both instances represent the same date,
		/// <b>false</b> otherwise.</returns>
		public bool Equals (DateValue other)
		{
			return (date == other.date);
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <see cref="object"/>.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (object other)
		{
			return (CompareTo (other as DateValue));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <b>DateValue</b>.
		/// </summary>
		/// <param name="other">The <b>DateValue</b> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (DateValue other)
		{
			return (date - other.date);
		}

		/// <summary>
		/// Converts the instance data members to a <see cref="string"/>
		/// representation that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
		public override string ToString ()
		{
			int				year	= Year; 
			int				month	= Month;
			int				day		= DayOfMonth;
			
			lock (buffer) {
				buffer.Length = 0;
				
				buffer.Append (year);
				buffer.Append ('-');
				buffer.Append ((char)('0' + month / 10));
				buffer.Append ((char)('0' + month % 10));
				buffer.Append ('-');
				buffer.Append ((char)('0' + day / 10));
				buffer.Append ((char)('0' + day % 10));
				
				return (buffer.ToString ());
			}
		}
		
		/// <summary>
		/// Lookup table of leap years.
		/// </summary>
		private static readonly bool [] leapYears = {
			// 1900-1909
			true,false,false,false, true,false,false,false, true,false,
			// 1910-1919
			false,false, true,false,false,false, true,false,false,false,
			// 1920-1929
			true,false,false,false, true,false,false,false, true,false,
			// 1930-1939
			false,false, true,false,false,false, true,false,false,false,
			// 1940-1949
			true,false,false,false, true,false,false,false, true,false,
			// 1950-1959
			false,false, true,false,false,false, true,false,false,false,
			// 1960-1969
			true,false,false,false, true,false,false,false, true,false,
			// 1970-1979
			false,false, true,false,false,false, true,false,false,false,
			// 1980-1989
			true,false,false,false, true,false,false,false, true,false,
			// 1990-1999
			false,false, true,false,false,false, true,false,false,false,
			// 2000-2009
			true,false,false,false, true,false,false,false, true,false,
			// 2010-2019
			false,false, true,false,false,false, true,false,false,false,
			// 2020-2029
			true,false,false,false, true,false,false,false, true,false,
			// 2030-2039
			false,false, true,false,false,false, true,false,false,false,
			// 2040-2049
			true,false,false,false, true,false,false,false, true,false,
			// 2050-2059
			false,false, true,false,false,false, true,false,false,false,
			// 2060-2069
			true,false,false,false, true,false,false,false, true,false,
			// 2070-2079
			false,false, true,false,false,false, true,false,false,false,
			// 2080-2089
			true,false,false,false, true,false,false,false, true,false,
			// 2090-2099
			false,false, true,false,false,false, true,false,false,false,
			// 2100
			false
		};
		    
		/// <summary>
		/// Normal year month lengths
		/// </summary>
		private static readonly int	[] monthLength = {
			31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
		};

		/// <summary>
		/// Leap year month lengths
		/// </summary>
		private static readonly int [] leapMonthLength =  {
			31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
		};

		/// <summary>
		/// Normal year month offsets
		/// </summary>
		private static readonly int [] monthOffset = {
			  0,  31,  59,  90, 120, 151,   // Jan - Jun
			181, 212, 243, 273, 304, 334,   // Jun - Dec
			365     // used in dayOfMonth to bracket day
		};

		/// <summary>
		/// Leap year month offsets.
		/// </summary>
		private static readonly int [] leapMonthOffset = {
			  0,  31,  60,  91, 121, 152,   // Jan - Jun
			182, 213, 244, 274, 305, 335,   // Jun - Dec
			366     // used in dayOfMonth to bracket day
		};

		/// <summary>
		/// Lookup table of year offsets based on the day number of December 31st in
		/// the preceding year.
		/// </summary>
		private static readonly int [] yearOffset = {
			// 1900-1909
			0,  366,  731, 1096, 1461, 1827, 2192, 2557, 2922, 3288,
			// 1910-1919
			3653, 4018, 4383, 4749, 5114, 5479, 5844, 6210, 6575, 6940,
			// 1920-1929
			7305, 7671, 8036, 8401, 8766, 9132, 9497, 9862,10227,10593,
			// 1930-1939
			10958,11323,11688,12054,12419,12784,13149,13515,13880,14245,
			// 1940-1949
			14610,14976,15341,15706,16071,16437,16802,17167,17532,17898,
			// 1950-1959
			18263,18628,18993,19359,19724,20089,20454,20820,21185,21550,
			// 1960-1969
			21915,22281,22646,23011,23376,23742,24107,24472,24837,25203,
			// 1970-1979
			25568,25933,26298,26664,27029,27394,27759,28125,28490,28855,
			// 1980-1989
			29220,29586,29951,30316,30681,31047,31412,31777,32142,32508,
			// 1990-1999
			32873,33238,33603,33969,34334,34699,35064,35430,35795,36160,
			// 2000-2009
			36525,36891,37256,37621,37986,38352,38717,39082,39447,39813,
			// 2010-2019
			40178,40543,40908,41274,41639,42004,42369,42735,43100,43465,
			// 2020-2029
			43830,44196,44561,44926,45291,45657,46022,46387,46752,47118,
			// 2030-2039
			47483,47848,48213,48579,48944,49309,49674,50040,50405,50770,
			// 2040-2049
			51135,51501,51866,52231,52596,52962,53327,53692,54057,54423,
			// 2050-2059
			54788,55153,55518,55884,56249,56614,56979,57345,57710,58075,
			// 2060-2069
			58440,58806,59171,59536,59901,60267,60632,60997,61362,61728,
			// 2070-2079
			62093,62458,62823,63189,63554,63919,64284,64650,65015,65380,
			// 2080-2089
			65745,66111,66476,66841,67206,67572,67937,68302,68667,69033,
			// 2090-2099
			69398,69763,70128,70494,70859,71224,71589,71955,72320,72685,
			// 2100
			73050
		};

		/// <summary>
		/// Returns the offset from the start of the year in days of the given
		/// month in a leap or non-leap year.
		/// </summary>
		/// <remarks>This function is not exposed publically as it reveals the
		/// implementation.</remarks>
		/// <param name="month">The month number (1-12).</param>
		/// <param name="leapYear">Flag to indicate a leap year.</param>
		/// <returns>The offset of the given month in the indicated type of
		/// year.</returns>
		private static int MonthOffset (int month, bool leapYear)
		{
			return (((leapYear) ? leapMonthOffset : monthOffset)[month - 1]);
		}
	
		/// <summary>
		/// Returns the day count offset of the given year.
		/// </summary>
		/// <param name="year">The year (1900-2099).</param>
		/// <returns>The day offset value.</returns>
		private static int YearOffset (int year)
		{
			return (yearOffset [year - 1900]);
		}

		/// <summary>
		/// Constructs a <b>DateValue</b> given a Julian day number.
		/// </summary>
		/// <param name="date">The Julian day number.</param>
		internal DateValue (int date)
		{
    		this.date = date;
		}
	    
		/// <summary>
		/// A static buffer used to produce the <b>ToString</b> value. The
		/// buffer must be locked before use to ensure thread safety.
		/// </summary>
		private static StringBuilder	buffer = new StringBuilder ();
		
		/// <summary>
		/// The date expressed as the number of days since 1st Jan 1900
		/// </summary>
		private readonly int			date;
	}
}