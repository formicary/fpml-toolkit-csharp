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
using System.Collections;
using System.Text;
using System.Xml.Schema;

using HandCoded.Meta;
using HandCoded.Xml.Resolver;

using log4net;

namespace HandCoded.Xml.Writer
{
	/// <summary>
	/// The <b>SchemaSet</b> class hold a collection of ...
	/// </summary>
	public sealed class SchemaSet
	{
		/// <summary>
		/// Contains the <see cref="XmlSchemaSet"/>.
		/// </summary>
		public XmlSchemaSet XmlSchemaSet {
			get {
				if (schema == null) {
					Source [] sourceArray = new Source [sources.size()];
					sources.copyInto (sourceArray);

					try {
						schema = SchemaFactory.newInstance (XMLConstants.W3C_XML_SCHEMA_NS_URI).newSchema (sourceArray);
					}
					catch (SAXException error) {
						logger.log (Level.SEVERE, "Unexpected SAX Exception", error);
					}
				}
				return (schema);
			}
		}

		/// <summary>
		/// Constructs a <b>SchemaSet</b>.
		/// </summary>
		public SchemaSet ()
		{ }

		/// <summary>
		/// Resolve the location of the indicated <see cref="SchemaRelease"/> (and
		/// any that it imports) to the schema set using default XML catalog to
		/// resolve the schema location.
		/// </summary>
		/// <param name="release">The <see cref="SchemaRelease"/> to be added.</param>
		public void Add (SchemaRelease release)
		{
			Add (release, XmlUtility.DefaultCatalog);
		}
		
		/// <summary>
		/// Resolve the location of the indicated <see cref="SchemaRelease"/> (and
		/// any that it imports) to the schema set using the given XML catalog to
		/// resolve the schema location.
		/// </summary>
		/// <param name="release">The <see cref="SchemaRelease"/> to be added.</param>
		/// <param name="catalog">The <see cref="Catalog"/> to resolve with.</param>
		public void Add (SchemaRelease release, Catalog catalog)
		{
			Vector	imports = release.getImportSet ();
			
			for (Iterator cursor = imports.iterator (); cursor.hasNext ();) {
				SchemaRelease schema = (SchemaRelease) cursor.next ();

				try {
					StreamSource source = catalog.resolveUri (schema.getNamespaceUri());
					
					if (!schemas.contains (schema)) {
						if (source == null) {
							logger.severe ("Failed to resolve schema URI '" + schema.getNamespaceUri() + "'");
							source = new StreamSource (schema.getSchemaLocation ());
						}
						sources.Add (source);
						schemas.Add (schema);
						
						schema = null;
					}
				}
				catch (SAXException error) {
					logger.log (Level.SEVERE, "Unexpected SAX exception creating schema source", error);
					System.exit (2);
				}
			}
		}
			
		/**
		 * A <CODE>Logger</CODE> instance used to report serious errors.
		 * @since	TFP 1.0
		 */
		private static ILog			log
			= LogManager.GetLogger (typeof (SchemeSet));

		/**
		 * The set of <CODE>SchemaReleases</CODE> added to the set.
		 * @since	TFP 1.1
		 */
		private List			schemas		= new List ();
		
		/**
		 * The set of <CODE>StreamSource</CODE> instances for the schemas.
		 * @since	TFP 1.0
		 */
		private List			sources		= new List ();
		
		/**
		 * The compiled schema representation of the schemas.
		 * @since	TFP 1.0
		 */
		private XmlSchemaSet	schemaSet	= null;	
	}
}