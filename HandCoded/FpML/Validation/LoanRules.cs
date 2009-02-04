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
	/// The <b>LoanRules</b> class contains a <see cref="RuleSet"/>
	/// initialised with FpML defined validation rules for syndicated loan messages.
	/// </summary>
	public sealed class LoanRules : FpMLRuleSet
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
		/// A <b>Rule</b> that ensures that the effective date of a loan contract 
		/// is not after the start date of the interest period.
		/// </summary>
		/// <remarks>Applies to FpML 4.4 and later.</remarks>
		public static readonly Rule RULE01
			= new DelegatedRule (Preconditions.R4_4__LATER, "ln-1", Rule01);

		
		/// <summary>
		/// A <b>Rule</b> that ensures that if the floating rate index contains the string 'PRIME' 
		/// then the rate fixing date must be equal to the effective date.
		/// </summary>
		/// <remarks>Applies to FpML 4.4 and later.</remarks>
		public static readonly Rule RULE02
			= new DelegatedRule (Preconditions.R4_4__LATER, "ln-2", Rule02);
		
		/// <summary>
		/// A <b>Rule</b> that ensures that the rateFixingDate must not be after the startDate, 
		/// the startDate must not be after the endDate, and the rateFixingDate must not be after
		/// the endDate.
		/// </summary>
		/// <remarks>Applies to FpML 4.4 and later.</remarks>
		public static readonly Rule RULE03
			= new DelegatedRule (Preconditions.R4_4__LATER, "ln-3", Rule03);

		/// <summary>
		/// A <b>Rule</b> that ensures that if mandatoryCostRate doesn't exist and interestRate
		/// and margin and allInRate exist, then allInRate = margin + interestRate
		/// </summary>
		/// <remarks>Applies to FpML 4.4 and later.</remarks>
		public static readonly Rule RULE04
			= new DelegatedRule (Preconditions.R4_4__LATER, "ln-4", Rule04);

		/// <summary>
		/// A <b>Rule</b> that ensures that if mandatoryCostRate and interestRate and margin and
		/// allInRate exist, then allInRate = margin + interestRate + mandatoryCostRate
		/// </summary>
		public static readonly Rule RULE05
			= new DelegatedRule (Preconditions.R4_4__LATER, "ln-5", Rule05);

		public static readonly Rule RULE10
			= new DelegatedRule (Preconditions.R4_4__LATER, "ln-10", Rule10);

		// --------------------------------------------------------------------

		private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule01 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "LoanContract"), errorHandler));					
				
			return (
				  Rule01 (name, nodeIndex.GetElementsByName ("loanContract"), errorHandler));
		}
		
		private static bool Rule01 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		start		= XPath.Path (context, "currentInterestRatePeriod", "startDate");
				XmlElement		effective	= XPath.Path (context, "effectiveDate");
				
				if ((start == null) || (effective == null) || 
					GreaterOrEqual (ToDate (start), ToDate (effective))) continue;
									
				errorHandler ("305", context,
						"The effectiveDate must not be after the currentInterestRatePeriod/startDate",
						name, null);
				
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		public static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule02 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "DrawdownNotice"), errorHandler));					
				
			return (
					  Rule02 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "DrawdownNotice"), errorHandler));
		}
		
		private static bool Rule02 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				if (Exists (XPath.Path (context, "loanContract"))){
					XmlElement		effective	= XPath.Path (context, "loanContract", "effectiveDate");
					XmlElement		fixingDate	= XPath.Path (context, "loanContract", "currentInterestRatePeriod", "rateFixingDate");
					XmlElement		rateIndex	= XPath.Path (context, "loanContract", "currentInterestRatePeriod", "floatingRateIndex");
				
					if ((fixingDate != null) && (effective != null) && (ToToken (rateIndex).Contains("PRIME"))) {
						if (NotEqual (ToDate (fixingDate) , ToDate (effective))){
							errorHandler ("305", context,
								"If the floatingRateIndex contains the string 'PRIME' then the currentInterestRatePeriod/rateFixingDate must be the same as the effectiveDate",
								name, null);
							result = false;
						}
					}
				}
			}
			return (result);
		}
	
		// --------------------------------------------------------------------

		public static bool Rule03 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule03 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "InterestRatePeriod"), errorHandler));					
				
			return (
				  Rule03 (name, nodeIndex.GetElementsByName ("currentInterestRatePeriod"), errorHandler));
		}
		
		private static bool Rule03 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		end			= XPath.Path (context, "endDate");
				XmlElement		start		= XPath.Path (context, "startDate");
				XmlElement		fixingDate	= XPath.Path (context, "rateFixingDate");
			
				if ((start!= null) && (fixingDate != null) && Less (ToDate (start), ToDate (fixingDate))) {
					errorHandler ("305", context,
							"The rateFixingDate must not be after the startDate",
							name, null);
					result = false;
				}						
				if ((end != null) && (start !=null) && Less (ToDate (end), ToDate (start))) {
					errorHandler ("305", context,
							"The startDate must not be after the endDate",
							name, null);
					result = false;
				}
				if ((end != null) && (fixingDate !=null) && Less (ToDate (end), ToDate (fixingDate))) {
					errorHandler ("305", context,
							"The rateFixingDate must not be after the endDate",
							name, null);
					result = false;
				}
			}
			return (result);
		}
	
		// --------------------------------------------------------------------

		public static bool Rule04 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule04 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "InterestRatePeriod"), errorHandler));					
				
			return (
				  Rule04 (name, nodeIndex.GetElementsByName ("currentInterestRatePeriod"), errorHandler));
		}
		
		private static bool Rule04 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		costRate		= XPath.Path (context, "mandatoryCostRate");
				XmlElement		interestRate	= XPath.Path (context, "interestRate");
				XmlElement		margin			= XPath.Path (context, "margin");
				XmlElement		allInRate		= XPath.Path (context, "allInRate");
			
				if ((costRate == null) && (interestRate != null) && (margin != null) && (allInRate != null)){
					decimal allInRateValue		= ToDecimal (allInRate);
					decimal marginValue			= ToDecimal (margin);
					decimal interestRateValue	= ToDecimal (interestRate);
					decimal marginPlusInterest	= marginValue + interestRateValue;
					
					if (allInRateValue.CompareTo(marginPlusInterest) != 0)
						errorHandler ("305", context,
								"The allInRate must be equal to margin + interestRate",
								name, null);
						result = false;
				}						
			}
			return (result);
		}			

		// --------------------------------------------------------------------

		public static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule05 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "InterestRatePeriod"), errorHandler));					
				
			return (
				  Rule05 (name, nodeIndex.GetElementsByName ("currentInterestRatePeriod"), errorHandler));
		}
		
		private static bool Rule05 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		costRate		= XPath.Path (context, "mandatoryCostRate");
				XmlElement		interestRate	= XPath.Path (context, "interestRate");
				XmlElement		margin			= XPath.Path (context, "margin");
				XmlElement		allInRate		= XPath.Path (context, "allInRate");
			
				if ((costRate!= null) && (interestRate != null) && (margin != null) && (allInRate != null)){
					decimal allInRateValue		= ToDecimal (allInRate);
					decimal marginValue			= ToDecimal (margin);
					decimal interestRateValue	= ToDecimal (interestRate);
					decimal costRateValue		= ToDecimal (costRate);
					decimal marginPlusInterest	= marginValue + interestRateValue;
					decimal totalMarginPlusCost	= marginPlusInterest + costRateValue;
							
					if (allInRateValue.CompareTo (totalMarginPlusCost) != 0) {
						errorHandler ("305", context,
							"The allInRate must be equal to margin + interestRate + mandatoryCostRate",
							name, null);
						result = false;
					}
				}						
			}
			return (result);
		}
	
		// --------------------------------------------------------------------

		public static bool Rule10 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule10 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FacilityNotice"), errorHandler));					
				
			return (
					  Rule10 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FacilityNotice"), errorHandler));					
		}
		
		private static bool Rule10 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		facilityAmount	= XPath.Path (context, "facilityIdentifier", "originalCommitmentAmount");
				XmlElement		loanAmount		= XPath.Path (context, "facilityCommitmentPosition", "loanContractPosition", "loanContractIdentifier", "originalAmount");
				
				if ((facilityAmount != null) && (loanAmount != null) && IsSameCurrency (facilityAmount, loanAmount)){
					XmlElement originalCommitment	= XPath.Path (facilityAmount, "amount");
					XmlElement originalAmount		= XPath.Path (loanAmount, "amount");
					
					if (Less (ToDecimal (originalCommitment), ToDecimal (originalAmount))){
						errorHandler ("305", context,
								"The facilityIdentifier/originalCommitmentAmount/amount must be greater than or equal to the facilityCommitmentPosition/loanContractPosition/loanContractIdentifier/originalAmount/amount",
								name, null);		
						result = false;
					}
					
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = RuleSet.ForName ("LoanRules");

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private LoanRules ()
		{ }
	}
}