// Copyright (C),2005-2011 HandCoded Software Ltd.
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
	/// The <b>Date</b> class implements an immutable date value.
	/// </summary>
	/// <remarks>The implementation of this class is based on the the C++ code
	/// in QuantLib and only covers years between 1900 and 2099. The code
	/// emulates a bug in Microsoft Excel that erroreously indicates 1900 as a 
	/// leap year but this is unlikely to be an issue in most applications.
	/// </remarks>
	[Serializable]
	public sealed class Date : TemporalDate, IComparable
	{
		/// <summary>
		/// The earliest possible date that can be correctly represented,
		/// </summary>
		public static readonly Date	MIN_VALUE
			= new Date (DateValue.MIN_VALUE, null);
	
		/// <summary>
		/// The latest possible date that can be correctly represented.
		/// </summary>
		public static readonly Date	MAX_VALUE
			= new Date (DateValue.MAX_VALUE, null);

		/// <summary>
		/// Constructs a <b>Date</b> instance given a day, month and year.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		public Date (int day, int month, int year)
			: this (new DateValue (day, month, year), null)
		{ }

		/// <summary>
		/// Constructs a <b>Date</b> instance given a day, month and year.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <param name="utc">Indicates UTC time zone.</param>
		public Date (int day, int month, int year, bool utc)
			: this (new DateValue (day, month, year), utc ? TimeZone.UTC : null)
		{ }

		/// <summary>
		/// Constructs a <b>Date</b> instance given a day, month and year.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <param name="offset">The time zone offset.</param>
		public Date (int day, int month, int year, int offset)
			: this (new DateValue (day, month, year), new TimeZone (offset))
		{ }

		/// <summary>
		/// Contains the weekday.
		/// </summary>
		public int Weekday {
			get {
				return (dateValue.Weekday);
			}
		}

		/// <summary>
		/// Contains the day of the month (1 - 31).
		/// </summary>
		public int DayOfMonth {
			get {
				return (dateValue.DayOfMonth);
			}
		}

		/// <summary>
		/// Contains the last day of the month (28 - 31)
		/// </summary>
		public int LastDayOfMonth {
			get {
				return (dateValue.LastDayOfMonth);
			}
		}

		/// <summary>
		/// Contains <c>true</c> if the date falls at the end of the month.
		/// </summary>
		public bool IsEndOfMonth {
			get {
				return (dateValue.IsEndOfMonth);
			}
		}

		/// <summary>
		/// Contains the day of the year (1 - 366)
		/// </summary>
		public int DayOfYear {
			get {
				return (dateValue.DayOfYear);
			}
		}

		/// <summary>
		/// Contains the month of the year (1 - 12)
		/// </summary>
		public int Month {
			get {
				return (dateValue.Month);
			}
		}
	
		/// <summary>
		/// Contains the year (1900 - 2099)
		/// </summary>
		public int Year {
			get {
				return (dateValue.Year);
			}
		}

		/// <summary>
		/// Parses a <b>DateTime</b> instance from a character string in the
		/// ISO date format (as produced by <b>ToString</b>).
		/// </summary>
		/// <param name="text">The text string to be parsed.</param>
		/// <returns>A new <b>DateTime</b> instance containing the parsed data.</returns>
		/// <exception cref="ArgumentException">If the character string is not in the
		///	correct format.</exception>
		public static Date Parse (String text)
		{
			int			limit = text.Length;
			int			index = 0;
			
			while (true) {
				// Extract date components
				if ((index >= limit) || !IsDigit (text [index])) break;
				int year = (text [index++] - '0') * 1000;
				if ((index >= limit) || !IsDigit (text [index])) break;
				year += (text [index++] - '0') * 100;
				if ((index >= limit) || !IsDigit (text [index])) break;
				year += (text [index++] - '0') *10;
				if ((index >= limit) || !IsDigit (text [index])) break;
				year += (text [index++] - '0');
				
				if ((index >= limit) || (text [index++] != '-')) break;
				
				if ((index >= limit) || !IsDigit (text [index])) break;
				int month = (text [index++] - '0') * 10;
				if ((index >= limit) || !IsDigit (text [index])) break;
				month += (text [index++] - '0');
				
				if ((index >= limit) || (text [index++] != '-')) break;
				
				if ((index >= limit) || !IsDigit (text [index])) break;
				int day = (text [index++] - '0') * 10;
				if ((index >= limit) || !IsDigit (text [index])) break;
				day += (text [index++] - '0');

				// Detect zulu time zone
				if ((index < limit)&& (text [index] == 'Z')) {
					return (new Date (day, month, year, true));
				}
				
				// Detect time offsets
				if ((index < limit)&& (text [index] == '+')) {
					++index;
					if ((index >= limit) && !IsDigit (text [index])) break; 
					int offset = (text [index++] - '0') * 600;
					if ((index >= limit) && !IsDigit (text [index])) break;
					offset += (text [index++] - '0') * 60;
					
					if ((index >= limit) && (text [index++] != ':')) break;
					
					if ((index >= limit) && !IsDigit (text [index])) break;
					offset = (text [index++] - '0') * 10;
					if ((index >= limit) && !IsDigit (text [index])) break;
					offset += (text [index++] - '0');

					return (new Date (day, month, year, offset));
				}
					
				if ((index < limit)&& (text [index] == '-')) {
					++index;
					if ((index >= limit) && !IsDigit (text [index])) break; 
					int offset = (text [index++] - '0') * 600;
					if ((index >= limit) && !IsDigit (text [index])) break;
					offset += (text [index++] - '0') * 60;
					
					if ((index >= limit) && (text [index++] != ':')) break;
					
					if ((index >= limit) && !IsDigit (text [index])) break;
					offset = (text [index++] - '0') * 10;
					if ((index >= limit) && !IsDigit (text [index])) break;
					offset += (text [index++] - '0');

					return (new Date (day, month, year, -offset));
				}
				
				return (new Date (day, month, year, false));
			}

			throw new ArgumentException ("Value is not in ISO date format");
		}

		/// <summary>
		/// Contains the current date.
		/// </summary>
		public static Date Now {
			get {
				System.DateTime	now = System.DateTime.Now;

				return (new Date (now.Day, now.Month, now.Year));
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
			return (DateValue.IsLeapYear (year));
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
			return (DateValue.MonthLength (month, leapYear));
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
			return (DateValue.MonthLength (month, year));
		}
	
		/// <summary>
		/// Creates a new <b>Date</b> based in the current instance and a given
		/// number of days adjustment.
		/// </summary>
		/// <param name="days">The number of days to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public Date PlusDays (int days)
		{
			return (new Date (dateValue.PlusDays (days), timeZone));
		}
	
		/// <summary>
		/// Creates a new <b>Date</b> based on the current instance and a given
		/// number of weeks adjustment.
		/// </summary>
		/// <param name="weeks">The number of weeks to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public Date PlusWeeks (int weeks)
		{
			return (new Date (dateValue.PlusWeeks (weeks), timeZone));
		}

		/// <summary>
		/// Creates a new <b>Date</b> based on the current instance and a given
		/// number of months adjustment.
		/// </summary>
		/// <param name="months">The number of months to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public Date PlusMonths (int months)
		{
			return (new Date (dateValue.PlusMonths (months), timeZone));
		}
		
		/// <summary>
		/// Creates a new <b>Date</b> based on the current instance and a given
		/// number of years adjustment.
		/// </summary>
		/// <param name="years">The number of years to adjust by.</param>
		/// <returns>The adjusted date.</returns>
		public Date PlusYears (int years)
		{
			return (new Date (dateValue.PlusYears (years), timeZone));
		}
		
		/// <summary>
		/// Creates a new <b>Date</b> based on the current instance and a given
		/// <see cref="Interval"/>.
		/// </summary>
		/// <param name="interval">The time <see cref="Interval"/>.</param>
		/// <returns>The adjusted date.</returns>
		public Date Plus (Interval interval)
		{
			return (new Date (dateValue.Plus (interval), timeZone));
		}

		/// <summary>
		/// Creates a <b>DateTime</b> instance representing midnight on the
		/// morning of the current date.
		/// </summary>
		/// <returns>The <b>DateTime</b> instance.</returns>
		public DateTime ToDateTime ()
		{
			return (new DateTime (dateValue, TimeValue.START_OF_DAY, timeZone));
		}

		/// <summary>
		/// Returns the hash value of the date for hash based data structures and
		/// algorithms. 
		/// </summary>
		/// <returns>The hash value of the date.</returns>
		public override int GetHashCode()
		{
			if (timeZone != null)
				return (dateValue.GetHashCode () ^ timeZone.GetHashCode ());
			else
				return (dateValue.GetHashCode ());
		}

		/// <summary>
		/// Determines if this <b>Date</b> instance and another hold the same
		/// date.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns><c>true</c> if both instance represent the same date,
		/// <c>false</c> otherwise.</returns>
		public override bool Equals (object other)
		{
			return ((other is Date) && Equals (other as Date));
		}

		/// <summary>
		/// Determines if this <b>Date</b> instance and another hold the same
		/// date.
		/// </summary>
		/// <param name="other">The <b>Date</b> instance to compare with.</param>
		/// <returns><c>true</c> if both instance represent the same date,
		/// <c>false</c> otherwise.</returns>
		public bool Equals (Date other)
		{
            if ((timeZone == null) && (other.timeZone == null))
                return (dateValue.Equals (other.dateValue));
            else if ((timeZone != null) && (other.timeZone != null) && timeZone.Equals (other.timeZone))
                return (dateValue.Equals (other.dateValue));
            else
			    return (ToDateTime ().Equals (other.ToDateTime ()));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <see cref="Object"/>.
		/// </summary>
		/// <param name="other">The <see cref="Object"/> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		/// <exception cref="InvalidCastException">If the argument is not a
		/// <c>Date</c> instance.</exception>
		public int CompareTo (Object other)
		{
			return (CompareTo (other as Date));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <b>Date</b>.
		/// </summary>
		/// <param name="other">The <b>Date</b> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (Date other)
		{
            if ((timeZone == null) && (other.timeZone == null))
                return (dateValue.CompareTo (other.dateValue));
            else if ((timeZone != null) && (other.timeZone != null) && timeZone.Equals (other.timeZone))
                return (dateValue.CompareTo (other.dateValue));
            else
			    return (ToDateTime ().CompareTo (other.ToDateTime ()));
		}

		/// <summary>
		/// Converts the instance data members to a <see cref="string"/> representation
		/// that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
		public override string ToString ()
		{
			if (timeZone != null)
				return (dateValue.ToString () + timeZone.ToString ());
			else
				return (dateValue.ToString ());
		}

		/// <summary>
		/// Constructs a <b>Date</b> using its <see cref="DateValue"/> and
		/// <see cref="TimeZone"/> components.
		/// </summary>
		/// <param name="dateValue">The <see cref="DateValue"/> component.</param>
		/// <param name="timeZone">The <see cref="TimeZone"/> component or <b>null</b>.</param>
		internal Date (DateValue dateValue, TimeZone timeZone)
			: base (timeZone)
		{	
			this.dateValue = dateValue;
		}

		/// <summary>
		/// The date part of the <b>DateTime</b>.
		/// </summary>
		private readonly DateValue		dateValue;
	}
}