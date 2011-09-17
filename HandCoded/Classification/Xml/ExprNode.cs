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

namespace HandCoded.Classification.Xml
{
    /// <summary>
    /// An instance of a class derived from the abstract <b>ExprNode</b> class
    /// represents part of an expression used to classify a business object.
    /// </summary>
    abstract class ExprNode
    {
        /// <summary>
        /// Evaluates an criteria defined by this <b>ExprNode</b> against a
	    /// selected context object.
        /// </summary>
        /// <param name="context">The context for the evaluation.</param>
        /// <returns>A <c>boolean</c> indicating of the criteria was satisfied
        /// or not.</returns>
	    public abstract bool Evaluate (object context);
    }
}