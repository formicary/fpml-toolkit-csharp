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
using System.Text;

namespace HandCoded.Finance
{
	/// <summary>
	/// The <b>Time</b> class provides an immutable representation for a
	/// simple time value accurate to seconds.
	/// </summary>
	[Serializable]
	public sealed class Time : Temporal, IComparable
	{
		/// <summary>
		/// A constant <b>Time</b> instance representing the first instant of
		/// the day.
		/// </summary>
		public static readonly Time	START_OF_DAY
			= new Time (TimeValue.START_OF_DAY, null);
		
		/// <summary>
		/// A constant <b>Time</b> instance representing the last instant of
		/// the day.
		/// </summary>
		public static readonly Time	END_OF_DAY
			= new Time (TimeValue.END_OF_DAY, null);
		
		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour and
		/// minute values.
		/// </summary>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		public Time (int hours, int minutes)
			: this (new TimeValue (hours, minutes, 0m), null)
		{ }
		
		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		public Time (int hours, int minutes, int seconds)
			: this (new TimeValue (hours, minutes, seconds), null)
		{ }

		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		/// <param name="utc">Indicates UTC time zone.</param>
		public Time (int hours, int minutes, int seconds, bool utc)
			: this (new TimeValue (hours, minutes, seconds), utc ? TimeZone.UTC : null)
		{ }

		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59.99).</param>
		/// <param name="utc">Indicates UTC time zone.</param>
		public Time (int hours, int minutes, decimal seconds, bool utc)
			: this (new TimeValue (hours, minutes, seconds), utc ? TimeZone.UTC : null)
		{ }
		
		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59).</param>
		/// <param name="offset">The time zone offset.</param>
		public Time (int hours, int minutes, int seconds, int offset)
			: this (new TimeValue (hours, minutes, seconds), new TimeZone (offset))
		{ }
		
		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The number of hours (0-24).</param>
		/// <param name="minutes">The number of minutes (0-59).</param>
		/// <param name="seconds">The number of seconds (0-59.99).</param>
		/// <param name="offset">The time zone offset.</param>
		public Time (int hours, int minutes, decimal seconds, int offset)
			: this (new TimeValue (hours, minutes, seconds), new TimeZone (offset))
		{ }

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
		/// Parses a character string in the format HH:MM:SS and uses the extracted
		/// values to construct a <b>Time</b> instance.
		/// </summary>
		/// <param name="text">The character string to be parsed.</param>
		/// <returns>A <b>Time</b> instance constructed from the parsed data.</returns>
		/// <exception cref="ArgumentException">If the character string is not
		/// in the correct format or the hour, minute or second values are outside
		/// the correct range.</exception>
		public static Time Parse (string text)
		{
			int			limit = text.Length;
			int			index = 0;
			
			while (true) {
				// Extract time components
				if ((index >= limit) || !IsDigit (text [index])) break;
				int hours = (text [index++] - '0') * 10;
				if ((index >= limit) || !IsDigit (text [index])) break;
				hours += text [index++] - '0';
				
				if ((index >= limit) || (text [index++] != ':')) break;
				
				if ((index >= limit) || !IsDigit (text [index])) break;
				int minutes = (text [index++] - '0') * 10;
				if ((index >= limit) || !IsDigit (text [index])) break;
				minutes += text [index++] - '0';
				
				if ((index >= limit) || (text [index++] != ':')) break;
				
				int start = index;
				if ((index >= limit) || !IsDigit (text [index++])) break;
				if ((index >= limit) || !IsDigit (text [index++])) break;
			
				if ((index < limit) && (text [index] == '.')) {
					do {
						++index;
					} while ((index < limit) && IsDigit (text [index]));
				}
				decimal seconds = Decimal.Parse (text.Substring (start, index - start));
				
				// Detect UTC time zone
				if ((index < limit) && (text [index] == 'Z')) {
					return (new Time (hours, minutes, seconds, true));
				}
				
				// Detect time offsets
				if ((index < limit) && (text [index] == '+')) {
					++index;
					if ((index >= limit) || !IsDigit (text [index])) break; 
					int offset = (text [index++] - '0') * 600;
					if ((index >= limit) || !IsDigit (text [index])) break;
					offset += (text [index++] - '0') * 60;
					
					if ((index >= limit) || (text [index++] != ':')) break;
					
					if ((index >= limit) || !IsDigit (text [index])) break;
					offset += (text [index++] - '0') * 10;
					if ((index >= limit) || !IsDigit (text [index])) break;
					offset += (text [index++] - '0');

					return (new Time (hours, minutes, seconds, offset));
				}
					
				if ((index < limit)&& (text [index] == '-')) {
					++index;
					if ((index >= limit) || !IsDigit (text [index])) break; 
					int offset = (text [index++] - '0') * 600;
					if ((index >= limit) || !IsDigit (text [index])) break;
					offset += (text [index++] - '0') * 60;
					
					if ((index >= limit) || (text [index++] != ':')) break;
					
					if ((index >= limit) || !IsDigit (text [index])) break;
					offset += (text [index++] - '0') * 10;
					if ((index >= limit) || !IsDigit (text [index])) break;
					offset += (text [index++] - '0');

					return (new Time (hours, minutes, seconds, -offset));
				}
				
				return (new Time (hours, minutes, seconds, false));
			}

			throw new ArgumentException ("Value is not in ISO time format");
		}

		/// <summary>
		/// Returns the hash value of the date for hash based data structures and
		/// algorithms.
		/// </summary>
		/// <returns>The hash value for the time.</returns>
		public override int GetHashCode ()
		{
			if (timeZone != null)
				return (timeValue.GetHashCode () ^ timeZone.GetHashCode ());
			else
				return (timeValue.GetHashCode ());
		}

		/// <summary>
		/// Determines if the <b>Time</b> instance and another <see cref="Object"/>
		/// hold the same value.
		/// </summary>
		/// <param name="other">The <see cref="Object"/> to compare with.</param>
		/// <returns><b>true</b> if both instances are times and represent the
		///	same value, <b>false</b> otherwise.</returns>
		public override bool Equals (object other)
		{
			return ((other is Time) && Equals (other as Time));
		}

		/// <summary>
		/// Determines if the <b>Time</b> instance and another hold the same value.
		/// </summary>
		/// <param name="other">The <see cref="Time"/> to compare with.</param>
		/// <returns><b>true</b> if both instances are times and represent the
		///	same value, <b>false</b> otherwise.</returns>
		public bool Equals (Time other)
		{
			return (ToDateTime ().Equals (other.ToDateTime ()));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <see cref="Object"/>.
		/// </summary>
		/// <param name="other">The other <see cref="Object"/> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (Object other)
		{
			return (CompareTo (other as Time));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <b>Time</b>
		/// instance.
		/// </summary>
		/// <param name="other">The other <b>Time</b> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (Time other)
		{
			return (ToDateTime ().CompareTo (other.ToDateTime ()));
		}

		/// <summary>
		/// Converts the value of this <b>Time</b> instance into a <see cref="string"/>
		/// for display in full ISO format (e.g. HH:MM:SS[.S+][[Z|+HH:MM|-HH:MM]]).
		/// </summary>
		/// <returns>A <see cref="string"/> in the ISO time format representing the
		/// <b>Time</b> value.</returns>
		public override string ToString ()
		{
			if (timeZone != null)
				return (timeValue.ToString () + timeZone.ToString ());
			else
				return (timeValue.ToString ());
		}
		
		/// <summary>
		/// Constructs a <b>Time</b> from its time and time zone components.
		/// </summary>
		/// <param name="timeValue">The <see cref="TimeValue"/> component.</param>
		/// <param name="timeZone">The <see cref="TimeZone"/> component or <b>null</b></param>
		internal Time (TimeValue timeValue, TimeZone timeZone)
			: base (timeZone)
		{			
			this.timeValue = timeValue;
		}
		
		/// <summary>
		/// Creates a <see cref="DateTime"/> instance from the current time.
		/// </summary>
		/// <returns>A <see cref="DateTime"/> instance.</returns>
		internal DateTime ToDateTime ()
		{
			return (new DateTime (EPOCH, timeValue, timeZone));
		}
		
		/// <summary>
		/// The date epoch used when converting to a <see cref="DateTime"/>.
		/// </summary>
		private static readonly DateValue 	EPOCH = new DateValue (31, 12, 1971);
		
		/// <summary>
		/// The time part of the <b>Time</b>
		/// </summary>
		private readonly TimeValue		timeValue;	
	}
}