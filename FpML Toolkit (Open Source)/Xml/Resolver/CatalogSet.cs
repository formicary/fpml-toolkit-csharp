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
	/// 
	/// </summary>
	public sealed class CatalogSet : XmlResolver
	{
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

		public override Object GetEntity (Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new Exception ("The method or operation is not implemented.");
		}

		/// <summary>
		/// Adds a <see cref="Catalog"/> to the collection.
		/// </summary>
		/// <param name="catalog"></param>
		public void AddCatalog (Catalog catalog)
		{
			catalogs.Add (catalog);
		}

		/// <summary>
		/// Removes a <see cref="Catalog"/> from the collection.
		/// </summary>
		/// <param name="catalog"></param>
		public void RemoveCatalog (Catalog catalog)
		{
			catalogs.Remove (catalog);
		}

		// TODO
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