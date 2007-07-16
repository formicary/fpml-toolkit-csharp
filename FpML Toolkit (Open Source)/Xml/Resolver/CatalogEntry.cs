// Copyright (C),2005-2007 HandCoded Software Ltd.
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

namespace HandCoded.Xml.Resolver
{
	/// <summary>
	/// An instance of the <b>CatalogEntry</b> class represents the
	/// &lt;catlog&gt; element within a parsed catalog file.
	/// </summary>
	sealed class CatalogEntry : GroupEntry
	{
		/// <summary>
		/// Constructs a <b>CatalogEntry</b> component.
		/// </summary>
		/// <param name="parent">The containing catalog element.</param>
		public CatalogEntry (String prefer, String xmlbase)
			: base (null, prefer, xmlbase)
		{ }

		/// <summary>
		/// Adds a group entry to the catalog.
		/// </summary>
		/// <param name="prefer">Optional <b>prefer</b> value.</param>
		/// <param name="xmlbase">Optional <b>xml:base</b> value.</param>
		/// <returns></returns>
		public GroupEntry AddGroup (String prefer, String xmlbase)
		{
			GroupEntry		result = new GroupEntry (this, prefer, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/// <summary>
		/// Adds a group entry to the catalog.
		/// </summary>
		/// <param name="prefer">Optional <b>prefer</b> value.</param>
		/// <returns></returns>
		public GroupEntry AddGroup (String prefer)
		{
			return (AddGroup (prefer, null));
		}

		/// <summary>
		/// The containing catalog element or <CODE>null</CODE>.
		/// </summary>
		private readonly GroupEntry	parent;
	}
}