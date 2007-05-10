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

using HandCoded.Finance;
using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>DataTypeRules</b> class contains HandCoded rule extensions for
	/// validating datatypes in DTD based documents.
	/// </summary>
	public class DataTypeRules
	{
		/// <summary>
		/// Contains the <see cref="RuleSet"/> of data type checking rules.
		/// </summary>
		public static RuleSet Rules {
			get {
				return (rules);
			}
		}

		/// <summary>
		/// A <see cref="Rule"/> instance for validating hourMinute time values.
		/// </summary>
		public static readonly Rule	RULE01
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "datatype-1", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> instance for validating date values.
		/// </summary>
		public static readonly Rule	RULE02
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "datatype-2", new RuleDelegate (Rule02));

		/// <summary>
		/// A <see cref="Rule"/> instance for validating decimal values.
		/// </summary>
		public static readonly Rule	RULE03
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "datatype-3", new RuleDelegate (Rule03));

		/// <summary>
		/// A <see cref="Rule"/> instance for validating integer time values.
		/// </summary>
		public static readonly Rule	RULE04
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "datatype-4", new RuleDelegate (Rule04));
	
		/// <summary>
		/// A <see cref="Rule"/> instance for validating positive integer values.
		/// </summary>
		public static readonly Rule	RULE05
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "datatype-5", new RuleDelegate (Rule05));
	
		/// <summary>
		/// A <see cref="Rule"/> instance for validating non-negative integer values.
		/// </summary>
		public static readonly Rule	RULE06
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "datatype-6", new RuleDelegate (Rule06));
	
		/// <summary>
		/// A <see cref="Rule"/> instance for validating boolean values.
		/// </summary>
		public static readonly Rule	RULE07
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "datatype-7", new RuleDelegate (Rule07));

		/// <summary>
		/// The underlying <see cref="RuleSet"/> of <see cref="Rule"/> instances.
		/// </summary>
		private static readonly RuleSet	rules = new RuleSet ();

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private DataTypeRules ()
		{ }

		// --------------------------------------------------------------------

		private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("hourMinuteTime")) {
				string		value = context.InnerText;

				try {
					if (Time.Parse (value).Seconds != 0) {
						errorHandler ("305", context,
							"The seconds component of the time must be zeroes",
							name, context.InnerText);
						result = false;
					}
				}
				catch (Exception) {
					errorHandler ("305", context,
						"The time value is not in HH:MM:SS format",
						name, value);
					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------------

		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule02 (name, nodeIndex.GetElementsByName ("adjustedEarlyTerminationDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedEffectiveDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedEndDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedExerciseDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedExerciseFeePaymentDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedExtendedTerminationDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedFixingDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedFxSpotFixingDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedPrincipalExchangeDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedRelevantSwapEffectiveDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedStartDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedTerminationDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("adjustedPaymentDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("currency1ValueDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("currency2ValueDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("expiryDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("firstNotionalStepDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("firstPaymentDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("firstRegularPeriodStartDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("fixingDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("lastNotionalStepDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("lastRegularPaymentDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("lastRegularPeriodEndDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("masterAgreementDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("observationDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("observationEndDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("observationStartDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("resetDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("stepDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("premiumSettlementDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("tradeDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("unadjustedDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("unadjustedEndDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("unadjustedFirstDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("unadjustedLastDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("unadjustedPaymentDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("unadjustedPrincipalExchangeDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("unadjustedStartDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("valuationDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("valueDate"), errorHandler));
		}

		private static bool Rule02 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				string		value = context.InnerText;

				try {
					Date.Parse (value);
				}
				catch (Exception) {
					errorHandler ("305", context,
						"The date value is not in YYYY-MM-DD format",
						name, value);
					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------------

		private static bool Rule03 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule03 (name, nodeIndex.GetElementsByName ("amount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("averageRateWeightingFactor"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("calculatedRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("discountRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("feeAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("feeRate"), errorHandler)	
				& Rule03 (name, nodeIndex.GetElementsByName ("fixedPaymentAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("fixedRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("floatingRateMultiplier"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("forwardPoints"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("initialRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("initialValue"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("integralMultipleAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("integralMultipleExercise"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("maximumNotionalAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("maximumNumberOfOptions"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("minimumNotionalAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("minimumNumberOfOptions"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("notionalAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("notionalStepAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("notionalStepRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("numberOfOptions"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("observedFxSpotRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("observedRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("optionEntitlement"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("percentageOfNotional"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("premiumValue"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("pricePerOption"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("principalExchangeAmount"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("rate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("spotPrice"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("spotRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("spread"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("stepValue"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("strikePrice"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("strikeRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("stubRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("thresholdRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("treatedRate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("triggerRate"), errorHandler));
		}

		private static bool Rule03 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				string		value = context.InnerText;

				try {
					Decimal.Parse (value);
				}
				catch (Exception) {
					errorHandler ("305", context, "Invalid decimal value", name, value);
					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------------

		private static bool Rule04 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule04 (name, nodeIndex.GetElementsByName ("intermediarySequenceNumber"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("periodMultiplier"), errorHandler));
		}

		private static bool Rule04 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				string		value = context.InnerText;

				try {
					Int32.Parse (value);
				}
				catch (Exception) {
					errorHandler ("305", context, "Invalid integer value", name, value);
					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------------

		private static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule05 (name, nodeIndex.GetElementsByName ("calculationPeriodNumberOfDays"), errorHandler)
				& Rule05 (name, nodeIndex.GetElementsByName ("observationWeight"), errorHandler)
				& Rule05 (name, nodeIndex.GetElementsByName ("periodSkip"), errorHandler));
		}

		private static bool Rule05 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				string		value = context.InnerText;

				try {
					if (Int32.Parse (value) <= 0)
						throw new ArgumentException ("Invalid value");
				}
				catch (Exception) {
					errorHandler ("305", context, "Invalid positive integer value", name, value);
					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------------

		private static bool Rule06 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("precision")) {
				string		value = context.InnerText;

				try {
					if (Int32.Parse (value) < 0)
						throw new ArgumentException ("Invalid value");
				}
				catch (Exception) {
					errorHandler ("305", context, "Invalid non-negative integer value", name, value);
					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------------

		private static bool Rule07 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule07 (name, nodeIndex.GetElementsByName ("automaticExerciseApplicable"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("cashflowsMatchParameters"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("failureToDeliverApplicable"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("fallbackExercise"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("finalExchange"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("followUpConfirmation"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("fraDiscounting"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("initialExchange"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("intermediateExchange"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("swapPremium"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("swaptionStraddle"), errorHandler));
		}

		private static bool Rule07 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				string		value = context.InnerText;

				if (!(value.Equals ("true") || value.Equals ("false"))) {
					errorHandler ("305", context, "Invalid boolean value", name, value);
					result = false;
				}
			}
			return (result);
		}

		/// <summary>
		/// Initialises the <see cref="RuleSet"/> contents.
		/// </summary>
		static DataTypeRules ()
		{
			rules.Add (RULE01);
			rules.Add (RULE02);
			rules.Add (RULE03);
			rules.Add (RULE04);
			rules.Add (RULE05);
			rules.Add (RULE06);
			rules.Add (RULE07);
		}
	}
}