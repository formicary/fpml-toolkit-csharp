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
	/// The <b>ImmutableTime</b> interface defines constants and methods provided
	/// by both the <see cref="Time"/> and <see cref="DateTime"/> classes (and an
	/// internal value representation).
	/// </summary>
	interface IImmutableTime
	{
		/// <summary>
		/// Contains the hours component of the time values.
		/// </summary>
		int Hours {
			get;
		}

		/// <summary>
		/// Contains the minutes component of the time value.
		/// </summary>
		int Minutes {
			get;
		}

		/// <summary>
		/// Contains the seconds component of the time value.
		/// </summary>
		decimal Seconds {
			get;
		}
	}
}
