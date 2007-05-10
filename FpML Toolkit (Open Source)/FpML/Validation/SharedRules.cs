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

using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>SharedRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the defined validation <see cref="Rule"/> instances for shared
	/// components.
	/// </summary>
	public class SharedRules : Logic
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
		/// A <see cref="Rule"/> instance that ensures that business centers are
		/// only present if the date adjustment convention allows them.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule RULE01
			= new DelegatedRule ("shared-1", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that period multiplier is 'D' if the
		/// &lt;dayType&gt; element is present.
		/// </summary>
		/// <remarks>Applies to FpML 1.0, 2.0 and 3.0.</remarks>
		public static readonly Rule	RULE02
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "shared-2", new RuleDelegate (Rule02));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that period multiplier is not zero when
		/// the day type is 'Business'
		/// </summary>
		/// <remarks>Applies to FpML 1.0, 2.0 and 3.0.</remarks>
		public static readonly Rule	RULE03
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "shared-3", new RuleDelegate (Rule03));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that the businessDayConvention is
		/// NONE when the day type is Business.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE04
			= new DelegatedRule ("shared-4", new RuleDelegate (Rule04));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the payer and receivers are
		/// different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE05
			= new DelegatedRule ("shared-5", new RuleDelegate (Rule05));

		/// <summary>
		/// A <see cref="Rule"/> that ensures latestExerciseTime is after the
		/// earliestExerciseTime for American exercises.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE06
			= new DelegatedRule (Preconditions.TR3_0__LATER, "shared-6", new RuleDelegate (Rule06));

		/// <summary>
		/// A <see cref="Rule"/> that ensures latestExerciseTime is after the
		/// earliestExerciseTime for American exercises.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE07
			= new DelegatedRule ("shared-7", new RuleDelegate (Rule07));

		/// <summary>
		/// A <see cref="Rule"/> that ensures unadjustedFirstDate is before
		/// unadjustedLastDate.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE08
			= new DelegatedRule ("shared-8", new RuleDelegate (Rule08));

		/// <summary>
		/// A <see cref="Rule"/> that ensures business centers are not defined
		/// or referenced if the businessDayConvention is NONE or NotApplicable.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE09
			= new DelegatedRule ("shared-9", new RuleDelegate (Rule09));

		/// <summary>
		/// A <see cref="Rule"/> that ensures calculationAgentPartyReference/@href
		/// attributes are unique.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE10
			= new DelegatedRule ("shared-10", new RuleDelegate (Rule10));

		/// <summary>
		/// A <see cref="Rule"/> that ensures businessDateRange references to
		/// business centers are within the same trade.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE11
			= new DelegatedRule ("shared-11", new RuleDelegate (Rule11));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the referential integrity of
		/// buyerPartyReference/@href instances.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE12
			= new DelegatedRule ("shared-12", new RuleDelegate (Rule12));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the referential integrity of
		/// sellerPartyReference/@href instances.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE13
			= new DelegatedRule ("shared-13", new RuleDelegate (Rule13));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the referential integrity of
		/// calculationAgentPartyReference/@href instances.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE14
			= new DelegatedRule ("shared-14", new RuleDelegate (Rule14));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that period multiplier is 'D' if the
		/// &lt;dayType&gt; element is present.
		/// </summary>
		/// <remarks>Applies to all FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE15
			= new DelegatedRule (Preconditions.R4_0__LATER, "shared-15", new RuleDelegate (Rule15));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the reference integrity of trade side
		/// party references.
		/// </summary>
		/// <remarks>Applies to all FpML 4.2 and later.</remarks>
		public static readonly Rule	RULE16
			= new DelegatedRule (Preconditions.TR4_2__LATER, "shared-16", new RuleDelegate (Rule16));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the reference integrity of trade side
		/// account references.
		/// </summary>
		/// <remarks>Applies to all FpML 4.2 and later.</remarks>
		public static readonly Rule	RULE17
			= new DelegatedRule (Preconditions.TR4_2__LATER, "shared-17", new RuleDelegate (Rule17));

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = new RuleSet ();

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private SharedRules ()
		{ }

		//---------------------------------------------------------------------

		private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				Rule01 (name, nodeIndex.GetElementsByName ("dateAdjustments"), errorHandler)
			  & Rule01 (name, nodeIndex.GetElementsByName ("calculationPeriodDatesAdjustments"), errorHandler)
			  & Rule01 (name, nodeIndex.GetElementsByName ("paymentDatesAdjustments"), errorHandler)
		      & Rule01 (name, nodeIndex.GetElementsByName ("resetDatesAdjustments"), errorHandler));
		}
			
		private static bool Rule01 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Iff (
						Or (
							Exists (context ["businessCenters"]),
							Exists (context ["businessCentersReference"])),
						And (
							NotEqual (context ["businessDayConvention"], "NONE"),
							NotEqual (context ["businessDayConvention"], "NotApplicable"))))
					continue;

				errorHandler ("305", context,
					"Date adjustment contained business centers even though its business " +
					"day convention was set to " + context ["businessDayConvention"].InnerText,
					name, context ["businessDayConvention"].InnerText);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------
		
		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				Rule02 (name, nodeIndex.GetElementsByName ("cashSettlementValuationDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("feePaymentDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("fixingDateOffset"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("fixingDates"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("initialFixingDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("paymentDaysOffset"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("rateCutOffDaysOffset"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("relativeDate"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("varyingNotionalInterimExchangePaymentDates"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("varyingNotionalFixingDates"), errorHandler));
		}

		private static bool Rule02 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Implies (
						Exists (context ["dayType"]),
						Equal (context ["period"], "D")))
					continue;

				errorHandler ("305", context,
					"Offset contains a day type but the period is '" +
					context ["period"].InnerText + "', not 'D'",
					name, context ["period"].InnerText);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule03 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule03 (name, nodeIndex.GetElementsByName ("cashSettlementValuationDate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("feePaymentDate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("fixingDateOffset"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("fixingDates"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("initialFixingDate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("paymentDaysOffset"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("rateCutOffDaysOffset"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("relativeDate"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("varyingNotionalInterimExchangePaymentDates"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("varyingNotionalFixingDates"), errorHandler));
		}

		private static bool Rule03 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Implies (
						Equal (context ["dayType"], "Business"),
						NotEqual (context ["periodMultiplier"], 0)))
					continue;

				errorHandler ("305", context,
					"Offset has day type set to 'Business' but the period " +
					"multiplier is set to zero.",
					name, context ["periodMultiplier"].InnerText);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule04 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				Rule04 (name, nodeIndex.GetElementsByName ("cashSettlementValuationDate"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("feePaymentDate"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("fixingDateOffset"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("fixingDates"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("initialFixingDate"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("relativeDate"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("varyingNotionalFixingDates"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("varyingNotionalInterimExchangePaymentDates"), errorHandler));
		}

		private static bool Rule04 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Implies (
						Equal (context ["dayType"], "Business"),
						Equal (context ["businessDayConvention"], "NONE")))
					continue;

				errorHandler ("305", context,
					"Relative date offset has day type set to 'Business' but " +
					"the business day convention is not 'NONE'",
					name, context ["businessDayConvention"].InnerText);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule05 (name, nodeIndex.GetElementsByName ("payerPartyReference"), errorHandler));
		}

		private static bool Rule05 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (NotEqual (context.GetAttribute ("href"), context.ParentNode ["receiverPartyReference"].GetAttribute ("href")))
					continue;

				errorHandler ("305", context.ParentNode,
					"Payer-receiver party references are equal: " +
					context.GetAttribute ("href"),
					name, context.GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule06 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule06 (name, nodeIndex.GetElementsByName ("americanExercise"), errorHandler));
		}

		private static bool Rule06 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Less (context ["earliestExerciseTime"]["hourMinuteTime"].InnerText,
							context ["latestExerciseTime"]["hourMinuteTime"].InnerText))
					continue;

				errorHandler ("305", context,
					"American exercise earliest exercise time " +
					context ["earliestExerciseTime"]["hourMinuteTime"].InnerText +
					" must be before latest exercise time " +
					context ["latestExerciseTime"]["hourMinuteTime"].InnerText,
					name, null);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule07 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule07 (name, nodeIndex.GetElementsByName ("bermudaExercise"), errorHandler));
		}

		private static bool Rule07 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	earliest = XPath.Path (context, "earliestExerciseTime", "hourMinuteTime");
				XmlElement	latest   = XPath.Path (context, "latestExerciseTime", "hourMinuteTime");

				if ((earliest == null) || (latest == null) || Less (earliest, latest))
					continue;

				errorHandler ("305", context,
					"Bermuda exercise earliest exercise time " + earliest.InnerText +
					" must be before latest exercise time " + latest.InnerText,
					name, null);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule08 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				Rule08 (name, nodeIndex.GetElementsByName ("businessDateRange"), errorHandler)
				& Rule08 (name, nodeIndex.GetElementsByName ("cashSettlementPaymentDate"), errorHandler)
			    & Rule08 (name, nodeIndex.GetElementsByName ("scheduleBounds"), errorHandler));
		}

		private static bool Rule08 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if ((context ["unadjustedFirstDate"] == null) ||
					(context ["unadjustedLastDate"] == null) ||
					Less (context ["unadjustedFirstDate"].InnerText,
							context ["unadjustedLastDate"].InnerText))
					continue;

				errorHandler ("305", context,
					"Date range's unadjusted first date " +
					context ["unadjustedFirstDate"].InnerText +
					" must be before unadjusted last date " +
					context ["unadjustedLastDate"].InnerText,
					name, null);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule09 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("cashSettlementPaymentDate"))
				result &= Rule09 (name, context.GetElementsByTagName ("businessDateRange"), errorHandler);

			return (result);
		}

		private static bool Rule09 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Iff (
						Or (
							Exists (context ["businessCenters"]),
							Exists (context ["businessCentersReference"])),
						And (
							NotEqual (context ["businessDayConvention"], "NONE"),
							NotEqual (context ["businessDayConvention"], "NotApplicable"))))
					continue;

				errorHandler ("305", context,
					"Business date range business day convention is '" +
					context ["businessDayConvention"].InnerText +
					"' but business centers have been included.",
					name, context ["businessDayConvention"].InnerText);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule10 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule10 (name, nodeIndex.GetElementsByName ("calculationAgent"), errorHandler));
		}

		private static bool Rule10 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlNodeList agents = context.GetElementsByTagName ("calculationAgentPartyReference");

				for (int i = 0; i < (agents.Count - 1); ++i) {
					for (int j = i + 1; j < agents.Count; ++j) {
						if (Equal ((agents [i] as XmlElement).GetAttribute ("href"),
									(agents [j] as XmlElement).GetAttribute ("href"))) {
							errorHandler ("305", context,
								"Duplicate calculation agent reference: " +
								(agents [i] as XmlElement).GetAttribute ("href"),
								name, (agents [i] as XmlElement).GetAttribute ("href"));

							result = false;
						}
					}
				}
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule11 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule11 (name, nodeIndex.GetElementsByName ("businessDateRange"), errorHandler));
		}

		private static bool Rule11 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	referree = context ["businessCentersReference"];

				if (referree != null) {
					XmlElement	referred = referree.OwnerDocument.GetElementById (referree.GetAttribute ("href"));

					XmlNode common = XPath.CommonAncestor (referree, referred);
					if ((common != null) && common.Name.Equals ("trade"))
						continue;
			
					errorHandler ("305", context,
						"Business centers reference " +	context.GetAttribute ("href") +
						" in date range does not reference business centers in the same trade.",
						name, context.GetAttribute ("href"));

					result = false;
				}
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule12 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule12 (name, nodeIndex.GetElementsByName ("buyerPartyReference"), errorHandler, nodeIndex));
		}

		private static bool Rule12 (string name, XmlNodeList list, ValidationErrorHandler errorHandler, NodeIndex nodeIndex)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				string		href	 = context.GetAttribute ("href");

				if (href.StartsWith ("#")) href = href.Substring (1);

				XmlElement	referred = nodeIndex.GetElementById (href);

				if ((referred != null) && (referred.LocalName.Equals ("party") || referred.LocalName.Equals ("tradeSide"))) continue;

				errorHandler ("305", context,
					"Buyer party reference '" + context.GetAttribute ("href") +
					"' does not match a party defined in the document.",
					name, context.GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule13 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule13 (name, nodeIndex.GetElementsByName ("sellerPartyReference"), errorHandler, nodeIndex));
		}

		private static bool Rule13 (string name, XmlNodeList list, ValidationErrorHandler errorHandler, NodeIndex nodeIndex)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				string		href	 = context.GetAttribute ("href");

				if (href.StartsWith ("#")) href = href.Substring (1);

				XmlElement	referred = nodeIndex.GetElementById (href);

				if ((referred != null) && (referred.LocalName.Equals ("party") || referred.LocalName.Equals ("tradeSide"))) continue;

				errorHandler ("305", context,
					"Seller party reference '" + context.GetAttribute ("href") +
					"' does not match a party defined in the document.",
					name, context.GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule14 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule14 (name, nodeIndex.GetElementsByName ("calculationAgentPartyReference"), errorHandler, nodeIndex));
		}

		private static bool Rule14 (string name, XmlNodeList list, ValidationErrorHandler errorHandler, NodeIndex nodeIndex)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				string		href	 = context.GetAttribute ("href");

				if (href.StartsWith ("#")) href = href.Substring (1);

				XmlElement	referred = nodeIndex.GetElementById (href);

				if ((referred != null) && referred.LocalName.Equals ("party")) continue;

				errorHandler ("305", context,
					"Calculation agent party reference '" + context.GetAttribute ("href") +
					"' does not match a party defined in the document.",
					name, context.GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule15 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				Rule15 (name, nodeIndex.GetElementsByName ("gracePeriod"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("paymentDaysOffset"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("rateCutOffDaysOffset"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("relativeDate"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("fixingDateOffset"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("initialFixingDate"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("fixingDates"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("cashSettlementValuationDate"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("varyingNotionalInterimExchangePaymentDates"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("varyingNotionalFixingDates"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("feePaymentDate"), errorHandler)
				& Rule15 (name, nodeIndex.GetElementsByName ("relativeDates"), errorHandler));
		}

		private static bool Rule15 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Iff (
						Exists (context ["dayType"]),
						And (					
					        Equal (context ["period"], "D"),
							NotEqual (context ["periodMultiplier"], 0))))
						continue;

				errorHandler ("305", context,
					"daytype must only be present if and only if the period " +
					"is 'D' and the periodMultiplier is non-zero",
					name, null);

				result = false;
			}
			return (result);
		}

		//---------------------------------------------------------------------

		private static bool Rule16 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in XPath.Paths (nodeIndex.GetElementsByName ("tradeSide"), "*", "party")) {
				string		href	= context.GetAttribute ("href");
				XmlElement	target	= nodeIndex.GetElementById (href);

				if (target.LocalName.Equals ("party")) continue;

				errorHandler ("305", context,
					"The value of the href attribute does not refer to a party structure",
					name, href);

				result = false;
			}
			return (result);
		}
		
		//---------------------------------------------------------------------

		private static bool Rule17 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in XPath.Paths (nodeIndex.GetElementsByName ("tradeSide"), "*", "account")) {
				string		href	= context.GetAttribute ("href");
				XmlElement	target  = nodeIndex.GetElementById (href);

				if (target.LocalName.Equals ("account")) continue;

				errorHandler ("305", context,
					"The value of the href attribute does not refer to an account structure",
					name, href);

				result = false;
			}
			return (result);
		}

		/// <summary>
		/// Initialises the <see cref="RuleSet"/> with copies of all the FpML
		/// defined <see cref="Rule"/> instances for shared components.
		/// </summary>
		static SharedRules ()
		{
			rules.Add (RULE01);
			rules.Add (RULE02);
			rules.Add (RULE03);
			rules.Add (RULE04);
			rules.Add (RULE05);
			rules.Add (RULE06);
			rules.Add (RULE07);
			rules.Add (RULE08);
			rules.Add (RULE09);
			rules.Add (RULE10);
			rules.Add (RULE11);
			rules.Add (RULE12);
			rules.Add (RULE13);
			rules.Add (RULE14);
			rules.Add (RULE15);
			rules.Add (RULE16);
			rules.Add (RULE17);
		}
	}
}