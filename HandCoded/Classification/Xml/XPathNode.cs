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
using System.Text;
using System.Xml;
using System.Xml.XPath;

using log4net;
using log4net.Config;

namespace HandCoded.Classification.Xml
{
    /// <summary>
    /// The <b>XPathNode</b> class implements an <see cref="ExprNode"/> that
    /// evaluates an XPath expression against a DOM tree to determine a matching
    /// classification has been found.
    /// </summary>
    sealed class XPathNode : ExprNode
    {
	    /**
	     * Constructs an <CODE>XPathNode</CODE> that will execute the indicated
	     * XPath expression on test <CODE>Element</CODE> instances. The path should
	     * use the prefix 'dyn' for elements that will be dynamically associated
	     * with a namespace derived from the context.
	     * 
	     * @param 	test			The XPath expression
	     * @since	TFP 1.6
	     */
	    public XPathNode (string test)
	    {
		    this.test = test;
	    }	

        /// <summary>
        /// Evaluates an criteria defined by this <b>ExprNode</b> against a
	    /// selected context object.
        /// </summary>
        /// <param name="context">The context for the evaluation.</param>
        /// <returns>A <c>boolean</c> indicating of the criteria was satisfied
        /// or not.</returns>
	    public override bool Evaluate (object context)
	    {
            XmlElement  element = context as XmlElement;

            try {
                XmlNamespaceManager nsManager = new XmlNamespaceManager (new NameTable ());

                if ((element.NamespaceURI != null) && (element.NamespaceURI.Length > 0))
                    nsManager.AddNamespace ("dyn", element.NamespaceURI);

                XPathNavigator navigator = element.CreateNavigator ();

                return (ToBool (navigator.Evaluate(test, nsManager)));
            }
		    catch (Exception error) {
                log.Fatal ("Failed to evaluate XPath (" + test + ")", error);
            }
            return (false);
	    }
    	
		/// <summary>
		/// The <see cref="ILog"/> instance used to record problems.
		/// </summary>
		private static ILog		log
			= LogManager.GetLogger (typeof (XPathNode));

        /// <summary>
        /// The XPath expression that will be evaluated against the context element.
        /// </summary>
	    private readonly string		test;	

        /// <summary>
        /// Convert the value returned from an <see cref="XPathExpression"/> evaluation
        /// to a boolean value based on the W3C rules.
        /// </summary>
        /// <param name="result">The expression result to be converted.</param>
        /// <returns>The corresponding <b>bool</b> value.</returns>#
        /// <remarks>The conversion for <see cref="Double"/> values only works for
        /// regular values, not Nan or +/- zero.</remarks>
        private bool ToBool (Object result)
        {
            if (result is Boolean)
                return ((Boolean) result);
            else if (result is Double)
                return (((Double) result) != 0.0);
            else if (result is String)
                return (((String) result).Length > 0);
            else if (result is XPathNodeIterator)
                return ((result as XPathNodeIterator).Count != 0);

            return (false);
        }
    }
}