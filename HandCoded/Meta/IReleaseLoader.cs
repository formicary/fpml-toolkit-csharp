// Copyright (C),2005-2010 HandCoded Software Ltd.
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
using System.Text;
using System.Xml;

namespace HandCoded.Meta
{
    /// <summary>
    /// Instances implementing the <b>ReleaseLoader</b> are created and used
    /// during the processing of the releases meta data file to load the data into
    /// an appropriate internal structure.
    /// </summary>
    public interface IReleaseLoader
    {
        /// <summary>
        /// Extracts the data from the DOM tree below the indicated context
        /// <see cref="XmlElement"/> and create a suitable <see crel="Release"/>
        /// structure to add to the indicated <see cref="Specification"/>.
        /// </summary>
        /// <param name="specification">The owning <see cref="Specification"/>.</param>
        /// <param name="context">The context <see cref="XmlElement"/> containing data</param>
        /// <param name="loadedSchemas">A dictionary of all ready loaded schemas.</param>
	    void LoadData (Specification specification, XmlElement context, Dictionary<String, SchemaRelease> loadedSchemas);
    }
}