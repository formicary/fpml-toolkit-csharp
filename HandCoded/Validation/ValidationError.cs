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
using System.Xml;

using HandCoded.Xml;

namespace HandCoded.Validation
{
    /// <summary>
    /// Instances of the <b>ValidationError</b> class are used to hold the
    /// details of previous validation failure events.
    /// </summary>
    public sealed class ValidationError
    {
        /// <summary>
        /// Contains the error code classifier.
        /// </summary>
	    public String Code 
	    {
            get {
                return (code);
            }
	    }
    	
        /// <summary>
        /// Contains the error context, either an XPath expression or a lexical (e.g.
	    /// line and column) location.
        /// </summary>
	    public String Context
	    {
            get
            {
                return (context);
            }
	    }

        /// <summary>
        /// Contains the lexical context indicator.
        /// </summary>
	    public bool IsLexical
	    {
            get
            {
                return (lexical);
            }
	    }

        /// <summary>
        /// Contains the error description text.
        /// </summary>
	    public String Description
	    {
            get
            {
                return (description);
            }
	    }
    	
        /// <summary>
        /// Contains the the rule name (if any).
        /// </summary>
        /// <returns></returns>
	    public String RuleName
	    {
            get
            {
                return (ruleName);
            }
	    }

    	/// <summary>
    	/// Contains the additional data (if any).
    	/// </summary>
	    public String AdditionalData
	    {
            get
            {
                return (additionalData);
            }
	    }

        /// <summary>
        /// Constructs a <b>ValidationError</b> instance from the details provided
	    /// to a syntax error handler.
        /// </summary>
        /// <param name="code">The FpML defined reason code associated with the error.</param>
        /// <param name="line">The line number where the problem was detected.</param>
        /// <param name="column">The column number where the problem was detected.</param>
        /// <param name="description">A textual description of the problem detected.</param>
        /// <param name="ruleName">The code for the FpML validation rule that has failed.</param>
        /// <param name="additionalData">Any additional data that may assist in problem solving.</param>
	    public ValidationError (String code, int line, int column, String description,
		    String ruleName, String additionalData)
		    : this (code, line + "," + column, true, description, ruleName, additionalData)
	    { }

        /// <summary>
        /// Constructs a <b>ValidationError</b> instance from the details provided
	    /// to a semantic error handler.
        /// </summary>
        /// <param name="code">The FpML defined reason code associated with the error.</param>
        /// <param name="context">The DOM <CODE>Node</CODE> containing the erroneous data.</param>
        /// <param name="description">A textual description of the problem detected.</param>
        /// <param name="ruleName">The code for the FpML validation rule that has failed.</param>
        /// <param name="additionalData">Any additional data that may assist in problem solving.</param>
	    public ValidationError (String code, XmlNode context, String description,
		    String ruleName, String additionalData)
	        : this (code, XPath.ForNode (context), false, description, ruleName, additionalData)
	    { }
    	
        /// <summary>
        /// The number code classifier for the error.
        /// </summary>
	    private readonly String		code;
    	
        /// <summary>
        /// The context for the error, either an XPath or 'line,column' pair.
        /// </summary>
	    private readonly String		context;

    	/// <summary>
    	/// <b>true</b> if the context is lexical.
    	/// </summary>
	    private readonly bool   	lexical;

    	/// <summary>
    	/// A description of the error.
    	/// </summary>
	    private readonly String		description;

    	/// <summary>
    	/// The rule which detected the problem or <b>null</b>.
    	/// </summary>
	    private readonly String		ruleName;

        /// <summary>
        /// Additional data provided with the error or <b>null</b>.
        /// </summary>
	    private readonly String		additionalData;

    	/// <summary>
    	/// Constructs a <CODE>ValidationError</CODE> instance from the details
	    /// provided.
    	/// </summary>
        /// <param name="code">The FpML defined reason code associated with the error.</param>
    	/// <param name="context">The context location for the error.</param>
    	/// <param name="lexical">A flag indicating is the context is lexical.</param>
        /// <param name="description">A textual description of the problem detected.</param>
        /// <param name="ruleName">The code for the FpML validation rule that has failed.</param>
        /// <param name="additionalData">Any additional data that may assist in problem solving.</param>
	    private ValidationError (String code, String context, bool lexical,
			    String description,	String ruleName, String additionalData)
	    {
		    this.code 			= code;
		    this.context 		= context;
		    this.lexical 		= lexical;
		    this.description 	= description;
		    this.ruleName 		= ruleName;
		    this.additionalData	= additionalData;
	    }
    }
}