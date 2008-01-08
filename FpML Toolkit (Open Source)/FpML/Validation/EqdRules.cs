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
	/// The <b>EqdRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the defined validation <see cref="Rule"/> instances for Equity
	/// Derivative Products.
	/// </summary>
	public class EqdRules : Logic
	{
		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = new RuleSet ();

		/// <summary>
		/// Contains the <see cref="RuleSet"/>.
		/// </summary>
		public static RuleSet Rules {
			get {
				return (rules);
			}
		}

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the unadjusted commencement
		/// date is the same as the trade date for american options.
		/// </summary>
		public static readonly Rule	RULE01
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-1", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the unadjusted expiration
		/// date is after the trade date for american options.
		/// </summary>
		public static readonly Rule	RULE02
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-2", new RuleDelegate (Rule02));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the latest exercise time is
		/// after the earliest exercise time for american options.
		/// </summary>
		public static readonly Rule	RULE03
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-3", new RuleDelegate (Rule03));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the unadjusted commencement
		/// date is the same as the trade date for bermudan options.
		/// </summary>
		public static readonly Rule	RULE04
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-4", new RuleDelegate (Rule04));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the unadjusted expiration
		/// date is after the trade date for bermudan options.
		/// </summary>
		public static readonly Rule	RULE05
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-5", new RuleDelegate (Rule05));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the latest exercise time is
		/// after the earliest exercise time for bermudan options.
		/// </summary>
		public static readonly Rule	RULE06
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-6", new RuleDelegate (Rule06));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures bermudan exercise dates are
		/// in order.
		/// </summary>
		/// <remarks>Deprecated.</remarks>
		public static readonly Rule	RULE07
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-7", new RuleDelegate (Rule07));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures bermudan exercise dates are
		/// after commencement.
		/// </summary>
		public static readonly Rule	RULE08
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-8", new RuleDelegate (Rule08));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures bermudan exercise dates are
		/// before expiry.
		/// </summary>
		public static readonly Rule	RULE09
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-9", new RuleDelegate (Rule09));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensurse bermudan exercise dates are
		/// unique.
		/// </summary>
		public static readonly Rule	RULE10
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-10", new RuleDelegate (Rule10));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the unadjusted expiration
		/// date is after the trade date for bermudan options.
		/// </summary>
		public static readonly Rule	RULE12
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-12", new RuleDelegate (Rule12));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures equity option premium payment
		/// date is on or after the trade date.
		/// </summary>
		public static readonly Rule	RULE13
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-13", new RuleDelegate (Rule13));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures broker equity option premium
		/// payment date is on or after the trade date.
		/// </summary>
		public static readonly Rule	RULE14
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-14", new RuleDelegate (Rule14));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures European option valuation
		/// date is the same as the expiration date.
		/// </summary>
		public static readonly Rule	RULE15
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-15", new RuleDelegate (Rule15));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the minimum number of options
		/// is less than the maximum.
		/// </summary>
		public static readonly Rule	RULE16
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-16", new RuleDelegate (Rule16));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the number of options in
		/// a multiple exercise American option is correct.
		/// </summary>
		public static readonly Rule	RULE17
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-17", new RuleDelegate (Rule17));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the number of options in
		/// a multiple exercise Bermudan option is correct.
		/// </summary>
		public static readonly Rule	RULE18
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-18", new RuleDelegate (Rule18));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures premium is the correct
		/// percentage of notional.
		/// </summary>
		public static readonly Rule	RULE19
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-19", new RuleDelegate (Rule19));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the payment amount is correct.
		/// </summary>
		public static readonly Rule	RULE20
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-20", new RuleDelegate (Rule20));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the calculationAgentPartyReference
		/// is present.
		/// </summary>
		public static readonly Rule	RULE21
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-21", new RuleDelegate (Rule21));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the BuyerPartyReference is
		/// different from the SellerPartyReference.
		/// </summary>
		public static readonly Rule	RULE22
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-22", new RuleDelegate (Rule22));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the effective date is the
		/// same or later than the trade date.
		/// </summary>
		public static readonly Rule	RULE23
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-23", new RuleDelegate (Rule23));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the end date is the same or after
		/// the start date.
		/// </summary>
		public static readonly Rule	RULE24
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-24", new RuleDelegate (Rule24));

		/// <summary>
		/// A <see cref="Rule"/> instance that ensures the payment amount is calculated
		/// correctly.
		/// </summary>
		public static readonly Rule	RULE25
			= new DelegatedRule (Preconditions.R4_0__LATER, "eqd-25", new RuleDelegate (Rule25));

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private EqdRules ()
		{ }

		private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule01 (name, nodeIndex.GetElementsByName ("equityAmericanExercise"), errorHandler));
		}

		private static bool Rule01 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	commence	= XPath.Path (context, "commencementDate", "adjustableDate", "unadjustedDate");
				XmlElement	trade		= XPath.Path (context, "..", "..", "..", "tradeHeader", "tradeDate");

				if ((commence == null) || (trade == null) || Equal (ToDate (commence), ToDate (trade)))
					continue;

				errorHandler ("305", context,
					"American exercise commencement date " + ToString (commence) +
					" should be the same as trade date " + ToString (trade),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule02 (name, nodeIndex.GetElementsByName ("equityAmericanExercise"), errorHandler));
		}

		private static bool Rule02 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	expiration	= XPath.Path (context, "expirationDate", "adjustableDate", "unadjustedDate");
				XmlElement	trade		= XPath.Path (context, "..", "..", "..", "tradeHeader", "tradeDate");

				if ((expiration == null) || (trade == null) || GreaterOrEqual (Types.ToDate (expiration), Types.ToDate (trade)))
					continue;

				errorHandler ("305", context,
					"American exercise expiration date " + ToString (expiration) +
					" should be the same or later than trade date " + ToString (trade),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule03 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule03 (name, nodeIndex.GetElementsByName ("equityAmericanExercise"), errorHandler));
		}

		private static bool Rule03 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (Implies (
						Equal (XPath.Path (context, "latestExerciseTimeType"), "SpecificTime"),
						Exists (XPath.Path (context, "latestExerciseTime"))))
					continue;

				errorHandler ("305", context,
					"American exercise structure should include a latest " +
					"exercise time, since time type is set to SpecificTime",
					name, null);
			
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule04 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule04 (name, nodeIndex.GetElementsByName ("equityBermudaExercise"), errorHandler));
		}

		private static bool Rule04 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	commence	= XPath.Path (context, "commencementDate", "adjustableDate", "unadjustedDate");
				XmlElement	trade		= XPath.Path (context, "..", "..", "..", "tradeHeader", "tradeDate");

				if ((commence == null) || (trade == null) || GreaterOrEqual (Types.ToDate (commence), Types.ToDate (trade)))
					continue;

				errorHandler ("305", context,
					"Bermuda exercise commencement date " + ToString (commence) +
					" should not be before the trade date " + ToString (trade),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule05 (name, nodeIndex.GetElementsByName ("equityBermudaExercise"), errorHandler));
		}

		private static bool Rule05 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	expiration	= XPath.Path (context, "expirationDate", "adjustableDate", "unadjustedDate");
				XmlElement	trade		= XPath.Path (context, "..", "..", "..", "tradeHeader", "tradeDate");

				if ((expiration == null) || (trade == null) || GreaterOrEqual (Types.ToDate (expiration), Types.ToDate (trade)))
					continue;

				errorHandler ("305", context,
					"Bermuda exercise expiration date " + ToString (expiration) +
					" should not be before trade date " + ToString (trade),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule06 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule06 (name, nodeIndex.GetElementsByName ("equityBermudaExercise"), errorHandler));
		}

		private static bool Rule06 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (Implies (
						Equal (XPath.Path (context, "latestExerciseTimeType"), "SpecificTime"),
						Exists (XPath.Path (context, "latestExerciseTime"))))
					continue;

				errorHandler ("305", context,
					"Bermuda exercise structure should include a latest " +
					"exercise time, since time type is set to SpecificTime",
					name, null);
			
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule07 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule07 (name, nodeIndex.GetElementsByName ("equityBermudaExercise"), errorHandler));
		}

		private static bool Rule07 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in XPath.Paths (list, "bermudaExerciseDates", "date")) {
				XmlElement		next	= DOM.GetNextSibling (context);

				if ((next == null) || Less (Types.ToDate (context), Types.ToDate (next)))
					continue;

				errorHandler ("305", context,
					"Bermuda exercise dates " + ToString (context) + " and " +
					ToString (next) + " are not in order",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule08 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule08 (name, nodeIndex.GetElementsByName ("equityBermudaExercise"), errorHandler));
		}

		private static bool Rule08 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in XPath.Paths (list, "bermudaExerciseDates", "date")) {
				XmlElement	commence	= XPath.Path (context, "..", "..", "commencementDate", "adjustableDate", "unadjustedDate");

				if ((commence == null) || Greater (Types.ToDate (context), Types.ToDate (commence)))
					continue;

				errorHandler ("305", context,
					"Bermuda exercise date " + ToString (context) +
					" should be after exercise period commencement date " +
					ToString (commence),
					name, null);
								
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule09 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule09 (name, nodeIndex.GetElementsByName ("equityBermudaExercise"), errorHandler));
		}

		private static bool Rule09 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in XPath.Paths (list, "bermudaExerciseDates", "date")) {
				XmlElement	expiration	= XPath.Path (context, "..", "..", "expirationDate", "adjustableDate", "unadjustedDate");

				if ((expiration == null) || LessOrEqual (ToDate (context), ToDate (expiration)))
					continue;

				errorHandler ("305", context,
					"Bermuda exercise date " + ToString (context) +
					" should be on or before exercise period expiration date " +
					ToString (expiration),
					name, null);
								
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule10 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule10 (name, nodeIndex.GetElementsByName ("equityBermudaExercise"), errorHandler));
		}

		private static bool Rule10 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in XPath.Paths (list, "bermudaExerciseDates", "date")) {
				XmlElement	other	= DOM.GetNextSibling (context);

				for (; other != null; other = DOM.GetNextSibling (other)) {
					if (NotEqual (ToDate (context), ToDate (other))) continue;

					errorHandler ("305", context,
						"Duplicate bermuda exercise date, " + ToString (other),
						name, ToString (other));

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule12 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule12 (name, nodeIndex.GetElementsByName ("equityEuropeanExercise"), errorHandler));
		}

		private static bool Rule12 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	expiration	= XPath.Path (context, "expirationDate", "adjustableDate", "unadjustedDate");
				XmlElement	trade		= XPath.Path (context, "..", "..", "..", "tradeHeader", "tradeDate");

				if ((expiration == null) || (trade == null) || GreaterOrEqual (ToDate (expiration), ToDate (trade)))
					continue;

				errorHandler ("305", context,
					"European exercise expiration date " + ToString (expiration) +
					" should not be before the trade date " + ToString (trade),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule13 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule13 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}

		private static bool Rule13 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	premiumDate	= XPath.Path (context, "equityOption", "equityPremium", "paymentDate", "unadjustedDate");
				XmlElement	tradeDate	= XPath.Path (context, "tradeHeader", "tradeDate");

				if ((premiumDate == null) || (tradeDate == null) || GreaterOrEqual (ToDate (premiumDate), ToDate (tradeDate)))
					continue;

				errorHandler ("305", context,
					"Equity premium payment date " + ToString (premiumDate) +
					" must be on or after trade date " + ToString (tradeDate),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule14 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule14 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}

		private static bool Rule14 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	premiumDate	= XPath.Path (context, "brokerEquityOption", "equityPremium", "paymentDate", "unadjustedDate");
				XmlElement	tradeDate	= XPath.Path (context, "tradeHeader", "tradeDate");

				if ((premiumDate == null) || (tradeDate == null) || GreaterOrEqual (ToDate (premiumDate), ToDate (tradeDate)))
					continue;

				errorHandler ("305", context,
					"Broker equity premium payment date " + ToString (premiumDate) +
					" must be on or after trade date " + ToString (tradeDate),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule15 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule15 (name, nodeIndex.GetElementsByName ("equityExercise"), errorHandler));
		}

		private static bool Rule15 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	valuationDate	= XPath.Path (context, "equityValuation", "valuationDate", "adjustableDate", "unadjustedDate");
				XmlElement	expirationDate	= XPath.Path (context, "equityEuropeanExercise", "expirationDate", "adjustableDate", "unadjustedDate");

				if ((valuationDate == null) || (expirationDate == null) || Equal (ToDate (valuationDate), ToDate (expirationDate)))
					continue;

				errorHandler ("305", context,
					"The valuation and expiration dates for a European option must be same",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule16 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule16 (name, nodeIndex.GetElementsByName ("equityMultipleExercise"), errorHandler));
		}

		private static bool Rule16 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	minimum = XPath.Path (context, "minimumNumberOfOptions");
				XmlElement	maximum = XPath.Path (context, "maximumNumberOfOptions");

				if ((minimum == null) || (maximum == null) || Less (ToDecimal (minimum), ToDecimal (maximum)))
					continue;

				errorHandler ("305", context,
					"Minimum number of options must be less than the maximum number",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule17 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule17 (name, nodeIndex.GetElementsByName ("equityOption"), errorHandler)
				& Rule17 (name, nodeIndex.GetElementsByName ("brokerEquityOption"), errorHandler));
		}

		private static bool Rule17 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	multiple	= XPath.Path (context, "equityExercise", "equityAmericanExercise", "equityMultipleExercise");
				XmlElement	number		= XPath.Path (context, "numberOfOptions");

				if ((multiple == null) || (number == null)) continue;

				XmlElement	integral	= XPath.Path (multiple, "integralMultipleExercise");
				XmlElement	maximum		= XPath.Path (multiple, "maximumNumberOfOptions");

				if ((integral == null) || (maximum == null) || GreaterOrEqual (ToDecimal (integral) * ToDecimal (maximum), ToDecimal (number)))
					continue;

				errorHandler ("305", context,
					"maximumNumberOfOptions * integralMultipleExercise should " +
					"not be greater than numberOfOptions",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule18 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule18 (name, nodeIndex.GetElementsByName ("equityOption"), errorHandler)
				& Rule18 (name, nodeIndex.GetElementsByName ("brokerEquityOption"), errorHandler));
		}

		private static bool Rule18 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	multiple	= XPath.Path (context, "equityExercise", "equityBermudaExercise", "equityMultipleExercise");
				XmlElement	number		= XPath.Path (context, "numberOfOptions");

				if ((multiple == null) || (number == null)) continue;

				XmlElement	integral	= XPath.Path (multiple, "integralMultipleExercise");
				XmlElement	maximum		= XPath.Path (multiple, "maximumNumberOfOptions");

				if ((integral == null) || (maximum == null) || LessOrEqual (ToDecimal (integral) * ToDecimal (maximum), ToDecimal (number)))
					continue;

				errorHandler ("305", context,
					"maximumNumberOfOptions * integralMultipleExercise should " +
					"not be greater than numberOfOptions",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule19 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule19 (name, nodeIndex.GetElementsByName ("equityOption"), errorHandler)
				& Rule19 (name, nodeIndex.GetElementsByName ("brokerEquityOption"), errorHandler));
		}

		private static bool Rule19 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	notional	= XPath.Path (context, "notional");
				XmlElement	payment		= XPath.Path (context, "equityPremium", "paymentAmount");

				if (!IsSameCurrency (notional, payment)) continue;

				XmlElement	totalValue	= XPath.Path (notional, "amount");
				XmlElement	percentage	= XPath.Path (context, "equityPremium", "percentageOfNotional");
				XmlElement	amount		= XPath.Path (payment, "amount");

				if ((totalValue == null) || (percentage == null) || (amount == null) ||
					Equal (Round (ToDecimal (amount), 2), Round (ToDecimal (totalValue) * ToDecimal (percentage), 2)))
					continue;

				errorHandler ("305", context,
					"The equity premium amount does not agree with the figures given for " +
					"the notional and premium percentage",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule20 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule20 (name, nodeIndex.GetElementsByName ("equityOption"), errorHandler)
				& Rule20 (name, nodeIndex.GetElementsByName ("brokerEquityOption"), errorHandler));
		}

		private static bool Rule20 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	price		= XPath.Path (context, "equityPremium", "pricePerOption");
				XmlElement	payment		= XPath.Path (context, "equityPremium", "paymentAmount");

				if (!IsSameCurrency (price, payment)) continue;

				XmlElement	number		= XPath.Path (context, "numberOfOptions");
				XmlElement	entitlement	= XPath.Path (context, "optionEntitlement");
				XmlElement	priceEach	= XPath.Path (price, "amount");
				XmlElement	amount		= XPath.Path (payment, "amount");

				if ((number == null) || (entitlement == null) || (priceEach == null) || (amount == null) ||
					Equal (Round (ToDecimal (amount), 2), Round (ToDecimal (priceEach) * ToDecimal (number) * ToDecimal (entitlement), 2)))
					continue;

				errorHandler ("305", context,
					"The equity premium amount does not agree with the figures given for " +
					"the number of options, price per option and entitlement",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule21 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule21 (name, nodeIndex.GetElementsByName ("calculationAgent"), errorHandler));
		}

		private static bool Rule21 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (!context.ParentNode.LocalName.Equals ("trade")) continue;

				if (Exists (
						XPath.Path (context, "calculationAgentPartyReference")))
					continue;

				errorHandler ("305", context,
					"Calculation agent field must contain a calculation agent party reference",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule22 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule22 (name, nodeIndex.GetElementsByName ("equityOption"), errorHandler)
				& Rule22 (name, nodeIndex.GetElementsByName ("brokerEquityOption"), errorHandler)
				& Rule22 (name, nodeIndex.GetElementsByName ("equityForward"), errorHandler));
		}

		private static bool Rule22 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	buyer	= XPath.Path (context, "buyerPartyReference");
				XmlElement	seller	= XPath.Path (context, "sellerPartyReference");

				if ((buyer == null) || (seller == null) ||
					NotEqual (buyer.GetAttribute ("href"), seller.GetAttribute ("href")))
					continue;
		
				errorHandler ("305", context,
					"The buyerPartyReference/@href must not be the same as the " +
					"sellerPartyReference/@href",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule23 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule23 (name, nodeIndex.GetElementsByName ("equityOption"), errorHandler)
				& Rule23 (name, nodeIndex.GetElementsByName ("brokerEquityOption"), errorHandler)
				& Rule23 (name, nodeIndex.GetElementsByName ("equityForward"), errorHandler));
		}

		private static bool Rule23 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	effectiveDate	= XPath.Path (context, "equityEffectiveDate");
				XmlElement	tradeDate		= XPath.Path (context, "..", "tradeHeader", "tradeDate");

				if ((effectiveDate == null) || (tradeDate == null) ||
						GreaterOrEqual (Types.ToDate (effectiveDate), Types.ToDate (tradeDate)))
					continue;

				errorHandler ("305", context,
					"The equity effective date must be on or after the trade date",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule24 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule24 (name, nodeIndex.GetElementsByName ("schedule"), errorHandler));
		}

		private static bool Rule24 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	startDate	= XPath.Path (context, "startDate");
				XmlElement	endDate		= XPath.Path (context, "endDate");

				if ((startDate == null) || (endDate == null) ||
						LessOrEqual (Types.ToDate (startDate), Types.ToDate (endDate)))
					continue;

				errorHandler ("305", context,
					"The equity schedule start date can not be after the end date",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule25 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule25 (name, nodeIndex.GetElementsByName ("brokerEquityOption"), errorHandler));
		}

		private static bool Rule25 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	price		= XPath.Path (context, "equityPremium", "pricePerOption");
				XmlElement	payment		= XPath.Path (context, "equityPremium", "paymentAmount");

				if (!IsSameCurrency (price, payment)) continue;

				XmlElement	number		= XPath.Path (context, "numberOfOptions");
				XmlElement	priceEach	= XPath.Path (price, "amount");
				XmlElement	amount		= XPath.Path (payment, "amount");

				if ((number == null) || (priceEach == null) || (amount == null) ||
					Equal (Round (ToDecimal (amount), 2), Round (ToDecimal (priceEach) * ToDecimal (number), 2)))
					continue;

				errorHandler ("305", context,
					"The equity premium amount does not agree with the figures given for " +
					"the number of options and price per option",
					name, null);

				result = false;
			}
			return (result);
		}

		/// <summary>
		/// Determine if two <see cref="XmlElement"/> structures containing
		/// <b>Money</b> instances have the same currency code.
		/// </summary>
		/// <param name="moneyA">The <see cref="XmlElement"/> containing the first <b>Money</b>.</param>
		/// <param name="moneyB">The <see cref="XmlElement"/> containing the second <b>Money</b>.</param>
		/// <returns><b>true</b> if both <b>Money</b> structures have the same currency.</returns>
		private static bool IsSameCurrency (XmlElement moneyA, XmlElement moneyB)
		{
			return (Equal (XPath.Path (moneyA, "currency"),
						   XPath.Path (moneyB, "currency")));
		}

		/// <summary>
		/// Initialises the <see cref="RuleSet"/> with copies of all the FpML
		/// defined <see cref="Rule"/> instances for Interest Rate Derivatives.
		/// </summary>
		static EqdRules ()
		{
			Rules.Add (RULE01);
			Rules.Add (RULE02);
			Rules.Add (RULE03);
			Rules.Add (RULE04);
			Rules.Add (RULE05);
			Rules.Add (RULE06);
			Rules.Add (RULE08);
			Rules.Add (RULE09);
			Rules.Add (RULE10);
			Rules.Add (RULE12);
			Rules.Add (RULE13);
			Rules.Add (RULE14);
			Rules.Add (RULE15);
			Rules.Add (RULE16);
			Rules.Add (RULE17);
			Rules.Add (RULE18);
			Rules.Add (RULE19);
			Rules.Add (RULE20);
			Rules.Add (RULE21);
			Rules.Add (RULE22);
			Rules.Add (RULE23);
			Rules.Add (RULE24);
			Rules.Add (RULE25);
		}
	}
}