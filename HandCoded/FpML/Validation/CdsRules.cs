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
	/// The <b>CdsRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the defined validation <see cref="Rule"/> instances for Credit
	/// Derivative Products.
	/// </summary>
	public sealed class CdsRules : FpMLRuleSet
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
		/// A <see cref="Rule"/> that ensures tradeHeader/tradeDate is before
		/// creditDefaultSwap/generalTerms/effectiveDate/unadjustedDate.
		/// </summary>
		public static readonly Rule	RULE01
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-1", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> that ensures tradeHeader/tradeDate is not before
		/// creditDefaultSwap/generalTerms/effectiveDate/unadjustedDate.
		/// </summary>
		public static readonly Rule	RULE01B
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-1b", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if calculationAgent is present
		/// then it can only contain calculationAgentPartyReference elements.
		/// </summary>
		public static readonly Rule	RULE02
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-2", new RuleDelegate (Rule02));

		/// <summary>
		/// A <see cref="Rule"/> that ensures contracts referencing ISDA 1999 definitions
		/// do not use ISDA 2003 supplements.
		/// </summary>
		public static readonly Rule	RULE03
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-3", new RuleDelegate (Rule03));

		/// <summary>
		/// A <see cref="Rule"/> that ensures contracts referencing ISDA 2003 definitions
		/// do not use ISDA 1999 supplements.
		/// </summary>
		public static readonly Rule	RULE04
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-4", new RuleDelegate (Rule04));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if scheduledTerminationDate/adjustableDate
		/// exists the effectiveDate/unadjustedDate &lt; scheduledTerminationDate/adjustableDate.
		/// </summary>
		public static readonly Rule	RULE05
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-5", new RuleDelegate (Rule05));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyerPartyReference/@href and
		/// sellerPartyReference/@href are different.
		/// </summary>
		public static readonly Rule	RULE06
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-6", new RuleDelegate (Rule06));

		/// <summary>
		/// A <see cref="Rule"/> that ensures long form contracts contain effective
		/// date adjustments.
		/// </summary>
		public static readonly Rule	RULE07
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-7", new RuleDelegate (Rule07));

		/// <summary>
		/// A <see cref="Rule"/> that ensures long form contracts contain termination
		/// date adjustments.
		/// </summary>
		public static readonly Rule	RULE08
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-8", new RuleDelegate (Rule08));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if referenceObligation/primaryObligorReference
		/// exists then if must reference the referenceEntity.
		/// </summary>
		public static readonly Rule	RULE09
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-9", new RuleDelegate (Rule09));

		/// <summary>
		/// A <see cref="Rule"/> that ensure if referenceObligation/guarantorReference
		/// exists then if must reference the referenceEntity.
		/// </summary>
		public static readonly Rule	RULE10
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-10", new RuleDelegate (Rule10));

		/// <summary>
		/// A <see cref="Rule"/> that ensures ISDA 2003 long form contracts contain
		/// allGuarantees.
		/// </summary>
		public static readonly Rule	RULE11
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-11", new RuleDelegate (Rule11));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if referencePrice is present then it
		/// is not negative.
		/// </summary>
		public static readonly Rule	RULE12
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-12", new RuleDelegate (Rule12));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if protectionTerms/creditEvents/creditEventNotice/notifyingParty/buyerPartyReference
		/// is present, its @href attribute must match that of generalTerms/buyerPartyReference.
		/// </summary>
		public static readonly Rule	RULE13
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-13", new RuleDelegate (Rule13));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if protectionTerms/creditEvents/creditEventNotice/notifyingParty/sellerPartyReference
		/// is present, its @href attribute must match that of generalTerms/sellerPartyReference.
		/// </summary>
		public static readonly Rule	RULE14
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-14", new RuleDelegate (Rule14));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the valuation method is valid when
		/// there is one obligation and one valuation date.
		/// </summary>
		public static readonly Rule	RULE15
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-15", new RuleDelegate (Rule15));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the valuation method is valid when
		/// there is one obligation and multiple valuation dates.
		/// </summary>
		public static readonly Rule	RULE16
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-16", new RuleDelegate (Rule16));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the valuation method is valid when
		/// there are multiple obligations and one valuation date.
		/// </summary>
		public static readonly Rule	RULE17
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-17", new RuleDelegate (Rule17));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the valuation method is valid when
		/// there are multiple obligations and multiple valuation dates.
		/// </summary>
		public static readonly Rule	RULE18
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-18", new RuleDelegate (Rule18));

		/// <summary>
		/// A <see cref="Rule"/> that ensures elements related to ISDA 2003 contracts
		/// are not present in ISDA 1999 contracts.
		/// </summary>
		public static readonly Rule	RULE19
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-19", new RuleDelegate (Rule19));

		/// <summary>
		/// A <see cref="Rule"/> that ensures elements related to ISDA 1999 contracts
		/// are not present in ISDA 2003 contracts.
		/// </summary>
		public static readonly Rule	RULE20
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-20", new RuleDelegate (Rule20));

		/// <summary>
		/// A <see cref="Rule"/> that ensures a short form contract does not contain invalid
		/// elements.
		/// </summary>
		public static readonly Rule	RULE21
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-21", new RuleDelegate (Rule21));

		/// <summary>
		/// A <see cref="Rule"/> that ensures a short form contract does not contain invalid
		/// elements.
		/// </summary>
		public static readonly Rule	RULE21B
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-21b", new RuleDelegate (Rule21b));

		/// <summary>
		/// A <see cref="Rule"/> that ensures short form contracts can only contain
		/// restructuring events.
		/// </summary>
		public static readonly Rule	RULE22
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-22", new RuleDelegate (Rule22));

		/// <summary>
		/// A <see cref="Rule"/> that ensures long form contracts specify the
		/// settlement terms.
		/// </summary>
		public static readonly Rule	RULE23
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-23", new RuleDelegate (Rule23));

		/// <summary>
		/// A <see cref="Rule"/> that ensures long form contracts contain mandatory
		/// data values.
		/// </summary>
		public static readonly Rule	RULE24
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-24", new RuleDelegate (Rule24));

		/// <summary>
		/// A <see cref="Rule"/> that ensures long form contracts with physical
		/// settlement contain the necessary data.
		/// </summary>
		public static readonly Rule	RULE25
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-25", new RuleDelegate (Rule25));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if feeLeg/singlePayment/adjustablePaymentDate
		/// is present then feeLeg/singlePayment/adjustablePaymentDate &gt;
		/// generalTerms/effectiveDate/unadjustedDate.
		/// </summary>
		public static readonly Rule	RULE26
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-26", new RuleDelegate (Rule26));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if a payment date is defined it falls
		/// before the termination date.
		/// </summary>
		public static readonly Rule	RULE27
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-27", new RuleDelegate (Rule27));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if a first payment date is present it
		/// falls after the effective date.
		/// </summary>
		public static readonly Rule	RULE28
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-28", new RuleDelegate (Rule28));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if a first payment date is present it
		/// falls before the termination date.
		/// </summary>
		public static readonly Rule	RULE29
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-29", new RuleDelegate (Rule29));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if a last regular payment date is present it
		/// falls before the termination date.
		/// </summary>
		public static readonly Rule	RULE30
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-30", new RuleDelegate (Rule30));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the first payment date falls before the
		/// last regular payment date.
		/// </summary>
		public static readonly Rule	RULE31
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-31", new RuleDelegate (Rule31));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if a long form contracts defines a feeLeg then
		/// it must contain a calculationAmount and dayCountFraction.
		/// </summary>
		public static readonly Rule	RULE32
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-32", new RuleDelegate (Rule32));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the interval between the first and last payment
		/// dates is a multiple of the paymentFrequency.
		/// </summary>
		public static readonly Rule	RULE33
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-33", new RuleDelegate (Rule33));

		/// <summary>
		/// A <see cref="Rule"/> that ensures only the allowed reference obligations
		/// are defined.
		/// </summary>
		public static readonly Rule	RULE34
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-34", new RuleDelegate (Rule34));

		/// <summary>
		/// A <see cref="Rule"/> that ensures at least on credit event type is defined.
		/// </summary>
		public static readonly Rule	RULE35
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-35", new RuleDelegate (Rule35));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the correct number of information sources
		/// are defined.
		/// </summary>
		public static readonly Rule	RULE36
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-36", new RuleDelegate (Rule36));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the quotation amount is no smaller than
		/// the minimum quotation amount.
		/// </summary>
		public static readonly Rule	RULE37
			= new DelegatedRule (Preconditions.R4_0__LATER, "cd-37", new RuleDelegate (Rule37));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the if any
		/// <c>referencePoolItem/constituentWeight/basketPercentage</c> values
		/// are defined then they must sum to 1.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule	RULE38
			= new DelegatedRule (Preconditions.R4_2__LATER, "cd-38", new RuleDelegate (Rule38));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if <c>nthToDefault</c> is present
		/// and <c>mthToDefault</c> is present then <c>nthToDefault</c>
		/// must be less than <c>mthToDefaultM</c>.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule	RULE39
			= new DelegatedRule (Preconditions.R4_2__LATER, "cd-39", new RuleDelegate (Rule39));

		/// <summary>
		/// A <see cref="Rule"/> that ensures <c>attachmentPoint</c> must be
		/// less than or equal to <c>exhaustionPoint</c>.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule	RULE40
			= new DelegatedRule (Preconditions.R4_2__LATER, "cd-40", new RuleDelegate (Rule40));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if <c>indexReferenceInformation/tranche</c>
		/// is not present then <c>modifiedEquityDelivery</c> must not be present.
		/// </summary>
		/// <remarks>Applies to FpML 4.3 and later.</remarks>
		public static readonly Rule	RULE41
			= new DelegatedRule (Preconditions.R4_3__LATER, "cd-41", new RuleDelegate (Rule41));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if <c>basketReferenceInformation</c>c>
		/// is not present then <c>substitution</c> must not be present.
		/// </summary>
		/// <remarks>Applies to FpML 4.3 and later.</remarks>
		public static readonly Rule	RULE42
			= new DelegatedRule (Preconditions.R4_3__LATER, "cd-42", new RuleDelegate (Rule42));

		/// <summary>
		/// A <see cref="Rule"/> that if the trade has an initial payment
		/// then it is paid by the protection buyer to the protection seller.
		/// </summary>
		/// <remarks>Applies to FpML 4.3 and later.</remarks>
		public static readonly Rule	RULE43
			= new DelegatedRule (Preconditions.R4_3__LATER, "cd-43", new RuleDelegate (Rule43));

		/// <summary>
		/// A <see cref="Rule"/> that ensures either every <c>referencePoolItem</c>
		/// has a <c>basketPercentage</c> or that none of them have.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule	RULE44
			= new DelegatedRule (Preconditions.R4_2__LATER, "cd-44", new RuleDelegate (Rule44));

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = RuleSet.ForName ("CdsRules");

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private CdsRules ()
		{ }

		/// <summary>
		/// Determines if the trade under test references ISDA 1999 definitions.
		/// </summary>
		/// <param name="trade">The trade element.</param>
		/// <returns><b>true</b> if trade uses ISDA 1999 definitions.</returns>
		private static bool IsIsda1999 (XmlElement trade)
		{
			if (Exists (XPath.Path (trade, "creditDefaultSwap"))) {
				XmlNode			target;

				if ((target = XPath.Path (trade, "documentation", "contractualDefinitions")) != null)
					if (target.InnerText.Trim ().StartsWith ("ISDA1999Credit"))
						return (true);
				
				if ((target = XPath.Path (trade, "documentation", "masterConfirmation", "masterConfirmationType")) != null)
					if (target.InnerText.Trim ().StartsWith ("ISDA1999Credit"))
						return (true);
			}		
			return (false);
		}

		/// <summary>
		/// Determines if the trade under test references ISDA 2003 definitions.
		/// </summary>
		/// <param name="trade">The trade element.</param>
		/// <returns><b>true</b> if trade uses ISDA 2003 definitions.</returns>
		private static bool IsIsda2003 (XmlElement trade)
		{
			if (Exists (XPath.Path (trade, "creditDefaultSwap"))) {
				XmlNode			target;

				if ((target = XPath.Path (trade, "documentation", "contractualDefinitions")) != null)
					if (target.InnerText.Trim ().StartsWith ("ISDA2003Credit"))
						return (true);
				
				if ((target = XPath.Path (trade, "documentation", "masterConfirmation", "masterConfirmationType")) != null)
					if (target.InnerText.Trim ().StartsWith ("ISDA2003Credit") ||
						target.InnerText.Trim ().StartsWith ("ISDA2004Credit"))
						return (true);
			}		
			return (false);
		}

		/// <summary>
		/// Determines if the trade under tests contains a short form contract.
		/// </summary>
		/// <param name="trade">The trade element.</param>
		/// <returns><b>true</b> if the contract is short form.</returns>
		private static bool IsShortForm (XmlElement trade)
		{
			XmlElement	target;

			if (Exists (XPath.Path (trade, "creditDefaultSwap"))) {
				if (Exists (XPath.Path (trade, "documentation", "masterConfirmation")))
					return (true);
				if (Exists (XPath.Path (trade, "documentation", "contractualMatrix")))
					return (true);

				if ((target = XPath.Path (trade, "documentation", "contractualTermsSupplement")) != null) {
					string	value = target.InnerText.Trim ();
					if (value.StartsWith ("iTraxx") ||
						value.StartsWith ("CDX"))
						return (true);
				}
			}
			return (false);
		}

		/// <summary>
		/// Determines if the trade under tests contains a long form contract.
		/// </summary>
		/// <param name="trade">The trade element.</param>
		/// <returns><b>true</b> if the contract is long form.</returns>
		private static bool IsLongForm (XmlElement trade)
		{
			XmlElement	cds;

			if (Exists (cds = XPath.Path (trade, "creditDefaultSwap"))) {
				if (Exists (XPath.Path (trade, "documentation", "masterConfirmation")))
					return (false);
				if (Exists (XPath.Path (trade, "documentation", "contractualMatrix")))
					return (false);

				return (IsSingleName (cds));
			}
			return (false);
		}

		/// <summary>
		/// Determines if a credit default swap is on a single name.
		/// </summary>
		/// <param name="cds">The credit default swap product.</param>
		/// <returns><b>true</b> if the product is single name.</returns>
		private static bool IsSingleName (XmlElement cds)
		{
			if (Exists (XPath.Path (cds, "generalTerms", "referenceInformation")))
				return (true);

			return (false);
		}

		/// <summary>
		/// Determines if a credit default swap is on an index.
		/// </summary>
		/// <param name="cds">The credit default swap product.</param>
		/// <returns><b>true</b> if the product is an index.</returns>
		private static bool IsCreditIndex (XmlElement cds)
		{
			if (Exists (XPath.Path (cds, "generalTerms", "indexReferenceInformation")) &&
			   !Exists (XPath.Path (cds, "generalTerms", "indexReferenceInformation", "tranche")))
				return (true);

			return (false);
		}

		//---------------------------------------------------------------------------

		private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule01 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule01 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule01 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in list) {
				XmlElement		cds;

				if (Exists (cds = XPath.Path (context, "creditDefaultSwap"))) {
					if (!IsSingleName (cds)) continue;

					XmlNode			tradeDate		= XPath.Path (context, "tradeHeader", "tradeDate");
					XmlNode			effectiveDate	= XPath.Path (context, "creditDefaultSwap", "generalTerms", "effectiveDate", "unadjustedDate");

					if ((tradeDate == null) || (effectiveDate == null) || Less (tradeDate, effectiveDate ))
						continue;

					errorHandler ("305", context,
						"Trade date " + tradeDate.InnerText.Trim () + " is not before " +
						"effective date " + effectiveDate.InnerText.Trim (),
						name, null);

					result = false;
				}
			}
			return (result);
		}

		//---------------------------------------------------------------------------

		private static bool Rule01b (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule01b (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule01b (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule01b (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in list) {
				XmlElement		cds;

				if (Exists (cds = XPath.Path (context, "creditDefaultSwap"))) {
					if (!IsCreditIndex (cds)) continue;

					XmlNode			tradeDate		= XPath.Path (context, "tradeHeader", "tradeDate");
					XmlNode			effectiveDate	= XPath.Path (context, "creditDefaultSwap", "generalTerms", "effectiveDate", "unadjustedDate");

					if ((tradeDate == null) || (effectiveDate == null) || !Less (tradeDate, effectiveDate ))
						continue;

					errorHandler ("305", context,
						"Trade date " + tradeDate.InnerText.Trim () + " is not before " +
						"effective date " + effectiveDate.InnerText.Trim (),
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule02 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule02 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in list) {
				if (context ["calculationAgent"] != null) {
					bool			failed	= false;

					foreach (XmlNode node in context ["calculationAgent"].ChildNodes) {
						if (node.NodeType == XmlNodeType.Element) {
							if (node.LocalName.Equals ("calculationAgentPartyReference"))
								continue;

							if (node.LocalName.Equals ("calculationAgentParty") &&
								node.InnerText.Trim ().Equals ("AsSpecifiedInMasterAgreement"))
								continue;

							failed = true;
						}
					}

					if (failed) {
						errorHandler ("305", context,
							"The calculationAgent element may only contain calculationAgentPartyReferences " +
							"or a calculationAgentParty with the value 'AsSpecifiedInMasterAgreement",
							name, null);

						result = false;
					}
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule03 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule03 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule03 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (IsIsda1999 (context)) {
					XmlNode	supplement = XPath.Path (context, "documentation", "contractualSupplement");
					if ((supplement == null) || !supplement.InnerText.Trim ().StartsWith ("ISDA2003Credit"))
						continue;

					errorHandler ("305", context,
						"Illegal contract supplement for ISDA 1999 credit derivative",
						name, supplement.InnerText);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule04 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule04 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}

		private static bool Rule04 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (IsIsda2003 (context)) {
					XmlNode	supplement = XPath.Path (context, "documentation", "contractualSupplement");
					if ((supplement == null) || !supplement.InnerText.Trim ().StartsWith ("ISDA1999Credit"))
						continue;

					errorHandler ("305", context,
						"Illegal contract supplement for ISDA 2003 credit derivative",
						name, supplement.InnerText);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("generalTerms")) {
				if (Exists (XPath.Path (context, "scheduledTerminationDate", "adjustableDate"))) {
					XmlNode			effectiveDate	= XPath.Path (context, "effectiveDate", "unadjustedDate");
					XmlNode			terminationDate	= XPath.Path (context, "scheduledTerminationDate", "adjustableDate", "unadjustedDate");

					if ((effectiveDate == null) || (terminationDate == null) || Less (effectiveDate, terminationDate ))
						continue;

					errorHandler ("305", context,
						"Effective date " + effectiveDate.InnerText.Trim () + " is not " +
						"before scheduled termination date " + terminationDate.InnerText.Trim (), 
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule06 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("generalTerms")) {
				if (NotEqual (context ["buyerPartyReference"].GetAttribute ("href"),
							  context ["sellerPartyReference"].GetAttribute ("href")))
					continue;

				errorHandler ("305", context,
					"Buyer party reference is equal to seller party reference",
					name, context ["sellerPartyReference"].GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule07 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule07 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule07 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement trade in list) {
				if (IsLongForm (trade)) {
					XmlElement		context = XPath.Path (trade, "creditDefaultSwap", "generalTerms") as XmlElement;

					if (Or (
							Exists (XPath.Path (context, "effectiveDate", "dateAdjustments")),
							Exists (XPath.Path (context, "effectiveDate", "dateAdjustmentsReference"))))
						continue;

					errorHandler ("305", context,
						"Neither date adjustments nor a date adjustments reference " +
						"have been supplied for the effective date",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule08 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule08 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule08 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule08 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement trade in list) {
				if (IsLongForm (trade)) {
					XmlElement		context = XPath.Path (trade, "creditDefaultSwap", "generalTerms");

					if (!Exists (XPath.Path (context, "scheduledTerminationDate"))) continue;

					if (Or (
							Exists (XPath.Path (context, "scheduledTerminationDate", "adjustableDate", "dateAdjustments")),
							Exists (XPath.Path (context, "scheduledTerminationDate", "adjustableDate", "dateAdjustmentsReference"))))
						continue;

					errorHandler ("305", context,
						"Neither date adjustments nor a date adjustments reference " +
						"have been supplied for the scheduled termination date",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule09 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("referenceInformation")) {
				string			primaryReference;
				string			primaryId;

				foreach (XmlElement primary in XPath.Paths (context, "referenceObligation", "primaryObligorReference")) {
					if (Equal (
							primaryReference = primary.GetAttribute ("href"),
							primaryId		 = context ["referenceEntity"].GetAttribute ("id")))
						continue;

					errorHandler ("305", context,
						"Primary obligor reference '" + primaryReference +
						"' should point to reference entity '" + primaryId + "'",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule10 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("referenceInformation")) {
				string			primaryReference;
				string			primaryId;

				foreach (XmlElement primary in XPath.Paths (context, "referenceObligation", "guarantorReference")) {
					if (Equal (
							primaryReference = primary.GetAttribute ("href"),
							primaryId		 = context ["referenceEntity"].GetAttribute ("id")))
						continue;

					errorHandler ("305", context,
						"Primary obligor reference '" + primaryReference +
						"' should point to the reference entity ' " + primaryId + "'",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule11 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement trade in nodeIndex.GetElementsByName ("trade")) {
				if (IsIsda2003 (trade) && IsLongForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap", "generalTerms", "referenceInformation") as XmlElement;

					if (Exists (XPath.Path (context, "allGuarantees"))) continue;

					errorHandler ("305", context,
						"allGuarantees element missing in protection terms",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule12 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("referenceInformation")) {
				if (Exists (context ["referencePrice"])) {
					if (GreaterOrEqual (context ["referencePrice"], 0.0))
						continue;

					errorHandler ("305", context,
						"If referencePrice is present it must not have a negative " +
						"value",
						name, context ["referencePrice"].InnerText);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule13 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				foreach (XmlElement buyer in XPath.Paths (context, "protectionTerms", "creditEvents", "creditEventNotice", "notifyingParty", "buyerPartyReference")) {
					string		buyerName;
					string		referenceName;

					if (Equal (
							buyerName = buyer.GetAttribute ("href"),
							referenceName = XPath.Path (context, "generalTerms", "buyerPartyReference").GetAttribute ("href")))
						continue;

					errorHandler ("305", context,
						"Credit event notice references buyer party reference " + buyerName +
						" but general terms references " + referenceName,
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule14 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				foreach (XmlElement seller in XPath.Paths (context, "protectionTerms", "creditEvents", "creditEventNotice", "notifyingParty", "sellerPartyReference")) {
					if (Equal (seller.GetAttribute ("href"),
							XPath.Path (context, "generalTerms", "sellerPartyReference").GetAttribute ("href")))
						continue;

					errorHandler ("305", context,
						"If protectionTerms/creditEvents/creditEventNotice/notifyingParty/sellerPartyReference " +
						"is present, its @href attribute must match that of generalTerms/sellerPartyReference",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule15 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				if (Implies (
						And (
							Equal (
								Count (XPath.Paths (context, "generalTerms", "referenceInformation", "referenceObligation")), 1),
							Exists (XPath.Path (context, "cashSettlementTerms", "valuationDate", "singleValuationDate"))),
						Or (
							Equal (
								XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
								"Market"),
							Equal (
								XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
								"Highest"))))
					continue;

				errorHandler ("305", context,
					"If there is exactly one generalTerms/referenceInformation/referenceObligation " +
					"and cashSettlementTerms/valuationDate/singleValuationDate occurs " +
					"then the value of cashSettlementTerms/valuationMethod must be " +
					"Market or Highest",
					name, XPath.Path (context, "cashSettlementTerms", "valuationMethod").InnerText);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule16 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				if (Implies (
						And (
							Equal (
								Count (XPath.Paths (context, "generalTerms", "referenceInformation", "referenceObligation")), 1),
							Exists (XPath.Path (context, "cashSettlementTerms", "valuationDate", "multipleValuationDates"))),
						Or (
							Equal (
								XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
								"AverageMarket"),
							Or (
								Equal (
									XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
									"Highest"),
								Equal (
									XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
									"AverageHighest")))))
					continue;

				errorHandler ("305", context,
					"If there is exactly one generalTerms/referenceInformation/referenceObligation " +
					"and cashSettlementTerms/valuationDate/multipleValuationDates occurs " +
					"then the value of cashSettlementTerms/valuationMethod must be " +
					"AverageMarket, Highest or AverageHighest",
					name, XPath.Path (context, "cashSettlementTerms", "valuationMethod").InnerText);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule17 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				if (Implies (
						And (
							Greater (
								Count (XPath.Paths (context, "generalTerms", "referenceInformation", "referenceObligation")), 1),
							Exists (XPath.Path (context, "cashSettlementTerms", "valuationDate", "singleValuationDate"))),
						Or (
							Equal (
								XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
								"BlendedMarket"),
							Equal (
								XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
								"BlendedHighest"))))
					continue;

				errorHandler ("305", context,
					"If there are more that one generalTerms/referenceInformation/referenceObligation " +
					"and cashSettlementTerms/valuationDate/singleValuationDate occurs " +
					"then the value of cashSettlementTerms/valuationMethod must be " +
					"BlendedMarket or BlendedHighest",
					name, XPath.Path (context, "cashSettlementTerms", "valuationMethod").InnerText);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule18 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				if (Implies (
						And (
							Greater (
								Count (XPath.Paths (context, "generalTerms", "referenceInformation", "referenceObligation")), 1),
							Exists (XPath.Path (context, "cashSettlementTerms", "valuationDate", "multipleValuationDates"))),
						Or (
							Equal (
								XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
								"AverageBlendedMarket"),
							Equal (
								XPath.Path (context, "cashSettlementTerms", "valuationMethod"),
								"AverageBlendedHighest"))))
					continue;

				errorHandler ("305", context,
					"If there are more than one generalTerms/referenceInformation/referenceObligation " +
					"and cashSettlementTerms/valuationDate/multipleValuationDates occurs " +
					"then the value of cashSettlementTerms/valuationMethod must be " +
					"AverageBlendedMarket or AverageBlendedHighest",
					name, XPath.Path (context, "cashSettlementTerms", "valuationMethod").InnerText);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule19 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule19 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule19 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule19 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement trade in list) {
				if (IsIsda1999 (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap");
	
					result &=
						  Rule19 (name, context, XPath.Path (context, "protectionTerms", "creditEvents", "creditEventNotice", "businessCenter"), errorHandler)
						& Rule19 (name, context, XPath.Path (context, "protectionTerms", "creditEvents", "restructuring", "multipleHolderObligation"), errorHandler)
						& Rule19 (name, context, XPath.Path (context, "protectionTerms", "creditEvents", "restructuring", "multipleCreditEventNotices"), errorHandler)
						& Rule19 (name, context, XPath.Path (context, "generalTerms", "referenceInformation", "allGuarantees"), errorHandler);
				}
			}
			return (result);
		}

		private static bool Rule19 (string name, XmlElement context, XmlElement illegal, ValidationErrorHandler errorHandler)
		{
			if (illegal != null) {
				errorHandler ("305", context,
					"Illegal element found in ISDA 1999 credit default swap",
					name, XPath.ForNode (illegal));

				return (false);
			}
			return (true);
		}

		// --------------------------------------------------------------------

		private static bool Rule20 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule20 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule20 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule20 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement trade in list) {
				if (IsIsda2003 (trade)) {
					XmlElement context = XPath.Path (trade, "creditDefaultSwap") as XmlElement;

					if (!Exists (XPath.Path (context, "protectionTerms", "obligations", "notContingent")))
						continue;

					errorHandler ("305", context,
						"Illegal element found in ISDA 2003 credit default swap",
						name, "notContingent");

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule21 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule21 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule21 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule21 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement trade in list) {
				if (IsShortForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap");

					if (!IsSingleName (context)) continue;

					result &=
						  Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "settlementCurrency"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "valuationDate"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "valuationTime"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "quotationMethod"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "quotationAmount"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "minimumQuotationAmount"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "dealer"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "cashSettlementBusinessDays"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "accruedInterest"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "cashSettlementTerms", "valuationMethod"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "physicalSettlementTerms"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "feeLeg", "periodicPayment", "fixedAmountCalculation", "calculationAmount"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "feeLeg", "periodicPayment", "fixedAmountCalculation", "dayCountFraction"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "protectionTerms", "obligations"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "generalTerms", "referenceInformation", "allGuarantees"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "generalTerms", "referenceInformation", "referencePrice"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "generalTerms", "effectiveDate", "dateAdjustments"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "generalTerms", "effectiveDate", "dateAdjustmentsReference"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "generalTerms", "scheduledTerminationDate", "adjustableDate", "dateAdjustments"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "generalTerms", "scheduledTerminationDate", "adjustableDate", "dateAdjustmentsReference"), errorHandler)
						& Rule21 (name, context, XPath.Path (context, "generalTerms", "dateAdjustments"), errorHandler);
				}
			}
			return (result);
		}

		private static bool Rule21 (string name, XmlElement context, XmlElement illegal, ValidationErrorHandler errorHandler)
		{
			if (illegal != null) {
				errorHandler ("305", context,
					"Illegal element found in short form credit default swap",
					name, XPath.ForNode (illegal));

				return (false);
			}
			return (true);
		}

		// --------------------------------------------------------------------

		private static bool Rule21b (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule21b (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule21b (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule21b (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool			result = true;

			foreach (XmlElement trade in list) {
				if (IsShortForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap");

					if (!IsCreditIndex (context)) continue;

					result &=
						  Rule21b (name, context, XPath.Path (context, "cashSettlementTerms"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "feeLeg", "periodicPayment", "fixedAmountCalculation", "calculationAmount"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "feeLeg", "periodicPayment", "fixedAmountCalculation", "dayCountFraction"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "protectionTerms", "obligations"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "generalTerms", "effectiveDate", "dateAdjustments"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "generalTerms", "effectiveDate", "dateAdjustmentsReference"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "generalTerms", "scheduledTerminationDate", "adjustableDate", "dateAdjustments"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "generalTerms", "scheduledTerminationDate", "adjustableDate", "dateAdjustmentsReference"), errorHandler)
						& Rule21b (name, context, XPath.Path (context, "generalTerms", "dateAdjustments"), errorHandler);
				}
			}
			return (result);
		}

		private static bool Rule21b (string name, XmlElement context, XmlElement illegal, ValidationErrorHandler errorHandler)
		{
			if (illegal != null) {
				errorHandler ("305", context,
					"Illegal element found in short form credit default swap",
					name, XPath.ForNode (illegal));

				return (false);
			}
			return (true);
		}

		// --------------------------------------------------------------------

		private static bool Rule22 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule22 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule22 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule22 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement trade in list) {
				if (IsShortForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap");
					XmlElement	events  = XPath.Path (context, "protectionTerms", "creditEvents");

					if (events != null) {
						XmlNodeList	children = events.ChildNodes;
						for (int count = 0; count < children.Count; ++count) {
							XmlNode	node = children [count];
							if (node is XmlElement) {
								String localName = node.LocalName;

								if (localName.Equals ("bankruptcy") ||
									localName.Equals ("failureToPay") ||
									localName.Equals ("repudiationMoratorium") ||
									localName.Equals ("obligationDefault") ||
									localName.Equals ("obligationAcceleration")) {
									errorHandler ("305", context,
										"Illegal element found in short form credit default swap",
										name, localName);

										result = false;
								}
							}
						}
					}
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule23 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule23 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule23 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule23 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement trade in list) {
				if (IsLongForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap");

					if (Or (
						Exists (XPath.Path (context, "cashSettlementTerms")),
						Exists (XPath.Path (context, "physicalSettlementTerms"))))
						continue;

					errorHandler ("305", context,
						"Neither cash nor physical settlement terms are present",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule24 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule24 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule24 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule24 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement trade in list) {
				if (IsLongForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap");

					if (!Exists (XPath.Path (context, "protectionTerms", "creditEvents", "creditEventNotice"))) {
						errorHandler ("305", context,
							"Long Form credit default swap is missing a mandatory element",
							name, "protectionEvents/creditEvents/creditEventNotices");

						result = false;
					}

					if (!Exists (XPath.Path (context, "protectionTerms", "obligations"))) {
						errorHandler ("305", context,
							"Long Form credit default swap is missing a mandatory element",
							name, "protectionTerms/obligations");

						result = false;
					}

					if (!Exists (XPath.Path (context, "generalTerms", "referenceInformation", "referencePrice"))) {
						errorHandler ("305", context,
							"Long Form credit default swap is missing a mandatory element",
							name, "generalTerms/referenceInformation/referencePrice");

						result = false;
					}
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule25 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule25 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule25 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement trade in list) {
				if (IsLongForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap");

					if (Exists (XPath.Path (context, "physicalSettlementTerms"))) {
						if (!Exists (XPath.Path (context, "physicalSettlementTerms", "settlementCurrency"))) {
							errorHandler ("305", context,
								"A mandatory element for physical settlement is missing",
								name, "physicalSettlementTerms/settlementCurrency");

							result = false;
						}

						if (!Exists (XPath.Path (context, "physicalSettlementTerms", "escrow"))) {
							errorHandler ("305", context,
								"A mandatory element for physical settlement is missing",
								name, "physicalSettlementTerms/escrow");

							result = false;
						}

						if (!Exists (XPath.Path (context, "physicalSettlementTerms", "deliverableObligations", "accruedInterest"))) {
							errorHandler ("305", context,
								"A mandatory element for physical settlement is missing",
								name, "physicalSettlementTerms/deliverableObligations/accruedInterest");

							result = false;
						}
					}
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule26 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool			result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				XmlElement		paymentDate;
				XmlElement		effectiveDate;

				if (Implies (
						Exists (XPath.Path (context, "feeLeg", "singlePayment", "adjustablePaymentDate")),
						Greater (
							paymentDate   = XPath.Path (context, "feeLeg", "singlePayment", "adjustablePaymentDate"),
							effectiveDate = XPath.Path (context, "generalTerms", "effectiveDate", "unadjustedDate"))))
					continue;

				errorHandler ("305", context,
					"Single payment date " + paymentDate.InnerText + " must be " +
					"after the effective date " + effectiveDate.InnerText,
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule27 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				XmlElement	feeDate;
				XmlElement	termDate;

				if (And (
					Exists (feeDate = XPath.Path (context, "feeLeg", "singlePayment", "adjustablePaymentDate")),
					Exists (termDate = XPath.Path (context, "generalTerms", "scheduledTerminationDate", "adjustableDate", "unadjustedDate")))) {
					if (Less (feeDate, termDate)) continue;

					errorHandler ("305", context,
						"Single payment date '" + feeDate.InnerText + "' must be " +
						"before scheduled termination date '" + termDate.InnerText + "'",
						name, null);

					result = false;
				}		
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule28 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				XmlElement	paymentDate;
				XmlElement	effectiveDate;

				if (Implies (
					Exists (XPath.Path (context, "feeLeg", "periodicPayment", "firstPaymentDate")),
					Greater (
						paymentDate = XPath.Path (context, "feeLeg", "periodicPayment", "firstPaymentDate"),
						effectiveDate = XPath.Path (context, "generalTerms", "effectiveDate", "unadjustedDate"))))
					continue;

				errorHandler ("305", context,
					"First periodic payment date '" + paymentDate.InnerText + "' " +
					"must be after the effective date '" + effectiveDate.InnerText + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule29 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				XmlElement	paymentDate;
				XmlElement	terminationDate;

				if (And (
					Exists (paymentDate = XPath.Path (context, "feeLeg", "periodicPayment", "firstPaymentDate")),
					Exists (terminationDate = XPath.Path (context, "generalTerms", "scheduledTerminationDate", "adjustableDate", "unadjustedDate")))) {
					if (Less (paymentDate, terminationDate)) continue;

					errorHandler ("305", context,
						"First periodic payment date '" + paymentDate.InnerText + "' " +
						"must be before the termination date '" + terminationDate.InnerText + "'",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule30 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				XmlElement	paymentDate;
				XmlElement	terminationDate;

				if (And (
					Exists (paymentDate = XPath.Path (context, "feeLeg", "periodicPayment", "lastRegularPaymentDate")),
					Exists (terminationDate = XPath.Path (context, "generalTerms", "scheduledTerminationDate", "adjustableDate", "unadjustedDate")))) {
					if (Less (paymentDate, terminationDate)) continue;

					errorHandler ("305", context,
						"Last regular periodic payment date '" + paymentDate.InnerText + "' " +
						"must be before the termination date '" + terminationDate.InnerText + "'",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule31 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule31 (name, nodeIndex.GetElementsByName ("periodicPayment"), errorHandler));
		}

		private static bool Rule31 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	firstDate;
				XmlElement	lastDate;

				if (Implies (
						And (
							Exists (firstDate = XPath.Path (context, "firstPaymentDate")),
							Exists (lastDate  = XPath.Path (context, "lastRegularPaymentDate"))),
						Less (firstDate, lastDate)))
					continue;

				errorHandler ("305", context,
					"First payment date '" + firstDate.InnerText + "' must be before " +
					"last payment date '" + lastDate.InnerText + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule32 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule32 (name, nodeIndex.GetElementsByName ("trade"), errorHandler)
				& Rule32 (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule32 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement trade in list) {
				if (IsLongForm (trade)) {
					XmlElement	context = XPath.Path (trade, "creditDefaultSwap", "feeLeg", "periodicPayment");

					if (context == null) continue;

					if (!Exists (XPath.Path (context, "fixedAmountCalculation", "calculationAmount"))) {
						errorHandler ("305", context,
							"Calculation amount must be present in the fixed amount " +
							"calculation of periodic payment",
							name, null);

						result = false;
					}

					if (!Exists (XPath.Path (context, "fixedAmountCalculation", "dayCountFraction"))) {
						errorHandler ("305", context,
							"Day count fraction must be present in the fixed amount " +
							"calculation of periodic payment",
							name, null);

						result = false;
					}
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule33 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule33 (name, nodeIndex.GetElementsByName ("periodicPayment"), errorHandler));
		}

		private static bool Rule33 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	firstDate	= XPath.Path (context, "firstPaymentDate");
				XmlElement	lastDate	= XPath.Path (context, "lastRegularPaymentDate");

				if ((firstDate == null) || (lastDate == null)) continue;

				Interval	interval	= ToInterval (XPath.Path (context, "paymentFrequency"));

				if (interval.DividesDates (ToDate (firstDate), ToDate (lastDate)))
					continue;

				errorHandler ("305", context,
					"Last regular payment date '" + lastDate.InnerText + "' is not " +
					"an integer multiple of the payment period after the first payment " +
					" date '" + firstDate.InnerText + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule34 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule34 (name, nodeIndex.GetElementsByName ("deliverableObligations"), errorHandler));
		}

		private static bool Rule34 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Equal (XPath.Path (context, "category"), "ReferenceObligationsOnly")) {
					foreach (XmlNode node in context.ChildNodes) {
						if ((node is XmlElement) && !node.LocalName.Equals ("category")) {
							errorHandler ("305", context,
								"Deliverable obligations category is set to 'Reference " +
								" Obligations Only' but further elements have been included",
								name, null);

							result = false;
							break;
						}
					}
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule35 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule35 (name, nodeIndex.GetElementsByName ("creditEvents"), errorHandler));
		}

		private static bool Rule35 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (DOM.GetFirstChild (context) == null) {
					errorHandler ("305", context,
						"No elements where found in creditEvents. The structure must " +
						"contain at least one element",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule36 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule36 (name, XPath.Paths (nodeIndex.GetElementsByName ("creditEvents"), "creditEventNotice", "publiclyAvailableInformation"), errorHandler));
		}

		private static bool Rule36 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				if (Or (
						Exists (XPath.Path (context, "standardPublicSources")),
						Exists (XPath.Path (context, "publicSource"))))
					continue;

				errorHandler ("305", context,
					"Either at least one public source or standard public sources " +
					"must be referred to in publiclyAvailableInformation",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule37 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule37 (name, nodeIndex.GetElementsByName ("cashSettlementTerms"), errorHandler));
		}

		private static bool Rule37 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	ccy1	= XPath.Path (context, "quotationAmount", "currency");
				XmlElement	amount	= XPath.Path (context, "quotationAmount", "amount");
				XmlElement	ccy2	= XPath.Path (context, "minimumQuotationAmount", "currency");
				XmlElement	minimum = XPath.Path (context, "minimumQuotationAmount", "amount");

				if ((ccy1 == null) || (ccy2 == null) || (amount == null) || (minimum == null)
					|| NotEqual (ccy1, ccy2)
					|| (ToDouble (amount) >= ToDouble (minimum)))
					continue;

				errorHandler ("305", context,
					"In cash settlement terms, quotation amount " + amount.InnerText +
					" must be greater or equal to minimum quotation amount",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule38 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule38 (name, nodeIndex.GetElementsByName ("creditDefaultSwap"), errorHandler));
		}

		private static bool Rule38 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement 	pool	= XPath.Path (context, "generalTerms", "basketReferenceInformation", "referencePool");
				XmlNodeList	items	= XPath.Paths (pool, "referencePoolItem", "constituentWeight", "basketPercentage");

				if (items.Count == 0) continue;

				Decimal total = 0;
				foreach (XmlElement item in items)
					total += ToDecimal (item);

				if (Equal (total, decimal.One)) continue;

				errorHandler ("305", pool,
						"The sum of referencePoolItem/constituentWeight/basketPercentage should be equal to 1",
						name, total.ToString ());

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule39 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule39 (name, nodeIndex.GetElementsByName ("creditDefaultSwap"), errorHandler));
		}

		private static bool Rule39 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	info	= XPath.Path (context, "generalTerms", "basketReferenceInformation");
				XmlElement	nth		= XPath.Path (context, "nthToDefault");
				XmlElement	mth		= XPath.Path (context, "mthToDefault");

				if ((nth == null) || (mth == null) || (ToInteger (nth) < ToInteger (mth))) continue;

				errorHandler ("305", info,
						"If nthToDefault is present and mthToDefault is present then nthToDefault must be less than mthToDefault.",
						name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule40 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule40 (name, nodeIndex.GetElementsByName ("creditDefaultSwap"), errorHandler));
		}

		private static bool Rule40 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	tranche	= XPath.Path (context, "generalTerms", "indexReferenceInformation", "tranche");
				XmlElement	attach	= XPath.Path (tranche, "attachmentPoint");
				XmlElement	exhaust	= XPath.Path (tranche, "exhaustionPoint");

				if ((attach == null) || (exhaust == null) || LessOrEqual (ToDecimal (attach), ToDecimal (exhaust))) continue;

				errorHandler ("305", tranche,
						"attachmentPoint must be less than or equal to exhaustionPoint.",
						name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule41 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result 	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("generalTerms")) {
				XmlElement		tranche		= XPath.Path (context, "indexReferenceInformation", "tranche");
				XmlElement		delivery	= XPath.Path (context, "modifiedEquityDelivery");

				if ((tranche == null) && (delivery != null)) {
					errorHandler ("305", context,
						"If indexReferenceInformation/tranche is not present then modifiedEquityDelivery must not be present.",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule42 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result 	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("generalTerms")) {
				XmlElement	basket			= XPath.Path (context, "basketReferenceInformation");
				XmlElement	substitution	= XPath.Path (context, "substitution");

				if ((basket == null) && (substitution != null)) {
					errorHandler ("305", context,
						"If basketReferenceInformation is not present then substitution must not be present.",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule43 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result 	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("creditDefaultSwap")) {
				if (!IsSingleName (context)) continue;

				if (!Exists (XPath.Path(context, "feeLeg", "initialPayment"))) continue;

				XmlElement	payer		= XPath.Path (context, "feeLeg", "initialPayment", "payerPartyReference");
				XmlElement	receiver 	= XPath.Path (context, "feeLeg", "initialPayment", "receiverPartyReference");
				XmlElement	seller		= XPath.Path (context, "generalTerms", "sellerPartyReference");
				XmlElement	buyer		= XPath.Path (context, "generalTerms", "buyerPartyReference");

				if ((payer != null) && (seller != null) && (receiver != null) && (buyer != null)) {
					if (payer.GetAttribute ("href").Equals (buyer.GetAttribute ("href")) &&
						receiver.GetAttribute ("href").Equals (seller.GetAttribute ("href")))
						continue;
				}

				errorHandler ("305", context,
					"The initial payment should be paid by the protection buyer to the protection seller",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule44 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule44 (name, nodeIndex.GetElementsByName ("creditDefaultSwap"), errorHandler));
		}

		private static bool Rule44 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result = true;

			foreach (XmlElement context in list) {
				XmlElement	pool	= XPath.Path (context, "generalTerms", "basketReferenceInformation", "referencePool");
				XmlNodeList	items  	= XPath.Paths (pool, "referencePoolItem");
				XmlNodeList	weights	= XPath.Paths (pool, "referencePoolItem", "constituentWeight", "basketPercentage");

				if ((weights.Count == 0) || (weights.Count == items.Count)) continue;

				errorHandler ("305", pool,
						"Either every referencePoolItem should have a basketPercentage or none should have one",
						name, null);

				result = false;
			}
			return (result);
		}
	}
}