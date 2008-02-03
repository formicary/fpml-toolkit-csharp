// Copyright (C),2005-2008 HandCoded Software Ltd.
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

		/// <summary>
		/// Maps a URI to an object containing the actual resource. 
		/// </summary>
		/// <param name="absoluteUri">The URI returned from ResolveUri.</param>
		/// <param name="role">The current version does not use this parameter when resolving URIs.
		/// This is provided for future extensibility purposes. For example, this can be mapped to
		/// the xlink:role and used as an implementation specific argument in other scenarios. </param>
		/// <param name="ofObjectToReturn">The type of object to return. The current version only
		/// returns System.IO.Stream objects.</param>
		/// <returns>A Uri representing the absolute URI or a null reference if the relative URI
		/// cannot be resolved.</returns>
		public override Object GetEntity (Uri absoluteUri, string role, Type ofObjectToReturn)
		{
//			Console.Error.WriteLine ("!! absoluteUri=" + absoluteUri);
			return (File.Open (absoluteUri.LocalPath, FileMode.Open));
		}

		/// <summary>
		/// Rresolves the absolute URI from the base and relative URIs.
		/// </summary>
		/// <param name="baseUri">The base URI used to resolve the relative URI</param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative.
		/// If absolute, this value effectively replaces the baseUri value. If relative, it
		/// combines with the baseUri to make an absolute URI.</param>
		/// <returns></returns>
		public override Uri ResolveUri (Uri baseUri, string relativeUri)
		{
//			Console.Error.WriteLine ("!! baseUri=" + baseUri + " relativeUri=" + relativeUri);
		    String	result = definition.ApplyRules (relativeUri, new Stack<GroupEntry> ());	
	
			// No mapping in the catalog try against the base URL directly
			if (result == null) {
				try {
					if (baseUri != null) 
						return (new Uri (baseUri, relativeUri));
					else
						return (new Uri (relativeUri, UriKind.Absolute));
				}
				catch (UriFormatException) {
					Console.Error.WriteLine ("URI '" + relativeUri + "' could not be resolved");
				}
				return (null);
			}

			return (new Uri (result));
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