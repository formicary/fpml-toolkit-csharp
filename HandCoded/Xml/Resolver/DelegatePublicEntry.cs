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
	/// The <b>DelegatePublic</b> class implements public identifier
	/// delegation.
	/// </summary>
	sealed class DelegatePublicEntry : RelativeEntry, IEntityRule
	{
		/// <summary>
		/// Constructs a <b>DelegatePublic</b> instance that will direct
		/// matching public identifier searches to a sub-catalog.
		/// </summary>
		/// <param name="parent">The containing catalog element.</param>
		/// <param name="prefix">The system identifier to be matched.</param>
		/// <param name="catalog">The URI of the sub-catalog.</param>
		/// <param name="xmlbase">The optional xml:base URI</param>
		public DelegatePublicEntry (GroupEntry parent, String prefix,
				String catalog, String xmlbase)
			: base (parent, xmlbase)
		{
			this.prefix  = prefix;
			this.catalog = catalog;
		}

		/// <summary>
		/// Applys the rule instance to the public or system identifier in an
		/// attempt to locate the URI of a resource with can provide the required
		/// information.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">The stack of catalogs being processed.</param>
		/// <returns>A new URI if the rule was successfully applied, otherwise
		/// <b>null</b>.</returns>
		public String ApplyTo (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			if (publicId.StartsWith (prefix))
				return (CatalogManager.Find (catalog).Definition.ApplyRules (publicId, systemId, catalogs));

			return (null);
		}

		/// <summary>
		/// Converts the instance data members to a <see cref="String"/> representation
		/// that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="String"/> representation.</returns>
		protected override String ToDebug ()
		{
			return ("prefix=\"" + prefix + "\",catalog=\"" + catalog + "\"," + base.ToDebug ());
		}

		/// <summary>
		/// The prefix to match against.
		/// </summary>
		private readonly String			prefix;

		/// <summary>
		/// The URI of the sub-catalog.
		/// </summary>
		private readonly String			catalog;
	}
}