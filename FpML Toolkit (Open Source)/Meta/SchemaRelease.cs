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
using System.Xml;

using log4net;

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
		/// <remarks>This constructor should be used when creating a description of a
		/// pure extension schema, i.e. one that contains no useable root elements.</remarks>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		public SchemaRelease (Specification specification, string version,
			string namespaceUri, string schemaLocation,
			string preferredPrefix, string alternatePrefix)
			: this (specification, version, namespaceUri, schemaLocation,
					preferredPrefix, alternatePrefix, (string []) null)
		{ }

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <remarks>This constructor should be used when creating a description of a
		/// pure extension schema, i.e. one that contains no useable root elements.</remarks>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="initialiser">The <see cref="IInstanceInitialiser"/>.</param>
		/// <param name="recogniser">The <see cref="ISchemaRecogniser"/>.</param>
		public SchemaRelease (Specification specification, string version,
			string namespaceUri, string schemaLocation,
			string preferredPrefix, string alternatePrefix,
			InstanceInitialiser initialiser, SchemaRecogniser recogniser)
			: this (specification, version, namespaceUri, schemaLocation,
					preferredPrefix, alternatePrefix,
					initialiser, recogniser, (string []) null)
		{ }

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <remarks>This constructor should be used when creating a description
		/// of a schema that has only a single root element.</remarks>
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
			: this (specification, version, namespaceUri, schemaLocation,
					preferredPrefix, alternatePrefix,
					new string [] { rootElement })
		{ }

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <remarks>This constructor should be used when creating a description
		/// of a schema that has only a single root element.</remarks>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="initialiser">The <see cref="IInstanceInitialiser"/>.</param>
		/// <param name="recogniser">The <see cref="ISchemaRecogniser"/>.</param>
		/// <param name="rootElement">The normal root element.</param>
		public SchemaRelease (Specification specification, string version,
			string namespaceUri, string schemaLocation,
			string preferredPrefix, string alternatePrefix,
			InstanceInitialiser initialiser, SchemaRecogniser recogniser,
			string rootElement)
			: this (specification, version, namespaceUri, schemaLocation,
					preferredPrefix, alternatePrefix, initialiser, recogniser,
					new string [] { rootElement })
		{ }

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <remarks>This constructor should be used when creating a description of a
		/// schema that has multiple root elements.</remarks>
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
			: this (specification, version, namespaceUri, schemaLocation,
					preferredPrefix, alternatePrefix,
					new DefaultInstanceInitialiser (),
					new DefaultSchemaRecogniser (),
					rootElements)
		{ }

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <remarks>This constructor should be used when creating a description of a
		/// schema that has multiple root elements.</remarks>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="initialiser">The <see cref="IInstanceInitialiser"/>.</param>
		/// <param name="recogniser">The <see cref="ISchemaRecogniser"/>.</param>
		/// <param name="rootElements">The set of possible root elements.</param>
		public SchemaRelease (Specification specification, string version,
			string namespaceUri, string schemaLocation,
			string preferredPrefix, string alternatePrefix,
			InstanceInitialiser initialiser, SchemaRecogniser recogniser,
			string [] rootElements)
			: base (specification, version, rootElements)
		{
			this.namespaceUri    = namespaceUri;
			this.schemaLocation  = schemaLocation;
			this.preferredPrefix = preferredPrefix;
			this.alternatePrefix = alternatePrefix;

			this.initialiser	= initialiser;
			this.recogniser		= recogniser;
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
		/// Contains all the imported schema releases.
		/// </summary>
		public List<SchemaRelease> ImportSet {
			get {
				return (FindAllImports (new List<SchemaRelease> ()));
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
			if (recogniser.Recognises (this, document)) {
				XmlElement			root = document.DocumentElement;

				// TODO: Improve import detection
				return (true);
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
			List<SchemaRelease>	releases	= new List<SchemaRelease> ();
			SchemaRelease		mainSchema	= null;
		
			FindAllImports (releases);
			foreach (SchemaRelease	release in releases) {
				if (release.HasRootElement (rootElement)) {
					if (mainSchema != null) {
						log.Fatal ("Multiple schemas define root element '" + rootElement + "'");
						return (null);
					}
					mainSchema = release;
				}
			}
			if (mainSchema == null) {
				log.Fatal ("No schema recognised '" + rootElement + "' as a root element.");
				return (null);
			}
			
			XmlDocument		document = new XmlDocument ();
			XmlElement		element  = document.CreateElement (rootElement, namespaceUri);

			element.SetAttribute ("xmlns:xsi", INSTANCE_URL);
			document.AppendChild (element);
			
			foreach (SchemaRelease	release in releases)
				release.initialiser.Initialise (release, element, release == mainSchema);

			return (document);
		}

		/// <summary>
		/// Creates a bi-directional reference between this <see cref="SchemaRelease"/>
		/// and the meta data for other instance that it imports.
		/// </summary>
		/// <param name="release">The imported <see cref="SchemaRelease"/>.</param>
		public void AddImport (SchemaRelease release)
		{
			this.imports.Add (release);
			release.importedBy.Add (this);
		}

		/// <summary>
		/// Breaks the bi-directional reference between this <see cref="SchemaRelease"/>
		/// and the indicated one.
		/// </summary>
		/// <param name="release">The <see cref="SchemaRelease"/> no longer imported.</param>
		public void RemoveImport (SchemaRelease release)
		{
			this.imports.Remove (release);
			release.importedBy.Remove (this);
		}

		/// <summary>
		/// <see cref="ILog"/> instance used to record problems.
		/// </summary>
		private static ILog			log
			= LogManager.GetLogger (typeof (SchemaRelease));

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

		/// <summary>
		/// The <see cref="InstanceInitialiser"/> used to build new documents.
		/// </summary>
		private readonly InstanceInitialiser	initialiser;

		/// <summary>
		/// The <see cref="SchemaRecogniser"/> used to determine document type.
		/// </summary>
		private readonly SchemaRecogniser		recogniser;
	
		/// <summary>
		/// The set of other <see cref="SchemaRelease"/> instances imported into this
		/// one.
		/// </summary>
		private List<SchemaRelease>	imports		= new List<SchemaRelease> ();

		/// <summary>
		/// The set of other <see cref="SchemaRelease"/> instance that import this
		/// one.
		/// </summary>
		private List<SchemaRelease>	importedBy	= new List<SchemaRelease> ();

		/// <summary>
		/// Recursively build a set of <b>SchemaRelease</b> instances
		/// containing this one and any that it imports with the least
		/// dependent first.
		/// </summary>
		/// <param name="releases">The <see cref="List&lt;SchemaRelease&gt;"/> of matches (so far).</param>
		/// <returns>The updated set of imported releases.</returns>
		private List<SchemaRelease> FindAllImports (List<SchemaRelease> releases)
		{
			if (!releases.Contains (this)) {
				// Add this schema to prevent infinte recursion
				releases.Add (this);
				
				foreach (SchemaRelease import in imports){
					import.FindAllImports (releases);
				
					// But reposition it after any schemas it imports
					releases.Remove (this);
					releases.Add (this);
				}
			}
			return (releases);
		}
	}
}