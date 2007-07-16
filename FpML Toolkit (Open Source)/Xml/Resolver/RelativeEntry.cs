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
	/// The abstract <b>RelativeEntry</b> class is the common base
	/// of all entries that resolve relative to <b>xml:base</b>.
	/// </summary>
	abstract class RelativeEntry : CatalogComponent
	{
		/// <summary>
		/// Contains the xml:base value, possibly obtained from an
		/// enclosing element.
		/// </summary>
		public String XmlBase {
			get {
				if (xmlbase != null)
					return (xmlbase);
				else if (Parent != null)
					return (Parent.XmlBase);
				else
					return (null);
			}
		}

		/// <summary>
		/// Converts the <b>xml:base</b> into a <see cref="Uri"/>
		/// for resolution operations.
		/// </summary>
		/// <returns>The base <see cref="Uri"/> for resolution.</returns>
		protected Uri BaseAsUri ()
		{
			return (new Uri (XmlBase, UriKind.RelativeOrAbsolute));
		}

		/// <summary>
		/// Constructs a <b>RelativeEntry</b> with holds the base URI used
		/// to resolve any local URI values.
		/// </summary>
		/// <param name="parent">The containing element.</param>
		/// <param name="xmlbase">Optional <b>xml:base</b> value.</param>
		protected RelativeEntry (GroupEntry parent, String xmlbase)
			: base (parent)
		{
			this.xmlbase = xmlbase;
		}

		/// <summary>
		/// Converts the instance's member values to <see cref="String"/> representations
		/// and concatenates them all together. This function is used by toString and
		/// may be overriden in derived classes.
		/// </summary>
		/// <returns>The object's <see cref="String"/> representation.</returns>
		protected override String ToDebug ()
		{
			return ("xml:base=" + ((xmlbase == null) ? "null" : ("\"" + xmlbase + "\"")));
		}

		/// <summary>
		/// The xml:base value for this component or <b>null</b>.
		/// </summary>
		private readonly String		xmlbase;
	}
}