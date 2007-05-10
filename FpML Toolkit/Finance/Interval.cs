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
	/// An <b>Interval</b> is a length of time expressed as an integer multiple
	/// of some <see cref="Period"/>, for example five days or three months.
	/// </summary>
	public sealed class Interval
	{
		/// <summary>
		/// Constructs an <b>Interval</b> from the provided multiplier and time
		/// unit.
		/// </summary>
		/// <param name="multiplier">The time period multiplier.</param>
		/// <param name="period">The time period unit.</param>
		public Interval (int multiplier, Period period)
		{
			this.multiplier = multiplier;
			this.period     = period;
		}

		/// <summary>
		/// Constructs an <b>Interval</b> from the provided multiplier and time
		/// unit code.
		/// </summary>
		/// <param name="multiplier">The time period unit multiplier.</param>
		/// <param name="code">The timer period unit code</param>
		public Interval (int multiplier, string code)
			: this (multiplier, Period.ForCode (code))
		{ }

		/// <summary>
		/// Contains the time period multiplier.
		/// </summary>
		public int Multiplier {
			get {
				return (multiplier);
			}
		}

		/// <summary>
		/// Contains the time period unit.
		/// </summary>
		public Period Period {
			get {
				return (period);
			}
		}

		/// <summary>
		/// Determines if this <b>Interval</b> is an integer multiple of another.
		/// </summary>
		/// <param name="other">The other <b>Interval</b> instance.</param>
		/// <returns><c>true</c> if this instance is an integer multiple of the
		/// other <b>Interval</b>, <c>false</c> otherwise.</returns>
		public bool IsMultipleOf (Interval other)
		{
			int				value = 0;

			if (period == other.Period)
				value = multiplier;
			else if ((period == Period.WEEK) && (other.Period == Period.DAY))
				value = 7 * multiplier;
			else if ((period == Period.YEAR) && (other.Period == Period.MONTH))
				value = 12 * multiplier;

			return (((value / other.Multiplier) >= 1) &&
				    ((value % other.Multiplier) == 0));
		}

		/// <summary>
		/// Determines if an <b>Interval</b> will divide the time period delimited
		/// by two dates exactly.
		/// </summary>
		/// <param name="first">The first date.</param>
		/// <param name="last">The last date.</param>
		/// <returns><b>true</b> if the <b>Interval</b> exactly divides the two
		/// dates an integer number of times.</returns>
		public bool DividesDates (Date first, Date last)
		{
			int				multiplier	= this.multiplier;
			Period			period		= this.period;

			if (period == Period.WEEK) {
				period = Period.DAY;
				multiplier *= 7;
			}

			if (period == Period.TERM) {
				period = Period.MONTH;
				multiplier *= 3;
			}

			if (period == Period.YEAR) {
				if (first.Month		 != last.Month)			return (false);
				if (first.DayOfMonth != last.DayOfMonth)	return (false);

				return (((last.Year - first.Year) % multiplier) == 0);
			}

			if (period == Period.MONTH) {
				if (first.DayOfMonth != last.DayOfMonth)	return (false);

				return ((((last.Year - first.Year) * 12 + last.Month - first.Month) % multiplier) == 0);
			}

			return (((last.GetHashCode () - first.GetHashCode ()) % multiplier) == 0);
		}

		/// <summary>
		/// Produces a hash value for the instance.
		/// </summary>
		/// <returns>The hash value.</returns>
		public override int GetHashCode () 
		{
			return (period.GetHashCode () + multiplier);
		}

		/// <summary>
		/// Compares an <b>Interval</b> instance with another object to see if
		/// they contain the same information.
		/// </summary>
		/// <remarks>This routine takes into account the equivalence of certain
		/// time intervals (e.g. 1Y = 4T = 12M and 1W = 7D).</remarks>
		/// <param name="other">The <see cref="object"/> to compare with.</param>
		/// <returns>A <see cref="bool"/> value indicating equality.</returns>
		public override bool Equals (object other)
		{
			return ((other is Interval) && Equals (other as Interval));
		}

		/// <summary>
		/// Compares two <b>Interval</b> instance to see if they contain the
		/// same information.
		/// </summary>
		/// <remarks>This routine takes into account the equivalence of certain
		/// time intervals (e.g. 1Y = 12M and 1W = 7D).</remarks>
		/// <param name="other">The other <b>Interval</b> instance.</param>
		/// <returns>A <see cref="bool"/> value indicating equality.</returns>
		public bool Equals (Interval other)
		{
			if (period == other.period)
				return (multiplier == other.multiplier);

			// Handle 1Y == 12M equivalence
			if (period == Period.YEAR) {
				if (other.period == Period.MONTH)
					return ((multiplier * 12) == other.multiplier);
				return (false);
			}
            if (period == Period.MONTH) {
				if (other.period == Period.YEAR)
					return (multiplier == (other.multiplier * 12));
				if (other.period == Period.TERM)
					return (multiplier == (other.multiplier * 3));
				return (false);
			}

			// Handle 1W == 7D equivalence
			if (period == Period.WEEK) {
				if (other.period == Period.DAY)
					return ((multiplier * 7) == other.multiplier);
				return (false);
			}
            if (period == Period.DAY) {
				if (other.period == Period.WEEK)
					return (multiplier == (other.multiplier * 7));
				return (false);
			}

			return (false);
		}

		/// <summary>
		/// Creates a debugging string describing the instance.
		/// </summary>
		/// <returns>The debugging string.</returns>
		public override string ToString ()
		{
			return (multiplier + period.ToString ());
		}

		/// <summary>
		/// The time unit multiplier.
		/// </summary>
		private readonly int		multiplier;

		/// <summary>
		/// The time period unit.
		/// </summary>
		private readonly Period		period;
	}
}