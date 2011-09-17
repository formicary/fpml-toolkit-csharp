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
    sealed class InterpretedRefinableCategory : RefinableCategory
    {
        /// <summary>
        /// Construct an <see cref="AbstractCategory"/> with a given name.
        /// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
        /// <param name="name">The name of the <see cref="Category"/>.</param>
        /// <param name="expression">The internal representation of the expression.</param>
	    public InterpretedRefinableCategory (Classification classification, string name, ExprNode expression)
            : base (classification, name)
	    {
		    this.expression = expression;
	    }

        /// <summary>
        /// Construct an <see cref="RefinableCategory"/> that is a sub-classification
	    /// of other <see cref="Category"/> instances.
        /// </summary>
        /// <param name="classification">The owning <CODE>Classification</CODE>.</param>
        /// <param name="name">The name of the <CODE>Category</CODE>.</param>
        /// <param name="parents">The parent <CODE>Category</CODE> instances</param>
        /// <param name="expression">The internal representation of the expression.</param>
	    public InterpretedRefinableCategory (Classification classification, string name, Category [] parents, ExprNode expression)
		    : base (classification, name, parents)
        {
		    this.expression = expression;
	    }
    	
		/// <summary>
		/// Determines if this <b>RefinableCategory</b> (and its sub-categories)
		/// is applicable to the given <see cref="Object"/>.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns><c>true</c> if the <see cref="Object"/> is applicable,
		/// <c>false</c> otherwise.</returns>
	    protected override bool IsApplicable (object value)
	    {
		    return (expression.Evaluate (value));
	    }
    	
        /// <summary>
        /// The underlying expression used to differentiate.
        /// </summary>
	    private readonly ExprNode	expression;
    }
}