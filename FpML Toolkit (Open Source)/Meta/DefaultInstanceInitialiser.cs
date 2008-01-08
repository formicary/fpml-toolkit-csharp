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

using HandCoded.Meta;

namespace HandCoded.Meta
{
	/// <summary>
	/// The <b>DefaultInstanceInitialiser</b> class performs the population of
	/// attributes and values on the root element of a new document.
	/// </summary>
    public class DefaultInstanceInitialiser : IInstanceInitialiser
    {
        /// <summary>
        /// Constructs a <b>DefaultInstanceInitialiser</b> that performs the
        /// default actions.
        /// </summary>
        public DefaultInstanceInitialiser ()
        { }

        /// <summary>
        /// Initialises a new <see cref="XmlDocument"/> by adding required definitions to
        /// the structure indicated by its root <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="release">The <see cref="SchemaRelease"/> being initialised.</param>
        /// <param name="root">The root <see cref="XmlElement"/> of the new document.</param>
        /// <param name="isDefaultNamespace"><b>true</b> if the default namespace is being initialised.</param>
        public void Initialise(SchemaRelease release, XmlElement root, bool isDefaultNamespace)
        {
            string namespaceUri = release.NamespaceUri;
            string preferredPrefix = release.PreferredPrefix;
            string schemaLocation = release.SchemaLocation;

			if (isDefaultNamespace)
				root.SetAttribute ("xmlns", SchemaRelease.NAMESPACES_URL, namespaceUri);
			else
				root.SetAttribute ("xmlns:" + preferredPrefix, SchemaRelease.NAMESPACES_URL, namespaceUri);
			
			String 	value = root.GetAttribute ("xsi:schemaLocation");

			if (value != null) value += " ";
			value += namespaceUri + " " + schemaLocation;
			root.SetAttribute ("xsi:schemaLocation", value);
        }
    }
}