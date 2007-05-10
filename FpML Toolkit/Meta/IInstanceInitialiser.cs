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

using System.Xml;

namespace HandCoded.Meta
{
    /// <summary>
    /// An instance of the <b>InstanceInitialiser</b> interface is used to
    /// initialise new schema based <see cref="XmlDocument"/> instances.
    /// </summary>
    interface IInstanceInitialiser
    {
        /// <summary>
        /// Initialises a new <see cref="XmlDocument"/> by adding required definitions to
	    /// the structure indicated by its root <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="release">The <see cref="SchemaRelease"/> being initialised.</param>
        /// <param name="root">The root <see cref="XmlElement"/> of the new document.</param>
        /// <param name="isDefaultNamespace"><b>true</b> if the default namespace is being initialised.</param>
        void Initialise(SchemaRelease release, XmlElement root, bool isDefaultNamespace);
    }
}