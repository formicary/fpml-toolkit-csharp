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
using System.Net;
using System.Text;
using System.Xml;

namespace HandCoded.Xml.Resolver
{
	/// <summary>
	/// A <b>CatalogSet</b> instance contains a collection of <see cref="Catalog"/>
	/// instances that can be used to resolve XML entity references. The <b>XCatalogSet</b>
	/// passes each resolution request to each constituent <see cref="Catalog"/>
	/// until either a match is found or all the catalogs have been tried.
	/// </summary>
	public sealed class CatalogSet : XmlResolver
	{
		/// <summary>
		/// Contains the user credentials object.
		/// </summary>
		/// <remarks>This property is not used.</remarks>
		public override ICredentials Credentials {
			set	{
				throw new Exception ("The method or operation is not implemented.");
			}
		}

		/// <summary>
		/// Constructs a <b>CatalogSet</b> containing an empty catalog
		/// collection.
		/// </summary>
		public CatalogSet ()
		{ }

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
			throw new Exception ("The method or operation is not implemented.");
		}

		/// <summary>
		/// Adds a <see cref="Catalog"/> to the collection.
		/// </summary>
		/// <param name="catalog">The <see cref="Catalog"/> to add.</param>
		public void AddCatalog (Catalog catalog)
		{
			catalogs.Add (catalog);
		}

		/// <summary>
		/// Removes a <see cref="Catalog"/> from the collection.
		/// </summary>
		/// <param name="catalog">The <see cref="Catalog"/> to remove.</param>
		public void RemoveCatalog (Catalog catalog)
		{
			catalogs.Remove (catalog);
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
			Uri				result;

			foreach (Catalog catalog in catalogs)
				if ((result = catalog.ResolveUri (baseUri, relativeUri)) != null)
					return (result);
		
			return (null);
		}

		/// <summary>
		/// The <see cref="Catalog"/> instances that comprise the set.
		/// </summary>
		private List<Catalog>		catalogs	= new List<Catalog> ();
	}
}