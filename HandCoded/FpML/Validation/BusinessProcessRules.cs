// Copyright (C),2005-2008 HandCoded Software Ltd.
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

using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>BusinessProcessRulesRules</b> class contains a <see cref="RuleSet"/>
	/// initialised with FpML defined validation rules for business process messages.
	/// </summary>
	public sealed class BusinessProcessRules : FpMLRuleSet
	{
		/// <summary>
		/// Contains the <see cref="RuleSet"/>.
		/// </summary>
		public static RuleSet Rules {
			get {
				return (rules);
			}
		}

        /// <summary>
        /// A <see cref="Rule"/> that ensures The @href attribute on a firstPeriodStartDate
	    /// must match the @id attribute of an element of type Party.
        /// </summary>
        /// <remarks>Applies to FpML 4.1 and later.</remarks>
        public static readonly Rule	RULE02
		    = new DelegatedRule (Preconditions.R4_1__LATER, "bp-2", new RuleDelegate (Rule02));

        //----------------------------------------------------------------------

		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule02 (name, nodeIndex, XPath.Paths (nodeIndex.GetElementsByName ("novation"), ".."), errorHandler));
		}
		
		private static bool Rule02 (string name, NodeIndex nodeIndex, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result 	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	    startDate	= XPath.Path (context, "novation", "firstPeriodStartDate");
				XmlAttribute	href;
				
				if ((startDate == null) || (href = startDate.GetAttributeNode ("href"))== null) continue;
				
				XmlElement		target	= nodeIndex.GetElementById (href.Value);
					
				if ((target == null) || !target.LocalName.Equals("party")) {
					errorHandler ("305", context,
						"The @href attribute on the firstPeriodStartDate must reference a party",
						name, href.Value);
					
					result = false;
				}
			}
			return (result);
		}

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = RuleSet.ForName ("BusinessProcessRules");

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private BusinessProcessRules ()
		{ }
	}
}