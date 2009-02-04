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
	/// Instances of the <b>TimeValue</b> are used to hold the time portion
	/// of <b>Time</b> and <b>DateTime</b> values.
	/// </summary>
	[Serializable]
	sealed class TimeValue : IImmutableTime, IComparable
	{
		/// <summary>
		/// A time value representing the first instant of the day.
		/// </summary>
		public static readonly TimeValue	START_OF_DAY	= new TimeValue (0,0);
		
		/// <summary>
		/// A time value representing the last instant of the day.
		/// </summary>
		public static readonly TimeValue	END_OF_DAY		= new TimeValue (24,0);

		/// <summary>
		/// Constructs a <b>TimeValue</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The hours (0-24)</param>
		/// <param name="minutes">The minutes (0-59)</param>
		/// <exception cref="ArgumentException">If the hours or minutes
		/// values are outside the correct range.</exception>
		public TimeValue (int hours, int minutes)
			: this (hours, minutes, 0m)
		{ }

		/// <summary>
		/// Constructs a <b>TimeValue</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The hours (0-24)</param>
		/// <param name="minutes">The minutes (0-59)</param>
		/// <param name="seconds">The seconds (0-59)</param>
		/// <exception cref="ArgumentException">If the hours, minutes or seconds
		/// values are outside the correct range.</exception>
		public TimeValue (int hours, int minutes, int seconds)
			: this (hours, minutes, (decimal) seconds)
		{ }

		/// <summary>
		/// Constructs a <b>TimeValue</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The hours (0-24)</param>
		/// <param name="minutes">The minutes (0-59)</param>
		/// <param name="seconds">The seconds (0-59.99)</param>
		/// <exception cref="ArgumentException">If the hours, minutes or seconds
		/// values are outside the correct range.</exception>
		public TimeValue (int hours, int minutes, decimal seconds)
		{
			if (hours == 24) {
				if ((minutes != 0) || (seconds != 0m))
					throw new ArgumentException ("Minutes and seconds must be zero in end-of-day value");
			}
			else {
				if ((hours < 0) || (hours > 23))
					throw new ArgumentException ("Invalid hours value");
				
				if ((minutes < 0) || (minutes > 59))
					throw new ArgumentException ("Invalid minutes value");
				
				if ((seconds < 0m) || (seconds >= 60m))
					throw new ArgumentException ("Invalid seconds value");
			}

			this.hours 	 = hours;
			this.minutes = minutes;
			this.seconds = seconds;
		}

		/// <summary>
		/// Contains the hours component of the time values.
		/// </summary>
		public int Hours {
			get {
				return (hours);
			}
		}

		/// <summary>
		/// Contains the minutes component of the time value.
		/// </summary>
		public int Minutes {
			get {
				return (minutes);
			}
		}

		/// <summary>
		/// Contains the seconds component of the time value.
		/// </summary>
		public decimal Seconds {
			get {
				return (seconds);
			}
		}

		/// <summary>
		/// Returns the hash value of the time for hash based data structures
		/// and algorithms.
		/// </summary>
		/// <returns>The hash value of this instance.</returns>
		public override int GetHashCode ()
		{
			return ((hours * 60 + minutes) * 60 ^ seconds.GetHashCode ());
		}

		/// <summary>
		/// Determines if this <b>TimeValue</b> instance and another object hold
		/// the same date.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns><b>true</b> if both instances represent the same date,
		///	<b>false</b> otherwise.</returns>
		public override bool Equals (object other)
		{
			return ((other is TimeValue) && Equals (other as TimeValue));
		}

		/// <summary>
		/// Determines if this <b>TimeValue</b> instance and another hold the
		/// same date.
		/// </summary>
		/// <param name="other">The <b>TimeValue</b> instance to compare with.</param>
		/// <returns><b>true</b> if both instances represent the same date,
		///	<b>false</b> otherwise.</returns>
		public bool Equals (TimeValue other)
		{
			return ((hours == other.hours) && (minutes == other.minutes)
						&& (seconds == other.seconds)); 
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <see cref="object"/>.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (object other)
		{
			return (CompareTo (other as TimeValue));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <b>TimeValue</b>.
		/// </summary>
		/// <param name="other">The <b>TimeValue</b> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (TimeValue other)
		{
			if (hours   != other.hours)   return (hours   - other.hours);
			if (minutes != other.minutes) return (minutes - other.minutes);
			return (seconds.CompareTo (other.seconds));
		}

		/// <summary>
		/// Converts the value of this <b>TimeValue</b> instance into a
		/// <see cref="string"/> for display in ISO format (e.g. HH:MM:SS[.S+]).
		/// </summary>
		/// <returns>A <see cref="string"/> in the partial ISO time format
		///	representing the time</returns>
		public override string ToString ()
		{
			int			secs = (int) seconds;
			decimal		fraction = seconds % 1m;
		
			lock (buffer) {
				buffer.Length = 0;

				buffer.Append ((char)('0' + hours / 10));
				buffer.Append ((char)('0' + hours % 10));
				buffer.Append (':');
				buffer.Append ((char)('0' + minutes / 10));
				buffer.Append ((char)('0' + minutes % 10));
				buffer.Append (':');
				buffer.Append ((char)('0' + seconds / 10));
				buffer.Append ((char)('0' + seconds % 10));

				if (fraction != 0m) {
					buffer.Append ('.');

					do {
						fraction *= 10m;
						buffer.Append ((char)('0' + ((int) fraction)));
						fraction %= 1m;
					} while (fraction != 0m);
				}

				return (buffer.ToString ());
			}
		}

		/// <summary>
		/// A <see cref="StringBuilder"/> instance used during formatting.
		/// </summary>
		/// <remarks>Access to this member must be made thread safe.</remarks>
		private static StringBuilder	buffer	= new StringBuilder ();

		/// <summary>
		/// The number of hours.
		/// </summary>
		private	readonly int			hours;
	
		/// <summary>
		/// The number of minutes.
		/// </summary>
		private readonly int			minutes;

		/// <summary>
		/// The number of seconds.
		/// </summary>
		private readonly decimal		seconds;
	}
}