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
	/// The <b>IrdRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the defined validation <see cref="Rule"/> instances for Interest
	/// Rate Derivative Products.
	/// </summary>
	public class IrdRules : Logic
	{
		/// <summary>
		/// Contains the <see cref="RuleSet"/>.
		/// </summary>
		public static RuleSet Rules {
			get {
				return (rules);
			}
		}

#if false
		/// <summary>
		/// A <see cref="Precondition"/> that recognizes IRD products.
		/// </summary>
		public static readonly Precondition	IRD
			= new ElementPrecondition (new string [] { "swap", "swaption", "fra", "bulletPayment" });
#endif

		/// <summary>
		/// A <see cref="Rule"/> that ensures reset dates are present for
		/// floating rate interest streams.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE01
			= new DelegatedRule ("ird-1", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> that ensures payment frequency is a multiple
		/// of the calculation frequency.
		/// </summary>
		public static readonly Rule	RULE02
			= new DelegatedRule ("ird-2", new RuleDelegate (Rule02));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the first payment date matches
		/// a calculation date.
		/// </summary>
		public static readonly Rule	RULE03
			= new DelegatedRule ("ird-3", new RuleDelegate (Rule03));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the last regular payment date
		/// matchs a calculation date.
		/// </summary>
		public static readonly Rule	RULE04
			= new DelegatedRule ("ird-4", new RuleDelegate (Rule04));

		/// <summary>
		/// A <see cref="Rule"/> that ensures calculation frequency is a
		/// multiple of the reset frequency.
		/// </summary>
		public static readonly Rule	RULE05
			= new DelegatedRule ("ird-5", new RuleDelegate (Rule05));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the first payment date is
		/// after the effective date.
		/// </summary>
		public static readonly Rule	RULE06
			= new DelegatedRule ("ird-6", new RuleDelegate (Rule06));

		/// <summary>
		/// A <see cref="Rule"/> that ensures compounding method is present
		/// when the payment frequency is less often than the calculation
		/// frequency.
		/// </summary>
		public static readonly Rule	RULE07
			= new DelegatedRule ("ird-7", new RuleDelegate (Rule07));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the payer and receiver are not
		/// the same party.
		/// </summary>
		/// <remarks>Duplicated by one of the shared rules.</remarks>
		public static readonly Rule	RULE08
			= new DelegatedRule ("ird-8", new RuleDelegate (Rule08));

		/// <summary>
		/// A <see cref="Rule"/> that ensures compounding method is present
		/// only when reset dates are defined.
		/// </summary>
		public static readonly Rule	RULE09
			= new DelegatedRule ("ird-9", new RuleDelegate (Rule09));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the calculation period start
		/// date is consistent with the roll convention.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE10
			= new DelegatedRule ("ird-10", new RuleDelegate (Rule10));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the calculation period end
		/// date is consistent with the roll convention.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE11
			= new DelegatedRule ("ird-11", new RuleDelegate (Rule11));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the calculation period divides
		/// the regular period precisely.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE12
			= new DelegatedRule ("ird-12", new RuleDelegate (Rule12));

		// Rule 13 was unlucky for some.

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted termination date
		/// is after the unadjusted effective date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE14
			= new DelegatedRule ("ird-14", new RuleDelegate (Rule14));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted termination date
		/// is after the unadjusted first period date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE15
			= new DelegatedRule ("ird-15", new RuleDelegate (Rule15));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted termination date
		/// is after the unadjusted first regular period start date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE16
			= new DelegatedRule ("ird-16", new RuleDelegate (Rule16));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted termination date
		/// is after the unadjusted last regular period end date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE17
			= new DelegatedRule ("ird-17", new RuleDelegate (Rule17));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted last regular period
		/// end date is after the unadjusted first regular period start date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE18
			= new DelegatedRule ("ird-18", new RuleDelegate (Rule18));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted last regular period
		/// end date is after the unadjusted first period start date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE19
			= new DelegatedRule ("ird-19", new RuleDelegate (Rule19));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted last regular period
		/// end date is after the unadjusted effective date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE20
			= new DelegatedRule ("ird-20", new RuleDelegate (Rule20));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted first period state
		/// date is before the unadjusted effective date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE21
			= new DelegatedRule ("ird-21", new RuleDelegate (Rule21));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the unadjusted first period start
		/// date is before the unadjusted first regular period start date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE22
			= new DelegatedRule ("ird-22", new RuleDelegate (Rule22));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the initial stub is only present
		/// under the right conditions.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE23
			= new DelegatedRule ("ird-23", new RuleDelegate (Rule23));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the final stub is only present
		/// under the right conditions.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE24
			= new DelegatedRule ("ird-24", new RuleDelegate (Rule24));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if not steps are present
		/// the initial value is non-zero.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE25
			= new DelegatedRule ("ird-25", new RuleDelegate (Rule25));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the business centers reference
		/// locates a set of business centers in the document.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE26
			= new DelegatedRule ("ird-26", new RuleDelegate (Rule26));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the cash settlement payment date
		/// is not present.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE27
			= new DelegatedRule ("ird-27", new RuleDelegate (Rule27));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the cash settlement payment date
		/// references the early termination date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE28A
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "ird-28a", new RuleDelegate (Rule28a));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the cash settlement payment date
		/// references the early termination date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE28B
			= new DelegatedRule (Preconditions.R4_0__LATER, "ird-28b", new RuleDelegate (Rule28b));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that floating rate calculations
		/// are present if there is compounding.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE29
			= new DelegatedRule ("ird-29", new RuleDelegate (Rule29));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that a start date is specified.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE30
			= new DelegatedRule ("ird-30", new RuleDelegate (Rule30));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that an end date is specified.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE31
			= new DelegatedRule ("ird-31", new RuleDelegate (Rule31));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that discount rate day count
		/// fraction is only present if there is a discont rate.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE32
			= new DelegatedRule ("ird-32", new RuleDelegate (Rule32));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that the adjusted termination
		/// date is after the adjusted effective date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE33
			= new DelegatedRule ("ird-33", new RuleDelegate (Rule33));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that a payment date is specified.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE34
			= new DelegatedRule ("ird-34", new RuleDelegate (Rule34));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the first payment date is before
		/// the last regular payment date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE35
			= new DelegatedRule ("ird-35", new RuleDelegate (Rule35));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the interval between the first
		/// payment date and the last regular payment date is a multiple of
		/// the payment frequency.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE36
			= new DelegatedRule ("ird-36", new RuleDelegate (Rule36));

		/// <summary>
		/// A <see cref="Rule"/> that ensures one of an initial or final
		/// stud is present.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE38
			= new DelegatedRule ("ird-38", new RuleDelegate (Rule38));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the adjusted exercise date is
		/// not after the adjusted termination date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE39
			= new DelegatedRule ("ird-39", new RuleDelegate (Rule39));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the adjusted exercise date is
		/// not after the adjusted cash settlement valuation date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE40
			= new DelegatedRule ("ird-40", new RuleDelegate (Rule40));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the adjusted cash settlement
		/// valuation date is not after the adjusted cash settlement payment
		/// date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE41
			= new DelegatedRule ("ird-41", new RuleDelegate (Rule41));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the adjusted exercise date is
		/// before the adjusted extended termination date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE42
			= new DelegatedRule ("ird-42", new RuleDelegate (Rule42));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that atleast one child element is
		/// present.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE43
			= new DelegatedRule ("ird-43", new RuleDelegate (Rule43));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the adjusted early termination
		/// date is not after the adjusted cash settlement date.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE44
			= new DelegatedRule ("ird-44", new RuleDelegate (Rule44));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the cash settlement valuation
		/// date is relative to the cash settlement payment date. 
		/// </summary>
		/// <remarks>Applies to all FpML 1.0, 2.0 and 3.0.</remarks>
		public static readonly Rule	RULE46A
			= new DelegatedRule (Preconditions.R1_0__TR3_0, "ird-46a", new RuleDelegate (Rule46A));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the cash settlement valuation
		/// date is relative to the cash settlement payment date. 
		/// </summary>
		/// <remarks>Applies to all FpML 4.0 and later.</remarks>
		public static readonly Rule	RULE46B
			= new DelegatedRule (Preconditions.R4_0__LATER, "ird-46b", new RuleDelegate (Rule46B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the cash settlement payment 
		/// date for an early termination is relative to an exercise definition.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE47
			= new DelegatedRule ("ird-47", new RuleDelegate (Rule47));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the cash settlement payment 
		/// date for a swaption is relative to an exercise definition.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE48
			= new DelegatedRule ("ird-48", new RuleDelegate (Rule48));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the weekly roll convention is
		/// specified for a weekly period.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE49
			= new DelegatedRule ("ird-49", new RuleDelegate (Rule49));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the step dates are valid
		/// for a notional step schedule.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE50
			= new DelegatedRule ("ird-50", new RuleDelegate (Rule50));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the step dates are valid
		/// for a fixed rate schedule.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE51
			= new DelegatedRule ("ird-51", new RuleDelegate (Rule51));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the step dates are valid
		/// for a cap rate schedule.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE52
			= new DelegatedRule ("ird-52", new RuleDelegate (Rule52));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the step dates are valid
		/// for a floor rate schedule.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE53
			= new DelegatedRule ("ird-53", new RuleDelegate (Rule53));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the step dates are valid
		/// for a known amount schedule.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE54
			= new DelegatedRule ("ird-54", new RuleDelegate (Rule54));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the calculation period dates
		/// reference matches a calculation period dates in same interest
		/// rate stream.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE55
			= new DelegatedRule ("ird-55", new RuleDelegate (Rule55));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the reset dates reference
		/// matches a reset dates definition in the same interest rate
		/// stream.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE56
			= new DelegatedRule ("ird-56", new RuleDelegate (Rule56));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the calculation period is consistent
		/// with the period when it is neither 'M' or 'Y'.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE57
			= new DelegatedRule ("ird-57", new RuleDelegate (Rule57));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the calculation period is weekly when
		/// the roll convention is a weekday.
		/// </summary>
		/// <remarks>Applies to all FpML releases.</remarks>
		public static readonly Rule	RULE58
			= new DelegatedRule ("ird-58", new RuleDelegate (Rule58));

		// --------------------------------------------------------------------

		/// <summary>
		/// Determines if an element of type <c>InterestRateStream</c> contains
		/// no <c>cashflows</c> element, or <c>cashflows/cashflowsMatchParameters</c>
		/// contains <c>true</c>.
		/// </summary>
		/// <param name="stream">The stream <see cref="XmlElement"/>.</param>
		/// <returns><c>true</c> if the swap is parametric.</returns>
		protected static bool IsParametric (XmlElement stream)
		{
			XmlElement	cashflows;

			if (Exists (cashflows = XPath.Path (stream, "cashflows")))
				return (Bool (XPath.Path (cashflows, "cashflowsMatchParameters")));

			return (true);
		}

		// --------------------------------------------------------------------

		private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule01 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule01 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule01 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (Iff (
						Exists (XPath.Path (context, "resetDates")),
						Or (
							Exists (XPath.Path (context, "calculationPeriodAmount", "calculation", "floatingRateCalculation")),
							Exists (XPath.Path (context, "calculationPeriodAmount", "calculation", "inflationRateCalculation")))))
					continue;

				errorHandler ("305", context,
					"resetDates must be present if and only if a floatingRateCalculation " +
					"element is present in calculationPeriodAmount",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule02 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule02 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule02 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (!IsParametric (context)) continue;

				XmlElement	paymentFreq	= XPath.Path (context, "paymentDates", "paymentFrequency");
				XmlElement	calcFreq	= XPath.Path (context, "calculationPeriodDates", "calculationPeriodFrequency");

				if ((paymentFreq == null) || (calcFreq == null)) continue;

				Interval payment = Interval (paymentFreq);
				Interval calc    = Interval (calcFreq);

				if ((payment == null) || (calc == null) || payment.IsMultipleOf (calc)) continue;

				errorHandler ("305", context,
					"Payment frequency '" + Interval (paymentFreq) +
					"' is not an integer multiple of calculation frequency '" +
					Interval (calcFreq) + "'",
					name, null);

				result = false;				
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule03 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule03 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule03 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule03 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (!IsParametric (context)) continue;

				XmlElement paymentDate = XPath.Path (context, "paymentDates", "firstPaymentDate");
				XmlElement	startDate = XPath.Path (context, "calculationPeriodDates", "firstRegularPeriodStartDate");
				XmlElement	endDate	  = XPath.Path (context, "calculationPeriodDates", "lastRegularPeriodEndDate");

				if (!Exists (startDate))
					startDate = XPath.Path (context, "calculationPeriodDates", "effectiveDate", "unadjustedDate");
				
				if (!Exists (endDate))
					endDate   = XPath.Path (context, "calculationPeriodDates", "terminationDate", "unadjustedDate");
				
				Interval interval	= Interval (XPath.Path (context, "calculationPeriodDates", "calculationPeriodFrequency"));
				
				if ((paymentDate == null) || (startDate == null) || (endDate == null) || (interval == null) ||
						IsUnadjustedCalculationPeriodDate (
								Date.Parse (String (paymentDate)),
								Date.Parse (String (startDate)),
								Date.Parse (String (endDate)),
								interval)) continue;
			
				errorHandler ("305", context,
					"The first payment date '" + String (paymentDate) + "' does not " +
					"fall on one of the unadjusted calculation period dates.",
					name, null);
				
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule04 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule04 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule04 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule04 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (!IsParametric (context)) continue;

				XmlElement paymentDate = XPath.Path (context, "paymentDates", "lastRegularPaymentDate");
				XmlElement	startDate = XPath.Path (context, "calculationPeriodDates", "firstRegularPeriodStartDate", "unadjustedDate");
				XmlElement	endDate	= XPath.Path (context, "calculationPeriodDates", "lastRegularPeriodStartDate", "unadjustedDate");

				if (!Exists (startDate))
					startDate = XPath.Path (context, "calculationPeriodDates", "effectiveDate", "unadjustedDate");
				
				if (!Exists (endDate))
					endDate = XPath.Path (context, "calculationPeriodDates", "terminationDate", "unadjustedDate");
				
				Interval interval	= Interval (XPath.Path (context, "calculationPeriodDates", "calculationPeriodFrequency"));
				
				if ((paymentDate == null) || (startDate == null) || (endDate == null) || (interval == null) ||
						IsUnadjustedCalculationPeriodDate (
								Date.Parse (String (paymentDate)),
								Date.Parse (String (startDate)),
								Date.Parse (String (endDate)),
								interval)) continue;
			
				errorHandler ("305", context,
					"The last regular payment date '" + String (paymentDate) + "' does not " +
					"fall on one of the unadjusted calculation period dates.",
					name, null);
				
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule05 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule05 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule05 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (!IsParametric (context)) continue;

				XmlElement	calcFreq	= XPath.Path (context, "calculationPeriodDates", "calculationPeriodFrequency");
				XmlElement	resetFreq	= XPath.Path (context, "resetDates", "resetFrequency");

				if ((calcFreq == null) || (resetFreq == null)) continue;
				
				Interval calc  = Interval (calcFreq);
				Interval reset = Interval (resetFreq);
				
				if ((calc == null) || (reset == null) || calc.IsMultipleOf (reset))
					continue;

				errorHandler ("305", context,
					"Calculation frequency '" + Interval(calcFreq) +
					"' is not an integer multiple of reset frequency '" +
					Interval (resetFreq) + "'",
					name, null);

				result = false;				
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule06 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule06 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule06 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule06 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (!IsParametric (context)) continue;

				XmlElement	payment	  = XPath.Path (context, "paymentDates", "firstPaymentDate");
				XmlElement	effective = XPath.Path (context, "calculationPeriodDates", "effectiveDate", "unadjustedDate");

				if ((payment == null) || (effective == null) || Greater (payment, effective))
					continue;

				errorHandler ("305", context,
					"The first payment date " + String (payment) + " must be after " +
					"the unadjusted effective date " + String (effective),
					name, null);
			
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule07 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule07 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule07 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule07 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (!IsParametric (context)) continue;

				XmlElement	compounding	= XPath.Path (context, "calculationPeriodAmount", "calculation", "compoundingMethod");
				XmlElement	paymentFreq	= XPath.Path (context, "paymentDates", "paymentFrequency");
				XmlElement	calcFreq	= XPath.Path (context, "calculationPeriodDates", "calculationPeriodFrequency");

				if ((paymentFreq == null) || (calcFreq == null)) continue;

				Interval payment = Interval (paymentFreq);
				Interval calc    = Interval (calcFreq);

				if ((payment == null) || (calc == null)) continue;

				if (Iff (
						Exists (compounding),
						Not (payment.Equals (calc))))
					continue;

				errorHandler ("305", context,
					"Compounding method must only be present when the payment frequency '" +
					Interval (paymentFreq) + "' is different from the calculation " +
					"frequency '" + Interval (calcFreq) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule08 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule08 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule08 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule08 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	payer	 = XPath.Path (context, "payerPartyReference");
				XmlElement	receiver = XPath.Path (context, "receiverPartyReference");

				if ((payer == null) || (receiver == null) || !payer.GetAttribute ("href").Equals (receiver.GetAttribute ("href")))
					continue;

				errorHandler ("305", context,
					"payerPartyReference/@href and receiverPartyReference/@href must " +
					"not be the same",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule09 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule09 (name, nodeIndex.GetElementsByName ("swapStream"), errorHandler)
				& Rule09 (name, nodeIndex.GetElementsByName ("capFloorStream"), errorHandler));
		}

		private static bool Rule09 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (Implies (
						Exists (XPath.Path (context, "calculationPeriodAmount", "calculation", "compoundingMethod")),
						Exists (XPath.Path (context, "resetDates"))))
					continue;

				errorHandler ("305", context,
					"calculationPeriodAmount/calculation/compoundingMethod can only be " +
					"present if a resetDates element is present",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule10 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement rollConvention	= XPath.Path (context, "calculationPeriodFrequency", "rollConvention");
				
				if (!IsNumber (String (rollConvention))) continue;
				
				XmlElement	startDate = XPath.Path (context, "firstRegularPeriodStartDate");
				if (!Exists (startDate))
					startDate = XPath.Path (context, "effectiveDate", "unadjustedDate");
				
				int		rollDate = Integer (rollConvention);
				Date	start	 = Date.Parse (String (startDate));
				
				if (rollDate < start.LastDayOfMonth) {
					if (rollDate == start.DayOfMonth) continue;
				}
				else
					if (start.IsEndOfMonth) continue;
				
				errorHandler ("305", context,
					"The start date of the calculation period,  '" + start + "' is not " +
					"consistent with the roll convention " + String (rollConvention),
					name, null);
				
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule11 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement	rollConvention	= XPath.Path (context, "calculationPeriodFrequency", "rollConvention");
				
				if (!IsNumber (String (rollConvention))) continue;
				
				XmlElement	endDate = XPath.Path (context, "firstRegularPeriodEndDate");
				if (!Exists (endDate))
					endDate = XPath.Path (context, "terminationDate", "unadjustedDate");
				
				int		rollDate = Integer (rollConvention);
				Date	end	 = Date.Parse (String (endDate));
				
				if (rollDate < end.LastDayOfMonth) {
					if (rollDate == end.DayOfMonth) continue;
				}
				else
					if (end.IsEndOfMonth) continue;
				
				errorHandler ("305", context,
					"The end date of the calculation period,  '" + end + "' is not " +
					"consistent with the roll convention " + String (rollConvention),
					name, null);
				
				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule12 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement		start	= XPath.Path (context, "firstRegularPeriodStartDate");
				XmlElement		end		= XPath.Path (context, "lastRegularPeriodEndDate");
				XmlElement		period	= XPath.Path (context, "calculationPeriodFrequency");

				if (start == null) start = XPath.Path (context, "effectiveDate", "unadjustedDate");
				if (end   == null) end   = XPath.Path (context, "terminationDate", "unadjustedDate");

				if ((start != null) && (end != null) && (period != null)) {
					Date		startDate = Date.Parse (String (start));
					Date		endDate	  = Date.Parse (String (end));
					Interval	interval  = Interval (period);

					if ((startDate == null) || (endDate == null) || (interval == null)) continue;

					if (interval.DividesDates (startDate, endDate)) continue;

					errorHandler ("305", context,
						"The calculation period '" + startDate + "' to '" + endDate +
						"' is not a multiple of the frequency '" + interval + "'",
						name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule14 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement		termination	= XPath.Path (context, "terminationDate", "unadjustedDate");
				XmlElement		effective	= XPath.Path (context, "effectiveDate", "unadjustedDate");

				if ((termination == null) || (effective == null) || Greater (termination, effective))
					continue;

				errorHandler ("305", context,
					"Unadjusted termination date '" + String (termination) + "' should " +
					"be after unadjusted effective date '" + String (effective) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule15 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement		termination	= XPath.Path (context, "terminationDate", "unadjustedDate");
				XmlElement		periodStart	= XPath.Path (context, "firstPeriodStartDate", "unadjustedDate");

				if ((termination == null) || (periodStart == null) || Greater (termination, periodStart))
					continue;

				errorHandler ("305", context,
					"Unadjusted termination date '" + String (termination) + "' should " +
					"be after unadjusted first period start date '" + String (periodStart) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule16 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement		termination	= XPath.Path (context, "terminationDate", "unadjustedDate");
				XmlElement		periodStart	= XPath.Path (context, "firstRegularPeriodStartDate");

				if ((termination == null) || (periodStart == null) || Greater (termination, periodStart))
					continue;

				errorHandler ("305", context,
					"Unadjusted termination date '" + String (termination) + "' should " +
					"be after unadjusted first regular period start date '" + String (periodStart) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule17 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement		termination	= XPath.Path (context, "terminationDate", "unadjustedDate");
				XmlElement		periodEnd	= XPath.Path (context, "lastRegularPeriodEndDate");

				if ((termination == null) || (periodEnd == null) || Greater (termination, periodEnd))
					continue;

				errorHandler ("305", context,
					"Unadjusted termination date '" + String (termination) + "' should " +
					"be after unadjusted last regular period end date '" + String (periodEnd) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule18 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement		periodEnd	= XPath.Path (context, "lastRegularPeriodEndDate");
				XmlElement		periodStart	= XPath.Path (context, "firstRegularPeriodStartDate");

				if ((periodEnd == null) || (periodStart == null) || Greater (periodEnd, periodStart))
					continue;

				errorHandler ("305", context,
					"Unadjusted last regular period end date '" + String (periodEnd) + "' should " +
					"be after unadjusted first regular period start date '" + String (periodStart) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule19 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement		periodEnd	= XPath.Path (context, "lastRegularPeriodEndDate");
				XmlElement		periodStart	= XPath.Path (context, "firstPeriodStartDate", "unadjustedDate");

				if ((periodEnd == null) || (periodStart == null) || Greater (periodEnd, periodStart))
					continue;

				errorHandler ("305", context,
					"Unadjusted last regular period end date '" + String (periodEnd) + "' should " +
					"be after unadjusted first period start date '" + String (periodStart) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule20 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement	last	  = XPath.Path (context, "lastRegularPeriodEndDate");
				XmlElement	effective = XPath.Path (context, "effectiveDate", "unadjustedDate");

				if ((last == null) || (effective == null) || Greater (last, effective))
					continue;

				errorHandler ("305", context,
					"Unadjusted last regular period end date " + String (last) +
					" must be after unadjusted effective date " + String (effective),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule21 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement	first	  = XPath.Path (context, "firstPeriodStartDate", "unadjustedDate");
				XmlElement	effective = XPath.Path (context, "effectiveDate", "unadjustedDate");

				if ((first == null) || (effective == null) || Less (first, effective))
					continue;

				errorHandler ("305", context,
					"Unadjusted first period start date " + String (first) +
					" must be before unadjusted effective date " + String (effective),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule22 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodDates")) {
				XmlElement	first	= XPath.Path (context, "firstPeriodStartDate", "unadjustedDate");
				XmlElement	regular = XPath.Path (context, "firstRegularPeriodStartDate");

				if ((first == null) || (regular == null) || Less (first, regular))
					continue;

				errorHandler ("305", context,
					"Unadjusted first period start date " + String (first) +
					" must be before first regular period start date " + String (regular),
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule23 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("stubCalculationPeriodAmount")) {
				XmlElement	datesRef	= XPath.Path (context, "calculationPeriodDatesReference");

				if (datesRef == null) continue;

				string		href		= datesRef.GetAttribute ("href");

				// Remove leading # from XPointer type references
				if ((href != null) && (href.Length > 0) && (href [0] == '#'))
					href = href.Substring (1);
				
				XmlElement	periodDates	= nodeIndex.GetElementById (href);

				if (periodDates == null) continue;

				if (Implies (
						Exists (XPath.Path (context, "initialStub")),
						Or (
							Exists (XPath.Path (periodDates, "firstPeriodStartDate")),
							Exists (XPath.Path (periodDates, "firstRegularPeriodStartDate"))))) continue;

				errorHandler ("305", context,
					"Initial stub is present but neither a first start date or first regular " +
					"period start date is defined in the referenced calculation period dates",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule24 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("stubCalculationPeriodAmount")) {
				XmlElement	datesRef	= XPath.Path (context, "calculationPeriodDatesReference");

				if (datesRef == null) continue;

				string		href		= datesRef.GetAttribute ("href");

				// Remove leading # from XPointer type references
				if ((href != null) && (href.Length > 0) && (href [0] == '#'))
					href = href.Substring (1);

				XmlElement	periodDates	= nodeIndex.GetElementById (href);

				if (periodDates == null) continue;

				if (Implies (
						Exists (XPath.Path (context, "finalStub")),
						Exists (XPath.Path (periodDates, "lastRegularPeriodEndDate")))) continue;

				errorHandler ("305", context,
					"Final stub is present but no last regular period end date is defined " +
					"in the referenced calculation period dates",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule25 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (
				  Rule25 (name, nodeIndex.GetElementsByName ("feeRateSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("floatingRateMultiplierSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("spreadSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("fixedRateSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("capRateSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("floorRateSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("feeAmountSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("knownAmountSchedule"), errorHandler)
				& Rule25 (name, nodeIndex.GetElementsByName ("notionalStepSchedule"), errorHandler));
		}

		private static bool Rule25 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
	
			foreach (XmlElement context in list) {
				if (Implies (
						Not (Exists (XPath.Path (context, "step"))),
						NotEqual (
							XPath.Path (context, "initialValue"),
							0.0M))) continue;

				errorHandler ("305", context,
					"An non-zero initial value must be provided when there are no steps " +
					"in the schedule",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule26 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool	result = true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("businessCentersReference")) {
				string	href		= context.GetAttribute ("href");

				// Handle XPointer syntax
				if ((href != null) && (href.Length > 1) && (href [0] == '#'))
					href = href.Substring (1);

				XmlElement	target	= nodeIndex.GetElementById (href);

				if ((target == null) || (target.LocalName.Equals ("businessCenters"))) continue;

				errorHandler ("305", context,
					"The businessCenterReference/@href attribute must reference a businessCenters element",
					name, context.GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule27 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule27 (name, nodeIndex.GetElementsByName ("mandatoryEarlyTermination"), errorHandler));
		}

		private static bool Rule27 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	paymentDate	= XPath.Path (context, "cashSettlement", "cashSettlementPaymentDate");

				if (Not (Exists (paymentDate))) continue;

				errorHandler ("305", context,
					"Mandatory early termination must not contain a cash settlement " +
					"payment date",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule28a (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule28a (name, nodeIndex.GetElementsByName ("mandatoryEarlyTermination"), errorHandler));
		}

		private static bool Rule28a (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	reference	= XPath.Path (context, "cashSettlement", "cashSettlementValuationDate", "dateRelativeTo");
				XmlElement	definition	= XPath.Path (context, "mandatoryEarlyTermination");

				if ((reference != null) && (definition != null)) {
					string	href	= reference.GetAttribute ("href");
					string	id		= definition.GetAttribute ("id");

					// Remove leading # from XPointer type references
					if ((href != null) && (href.Length > 0) && (href [0] == '#'))
						href = href.Substring (1);

					if ((href != null) && (id != null) && href.Equals (id)) continue;

					errorHandler ("305", context,
						"The href of the relative cash settlement valuation date must refer to " +
						"the mandatory early termination date",
						name, href);

					result = false;
				}
			}
			return (result);
		}
		// --------------------------------------------------------------------

		private static bool Rule28b (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule28b (name, nodeIndex.GetElementsByName ("mandatoryEarlyTermination"), errorHandler));
		}

		private static bool Rule28b (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	reference	= XPath.Path (context, "cashSettlement", "cashSettlementValuationDate", "dateRelativeTo");
				XmlElement	definition	= XPath.Path (context, "mandatoryEarlyTerminationDate");

				if ((reference != null) && (definition != null)) {
					string	href	= reference.GetAttribute ("href");
					string	id		= definition.GetAttribute ("id");

					if ((href != null) && (id != null) && href.Equals (id)) continue;

					errorHandler ("305", context,
						"The href of the relative cash settlement valuation date must refer to " +
						"the mandatory early termination date",
						name, href);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule29 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule29 (name, nodeIndex.GetElementsByName ("calculation"), errorHandler));
		}

		private static bool Rule29 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	compounding	= XPath.Path (context, "compoundingMethod");
				XmlElement	floating	= XPath.Path (context, "floatingRateCalculation");
				XmlElement	inflation	= XPath.Path (context, "inflationRateCalculation");

				if (Implies (
						Exists (compounding),
						Or (
							Exists (floating),
							Exists (inflation))))
					continue;

				errorHandler ("305", context,
					"The calculation element contains a compounding method but " +
					"no floating rate calculation element",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule30 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule30 (name, nodeIndex.GetElementsByName ("calculationPeriod"), errorHandler));
		}

		private static bool Rule30 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	unadjusted	= XPath.Path (context, "unadjustedStartDate");
				XmlElement	adjusted	= XPath.Path (context, "adjustedStartDate");

				if (Or (
						Exists (unadjusted),
						Exists (adjusted)))
					continue;

				errorHandler ("305", context,
					"Calculation period contains neither an adjusted nor unadjusted " +
					"start date",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule31 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule31 (name, nodeIndex.GetElementsByName ("calculationPeriod"), errorHandler));
		}

		private static bool Rule31 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	unadjusted	= XPath.Path (context, "unadjustedEndDate");
				XmlElement	adjusted	= XPath.Path (context, "adjustedEndDate");

				if (Or (
						Exists (unadjusted),
						Exists (adjusted)))
					continue;

				errorHandler ("305", context,
					"Calculation period contains neither an adjusted nor unadjusted " +
					"end date",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule32 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule32 (name, nodeIndex.GetElementsByName ("discounting"), errorHandler));
		}

		private static bool Rule32 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	rate		= XPath.Path (context, "discountRate");
				XmlElement	dayCount	= XPath.Path (context, "discountRateDayCountFraction");

				if (Implies (
						Not (Exists (rate)),
						Not (Exists (dayCount))))
					continue;

				errorHandler ("305", context,
					"Discount rate is missing but discount rate day fraction is present",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule33 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule33 (name, nodeIndex.GetElementsByName ("fra"), errorHandler));
		}

		private static bool Rule33 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	termination	= XPath.Path (context, "adjustedTerminationDate");
				XmlElement	effective	= XPath.Path (context, "adjustedEffectiveDate");

				if ((termination == null) || (effective == null) || Greater (termination, effective))
					continue;

				errorHandler ("305", context,
					"Adjusted termination date '" + String (termination) + "' must be " +
					"after adjusted effective date '" + String (effective) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule34 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule34 (name, nodeIndex.GetElementsByName ("paymentCalculationPeriod"), errorHandler));
		}

		private static bool Rule34 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	unadjusted	= XPath.Path (context, "unadjustedPaymentDate");
				XmlElement	adjusted	= XPath.Path (context, "adjustedPaymentDate");

				if (Or (
						Exists (unadjusted),
						Exists (adjusted)))
					continue;

				errorHandler ("305", context,
					"Both the unadjusted and adjusted payment date are missing from " +
					"the payment calculation period",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule35 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule35 (name, nodeIndex.GetElementsByName ("paymentDates"), errorHandler));
		}

		private static bool Rule35 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	firstDate	= XPath.Path (context, "firstPaymentDate");
				XmlElement	lastDate	= XPath.Path (context, "lastPaymentDate");

				if ((firstDate == null) || (lastDate == null) || Less (firstDate, lastDate))
					continue;

				errorHandler ("305", context,
					"The first payment date '" + String (firstDate) + "' should be " +
					"before the last payment date '" + String (lastDate) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule36 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule36 (name, nodeIndex.GetElementsByName ("paymentDates"), errorHandler));
		}

		private static bool Rule36 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	first	= XPath.Path (context, "firstPaymentDate");
				XmlElement	last	= XPath.Path (context, "lastRegularPaymentDate");
				XmlElement	period	= XPath.Path (context, "paymentFrequency");

				if ((first != null) && (last != null)) {
					Date		firstDate	= Date.Parse (String (first));
					Date		lastDate	= Date.Parse (String (last));
					Interval	frequency	= Interval (period);

					if (frequency.DividesDates (firstDate, lastDate)) continue;

					errorHandler ("305", context,
						"The first payment date and last regular payment date are not " +
						"a multiple of the payment frequency apart", name, null);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule38 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule38 (name, nodeIndex.GetElementsByName ("stubCalculationPeriodAmount"), errorHandler));
		}

		private static bool Rule38 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	initial		= XPath.Path (context, "initialStub");
				XmlElement	final		= XPath.Path (context, "finalStub");

				if (Or (
						Exists (initial),
						Exists (final)))
					continue;

				errorHandler ("305", context,
					"Both the initial and final stub are missing from the stub " +
					"calculation period amount",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule39 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule39 (name, nodeIndex.GetElementsByName ("earlyTerminationEvent"), errorHandler));
		}

		private static bool Rule39 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	exercise	= XPath.Path (context, "adjustedExerciseDate");
				XmlElement	termination	= XPath.Path (context, "adjustedEarlyTerminationDate");

				if ((exercise == null) || (termination == null) || LessOrEqual (exercise, termination))
					continue;

				errorHandler ("305", context,
					"The adjusted exercise date '" + String (exercise) + "' should be " +
					"on or before the adjusted early termination date '" + String (termination) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule40 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule40 (name, nodeIndex.GetElementsByName ("earlyTerminationEvent"), errorHandler));
		}

		private static bool Rule40 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	exercise	= XPath.Path (context, "adjustedExerciseDate");
				XmlElement	valuation	= XPath.Path (context, "adjustedCashSettlementValuationDate");

				if ((exercise == null) || (valuation == null) || LessOrEqual (exercise, valuation))
					continue;

				errorHandler ("305", context,
					"The adjusted exercise date '" + String (exercise) + "' should be " +
					"on or before the adjusted cash settlement date '" + 
					String (valuation) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule41 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule41 (name, nodeIndex.GetElementsByName ("earlyTerminationEvent"), errorHandler));
		}

		private static bool Rule41 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	valuation	= XPath.Path (context, "adjustedCashSettlementValuationDate");
				XmlElement	payment		= XPath.Path (context, "adjustedCashSettlementPaymentDate");

				if ((payment == null) || (valuation == null) || LessOrEqual (valuation, payment))
					continue;

				errorHandler ("305", context,
					"The adjusted case settlement valuation date '" + String (valuation) +
					"' should be on or before the adjusted cash settlement payment date '" + 
					String (payment) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule42 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule42 (name, nodeIndex.GetElementsByName ("extensionEvent"), errorHandler));
		}
	
		private static bool Rule42 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	exercise	= XPath.Path (context, "adjustedExerciseDate");
				XmlElement	termination	= XPath.Path (context, "adjustedExtendedTerminationDate");

				if ((exercise == null) || (termination == null) || LessOrEqual (exercise, termination))
					continue;

				errorHandler ("305", context,
					"The adjusted exercise date '" + String (exercise) + "' should be " +
					"on or before the adjusted extended termination date '" + String (termination) + "'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule43 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule43 (name, nodeIndex.GetElementsByName ("fxLinkedNotionalAmount"), errorHandler));
		}

		private static bool Rule43 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				if (Exists (DOM.GetFirstChild (context))) continue;

				errorHandler ("305", context,
					"fxLinkedNotionalAmount did not contain any elements",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule44 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule44 (name, nodeIndex.GetElementsByName ("mandatoryEarlyTerminationAdjustedDates"), errorHandler));
		}

		private static bool Rule44 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	termination	= XPath.Path (context, "adjustedEarlyTerminationDate");
				XmlElement	valuation	= XPath.Path (context, "adjustedCashSettlementValuationDate");
				XmlElement	payment		= XPath.Path (context, "adjustedCashSettlementPaymentDate");

				if ((termination == null) || (valuation == null) || (payment == null) ||
					And (
						LessOrEqual (termination, valuation),
						LessOrEqual (valuation, payment)))
					continue;

				errorHandler ("305", context,
					"The adjusted mandatory early termination date '" + String (termination) + "', " +
					"cash settlement valuation date '" + String (valuation) + "' and " +
					"cash settlement payment date '" + String (payment) + "' " +
					"are not in order",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule46A (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule46A (name, nodeIndex.GetElementsByName ("optionalEarlyTermination"), errorHandler));
		}

		private static bool Rule46A (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	reference	= XPath.Path (context, "cashSettlement", "cashSettlementValuationDate", "dateRelativeTo");
				XmlElement	definition	= XPath.Path (context, "cashSettlement");

				if ((reference == null) || (definition == null)) continue;

				string		href	= reference.GetAttribute ("href");
				string		id		= definition.GetAttribute ("id");

				// Remove leading # from XPointer type references
				if ((href != null) && (href.Length > 0) && (href [0] == '#'))
					href = href.Substring (1);

				if ((href != null) && (id != null) && Equal (href, id)) continue;

				errorHandler ("305", context,
					"dateRelativeTo element in cash settlement valuation date must " +
					"be relative to the cash settlement payment date",
					name, href);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule46B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule46B (name, nodeIndex.GetElementsByName ("optionalEarlyTermination"), errorHandler));
		}

		private static bool Rule46B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in list) {
				XmlElement	reference	= XPath.Path (context, "cashSettlement", "cashSettlementValuationDate", "dateRelativeTo");
				XmlElement	definition	= XPath.Path (context, "cashSettlement", "cashSettlementPaymentDate");

				if ((reference == null) || (definition == null)) continue;

				string		href	= reference.GetAttribute ("href");
				string		id		= definition.GetAttribute ("id");

				// Remove leading # from XPointer type references
				if ((href != null) && (href.Length > 0) && (href [0] == '#'))
					href = href.Substring (1);

				if ((href != null) && (id != null) && Equal (href, id))
					continue;

				errorHandler ("305", context,
					"dateRelativeTo element in cash settlement valuation date must " +
					"be relative to the cash settlement payment date",
					name, reference.GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule47 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule47 (name, nodeIndex.GetElementsByName ("optionalEarlyTermination"), errorHandler));
		}

		private static bool Rule47 (string name, XmlNodeList nodeList, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeList) {
				XmlElement		reference	= XPath.Path (context, "cashSettlement", "cashSettlementPaymentDate", "relativeDate", "dateRelativeTo");
				XmlElement		exercise	= XPath.Path (context, "americanExercise");

				if (exercise == null) {
					exercise = XPath.Path (context, "bermudaExercise");
					if (exercise == null)
						exercise = XPath.Path (context, "europeanExercise");
				}

				if ((reference == null) || (exercise == null)) continue;

				string		href	= reference.GetAttribute ("href");
				string		id		= exercise.GetAttribute ("id");

				// Remove leading # from XPointer type references
				if ((href != null) && (href.Length > 0) && (href [0] == '#'))
					href = href.Substring (1);

				if ((href != null) && (id != null) && Equal (href, id))
					continue;

				errorHandler ("305", context,
					"dateRelativeTo element in cash settlement payment date must " +
					"be relative to the exercise structure",
					name, reference.GetAttribute ("href"));

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule48 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule48 (name, nodeIndex.GetElementsByName ("swaption"), errorHandler));
		}

		private static bool Rule48 (string name, XmlNodeList nodeList, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeList) {
				XmlElement		reference	= XPath.Path (context, "cashSettlement", "cashSettlementPaymentDate", "relativeDate", "dateRelativeTo");
				XmlElement		exercise	= XPath.Path (context, "americanExercise");

				if (exercise == null) {
					exercise = XPath.Path (context, "bermudaExercise");
					if (exercise == null)
						exercise = XPath.Path (context, "europeanExercise");
				}

				if ((reference == null) || (exercise == null)) continue;

				string		href	= reference.GetAttribute ("href");
				string		id		= exercise.GetAttribute ("id");

				// Remove leading # from XPointer type references
				if ((href != null) && (href.Length > 0) && (href [0] == '#'))
					href = href.Substring (1);

				if ((href != null) && (id != null) && Equal (href, id))
					continue;

				errorHandler ("305", context,
					"dateRelativeTo element in cash settlement payment date must " +
					"be relative to the exercise structure",
					name, href);

				result = false;				
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule49 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Rule49 (name, nodeIndex.GetElementsByName ("resetFrequency"), errorHandler));
		}

		private static bool Rule49 (string name, XmlNodeList nodeList, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeList) {
				XmlElement	period	= XPath.Path (context, "period");

				if (Iff (
						Exists (XPath.Path (context, "weeklyRollConvention")),
						Equal (period, "W")))
					continue;

				errorHandler ("305", context,
					"weeklyRollConvention should be present if and only if the period is 'W'",
					name, null);

				result = false;
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule50 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("notionalStepSchedule")) {
				XmlNodeList	dates		= XPath.Paths (context, "step", "stepDate");
				XmlElement	calculation	= XPath.Path (context, "..", "..", "..", "..", "calculationPeriodDates");

				XmlElement	firstDate		= XPath.Path (calculation, "firstRegularPeriodStartDate");
				XmlElement	lastDate		= XPath.Path (calculation, "lastRegularPeriodEndDate");

				if (firstDate == null)
					firstDate = XPath.Path (calculation, "effectiveDate", "unadjustedDate");

				if (lastDate == null)
					lastDate  = XPath.Path (calculation, "terminationDate", "unadjustedDate");

				XmlElement	period			= XPath.Path (calculation, "calculationPeriodFrequency");
				Interval	interval		= Interval (period);

				if ((firstDate == null) || (lastDate == null) || (interval == null)) continue;

				Date		first	= Date.Parse (String (firstDate));
				Date		last	= Date.Parse (String (lastDate));

				if ((first ==null) || (last == null)) continue;

				foreach (XmlElement date in dates) {
					Date		payment = Date.Parse (String (date));

					if (IsUnadjustedCalculationPeriodDate (payment, first, last, interval)) continue;
						
					errorHandler ("305", context,
						"The notional step schedule step date '" + payment + "' does not fall " +
						"on one of the calculated period dates between '" + first + "' and '" +
						last + "'",
						name, String (date));

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule51 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("fixedRateSchedule")) {
				XmlNodeList	dates		= XPath.Paths (context, "step", "stepDate");
				XmlElement	calculation	= XPath.Path (context, "..", "..", "..", "calculationPeriodDates");


				XmlElement	firstDate		= XPath.Path (calculation, "firstRegularPeriodStartDate");
				XmlElement	lastDate		= XPath.Path (calculation, "lastRegularPeriodEndDate");

				if (firstDate == null)
					firstDate = XPath.Path (calculation, "effectiveDate", "unadjustedDate");

				if (lastDate == null)
					lastDate  = XPath.Path (calculation, "terminationDate", "unadjustedDate");

				XmlElement	period			= XPath.Path (calculation, "calculationPeriodFrequency");
				Interval	interval		= Interval (period);

				if ((firstDate == null) || (lastDate == null) || (interval == null)) continue;

				Date		first	= Date.Parse (String (firstDate));
				Date		last	= Date.Parse (String (lastDate));

				if ((first == null) || (last == null)) continue;

				foreach (XmlElement date in dates) {
					Date		payment = Date.Parse (String (date));

					if (IsUnadjustedCalculationPeriodDate (payment, first, last, interval)) continue;
						
					errorHandler ("305", context,
						"The fixed rate schedule step date '" + payment + "' does not fall " +
						"on one of the calculated period dates between '" + first + "' and '" +
						last + "'",
						name, String (date));

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule52 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("capRateSchedule")) {
				XmlNodeList	dates		= XPath.Paths (context, "step", "stepDate");
				XmlElement	calculation	= XPath.Path (context, "..", "..", "..", "..", "calculationPeriodDates");

				XmlElement	firstDate		= XPath.Path (calculation, "firstRegularPeriodStartDate");
				XmlElement	lastDate		= XPath.Path (calculation, "lastRegularPeriodEndDate");

				if (firstDate == null)
					firstDate = XPath.Path (calculation, "effectiveDate", "unadjustedDate");

				if (lastDate == null)
					lastDate  = XPath.Path (calculation, "terminationDate", "unadjustedDate");

				XmlElement	period			= XPath.Path (calculation, "calculationPeriodFrequency");
				Interval	interval		= Interval (period);

				if ((firstDate == null) || (lastDate == null) || (interval == null)) continue;

				Date		first	= Date.Parse (String (firstDate));
				Date		last	= Date.Parse (String (lastDate));

				if ((first == null) || (last == null)) continue;

				foreach (XmlElement date in dates) {
					Date		payment = Date.Parse (String (date));

					if (IsUnadjustedCalculationPeriodDate (payment, first, last, interval)) continue;
						
					errorHandler ("305", context,
						"The cap rate schedule step date '" + payment + "' does not fall " +
						"on one of the calculated period dates between '" + first + "' and '" +
						last + "'",
						name, String (date));

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule53 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("floorRateSchedule")) {
				XmlNodeList	dates		= XPath.Paths (context, "step", "stepDate");
				XmlElement	calculation	= XPath.Path (context, "..", "..", "..", "..", "calculationPeriodDates");

				XmlElement	firstDate		= XPath.Path (calculation, "firstRegularPeriodStartDate");
				XmlElement	lastDate		= XPath.Path (calculation, "lastRegularPeriodEndDate");

				if (firstDate == null)
					firstDate = XPath.Path (calculation, "effectiveDate", "unadjustedDate");

				if (lastDate == null)
					lastDate  = XPath.Path (calculation, "terminationDate", "unadjustedDate");

				XmlElement	period			= XPath.Path (calculation, "calculationPeriodFrequency");
				Interval	interval		= Interval (period);

				if ((firstDate == null) || (lastDate == null) || (interval == null)) continue;

				Date		first	= Date.Parse (String (firstDate));
				Date		last	= Date.Parse (String (lastDate));

				if ((first ==null) || (last == null)) continue;

				foreach (XmlElement date in dates) {
					Date		payment = Date.Parse (String (date));

					if (IsUnadjustedCalculationPeriodDate (payment, first, last, interval)) continue;
						
					errorHandler ("305", context,
						"The floor rate schedule step date '" + payment + "' does not fall " +
						"on one of the calculated period dates between '" + first + "' and '" +
						last + "'",
						name, String (date));

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule54 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("knownAmounSchedule")) {
				XmlNodeList	dates		= XPath.Paths (context, "step", "stepDate");
				XmlElement	calculation	= XPath.Path (context, "..", "..", "calculationPeriodDates");


				XmlElement	firstDate		= XPath.Path (calculation, "firstRegularPeriodStartDate");
				XmlElement	lastDate		= XPath.Path (calculation, "lastRegularPeriodEndDate");

				if (firstDate == null)
					firstDate = XPath.Path (calculation, "effectiveDate", "unadjustedDate");

				if (lastDate == null)
					lastDate  = XPath.Path (calculation, "terminationDate", "unadjustedDate");

				XmlElement	period			= XPath.Path (calculation, "calculationPeriodFrequency");
				Interval	interval		= Interval (period);

				if ((firstDate == null) || (lastDate == null) || (interval == null)) continue;

				Date		first	= Date.Parse (String (firstDate));
				Date		last	= Date.Parse (String (lastDate));

				if ((first ==null) || (last == null)) continue;

				foreach (XmlElement date in dates) {
					Date		payment = Date.Parse (String (date));

					if (IsUnadjustedCalculationPeriodDate (payment, first, last, interval)) continue;
						
					errorHandler ("305", context,
						"The known amount schedule step date '" + payment + "' does not fall " +
						"on one of the calculated period dates between '" + first + "' and '" +
						last + "'",
						name, String (date));

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule55 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("paymentDates")) {
				XmlElement	reference	= XPath.Path (context, "calculationPeriodDatesReference");
				XmlElement	definition	= XPath.Path (context, "..", "calculationPeriodDates");

				if ((reference != null) && (definition != null)) {
					string		href = reference.GetAttribute ("href");
					string		id	 = definition.GetAttribute ("id");

					// Remove leading # from XPointer type references
					if ((href != null) && (href.Length > 0) && (href [0] == '#'))
						href = href.Substring (1);

					if ((href != null) && (id != null) && Equal (href, id)) continue;

					errorHandler ("305", context,
						"The calculation period dates reference does not match with dates defined " +
						"in the same interest rate stream", name, href);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule56 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("paymentDates")) {
				XmlElement	reference	= XPath.Path (context, "resetDatesReference");
				XmlElement	definition	= XPath.Path (context, "..", "resetDates");

				if ((reference != null) && (definition != null)) {
					string		href = reference.GetAttribute ("href");
					string		id	 = definition.GetAttribute ("id");

					// Remove leading # from XPointer type references
					if ((href != null) && (href.Length > 0) && (href [0] == '#'))
						href = href.Substring (1);

					if ((href != null) && (id != null) && Equal (href, id)) continue;

					errorHandler ("305", context,
						"The reset dates reference does not match with dates defined " +
						"in the same interest rate stream", name, href);

					result = false;
				}
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule57 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodFrequency")) {
				XmlElement	convention	= XPath.Path (context, "rollConvention");
				XmlElement	period		= XPath.Path (context, "period");

				if ((convention == null) || (period == null) ||
					Implies (
						Not (
							Or (
								IsWeekDayName (String (convention)),
								Or (
									Equal (convention, "NONE"),
									Equal (convention, "SFE")))),
						Or (
							Equal (period, "M"),
							Equal (period, "Y"))))
					continue;

				errorHandler ("305", context,
					"Calculation period frequency roll convention '" + convention.InnerText.Trim () +
					"' is inconsistent with the calculation period '" + period.InnerText.Trim () + "'",
					name, null);
				
				result = false;	
			}
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule58 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			foreach (XmlElement context in nodeIndex.GetElementsByName ("calculationPeriodFrequency")) {
				XmlElement	convention	= XPath.Path (context, "rollConvention");
				XmlElement	period		= XPath.Path (context, "period");

				if ((convention == null) || (period == null) ||
					Implies (
						IsWeekDayName (String (convention)),
						Equal (period, "W")))
					continue;

				errorHandler ("305", context,
					"Calculation period frequency roll convention '" + convention.InnerText.Trim () +
					"' is inconsistent with the calculation period '" + period.InnerText.Trim () + "'",
					name, null);
				
				result = false;
			}
			return (result);
		}

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = new RuleSet ();

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private IrdRules ()
		{ }

		/// <summary>
		/// Determines if a string contains a number.
		/// </summary>
		/// <param name="value">The string to be tested.</param>
		/// <returns><c>true</c> if the string just contains digits.</returns>
		private static bool IsNumber (string value)
		{
			foreach (char ch in value)
				if (!((ch >= '0') && (ch <= '9'))) return (false);
			return (value.Length > 0);
		}

		/// <summary>
		/// Determines if a string value contains an abbreviated English weekday
		/// name.
		/// </summary>
		/// <param name="name">The string to be tested.</param>
		/// <returns><c>true</c> if the string matches a recognised week day value,
		/// <c>false</c> otherwise.</returns>
		private static bool IsWeekDayName (string name)
		{
			if (name.Equals ("MON")) return (true);
			if (name.Equals ("TUE")) return (true);
			if (name.Equals ("WED")) return (true);
			if (name.Equals ("THU")) return (true);
			if (name.Equals ("FRI")) return (true);
			if (name.Equals ("SAT")) return (true);
			if (name.Equals ("SUN")) return (true);

			return (false);
		}

		/// <summary>
		/// Tests if the payment date falls on a calculated date.
		/// </summary>
		/// <param name="paymentDate">The payment date.</param>
		/// <param name="startDate">The calculation period start date.</param>
		/// <param name="endDate">The calculation perion end date.</param>
		/// <param name="freq">	The period frequency.</param>
		/// <returns><c>true</c> if the payment date falls on a calculated date.</returns>
		private static bool IsUnadjustedCalculationPeriodDate (Date paymentDate, Date startDate, Date endDate, Interval freq)
		{
			while (startDate.CompareTo (endDate) <= 0) {
				if (paymentDate.Equals (startDate)) return (true);
				
				startDate = startDate.Plus (freq);
			}
			return (false);
		}

		/// <summary>
		/// Extracts an <see cref="Interval"/> from the data stored below the
		/// given context node.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <returns>An <see cref="Interval"/> constructed from the stored data.</returns>
		private static Interval Interval (XmlElement context)
		{
			try {
				return (new Interval (
					Int32.Parse (String (XPath.Path (context, "periodMultiplier"))),
					Period.ForCode (String (XPath.Path (context, "period")))));
			}
			catch (Exception) {
				return (null);
			}
		}

		/// <summary>
		/// Initialises the <see cref="RuleSet"/> with copies of all the FpML
		/// defined <see cref="Rule"/> instances for Interest Rate Derivatives.
		/// </summary>
		static IrdRules ()
		{
			Rules.Add (RULE01);
			Rules.Add (RULE02);
			Rules.Add (RULE03);
			Rules.Add (RULE04);
			Rules.Add (RULE05);
			Rules.Add (RULE06);
			Rules.Add (RULE07);
			//Rules.Add (RULE08);
			Rules.Add (RULE09);
			Rules.Add (RULE10);
			Rules.Add (RULE11);
			Rules.Add (RULE12);
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
			Rules.Add (RULE26);
			Rules.Add (RULE27);
			Rules.Add (RULE28A);
			Rules.Add (RULE28B);
			Rules.Add (RULE29);
			Rules.Add (RULE30);
			Rules.Add (RULE31);
			Rules.Add (RULE32);
			Rules.Add (RULE33);
			Rules.Add (RULE34);
			Rules.Add (RULE35);
			Rules.Add (RULE36);
			Rules.Add (RULE38);
			Rules.Add (RULE39);
			Rules.Add (RULE40);
			Rules.Add (RULE41);
			Rules.Add (RULE42);
			Rules.Add (RULE43);
			Rules.Add (RULE44);
			Rules.Add (RULE46A);
			Rules.Add (RULE46B);
			Rules.Add (RULE47);
			Rules.Add (RULE48);
			Rules.Add (RULE49);
			Rules.Add (RULE50);
			Rules.Add (RULE51);
			Rules.Add (RULE52);
			Rules.Add (RULE53);
			Rules.Add (RULE54);
			Rules.Add (RULE55);
			Rules.Add (RULE56);
			Rules.Add (RULE57);
			Rules.Add (RULE58);
		}
	}
}