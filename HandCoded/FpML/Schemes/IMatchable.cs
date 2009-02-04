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
	/// The <b>IMatchable</b> defines a standard API for extracting values
	/// from a <see cref="Scheme"/> based on a regular expression.
	/// </summary>
	public interface IMatchable
	{
		/// <summary>
		/// Finds the subset of scheme values that match the provided regular
		/// expression. No particular ordering of the values is enforced.
		/// </summary>
		/// <param name="pattern">A regular expression.</param>
		/// <returns>An array containing the matching <see cref="Value"/> instances.</returns>
		Value [] Like (string pattern);
	}
}