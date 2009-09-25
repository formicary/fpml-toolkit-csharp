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
		/// The <see cref="HandCoded.Meta.InstanceInitialiser"/> used to populate new documents.
		/// </summary>
		private static HandCoded.Meta.InstanceInitialiser	initialiser
			= new FpMLInstanceInitialiser ();
		
		/// <summary>
		/// The <see cref="HandCoded.Meta.SchemaRecogniser"/> used to detect schema based documents.
		/// </summary>
		private static HandCoded.Meta.SchemaRecogniser	recogniser
			= new FpMLSchemaRecogniser ();
		
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
						ParseSchemes ("schemes1-0.xml"));

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
							ParseSchemes ("schemes2-0.xml"));

		/// <summary>
		/// A <see cref="DTDRelease"/> instance containing the details for the
		/// FpML 3-0 trial recommendation.
		/// </summary>
		public static readonly DTDRelease	R3_0
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
								{ 	"definitionsSchemeDefault",
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
								{ 	"compoundingMethodScheme",
									"compoundingMethodSchemeDefault" },
							    {	"contractualDefinitionsScheme",
									"definitionsSchemeDefault" },
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
									"nationalisationOrInsolvencyOrDelistingSchemeDefault" },	// Irregular
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
							ParseSchemes ("schemes3-0.xml"));

		
		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-0 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_0
			= new SchemaRelease (FPML, "4-0",
					"http://www.fpml.org/2003/FpML-4-0", "fpml-main-4-0.xsd",
					"fpml", "fpml4-0", initialiser, recogniser, "FpML",
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
							ParseSchemes ("schemes4-0.xml"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-1 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_1
			= new SchemaRelease (FPML, "4-1",
					"http://www.fpml.org/2004/FpML-4-1", "fpml-main-4-1.xsd",
					"fpml", "fpml4-1", initialiser, recogniser, "FpML",
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
							ParseSchemes ("schemes4-1.xml"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-2 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_2
			= new SchemaRelease (FPML, "4-2",
					"http://www.fpml.org/2005/FpML-4-2", "fpml-main-4-2.xsd",
					"fpml", "fpml4-2", initialiser, recogniser, "FpML",
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
							ParseSchemes ("schemes4-2.xml"));


		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-3 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_3
			= new SchemaRelease (FPML, "4-3",
					"http://www.fpml.org/2007/FpML-4-3", "fpml-main-4-3.xsd",
					"fpml", "fpml4-3", initialiser, recogniser, "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"additionalTermScheme",
									"http://www.fpml.org/spec/2003/additional-term-1-0" },
								{	"businessCenterScheme",
									"http://www.fpml.org/spec/2000/business-center-6-2" },
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
							ParseSchemes ("schemes4-3.xml"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-4 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_4
			= new SchemaRelease (FPML, "4-4",
					"http://www.fpml.org/2007/FpML-4-4", "fpml-main-4-4.xsd",
					"fpml", "fpml4-4", initialiser, recogniser, "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"assetMeasureScheme",
									"http://www.fpml.org/coding-scheme/asset-measure-5-0" },
								{	"brokerConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/broker-confirmation-type-3-2" },
								{	"businessCenterScheme",
									"http://www.fpml.org/coding-scheme/business-center-6-4" },
								{	"cashflowTypeScheme",
									"http://www.fpml.org/coding-scheme/cashflow-type-2-0" },
								{	"clearanceSystemIdScheme",
									"http://www.fpml.org/coding-scheme/clearance-system-1-0" },
								{	"compoundingFrequencyScheme",
									"http://www.fpml.org/coding-scheme/compounding-frequency-1-0" },
								{	"contractualDefinitionsScheme",
									"http://www.fpml.org/coding-scheme/contractual-definitions-3-2" },
								{	"contractualSupplementScheme",
									"http://www.fpml.org/coding-scheme/contractual-supplement-6-3" },
								{	"countryScheme",
									"http://www.fpml.org/ext/iso3166" },
								{	"couponTypeScheme",
									"http://www.fpml.org/coding-scheme/coupon-type-1-0" },
								{	"creditSeniorityScheme",
									"http://www.fpml.org/coding-scheme/credit-seniority-1-0" },
								{	"currencyScheme",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"cutNameScheme",
									"http://www.fpml.org/coding-scheme/cut-name-1-0" },
								{	"dayCountFractionScheme",
									"http://www.fpml.org/coding-scheme/day-count-fraction-2-1" },
								{	"derivativeCalculationMethodScheme",
									"http://www.fpml.org/coding-scheme/derivative-calculation-method-1-0" },
								{	"entityIdScheme",
									"http://www.fpml.org/spec/2003/entity-id-RED-1-0" },
								{	"entityNameScheme",
									"http://www.fpml.org/spec/2003/entity-name-RED-1-0" },
								{	"entityTypeScheme",
									"http://www.fpml.org/coding-scheme/entity-type-1-0" },
								{	"exchangeIdScheme",
									"http://www.fpml.org/spec/2002/exchange-id-MIC-1-0" },
								{	"facilityTypeScheme",
									"http://www.fpml.org/coding-scheme/facility-type-1-0" },
								{	"floatingRateIndexScheme",
									"http://www.fpml.org/coding-scheme/floating-rate-index-2-0" },
								{	"governingLawScheme",
									"http://www.fpml.org/coding-scheme/governing-law-1-0" },
								{	"indexAnnexSourceScheme",
									"http://www.fpml.org/coding-scheme/cdx-index-annex-source-1-0" },
								{	"informationProviderScheme",
									"http://www.fpml.org/coding-scheme/information-provider-2-0" },
								{	"interpolationMethodScheme",
									"http://www.fpml.org/coding-scheme/interpolation-method-1-0" },
								{	"lienScheme",
									"http://www.fpml.org/coding-scheme/designated-priority-1-0" },
								{	"loanTrancheScheme",
									"http://www.fpml.org/coding-scheme/underlying-asset-tranche" },
								{	"mainPublicationScheme",
									"http://www.fpml.org/coding-scheme/inflation-main-publication-1-0" },
								{	"marketDisruptionScheme",
									"http://www.fpml.org/coding-scheme/market-disruption-1-0" },
								{	"masterAgreementTypeScheme",
									"http://www.fpml.org/coding-scheme/master-agreement-type-1-0" },
								{	"masterConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/master-confirmation-type-5-3" },
								{	"matrixTermScheme",
									"http://www.fpml.org/coding-scheme/credit-matrix-transaction-type-2-2" },
								{	"matrixTypeScheme",
									"http://www.fpml.org/coding-scheme/matrix-type-1-0" },
								{	"mortgageSectorScheme",
									"http://www.fpml.org/coding-scheme/mortgage-sector-1-0" },
								{	"partyIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{	"perturbationTypeScheme",
									"http://www.fpml.org/coding-scheme/perturbation-type-1-0" },
								{	"positionStatusScheme",
									"http://www.fpml.org/coding-scheme/position-status-1-0" },
								{	"priceQuoteUnitsScheme",
									"http://www.fpml.org/coding-scheme/price-quote-units-1-1" },
								{	"pricingInputTypeScheme",
									"http://www.fpml.org/coding-scheme/pricing-input-type-1-0" },
								{	"productTypeScheme",
									"http://www.fpml.org/coding-scheme/product-type-simple-1-2" },
								{	"queryParameterOperatorScheme",
									"http://www.fpml.org/coding-scheme/query-parameter-operator-1-0" },
								{	"quoteTimingScheme",
									"http://www.fpml.org/coding-scheme/quote-timing-1-0" },
								{	"reasonCodeScheme",
									"http://www.fpml.org/coding-scheme/reason-code-1-0" },
								{	"restructuringScheme",
									"http://www.fpml.org/coding-scheme/restructuring-1-0" },
								{ 	"routingIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{ 	"scheduledDateTypeScheme",
									"http://www.fpml.org/coding-scheme/scheduled-date-type-1-0" },
								{ 	"settledEntityMatrixSourceScheme",
									"http://www.fpml.org/coding-scheme/settled-entity-matrix-source-1-0" },
								{ 	"settlementMethodScheme",
									"http://www.fpml.org/coding-scheme/settlement-method-1-0" },
								{ 	"settlementPriceSourceScheme",
									"http://www.fpml.org/coding-scheme/settlement-price-source-1-0" },
								{ 	"spreadScheduleTypeScheme",
									"http://www.fpml.org/coding-scheme/spread-schedule-type-1-0" },
								{ 	"tradeCashflowsStatusScheme",
									"http://www.fpml.org/coding-scheme/trade-cashflows-status-1-0" }
							}),
							ParseSchemes ("schemes4-4.xml"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-5 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_5
			= new SchemaRelease (FPML, "4-5",
					"http://www.fpml.org/2008/FpML-4-5", "fpml-main-4-5.xsd",
					"fpml", "fpml4-5", initialiser, recogniser, "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"assetMeasureScheme",
									"http://www.fpml.org/coding-scheme/asset-measure" },
								{	"brokerConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/broker-confirmation-type" },
								{	"businessCenterScheme",
									"http://www.fpml.org/coding-scheme/business-center" },
								{	"cashflowTypeScheme",
									"http://www.fpml.org/coding-scheme/cashflow-type" },
								{	"clearanceSystemIdScheme",
									"http://www.fpml.org/coding-scheme/clearance-system" },
								{	"compoundingFrequencyScheme",
									"http://www.fpml.org/coding-scheme/compounding-frequency" },
								{	"contractualDefinitionsScheme",
									"http://www.fpml.org/coding-scheme/contractual-definitions" },
								{	"contractualSupplementScheme",
									"http://www.fpml.org/coding-scheme/contractual-supplement" },
								{	"countryScheme",
									"http://www.fpml.org/ext/iso3166" },
								{	"couponTypeScheme",
									"http://www.fpml.org/coding-scheme/coupon-type" },
								{	"creditSeniorityScheme",
									"http://www.fpml.org/coding-scheme/credit-seniority" },
								{	"currencyScheme",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"cutNameScheme",
									"http://www.fpml.org/coding-scheme/cut-name" },
								{	"dayCountFractionScheme",
									"http://www.fpml.org/coding-scheme/day-count-fraction" },
								{	"derivativeCalculationMethodScheme",
									"http://www.fpml.org/coding-scheme/derivative-calculation-method" },
								{	"entityIdScheme",
									"http://www.fpml.org/spec/2003/entity-id-RED" },
								{	"entityNameScheme",
									"http://www.fpml.org/spec/2003/entity-name-RED" },
								{	"entityTypeScheme",
									"http://www.fpml.org/coding-scheme/entity-type" },
								{	"exchangeIdScheme",
									"http://www.fpml.org/spec/2002/exchange-id-MIC" },
								{	"facilityTypeScheme",
									"http://www.fpml.org/coding-scheme/facility-type" },
								{	"floatingRateIndexScheme",
									"http://www.fpml.org/coding-scheme/floating-rate-index" },
								{	"governingLawScheme",
									"http://www.fpml.org/coding-scheme/governing-law" },
								{	"indexAnnexSourceScheme",
									"http://www.fpml.org/coding-scheme/cdx-index-annex-source" },
								{	"informationProviderScheme",
									"http://www.fpml.org/coding-scheme/information-provider" },
								{	"interpolationMethodScheme",
									"http://www.fpml.org/coding-scheme/interpolation-method" },
								{	"lienScheme",
									"http://www.fpml.org/coding-scheme/designated-priority" },
								{	"loanTrancheScheme",
									"http://www.fpml.org/coding-scheme/underlying-asset-tranche" },
								{	"mainPublicationScheme",
									"http://www.fpml.org/coding-scheme/inflation-main-publication" },
								{	"marketDisruptionScheme",
									"http://www.fpml.org/coding-scheme/market-disruption" },
								{	"masterAgreementTypeScheme",
									"http://www.fpml.org/coding-scheme/master-agreement-type" },
								{	"masterConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/master-confirmation-type" },
								{	"matrixTermScheme",
									"http://www.fpml.org/coding-scheme/credit-matrix-transaction-type" },
								{	"matrixTypeScheme",
									"http://www.fpml.org/coding-scheme/matrix-type" },
								{	"mortgageSectorScheme",
									"http://www.fpml.org/coding-scheme/mortgage-sector" },
								{	"partyIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{	"perturbationTypeScheme",
									"http://www.fpml.org/coding-scheme/perturbation-type" },
								{	"positionStatusScheme",
									"http://www.fpml.org/coding-scheme/position-status" },
								{	"priceQuoteUnitsScheme",
									"http://www.fpml.org/coding-scheme/price-quote-units" },
								{	"pricingInputTypeScheme",
									"http://www.fpml.org/coding-scheme/pricing-input-type" },
								{	"productTypeScheme",
									"http://www.fpml.org/coding-scheme/product-type-simple" },
								{	"queryParameterOperatorScheme",
									"http://www.fpml.org/coding-scheme/query-parameter-operator" },
								{	"quoteTimingScheme",
									"http://www.fpml.org/coding-scheme/quote-timing" },
								{	"reasonCodeScheme",
									"http://www.fpml.org/coding-scheme/reason-code" },
								{	"restructuringScheme",
									"http://www.fpml.org/coding-scheme/restructuring" },
								{ 	"routingIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{ 	"scheduledDateTypeScheme",
									"http://www.fpml.org/coding-scheme/scheduled-date-type" },
								{ 	"settledEntityMatrixSourceScheme",
									"http://www.fpml.org/coding-scheme/settled-entity-matrix-source" },
								{ 	"settlementMethodScheme",
									"http://www.fpml.org/coding-scheme/settlement-method" },
								{ 	"settlementPriceSourceScheme",
									"http://www.fpml.org/coding-scheme/settlement-price-source" },
								{ 	"spreadScheduleTypeScheme",
									"http://www.fpml.org/coding-scheme/spread-schedule-type" },
								{ 	"tradeCashflowsStatusScheme",
									"http://www.fpml.org/coding-scheme/trade-cashflows-status" }
							}),
							ParseSchemes ("schemes4-5.xml"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 4-6 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R4_6
			= new SchemaRelease (FPML, "4-6",
					"http://www.fpml.org/2009/FpML-4-6", "fpml-main-4-6.xsd",
					"fpml", "fpml4-6", initialiser, recogniser, "FpML",
					new SchemeDefaults (
							new string [,] {
								{	"assetMeasureScheme",
									"http://www.fpml.org/coding-scheme/asset-measure" },
								{	"brokerConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/broker-confirmation-type" },
								{	"businessCenterScheme",
									"http://www.fpml.org/coding-scheme/business-center" },
								{	"cashflowTypeScheme",
									"http://www.fpml.org/coding-scheme/cashflow-type" },
								{	"clearanceSystemIdScheme",
									"http://www.fpml.org/coding-scheme/clearance-system" },
								{	"compoundingFrequencyScheme",
									"http://www.fpml.org/coding-scheme/compounding-frequency" },
								{	"contractualDefinitionsScheme",
									"http://www.fpml.org/coding-scheme/contractual-definitions" },
								{	"contractualSupplementScheme",
									"http://www.fpml.org/coding-scheme/contractual-supplement" },
								{	"countryScheme",
									"http://www.fpml.org/ext/iso3166" },
								{	"couponTypeScheme",
									"http://www.fpml.org/coding-scheme/coupon-type" },
								{	"creditSeniorityScheme",
									"http://www.fpml.org/coding-scheme/credit-seniority" },
								{	"currencyScheme",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"cutNameScheme",
									"http://www.fpml.org/coding-scheme/cut-name" },
								{	"dayCountFractionScheme",
									"http://www.fpml.org/coding-scheme/day-count-fraction" },
								{	"derivativeCalculationMethodScheme",
									"http://www.fpml.org/coding-scheme/derivative-calculation-method" },
								{	"entityIdScheme",
									"http://www.fpml.org/spec/2003/entity-id-RED" },
								{	"entityNameScheme",
									"http://www.fpml.org/spec/2003/entity-name-RED" },
								{	"entityTypeScheme",
									"http://www.fpml.org/coding-scheme/entity-type" },
								{	"exchangeIdScheme",
									"http://www.fpml.org/spec/2002/exchange-id-MIC" },
								{	"facilityTypeScheme",
									"http://www.fpml.org/coding-scheme/facility-type" },
								{	"floatingRateIndexScheme",
									"http://www.fpml.org/coding-scheme/floating-rate-index" },
								{	"governingLawScheme",
									"http://www.fpml.org/coding-scheme/governing-law" },
								{	"indexAnnexSourceScheme",
									"http://www.fpml.org/coding-scheme/cdx-index-annex-source" },
								{	"informationProviderScheme",
									"http://www.fpml.org/coding-scheme/information-provider" },
								{	"interpolationMethodScheme",
									"http://www.fpml.org/coding-scheme/interpolation-method" },
								{	"lienScheme",
									"http://www.fpml.org/coding-scheme/designated-priority" },
								{	"loanTrancheScheme",
									"http://www.fpml.org/coding-scheme/underlying-asset-tranche" },
								{	"mainPublicationScheme",
									"http://www.fpml.org/coding-scheme/inflation-main-publication" },
								{	"marketDisruptionScheme",
									"http://www.fpml.org/coding-scheme/market-disruption" },
								{	"masterAgreementTypeScheme",
									"http://www.fpml.org/coding-scheme/master-agreement-type" },
								{	"masterConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/master-confirmation-type" },
								{	"matrixTermScheme",
									"http://www.fpml.org/coding-scheme/credit-matrix-transaction-type" },
								{	"matrixTypeScheme",
									"http://www.fpml.org/coding-scheme/matrix-type" },
								{	"mortgageSectorScheme",
									"http://www.fpml.org/coding-scheme/mortgage-sector" },
								{	"partyIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{	"perturbationTypeScheme",
									"http://www.fpml.org/coding-scheme/perturbation-type" },
								{	"positionStatusScheme",
									"http://www.fpml.org/coding-scheme/position-status" },
								{	"priceQuoteUnitsScheme",
									"http://www.fpml.org/coding-scheme/price-quote-units" },
								{	"pricingInputTypeScheme",
									"http://www.fpml.org/coding-scheme/pricing-input-type" },
								{	"productTypeScheme",
									"http://www.fpml.org/coding-scheme/product-type-simple" },
								{	"queryParameterOperatorScheme",
									"http://www.fpml.org/coding-scheme/query-parameter-operator" },
								{	"quoteTimingScheme",
									"http://www.fpml.org/coding-scheme/quote-timing" },
								{	"reasonCodeScheme",
									"http://www.fpml.org/coding-scheme/reason-code" },
								{	"restructuringScheme",
									"http://www.fpml.org/coding-scheme/restructuring" },
								{ 	"routingIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{ 	"scheduledDateTypeScheme",
									"http://www.fpml.org/coding-scheme/scheduled-date-type" },
								{ 	"settledEntityMatrixSourceScheme",
									"http://www.fpml.org/coding-scheme/settled-entity-matrix-source" },
								{ 	"settlementMethodScheme",
									"http://www.fpml.org/coding-scheme/settlement-method" },
								{ 	"settlementPriceSourceScheme",
									"http://www.fpml.org/coding-scheme/settlement-price-source" },
								{ 	"spreadScheduleTypeScheme",
									"http://www.fpml.org/coding-scheme/spread-schedule-type" },
								{ 	"tradeCashflowsStatusScheme",
									"http://www.fpml.org/coding-scheme/trade-cashflows-status" }
							}),
							ParseSchemes ("schemes4-6.xml"));

        /// <summary>
        /// A <see cref="SchemaRelease"/> instance containing the details for
        /// FpML 4-7 recommendation.
        /// </summary>
        public static readonly SchemaRelease R4_7
            = new SchemaRelease (FPML, "4-7",
                    "http://www.fpml.org/2009/FpML-4-7", "fpml-main-4-7.xsd",
                    "fpml", "fpml4-7", initialiser, recogniser, "FpML",
                    new SchemeDefaults (
                            new string [,] {
								{	"assetMeasureScheme",
									"http://www.fpml.org/coding-scheme/asset-measure" },
								{	"brokerConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/broker-confirmation-type" },
								{	"businessCenterScheme",
									"http://www.fpml.org/coding-scheme/business-center" },
								{	"cashflowTypeScheme",
									"http://www.fpml.org/coding-scheme/cashflow-type" },
								{	"clearanceSystemIdScheme",
									"http://www.fpml.org/coding-scheme/clearance-system" },
								{	"compoundingFrequencyScheme",
									"http://www.fpml.org/coding-scheme/compounding-frequency" },
								{	"contractualDefinitionsScheme",
									"http://www.fpml.org/coding-scheme/contractual-definitions" },
								{	"contractualSupplementScheme",
									"http://www.fpml.org/coding-scheme/contractual-supplement" },
								{	"countryScheme",
									"http://www.fpml.org/ext/iso3166" },
								{	"couponTypeScheme",
									"http://www.fpml.org/coding-scheme/coupon-type" },
								{	"creditSeniorityScheme",
									"http://www.fpml.org/coding-scheme/credit-seniority" },
								{	"currencyScheme",
									"http://www.fpml.org/ext/iso4217-2001-08-15" },
								{	"cutNameScheme",
									"http://www.fpml.org/coding-scheme/cut-name" },
								{	"dayCountFractionScheme",
									"http://www.fpml.org/coding-scheme/day-count-fraction" },
								{	"derivativeCalculationMethodScheme",
									"http://www.fpml.org/coding-scheme/derivative-calculation-method" },
								{	"entityIdScheme",
									"http://www.fpml.org/spec/2003/entity-id-RED" },
								{	"entityNameScheme",
									"http://www.fpml.org/spec/2003/entity-name-RED" },
								{	"entityTypeScheme",
									"http://www.fpml.org/coding-scheme/entity-type" },
								{	"exchangeIdScheme",
									"http://www.fpml.org/spec/2002/exchange-id-MIC" },
								{	"facilityTypeScheme",
									"http://www.fpml.org/coding-scheme/facility-type" },
								{	"floatingRateIndexScheme",
									"http://www.fpml.org/coding-scheme/floating-rate-index" },
								{	"governingLawScheme",
									"http://www.fpml.org/coding-scheme/governing-law" },
								{	"indexAnnexSourceScheme",
									"http://www.fpml.org/coding-scheme/cdx-index-annex-source" },
								{	"informationProviderScheme",
									"http://www.fpml.org/coding-scheme/information-provider" },
								{	"interpolationMethodScheme",
									"http://www.fpml.org/coding-scheme/interpolation-method" },
								{	"lienScheme",
									"http://www.fpml.org/coding-scheme/designated-priority" },
								{	"loanTrancheScheme",
									"http://www.fpml.org/coding-scheme/underlying-asset-tranche" },
								{	"mainPublicationScheme",
									"http://www.fpml.org/coding-scheme/inflation-main-publication" },
								{	"marketDisruptionScheme",
									"http://www.fpml.org/coding-scheme/market-disruption" },
								{	"masterAgreementTypeScheme",
									"http://www.fpml.org/coding-scheme/master-agreement-type" },
								{	"masterConfirmationTypeScheme",
									"http://www.fpml.org/coding-scheme/master-confirmation-type" },
								{	"matrixTermScheme",
									"http://www.fpml.org/coding-scheme/credit-matrix-transaction-type" },
								{	"matrixTypeScheme",
									"http://www.fpml.org/coding-scheme/matrix-type" },
								{	"mortgageSectorScheme",
									"http://www.fpml.org/coding-scheme/mortgage-sector" },
								{	"partyIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{	"perturbationTypeScheme",
									"http://www.fpml.org/coding-scheme/perturbation-type" },
								{	"positionStatusScheme",
									"http://www.fpml.org/coding-scheme/position-status" },
								{	"priceQuoteUnitsScheme",
									"http://www.fpml.org/coding-scheme/price-quote-units" },
								{	"pricingInputTypeScheme",
									"http://www.fpml.org/coding-scheme/pricing-input-type" },
								{	"productTypeScheme",
									"http://www.fpml.org/coding-scheme/product-type-simple" },
								{	"queryParameterOperatorScheme",
									"http://www.fpml.org/coding-scheme/query-parameter-operator" },
								{	"quoteTimingScheme",
									"http://www.fpml.org/coding-scheme/quote-timing" },
								{	"reasonCodeScheme",
									"http://www.fpml.org/coding-scheme/reason-code" },
								{	"restructuringScheme",
									"http://www.fpml.org/coding-scheme/restructuring" },
								{ 	"routingIdScheme",
									"http://www.fpml.org/ext/iso9362" },
								{ 	"scheduledDateTypeScheme",
									"http://www.fpml.org/coding-scheme/scheduled-date-type" },
								{ 	"settledEntityMatrixSourceScheme",
									"http://www.fpml.org/coding-scheme/settled-entity-matrix-source" },
								{ 	"settlementMethodScheme",
									"http://www.fpml.org/coding-scheme/settlement-method" },
								{ 	"settlementPriceSourceScheme",
									"http://www.fpml.org/coding-scheme/settlement-price-source" },
								{ 	"spreadScheduleTypeScheme",
									"http://www.fpml.org/coding-scheme/spread-schedule-type" },
								{ 	"tradeCashflowsStatusScheme",
									"http://www.fpml.org/coding-scheme/trade-cashflows-status" }
							}),
                            ParseSchemes ("schemes4-7.xml"));

        /// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 5-0 confirmation view recommendation.
		/// </summary>
		public static readonly SchemaRelease	R5_0_CONFIRMATION
			= new SchemaRelease (FPML, "5-0",
					"http://www.fpml.org/FpML-5-0/confirmation", "fpml-main-5-0.xsd",
					"fpml", "fpml5-0", initialiser, recogniser,
					new String [] {
						// In all views
				  		"messageRejected",
				  		"requestTradeStatus",
				  		"tradeNotFound",
				  		"tradeStatus",
				  		"tradeAlreadyCancelled",
				 		"tradeAlreadySubmitted",
				 		"tradeAlreadyTerminated",
				 		// View specific
				  		"cancelTradeConfirmation",
				  		"confirmationCancelled",
				  		"confirmTrade",
				  		"modifyTradeConfirmation",
				  		"requestTradeConfirmation",
				 		"tradeAffirmation",
				 		"tradeAffirmed",
				  		"tradeAlreadyAffirmed",
				  		"tradeAlreadyConfirmed",
				 		"tradeConfirmed"
					},
					new SchemeDefaults (
							new string [,] {
								{	"additionalTermScheme",
									"http://www.fpml.org/spec/2003/additional-term-1-0" },
								{	"businessCenterScheme",
									"http://www.fpml.org/spec/2000/business-center-6-2" },
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
							ParseSchemes ("schemes5-0.xml"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 5-0 pretrade view recommendation.
		/// </summary>
		public static readonly SchemaRelease	R5_0_PRETRADE
			= new SchemaRelease (FPML, "5-0",
					"http://www.fpml.org/FpML-5-0/pretrade", "fpml-main-5-0.xsd",
					"fpml", "fpml5-0", initialiser, recogniser,
					new String [] {
						// In all views
				  		"messageRejected",
				  		"requestTradeStatus",
				  		"tradeNotFound",
				  		"tradeStatus",
				  		"tradeAlreadyCancelled",
				 		"tradeAlreadySubmitted",
				 		"tradeAlreadyTerminated",
				 		// View specific
				 		"acceptQuote",
						"quoteAcceptanceConfirmed",
						"quoteAlreadyExpired",
						"quoteUpdated",
						"requestQuote",
						"requestQuoteResponse",
					},
					new SchemeDefaults (
							new string [,] {
								{	"additionalTermScheme",
									"http://www.fpml.org/spec/2003/additional-term-1-0" },
								{	"businessCenterScheme",
									"http://www.fpml.org/spec/2000/business-center-6-2" },
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
							ParseSchemes ("schemes5-0.xml"));

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for
		/// FpML 5-0 pretrade view recommendation.
		/// </summary>
		public static readonly SchemaRelease	R5_0_REPORTING
			= new SchemaRelease (FPML, "5-0",
					"http://www.fpml.org/FpML-5-0/reporting", "fpml-main-5-0.xsd",
					"fpml", "fpml5-0", initialiser, recogniser,
					new String [] {
						// In all views
				  		"messageRejected",
				  		"requestTradeStatus",
				  		"tradeNotFound",
				  		"tradeStatus",
				  		"tradeAlreadyCancelled",
				 		"tradeAlreadySubmitted",
				 		"tradeAlreadyTerminated",
				 		// View specific
				 		"positionReport",
				 		"requestPositionReport",
				 		"requestValuationReport",
				 		"valuationReport",
				 		"cancelTradeCashflows",
						"tradeCashflowsAsserted",
						"tradeCashflowsMatchResult",
						"positionsAcknowledged",
						"positionsAsserted",
						"positionsMatchResults",
						"requestPortfolio"
					},
					new SchemeDefaults (
							new string [,] {
								{	"additionalTermScheme",
									"http://www.fpml.org/spec/2003/additional-term-1-0" },
								{	"businessCenterScheme",
									"http://www.fpml.org/spec/2000/business-center-6-2" },
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
							ParseSchemes ("schemes5-0.xml"));

		/// <summary>
		/// The FpML 1.0 to 2.0 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R1_0__R2_0
			= new Conversions.R1_0__R2_0 ();

		/// <summary>
		/// The FpML 2.0 to 3.0 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R2_0__R3_0
			= new Conversions.R2_0__R3_0 ();

		/// <summary>
		/// The FpML 3.0 to 4.0 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R3_0__R4_0
			= new Conversions.R3_0__R4_0 ();

		/// <summary>
		/// The FpML 4.0 to 4.1 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R4_0__R4_1
			= new Conversions.R4_0__R4_1 ();

		/// <summary>
		/// The FpML 4.1 to 4.2 <see cref="HandCoded.Meta.Conversion"/> instance.
		/// </summary>
		private static readonly HandCoded.Meta.Conversion R4_1__R4_2
			= new Conversions.R4_1__R4_2 ();

        /// <summary>
        /// The FpML 4.2 to 4.3 <see cref="HandCoded.Meta.Conversion"/> instance.
        /// </summary>
        private static readonly HandCoded.Meta.Conversion R4_2__R4_3
            = new Conversions.R4_2__R4_3 ();

        /// <summary>
        /// The FpML 4.3 to 4.4 <see cref="HandCoded.Meta.Conversion"/> instance.
        /// </summary>
        private static readonly HandCoded.Meta.Conversion R4_3__R4_4
            = new Conversions.R4_3__R4_4 ();

        /// <summary>
        /// The FpML 4.4 to 4.5 <see cref="HandCoded.Meta.Conversion"/> instance.
        /// </summary>
        private static readonly HandCoded.Meta.Conversion R4_4__R4_5
            = new Conversions.R4_4__R4_5 ();

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
				schemes.Parse (
					Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
						Path.Combine (
							ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.SchemesDirectory"], suffix)));
            }
			catch (Exception error) {
				log.Fatal ("Unable to load standard FpML schemes", error);
			}

			try {
				schemes.Parse (
					Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
						Path.Combine (
							ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.SchemesDirectory"],
							ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.AdditionalSchemes"])));
			}
			catch (Exception error) {
				log.Fatal ("Unable to load additional FpML schemes", error);
			}

			return (schemes);
		}

		/// <summary>
		/// Ensures that schema releases are linked to the DSIG schema.
		/// </summary>
		static Releases ()
		{
			R4_0.AddImport (HandCoded.DSig.Releases.R1_0);
			R4_1.AddImport (HandCoded.DSig.Releases.R1_0);
			R4_2.AddImport (HandCoded.DSig.Releases.R1_0);
			R4_3.AddImport (HandCoded.DSig.Releases.R1_0);
			R4_4.AddImport (HandCoded.DSig.Releases.R1_0);
			R4_5.AddImport (HandCoded.DSig.Releases.R1_0);
			R4_6.AddImport (HandCoded.DSig.Releases.R1_0);
            R4_7.AddImport (HandCoded.DSig.Releases.R1_0);

			R5_0_CONFIRMATION.AddImport (HandCoded.DSig.Releases.R1_0);
			R5_0_PRETRADE.AddImport (HandCoded.DSig.Releases.R1_0);
			R5_0_REPORTING.AddImport (HandCoded.DSig.Releases.R1_0);
		}
	}
}