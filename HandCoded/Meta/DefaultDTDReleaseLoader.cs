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

using HandCoded.Xml;

namespace HandCoded.Meta
{
    /// <summary>
    /// An instance of the <b>DefaultDTDReleaseLoader</b> class will
    /// extract the description of a DTD based grammar from the bootstrap data
    /// file and construct a <see cref="DTDRelease"/> to hold it.
    /// </summary>
    public class DefaultDTDReleaseLoader : IReleaseLoader
    {
        /// <summary>
        /// Extracts the data from the DOM tree below the indicated context
        /// <see cref="XmlElement"/> and create a suitable <see crel="Release"/>
        /// structure to add to the indicated <see cref="Specification"/>.
        /// </summary>
        /// <param name="specification">The owning <see cref="Specification"/>.</param>
        /// <param name="context">The context <see cref="XmlElement"/> containing data</param>
        /// <param name="loadedSchemas">A dictionary of all ready loaded schemas.</param>
	    public virtual void LoadData (Specification specification, XmlElement context,
            Dictionary<string, SchemaRelease> loadedSchemas)
	    {
		    new DTDRelease (specification, GetVersion (context),
				    GetPublicId (context), GetSystemId (context),
				    GetRootElement (context));
	    }
    	
        /// <summary>
        /// Extracts the release's version number string from the XML section
        /// describing the DTD.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The release version number string.</returns>
	    protected string GetVersion (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "version")));
	    }
    	
        /// <summary>
        /// Extracts the release's public name from the XML section
	    /// describing the DTD.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The public name string.</returns>
	    protected string GetPublicId (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "publicName")));
	    }
    	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns></returns>
	    protected string GetSystemId (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "systemId")));
	    }

        /// <summary>
        /// Extracts the release's system identifier from the XML section
	    /// describing the DTD.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The system identifier string.</returns>
   	    protected string GetRootElement (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "rootElement")));
	    }
    }
}