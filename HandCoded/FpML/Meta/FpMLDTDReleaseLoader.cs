// Copyright (C),2005-2011 HandCoded Software Ltd.
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
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

using HandCoded.FpML.Schemes;
using HandCoded.Framework;
using HandCoded.Xml;

namespace HandCoded.FpML.Meta
{
    /// <summary>
    /// An instance of the <b>FpMLDTDReleaseLoader</b> class will
    /// extract the description of an FpML DTD based grammar from the bootstrap data
    /// file and construct a <see cref="DTDRelease"/> to hold it.
    /// </summary>
    public sealed class FpMLDTDReleaseLoader : HandCoded.Meta.DefaultDTDReleaseLoader
    {
        /// <summary>
        /// Extracts the data from the DOM tree below the indicated context
        /// <see cref="XmlElement"/> and create a suitable <see crel="Release"/>
        /// structure to add to the indicated <see cref="HandCoded.Meta.Specification"/>.
        /// </summary>
        /// <param name="specification">The owning <see cref="HandCoded.Meta.Specification"/>.</param>
        /// <param name="context">The context <see cref="XmlElement"/> containing data</param>
        /// <param name="loadedSchemas">A dictionary of all ready loaded schemas.</param>
	    public override void LoadData (HandCoded.Meta.Specification specification, XmlElement context,
            Dictionary<string, HandCoded.Meta.SchemaRelease> loadedSchemas)
	    {
		    new DTDRelease (specification, GetVersion (context),
				    GetPublicId (context), GetSystemId (context),
				    GetRootElement (context), GetSchemeDefaults (context),
				    GetSchemeCollection (context));
	    }

        /// <summary>
        /// Extracts the release's scheme defaults data from the XML section
        /// describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>A populated <see cref="SchemeDefaults"/> instance.</returns>
	    private SchemeDefaults GetSchemeDefaults (XmlElement context)
	    {
		    XmlNodeList list = XPath.Paths (context, "schemeDefault");
		    string [,] values	= new string [list.Count, 2];
    		
		    for (int index = 0; index < list.Count; ++index) {
			    XmlElement node = list [index] as XmlElement;
			    values [index, 0] = Types.ToToken (XPath.Path (node, "attribute"));
			    values [index, 1] = Types.ToToken (XPath.Path (node, "schemeUri"));
		    }

		    list = XPath.Paths (context, "defaultAttribute");
		    string [,] names	= new string [list.Count, 2];

		    for (int index = 0; index < list.Count; ++index) {
			    XmlElement node = list [index] as XmlElement;
			    names [index, 0] = Types.ToToken (XPath.Path (node, "attribute"));
			    names [index, 1] = Types.ToToken (XPath.Path (node, "default"));
		    }

		    return (new SchemeDefaults (values, names));
	    }
    	
        /// <summary>
        /// Build a <see cref="SchemeCollection"/> instance for the release using
	    /// the scheme filenames from the XML section describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>A populated <see cref="SchemeCollection"/> instance.</returns>
	    private SchemeCollection GetSchemeCollection (XmlElement context)
	    {
		    SchemeCollection schemes = new SchemeCollection ();
    		
		    foreach (XmlElement node in XPath.Paths (context, "schemes"))
			    schemes.Parse (Application.PathTo (Types.ToToken (node)));
    		
		    return (schemes);
	    }
    }
}