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

using HandCoded.FpML.Schemes;

namespace HandCoded.FpML.Meta
{
	/// <summary>
	/// The <b>ISchemeAccess</b> interface provides access to an instances
	/// <see cref="SchemeDefaults"/> meta-description and its <see cref="SchemeCollection"/>.
	/// </summary>
	public interface ISchemeAccess
	{
		/// <summary>
		/// Contains scheme default information.
		/// </summary>
		SchemeDefaults SchemeDefaults {
			get;
		}

		/// <summary>
		/// Contains the scheme collection used for validation.
		/// </summary>
		SchemeCollection SchemeCollection {
			get;
		}
	}
}