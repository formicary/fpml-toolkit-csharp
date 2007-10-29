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
	/// The <b>DateTime</b> class provides an immutable representation for a
	/// an ISO datetime value.
	/// </summary>
	[Serializable]
	public sealed class DateTime : TemporalDate, IImmutableDate, IImmutableTime, IComparable
	{
		/// <summary>
		/// The earliest possible date that can be correctly represented,
		/// </summary>
		public static readonly DateTime	MIN_VALUE
			= new DateTime (DateValue.MIN_VALUE, TimeValue.START_OF_DAY, null);
	
		/// <summary>
		/// The latest possible date that can be correctly represented.
		/// </summary>
		public static readonly DateTime	MAX_VALUE
			= new DateTime (DateValue.MAX_VALUE, TimeValue.END_OF_DAY, null);

		/// <summary>
		/// Constructs a <b>DateTime</b> instance given all the date and
		/// time components.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		public DateTime (int day, int month, int year,
				int hours, int minutes, int seconds)
			: this (new DateValue (day, month, year),
					new TimeValue (hours, minutes, seconds), null)
		{ }

		/// <summary>
		/// Constructs a <b>DateTime</b> instance given all the date and
		/// time components and a flag indicating UTC time or not.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		/// <param name="utc">Indicates UTC time zone.</param>
		public DateTime (int day, int month, int year,
				int hours, int minutes, int seconds, bool utc)
			: this (new DateValue (day, month, year),
					  new TimeValue (hours, minutes, seconds),
					  utc ? TimeZone.UTC : null)
		{ }

		/// <summary>
		/// Constructs a <b>DateTime</b> instance given all the date and time
		/// components plus a time zone offset.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		/// <param name="offset">The time zone offset.</param>
		public DateTime (int day, int month, int year,
				int hours, int minutes, int seconds, int offset)
			: this (new DateValue (day, month, year),
					  new TimeValue (hours, minutes, seconds),
					  new TimeZone (offset))
		{ }

		/// <summary>
		/// Constructs a <b>DateTime</b> instance given all the date and
		/// time components and a flag indicating UTC time or not.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		/// <param name="utc">Indicates UTC time zone.</param>
		public DateTime (int day, int month, int year,
				int hours, int minutes, decimal seconds, bool utc)
			: this (new DateValue (day, month, year),
					  new TimeValue (hours, minutes, seconds),
					  utc ? TimeZone.UTC : null)
		{ }

		/// <summary>
		/// Constructs a <b>DateTime</b> instance given all the date and time
		/// components plus a time zone offset.
		/// </summary>
		/// <param name="day">The day of the month (1-31).</param>
		/// <param name="month">The month of the year (1-12).</param>
		/// <param name="year">The year (1900-2099).</param>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		/// <param name="offset">The time zone offset.</param>
		public DateTime (int day, int month, int year,
				int hours, int minutes, decimal seconds, int offset)
			: this (new DateValue (day, month, year),
					  new TimeValue (hours, minutes, seconds),
					  new TimeZone (offset))
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
		/// Contains the hours component of the time values.
		/// </summary>
		public int Hours {
			get {
				return (timeValue.Hours);
			}
		}

		/// <summary>
		/// Contains the minutes component of the time value.
		/// </summary>
		public int Minutes {
			get {
				return (timeValue.Minutes);
			}
		}

		/// <summary>
		/// Contains the seconds component of the time value.
		/// </summary>
		public decimal Seconds {
			get {
				return (timeValue.Seconds);
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
		public static DateTime Parse (String text)
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

				if ((index >= limit) || (text [index++] != 'T')) break;

				// Extract time components
				if ((index >= limit) || !IsDigit (text [index])) break;
				int hours = (text [index++] - '0') * 10;
				if ((index >= limit) || !IsDigit (text [index])) break;
				hours += (text [index++] - '0');
				
				if ((index >= limit) || (text [index++] != ':')) break;
				
				if ((index >= limit) || !IsDigit (text [index])) break;
				int minutes = (text [index++] - '0') * 10;
				if ((index >= limit) || !IsDigit (text [index])) break;
				minutes += (text [index++] - '0');
				
				if ((index >= limit) || (text [index++] != ':')) break;
				
				int start = index;
				if ((index >= limit) || !IsDigit (text [index++])) break;
				if ((index >= limit) || !IsDigit (text [index++])) break;
			
				if ((index < limit) && (text [index] == '.')) {
					do {
						++index;
					} while ((index < limit) && IsDigit (text [index]));
				}
				decimal seconds = Decimal.Parse (text.Substring (start, index));

				// Detect zulu time zone
				if ((index < limit)&& (text [index] == 'Z')) {
					return (new DateTime (day, month, year, hours, minutes, seconds, true));
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

					return (new DateTime (day, month, year, hours, minutes, seconds, offset));
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

					return (new DateTime (day, month, year, hours, minutes, seconds, -offset));
				}
				
				return (new DateTime (day, month, year, hours, minutes, seconds, false));
			}

			throw new ArgumentException ("Value is not in ISO date & time format");
		}

		/// <summary>
		/// Creates a <see cref="Date"/> instance based in the date component values
		/// of the current instance.
		/// </summary>
		/// <returns>A <see cref="Date"/> instance.</returns>
		public Date ToDate ()
		{
			return (new Date (dateValue, timeZone));
		}

		/// <summary>
		/// Creates a <see cref="Time"/> instance based in the date component values
		/// of the current instance.
		/// </summary>
		/// <returns>A <see cref="Time"/> instance.</returns>
		public Time ToTime ()
		{
			return (new Time (timeValue, timeZone));
		}

		/// <summary>
		/// Uses the timezone information to create a UTC normalised <b>DateTime</b>
		/// from the current instance.
		/// </summary>
		/// <returns>The normalised <b>DateTime</b> instance.</returns>
		public DateTime Normalize ()
		{
			// Already in UTC?
			if ((timeZone != null) && timeZone.IsUTC ()) return (this);
			
			int offset = ((timeZone != null) ? timeZone : ImplicitTimeZone).Offset;
			
			int dt = DayOfMonth;
			int mo = Month;
			int yr = Year;
			int hr = Hours   - offset / 60;
			int mn = Minutes - offset % 60;
			
			// Rolled into previous day?
			while (mn < 0) {
				mn += 60;
				if (--hr < 0) {
					hr = 23;
					if (--dt < 1) {
						if (--mo < 1) {
							mo = 12;
							--yr;
						}
						dt = MonthLength (mo, yr);
					}
				}
			}
			while (hr < 0) {
				hr += 24;
				if (--dt < 1) {
					if (--mo < 1) {
						mo = 12;
						--yr;
					}
					dt = MonthLength (mo, yr);
				}
			}
			
			// Rolled into next day?
			while (mn > 59) {
				mn -= 60;
				if (++hr > 23) {
					hr = 0;
					if (++dt > MonthLength (mo, yr)) {
						if (++mo > 12) {
							mo = 1;
							++yr;
						}
						dt = 1;
					}
				}
			}
			while (hr > 23) {
				hr -= 24;
				if (++dt > MonthLength (mo, yr)) {
					if (++mo > 12) {
						mo = 1;
						++yr;
					}
					dt = MonthLength (mo, yr);
				}
			}
			
			return (new DateTime (dt, mo, yr, hr, mn, Seconds, true));
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
		/// Returns the hash value of the date for hash based data structures and
		/// algorithms. 
		/// </summary>
		/// <returns>The hash value of the date.</returns>
		public override int GetHashCode()
		{
			if (timeZone != null)
				return (dateValue.GetHashCode() ^ timeValue.GetHashCode() ^ timeZone.GetHashCode ());
			else
				return (dateValue.GetHashCode() ^ timeValue.GetHashCode());
		}

		/// <summary>
		/// Determines if this <b>Date</b> instance and another <see cref="object"/>
		/// hold the same date.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns><c>true</c> if both instance represent the same date,
		/// <c>false</c> otherwise.</returns>
		public override bool Equals (object other)
		{
			return ((other is DateTime) && Equals (other as DateTime));
		}

		/// <summary>
		/// Determines if this <b>Date</b> instance and another hold the same
		/// date.
		/// </summary>
		/// <param name="other">The <b>Date</b> instance to compare with.</param>
		/// <returns><c>true</c> if both instance represent the same date,
		/// <c>false</c> otherwise.</returns>
		public bool Equals (DateTime other)
		{
			DateTime	lhs = Normalize ();
			DateTime	rhs = other.Normalize ();
			
			return (lhs.dateValue.Equals (rhs.dateValue) && lhs.timeValue.Equals (rhs.timeValue));
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
			return (CompareTo (other as DateTime));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <b>Date</b>.
		/// </summary>
		/// <param name="other">The <b>Date</b> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (DateTime other)
		{
			DateTime	lhs = Normalize ();
			DateTime	rhs = other.Normalize ();

			int	result = lhs.dateValue.CompareTo (rhs.dateValue);
			if (result != 0) return (result);
			return (lhs.timeValue.CompareTo (rhs.timeValue));
		}

		/// <summary>
		/// Converts the instance data members to a <see cref="string"/> representation
		/// that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
		public override string ToString ()
		{
			if (timeZone != null)
				return (dateValue.ToString() + "T" + timeValue.ToString() + timeZone.ToString());
			else
				return (dateValue.ToString() + "T" + timeValue.ToString());
		}

		/// <summary>
		/// Constructs a <b>DateTime</b> instance from its date, time and
		/// time zone components.
		/// </summary>
		/// <param name="dateValue">The <see cref="DateValue"/> component.</param>
		/// <param name="timeValue">The <see cref="TimeValue"/> component.</param>
		/// <param name="timeZone">The <see cref="TimeZone"/> component or <b>null</b>.</param>
		internal DateTime (DateValue dateValue, TimeValue timeValue, TimeZone timeZone)
			: base (timeZone)
		{
			this.dateValue = dateValue;
			this.timeValue = timeValue;
		}
	
		/// <summary>
		/// The date part of the <b>DateTime</b>.
		/// </summary>
		private readonly DateValue		dateValue;

		/// <summary>
		/// The time part of the <b>DateTime</b>.
		/// </summary>
		private readonly TimeValue		timeValue;
	}
}