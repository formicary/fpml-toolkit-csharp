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
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;

using HandCoded.Framework;

using log4net;

namespace HandCoded.Finance
{
	/// <summary>
	/// <b>Calendar</b> instances implement the rules for determining if a
	/// <see cref="Date"/> is a business day in the location it represents.
	/// <para>The rules for public holidays vary from country to country but
	/// with the exception of special occaisions (e.g. royal weddings,
	/// funerals, etc.) can usually be predicted years in advance.</para>
	/// </summary>
	/// <remarks>On first reference this class bootstraps an initial set of
	/// <b>Calendar</b> instances by processing a XML data file.</remarks>
	public abstract class Calendar
	{
		/// <summary>
		/// Contains an array of all the currently defined calendars.
		/// </summary>
		public static Calendar [] Calendars {
			get {
				Calendar []		result = new Calendar [extent.Count];

				extent.Values.CopyTo (result, 0);
				return (result);
			}
		}

		/// <summary>
		/// If a <b>Calendar</b> with the given name exists in the extent set
		/// then a reference to it is returned to the caller.
		/// </summary>
		/// <param name="name">The required <b>Calendar</b> name.</param>
		/// <returns>A reference to the <b>Calendar</b> instance or <c>null</c>
		/// if the name was not found.</returns>
		public static Calendar ForName (string name)
		{
			return (extent.ContainsKey (name) ? extent [name] : null);
		}

		/// <summary>
		/// The name of this <b>Calendar</b>.
		/// </summary>
		public string Name {
			get {
				return (name);
			}
		}

		/// <summary>
		/// Determines if the <see cref="Date"/> provided falls on a business
		/// day in this <b>Calendar</b> (e.g. not a holiday or weekend).
		/// </summary>
		/// <param name="date">The <see cref="Date"/> to be tested.</param>
		/// <returns><c>true</c> if the date is a business day, <c>false</c>
		/// otherwise.</returns>
		public abstract bool IsBusinessDay (Date date);

		/// <summary>
		/// Produces a debugging string describing the instance.
		/// </summary>
		/// <returns>The debugging string.</returns>
		public override string ToString ()
		{
			return (GetType ().FullName + " [" + ToDebug () + "]");
		}

		/// <summary>
		/// Constructs a <b>Calendar</b> instance with a given name.
		/// </summary>
		/// <param name="name">The reference name for the instance.</param>
		protected Calendar (string name)
		{
			if ((this.name = name) != null)
				extent [name] = this;
		}

		/// <summary>
		/// Produces a debugging string describing the state of the instance.
		/// </summary>
		/// <returns>The debugging string.</returns>
		protected virtual string ToDebug ()
		{
			return ("name=\"" + name + "\"");
		}

		/// <summary>
		/// A <see cref="ILog"/> instance used to report run-time problems.
		/// </summary>
		private static ILog			log
			= LogManager.GetLogger (typeof (Calendar));

		/// <summary>
		/// The set of all named <b>Calendar</b> instances.
		/// </summary>
		private static Dictionary<string, Calendar>	extent
            = new Dictionary<string, Calendar> ();

		/// <summary>
		/// The name of this <b>Calendar</b> instance.
		/// </summary>
		private readonly string		name;

		/// <summary>
		/// Parses the XML data file indicated by the URI to extract default sets
		/// of calendars.
		/// </summary>
		/// <param name="uri">The URI of the source XML document.</param>
		private static void ParseCalendars (string uri)
		{
			RuleBasedCalendar calendar	= null;
			XmlReader		reader		= XmlReader.Create (uri);

			while (reader.Read ()) {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					{
						if (reader.LocalName.Equals ("calendar")) {
							string		name	= reader ["name"];
							Weekend		weekend	= Weekend.ForName (reader ["weekend"]);

							if ((name != null) && (weekend != null))
								calendar = new RuleBasedCalendar (name, weekend);
							else
								calendar = null;							
						}
						else if (reader.LocalName.Equals ("fixed")) {
							string		name	 = reader ["name"];
							int			from	 = Int32.Parse (reader ["from"]);
							int			until	 = Int32.Parse (reader ["until"]);
							int			month	 = ConvertMonth (reader ["month"]);
							int			date	 = Int32.Parse (reader ["date"]);
							DateRoll	dateRoll = DateRoll.ForName (reader ["roll"]);

							if ((calendar != null) && (month != 0) && (dateRoll != null))
								calendar.AddRule (new CalendarRule.Fixed (name, from, until, month, date, dateRoll));
						}
						else if (reader.LocalName.Equals ("offset")) {
							string		name	 = reader ["name"];
							int			from	 = Int32.Parse (reader ["from"]);
							int			until	 = Int32.Parse (reader ["until"]);
							int			when	 = ConvertWhen (reader ["when"]);
							int			weekday	 = ConvertWeekday (reader ["weekday"]);
							int			month	 = ConvertMonth (reader ["month"]);

							if ((calendar != null) && (month != 0) && (when != 0) && (weekday != 0))
								calendar.AddRule (new CalendarRule.Offset (name, from, until, when, weekday, month));
						}
						else if (reader.LocalName.Equals ("easter")) {
							string		name	 = reader ["name"];
							int			from	 = Int32.Parse (reader ["from"]);
							int			until	 = Int32.Parse (reader ["until"]);
							int			offset	 = Int32.Parse (reader ["offset"]);

							if (calendar != null)
								calendar.AddRule (new CalendarRule.Easter (name, from, until, offset));
						}
						break;
					}
				}
			}
			reader.Close ();
		}

		private static int ConvertMonth (string name)
		{
			if (name.Equals ("JAN")) return (1);
			if (name.Equals ("FEB")) return (2);
			if (name.Equals ("MAR")) return (3);
			if (name.Equals ("APR")) return (4);
			if (name.Equals ("MAY")) return (5);
			if (name.Equals ("JUN")) return (6);
			if (name.Equals ("JUL")) return (7);
			if (name.Equals ("AUG")) return (8);
			if (name.Equals ("SEP")) return (9);
			if (name.Equals ("OCT")) return (10);
			if (name.Equals ("NOV")) return (11);
			if (name.Equals ("DEC")) return (12);
			
			return (0);
		}

		private static int ConvertWhen (string name)
		{
			if (name.Equals ("FIRST")) 	return (CalendarRule.Offset.FIRST);
			if (name.Equals ("SECOND")) return (CalendarRule.Offset.SECOND);
			if (name.Equals ("THIRD")) 	return (CalendarRule.Offset.THIRD);
			if (name.Equals ("FOURTH")) return (CalendarRule.Offset.FOURTH);
			if (name.Equals ("LAST")) 	return (CalendarRule.Offset.LAST);
			
			return (0);
		}

		private static int ConvertWeekday (string name)
		{
			if (name.Equals ("MON")) return (Date.MONDAY);
			if (name.Equals ("TUE")) return (Date.TUESDAY);
			if (name.Equals ("WED")) return (Date.WEDNESDAY);
			if (name.Equals ("THU")) return (Date.THURSDAY);
			if (name.Equals ("FRI")) return (Date.FRIDAY);
			if (name.Equals ("SAT")) return (Date.SATURDAY);
			if (name.Equals ("SUN")) return (Date.SUNDAY);
			
			return (0);
		}

		/// <summary>
		/// Initialises the <b>Calendar</b> cache by parsing a standard set of
		/// definitions held in a XML file distributed with the toolkit.
		/// </summary>
		static Calendar ()
		{
			log.Debug ("Bootstrapping");

			try {
				ParseCalendars (Application.PathTo (
                    ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.StandardCalendars"]));
			}
			catch (Exception error) {
				log.Fatal ("Unable to load standard calendar definitions", error);
			}

			log.Debug ("Completed");
		}
	}
}