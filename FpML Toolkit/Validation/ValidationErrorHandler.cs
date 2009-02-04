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
using System.Xml;

namespace HandCoded.Validation
{
	/// <summary>
	/// The <b>ValidationErrorHandler</b> delegate is used it inform of a validation
	/// rule failure and describes the location and nature of the error to allow
	/// correction. All of the arguments except the reason code are optional
	/// and a <c>null</c> value may be supplied.
	/// </summary>
	/// <param name="code">The FpML defined reason code associated with the error.</param>
	/// <param name="context">The DOM <see cref="XmlNode"/> containing the erroneous data.</param>
	/// <param name="description">A textual description of the problem detected.</param>
	/// <param name="ruleName">The code for the FpML validation rule that has failed.</param>
	/// <param name="additionalData">Any additional data that may assist in problem solving.</param>
	public delegate void ValidationErrorHandler (string code, XmlNode context, string description,
		string ruleName, string additionalData);
}