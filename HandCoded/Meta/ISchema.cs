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

namespace HandCoded.Meta
{
	/// <summary>
	/// The <b>ISchema</b> interface provides access to information that
	/// describes an XML Schema based grammar.
	/// </summary>
	public interface ISchema
	{
		/// <summary>
		/// Contains the schema's namespace URI.
		/// </summary>
		string NamespaceUri {
			get;
		}

		/// <summary>
		/// Contains the schema's location.
		/// </summary>
		string SchemaLocation {
			get;
		}

		/// <summary>
		/// Contains the schemas preferred namespace prefix.
		/// </summary>
		string PreferredPrefix {
			get;
		}

		/// <summary>
		/// Contains the schemas alternate namespace prefix.
		/// </summary>
		string AlternatePrefix {
			get;
		}
	}
}