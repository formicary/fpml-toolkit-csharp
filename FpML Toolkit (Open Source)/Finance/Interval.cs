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
        /// <remarks>The calculation recognises that a week is seven days and that
        /// a year is twelve months. It also allows 1T to match any time period
        /// longer than a day and for any time period to be a multiple of 1 day.</remarks>
		/// <param name="other">The other <b>Interval</b> instance.</param>
		/// <returns><c>true</c> if this instance is an integer multiple of the
		/// other <b>Interval</b>, <c>false</c> otherwise.</returns>
		public bool IsMultipleOf (Interval other)
		{
			int				value = 0;

            // 1T is a positive integer multiple (>= 1) of any frequency
            if ((multiplier == 1) && (period == Period.TERM) && (other.multiplier >= 1))
                return (true);

            // Any period > 0 is a multiple of 1D
            if ((multiplier > 0) && (other.multiplier == 1) && (other.period == Period.DAY))
                return (true);

            // Handle 1W = 7D and 1Y = 12M or multiples thereof
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

			if (period == Period.TERM)
				return (multiplier == 1);

			if (period == Period.WEEK) {
				period = Period.DAY;
				multiplier *= 7;
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
		/// Calculates the result of adding another <b>Interval</b> to this
		/// one.
		/// </summary>
		/// <param name="other">The <b>Interval</b> to add.</param>
		/// <returns>A new <b>Interval</b> representing the combined time
		/// period.</returns>
		/// <exception cref="ArgumentException">If the two time periods
		/// cannot be combined.</exception>
		public Interval Plus (Interval other)
		{
			// One of the Intervals is zero length?
			if (multiplier == 0) return (other);
			if (other.multiplier == 0) return (this);

			// Both Intervals have the same unit?
			if (period == other.period)
				return (new Interval (multiplier + other.multiplier, period));
		
			// Otherwise check for equivalences
			if ((period == Period.YEAR) && (other.period == Period.MONTH))
				return (new Interval (12 * multiplier + other.multiplier, Period.MONTH));
			if ((period == Period.MONTH) && (other.period == Period.YEAR))
				return (new Interval (multiplier + 12 * other.multiplier, Period.MONTH));
			if ((period == Period.WEEK) && (other.period == Period.DAY))
				return (new Interval (7 * multiplier + other.multiplier, Period.MONTH));
			if ((period == Period.DAY) && (other.period == Period.WEEK))
				return (new Interval (multiplier + 7 * other.multiplier, Period.MONTH));
		
			throw new ArgumentException ("Intervals cannot be combined");
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
		/// time intervals (e.g. 1Y = 12M and 1W = 7D).</remarks>
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