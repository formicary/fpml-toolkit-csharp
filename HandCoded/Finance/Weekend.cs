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
using System.Collections;

namespace HandCoded.Finance
{
	/// <summary>
	/// The <b>Weekend</b> class provides a mechanism to test if a <see cref="Date"/>
	/// falls on a weekend (e.g. non-working day). In traditionally Christian
	/// countries Saturday and Sunday are non-working but other religions have
	/// selected other days.
	/// </summary>
	public abstract class Weekend
	{
		/// <summary>
		/// The extent set of all <b>Weekend</b> instances.
		/// </summary>
		private static Hashtable	extent	= new Hashtable ();

		/// <summary>
		/// A <b>Weekend</b> instance that detects Islamic style
		/// (Thursday &amp; Friday) weekends.
		/// </summary>
		public static readonly Weekend	THU_FRI
			= new DelegatedWeekend ("THU/FRI", new WeekendDelegate (IsThuOrFri));

		/// <summary>
		/// A <b>Weekend</b> instance that detects Jewish style
		/// (Friday &amp; Saturday) weekends.
		/// </summary>
		public static readonly Weekend	FRI_SAT
			= new DelegatedWeekend ("FRI/SAT", new WeekendDelegate (IsFriOrSat));

		/// <summary>
		/// A <b>Weekend</b> instance that detects Christian style
		/// (Saturday &amp; Sunday) weekends.
		/// </summary>
		public static readonly Weekend	SAT_SUN
			= new DelegatedWeekend ("SAT/SUN", new WeekendDelegate (IsSatOrSun));

		/// <summary>
		/// The reference name for this <b>Weekend</b>.
		/// </summary>
		public string Name {
			get {
				return (name);
			}
		}

		/// <summary>
		/// Attempts to find a <b>Weekend</b> instance in the extent with the
		/// given reference name.
		/// </summary>
		/// <param name="name">The reference name for the required instance.</param>
		/// <returns>The matching <b>Weekend</b> instance or <c>null</c> if
		/// no match could be found.</returns>
		public static Weekend ForName (string name)
		{
			return (extent [name] as Weekend);
		}

		/// <summary>
		/// Determines if the given <see cref="Date"/> falls on a weekend.
		/// </summary>
		/// <param name="date">The <see cref="Date"/> to check.</param>
		/// <returns><c>true</c> if the date falls on a weekend, <c>false</c>
		/// otherwise.</returns>
		public abstract bool IsWeekend (Date date);

		/// <summary>
		/// Constructs a <b>Weekend</b> instance with the given name and adds
		/// it to the extent set.
		/// </summary>
		/// <param name="name">The name used to reference this instance.</param>
		protected Weekend (string name)
		{
			extent [this.name = name] = this;
		}

		/// <summary>
		/// The reference name of this instance.
		/// </summary>
		private	readonly string			name;

		/// <summary>
		/// Tests is a <see cref="Date"/> falls on a Thursday or a Friday. 
		/// </summary>
		/// <param name="date">The <see cref="Date"/> to check.</param>
		/// <returns><c>true</c> if the date falls on a weekend, <c>false</c>
		/// otherwise.</returns>
		private static bool IsThuOrFri (Date date)
		{
			int			day = date.Weekday;

			return ((day == Date.THURSDAY) || (day == Date.FRIDAY));
		}

		/// <summary>
		/// Tests is a <see cref="Date"/> falls on a Friday or a Saturday. 
		/// </summary>
		/// <param name="date">The <see cref="Date"/> to check.</param>
		/// <returns><c>true</c> if the date falls on a weekend, <c>false</c>
		/// otherwise.</returns>
		private static bool IsFriOrSat (Date date)
		{
			int			day = date.Weekday;

			return ((day == Date.FRIDAY) || (day == Date.SATURDAY));
		}

		/// <summary>
		/// Tests is a <see cref="Date"/> falls on a Saturday or a Sunday. 
		/// </summary>
		/// <param name="date">The <see cref="Date"/> to check.</param>
		/// <returns><c>true</c> if the date falls on a weekend, <c>false</c>
		/// otherwise.</returns>
		private static bool IsSatOrSun (Date date)
		{
			int			day = date.Weekday;

			return ((day == Date.SATURDAY) || (day == Date.SUNDAY));
		}
	}
}