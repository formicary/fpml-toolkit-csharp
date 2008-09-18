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
using System.Xml;

using HandCoded.FpML.Schemes;

namespace HandCoded.FpML.Meta
{
	/// <summary>
	/// The <b>SchemaRelease</b> adds FpML specific knowledge to the base
	/// <see cref="HandCoded.Meta.SchemaRelease"/>. It ensures that the <c>FpML</c>
	/// element is automatically assigned the correct version identifier and
	/// holds a reference to a schemes description for the release.
	/// </summary>
	public sealed class SchemaRelease : HandCoded.Meta.SchemaRelease, ISchemeAccess
	{
		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="HandCoded.Meta.Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="HandCoded.Meta.Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="rootElement">The normal root element.</param>
		/// <param name="schemeDefaults">Scheme default URI information.</param>
		/// <param name="schemeCollection">The SchemeCollection for this release.</param>
		public SchemaRelease (HandCoded.Meta.Specification specification, string version,
			string namespaceUri, string schemaLocation,	string preferredPrefix,
			string alternatePrefix, string rootElement, SchemeDefaults schemeDefaults,
			SchemeCollection schemeCollection)
			: base (specification, version, namespaceUri, schemaLocation,
				preferredPrefix, alternatePrefix, rootElement)
		{
			this.schemeDefaults   = schemeDefaults;
			this.schemeCollection = schemeCollection;
		}

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="HandCoded.Meta.Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="HandCoded.Meta.Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="rootElements">The possible root elements.</param>
		/// <param name="schemeDefaults">Scheme default URI information.</param>
		/// <param name="schemeCollection">The SchemeCollection for this release.</param>
		public SchemaRelease (HandCoded.Meta.Specification specification, string version,
			string namespaceUri, string schemaLocation,	string preferredPrefix,
			string alternatePrefix, string [] rootElements, SchemeDefaults schemeDefaults,
			SchemeCollection schemeCollection)
			: base (specification, version, namespaceUri, schemaLocation,
				preferredPrefix, alternatePrefix, rootElements)
		{
			this.schemeDefaults   = schemeDefaults;
			this.schemeCollection = schemeCollection;
		}

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="HandCoded.Meta.Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="HandCoded.Meta.Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="initialiser">The <see cref="HandCoded.Meta.IInstanceInitialiser"/>.</param>
		/// <param name="recogniser">The <see cref="HandCoded.Meta.ISchemaRecogniser"/>.</param>
		/// <param name="rootElement">The normal root element.</param>
		/// <param name="schemeDefaults">Scheme default URI information.</param>
		/// <param name="schemeCollection">The SchemeCollection for this release.</param>
		public SchemaRelease (HandCoded.Meta.Specification specification, string version,
			string namespaceUri, string schemaLocation,	string preferredPrefix,
			string alternatePrefix, HandCoded.Meta.InstanceInitialiser initialiser,
			HandCoded.Meta.SchemaRecogniser recogniser, string rootElement,
			SchemeDefaults schemeDefaults, SchemeCollection schemeCollection)
			: base (specification, version, namespaceUri, schemaLocation,
				preferredPrefix, alternatePrefix, initialiser, recogniser, rootElement)
		{
			this.schemeDefaults   = schemeDefaults;
			this.schemeCollection = schemeCollection;
		}

		/// <summary>
		/// Constructs a <b>SchemaRelease</b> instance describing a schema
		/// based release of a particular <see cref="HandCoded.Meta.Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="HandCoded.Meta.Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="namespaceUri">The namespace used to identify the schema.</param>
		/// <param name="schemaLocation">The default schema location.</param>
		/// <param name="preferredPrefix">The preferred prefix for the namespace.</param>
		/// <param name="alternatePrefix">The alternate prefix for the namespace.</param>
		/// <param name="initialiser">The <see cref="HandCoded.Meta.IInstanceInitialiser"/>.</param>
		/// <param name="recogniser">The <see cref="HandCoded.Meta.ISchemaRecogniser"/>.</param>
		/// <param name="rootElements">The possible root elements.</param>
		/// <param name="schemeDefaults">Scheme default URI information.</param>
		/// <param name="schemeCollection">The SchemeCollection for this release.</param>
		public SchemaRelease (HandCoded.Meta.Specification specification, string version,
			string namespaceUri, string schemaLocation,	string preferredPrefix,
			string alternatePrefix, HandCoded.Meta.InstanceInitialiser initialiser,
			HandCoded.Meta.SchemaRecogniser recogniser, string [] rootElements,
			SchemeDefaults schemeDefaults, SchemeCollection schemeCollection)
			: base (specification, version, namespaceUri, schemaLocation,
				preferredPrefix, alternatePrefix, initialiser, recogniser, rootElements)
		{
			this.schemeDefaults   = schemeDefaults;
			this.schemeCollection = schemeCollection;
		}

		/// <summary>
		/// Contains scheme default information
		/// </summary>
		public SchemeDefaults SchemeDefaults {
			get {
				return (schemeDefaults);
			}
		}

		/// <summary>
		/// Contains the scheme collection used for validation.
		/// </summary>
		public SchemeCollection SchemeCollection {
			get {
				return (schemeCollection);
			}
		}

		/// <summary>
		/// Scheme default information for this FpML release.
		/// </summary>
		private readonly SchemeDefaults		schemeDefaults;

		/// <summary>
		/// SchemeCollection used for validation.
		/// </summary>
		private readonly SchemeCollection	schemeCollection;
	}
}