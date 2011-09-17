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

using HandCoded.Meta;

namespace HandCoded.Classification.Xml
{
    /// <summary>
    /// 
    /// </summary>
    sealed class RangeNode : ExprNode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
	    public RangeNode (Specification specification, Release lower, Release upper)
	    {
		    this.specification = specification;
		    this.lower	   	   = (lower != null) ? HandCoded.FpML.Util.Version.Parse (lower.Version) : null;
		    this.upper	   	   = (upper != null) ? HandCoded.FpML.Util.Version.Parse (upper.Version) : null;
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
		    XmlDocument	document = ((XmlElement) context).OwnerDocument;
		    Release		release	 = specification.GetReleaseForDocument (document);
		    HandCoded.FpML.Util.Version	version	= HandCoded.FpML.Util.Version.Parse (release.Version);
    		
		    bool validMin = (lower != null) ? (version.CompareTo (lower) >= 0) : true;
		    bool validMax = (upper != null) ? (version.CompareTo (upper) <= 0) : true;
    		
		    return (validMin & validMax);
	    }

        /// <summary>
        /// The specification the document must match.
        /// </summary>
	    private readonly Specification	specification;
   	
        /// <summary>
        /// 
        /// </summary>
	    private readonly HandCoded.FpML.Util.Version lower;
    	
        /// <summary>
        /// 
        /// </summary>
	    private readonly HandCoded.FpML.Util.Version upper;
    }
}