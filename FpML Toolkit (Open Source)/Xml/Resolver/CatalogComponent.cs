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
	/// The abstract <b>CatalogComponent</b> class defines the standard API
	/// provided by all catalog entry components.
	/// </summary>
	abstract class CatalogComponent
	{
		/// <summary>
		/// Contains the the parent <see cref="GroupEntry"/>.
		/// </summary>
		public GroupEntry Parent {
			get {
				return (parent);
			}
		}

		/// <summary>
		/// Contains the parent entry that represents the catalog as a whole.
		/// </summary>
		public CatalogEntry Catalog {
			get {
				for (GroupEntry entry = parent; entry != null; entry = entry.Parent)
					if (entry is CatalogEntry) return (entry as CatalogEntry);

				return (null);
			}
		}

		/// <summary>
		/// Converts the instance data members to a <see cref="String"/> representation
		/// that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="String"/> representation.</returns>
		public override String ToString ()
		{
			return (GetType ().Name + "[" + ToDebug () + "]");
		}

		/// <summary>
		/// Constructs a <b>CatalogComponent</b> instance.
		/// </summary>
		/// <param name="parent">The containing catalog element.</param>
		protected CatalogComponent (GroupEntry parent)
		{
			this.parent = parent;
		}

		/// <summary>
		/// Converts the instance's member values to <see cref="String"/> representations
		/// and concatenates them all together. This function is used by ToString and
		/// may be overriden in derived classes.
		/// </summary>
		/// <returns>The object's <see cref="String"/> representation.</returns>
		protected abstract String ToDebug ();

		protected Uri Resolve (Uri baseurl, Uri relative)
		{
			if (relative.IsAbsoluteUri || (baseurl == null))
				return (relative);
			else {
				// FIX
				return (new Uri (baseurl.ToString () + "/" + relative.ToString (), UriKind.RelativeOrAbsolute));
			}
		}

		/// <summary>
		/// The containing catalog element or <b>null</b>.
		/// </summary>
		private readonly GroupEntry		parent;
	}
}