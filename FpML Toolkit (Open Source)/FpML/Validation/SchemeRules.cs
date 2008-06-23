// Copyright (C),2005-2007 HandCoded Software Ltd.
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
	/// Summary description for SchemeRules.
	/// </summary>
	public class SchemeRules {
		/// <summary>
		/// Contains the <see cref="RuleSet"/>.
		/// </summary>
		public static RuleSet Rules {
			get {
				return (rules);
			}
		}
	
		// FpML 1.0 ------------------------------------------------------------ 

		/// <summary>
		/// Rule 1: The value of any <b>averagingMethod</b> element must be valid
		/// within the domain defined by its <b>@averagingMethodScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE01
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-1", "averagingMethod", "averagingMethodScheme");

		/// <summary>
		/// Rule 2: The value of any <b>businessCenter</b> element must be valid
		/// within the domain defined by its <b>businessCenterScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE02
			= new SchemeRule ("scheme-2", "businessCenter", "businessCenterScheme");
		
		/// <summary>
		/// Rule 3: The value of any <b>businessDayConvention</b> element must be valid
		/// within the domain defined by its <b>@businessDayConventionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE03
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-3", "businessDayConvention", "businessDayConventionScheme");

		/// <summary>
		/// Rule 4: The value of any <b>compoundingMethod</b> element must be valid
		/// within the domain defined by its <b>@compoundingMethodScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE04
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-4", "compoundingMethod", "compoundingMethodScheme");

		/// <summary>
		/// Rule 5: The value of any <b>Currency</b> type element must be valid
		/// within the domain defined by its <b>@currencyScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE05
			= new SchemeRule ("scheme-5",
			new String [] {
							  "currency", "settlementCurrency", "referenceCurrency",
							  "cashSettlementCurrency", "payoutCurrency", "optionOnCurrency",
							  "faceOnCurrency", "baseCurrency", "currency1", "currency2"
						  }, "currencyScheme");
		
		/// <summary>
		/// Rule 6: The value of any <b>dateRelativeTo</b> element must be valid
		/// within the domain defined by its <b>@dateRelativeToScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE06
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-6", "dateRelativeTo", "dateRelativeToScheme");

		/// <summary>
		/// Rule 7: The value of any <b>dayCountFraction</b> element must be valid
		/// within the domain defined by its <b>@dayCountFractionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to all FpML releases EXCEPT 4-0.</remarks>
		public static readonly Rule	RULE07
			= new SchemeRule (Preconditions.NOT_R4_0, "scheme-7", "dayCountFraction", "dayCountFractionScheme");

		/// <summary>
		/// Rule 8: The value of any <b>dayType</b> element must be valid
		/// within the domain defined by its <b>@dayTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE08
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-8", "dayType", "dayTypeScheme");

		/// <summary>
		/// Rule 9: The value of any <b>discountingType</b> element must be valid
		/// within the domain defined by its <b>@discountingTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE09
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-9", "discountingType", "discountingTypeScheme");

#if false
		/// <summary>
		/// Rule 10: The value of any <b>floatingRateIndex</b> type element must be valid
		/// within the domain defined by its <b>@floatingRateIndexScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE10
			= new SchemeRule ("scheme-10", "floatingRateIndex", "floatingRateIndexScheme");
#endif

		/// <summary>
		/// Rule 11: The value of any <b>negativeInterestRateTreatment</b> element must be valid
		/// within the domain defined by its <b>@negativeInterestRateTreatmentScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE11
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-11", "negativeInterestRateTreatment", "negativeInterestRateTreatmentScheme");

		/// <summary>
		/// Rule 12: The value of any <b>payRelativeTo</b> element must be valid
		/// within the domain defined by its <b>@payRelativeToScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE12
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-12", "payRelativeTo", "payRelativeToScheme");

		/// <summary>
		/// Rule 13: The value of any <b>period</b> element must be valid
		/// within the domain defined by its <b>@periodScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE13
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-13", "period", "periodScheme");

		/// <summary>
		/// Rule 14: The value of any <b>rateTreatment</b> element must be valid
		/// within the domain defined by its <b>@rateTreatmentScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE14
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-14", "rateTreatment", "rateTreatmentScheme");

		/// <summary>
		/// Rule 15: The value of any <b>resetRelativeTo</b> element must be valid
		/// within the domain defined by its <b>@resetRelativeToScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE15
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-15", "resetRelativeTo", "resetRelativeToScheme");

		/// <summary>
		/// Rule 16: The value of any <b>rollConvention</b> element must be valid
		/// within the domain defined by its <b>@rollConventionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE16
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-16", "rollConvention", "rollConventionScheme");

		/// <summary>
		/// Rule 17: The value of any <b>roundingDirection</b> element must be valid
		/// within the domain defined by its <b>@roundingDirectionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE17
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-17", "roundingDirection", "roundingDirectionScheme");

		/// <summary>
		/// Rule 18: The value of any <b>stepRelativeTo</b> element must be valid
		/// within the domain defined by its <b>@stepRelativeToScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE18
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-18", "stepRelativeTo", "stepRelativeToScheme");

		/// <summary>
		/// Rule 19: The value of any <b>weeklyRollConvention</b> element must be valid
		/// within the domain defined by its <b>@weeklyRollConventionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 1-0, 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE19
			= new SchemeRule (Preconditions.R1_0__R3_0, "scheme-19", "weeklyRollConvention", "weeklyRollConventionScheme");

		// FpML 2.0 ------------------------------------------------------------

		/// <summary>
		/// Rule 20: The value of any <b>calculationAgentParty</b> element must be valid
		/// within the domain defined by its <b>@calculationAgentPartyScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE20
			= new SchemeRule (Preconditions.R2_0__R3_0, "scheme-20", "calculationAgentParty", "calculationAgentPartyScheme");

		/// <summary>
		/// Rule 21: The value of any <b>rateSource</b> element must be valid
		/// within the domain defined by its <b>@informationProviderScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 2-0 and later.</remarks>
		public static readonly Rule	RULE21
			= new SchemeRule (Preconditions.R2_0__LATER, "scheme-21", "rateSource", "informationProviderScheme");

		/// <summary>
		/// Rule 22: The value of any <b>buyer</b> or <b>seller</b> element must be valid
		/// within the domain defined by its <b>@payerReceiverScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE22
			= new SchemeRule (Preconditions.R2_0__R3_0, "scheme-22",
			new String [] { "buyer", "seller" }, "payerReceiverScheme");

		/// <summary>
		/// Rule 23: The value of any <b>quotationRateType</b> element must be valid
		/// within the domain defined by its <b>@quotationRateTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 2-0 and 3-0.</remarks>
		public static readonly Rule	RULE23
			= new SchemeRule (Preconditions.R2_0__R3_0, "scheme-23", "quotationRateType", "quotationRateTypeScheme");

		// FpML 3.0 ------------------------------------------------------------

		/// <summary>
		/// Rule 24: The value of any <b>clearanceSystem</b> element must be valid
		/// within the domain defined by its <b>clearanceSystemScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE24A
			= new SchemeRule (Preconditions.R3_0__R4_0, "scheme-24a", "clearanceSystem", "clearanceSystemScheme");
		
		/// <summary>
		/// Rule 24: The value of any <b>clearanceSystem</b> element must be valid
		/// within the domain defined by its <b>clearanceSystemScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE24B
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-24b", "clearanceSystem", "clearanceSystemScheme");
		
		/// <summary>
		/// Rule 25: The value of any <b>contractualDefinitions</b> element must
		/// be valid within the domain defined by its <b>@contractualDefinitionsScheme</b>
		/// attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE25
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-25", "contractualDefinitions", "contractualDefinitionsScheme");

		/// <summary>
		/// Rule 26: The value of any <b>country</b> element must be valid
		/// within the domain defined by its <b>@countryScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE26
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-26", "country", "countryScheme");

		/// <summary>
		/// Rule 27: The value of any <b>cutName</b> element must be valid
		/// within the domain defined by its <b>@cutNameScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE27
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-27", "cutName", "cutNameScheme");
		
		/// <summary>
		/// Rule 28: The value of any <b>exerciseStyle</b> element must be valid
		/// within the domain defined by its <b>@exerciseStyleScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE28
			= new SchemeRule (Preconditions.R3_0, "scheme-28", "exerciseStyle", "exerciseStyleScheme");
		
		/// <summary>
		/// Rule 29: The value of any <b>fxBarrierType</b> element must be valid
		/// within the domain defined by its <b>@fxBarrierTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE29
			= new SchemeRule (Preconditions.R3_0, "scheme-29", "fxBarrierType", "fxBarrierTypeScheme");
		
		/// <summary>
		/// Rule 30: The value of any <b>governingLaw</b> element must be valid
		/// within the domain defined by its <b>governingLawScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE30
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-30", "governingLaw", "governingLawScheme");
		
		/// <summary>
		/// Rule 31: The value of any <b>masterAgreementType</b> element must be valid
		/// within the domain defined by its <b>masterAgreementTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE31
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-31", "masterAgreementType", "masterAgreementTypeScheme");
		
		/// <summary>
		/// Rule 32: The value of any <b>methodOfAdjustment</b> element must be valid
		/// within the domain defined by its <b>@methodOfAdjustmentScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE32
			= new SchemeRule (Preconditions.R3_0, "scheme-32", "methodOfAdjustment", "methodOfAdjustmentScheme");
		
		/// <summary>
		/// Rule 33: The value of any <b>nationalisationOrInsolvency</b> element must be valid
		/// within the domain defined by its <b>@nationalisationOrInsolvencyOrDelistingScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE33
			= new SchemeRule (Preconditions.R3_0, "scheme-33",
					new String [] {	"nationalisationOrInsolvency", "delisting" },
					"nationalisationOrInsolvencyOrDelistingScheme");
		
		/// <summary>
		/// Rule 34: The value of any <b>optionType</b> element must be valid
		/// within the domain defined by its <b>@optionTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE34
			= new BrokenSchemeRule (Preconditions.R3_0, "scheme-34", "optionType", "optionTypeScheme");
		
		/// <summary>
		/// Rule 35: The value of any <b>partyContactDetails</b> element must be valid
		/// within the domain defined by its <b>@partyContactDetailsScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE35
			= new SchemeRule (Preconditions.R3_0, "scheme-35", "partyContactDetails", "partyContactDetailsScheme");
		
		/// <summary>
		/// Rule 36: The value of any <b>payout</b> element must be valid
		/// within the domain defined by its <b>@payoutScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE36
			= new SchemeRule (Preconditions.R3_0, "scheme-36", "payoutStyle", "payoutScheme");

		/// <summary>
		/// Rule 37: The value of any <b>premiumQuoteBasis</b> element must be valid
		/// within the domain defined by its <b>@premiumQuoteBasisScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE37
			= new SchemeRule (Preconditions.R3_0, "scheme-37", "premiumQuoteBasis", "premiumQuoteBasisScheme");

		/// <summary>
		/// Rule 38: The value of any <b>quoteBasis</b> element must be valid
		/// within the domain defined by its <b>@quoteBasisScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE38
			= new SchemeRule (Preconditions.R3_0, "scheme-38", "quoteBasis", "quoteBasisScheme");

		/// <summary>
		/// Rule 39: The value of any <b>routingCodeId</b> element must be valid
		/// within the domain defined by its <b>routingCodeIdScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE39
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-39", "routingCodeId", "routingCodeIdScheme");
		
		/// <summary>
		/// Rule 40: The value of any <b>settlementMethod</b> element must be valid
		/// within the domain defined by its <b>settlementScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE40
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-40", "settlementMethod", "settlementMethodScheme");
		
		/// <summary>
		/// Rule 41: The value of any <b>settlementPriceSource</b> element must be valid
		/// within the domain defined by its <b>settlementPriceSourceScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0 and later.</remarks>
		public static readonly Rule	RULE41
			= new SchemeRule (Preconditions.R3_0__LATER, "scheme-41", "settlementPriceSource", "settlementPriceSourceScheme");
		
		/// <summary>
		/// Rule 42: The value of any <b>settlementType</b> element must be valid
		/// within the domain defined by its <b>@settlementTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE42
			= new SchemeRule (Preconditions.R3_0, "scheme-42", "settlementType", "settlementTypeScheme");

		/// <summary>
		/// Rule 43: The value of any <b>shareExtraordinaryEvent</b> element must be valid
		/// within the domain defined by its <b>@shareExtraordinaryEventScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE43
			= new SchemeRule (Preconditions.R3_0, "scheme-43", "shareExtraordinaryEvent", "shareExtraordinaryEventScheme");

		/// <summary>
		/// Rule 44: The value of any <b>sideRateBasis</b> element must be valid
		/// within the domain defined by its <b>@sideRateBasisScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE44
			= new SchemeRule (Preconditions.R3_0, "scheme-44", "sideRateBasis", "sideRateBasisScheme");

		/// <summary>
		/// Rule 45: The value of any <b>standardSettlementStyle</b> element must be valid
		/// within the domain defined by its <b>@standardSettlementStyleScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE45
			= new SchemeRule (Preconditions.R3_0, "scheme-45", "standardSettlementStyle", "standardSettlementStyleScheme");

		/// <summary>
		/// Rule 46: The value of any <b>strikeQuoteBasis</b> element must be valid
		/// within the domain defined by its <b>@strikeQuoteBasisScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE46
			= new SchemeRule (Preconditions.R3_0, "scheme-46", "strikeQuoteBasis", "strikeQuoteBasisScheme");

		/// <summary>
		/// Rule 47: The value of any <b>timeType</b> element must be valid
		/// within the domain defined by its <b>@timeTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE47
			= new SchemeRule (Preconditions.R3_0, "scheme-47",
					new String [] {
						  "latestExerciseTimeType", "equityExpirationTimeType", "valuationTimeType" },
					"timeTypeScheme");

		/// <summary>
		/// Rule 48: The value of any <b>touchCondition</b> element must be valid
		/// within the domain defined by its <b>@touchConditionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE48
			= new SchemeRule (Preconditions.R3_0, "scheme-48", "touchCondition", "touchConditionScheme");

		/// <summary>
		/// Rule 49: The value of any <b>triggerCondition</b> element must be valid
		/// within the domain defined by its <b>@triggerConditionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 3-0.</remarks>
		public static readonly Rule	RULE49
			= new SchemeRule (Preconditions.R3_0, "scheme-49", "triggerCondition", "triggerConditionScheme");

		// FpML 4.0 ------------------------------------------------------------

		/// <summary>
		/// Rule 50: The value of any <b>additionalTerm</b> element must be valid
		/// within the domain defined by its <b>@additionalTermScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE50
			= new SchemeRule (Preconditions.R4_0__LATER, "scheme-50", "additionalTerm", "additionalTermScheme");

		/// <summary>
		/// Rule 51: The value of any <b>contractualSupplement</b> element must
		/// be valid within the domain defined by its <b>contractualSupplementScheme</b>
		/// attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE51
			= new SchemeRule (Preconditions.R4_0__LATER, "scheme-51", "contractualSupplement", "contractualSupplementScheme");
		
		/// <summary>
		/// Rule 52: The value of any <b>fxFeatureType</b> element must be valid
		/// within the domain defined by its <b>@fxFeatureTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4-0 only.</remarks>
		public static readonly Rule	RULE52
			= new SchemeRule (Preconditions.R4_0, "scheme-52", "fxFeatureType", "fxFeatureTypeScheme");
		
		/// <summary>
		/// Rule 53: The value of any <b>marketDisruption</b> element must be valid
		/// within the domain defined by its <b>marketDisruptionScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE53
			= new SchemeRule (Preconditions.R4_0__LATER, "scheme-53", "marketDisruption", "marketDisruptionScheme");
		
		/// <summary>
		/// Rule 54: The value of any <b>masterConfirmationType</b>b> element must be valid
		/// within the domain defined by its <b>masterConfirmationTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE54
			= new SchemeRule (Preconditions.R4_0__LATER, "scheme-54", "masterConfirmationType", "masterConfirmationTypeScheme");
		
		/// <summary>
		/// Rule 55: The value of any <b>restructuringType</b> element must be valid
		/// within the domain defined by its <b>restructuringTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE55
			= new SchemeRule (Preconditions.R4_0__LATER, "scheme-55", "restructuringType", "restructuringScheme");

		// FpML 4.1 ------------------------------------------------------------

		/// <summary>
		/// Rule 56: The value of any <b>assetMeasure</b> element must be valid
		/// within the domain defined by its <b>assetMeasureScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE56
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-56", "measureType", "assetMeasureScheme");
		
		/// <summary>
		/// Rule 57: The value of any <b>brokerConfirmationType</b> element must be valid
		/// within the domain defined by its <b>brokerConfirmationTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE57
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-57", "brokerConfirmationType", "brokerConfirmationTypeScheme");
		
		/// <summary>
		/// Rule 58: The value of any <b>compoundingFrequency</b> element must be valid
		/// within the domain defined by its <b>compoundingFrequencyScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE58
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-58", "compoundingFrequency", "compoundingFrequencyScheme");
		
		/// <summary>
		/// Rule 59: The value of any <b>couponType</b> element must be valid
		/// within the domain defined by its <b>couponTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE59
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-59", "couponType", "couponTypeScheme");
		
		/// <summary>
		/// Rule 60: The value of any <b>creditSeniority</b> element must be valid
		/// within the domain defined by its <b>creditSeniorityScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE60
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-60", "creditSeniority", "creditSeniorityScheme");
		
		/// <summary>
		/// Rule 61: The value of any <b>indexAnnexSource</b> element must be valid
		/// within the domain defined by its <b>indexAnnexSourceScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE61
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-61", "indexAnnexSource", "indexAnnexSourceScheme");
		
		/// <summary>
		/// Rule 62: The value of any <b>interpolationMethod</b> element must be valid
		/// within the domain defined by its <b>interpolationMethodScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE62
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-62", "interpolationMethod", "interpolationMethodScheme");
		
		/// <summary>
		/// Rule 63: The value of any <b>matrixTerm</b> element must be valid
		/// within the domain defined by its <b>matrixTermScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE63
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-63", "matrixTerm", "matrixTermScheme");
		
		/// <summary>
		/// Rule 64: The value of any <b>perturbationType</b> element must be valid
		/// within the domain defined by its <b>perturbationTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE64
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-64", "perturbationType", "perturbationTypeScheme");
		
		/// <summary>
		/// Rule 65: The value of any <b>priceQuoteUnit</b> element must be valid
		/// within the domain defined by its <b>priceQuoteUnitScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE65
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-65", "priceQuoteUnit", "priceQuoteUnitScheme");
		
		/// <summary>
		/// Rule 66: The value of any <b>pricingInputType</b> element must be valid
		/// within the domain defined by its <b>pricingInputTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE66
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-66", "pricingInputType", "pricingInputTypeScheme");
		
		/// <summary>
		/// Rule 67: The value of any <b>queryParameterOperator</b> element must be valid
		/// within the domain defined by its <b>queryParameterOperatorScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE67
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-67", "queryParameterOperator", "queryParameterOperatorScheme");
		
		/// <summary>
		/// Rule 68: The value of any <b>quoteTiming</b> element must be valid
		/// within the domain defined by its <b>quoteTimingScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE68
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-68", "quoteTiming", "quoteTimingScheme");
		
		/// <summary>
		/// Rule 69: The value of any <b>valuationSetDetail</b> element must be valid
		/// within the domain defined by its <b>valuationSetDetailScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.1 and later.</remarks>
		public static readonly Rule	RULE69
			= new SchemeRule (Preconditions.R4_1__LATER, "scheme-69", "valuationSetDetail", "valuationSetDetailScheme");
		
		// FpML 4.2 ------------------------------------------------------------

		/// <summary>
		/// Rule 70: The value of any <b>creditSeniorityTrading</b> element must be valid
		/// within the domain defined by its <b>creditSeniorityTradingScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.2</remarks>
		public static readonly Rule	RULE70
			= new SchemeRule (Preconditions.R4_2, "scheme-70", "creditSeniorityTrading", "creditSeniorityTradingScheme");
		
		/// <summary>
		/// Rule 71: The value of any <b>derivativeCalculationMethod</b> element must be valid
		/// within the domain defined by its <b>derivativeCalculationMethodScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.2</remarks>
		public static readonly Rule	RULE71
			= new SchemeRule (Preconditions.R4_2, "scheme-71", "derivativeCalculationMethod", "derivativeCalculationMethodScheme");
		
		/// <summary>
		/// Rule 72: The value of any <b>matrixType</b> element must be valid
		/// within the domain defined by its <b>matrixTypeScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.2</remarks>
		public static readonly Rule	RULE72
			= new SchemeRule (Preconditions.R4_2, "scheme-72", "matrixType", "matrixTypeScheme");
		
		/// <summary>
		/// Rule 73: The value of any <b>interestShortfall/rateSource</b> type element must
		/// be valid within the domain defined by its <b>@floatingRateIndexScheme</b> attribute.
		/// </summary>
		/// <remarks>Applies to FpML 4.3 and later.</remarks>
		public static readonly Rule	RULE73
			= new SchemeRule (Preconditions.R4_3__LATER, "scheme-73", "interestShortfall", "rateSource", "floatingRateIndexScheme");

		/// <summary>
		/// Ensures no instances can be constructed.
		/// </summary>
		private SchemeRules ()
		{ }
	
		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = new RuleSet ();

		/// <summary>
		/// Initialises the <see cref="RuleSet"/> by adding the individually defined
		/// validation rules.
		/// </summary>
		static SchemeRules () {
			rules.Add (RULE01);
			rules.Add (RULE02);
			rules.Add (RULE03);
			rules.Add (RULE04);
			rules.Add (RULE05);
			rules.Add (RULE06);
			rules.Add (RULE07);
			rules.Add (RULE08);
			rules.Add (RULE09);
			// rules.Add (RULE10);
			rules.Add (RULE11);
			rules.Add (RULE12);
			rules.Add (RULE13);
			rules.Add (RULE14);
			rules.Add (RULE15);
			rules.Add (RULE16);
			rules.Add (RULE17);
			rules.Add (RULE18);
			rules.Add (RULE19);
			rules.Add (RULE20);
			rules.Add (RULE21);
			rules.Add (RULE22);
			rules.Add (RULE23);
			rules.Add (RULE24A);
			rules.Add (RULE24B);
			rules.Add (RULE25);
			rules.Add (RULE26);
			rules.Add (RULE27);
			rules.Add (RULE28);
			rules.Add (RULE29);
			rules.Add (RULE30);
			rules.Add (RULE31);
			rules.Add (RULE32);
			rules.Add (RULE33);
			rules.Add (RULE34);
			rules.Add (RULE35);
			rules.Add (RULE36);
			rules.Add (RULE37);
			rules.Add (RULE38);
			rules.Add (RULE39);
			rules.Add (RULE40);
			rules.Add (RULE41);
			rules.Add (RULE42);
			rules.Add (RULE43);
			rules.Add (RULE44);
			rules.Add (RULE45);
			rules.Add (RULE46);
			rules.Add (RULE47);
			rules.Add (RULE48);
			rules.Add (RULE49);
			rules.Add (RULE50);
			rules.Add (RULE51);
			rules.Add (RULE52);
			rules.Add (RULE53);
			rules.Add (RULE54);
			rules.Add (RULE55);
			rules.Add (RULE56);
			rules.Add (RULE57);
			rules.Add (RULE58);
			rules.Add (RULE59);
			rules.Add (RULE60);
			rules.Add (RULE61);
			rules.Add (RULE62);
			rules.Add (RULE63);
			rules.Add (RULE64);
			rules.Add (RULE65);
			rules.Add (RULE66);
			rules.Add (RULE67);
			rules.Add (RULE68);
			rules.Add (RULE69);
			rules.Add (RULE70);
			rules.Add (RULE71);
			rules.Add (RULE72);
			rules.Add (RULE73);
		}
	}
}