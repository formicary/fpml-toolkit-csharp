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
using System.IO;
using System.Text;
using System.Xml;

using HandCoded.Meta;
using HandCoded.Xml;

using log4net;
using log4net.Config;

namespace HandCoded.Classification.Xml
{
    /// <summary>
    /// The <b>ClassificationLoader</b> class provides the logic to convert a
    /// classification defined within an XML file into a set of <see cref="Category"/>
    /// instance held in an indexed <b>Classification</b>.
    /// </summary>
    class ClassificationLoader
    {
        /// <summary>
        /// Parses the XML classification file indicated by the filename and
	    /// constructs an appropriate set of <see cref="Category"/> instances
	    /// held in an indexed <see cref="Classification"/>.
        /// </summary>
        /// <param name="filename">The name of the configuration file.</param>
        /// <returns>A populated <see cref="Classification"/> instance.</returns>
	    public static Classification Load (string filename)
	    {
		    Classification	classification	= new Classification ();
		    Dictionary<String, Category> idMap = new Dictionary<String, Category> ();

            try {
    		    FileStream	stream	= File.OpenRead (filename);
		        XmlDocument document = XmlUtility.NonValidatingParse (stream);

		        foreach (XmlElement context in DOM.GetChildElements (document.DocumentElement)) {
        			
			        if (context.LocalName.Equals ("abstractCategory")) {
				        string	id		= context.GetAttribute ("id");
				        string	name 	= context.GetAttribute ("name");
				        string	supers 	= context.GetAttribute ("superClasses");
				        Category category;
        				
				        if ((supers != null) && supers.Length != 0) {
					        String [] ids = supers.Split (' ');
					        Category [] objs = new Category [ids.Length];
        					
					        for (int count = 0; count < ids.Length; ++count)
						        objs [count] = idMap [ids [count]];
        					
					        category = new AbstractCategory (classification, name, objs);
				        }
				        else
					        category = new AbstractCategory (classification, name);

				        if ((id != null) && id.Length != 0)
					        idMap [id] = category;
			        }
			        else if (context.LocalName.Equals ("refinableCategory")) {
				        string	id		= context.GetAttribute ("id");
				        string	name 	= context.GetAttribute ("name");
				        string	supers 	= context.GetAttribute ("superClasses");
				        ExprNode expr	= LoadExpr (DOM.GetFirstChild (context));
				        Category category;
        				
				        if ((supers != null) && supers.Length != 0) {
					        string [] ids = supers.Split (' ');
					        Category [] objs = new Category [ids.Length];
        					
					        for (int count = 0; count < ids.Length; ++count)
						        objs [count] = idMap [ids [count]];
        					
					        category = new InterpretedRefinableCategory (classification, name, objs, expr);
				        }
				        else
					        category = new InterpretedRefinableCategory (classification, name, expr);

				        if ((id != null) && id.Length != 0)
					        idMap [id] = category;
			        }

                    stream.Close ();
		        }
		        return (classification);
            }
            catch (Exception error) {
                log.Fatal ("Failed to load classification from " + filename, error);
            }
            return (null);
	    }
    	
		/// <summary>
		/// The <see cref="ILog"/> instance used to record problems.
		/// </summary>
		private static ILog		log
			= LogManager.GetLogger (typeof (ClassificationLoader));

	    /**
	     * Ensures instances of <CODE>ClassificationLoader</CODE> cannot be constructed.
	     * @since	TFP 1.6
	     */
	    private ClassificationLoader ()
	    { }
    	
	    /**
	     * Recursively constructs the <CODE>ExprNode</CODE> instance used to
	     * represent the classification expressions.
	     * 
	     * @param 	element			The context <CODE>Element</CODE>.
	     * @return	A <CODE>ExprNode</CODE> representing the expression.
	     * @since	TFP 1.6
	     */
	    private static ExprNode LoadExpr (XmlElement element)
	    {
		    if (element.LocalName.Equals ("if")) {
			    XmlElement	testElement = XPath.Path (element, "test");
			    XmlElement	thenElement = XPath.Path (element, "then");
			    XmlElement	elseElement = XPath.Path (element, "else");
			    ExprNode testExpr;
			    ExprNode thenExpr;
			    ExprNode elseExpr;
    			
			    if (testElement.HasAttribute ("test"))
				    testExpr = new XPathNode (testElement.GetAttribute ("test"));
			    else
				    testExpr = LoadExpr (DOM.GetFirstChild (testElement));
    				
			    if (thenElement.HasAttribute ("test"))
				    thenExpr = new XPathNode (thenElement.GetAttribute ("test"));
			    else
				    thenExpr = LoadExpr (DOM.GetFirstChild (thenElement));

			    if (elseElement.HasAttribute ("test"))
				    elseExpr = new XPathNode (elseElement.GetAttribute ("test"));
			    else
				    elseExpr = LoadExpr (DOM.GetFirstChild (elseElement));

			    return (new IfNode (testExpr, thenExpr, elseExpr));
		    }
		    else if (element.LocalName.Equals ("release")) {
			    Specification	spec	= Specification.ForName (element.GetAttribute ("specification"));
			    Release			vers	= spec.GetReleaseForVersion (element.GetAttribute ("version"));
    			
			    return (new ReleaseNode (spec, vers));
		    }
		    else if (element.LocalName.Equals ("range")) {
			    Specification	spec	= Specification.ForName (element.GetAttribute ("specification"));
			    Release			lower	= null;
			    Release			upper	= null;
			    String			version;
    			
			    if ((version = element.GetAttribute ("lower")) != null)
				    lower = spec.GetReleaseForVersion (version);
    			
			    if ((version = element.GetAttribute ("upper")) != null)
				    upper = spec.GetReleaseForVersion (version);
    		
			    return (new RangeNode (spec, lower, upper));
		    }
		    else if (element.LocalName.Equals ("xpath")) {
			    String	test	= element.GetAttribute ("test");
    			
			    return (new XPathNode (test));
		    }
		    else return (null);
	    }
    }
}