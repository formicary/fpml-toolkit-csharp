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

namespace HandCoded.FpML.Schemes
{
	/// <summary>
	/// The <b>IEnumerable</b> interface defines an API for extracting a
	/// complete list of the possible values of a <see cref="Scheme"/> 
	/// instance.
	/// </summary>
	/// <remarks>The <b>IEnumerable</b> interface should only be supported 
	/// by closed domains.</remarks>
	public interface IEnumerable
	{
		/// <summary>
		/// Contains an array of all the possible <see cref="Value"/> instances
		/// supported by the <see cref="Scheme"/> in no particular order.
		/// </summary>
		Value [] Values {
			get;
		}

		/// <summary>
		/// Contains the number of values held in the <see cref="Scheme"/>.
		/// </summary>
		int Count {
			get;
		}
	}
}