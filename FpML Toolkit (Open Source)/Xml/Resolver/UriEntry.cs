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
	/// The <b>UriEntry</b> class implements simple URI matching.
	/// </summary>
	sealed class UriEntry : RelativeEntry, IUriRule
	{
		/// <summary>
		/// Constructs a <b>UriEntry</b> instance that will replace
		/// one URI with another.
		/// </summary>
		/// <param name="parent">The containing element.</param>
		/// <param name="name">URI to be matched.</param>
		/// <param name="uri">The replacement URI.</param>
		/// <param name="xmlbase">Optional <CODE>xml:base</CODE> value.</param>
		public UriEntry (GroupEntry parent, String name, String uri,
				String xmlbase)
			: base (parent, xmlbase)
		{
			this.name = name;
			this.uri  = uri;
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
			String				targetUri;
			String				systemUri;
			
			// Convert the target uri value to a URI
			try {
				if (uri.StartsWith ("file:"))
					targetUri = uri.Substring (5);
				else
					targetUri = Resolve (XmlBase, uri);
			}
			catch (UriFormatException error) {
				throw new Exception ("Failed to normalise target URI", error);
			}
		
			// Convert the catalog name value to a URI
			try {
				systemUri = Resolve (XmlBase, name);
			}
			catch (UriFormatException error) {
				throw new Exception ("Failed to normalise name", error);
			}
			
			// If they match then replace with the catalog URI
			if (systemUri.Equals (targetUri)) {
				try {
					return (Resolve (XmlBase, this.uri));
				}
				catch (UriFormatException error) {
					throw new Exception ("Failed to resolve new URI", error);
				}
			}

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
			return ("name=\"" + name + "\",uri=\"" + uri + "\"," + base.ToDebug ());
		}

		/// <summary>
		/// The URI to be matched against.
		/// </summary>
		private readonly String	name;

		/// <summary>
		/// The URI to map to.
		/// </summary>
		private readonly String	uri;
	}
}