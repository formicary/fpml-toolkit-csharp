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
using System.Xml;

namespace HandCoded.Meta
{
    /// <summary>
    /// An instance implementing the <b>SchemaRecognise</b> interface is used
    /// to compare an <see cref="XmlDocument"/> to a <see cref="SchemaRelease"/> to
    /// see if it could be an instance of it.
    /// </summary>
    public class DefaultSchemaRecogniser : ISchemaRecogniser
    {
		/// <summary>
		/// The <b>DefaultSchemaRecognise</b> class uses the following strategy
		/// to see if an XML <see cref="XmlDocument"/> could be an instance of the
		/// indicated <see cref="SchemaRelease"/>. The approach taken depends on
		/// the nature of the <see cre="SchemaRelease"/>:
		/// <UL>
		/// <LI>If the <see cref="SchemaRelease"/> defines one or more possible
		/// root elements then this implementation checks of the documents root
		/// element name and namespace match the schema.</LI>
		/// <LI>If the <see cref="SchemaRelease"/> is a pure extension schema (e.g.
		/// one containing only new types or substituting elements) then code looks
		/// for a reference to the schema's namespace on the root element. This
		/// might not be thorough enough for some documents.</LI>
		/// </UL>
		/// </summary>
        public DefaultSchemaRecogniser()
        { }

        /// <summary>
        /// Determines if the <see cref="XmlDocument"/> could be an instance of the
	    /// indicated <see cref="SchemaRelease"/>.
        /// </summary>
        /// <param name="release">The potential <see cref="SchemaRelease"/>.</param>
        /// <param name="document">The <see cref="XmlDocument"/> to be tested.</param>
        /// <returns><b>true</b> if the <see cref="XmlDocument"/> could be an
	    ///	instance of the indicated <see cref="SchemaRelease"/>.</returns>
        public bool Recognises (SchemaRelease release, XmlDocument document)
        {
			if (release.IsExtensionOnly) {
				// If the schema is a pure extension then check to see if any
				// xmlns attribute references its namespace.
				
				foreach (XmlAttribute attr in document.DocumentElement.Attributes) {
					if (attr.Name.StartsWith ("xmlns") && attr.Value.Equals (release.NamespaceUri))
						return (true);
				}
			}
			else {
				// If the schema declares root elements then check the name and
				// namespace matches.
				
				XmlElement			root	= document.DocumentElement;
				
				if (root != null) {
					String			namespaceUri = root.NamespaceURI;
					
					if ((namespaceUri != null) && namespaceUri.Equals (release.NamespaceUri)
							&& release.HasRootElement (root.LocalName))
						return (true);
				}
			}
			return (false);
       }
    }
}