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
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace HandCoded.Xml.Resolver
{
	/// <summary>
	/// The <b>Catalog</b> provides a configurable <see cref="XmlResolver"/>
	/// for an XML parser.
	/// </summary>
	public sealed class Catalog : XmlResolver
	{
		/// <summary>
		/// Contains the URL associated with this catalog.
		/// </summary>
		public String Url {
			get {
				return (url);
			}
		}

		/// <summary>
		/// Not implemented.
		/// </summary>
		public override ICredentials Credentials {
		    set	{
		        throw new Exception ("The method or operation is not implemented.");
		    }
		}

		/// <summary>
		/// Constructs a new <b>Catalog</b> instance.
		/// </summary>
		/// <param name="url">The URL of this <b>Catalog</b>.</param>
		/// <param name="prefer">Optional <b>prefer</b> value.</param>
		/// <param name="xmlbase">Optional <b>xml:base</b> value.</param>
		public Catalog (String url, String prefer, String xmlbase)
		{
			this.url = url;

			if (!url.Contains (":"))
				url = Path.GetDirectoryName (Path.GetFullPath (url));

			definition = new CatalogEntry (prefer, (xmlbase != null) ? xmlbase : url);
		}

		public override Object GetEntity (Uri absoluteUri, string role, Type ofObjectToReturn)
		{
		    throw new Exception ("The method or operation is not implemented.");
		}

		public override Uri ResolveUri (Uri baseUri, string relativeUri)
		{
		    String	result = definition.ApplyRules (relativeUri, new Stack<GroupEntry> ());	
	
			return ((result != null) ? new Uri (result) : null);
		}

		/// <summary>
		/// The URL of this catalog.
		/// </summary>
		private readonly String			url;

		/// <summary>
		/// The component containing the catalog.
		/// </summary>
		private readonly CatalogEntry	definition;

		/// <summary>
		/// Contains the catalog definition.
		/// </summary>
		internal CatalogEntry Definition {
			get {
				return (definition);
			}
		}
	}
}