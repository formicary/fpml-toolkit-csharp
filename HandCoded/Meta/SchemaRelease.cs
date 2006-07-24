// Copyright (C),2005-2006 HandCoded Software Ltd.
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
using System.Xml;

namespace HandCoded.Meta
{
	/// <summary>
	/// The <b>SchemaRelease</b> class adds support for the <see cref="ISchema"/>
	/// interface to the base <see cref="Release"/> class.
	/// </summary>
	public class SchemaRelease : Release, ISchema
	{
		/// <summary>
		/// The W3C standard URI for XML Namespaces.
		/// </summary>
		public const string 	NAMESPACES_URL
			= "http://www.w3.org/2000/xmlns/";
	
		/// <summary>
		/// The W3C standard URI for XML Schema
		/// </summary>
		public const string		SCHEMA_URL
			= "http://www.w3.org/2001/XMLSchema";
	
		/// <summary>
		/// The W3C standard URI for XML Schema instances.
		/// </summary>
		public const string		INSTANCE_URL
			= "http://www.w3.org/2001/XMLSchema-instance";
	
		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="rootElement">The normal root element.</param>
		public SchemaRelease (Specification specification, string version,
			string namespaceUri, string schemaLocation,
			string preferredPrefix, string alternatePrefix,
			string rootElement)
			: base (specification, version, new string [] { rootElement })
		{
			this.namespaceUri    = namespaceUri;
			this.schemaLocation  = schemaLocation;
			this.preferredPrefix = preferredPrefix;
			this.alternatePrefix = alternatePrefix;
		}

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="rootElements">The set of possible root elements.</param>
		public SchemaRelease (Specification specification, string version,
			string namespaceUri, string schemaLocation,
			string preferredPrefix, string alternatePrefix,
			string [] rootElements)
			: base (specification, version, rootElements)
		{
			this.namespaceUri    = namespaceUri;
			this.schemaLocation  = schemaLocation;
			this.preferredPrefix = preferredPrefix;
			this.alternatePrefix = alternatePrefix;
		}

		/// <summary>
		/// Contains the schema's namespace URI.
		/// </summary>
		public string NamespaceUri
		{
			get {
				return (namespaceUri);
			}
		}

		/// <summary>
		/// Contains the schema's location.
		/// </summary>
		public string SchemaLocation
		{
			get {
				return (schemaLocation);
			}
		}

		/// <summary>
		/// Contains the schemas preferred namespace prefix.
		/// </summary>
		public string PreferredPrefix
		{
			get {
				return (preferredPrefix);
			}
		}

		/// <summary>
		/// Contains the schemas alternate namespace prefix.
		/// </summary>
		public string AlternatePrefix
		{
			get {
				return (alternatePrefix);
			}
		}

		/// <summary>
		/// Determines if the given <see cref="XmlDocument"/> is an instance of the XML
		/// grammar represented by this instance.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be tested.</param>
		/// <returns><c>true</c> if the <see cref="XmlDocument"/> is an instance of this
		/// <see cref="IGrammar"/>, <c>false</c> otherwise.</returns>
		public override bool IsInstance (XmlDocument document)
		{
			XmlElement		element = document.DocumentElement;

			if ((element != null) && (element.NamespaceURI.Equals (namespaceUri))) {
				foreach (string name in RootElements) {
					if (element.LocalName.Equals (name))
						return (true);
				}
			}
			return (false);
		}

		/// <summary>
		/// Creates a new instance the XML grammar represented by this instance
		/// using the indicated element name as the root element for the document.
		/// </summary>
		/// <param name="rootElement">The name of the root element.</param>
		/// <returns>A new <see cref="XmlDocument"/> instance.</returns>
		public override XmlDocument NewInstance (string rootElement)
		{
			XmlDocument		document = new XmlDocument ();
			XmlElement		element  = document.CreateElement (rootElement, namespaceUri);

			element.SetAttribute ("xmlns",	   NAMESPACES_URL, namespaceUri);
			element.SetAttribute ("xmlns:xsi", INSTANCE_URL);
			document.AppendChild (element);

			return (document);
		}

		/// <summary>
		/// The namespace URI for the schema.
		/// </summary>
		private readonly string		namespaceUri;

		/// <summary>
		/// The default schema location for the schema.
		/// </summary>
		private readonly string		schemaLocation;

		/// <summary>
		/// The preferred namespace prefix.
		/// </summary>
		private readonly string		preferredPrefix;

		/// <summary>
		/// The altername namespace prefix.
		/// </summary>
		private readonly string		alternatePrefix;
	}
}