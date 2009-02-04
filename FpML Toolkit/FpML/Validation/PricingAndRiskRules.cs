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
	/// The <b>PricingAndRiskRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the defined validation <see cref="Rule"/> instances for Pricing and Risk
	/// data structures.
	/// </summary>
	/// <remarks>All these rules have been relocated to the Reference rules set and
	/// are deprecated here.</remarks>
	public sealed class PricingAndRiskRules : FpMLRuleSet
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
		/// A <see cref="Rule"/> that ensures the generic/@href attribute must match the @id
		/// attribute of an element of type Asset.
		/// </summary>
		/// <remarks>Applies to all FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE01
			= new DelegatedRule ("pr-1", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the @href attribute must match the @id
		/// attribute of an element of type PricingStructure.
		/// </summary>
		/// <remarks>Applies to all FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE02
			= new DelegatedRule ("pr-2", new RuleDelegate (Rule02));

		// --------------------------------------------------------------------

		private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule01 (name, nodeIndex, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "PricingDataPointCoordinate"), errorHandler));

			return (Rule01 (name, nodeIndex, nodeIndex.GetElementsByName ("coordinate"), errorHandler));
		}
		
		private static bool Rule01 (string name, NodeIndex nodeIndex, XmlNodeList list,
			ValidationErrorHandler errorHandler)
		{
			bool		result 	= true;
			
			foreach (XmlElement	 context in list) {
				XmlElement		generic	= XPath.Path (context, "generic");
				XmlAttribute	href;
				XmlElement		target;
				
				if ((generic == null) ||
					((href = generic.GetAttributeNode ("href")) == null) ||
					((target = nodeIndex.GetElementById (href.Value)) == null)) continue;
				
				string targetName = target.LocalName;
				
				if (targetName.Equals ("basket") ||
					targetName.Equals ("cash") ||
					targetName.Equals ("commodity") ||
					targetName.Equals ("deposit") ||
					targetName.Equals ("bond") ||
					targetName.Equals ("convertibleBond") ||		
					targetName.Equals ("equity") ||
					targetName.Equals ("exchangeTradedFund") ||
					targetName.Equals ("index") ||
					targetName.Equals ("future") ||
					targetName.Equals ("fxRate") ||
					targetName.Equals ("loan") ||
					targetName.Equals ("mortgage") ||
					targetName.Equals ("mutualFund") ||
					targetName.Equals ("rateIndex") ||
					targetName.Equals ("simpleCreditDefautSwap") ||
					targetName.Equals ("simpleFra") ||
					targetName.Equals ("simpleIrSwap") ||
					targetName.Equals ("dealSummary") ||
					targetName.Equals ("facilitySummary")) continue;
				
				errorHandler ("305", context,
					"generic/@href must match the @id attribute of an element of type Asset",
					name, targetName);
				
				result = false;
			}
			return (result);
		}
		
		// --------------------------------------------------------------------

		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule02 (name, nodeIndex, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "PaymentCalculationPeriod"), errorHandler));

			return (Rule02 (name, nodeIndex, nodeIndex.GetElementsByName ("paymentCalculationPeriod"), errorHandler));
		}

		private static bool Rule02 (string name, NodeIndex nodeIndex, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result 	= true;
			
			foreach (XmlElement context in list) {
				XmlAttribute	href;
				XmlElement		target;
				
				if (((href = context.GetAttributeNode ("href")) == null) ||
					((target = nodeIndex.GetElementById (href.Value)) == null)) continue;
				
				string targetName = target.LocalName;
				
				if (targetName.Equals ("creditCurve") ||
					targetName.Equals ("fxCurve") ||
					targetName.Equals ("volatilityRepresentation") ||
					targetName.Equals ("yieldCurve")) continue;
				
				errorHandler ("305", context,
					"@href must match the @id attribute of an element of type PricingStructure",
					name, targetName);
				
				result = false;
			}
			return (result);
		}

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = RuleSet.ForName ("PricingAndRiskRules");

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private PricingAndRiskRules ()
		{ }
	}
}