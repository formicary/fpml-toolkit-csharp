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
using System.Collections.Generic;
using System.Text;

namespace HandCoded.Finance
{
	/// <summary>
	/// The <b>ImmutableDate</b> interface defines methods provided by both
	/// the <see cref="Date"/> and <see cref="DateTime"/> classes (and an
	/// internal value representation).
	/// </summary>
	interface IImmutableDate
	{
		/// <summary>
		/// Contains the weekday.
		/// </summary>
		int Weekday {
			get;
		}

		/// <summary>
		/// Contains the day of the month (1 - 31).
		/// </summary>
		int DayOfMonth {
			get;
		}

		/// <summary>
		/// Contains the last day of the month (28 - 31)
		/// </summary>
		int LastDayOfMonth {
			get;
		}

		/// <summary>
		/// Contains <c>true</c> if the date falls at the end of the month.
		/// </summary>
		bool IsEndOfMonth {
			get;
		}

		/// <summary>
		/// Contains the day of the year (1 - 366)
		/// </summary>
		int DayOfYear {
			get;
		}

		/// <summary>
		/// Contains the month of the year (1 - 12)
		/// </summary>
		int Month {
			get;
		}
	
		/// <summary>
		/// Contains the year (1900 - 2099)
		/// </summary>
		int Year {
			get;
		}
	}
}