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
	/// The abstract <b>CatalogEntry</b> class defines the standard API
	/// provided by all catalog entry components.
	/// </summary>
	public abstract class CatalogEntry
	{
		/// <summary>
		/// Contains the parent <see cref="CatalogEntry"/>
		/// </summary>
		public GroupEntry Parent {
			get {
				return (parent);
			}
		}

		/// <summary>
		/// Converts the instance data members to a <see cref="String"/> representation
		/// that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="String"/> representation.</returns>
		public override String ToString ()
		{
			return (getClass ().getName () + "[" + toDebug () + "]");
		}

		/// <summary>
		/// Contains a reference to the owning <see cref="Catalog"/>.
		/// </summary>
		protected Catalog Catalog {
			get {
				for (GroupEntry entry = parent; entry != null; entry = entry.Parent)
					if (entry is Catalog) return (entry as Catalog);
				return (null);
			}
		}

		/// <summary>
		/// Constructs a <b>CatalogRule</b> component.
		/// </summary>
		/// <param name="parent">The containing catalog element.</param>
		protected CatalogEntry (GroupEntry parent)
		{
			this.parent  = parent;
		}

		/// <summary>
		/// Converts the instance's member values to <see cref="String"/> representations
		/// and concatenates them all together. This function is used by ToString and
		/// may be overriden in derived classes.
		/// </summary>
		/// <returns></returns>
		protected abstract String ToDebug ();
		
		/**
		 * The containing catalog element or <CODE>null</CODE>.
		 */
		private readonly GroupEntry	parent;
	}
}