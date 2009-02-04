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

using HandCoded.FpML.Schemes;

namespace HandCoded.FpML.Meta
{
	/// <summary>
	/// The <b>DTDRelease</b> adds FpML specific knowledge to the base
	/// <see cref="HandCoded.Meta.DTDRelease"/>. It ensures that the <c>FpML</c>
	/// element is automatically assigned the correct version identifier
	/// and holds a reference to a schemes description for the release.
	/// </summary>
	public sealed class DTDRelease : HandCoded.Meta.DTDRelease, ISchemeAccess
	{
		/// <summary>
		/// Constructs a <b>DTDRelease</b> instance describing a DTD
		/// based release of a particular <see cref="HandCoded.Meta.Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="HandCoded.Meta.Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="publicId">The public name for the DTD.</param>
		/// <param name="systemId">The system name for the DTD.</param>
		/// <param name="rootElement">The normal root element.</param>
		/// <param name="schemeDefaults">Scheme default URI information.</param>
		/// <param name="schemeCollection">The SchemeCollection of this release.</param>
		public DTDRelease (HandCoded.Meta.Specification specification, string version,
			string publicId, string systemId, string rootElement,
			SchemeDefaults schemeDefaults, SchemeCollection schemeCollection)
			: base (specification, version, publicId, systemId, rootElement)
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
		/// Creates a new instance the XML grammar represented by this instance
		/// using the indicated element name as the root element for the document.
		/// </summary>
		/// <param name="rootElement">The name of the root element.</param>
		/// <returns>A new <see cref="XmlDocument"/> instance.</returns>
		public override XmlDocument NewInstance (string rootElement)
		{
			XmlDocument		document = base.NewInstance (rootElement);
			XmlElement		element  = document.DocumentElement;

			element.SetAttribute ("version", null, Version);

			return (document);
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