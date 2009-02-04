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
using System.Xml;

namespace HandCoded.Xml.Resolver
{
	/// <summary>
	/// Creates an <see cref="Catalog"/> instance from the definition held in
	/// the indicated URL. If the file has been requested previously then the
	/// cached object is returned.
	/// </summary>
	public sealed class CatalogManager
	{
		/// <summary>
		/// Creates an <see cref="Catalog"/> instance from the definition held in
		/// the indicated URL. If the file has been requested previously then the
		/// cached object is returned.
		/// </summary>
		/// <param name="url">The catalog's URI.</param>
		/// <param name="validate">Determines if the catalog should be validated.</param>
		/// <returns>The <see cref="Catalog"/> instance created from the URL.</returns>
		public static Catalog Find (String url, bool validate)
		{
			
			if (catalogs.ContainsKey (url))
				return (catalogs [url]);

			Catalog 			catalog = null;	
			XmlReader			reader	= new XmlTextReader (url);
			Stack<GroupEntry>	stack	= new Stack<GroupEntry> ();

			while (reader.Read ()) {
				if (reader.NodeType == XmlNodeType.Element) {
					String localName = reader.LocalName;

					if (localName.Equals ("catalog")) {
						String prefer 		= reader.GetAttribute ("prefer");
						String xmlbase		= reader.GetAttribute ("xml:base");

						stack.Push ((catalog = new Catalog (url, prefer, xmlbase)).Definition);
					}
					else if (localName.Equals ("group")) {
						String prefer 		= reader.GetAttribute ("prefer");
						String xmlbase		= reader.GetAttribute ("xml:base");

						stack.Push (catalog.Definition.AddGroup (prefer, xmlbase));
					}
					else {
						GroupEntry group = stack.Peek ();

						if (localName.Equals ("public")) {
							String publicId 	= reader.GetAttribute ("publicId");
							String uri 			= reader.GetAttribute ("uri");
							String xmlbase		= reader.GetAttribute ("xml:base");

							group.AddPublic (publicId, uri, xmlbase);
						}
						else if (localName.Equals ("system")) {
							String systemId		= reader.GetAttribute ("systemId");
							String uri			= reader.GetAttribute ("uri");
							String xmlbase		= reader.GetAttribute ("xml:base");

							group.AddSystem (systemId, uri, xmlbase);
						}
						else if (localName.Equals ("rewriteSystem")) {
							String startString	= reader.GetAttribute ("systemIdStartString");
							String rewritePrefix= reader.GetAttribute ("rewritePrefix");

							group.AddRewriteSystem (startString, rewritePrefix);
						}
						else if (localName.Equals ("delegatePublic")) {
							String startString	= reader.GetAttribute ("publicIdStartString");
							String file			= reader.GetAttribute ("catalog");
							String xmlbase		= reader.GetAttribute ("xml:base");

							group.AddDelegatePublic (startString, file, xmlbase);
						}
						else if (localName.Equals ("delegateSystem")) {
							String startString	= reader.GetAttribute ("systemIdStartString");
							String file			= reader.GetAttribute ("catalog");
							String xmlbase		= reader.GetAttribute ("xml:base");

							group.AddDelegateSystem (startString, file, xmlbase);
						}
						else if (localName.Equals ("uri")) {
							String name			= reader.GetAttribute ("name");
							String uri			= reader.GetAttribute ("uri");
							String xmlbase		= reader.GetAttribute ("xml:base");
							
							group.AddUri (name, uri, xmlbase);
						}
						else if (localName.Equals ("rewriteUri")) {
							String startString	= reader.GetAttribute ("uriStartString");
							String rewritePrefix = reader.GetAttribute ("rewritePrefix");
							
							group.AddRewriteUri (startString, rewritePrefix);
						}
						else if (localName.Equals ("delegateUri")) {
							String startString	= reader.GetAttribute ("uriStartString");
							String file			= reader.GetAttribute ("catalog");
							String xmlbase		= reader.GetAttribute ("xml:base");

							group.AddDelegateUri (startString, file, xmlbase);
						}
						else if (localName.Equals ("nextCatalog")) {
							String file			= reader.GetAttribute ("catalog");
							String xmlbase		= reader.GetAttribute ("xml:base");

							group.AddNextCatalog (file, xmlbase);
						}
						else
							throw new Exception ("Unexpected element tag in XML catalog file");
					}
				}
				else if (reader.NodeType == XmlNodeType.EndElement) {
					String localName = reader.LocalName;

					if (localName.Equals ("catalog") || localName.Equals ("group"))
						stack.Pop ();
				}
			}
			reader.Close ();

			return (catalogs [url] = catalog);
		}

		/// <summary>
		/// Creates an <see cref="Catalog"/> instance from the definition held in
		/// the indicated URL. If the file has been requested previously then the
		/// cached object is returned.
		/// </summary>
		/// <param name="url">The catalog's URI.</param>
		/// <returns>The <see cref="Catalog"/> instance created from the URL.</returns>
		public static Catalog Find (String url)
		{
			return (Find (url, false));
		}

		/// <summary>
		/// The set of previously processed catalogs indexed by filename.
		/// </summary>
		private static Dictionary<String, Catalog>	catalogs
			= new Dictionary<String, Catalog> ();

		/// <summary>
		/// Ensures no instances can be constructed.
		/// </summary>
		private CatalogManager ()
		{ }
	}
}