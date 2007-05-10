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
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;

using log4net;

using HandCoded.FpML.Meta;
using HandCoded.FpML.Schemes;
using HandCoded.Xml;

namespace HandCoded.FpML
{
	/// <summary>
	/// The <b>Releases</b> class contains a set of static objects describing
	/// the FpML specification and its various releases.
	/// </summary>
	public sealed class Releases
	{
		/// <summary>
		/// A <see cref="HandCoded.Meta.Specification"/> instance representing FpML as a whole.
		/// </summary>
		public static readonly HandCoded.Meta.Specification FPML
			= new HandCoded.Meta.Specification ("FpML");

		/// <summary>
		/// A <see cref="DTDRelease"/> instance containing the details for the
		/// FpML 1-0 recommendation.
		/// </summary>
		public static readonly DTDRelease	R1_0
			= new DTDRelease (FPML, "1-0",
					"-//FpML//DTD Financial product Markup Language 1-0//EN",
					"fpml-dtd-1-0-2001-05-14.dtd", "FpML",
					new SchemeDefaults (
						new string [,] {
							{	"averagingMethodSchemeDefault",
								"http://www.fpml.org/spec/2000/averaging-method-1-0" },
							{	"businessCenterSchemeDefault",
								"http://www.fpml.org/spec/2000/business-center-1-0" },
							{	"businessDayConventionSchemeDefault",
								"http://www.fpml.org/spec/2000/business-day-convention-1-0" },
							{	"compoundingMethodSchemeDefault",
								"http://www.fpml.org/spec/2000/compounding-method-1-0" },
							{	"currencySchemeDefault",
								"http://www.fpml.org/ext/iso4217-2001-08-15" },
							{	"dateRelativeToSchemeDefault",
								"http://www.fpml.org/spec/2001/date-relative-to-2-0" },
							{	"dayCountFractionSchemeDefault",
								"http://www.fpml.org/spec/2000/day-count-fraction-1-0" },
							{	"dayTypeSchemeDefault",
								"http://www.fpml.org/spec/2000/day-type-1-0" },
							{	"discountingTypeSchemeDefault",
								"http://www.fpml.org/spec/2000/discounting-type-1-0" },
							{	"floatingRateIndexSchemeDefault",
								"http://www.fpml.org/ext/isda-2000-definitions" },
							{	"negativeInterestRateTreatmentSchemeDefault",
								"http://www.fpml.org/spec/2001/negative-interest-rate-treatment-1-0" },
							{	"partyIdSchemeDefault",
								"http://www.fpml.org/ext/iso9362" },
							{	"payRelativeToSchemeDefault",
								"http://www.fpml.org/spec/2000/pay-relative-to-1-0"	},
							{	"periodSchemeDefault",
								"http://www.fpml.org/spec/2000/period-1-0" },
							{	"rateTreatmentSchemeDefault",
								"http://www.fpml.org/spec/2000/rate-treatment-1-0" },
							{	"resetRelativeToSchemeDefault",
								"http://www.fpml.org/spec/2000/reset-relative-to-1-0" },
							{	"rollConventionSchemeDefault",
								"http://www.fpml.org/spec/2000/roll-convention-1-0" },
							{	"roundingDirectionSchemeDefault",
								"http://www.fpml.org/spec/2000/rounding-direction-1-0" },
							{	"stepRelativeToSchemeDefault",
								"http://www.fpml.org/spec/2000/step-relative-to-1-0" },
							{	"weeklyRollConventionSchemeDefault",
								"http://www.fpml.org/spec/2000/weekly-roll-convention-1-0"
							}
						},
						new string [,] {
							{ 	"averagingMethodScheme",
								"averagingMethodSchemeDefault" },
							{ 	"businessCenterScheme",
								"businessCenterSchemeDefault" },
							{ 	"businessDayConventionScheme",
								"businessDayConventionSchemeDefault" },
							{ 	"compoundingMethodScheme",
								"compoundingMethodSchemeDefault" },
							{ 	"currencyScheme",
								"currencySchemeDefault" },
							{ 	"dateRelativeToScheme",
								"dateRelativeToSchemeDefault" },
							{ 	"dayCountFractionScheme",
								"dayCountFractionSchemeDefault" },
							{ 	"dayTypeScheme",
								"dayTypeSchemeDefault" },
							{ 	"discountingTypeScheme",
								"discountingTypeSchemeDefault" },
							{ 	"floatingRateIndexScheme",
								"floatingRateIndexSchemeDefault" },
							{ 	"linkIdScheme",
								"linkIdSchemeDefault" },
							{ 	"negativeInterestRateTreatmentScheme",
								"negativeInterestRateTreatmentSchemeDefault" },
							{ 	"partyIdScheme",
								"partyIdSchemeDefault" },
							{ 	"paymentTypeScheme",
								"paymentTypeSchemeDefault" },
							{ 	"payRelativeToScheme",
								"payRelativeToSchemeDefault" },
							{ 	"periodScheme",
								"periodSchemeDefault" },
							{ 	"rateTreatmentScheme",
								"rateTreatmentSchemeDefault" },
							{ 	"resetRelativeToScheme",
								"resetRelativeToSchemeDefault" },
							{ 	"rollConventionScheme",
								"rollConventionSchemeDefault" },
							{ 	"roundingDirectionScheme",
								"roundingDirectionSchemeDefault" },
							{ 	"stepRelativeToScheme",
								"stepRelativeToSchemeDefault" },
							{ 	"tradeIdScheme",
								"tradeIdSchemeDefault" },
							{ 	"weeklyRollConventionScheme",
								"weeklyRollConventionSchemeDefault" }
						}),
						ParseSchemes ("Schemes1-0"));

		/// <summary>
		/// A <see cref="DTDRelease"/> instance containing the details for the
		///FpML 2-0 recommendation.
		/// </summary>
		public static readonly DTDRelease	R2_0
			= new DTDRelease (FPML, "2-0",
					"-//FpML//DTD Financial product Markup Language 2-0//EN",
					"fpml-dtd-2-0-2003-05-05.dtd", "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"averagingMethodSchemeDefault",
									"http://www.fpml.org/spec/2000/averaging-method-1-0" },
								{	"businessCenterSchemeDefault",
									"http://www.fpml.org/spec/2000/business-center-1-0" },
								{	"businessDayConventionSchemeDefault",
									"http://www.fpml.org/spec/2000/business-day-convention-1-0" },
								{	"calculationAgentPartySchemeDefault",
									"http://www.fpml.org/spec/2001/calculation-agent-party-1-0" },
								{	"compoundingMethodSchemeDefault",
									"http://www.fpml.org/spec/2000/compounding-method-1-0" },
								{	"currencySchemeDefault",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"dateRelativeToSchemeDefault",
									"http://www.fpml.org/spec/2001/date-relative-to-2-0" },
								{	"dayCountFractionSchemeDefault",
									"http://www.fpml.org/spec/2000/day-count-fraction-1-0" },
								{	"dayTypeSchemeDefault",
									"http://www.fpml.org/spec/2000/day-type-1-0" },
								{	"discountingTypeSchemeDefault",
									"http://www.fpml.org/spec/2000/discounting-type-1-0" },
								{	"floatingRateIndexSchemeDefault",
									"http://www.fpml.org/ext/isda-2000-definitions" },
								{	"informationProviderSchemeDefault",
									"http://www.fpml.org/spec/2001/information-provider-1-0" },
								{	"negativeInterestRateTreatmentSchemeDefault",
									"http://www.fpml.org/spec/2001/negative-interest-rate-treatment-1-0" },
								{	"partyIdSchemeDefault",
									"http://www.fpml.org/ext/iso9362" },
								{	"payerReceiverSchemeDefault",
									"http://www.fpml.org/spec/2001/payer-receiver-1-0" },
								{	"payRelativeToSchemeDefault",
									"http://www.fpml.org/spec/2000/pay-relative-to-1-0"	},
								{	"periodSchemeDefault",
									"http://www.fpml.org/spec/2000/period-1-0" },
								{	"quotationRateTypeSchemeDefault",
									"http://www.fpml.org/spec/2001/quotation-rate-type-1-0" },
								{	"rateTreatmentSchemeDefault",
									"http://www.fpml.org/spec/2000/rate-treatment-1-0" },
								{	"referenceBankIdSchemeDefault",
									"http://www.fpml.org/ext/iso9362" },
								{	"resetRelativeToSchemeDefault",
									"http://www.fpml.org/spec/2000/reset-relative-to-1-0" },
								{	"rollConventionSchemeDefault",
									"http://www.fpml.org/spec/2000/roll-convention-1-0" },
								{	"roundingDirectionSchemeDefault",
									"http://www.fpml.org/spec/2000/rounding-direction-1-0" },
								{	"stepRelativeToSchemeDefault",
									"http://www.fpml.org/spec/2000/step-relative-to-1-0" },
								{	"weeklyRollConventionSchemeDefault",
									"http://www.fpml.org/spec/2000/weekly-roll-convention-1-0"
								}
							},
							new string [,] {
								{ 	"averagingMethodScheme",
									"averagingMethodSchemeDefault" },
								{ 	"businessCenterScheme",
									"businessCenterSchemeDefault" },
								{ 	"businessDayConventionScheme",
									"businessDayConventionSchemeDefault" },
								{ 	"calculationAgentPartyScheme",
									"calculationAgentPartySchemeDefault" },
								{ 	"compoundingMethodScheme",
									"compoundingMethodSchemeDefault" },
								{ 	"currencyScheme",
									"currencySchemeDefault" },
								{ 	"dateRelativeToScheme",
									"dateRelativeToSchemeDefault" },
								{ 	"dayCountFractionScheme",
									"dayCountFractionSchemeDefault" },
								{ 	"dayTypeScheme",
									"dayTypeSchemeDefault" },
								{ 	"discountingTypeScheme",
									"discountingTypeSchemeDefault" },
								{ 	"floatingRateIndexScheme",
									"floatingRateIndexSchemeDefault" },
								{ 	"informationProviderScheme",
									"informationProviderSchemeDefault" },
								{ 	"linkIdScheme",
									"linkIdSchemeDefault" },
								{ 	"negativeInterestRateTreatmentScheme",
									"negativeInterestRateTreatmentSchemeDefault" },
								{ 	"partyIdScheme",
									"partyIdSchemeDefault" },
								{ 	"payRelativeToScheme",
									"payRelativeToSchemeDefault" },
								{ 	"payerReceiverScheme",
									"payerReceiverSchemeDefault" },
								{ 	"paymentTypeScheme",
									"paymentTypeSchemeDefault" },
								{ 	"periodScheme",
									"periodSchemeDefault" },
								{ 	"productTypeScheme",
									"productTypeSchemeDefault" },
								{ 	"quotationRateTypeScheme",
									"quotationRateTypeSchemeDefault" },
								{ 	"rateSourcePageScheme",
									"rateSourcePageSchemeDefault" },
								{ 	"rateTreatmentScheme",
									"rateTreatmentSchemeDefault" },
								{ 	"referenceBankIdScheme",
									"referenceBankIdSchemeDefault" },
								{ 	"resetRelativeToScheme",
									"resetRelativeToSchemeDefault" },
								{ 	"rollConventionScheme",
									"rollConventionSchemeDefault" },
								{ 	"roundingDirectionScheme",
									"roundingDirectionSchemeDefault" },
								{ 	"stepRelativeToScheme",
									"stepRelativeToSchemeDefault" },
								{ 	"tradeIdScheme",
									"tradeIdSchemeDefault" },
								{ 	"weeklyRollConventionScheme",
									"weeklyRollConventionSchemeDefault" }
							}),
							ParseSchemes ("Schemes2-0"));

		/// <summary>
		/// A <see cref="DTDRelease"/> instance containing the details for the
		/// FpML 3-0 trial recommendation.
		/// </summary>
		public static readonly DTDRelease	TR3_0
			= new DTDRelease (FPML, "3-0",
					"-//FpML//DTD Financial product Markup Language 3-0//EN",
					"fpml-dtd-main-3-0.dtd", "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"averagingMethodSchemeDefault",
									"http://www.fpml.org/spec/2000/averaging-method-1-0" },
								{	"businessCenterSchemeDefault",
									"http://www.fpml.org/spec/2000/business-center-1-0" },
								{	"businessDayConventionSchemeDefault",
									"http://www.fpml.org/spec/2000/business-day-convention-1-0" },
								{	"calculationAgentPartySchemeDefault",
									"http://www.fpml.org/spec/2001/calculation-agent-party-1-0" },
								{	"clearanceSystemSchemeDefault",
									"http://www.fpml.org/spec/2002/clearance-system-1-0" },
								{	"compoundingMethodSchemeDefault",
									"http://www.fpml.org/spec/2000/compounding-method-1-0" },
								{	"countrySchemeDefault",
									"http://www.fpml.org/ext/iso3166" },
								{	"currencySchemeDefault",
									"http://www.fpml.org/ext/iso4217" },
								{	"cutNameSchemeDefault",
									"http://www.fpml.org/spec/2002/cut-name-1-0" },
								{ 	"dateRelativeToSchemeDefault",
									"http://www.fpml.org/spec/2001/date-relative-to-3-0" },
								{ 	"dayCountFractionSchemeDefault",
									"http://www.fpml.org/spec/2000/day-count-fraction-1-0" },
								{ 	"dayTypeSchemeDefault",
									"http://www.fpml.org/spec/2000/day-type-1-0" },
								{ 	"definitionsToSchemeDefault",
									"http://www.fpml.org/spec/2002/contractual-definitions-scheme-1-0" },
								{ 	"discountingSchemeDefault",
									"http://www.fpml.org/spec/2000/discounting-type-1-0" },
								{	"exchangeIdSchemeDefault",
									"http://www.fpml.org/spec/2002/exchange-id-MIC-1-0" },
								{ 	"exerciseStyleSchemeDefault",
									"http://www.fpml.org/spec/2002/exercise-style-scheme-1-0" },
								{	"floatingRateIndexSchemeDefault",
									"http://www.fpml.org/ext/isda-2000-definitions" },
								{	"fxBarrierTypeSchemeDefault",
									"http://www.fpml.org/spec/2002/fx-barrier-type-1-0" },
								{	"governingLawSchemeDefault",
									"http://www.fpml.org/spec/2002/governing-law-1-0" },
								{	"informationProviderSchemeDefault",
									"http://www.fpml.org/spec/2001/information-provider-1-0" },
								{	"masterAgreementSchemeDefault",
									"http://www.fpml.org/spec/2002/master-agreement-type-scheme-1-0" },
								{ 	"methodOfAdjustmentSchemeDefault",
									"http://www.fpml.org/spec/2002/method-of-adjustment-scheme-1-0" },
								{ 	"nationalisationOrInsolvencySchemeDefault",
									"http://www.fpml.org/spec/2002/nationalisation-or-insolvency-event-scheme-1-0" },
								{ 	"negativeInterestRateTreatmentSchemeDefault",
									"http://www.fpml.org/spec/2001/negative-interest-rate-treatment-scheme-1-0" },
								{	"partyIdSchemeDefault",
									"http://www.fpml.org/ext/iso9362" },
								{ 	"payRelativeToSchemeDefault",
									"http://www.fpml.org/spec/2000/pay-relative-to-1-0" },
								{ 	"payerReceiverSchemeDefault",
									"http://www.fpml.org/spec/2001/payer-receiver-1-0" },
								{ 	"payoutSchemeDefault",
									"http://www.fpml.org/spec/2002/payout-scheme-1-0" },
								{ 	"periodSchemeDefault",
									"http://www.fpml.org/spec/2000/period-1-0" },
								{ 	"premiumQuoteBasisSchemeDefault",
									"http://www.fpml.org/spec/2002/premium-quote-basis-scheme-1-0" },
								{ 	"quotationRateTypeSchemeDefault",
									"http://www.fpml.org/spec/2001/quotation-rate-type-scheme-1-0" },
								{ 	"quoteBasisSchemeDefault",
									"http://www.fpml.org/spec/2001/quote-basis-1-0" },
								{ 	"rateTreatmentSchemeDefault",
									"http://www.fpml.org/spec/2000/rate-treatment-1-0" },
								{ 	"resetRelativeToSchemeDefault",
									"http://www.fpml.org/spec/2000/reset-relative-to-1-0" },
								{ 	"rollConventionSchemeDefault",
									"http://www.fpml.org/spec/2000/roll-convention-1-0" },
								{ 	"roundingDirectionSchemeDefault",
									"http://www.fpml.org/spec/2000/rounding-direction-1-0" },
								{ 	"routingIdSchemeDefault",
									"http://www.fpml.org/spec/2002/routing-id-code-1-0" },
								{ 	"settlementMethodSchemeDefault",
									"http://www.fpml.org/spec/2002/settlement-method-1-0" },
								{ 	"settlementPriceSourceSchemeDefault",
									"http://www.fpml.org/spec/2002/settlement-price-source-1-0" },
								{	"settlementTypeSchemeDefault",
									"http://www.fpml.org/spec/2002/settlement-type-scheme-1-0" },
								{	"shareExtraordinaryEventSchemeDefault",
									"http://www.fpml.org/spec/2002/share-extraordinary-event-scheme-1-0" },
								{	"sideRateBasisSchemeDefault",
									"http://www.fpml.org/spec/2002/side-rate-basis-scheme-1-0" },
								{	"standardSettlementStyleSchemeDefault",
									"http://www.fpml.org/spec/2002/standard-settlement-style-scheme-1-0" },
								{	"stepRelativeToSchemeDefault",
									"http://ww.fpml.org/spec/2000/step-relative-to-1-0" },
								{	"strikeQuoteBasisSchemeDefault",
									"http://www.fpml.org/spec/2002/strike-quote-basis-scheme-1-0" },
								{	"timeTypeSchemeDefault",
									"http://www.fpml.org/spec/2002/time-type-scheme-1-0" },
								{	"touchConditionSchemeDefault",
									"http://www.fpml.org/spec/2002/touch-condition-scheme-1-0" },
								{	"triggerConditionSchemeDefault",
									"http://www.fpml.org/spec/2002/trigger-condition-scheme-1-0" },
								{	"weeklyRollConventionSchemeDefault",
									"http://wwww.fpml.org/spec/2000/weekly-roll-convention-1-0" }
							},
							new string [,] {
								{ 	"averagingMethodScheme",
									"averagingMethodSchemeDefault" },
								{ 	"businessCenterScheme",
									"businessCenterSchemeDefault" },
								{ 	"businessDayConventionScheme",
									"businessDayConventionSchemeDefault" },
								{ 	"calculationAgentPartyScheme",
									"calculationAgentPartySchemeDefault" },
								{ 	"clearanceSystemScheme",
									"clearanceSystemSchemeDefault" },
							    {	"contractualDefinitionsScheme",
									"definitionsSchemeDefault" },		// Irregular
								{ 	"compoundingMethodScheme",
									"compoundingMethodSchemeDefault" },
								{ 	"countryScheme",
									"countrySchemeDefault" },
								{ 	"currencyScheme",
									"currencySchemeDefault" },
								{ 	"cutNameScheme",
									"cutNameSchemeDefault" },
								{ 	"dateRelativeToScheme",
									"dateRelativeToSchemeDefault" },
								{ 	"dayCountFractionScheme",
									"dayCountFractionSchemeDefault" },
								{ 	"dayTypeScheme",
									"dayTypeSchemeDefault" },
								{ 	"discountingTypeScheme",
									"discountingTypeSchemeDefault" },
								{ 	"exchangeIdScheme",
									"exchangeIdTypeSchemeDefault" },	// Irregular
								{ 	"exerciseStyleScheme",
									"exerciseStyleSchemeDefault" },
								{ 	"floatingRateIndexScheme",
									"floatingRateIndexSchemeDefault" },
								{ 	"fxBarrierTypeScheme",
									"fxBarrierTypeSchemeDefault" },
								{ 	"governingLawScheme",
									"governingLawSchemeDefault" },
								{ 	"informationProviderScheme",
									"informationProviderSchemeDefault" },
								{ 	"linkIdScheme",
									"linkIdSchemeDefault" },
								{	"masterAgreementTypeScheme",
									"masterAgreementSchemeDefault" },	// Irregular
								{ 	"methodOfAdjustmentScheme",
									"methodOfAdjustmentSchemeDefault" },
								{ 	"nationalisationOrInsolvencyOrDelistingScheme",
									"nationalisationOrInsolvencySchemeDefault" },	// Irregular
								{ 	"negativeInterestRateTreatmentScheme",
									"negativeInterestRateTreatmentSchemeDefault" },
								{ 	"partyIdScheme",
									"partyIdSchemeDefault" },
								{ 	"payerReceiverScheme",
									"payerReceiverSchemeDefault" },
								{ 	"paymentTypeScheme",
									"paymentTypeSchemeDefault" },
								{ 	"payoutScheme",
									"payoutSchemeDefault" },
								{ 	"payRelativeToScheme",
									"payRelativeToSchemeDefault" },
								{ 	"periodScheme",
									"periodSchemeDefault" },
								{ 	"portfolioNameScheme",
									"portfolioNameSchemeDefault" },
								{ 	"premiumQuoteBasisScheme",
									"premiumQuoteBasisSchemeDefault" },
								{ 	"productIdScheme",
									"productIdSchemeDefault" },
								{ 	"productTypeScheme",
									"productTypeSchemeDefault" },
								{ 	"quotationRateTypeScheme",
									"quotationRateTypeSchemeDefault" },
								{ 	"quoteBasisScheme",
									"quoteBasisSchemeDefault" },
								{ 	"rateSourcePageScheme",
									"rateSourcePageSchemeDefault" },
								{ 	"rateTreatmentScheme",
									"rateTreatmentSchemeDefault" },
								{ 	"referenceBankIdScheme",
									"referenceBankIdSchemeDefault" },
								{ 	"resetRelativeToScheme",
									"resetRelativeToSchemeDefault" },
								{ 	"rollConventionScheme",
									"rollConventionSchemeDefault" },
								{ 	"roundingDirectionScheme",
									"roundingDirectionSchemeDefault" },
								{ 	"routingIdScheme",
									"routingIdTypeSchemeDefault" },		// Irregular
								{ 	"settlementMethodScheme",
									"settlementMethodSchemeDefault" },
								{ 	"settlementPriceSourceScheme",
									"settlementPriceSourceSchemeDefault" },
								{ 	"settlementTypeScheme",
									"settlementTypeSchemeDefault" },
								{ 	"shareExtraordinaryEventScheme",
									"shareExtraordinaryEventSchemeDefault" },
								{ 	"sideRateBasisScheme",
									"sideRateBasisSchemeDefault" },
								{ 	"standardSettlementStyleScheme",
									"standardSettlementStyleSchemeDefault" },
								{ 	"stepRelativeToScheme",
									"stepRelativeToSchemeDefault" },
								{ 	"strikeQuoteBasisScheme",
									"strikeQuoteBasisSchemeDefault" },
								{ 	"timeTypeScheme",
									"timeTypeSchemeDefault" },
								{ 	"touchConditionScheme",
									"touchConditionSchemeDefault" },
								{ 	"tradeIdScheme",
									"tradeIdSchemeDefault" },
								{ 	"triggerConditionScheme",
									"triggerConditionSchemeDefault" },
								{ 	"weeklyRollConventionScheme",
									"weeklyRollConventionSchemeDefault" }
							}),
							ParseSchemes ("Schemes3-0"));

		
		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-0 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_0
			= new SchemaRelease (FPML, "4-0",
					"http://www.fpml.org/2003/FpML-4-0", "fpml-main-4-0.xsd",
					"fpml", "fpml4-0", "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"additionalTermScheme",
									"http://www.fpml.org/spec/2003/additional-term-1-0" },
								{	"businessCenterScheme",
									"http://www.fpml.org/spec/2000/business-center-1-0" },
								{	"clearanceSystemIdScheme",
									"http://www.fpml.org/spec/2002/clearance-system-1-0" },
								{	"contractualDefinitionsScheme",
									"http://www.fpml.org/spec/2003/contractual-definitions-2-0" },
								{	"contractualSupplementScheme",
									"http://www.fpml.org/spec/2003/contractual-supplement-1-0" },
								{	"countryScheme",
									"http://www.fpml.org/ext/iso3166" },
								{	"currencyScheme",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"cutNameScheme",
									"http://www.fpml.org/spec/2002/cut-name-1-0" },
								{	"exchangeIdScheme",
									"http://www.fpml.org/spec/2002/exchange-id-MIC-1-0" },
								{	"floatingRateIndexScheme",
									"http://www.fpml.org/ext/isda-2000-definitions" },
								{	"fxFeatureTypeScheme",
									"http://www.fpml.org/spec/2003/fxFeatureType-1-0" },
								{	"governingLawScheme",
									"http://www.fpml.org/spec/2002/governing-law-1-0" },
								{	"informationProviderScheme",
									"http://www.fpml.org/spec/2003/information-provider-2-0" },
								{	"marketDisruptionScheme",
									"http://www.fpml.org/spec/2003/marketDisruption-1-0" },
								{	"masterAgreementTypeScheme",
									"http://www.fpml.org/spec/2002/master-agreement-type-1-0" },
								{	"masterConfirmationTypeScheme",
									"http://www.fpml.org/spec/2003/master-confirmation-type-1-0" },
								{	"partyIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{	"restructuringScheme",
									"http://www.fpml.org/spec/2003/restructuring-1-0" },
								{ 	"routingIdScheme",
									"http://www.fpml.org/spec/2002/routing-id-code-1-0" },
								{ 	"settlementMethodScheme",
									"http://www.fpml.org/spec/2002/settlement-method-1-0" },
								{ 	"settlementPriceSourceScheme",
									"http://www.fpml.org/spec/2002/settlement-price-source-1-0" }
							}),
							ParseSchemes ("Schemes4-0"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-1 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_1
			= new SchemaRelease (FPML, "4-1",
					"http://www.fpml.org/2004/FpML-4-1", "fpml-main-4-1.xsd",
					"fpml", "fpml4-1", "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"additionalTermScheme",
									"http://www.fpml.org/spec/2003/additional-term-1-0" },
								{	"businessCenterScheme",
									"http://www.fpml.org/spec/2000/business-center-1-0" },
								{	"clearanceSystemIdScheme",
									"http://www.fpml.org/spec/2002/clearance-system-1-0" },
								{	"contractualDefinitionsScheme",
									"http://www.fpml.org/spec/2003/contractual-definitions-2-0" },
								{	"contractualSupplementScheme",
									"http://www.fpml.org/spec/2003/contractual-supplement-1-0" },
								{	"countryScheme",
									"http://www.fpml.org/ext/iso3166" },
								{	"currencyScheme",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"cutNameScheme",
									"http://www.fpml.org/spec/2002/cut-name-1-0" },
								{	"exchangeIdScheme",
									"http://www.fpml.org/spec/2002/exchange-id-MIC-1-0" },
								{	"floatingRateIndexScheme",
									"http://www.fpml.org/ext/isda-2000-definitions" },
								{	"fxFeatureTypeScheme",
									"http://www.fpml.org/spec/2003/fxFeatureType-1-0" },
								{	"governingLawScheme",
									"http://www.fpml.org/spec/2002/governing-law-1-0" },
								{	"informationProviderScheme",
									"http://www.fpml.org/spec/2003/information-provider-2-0" },
								{	"marketDisruptionScheme",
									"http://www.fpml.org/spec/2003/marketDisruption-1-0" },
								{	"masterAgreementTypeScheme",
									"http://www.fpml.org/spec/2002/master-agreement-type-1-0" },
								{	"masterConfirmationTypeScheme",
									"http://www.fpml.org/spec/2003/master-confirmation-type-1-0" },
								{	"partyIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{	"restructuringScheme",
									"http://www.fpml.org/spec/2003/restructuring-1-0" },
								{ 	"routingIdScheme",
									"http://www.fpml.org/spec/2002/routing-id-code-1-0" },
								{ 	"settlementMethodScheme",
									"http://www.fpml.org/spec/2002/settlement-method-1-0" },
								{ 	"settlementPriceSourceScheme",
									"http://www.fpml.org/spec/2002/settlement-price-source-1-0" }
							}),
							ParseSchemes ("Schemes4-1"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-2 trial recommendation.
		/// </summary>
		public static readonly SchemaRelease	TR4_2
			= new SchemaRelease (FPML, "4-2",
					"http://www.fpml.org/2005/FpML-4-2", "fpml-main-4-2.xsd",
					"fpml", "fpml4-2", "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"additionalTermScheme",
									"http://www.fpml.org/spec/2003/additional-term-1-0" },
								{	"businessCenterScheme",
									"http://www.fpml.org/spec/2000/business-center-1-0" },
								{	"clearanceSystemIdScheme",
									"http://www.fpml.org/spec/2002/clearance-system-1-0" },
								{	"contractualDefinitionsScheme",
									"http://www.fpml.org/spec/2003/contractual-definitions-2-0" },
								{	"contractualSupplementScheme",
									"http://www.fpml.org/spec/2003/contractual-supplement-1-0" },
								{	"countryScheme",
									"http://www.fpml.org/ext/iso3166" },
								{	"currencyScheme",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"cutNameScheme",
									"http://www.fpml.org/spec/2002/cut-name-1-0" },
								{	"exchangeIdScheme",
									"http://www.fpml.org/spec/2002/exchange-id-MIC-1-0" },
								{	"floatingRateIndexScheme",
									"http://www.fpml.org/ext/isda-2000-definitions" },
								{	"fxFeatureTypeScheme",
									"http://www.fpml.org/spec/2003/fxFeatureType-1-0" },
								{	"governingLawScheme",
									"http://www.fpml.org/spec/2002/governing-law-1-0" },
								{	"informationProviderScheme",
									"http://www.fpml.org/spec/2003/information-provider-2-0" },
								{	"marketDisruptionScheme",
									"http://www.fpml.org/spec/2003/marketDisruption-1-0" },
								{	"masterAgreementTypeScheme",
									"http://www.fpml.org/spec/2002/master-agreement-type-1-0" },
								{	"masterConfirmationTypeScheme",
									"http://www.fpml.org/spec/2003/master-confirmation-type-1-0" },
								{	"partyIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{	"restructuringScheme",
									"http://www.fpml.org/spec/2003/restructuring-1-0" },
								{ 	"routingIdScheme",
									"http://www.fpml.org/spec/2002/routing-id-code-1-0" },
								{ 	"settlementMethodScheme",
									"http://www.fpml.org/spec/2002/settlement-method-1-0" },
								{ 	"settlementPriceSourceScheme",
									"http://www.fpml.org/spec/2002/settlement-price-source-1-0" }
							}),
							ParseSchemes ("Schemes4-2"));


		/// <summary>
		/// The FpML 1.0 to 2.0 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R1_0__R2_0
			= new Conversions.R1_0__R2_0 ();

		/// <summary>
		/// The FpML 2.0 to 3.0 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R2_0__TR3_0
			= new Conversions.R2_0__TR3_0 ();

		/// <summary>
		/// The FpML 3.0 to 4.0 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion TR3_0__R4_0
			= new Conversions.TR3_0__R4_0 ();

		/// <summary>
		/// The FpML 4.0 to 4.1 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R4_0__R4_1
			= new Conversions.R4_0__R4_1 ();

		/// <summary>
		/// The FpML 4.1 to 4.2 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R4_1__TR4_2
			= new Conversions.R4_1__TR4_2 ();

		/// <summary>
		/// <see cref="ILog"/> instance used to record problems.
		/// </summary>
		private static ILog			log
			= LogManager.GetLogger (typeof (Releases));

		/// <summary>
		/// Ensures that no instances can be created.
		/// </summary>
		private Releases ()
		{ }

		/// <summary>
		/// Attempts to build a <see cref="SchemeCollection"/> instance for an
		/// indicated FpML release.
		/// </summary>
		/// <param name="suffix">Indicates which version of FpML.</param>
		/// <returns>A populated <see cref="SchemeCollection"/> instance.</returns>
		private static SchemeCollection ParseSchemes (string suffix)
		{
			SchemeCollection	schemes	= new SchemeCollection ();

			try {
				schemes.Parse (Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
					ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit." + suffix]));
            }
			catch (Exception error) {
				log.Fatal ("Unable to load standard FpML schemes", error);
			}

			try {
				schemes.Parse (Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
					ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.AdditionalSchemes"]));
			}
			catch (Exception error) {
				log.Fatal ("Unable to load additional FpML schemes", error);
			}

			return (schemes);
		}

		/// <summary>
		/// Ensures that a default resolver is in place and the schema releases are
		/// linked to the DSIG schema.
		/// </summary>
		static Releases ()
		{
			DTDRelease.DefaultResolver = FpMLUtility.GetResolver ();

			R4_0.AddImport (HandCoded.DSig.Releases.R1_0);
			R4_1.AddImport (HandCoded.DSig.Releases.R1_0);
			TR4_2.AddImport (HandCoded.DSig.Releases.R1_0);
		}
	}
}