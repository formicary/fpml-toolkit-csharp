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
	/// A <b>DTDRelease</b> instance contains a meta-description of an XML
	/// <see cref="Specification"/> represented by an XML DTD.
	/// </summary>
	public class DTDRelease : Release, IDTD
	{
		/// <summary>
		/// Constructs a <b>DTDRelease</b> instance describing a DTD
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="publicId">The public name for the DTD.</param>
		/// <param name="systemId">The system name for the DTD.</param>
		/// <param name="rootElement">The normal root element.</param>
		public DTDRelease (Specification specification, string version,
			string publicId, string systemId, string rootElement)
			: base (specification, version, new string [] { rootElement })
		{
			this.publicId = publicId;
			this.systemId = systemId;
		}

		/// <summary>
		/// Constructs a <b>DTDRelease</b> instance describing a DTD
		/// based release of a particular <see cref="Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="publicId">The public name for the DTD.</param>
		/// <param name="systemId">The system name for the DTD.</param>
		/// <param name="rootElements">The set of possible root element.</param>
		public DTDRelease (Specification specification, string version,
			string publicId, string systemId, string [] rootElements)
			: base (specification, version, rootElements)
		{
			this.publicId = publicId;
			this.systemId = systemId;
		}

		/// <summary>
		/// Provides access to the default <see cref="XmlResolver"/> instance.
		/// </summary>
		public static XmlResolver DefaultResolver {
			get {
				return (defaultResolver);
			}
			set {
				defaultResolver = value;
			}
		}

		/// <summary>
		/// Contains the Public name for the DTD.
		/// </summary>
		public string PublicId {
			get {
				return (publicId);
			}
		}

		/// <summary>
		/// Contains the System name for the DTD.
		/// </summary>
		public string SystemId {
			get {
				return (systemId);
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
			XmlDocumentType	doctype = document.DocumentType;
			XmlElement		element = document.DocumentElement;

			if ((doctype != null) && (doctype.PublicId.Equals (publicId))) {
				if (element != null) {
					foreach (string name in RootElements) {
						if (element.LocalName.Equals (name))
							return (true);
					}
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

			document.XmlResolver = DefaultResolver;

			document.AppendChild (document.CreateDocumentType (rootElement, publicId, systemId, null));
			document.AppendChild (document.CreateElement (rootElement));

			return (document);
		}

		/// <summary>
		/// The default <see cref="XmlResolver"/> used to access DTDs.
		/// </summary>
		private static XmlResolver	defaultResolver	= null;			

		/// <summary>
		/// The public name for the DTD.
		/// </summary>
		private readonly string		publicId;

		/// <summary>
		/// The system name for the DTD.
		/// </summary>
		private readonly string		systemId;
	}
}