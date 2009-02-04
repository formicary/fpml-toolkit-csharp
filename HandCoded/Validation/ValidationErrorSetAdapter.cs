// Copyright (C),2007 HandCoded Software Ltd.
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
using System.Xml;
using System.Xml.Schema;

namespace HandCoded.Validation
{
    /// <summary>
    /// The <b>ValidationErrorSetAdapter</b> class provides implementations of
    /// the delegate functions used by both the XML parse and the business rule
    /// validator that capture the errors into an <see cref="ValidationErrorSet"/>.
    /// </summary>
    public sealed class ValidationErrorSetAdapter
    {
        /// <summary>
        /// Construct a <b>ValidationErrorSetAdapter</b> which will capture
	    /// errors and store them in the indicated <see cref="ValidationErrorSet"/>.
        /// </summary>
        /// <param name="errorSet"></param>
	    public ValidationErrorSetAdapter (ValidationErrorSet errorSet)
	    {
		    this.errorSet = errorSet;
	    }

        /// <summary>
		/// Captures the details of a syntax error in the error set.
		/// </summary>
        /// <remarks>This function can be used as the delegate for an XML
        /// parser invocation.</remarks>
		/// <param name="sender">The object raising the error.</param>
		/// <param name="args">A description of the parser error.</param>
		private void SyntaxError (object sender, ValidationEventArgs args)
		{
            errorSet.AddError ("305",
                args.Exception.LineNumber, args.Exception.LinePosition,
                args.Exception.Message, null, null);
		}

        /// <summary>
        /// Captures the details of a semantic error in the error set.   
        /// </summary>
        /// <remarks>This function can be used as the delegate in
        /// validation invocations.</remarks>
        /// <param name="code">The FpML defined reason code associated with the error.</param>
        /// <param name="context">The DOM <see cref="XmlNode"/> containing the erroneous data.</param>
        /// <param name="description">A textual description of the problem detected.</param>
        /// <param name="ruleName">The code for the FpML validation rule that has failed.</param>
        /// <param name="additionalData">Any additional data that may assist in problem solving.</param>
        public void SemanticError (string code, XmlNode context, string description,
	        string ruleName, string additionalData)
        {
            errorSet.AddError (code, context, description, ruleName, additionalData);
        }

        /// <summary>
        /// The <see cref="ValidationErrorSet"/> to be populated with errors.
        /// </summary>
	    private readonly ValidationErrorSet	errorSet;
   }
}