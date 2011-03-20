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

using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
    /// <summary>
    ///  The <B>ContentPrecondition</B> checks if the document being processed
    /// contains specific elements determined from either their XML Schema type or
    /// from a list (if type information is not available).
    /// </summary>
    class ContentPrecondition : HandCoded.Validation.Precondition
    {
        /// <summary>
        /// Constructions a <b>ProductPrecondition</b> instance that checks
	    /// documents containing the indicated elements or types.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="types"></param>
        public ContentPrecondition (string [] elements, string [] types)
        {
            this.elements = elements;
            this.types = types;
        }

		/// <summary>
		/// Evaluates this <see cref="Precondition"/> against the contents of the
		/// indicated <see cref="NodeIndex"/>.
		/// </summary>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/></param>
        /// <param name="cache">A cache of previously evaluated precondition results.</param>
		/// <returns>A <see cref="bool"/> value indicating the applicability of this
		/// <see cref="Precondition"/> to the <see cref="XmlDocument"/>.</returns>
	    public override bool Evaluate (NodeIndex nodeIndex, Dictionary<Precondition, bool> cache)
	    {
		    if (nodeIndex.HasTypeInformation) {
			    string ns = FpMLRuleSet.DetermineNamespace (nodeIndex);
    			
			    foreach (string type in types) {
				    XmlNodeList list = nodeIndex.GetElementsByType (ns, type);
    				
				    if ((list != null) && (list.Count > 0)) return (true);
			    }
		    }
		    else {
			    foreach (String element in elements) {
				    XmlNodeList list = nodeIndex.GetElementsByName (element);
    				
				    if ((list != null) && (list.Count > 0)) return (true);
			    }
		    }
		    return (false);
	    }

        /// <summary>
        /// A list of element names to check for.
        /// </summary>
	    private readonly string []	    elements;
    	
        /// <summary>
        /// A list of type names to check for.
        /// </summary>
	    private readonly string []	    types;
    }
}