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
    /// An instance of the <b>DefaultSchemaReleaseLoader</b> class will
    /// extract the description of an XML Schema based grammar from the bootstrap data
    /// file and construct a <CODE>SchemaRelease</CODE> to hold it.
    /// </summary>
    public class DefaultSchemaReleaseLoader : IReleaseLoader
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
		    XmlAttribute	id 		= context.GetAttributeNode ("id");
    		
		    SchemaRelease release = new SchemaRelease (specification,
				    GetVersion (context), GetNamespaceUri (context),
				    GetSchemaLocation (context), GetPreferredPrefix (context),
				    GetAlternatePrefix (context), GetRootElements (context));
    		
		    HandleImports (release, context, loadedSchemas);
    		
		    if (id != null) loadedSchemas.Add (id.Value, release);
	    }
    	
        /// <summary>
        /// Extracts the release's version number string from the XML section
        /// describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The release version number string.</returns>
	    protected string GetVersion (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "version")));
	    }
    	
        /// <summary>
        /// Extracts the release's namespace URI from the XML section
	    /// describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The namespace URI.</returns>
	    protected string GetNamespaceUri (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "namespaceUri")));
	    }
    	
        /// <summary>
        /// Extracts the release's default schema location path from the XML section
	    /// describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The default schema location path.</returns>
	    protected string GetSchemaLocation (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "schemaLocation")));
	    }
    	
        /// <summary>
        /// Extracts the release's preferred prefix string from the XML section
	    /// describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The preferred prefix string.</returns>
	    protected string GetPreferredPrefix (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "preferredPrefix")));
	    }
    	
        /// <summary>
        /// Extracts the release's alternate prefix string from the XML section
        /// describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The alternate prefix string.</returns>
	    protected string GetAlternatePrefix (XmlElement context)
	    {
		    return (Types.ToToken (XPath.Path (context, "alternatePrefix")));
	    }
    	
        /// <summary>
        /// Extracts the release's root element names from the XML section
        /// describing the schema.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <returns>The set of root element names.</returns>
	    protected string [] GetRootElements (XmlElement context)
	    {
		    XmlNodeList	list = XPath.Paths (context, "rootElement");
		    string []	rootElements = new string [list.Count];
    		
		    for (int index = 0; index < list.Count; ++index)
			    rootElements [index] = Types.ToToken (list [index]);
    		
		    return (rootElements);
	    }
    	
        /// <summary>
        /// Connects this schema to any other schemas that it imports.
        /// </summary>
        /// <param name="release">The <see cref="SchemaRelease"/> for this schema.</param>
        /// <param name="context">The context <see cref="XmlElement"/> for the section.</param>
        /// <param name="loadedSchemas">A dictionay of previous bootstrapped schemas.</param>
	    protected void HandleImports (SchemaRelease release, XmlElement context, Dictionary<string, SchemaRelease> loadedSchemas)
	    {
		    foreach (XmlElement node in XPath.Paths (context, "import")) {
			    XmlAttribute href = node.GetAttributeNode ("href");
			    if (href != null)
				    release.AddImport ((SchemaRelease) loadedSchemas [href.Value]);
		    }
	    }
    }
}