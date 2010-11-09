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
	/// The <b>ReferenceRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the defined validation <see cref="Rule"/> instances for IDREF based
	/// intra-document references.
	/// </summary>
	public sealed class ReferenceRules : FpMLRuleSet
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
		/// A <b>Rule</b> that ensures an <b>AssetReference</b> correctly
		/// refers to an <b>Asset</b>.
		/// </summary>
		public static readonly Rule	RULE01
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-1",
					"AssetReference", new string [] {
						"assetReference", "definition", "underlyingAssetReference",
						"underlyerReference" },
					"Asset", new string [] {
						"basket", "cash", "commodity", "deposit", "bond",
						"convertibleBond", "equity", "exchangeTradedFund",
						"index", "future", "fxRate", "loan", "mortgage",
						"mutualFund", "rateIndex", "simpleCreditDefautSwap",
						"simpleFra", "simpleIrSwap", "dealSummary", "facilitySummary" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>FixedRateReference</b> correctly
		/// refers to a <b>FixedRate</b>.
		/// </summary>
		public static readonly Rule	RULE02
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-2",
					"FixedRateReference", new String [] {
						"strikeReference" },
					"FixedRate", new String [] {
						"fixedRate" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>ProtectionTermsReference</b> correctly
		/// refers to a <b>ProtectionTerms</b>.
		/// </summary>
		public static readonly Rule	RULE03
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-3",
					"ProtectionTermsReference", new String [] {
						"protectionTermsReference" },
					"ProtectionTerms", new String [] {
						"protectionTerms" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>SettlementTermsReference</b> correctly
		/// refers to a <b>SettlementTerms</b>.
		/// </summary>
		public static readonly Rule	RULE04
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-4",
					"SettlementTermsReference", new String [] {
						"settlementTermsReference" },
					"SettlementTerms", new String [] {
						"settlementTerms" });

		/// <summary>
		/// A <b>Rule</b> that ensures the <b>@href</b> attribute of a
		/// <b>firstPeriodStartDate</b> must match the @id attribute of an
		/// element of type <b>Party</b>.
		/// </summary>
		public static readonly Rule	RULE05
			= new DelegatedRule (Preconditions.R4_1__LATER, "ref-5", Rule05);

		/// <summary>
		/// A <b>Rule</b> that ensures an <b>InterestCalculationReference</b> correctly
		/// refers to an <b>Rate</b>.
		/// </summary>
		public static readonly Rule	RULE06
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-6",
					"InterestCalculationReference", new String [] {
						"interestLegRate" },
					"Rate", new String [] {
					"rateCalculation", "floatingRate", "floatingRateCalculation",
					"inflationRateCalculation" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures an <b>InterestLegCalculationPeriodDatesReference</b> correctly
		/// refers to an <b>InterestLegCalculationPeriodDates</b>.
		/// </summary>
		public static readonly Rule	RULE07
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-7",
					"InterestLegCalculationPeriodDatesReference", new String [] {
						"calculationPeriodDatesReference" },
					"InterestLegCalculationPeriodDates", new String [] {
						"interestLegCalculationPeriodDates" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>CalculationPeriodDatesReference</b> correctly
		/// refers to an <b>CalculationPeriodDates</b>.
		/// </summary>
		public static readonly Rule	RULE08
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-8",
					"CalculationPeriodDatesReference", new String [] {
						"calculationPeriodDatesReference" },
					"CalculationPeriodDates", new String [] {
						"calculationPeriodDates" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures an <b>InterestRateStreamReference</b> correctly
		///refers to an <b>InterestRateStream</b>.
		/// </summary>
		public static readonly Rule	RULE09
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-9",
					"InterestRateStreamReference", new String [] {
						"swapStreamReference" },
					"InterestRateStream", new String [] {
						"capFloorStream", "swapStream" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PaymentCalculationPeriod</b> correctly
		/// refers to a <b>PricingStructure</b>.
		/// </summary>
		public static readonly Rule	RULE10
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-10",
					"PaymentCalculationPeriod", new String [] {
						"paymentCalculationPeriod" },
					"PricingStructure", new String [] {
						"creditCurve", "fxCurve", "volatilityRepresentation",
						"yieldCurve" });
	
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PaymentDatesReference</b> correctly
		/// refers to a <b>PaymentDates</b>.
		/// </summary>
		public static readonly Rule	RULE11
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-11",
					"PaymentDatesReference", new String [] {
						"paymentDatesReference" },
					"PaymentDates", new String [] {
						"paymentDates" });
	
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PaymentCalculationPeriod</b> correctly
		/// refers to a <b>PricingStructure</b>.
		/// </summary>
		public static readonly Rule	RULE12
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-12",
					"ResetDatesReference", new String [] {
						"resetDatesReference" },
					"ResetDates", new String [] {
						"resetDates" });
	
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>CreditEventsReference</b> correctly
		/// refers to a <b>CreditEvents</b>.
		/// </summary>
		public static readonly Rule	RULE13
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-13",
					"CreditEventsReference", new String [] {
						"creditEventsReference" },
					"CreditEvents", new String [] {
						"creditEvents" });
	
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>CreditEventsReference</b> correctly
		/// refers to a <b>CreditEvents</b>.
		/// </summary>
		public static readonly Rule	RULE14
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-14",
					"CashflowFixingReference", new String [] {
						"calculatedRateReference" },
					"CashflowFixing", new String [] {
						"calculatedRate" });
	
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>CashflowObservationReference</b> correctly
		/// refers to a <b>CreditEvents</b>.
		/// </summary>
		public static readonly Rule	RULE15
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-15",
					"CashflowObservationReference", new String [] {
						"observationReference" },
					"CashflowObservation", new String [] {
						"observationElements" });
	
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>StepReference</b> correctly
		/// refers to a <b>Step</b>.
		/// </summary>
		public static readonly Rule	RULE16
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-16",
					"StepReference", new String [] {
						"fixedRateStepReference" },
					"Step", new String [] {
						"step" });
	
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>TradeUnderlyerReference</b> correctly
		/// refers to a <b>Step</b>.
		/// </summary>
		public static readonly Rule	RULE17
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-17",
					"TradeUnderlyerReference", new String [] {
						"underlyerReference" },
					"TradeUnderlyer", new String [] {
						"underlyer" });
	
		/// <summary>
		/// A <see cref="Rule"/> that ensures the generic/@href attribute must match the @id
		/// attribute of an element of type Asset.
		/// </summary>
		/// <remarks>Applies to all FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE18
			= new DelegatedRule ("ref-18", new RuleDelegate (Rule18));

		/// <summary>
		/// A <b>Rule</b> that ensures a <b>TradeUnderlyerReference</b> correctly
		/// refers to a <b>Step</b>.
		/// </summary>
		public static readonly Rule	RULE19
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-19",
					"MarketReference", new String [] {
						"marketReference" },
					"Market", new String [] {
						"market" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PricingDataPointCoordinateReference</b> correctly
		/// refers to a <b>PricingDataPointCoordinate</b>.
		/// </summary>
		public static readonly Rule	RULE20
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-20",
					"PricingDataPointCoordinateReference", new String [] {
						"coordinateReference" },
					"PricingDataPointCoordinate", new String [] {
						"coordinate" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PricingParameterDerivativeReference</b> correctly
		/// refers to a <b>PricingParameterDerivative</b>.
		/// </summary>
		public static readonly Rule	RULE21
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-21",
					"PricingParameterDerivativeReference", new String [] {
						"partialDerivativeReference" },
					"PricingParameterDerivative", new String [] {
						"partialDerivative" });

        /// <summary>
        /// A <b>Rule</b> that ensures a <b>PricingParameterDerivativeReference</b> correctly
	    /// refers to a <b>PricingParameterDerivative</b>.
        /// </summary>
        /// <remarks>Applies to FpML 4.0 and later.</remarks>
	    public static readonly Rule	RULE22
		    = new ReferenceRule (Preconditions.R4_0__LATER, "ref-22",
				    "Valuation", new String [] {
					    "valuation", "assetValuation", "associatedValue",
					    "assetQuote", "pricingStructureValuation", "creditCurveValuation",
					    "defaultProbabilityCurve", "fxCurveValuation",
					    "volatilityMatrixValuation", "yieldCurveValuation" },
				    "ValuationScenario", new String [] {
					    "valuationScenario" },
				    "definitionRef");
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>ValuationReference</b> correctly
		/// refers to a <b>Valuation</b>.
		/// </summary>
		public static readonly Rule	RULE23
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-23",
					"ValuationReference", new String [] {
						"valuationReference", "associatedValueReference",
						"inputDateReference" },
					"Valuation", new String [] {
						"valuation", "assetValuation", "associatedValue", "assetQuote",
						"pricingStructionValuation", "creditCurveValuation",
						"defaultProbabilityCurve", "fxCurveValuation",
						"volatilityMatrixValuation", "yieldCurveValuation" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>ValuationScenarioReference</b> correctly
		/// refers to a <b>ValuationScenario</b>.
		/// </summary>
		public static readonly Rule	RULE24
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-24",
					"ValuationScenarioReference", new String [] {
						"valuationScenarioReference", "baseValuationScenario" },
					"ValuationScenario", new String [] {
						"valuationScenario" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures an <b>AccountReference</b> correctly
		/// refers to an <b>Account</b>.
		/// </summary>
		public static readonly Rule	RULE25
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-25",
					"AccountReference", new String [] {
						"accountReference", "account" },
					"Account", new String [] {
						"account" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>BusinessCentersReference</b> correctly
		/// refers to a <b>BusinessCenters</b>.
		/// </summary>
		public static readonly Rule	RULE26
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-26",
					"BusinessCentersReference", new String [] {
						"businessCentersReference" },
					"BusinessCenters", new String [] {
						"businessCenters" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>BusinessDayAdjustmentsReference</b> correctly
		/// refers to a <b>BusinessDayAdjustments</b>.
		/// </summary>
		public static readonly Rule	RULE27
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-27",
					"BusinessDayAdjustmentsReference", new String [] {
						"businessCentersReference" },
					"BusinessDayAdjustments", new String [] {
						"dateAdjustments", "calculationPeriodDatesAdjustments",
						"paymentDatesAdjustments", "resetDatesAdjustments",
						"relativeDateAdjustments" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>LegalEntityReference</b> correctly
		/// refers to a <b>LegalEntity</b>.
		/// </summary>
		public static readonly Rule	RULE28
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-28",
					"LegalEntityReference", new String [] {
						"primaryObligorReference", "guarantorReference",
						"borrowerReference", "insurerReference",
						"creditEntityReference"},
					"LegalEntity", new String [] {
						"referenceEntity", "excludedReferenceEntity",
						"primaryObligor", "guarantor", "borrower",
						"insurer" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PartyReference</b> correctly
		/// refers to a <b>Party</b>.
		/// </summary>
		public static readonly Rule	RULE29
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-29",
					"PartyReference", new String [] {
						"intermediaryPartyReference", "depositoryPartyReference",
						"notifyingPartyReference", "notifiedPartyReference",
						"transferor", "transferee", "remainingParty", "otherRemainingParty",
						"definingParty", "matchingParty", "baseParty",
						"activityProvider",	"positionProvider", "valuationProvider",
						"partyReference", "party", "buyerPartyReference", "sellerPartyReference",
						"payerPartyReference", "receiverPartyReference", "issuerPartyReference",
						"accountBeneficiary", "beneficiaryPartyReference",
						"calculationAgentPartyReference", "correspondentPartyReference",
						"extraOrdinaryDividends", "exerciseNoticePartyReference",
						"issuingBankPartyReference", "borrowerPartyReference",
						"agentBankPartyReference" },
					"Party", new String [] {
						"party" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>Payment</b> correctly
		/// refers to a <b>PricingStructure</b>.
		/// </summary>
		public static readonly Rule	RULE30
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-30",
					"Payment", new String [] {
						"payment", "otherPartyPayment", "premium",
						"additionalPayment", "exchangedCurrency1",
						"exchangedCurrency2" },
					"PricingStructure", new String [] {
						"creditCurve", "fxCurve", "volatilityRepresentation",
						"yieldCurve" });

		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PricingStructureReference</b> correctly
		/// refers to a <b>PricingStructure</b>.
		/// </summary>
		public static readonly Rule	RULE31
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-31",
					"PricingStructureReference", new String [] {
						"baseYieldCurve", "settlementCurrencyYieldCurve",
						"forecastCurrencyYieldCurve", "originalInputReference",
						"replacementInputReference", "pricingInputReference",
						"partialDerivativeReference", "replacementMarketInput" },
					"PricingStructure", new String [] {
						"creditCurve", "fxCurve", "volatilityRepresentation",
						"yieldCurve" });

		/// <summary>
		/// A <b>Rule</b> that ensures a <b>PricingStructureReference</b> correctly
		/// refers to a <b>PricingStructure</b>.
		/// </summary>
		public static readonly Rule	RULE32
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-32",
					"ProductReference", new String [] {
						"premiumProductReference", "definition" },
					"Product", new String [] {
						"swap", "fra", "capFloor", "swaption", "strategy",
						"commoditySwap", "bulletPayment", "creditDefaultSwap",
						"dividendSwapTransactionSupplement", "equityForward",
						"equityOption", "equityOptionTransactionSupplement",
						"equitySwapTransactionSupplement", "fxAverageRateOption",
						"fxBarrierOption", "fxDigitalOption", "fxSwap",
						"fxSingleLeg", "equitySwap", "returnSwap",
						"termDeposit", "varianceSwap" });

		/// <summary>
		/// A <b>Rule</b> that ensures a <b>RateReference</b> correctly
		/// refers to a <b>Rate</b>.
		/// </summary>
		public static readonly Rule	RULE33
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-33",
					"RateReference", new String [] {
						"rateReference" },
					"Rate", new String [] {
						"rateCalculation", "floatingRate", "floatingRateCalculation",
						"inflationRateCalculation" });

		/// <summary>
		/// A <b>Rule</b> that ensures a <b>ScheduleReference</b> correctly
		/// refers to a <b>Schedule</b>.
		/// </summary>
		public static readonly Rule	RULE34
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-34",
					"ScheduleReference", new String [] {
						"notionalReference", "constantNotionalScheduleReference" },
					"Schedule", new String [] {
						"fixedRate", "feeRateSchedule", "floatingRateMultiplierSchedule",
						"fixedRateSchedule", "knownAmountSchedule", "notionalStepSchedule",
						"feeAmountSchedule", "spreadSchedule", "capRateSchedule",
						"floorRateSchedule" });

		/// <summary>
		/// A <b>Rule</b> that ensures a <b>SpreadScheduleReference</b> correctly
		/// refers to a <b>SpreadSchedule</b>.
		/// </summary>
		public static readonly Rule	RULE35
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-35",
					"SpreadScheduleReference", new String [] {
						"underlyerSpread" },
					"SpreadSchedule", new String [] {
						"spreadSchedule" });
		
		/// <summary>
		/// A <b>Rule</b> that ensures a <b>SensitivitySetDefinitionReference</b>
		/// correctly refers to a <b>SensitivitySetDefinition</b>.
		/// </summary>
		public static readonly Rule	RULE36
			= new ReferenceRule (Preconditions.R4_0__LATER, "ref-36",
					"SensitivitySetDefinitionReference", new String [] {
						"definitionReference" },
					"SensitivitySetDefinition", new String [] {
						"sensitivitySetDefinition" });
		
		// --------------------------------------------------------------------

		private static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule05 (name, nodeIndex, XPath.Paths (nodeIndex.GetElementsByName ("novation"), ".."), errorHandler));
		}

		private static bool Rule05 (string name, NodeIndex nodeIndex, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result 	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		startDate	= XPath.Path (context, "novation", "firstPeriodStartDate");
				XmlAttribute	href;
				
				if ((startDate == null) || (href = startDate.GetAttributeNode ("href"))== null) continue;
				
				XmlElement	target	= nodeIndex.GetElementById (href.Value);
					
				if ((target == null) || !target.LocalName.Equals ("party")) {
					errorHandler ("305", context,
						"The @href attribute on the firstPeriodStartDate must reference a party",
						name, href.Value);
					
					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule18 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule18 (name, nodeIndex, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "PricingDataPointCoordinate"), errorHandler));

			return (Rule18 (name, nodeIndex, nodeIndex.GetElementsByName ("coordinate"), errorHandler));
		}
		
		private static bool Rule18 (string name, NodeIndex nodeIndex, XmlNodeList list,
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
		
		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = RuleSet.ForName ("ReferenceRules");

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private ReferenceRules ()
		{ }
	}
}