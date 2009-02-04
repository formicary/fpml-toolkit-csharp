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
	/// Instances of the <b>TimeZone</b> represents offsets from UTC expressed
	/// as a number of hours and minutes.
	/// </summary>
	[Serializable]
	public sealed class TimeZone : IComparable
	{
		/// <summary>
		/// A <b>TimeZone</b> instance representing UTC.
		/// </summary>
		public static readonly TimeZone UTC = new TimeZone (0);
	
		/// <summary>
		/// The smallest allowed timezone offset value.
		/// </summary>
		public const int 	MIN_TIMEZONE_OFFSET	= -14 * 60;

		/// <summary>
		/// The largest allowed timezone offset value.
		/// </summary>
		public const int 	MAX_TIMEZONE_OFFSET =  14 * 60;

		/// <summary>
		/// Contains the <b>TimeZone</b> offset in minutes.
		/// </summary>
		public int Offset {
			get {
				return (offset);
			}
		}

		/// <summary>
		/// Constructs a <b>TimeZone</b> instance initialised with the
		/// offset for the default time zone where the application is executing.
		/// </summary>
		public TimeZone ()
			: this (System.TimeZone.CurrentTimeZone.GetUtcOffset (System.DateTime.Now).Minutes)
		{ }

		/// <summary>
		/// Constructs a <b>TimeZone</b> instance having a specified
		/// offset value.
		/// </summary>
		/// <param name="offset">The timezone offset in minutes.</param>
		public TimeZone (int offset)
		{
			if ((offset < MIN_TIMEZONE_OFFSET) || (offset > MAX_TIMEZONE_OFFSET))
				throw new ArgumentException ("Invalue TimeZone offset value");
			
			this.offset = offset;
		}

		/// <summary>
		/// Determines if the <b>TimeZone</b> is UTC.
		/// </summary>
		/// <returns><b>true</b> if the <b>TimeZone</b> is UTC,
		/// <b>false</b> otherwise.</returns>
		public bool IsUTC ()
		{
			return (Equals (UTC));
		}

		/// <summary>
		/// Return a formatted represetation of the <b>TimeZone</b>. If the
		/// offset is zero this will be a 'Z' otherwise it will be '+HH:MM' or
		/// '-HH:MM'.
		/// </summary>
		/// <returns>The formatted value of the <b>TimeZone</b>.</returns>
		public override string ToString ()
		{
			if (offset == 0) return ("Z");
			
			lock (buffer) {
				int 				value;
				
				buffer.Length = 0;
				
				if (offset < 0) {
					buffer.Append ('-');
					value = -offset;
				}
				else {
					buffer.Append ('+');
					value =  offset;
				}
			
				int hours 	= value / 60;
				int minutes = value % 60;
				
				buffer.Append ((char)('0' + hours / 10));
				buffer.Append ((char)('0' + hours % 10));
				buffer.Append (':');
				buffer.Append ((char)('0' + minutes / 10));
				buffer.Append ((char)('0' + minutes % 10));
				
				return (buffer.ToString ());
			}
		}

		/// <summary>
		/// Returns the hash value of the instance for hash based data structures
		/// and algorithms.
		/// </summary>
		/// <returns>The hash value for the <b>TimeZone</b>.</returns>
		public override int GetHashCode()
		{
 			 return (offset);
		}

		/// <summary>
		/// Determines if this <b>TimeZone</b> instance and another object
		/// hold the same time value.
		/// </summary>
		/// <param name="other">The other <see cref="object"/> to compare with.</param>
		/// <returns><b>true</b> if both instances are <b>TimeZone</b> instances
		/// and represent the same value, <b>false</b> otherwise.</returns>
		public override bool  Equals (object other)
		{
 			 return ((other is TimeZone) && Equals (other as TimeZone));
		}

		/// <summary>
		/// Determines if this instance and another <b>TimeZone</b> hold
		/// the same value.
		/// </summary>
		/// <param name="other">The other <b>TimeZone</b>.</param>
		/// <returns><b>true</b> if both instances represent the same value,
		///	<b>false</b> otherwise.</returns>
		public bool Equals (TimeZone other)
		{
			return (offset == other.offset);
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <see cref="object"/>.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (object other)
		{
			return (CompareTo (other as TimeZone));
		}

		/// <summary>
		/// Returns the result of comparing this instance to another <b>TimeZone</b>.
		/// </summary>
		/// <param name="other">The <b>TimeZone</b> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		public int CompareTo (TimeZone other)
		{
			return (offset - other.offset);
		}

		/**
		 * Buffer area used for formatting.
		 * @since	TFP 1.1
		 */
		private static StringBuilder	buffer	= new StringBuilder ();
		
		/**
		 * Represents an offset in minutes from UTC.
		 * @since	TFP 1.1
		 */
		private readonly int			offset;
	}
}