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
using System.Collections.Generic;
using System.Text;

namespace HandCoded.Xml.Resolver
{
	/// <summary>
	/// The <CODE>DelegateUriEntry</CODE> class implements uri delegation.
	/// </summary>
	sealed class DelegateUriEntry : RelativeEntry, IUriRule
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="startString"></param>
		/// <param name="catalog"></param>
		/// <param name="xmlbase"></param>
		public DelegateUriEntry (GroupEntry parent, String startString, String catalog, String xmlbase)
			: base (parent, xmlbase)
		{
			this.startString  = startString;
			this.catalog = catalog;
		}
		
		/// <summary>
		/// Applys the rule instance to the public or system identifier in an
		/// attempt to locate the URI of a resource with can provide the required
		///	information.
		/// </summary>
		/// <param name="uri">The uri needing to be resolved.</param>
		/// <param name="catalogs">The stack of catalogs being processed.</param>
		/// <returns>A new URI if the rule was successfully applied, otherwise
		///	<b>null</b>.</returns>
		public String ApplyTo (String uri, Stack<GroupEntry> catalogs)
		{
			if (uri.StartsWith (startString))
				return (CatalogManager.Find (catalog).Definition.ApplyRules (uri, catalogs));

			return (null);
		}

		/// <summary>
		/// Converts the instance's member values to <see cref="String"/> representations
		/// and concatenates them all together. This function is used by ToString and
		/// may be overriden in derived classes.
		/// </summary>
		/// <returns>The object's <see cref="String"/> representation.</returns>
		protected override String ToDebug ()
		{
			return ("startString=\"" + startString + "\",catalog=\"" + catalog + "\"," + base.ToDebug ());
		}

		/// <summary>
		/// The uri prefix to match with.
		/// </summary>
		private readonly String		startString;
		
		/// <summary>
		/// The catalog to delegate to.
		/// </summary>
		private readonly String		catalog;
	}
}