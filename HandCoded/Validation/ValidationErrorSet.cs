// Copyright (C),2005-2006 HandCoded Software Ltd.
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
using System.Xml;

namespace HandCoded.Validation
{
    /// <summary>
    /// A <b>ValidationErrorSet</b> instance can be used to capture syntax or
    /// semantic validation errors generated during XML parsing or business rule
    /// evaluation.
    /// </summary>
    public sealed class ValidationErrorSet
    {
        /// <summary>
        /// Contains the number of <see cref="ValidationError"/> instances in
        /// the set.
        /// </summary>
 	    public int Count
	    {
            get {
                return (errors.Count);
            }
	    }

        /// <summary>
        /// Access the specified <see cref="ValidationError"/> within the set.
        /// </summary>
        /// <param name="index">The required item index.</param>
        /// <returns>The corresponding <see cref="ValidationError"/> instance.</returns>
        public ValidationError this [int index]
        {
            get {
                return (errors [index]);
            }
        }
 
        /// <summary>
        /// Constructs an empty <b>ValidationErrorSet</b>.
        /// </summary>
	    public ValidationErrorSet ()
	    { }
    	
        /// <summary>
        /// Clears the set so that it can be reused.
        /// </summary>
        public void Clear ()
	    {
		    errors.Clear ();
	    }
    	
        /// <summary>
        /// Adds the indicated <see cref="ValidationError"/> instance to the set.
        /// </summary>
        /// <param name="error">The <see cref="ValidationError"/> to store.</param>
	    public void AddError (ValidationError error)
	    {
		    errors.Add (error);
	    }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> from the parameters and adds it
	    /// to the set.
        /// </summary>
        /// <param name="code">The FpML defined reason code associated with the error.</param>
        /// <param name="line">The line number where the problem was detected.</param>
        /// <param name="column">The column number where the problem was detected.</param>
        /// <param name="description">A textual description of the problem detected.</param>
        /// <param name="ruleName">The code for the FpML validation rule that has failed.</param>
        /// <param name="additionalData">Any additional data that may assist in problem solving.</param>
	    public void AddError (String code, int line, int column, String description,
			    String ruleName, String additionalData)
	    {
		    AddError (new ValidationError (code, line, column, description, ruleName, additionalData));
	    }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> from the parameters and adds it
        /// to the set.
        /// </summary>
        /// <param name="code">The FpML defined reason code associated with the error.</param>
        /// <param name="context">The DOM <CODE>Node</CODE> containing the erroneous data.</param>
        /// <param name="description">A textual description of the problem detected.</param>
        /// <param name="ruleName">The code for the FpML validation rule that has failed</param>
        /// <param name="additionalData">Any additional data that may assist in problem solving.</param>
	    public void AddError (String code, XmlNode context, String description,
			    String ruleName, String additionalData)
	    {
		    AddError (new ValidationError (code, context, description, ruleName, additionalData));
	    }

        /// <summary>
        /// The list used to the hold the errors.
        /// </summary>
        private List<ValidationError>	errors 	= new List<ValidationError> ();
    }
}