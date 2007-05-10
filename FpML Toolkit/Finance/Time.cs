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
using System.Text;

namespace HandCoded.Finance
{
	/// <summary>
	/// The <b>Time</b> class provides an immutable representation for a
	/// simple time value accurate to seconds.
	/// </summary>
	public class Time : IComparable
	{
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
			if ((text.Length == 8) && (text [2] == ':') && (text [5] == ':') &&
				Char.IsDigit (text [0]) && Char.IsDigit (text [1]) &&
				Char.IsDigit (text [3]) && Char.IsDigit (text [4]) &&
				Char.IsDigit (text [6]) && Char.IsDigit (text [7]))
				return (new Time ((text [0] - '0') * 10 + (text [1] - '0'),
								  (text [3] - '0') * 10 + (text [4] - '0'),
								  (text [6] - '0') * 10 + (text [7] - '0')));

			throw new ArgumentException ("Time is not in HH:MM:SS format");
		}

		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour,
		/// minute and seconds values.
		/// </summary>
		/// <param name="hours">The hours (0-23)</param>
		/// <param name="minutes">The minutes (0-59)</param>
		/// <param name="seconds">The seconds (0-59)</param>
		/// <exception cref="ArgumentException">If the hours, minutes or seconds
		/// values are outside the correct range.</exception>
		public Time (int hours, int minutes, int seconds)
		{
			if ((hours   < 0) || (hours   > 23) ||
				(minutes < 0) || (minutes > 59) ||
				(seconds < 0) || (seconds > 59))
				throw new ArgumentException ("One or more of the arguments is incorrect");

			time = (hours * 60 + minutes) * 60 + seconds;
		}

		/// <summary>
		/// Constructs a <b>Time</b> instance based on the supplied hour,
		/// and minutes values.
		/// </summary>
		/// <param name="hours">The hours (0-23)</param>
		/// <param name="minutes">The minutes (0-59)</param>
		/// <exception cref="ArgumentException">If the hours or minutes
		/// values are outside the correct range.</exception>
		public Time (int hours, int minutes)
			: this (hours, minutes, 0)
		{ }

		/// <summary>
		/// Contains the hours component of the time values.
		/// </summary>
		public int Hours {
			get {
				return ((time / 3600) % 24);
			}
		}

		/// <summary>
		/// Contains the minutes component of the time value.
		/// </summary>
		public int Minutes {
			get {
				return ((time / 60) % 60);
			}
		}

		/// <summary>
		/// Contains the seconds component of the time value.
		/// </summary>
		public int Seconds {
			get {
				return (time % 60);
			}
		}

		/// <summary>
		/// Returns the hash value of the time for hash based data structures
		/// and algorithms.
		/// </summary>
		/// <returns>The hash value of this instance.</returns>
		public override int GetHashCode ()
		{
			return (time);
		}

		/// <summary>
		/// Determines if the <b>Time</b> instance and another <see cref="Object"/>
		/// hold the same value.
		/// </summary>
		/// <param name="other">The <see cref="Object"/> to compare with.</param>
		/// <returns></returns>
		public override bool Equals (object other)
		{
			return ((other is Time) && ((other as Time).time == time));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <b>Time</b>
		/// instance.
		/// </summary>
		/// <param name="other">The other <c>Time</c> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (Time other)
		{
			return (time - other.time);
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
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			int			hours	= Hours;
			int			minutes	= Minutes;
			int			seconds = Seconds;
		
			lock (buffer) {
				buffer.Length = 0;

				if (hours < 10)
					buffer.Append ('0');
				buffer.Append (hours);
				buffer.Append (':');
				if (minutes < 10)
					buffer.Append ('0');
				buffer.Append (minutes);
				buffer.Append (':');
				if (minutes < 10)
					buffer.Append ('0');
				buffer.Append (seconds);
			
				return (buffer.ToString ());
			}
		}

		/// <summary>
		/// A <see cref="StringBuilder"/> instance used during formatting.
		/// </summary>
		/// <remarks>Access to this member must be made thread safe.</remarks>
		private static StringBuilder	buffer	= new StringBuilder ();

		/// <summary>
		/// The time as a number of seconds from midnight.
		/// </summary>
		private int					time;
	}
}