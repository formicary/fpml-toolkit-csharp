// Copyright (C),2005-2011 HandCoded Software Ltd.
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
using System.Xml;

using HandCoded.Finance;
using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>FxRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the defined validation <see cref="Rule"/> instances for Foreign
	/// Exchange Products.
	/// </summary>
    /// <remarks>These rules cope with DTD based instances where no type
    /// information is available and schema based instances where it is
    /// (including where FpML renamed the types between 4.0 and 4.1).</remarks>
	public sealed class FxRules : FpMLRuleSet
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
        /// A <see cref="Precondition"/> instance that detects documents containing
        /// at least one FX single leg.
        /// </summary>
 	    private static readonly Precondition	FX_SINGLE_LEG
		    = new ContentPrecondition (
				new string [] { "fxSingleLeg" },
				new string [] { "FxSingleLeg", "FXSingleLeg" }
				);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects documents containing
        /// at least one FX swap leg.
        /// </summary>
 	    private static readonly Precondition	FX_SWAP_LEG
		    = new ContentPrecondition (
				new string [] { "nearLeg", "farLeg" },
				new string [] { "FxSwapLeg" }
				);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects documents containing
        /// at least one trade.
        /// </summary>
 	    private static readonly Precondition	TRADE
		    = new ContentPrecondition (
				new string [] { "trade" },
				new string [] { "Trade" }
				);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects documents containing
        /// at least one contract.
        /// </summary>
 	    private static readonly Precondition	CONTRACT
		    = new ContentPrecondition (
				new string [] { "contract" },
				new string [] { "Contract" }
				);
        
        /// <summary>
	    /// A <see cref="Rule"/> that ensures that the rate is positive.
		/// </summary>
        /// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE01_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-1[OLD]", new RuleDelegate (Rule01_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if forwardPoints exists then
		/// spotRate should also exist.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE02_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-2[OLD]", new RuleDelegate (Rule02_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if both forwardPoints and spotRate
		/// exist then rate equals 'spotRate + forwardRate'.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE03
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-3", new RuleDelegate (Rule03));

		/// <summary>
		/// A <see cref="Rule"/> that ensures sideRates/baseCurrency must be neither
		/// quotedCurrencyPair/currency1 nor quotedCurrencyPair/currency2.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE04_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-4[OLD]", new RuleDelegate (Rule04_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures sideRates/currency1SideRate/currency
		/// must be the same as quotedCurrencyPair/currency1.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE05_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-5[OLD]", new RuleDelegate (Rule05_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures sideRates/currency2SideRate/currency
		/// must be the same as quotedCurrencyPair/currency2.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE06_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-6[OLD]", new RuleDelegate (Rule06_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE07_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-7[OLD]", new RuleDelegate (Rule07_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE08_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-8[OLD]", new RuleDelegate (Rule08_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE08
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-8", new RuleDelegate (Rule08));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE09_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-9[OLD]", new RuleDelegate (Rule09_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE09
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-9", new RuleDelegate (Rule09));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the observation period defined by
		/// observationStartDate and observationEndDate should be an integer
		/// multiple of the calculationPeriodFrequency.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE10_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-10[OLD]", new RuleDelegate (Rule10_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the observation period defined by
		/// observationStartDate and observationEndDate should be an integer
		/// multiple of the calculationPeriodFrequency.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE10
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-10", new RuleDelegate (Rule10));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each observationDate is unique.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE11_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-11[OLD]", new RuleDelegate (Rule11_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each observationDate is unique.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE11
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-11", new RuleDelegate (Rule11));

		/// <summary>
		/// A <see cref="Rule"/> that each observationDate matches one defined
		/// by the schedule.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE12_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-12[OLD]", new RuleDelegate (Rule12_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that each observationDate matches one defined
		/// by the schedule.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE12
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-12", new RuleDelegate (Rule12));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each observationDate is unique.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE13_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-13[OLD]", new RuleDelegate (Rule13_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE14_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-14[OLD]", new RuleDelegate (Rule14_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE14
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-14", new RuleDelegate (Rule14));

		/// <summary>
		/// A <see cref="Rule"/> that ensures spotRate is positive if it exists.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x</remarks>
		public static readonly Rule	RULE15_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-15[OLD]", new RuleDelegate (Rule15_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures spotRate is positive if it exists.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x</remarks>
		public static readonly Rule	RULE16_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-16[OLD]", new RuleDelegate (Rule16_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE17_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-17[OLD]", new RuleDelegate (Rule17_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures payer and receiver of an FxLeg are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE18_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-18[OLD]", new RuleDelegate (Rule18_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures payer and receiver of an FxSingleLeg are correct.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE18
			= new DelegatedRule (Precondition.And (FX_SINGLE_LEG, Preconditions.R5_1__LATER), "fx-18", new RuleDelegate (Rule18));

		/// <summary>
		/// A <see cref="Rule"/> that ensures exchanged currencies in an FxLeg are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE19_OLD
			= new DelegatedRule (Precondition.And (FX_SINGLE_LEG, Preconditions.R3_0__R4_X), "fx-19[OLD]", new RuleDelegate (Rule19_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures exchanged currencies in an FxLeg are different.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE19
			= new DelegatedRule (Precondition.And (FX_SINGLE_LEG, Preconditions.R5_1__LATER), "fx-19", new RuleDelegate (Rule19));

		/// <summary>
		/// A <see cref="Rule"/> that ensures split settlement dates are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE20_OLD
			= new DelegatedRule (Precondition.And (FX_SINGLE_LEG, Preconditions.R3_0__R4_X), "fx-20[OLD]", new RuleDelegate (Rule20_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures split settlement dates are different.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE20
			= new DelegatedRule (Precondition.And (FX_SINGLE_LEG, Preconditions.R5_1__LATER), "fx-20", new RuleDelegate (Rule20));

		/// <summary>
		/// A <see cref="Rule"/> that ensures non-deliverable forwards contains
		/// a specification of the forward points.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE21_OLD
			= new DelegatedRule (Precondition.And (FX_SINGLE_LEG, Preconditions.R3_0__R4_X), "fx-21[OLD]", new RuleDelegate (Rule21_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures non-deliverable forwards contains
		/// a specification of the forward points.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE21
			= new DelegatedRule (Precondition.And (FX_SINGLE_LEG, Preconditions.R5_1__LATER), "fx-21", new RuleDelegate (Rule21));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE22_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-22[OLD]", new RuleDelegate (Rule22_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE22
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-22", new RuleDelegate (Rule22));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the put and call currencies are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE23_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-23[OLD]", new RuleDelegate (Rule23_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the put and call currencies are different.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE23
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-23", new RuleDelegate (Rule23));

		/// <summary>
		/// A <see cref="Rule"/> that ensures rate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE24_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-24[OLD]", new RuleDelegate (Rule24_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures two or more legs are present.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE25_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-25[OLD]", new RuleDelegate (Rule25_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if two legs are present they must have
		/// different value dates.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE26_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-26[OLD]", new RuleDelegate (Rule26_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that value date of the near leg of an
        /// FX swap is before that of the far leg.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE26
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-26", new RuleDelegate (Rule26));

		/// <summary>
		/// A <see cref="Rule"/> that ensures two different currencies are specified.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE27
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-27", new RuleDelegate (Rule27));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE28_OLD
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-28[OLD]", new RuleDelegate (Rule28_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if forwardPoints exists then
		/// spotRate should also exist.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE29_OLD
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-29[OLD]", new RuleDelegate (Rule29_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if both forwardPoints and spotRate
		/// exist then rate equals 'spotRate + forwardRate'.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE30_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-30[OLD]", new RuleDelegate (Rule30_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if both forwardPoints and spotRate
		/// exist then rate equals 'spotRate + forwardRate'.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE30
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-30", new RuleDelegate (Rule30));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that side rates are obtained relative
		/// to another currency.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE31_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-31[OLD]", new RuleDelegate (Rule31_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the initial payer and reciever
		/// are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE32_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-32[OLD]", new RuleDelegate (Rule32_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the initial payer and reciever
		/// are different.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE32
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-32", new RuleDelegate (Rule32));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the maturity date is after the start date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE33
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-33", new RuleDelegate (Rule33));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the principal amount is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE34_OLD
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-34[OLD]", new RuleDelegate (Rule34_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the fixed rate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE35_OLD
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-35[OLD]", new RuleDelegate (Rule35_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE36_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-36[OLD]", new RuleDelegate (Rule36_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE36
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R5_1__LATER), "fx-36", new RuleDelegate (Rule36));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 up to 4.x.</remarks>
		public static readonly Rule RULE36B_OLD
			= new DelegatedRule (Precondition.And (CONTRACT, Preconditions.R4_2__R4_X), "fx-36b[OLD]", new RuleDelegate (Rule36B_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE37_OLD
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R3_0__R4_X), "fx-37[OLD]", new RuleDelegate (Rule37_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 up to 4.x.</remarks>
		public static readonly Rule RULE37B_OLD
			= new DelegatedRule (Precondition.And (CONTRACT, Preconditions.R4_2__R4_X), "fx-37b[OLD]", new RuleDelegate (Rule37B_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE38_OLD
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R3_0__R4_X), "fx-38[OLD]", new RuleDelegate (Rule38_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE38
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R5_1__LATER), "fx-38", new RuleDelegate (Rule38));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 up to 4.x.</remarks>
		public static readonly Rule RULE38B_OLD
			= new DelegatedRule (Precondition.And (CONTRACT, Preconditions.R4_2__R4_X), "fx-38b[OLD]", new RuleDelegate (Rule38B_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures value date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE39
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R3_0__LATER), "fx-39", new RuleDelegate (Rule39));

		/// <summary>
		/// A <see cref="Rule"/> that ensures value date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule RULE39B_OLD
			= new DelegatedRule (Precondition.And (CONTRACT, Preconditions.R4_2__R4_X), "fx-39b[OLD]", new RuleDelegate (Rule39B_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that all FX swap value dates are after the
		/// trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE40_OLD
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R3_0__R4_X), "fx-40[OLD]", new RuleDelegate (Rule40_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that all FX swap value dates are after the
		/// trade date.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE40
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R5_1__LATER), "fx-40", new RuleDelegate (Rule40));

		/// <summary>
		/// A <see cref="Rule"/> that all FX swap value dates are after the
		/// contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule RULE40B_OLD
			= new DelegatedRule (Precondition.And (CONTRACT, Preconditions.R4_2__R4_X), "fx-40b[OLD]", new RuleDelegate (Rule40B_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE41_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-41[OLD]", new RuleDelegate (Rule41_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each averageRateObservationDate/observationDate
		/// is unique.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE42_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-42[OLD]", new RuleDelegate (Rule42_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the put and call currencies are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE43_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-43[OLD]", new RuleDelegate (Rule43_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE44_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-44[OLD]", new RuleDelegate (Rule44_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE45_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-45[OLD]", new RuleDelegate (Rule45_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE45
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-45", new RuleDelegate (Rule45));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the side rates definition for currency1
		/// uses an appropriate basis. 
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE46_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-46[OLD]", new RuleDelegate (Rule46_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 4.x.</remarks>
		public static readonly Rule	RULE47_OLD
			= new DelegatedRule (Preconditions.R3_0__R4_X, "fx-47[OLD]", new RuleDelegate (Rule47_OLD));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if one rateObservation/rate exists,
        /// then rateObservationQuoteBasis must exist.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE48
			= new DelegatedRule (Preconditions.R5_1__LATER, "fx-48", new RuleDelegate (Rule48));

		/// <summary>
		/// A <see cref="Rule"/> that ensures two different currencies are used in
        /// each FxSwapLeg.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE49
			= new DelegatedRule (Precondition.And (FX_SWAP_LEG, Preconditions.R5_1__LATER), "fx-49", new RuleDelegate (Rule49));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if split settlement is specified then
        /// the settlement dates must be different.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE50
			= new DelegatedRule (Precondition.And (FX_SWAP_LEG, Preconditions.R5_1__LATER), "fx-50", new RuleDelegate (Rule50));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if cash settlement is specified the
	    /// deal must be a forward.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE51
			= new DelegatedRule (Precondition.And (FX_SWAP_LEG, Preconditions.R5_1__LATER), "fx-51", new RuleDelegate (Rule51));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the expiry date of an American option
	    /// falls after the trade date.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE52
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R5_1__LATER), "fx-52", new RuleDelegate (Rule52));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the expiry date of an American option
	    /// falls after the trade date.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE53
			= new DelegatedRule (Precondition.And (TRADE, Preconditions.R5_1__LATER), "fx-53", new RuleDelegate (Rule53));

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = RuleSet.ForName ("FxRules");

		/// <summary>
		/// Ensures no instances can be created.
		/// </summary>
		private FxRules ()
		{ }

		// --------------------------------------------------------------------

        private static bool Rule01_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule01_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule01_OLD (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
        }

        private static bool Rule01_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
            bool        result  = true;

			foreach (XmlElement context in list) {
				XmlElement		rate	= XPath.Path (context, "rate");
				
				if ((rate == null) || IsPositive (rate)) continue;
				
				errorHandler ("305", context,
						"The rate must be positive",
                        name, ToToken (rate));
				
				result = false;
			}

            return (result);
        }

		// --------------------------------------------------------------------

		private static bool Rule02_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule02_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule02_OLD (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule02_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement	context in list) {
				XmlElement 	forward = XPath.Path (context, "forwardPoints");
				XmlElement	spot	= XPath.Path (context, "spotRate");
				
				if (!((forward != null) && (spot == null))) continue;
				
				errorHandler ("305", context,
						"If forwardPoints exists then spotRate should also exist.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule03 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule03 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule03 (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule03 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement	context in list) {
				XmlElement 	forward = XPath.Path (context, "forwardPoints");
				XmlElement	spot	= XPath.Path (context, "spotRate");
				XmlElement	rate	= XPath.Path (context, "rate");
				
				if ((rate == null) || (forward == null) || (spot == null)) continue;
				
				if (ToDecimal (rate).Equals (ToDecimal (spot) + ToDecimal (forward)))
					continue;
				
				errorHandler ("305", context,
						"Sum of spotRate and forwardPoints does not equal rate.",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule04_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule04_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule04_OLD (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule04_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement 	baseCcy = XPath.Path (context, "sideRates", "baseCurrency");
				XmlElement	ccy1	= XPath.Path (context, "quotedCurrencyPair", "currency1");
				XmlElement	ccy2	= XPath.Path (context, "quotedCurrencyPair", "currency2");
				
				if ((baseCcy == null) || (ccy2 == null) || (ccy2 == null)) continue;
				
				if (Equal (baseCcy, ccy1) || Equal (baseCcy, ccy2)) {
					errorHandler ("305", context,
							"The side rate base currency must not be one of the trade currencies.",
							name, ToToken (baseCcy));
				
					result = false;
				}
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule05_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule05_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule05_OLD (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule05_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy		= XPath.Path (context, "quotedCurrencyPair", "currency1");
				XmlElement 	ccy1 	= XPath.Path (context, "sideRates", "currency1SideRate", "currency");
				
				if ((ccy == null) || (ccy1 == null) || Equal (ccy, ccy1)) continue;
				
				errorHandler ("305", context,
						"The side rate currency1 '" + ToToken (ccy1) +
						"' must be the same as trade currency1 '" + ToToken (ccy) + "'.",
						name, null);
			
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule06_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule06_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule06_OLD (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule06_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy		= XPath.Path (context, "quotedCurrencyPair", "currency2");
				XmlElement 	ccy1 	= XPath.Path (context, "sideRates", "currency2SideRate", "currency");
				
				if ((ccy == null) || (ccy1 == null) || Equal (ccy, ccy1)) continue;
				
				errorHandler ("305", context,
						"The side rate currency2 '" + ToToken (ccy1) +
						"' must be the same as trade currency2 '" + ToToken (ccy) + "'.",
						name, null);
			
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule07_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule07_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAmericanTrigger"), errorHandler)
					& Rule07_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAmericanTrigger"), errorHandler));					
				
			return (
				  Rule07_OLD (name, nodeIndex.GetElementsByName ("fxAmericanTrigger"), errorHandler));
		}
		
		private static bool Rule07_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		rate	= XPath.Path (context, "triggerRate");
				
				if ((rate == null) || IsPositive (rate)) continue;
									
				errorHandler ("305", context,
						"The trigger rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule08_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule08_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAmericanTrigger"), errorHandler)
					& Rule08_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAmericanTrigger"), errorHandler));					
				
			return (
				  Rule08_OLD (name, nodeIndex.GetElementsByName ("fxAmericanTrigger"), errorHandler));
		}
		
		private static bool Rule08_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "observationStartDate");
				XmlElement	end		= XPath.Path (context, "observationEndDate");
				
				if ((start == null) || (end == null) || 
					LessOrEqual (ToDate (start), ToDate (end))) continue;
									
				errorHandler ("305", context,
						"The observationStartDate must not be after the observationEndDate",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule08 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule08 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxTouch"), errorHandler));					
				
			return (
				  Rule08 (name, nodeIndex.GetElementsByName ("touch"), errorHandler));
		}
		
		private static bool Rule08 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "observationStartDate");
				XmlElement	end		= XPath.Path (context, "observationEndDate");
				
				if ((start == null) || (end == null) || 
					LessOrEqual (ToDate (start), ToDate (end))) continue;
									
				errorHandler ("305", context,
						"The observationStartDate must not be after the observationEndDate",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule09_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule09_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateObservationSchedule"), errorHandler)
					& Rule09_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
				
			return (
				  Rule09_OLD (name, nodeIndex.GetElementsByName ("averageRateObservationSchedule"), errorHandler));
		}
		
		private static bool Rule09_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "observationStartDate");
				XmlElement	end		= XPath.Path (context, "observationEndDate");
				
				if ((start == null) || (end == null) || 
					LessOrEqual (ToDate (start), ToDate (end))) continue;
									
				errorHandler ("305", context,
						"The observationStartDate must not be after the observationEndDate",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule09 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule09 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
				
			return (
				  Rule09 (name, nodeIndex.GetElementsByName ("averageRateObservationSchedule"), errorHandler));
		}
		
		private static bool Rule09 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "startDate");
				XmlElement	end		= XPath.Path (context, "endDate");
				
				if ((start == null) || (end == null) || 
					LessOrEqual (ToDate (start), ToDate (end))) continue;
									
				errorHandler ("305", context,
						"The startDate must not be after the endDate",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule10_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule10_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateObservationSchedule"), errorHandler)
					& Rule10_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
				
			return (
				  Rule10_OLD (name, nodeIndex.GetElementsByName ("averageRateObservationSchedule"), errorHandler));
		}
		
		private static bool Rule10_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "observationStartDate");
				XmlElement	end		= XPath.Path (context, "observationEndDate");
				XmlElement	period	= XPath.Path (context, "calculationPeriodFrequency");
				
				if ((start == null) || (end == null) || (period == null) ||
						ToInterval (period).DividesDates(ToDate (start), ToDate (end))) continue;
							
				errorHandler ("305", context,
						"The observation period is not a multiple of the calculationPeriodFrequency",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule10 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule10 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
				
			return (
				  Rule10 (name, nodeIndex.GetElementsByName ("averageRateObservationSchedule"), errorHandler));
		}
		
		private static bool Rule10 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "startDate");
				XmlElement	end		= XPath.Path (context, "endDate");
				XmlElement	period	= XPath.Path (context, "calculationPeriodFrequency");
				
				if ((start == null) || (end == null) || (period == null) ||
						ToInterval (period).DividesDates(ToDate (start), ToDate (end))) continue;
							
				errorHandler ("305", context,
						"The observation period is not a multiple of the calculationPeriodFrequency",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule11_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule11_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule11_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule11_OLD (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule11_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	nodes	= XPath.Paths (context, "observedRates", "observationDate");
				
				int			limit	= nodes.Count;
				Date []		dates	= new Date [limit];
				
				for (int count = 0; count < limit; ++count)
					dates [count] = ToDate (nodes [count]);					
				
				for (int outer = 0; outer < (limit - 1); ++outer) {
					for (int inner = outer + 1; inner < limit; ++inner) {
						if (Equal (dates [outer], dates [inner]))
							errorHandler ("305", nodes [inner],
									"Duplicate observation date",
									name, ToToken (nodes [inner]));
						
						result = false;
					}
				}
				dates = null;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule11 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule11 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAsianFeature"), errorHandler));					
				
			return (
				  Rule11 (name, nodeIndex.GetElementsByName ("asian"), errorHandler));
		}
		
		private static bool Rule11 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	nodes	= XPath.Paths (context, "rateObservation", "date");
				
				int			limit	= nodes.Count;
				Date []		dates	= new Date [limit];
				
				for (int count = 0; count < limit; ++count)
					dates [count] = ToDate (nodes [count]);					
				
				for (int outer = 0; outer < (limit - 1); ++outer) {
					for (int inner = outer + 1; inner < limit; ++inner) {
						if (Equal (dates [outer], dates [inner]))
							errorHandler ("305", nodes [inner],
									"Duplicate observation date",
									name, ToToken (nodes [inner]));
						
						result = false;
					}
				}
				dates = null;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule12_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule12_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule12_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule12_OLD (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule12_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	schedule	= XPath.Path (context, "averageRateObservationSchedule");
				
				if (schedule == null) continue;
				
				XmlElement	start	= XPath.Path (schedule, "observationStartDate");
				XmlElement	end		= XPath.Path (schedule, "observationEndDate");
				XmlElement	freq	= XPath.Path (schedule, "calculationPeriodFrequency");
				XmlElement	roll	= XPath.Path (freq, "rollConvention");
				
				if ((start == null) || (end == null) || (freq == null) || (roll == null)) continue;
				
				Date [] 	dates	= GenerateSchedule (ToDate (start), ToDate (end),
						ToInterval (freq), DateRoll.ForName (ToToken (roll)), null);
				
				XmlNodeList	nodes	= XPath.Paths (context, "observedRates", "observationDate");
									
				foreach (XmlElement observed in nodes) {
					Date		date 	 = ToDate (observed);
					
					bool		found = false;
					foreach (Date match in dates) {
						if (Equal (date, match)) {
							found = true;
							break;
						}
					}
					
					if (!found) {
						errorHandler ("305", observed,
								"Observation date '" + ToToken (observed) +
								"' does not match with the schedule.",
								name, ToToken (observed));
						
						result = false;
					}
				}
				dates = null;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule12 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule12 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAsianFeature"), errorHandler));					
				
			return (
				  Rule12 (name, nodeIndex.GetElementsByName ("asian"), errorHandler));
		}
		
		private static bool Rule12 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	schedule	= XPath.Path (context, "observationSchedule");
				
				if (schedule == null) continue;
				
				XmlElement	start	= XPath.Path (schedule, "observationStartDate");
				XmlElement	end		= XPath.Path (schedule, "observationEndDate");
				XmlElement	freq	= XPath.Path (schedule, "calculationPeriodFrequency");
				XmlElement	roll	= XPath.Path (freq, "rollConvention");
				
				if ((start == null) || (end == null) || (freq == null) || (roll == null)) continue;
				
				Date [] 	dates	= GenerateSchedule (ToDate (start), ToDate (end),
						ToInterval (freq), DateRoll.ForName (ToToken (roll)), null);
				
				XmlNodeList	nodes	= XPath.Paths (context, "observedRates", "observationDate");
									
				foreach (XmlElement observed in nodes) {
					Date		date 	 = ToDate (observed);
					
					bool		found = false;
					foreach (Date match in dates) {
						if (Equal (date, match)) {
							found = true;
							break;
						}
					}
					
					if (!found) {
						errorHandler ("305", observed,
								"Observation date '" + ToToken (observed) +
								"' does not match with the schedule.",
								name, ToToken (observed));
						
						result = false;
					}
				}
				dates = null;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule13_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule13_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule13_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule13_OLD (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule13_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	schedule	= XPath.Paths (context, "averageRateObservationDate", "observationDate");
				int			limit		= (schedule != null) ? schedule.Count : 0;
				
				if (limit == 0) continue;
				
				Date []		dates	= new Date [limit];
				
				for (int count = 0; count < limit; ++count)
					dates [count] = ToDate (schedule [count]);					
				
				XmlNodeList	nodes	= XPath.Paths (context, "observedRates", "observationDate");
									
				foreach (XmlElement observed in nodes) {
					Date		date 	 = ToDate (observed);
					
					bool		found = false;
					for (int match = 0; match < dates.Length; ++match) {
						if (Equal (date, dates [match])) {
							found = true;
							break;
						}
					}
					
					if (!found) {
						errorHandler ("305", observed,
								"Observation date '" + ToToken (observed) +
								"' does not match with a defined observationDate.",
								name, ToToken (observed));
						
						result = false;
					}
				}
				dates = null;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule14_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule14_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXBarrier"), errorHandler)
					& Rule14_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrier"), errorHandler));					
				
			return (
				  Rule14_OLD (name, nodeIndex.GetElementsByName ("fxBarrier"), errorHandler));
		}
		
		private static bool Rule14_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "observationStartDate");
				XmlElement	end		= XPath.Path (context, "observationEndDate");
				
				if ((start == null) || (end == null) || 
					LessOrEqual (ToDate (start), ToDate (end))) continue;
									
				errorHandler ("305", context,
						"The observationStartDate must not be after the observationEndDate",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule14 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule14 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrierFeature"), errorHandler));					
				
			return (
				  Rule14 (name, nodeIndex.GetElementsByName ("barrier"), errorHandler));
		}
		
		private static bool Rule14 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	= XPath.Path (context, "observationStartDate");
				XmlElement	end		= XPath.Path (context, "observationEndDate");
				
				if ((start == null) || (end == null) || 
					LessOrEqual (ToDate (start), ToDate (end))) continue;
									
				errorHandler ("305", context,
						"The observationStartDate must not be after the observationEndDate",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule15_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule15_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXBarrierOption"), errorHandler)
					& Rule15_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrierOption"), errorHandler));					
				
			return (
				  Rule15_OLD (name, nodeIndex.GetElementsByName ("fxBarrierOption"), errorHandler));
		}
		
		private static bool Rule15_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	rate	= XPath.Path (context, "spotRate");
				
				if ((rate == null) || IsPositive (rate)) continue;
									
				errorHandler ("305", context,
						"The spot rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule16_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule16_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXDigitalOption"), errorHandler)
					& Rule16_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxDigitalOption"), errorHandler));					
				
			return (
				  Rule16_OLD (name, nodeIndex.GetElementsByName ("fxDigitalOption"), errorHandler));
		}
		
		private static bool Rule16_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	rate	= XPath.Path (context, "spotRate");
				
				if ((rate == null) || IsPositive (rate)) continue;
									
				errorHandler ("305", context,
						"The spot rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule17_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule17_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXEuropeanTrigger"), errorHandler)
					& Rule17_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxEuropeanTrigger"), errorHandler));					
				
			return (
				  Rule17_OLD (name, nodeIndex.GetElementsByName ("fxEuropeanTrigger"), errorHandler));
		}
		
		private static bool Rule17_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	rate	= XPath.Path (context, "triggerRate");
				
				if ((rate == null) || IsPositive (rate)) continue;
									
				errorHandler ("305", context,
						"The trigger rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule18_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule18_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule18_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
			return (
				  Rule18_OLD (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule18_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy1Pay	= XPath.Path (context, "exchangedCurrency1", "payerPartyReference");
				XmlElement	ccy1Rec	= XPath.Path (context, "exchangedCurrency1", "receiverPartyReference");
				XmlElement	ccy2Pay	= XPath.Path (context, "exchangedCurrency2", "payerPartyReference");
				XmlElement	ccy2Rec	= XPath.Path (context, "exchangedCurrency2", "receiverPartyReference");
				
				if ((ccy1Pay == null) || (ccy1Rec == null) ||
					(ccy2Pay == null) || (ccy2Rec == null)) continue;
				
				if (Equal (ccy1Pay.GetAttribute("href"), ccy2Rec.GetAttribute("href")) &&
					Equal (ccy2Pay.GetAttribute("href"), ccy1Rec.GetAttribute("href"))) continue;
									
				errorHandler ("305", context,
						"Exchanged currency payers and receivers don't match.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule18 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule18 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSingleLeg"), errorHandler));					
				
			return (
				  Rule18 (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule18 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		ccy1PayPty	= XPath.Path (context, "exchangedCurrency1", "payerPartyReference");
				XmlElement		ccy1RecPty	= XPath.Path (context, "exchangedCurrency1", "receiverPartyReference");
				XmlElement		ccy2PayPty	= XPath.Path (context, "exchangedCurrency2", "payerPartyReference");
				XmlElement		ccy2RecPty	= XPath.Path (context, "exchangedCurrency2", "receiverPartyReference");
				
				if ((ccy1PayPty == null) || (ccy1RecPty == null) ||
					(ccy2PayPty == null) || (ccy2RecPty == null)) continue;

				XmlElement		ccy1PayAcc	= XPath.Path (context, "exchangedCurrency1", "payerAccountReference");
				XmlElement		ccy1RecAcc	= XPath.Path (context, "exchangedCurrency1", "receiverAccountReference");
				XmlElement		ccy2PayAcc	= XPath.Path (context, "exchangedCurrency2", "payerAccountReference");
				XmlElement		ccy2RecAcc	= XPath.Path (context, "exchangedCurrency2", "receiverAccountReference");
				
				if (Equal (DOM.GetAttribute (ccy1PayPty, "href"), DOM.GetAttribute (ccy2RecPty, "href")) &&
					Equal (DOM.GetAttribute (ccy2PayPty, "href"), DOM.GetAttribute (ccy1RecPty, "href")) &&
					((!Exists (ccy1PayAcc) && !Exists (ccy2RecAcc)) || Equal (DOM.GetAttribute (ccy1PayAcc, "href"), DOM.GetAttribute (ccy2RecAcc, "href"))) &&
					((!Exists (ccy2PayAcc) && !Exists (ccy1RecAcc)) || Equal (DOM.GetAttribute (ccy2PayAcc, "href"), DOM.GetAttribute (ccy1RecAcc, "href"))))
					continue;
									
				errorHandler ("305", context,
						"Exchanged currency payers and receivers don't match.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

        // --------------------------------------------------------------------

		private static bool Rule19_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule19_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule19_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
			return (
				  Rule19_OLD (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule19_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy1	= XPath.Path (context, "exchangedCurrency1", "paymentAmount", "currency");
				XmlElement	ccy2	= XPath.Path (context, "exchangedCurrency2", "paymentAmount", "currency");
				
				if ((ccy1 == null) || (ccy2 == null) || !IsSameCurrency (ccy1, ccy2)) continue;
									
				errorHandler ("305", context,
						"Exchanged currencies must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

        // --------------------------------------------------------------------

		private static bool Rule19 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule19 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSingleLeg"), errorHandler));					
				
			return (
				  Rule19 (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule19 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy1	= XPath.Path (context, "exchangedCurrency1", "paymentAmount", "currency");
				XmlElement	ccy2	= XPath.Path (context, "exchangedCurrency2", "paymentAmount", "currency");
				
				if ((ccy1 == null) || (ccy2 == null) || !IsSameCurrency (ccy1, ccy2)) continue;
									
				errorHandler ("305", context,
						"Exchanged currencies must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------
		
		private static bool Rule20_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule20_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule20_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
			return (
				  Rule20_OLD (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule20_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement	context in list) {
				XmlElement	date1	= XPath.Path (context, "currency1ValueDate");
				XmlElement	date2	= XPath.Path (context, "currency2ValueDate");
				
				if ((date1 == null) || (date2 == null) ||
					NotEqual (ToDate (date1), ToDate (date2))) continue;
									
				errorHandler ("305", context,
						"currency1ValueDate and currency2ValueDate must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------
		
		private static bool Rule20 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule20 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSingleLeg"), errorHandler));					
				
			return (
				  Rule20 (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule20 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement	context in list) {
				XmlElement	date1	= XPath.Path (context, "currency1ValueDate");
				XmlElement	date2	= XPath.Path (context, "currency2ValueDate");
				
				if ((date1 == null) || (date2 == null) ||
					NotEqual (ToDate (date1), ToDate (date2))) continue;
									
				errorHandler ("305", context,
						"currency1ValueDate and currency2ValueDate must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule21_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule21_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule21_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
			return (
				  Rule21_OLD (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule21_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ndf		= XPath.Path (context, "nonDeliverableForward");
				XmlElement	fwd		= XPath.Path (context, "exchangeRate", "forwardPoints");
				
				if ((ndf == null) || (fwd != null)) continue;
				
				errorHandler ("305", context,
						"Non-deliverable forward does not specify forwardPoints.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule21 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule21 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSingleLeg"), errorHandler));					
				
			return (
				  Rule21 (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule21 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ndf		= XPath.Path (context, "nonDeliverableSettlement");
				XmlElement	fwd		= XPath.Path (context, "exchangeRate", "forwardPoints");
				
				if ((ndf == null) || (fwd != null)) continue;
				
				errorHandler ("305", context,
						"Non-deliverable forward does not specify forwardPoints.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule22_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule22_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXOptionLeg"), errorHandler)
					& Rule22_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxOptionLeg"), errorHandler));					
				
			return (
				  Rule22_OLD (name, nodeIndex.GetElementsByName ("fxSimpleOption"), errorHandler)
				& Rule22_OLD (name, nodeIndex.GetElementsByName ("fxBarrierOption"), errorHandler));
		}
		
		private static bool Rule22_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	buyer	 = XPath.Path (context, "buyerPartyReference");
				XmlElement	seller	 = XPath.Path (context, "sellerPartyReference");
				XmlElement	payer	 = XPath.Path (context, "fxOptionPremium", "payerPartyReference");
				XmlElement	receiver = XPath.Path (context, "fxOptionPremium", "receiverPartyReference");
				
				if ((buyer == null) || (seller == null) ||
					(payer == null) || (receiver == null)) continue;
				
				if (Equal (buyer.GetAttribute("href"), payer.GetAttribute("href")) &&
					Equal (seller.GetAttribute("href"), receiver.GetAttribute("href"))) continue;
									
				errorHandler ("305", context,
						"Premium payer and receiver don't match with option buyer and seller.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
		
		// --------------------------------------------------------------------

		private static bool Rule22 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule22 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxOption"), errorHandler));					
				
			return (
				  Rule22 (name, nodeIndex.GetElementsByName ("fxOption"), errorHandler));
		}
		
		private static bool Rule22 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	buyer	 = XPath.Path (context, "buyerPartyReference");
				XmlElement	seller	 = XPath.Path (context, "sellerPartyReference");
				XmlElement	payer	 = XPath.Path (context, "premium", "payerPartyReference");
				XmlElement	receiver = XPath.Path (context, "premium", "receiverPartyReference");
				
				if ((buyer != null) && (seller != null) &&
					(payer != null) && (receiver != null)) {

                    if (Equal (buyer.GetAttribute("href"), seller.GetAttribute("href"))) {
				        XmlElement	buyerAccount	 = XPath.Path (context, "buyerAccountReference");
				        XmlElement	sellerAccount	 = XPath.Path (context, "sellerAccountReference");
				        XmlElement	payerAccount	 = XPath.Path (context, "premium", "payerAccountReference");
				        XmlElement	receiverAccount  = XPath.Path (context, "premium", "receiverAccountReference");

				        if ((buyerAccount != null) && (sellerAccount != null) &&
                            Equal (buyer.GetAttribute("href"), payer.GetAttribute("href")) &&
					        Equal (seller.GetAttribute("href"), receiver.GetAttribute("href"))&&
                            Equal (buyerAccount.GetAttribute("href"), payerAccount.GetAttribute("href")) &&
					        Equal (sellerAccount.GetAttribute("href"), receiverAccount.GetAttribute("href"))) continue;
                    }
                    else	
				        if (Equal (buyer.GetAttribute("href"), payer.GetAttribute("href")) &&
					        Equal (seller.GetAttribute("href"), receiver.GetAttribute("href"))) continue;
                }
		
				errorHandler ("305", context,
						"Premium payer and receiver don't match with option buyer and seller.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
		
		// --------------------------------------------------------------------

		private static bool Rule23_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule23_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXOptionLeg"), errorHandler)
					& Rule23_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxOptionLeg"), errorHandler));					
				
			return (
				  Rule23_OLD (name, nodeIndex.GetElementsByName ("fxSimpleOption"), errorHandler)
				& Rule23_OLD (name, nodeIndex.GetElementsByName ("fxBarrierOption"), errorHandler));
		}
		
		private static bool Rule23_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy1	= XPath.Path (context, "putCurrencyAmount", "currency");
				XmlElement	ccy2	= XPath.Path (context, "callCurrencyAmount", "currency");
				
				if ((ccy1 == null) || (ccy2 == null) || !IsSameCurrency (ccy1, ccy2)) continue;
				
				errorHandler ("305", context,
						"Put and call currencies must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule23 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule23 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxOption"), errorHandler));					
				
			return (
				  Rule23 (name, nodeIndex.GetElementsByName ("fxOption"), errorHandler));
		}
		
		private static bool Rule23 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy1	= XPath.Path (context, "putCurrencyAmount", "currency");
				XmlElement	ccy2	= XPath.Path (context, "callCurrencyAmount", "currency");
				
				if ((ccy1 == null) || (ccy2 == null) || !IsSameCurrency (ccy1, ccy2)) continue;
				
				errorHandler ("305", context,
						"Put and call currencies must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule24_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule24_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXStrikePrice"), errorHandler)
					& Rule24_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxStrikePrice"), errorHandler));					
				
			return (
				  Rule24_OLD (name, nodeIndex.GetElementsByName ("fxStrikePrice"), errorHandler));
		}
		
		private static bool Rule24_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	rate	= XPath.Path (context, "rate");
				
				if ((rate == null) || IsPositive (rate)) continue;
									
				errorHandler ("305", context,
						"The rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule25_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule25_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXSwap"), errorHandler)
					& Rule25_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwap"), errorHandler));					
				
			return (
				  Rule25_OLD (name, nodeIndex.GetElementsByName ("fxSwap"), errorHandler));
		}
		
		private static bool Rule25_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	legs	= XPath.Paths (context, "fxSingleLeg");
				
				if (Count (legs) >= 2) continue;
									
				errorHandler ("305", context,
						"FX swaps must have at least two legs.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule26_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule26_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXSwap"), errorHandler)
					& Rule26_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwap"), errorHandler));					
				
			return (
				  Rule26_OLD (name, nodeIndex.GetElementsByName ("fxSwap"), errorHandler));
		}
		
		private static bool Rule26_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	legs	= XPath.Paths (context, "fxSingleLeg");
				
				if (Count (legs) != 2) continue;
				
				XmlElement 	date1	= XPath.Path (legs [0] as XmlElement, "valueDate");
				XmlElement 	date2	= XPath.Path (legs [1] as XmlElement, "valueDate");
									
				if (NotEqual (ToDate (date1), ToDate (date2))) continue;
				
				errorHandler ("305", context,
						"FX swaps legs must settle on different days.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule26 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule26 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwap"), errorHandler));					
				
			return (
				  Rule26 (name, nodeIndex.GetElementsByName ("fxSwap"), errorHandler));
		}
		
		private static bool Rule26 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement 	nearDate = XPath.Path (context, "nearLeg", "valueDate");
				XmlElement 	farDate  = XPath.Path (context, "farLeg", "valueDate");
									
				if ((nearDate == null) || (farDate == null) ||
                    NotEqual (ToDate (nearDate), ToDate (farDate))) continue;
				
				errorHandler ("305", context,
						"The value date of the near leg must be before that of the far leg.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule27 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule27 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "QuotedCurrencyPair"), errorHandler));					
				
			return (
				  Rule27 (name, nodeIndex.GetElementsByName ("quotedCurrencyPair"), errorHandler));
		}
		
		private static bool Rule27 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement	context in list) {
				XmlElement	ccy1	= XPath.Path (context, "currency1");
				XmlElement	ccy2	= XPath.Path (context, "currency2");
				
				if ((ccy1 == null) || (ccy2 == null) || !IsSameCurrency (ccy1, ccy2)) continue;
				
				errorHandler ("305", context,
						"Currencies must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
				
		// --------------------------------------------------------------------

		private static bool Rule28_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule28_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRate"), errorHandler));					
				
			return (
				  Rule28_OLD (name, nodeIndex.GetElementsByName ("currency1SideRate"), errorHandler)
				& Rule28_OLD (name, nodeIndex.GetElementsByName ("currency2SideRate"), errorHandler));
		}
		
		private static bool Rule28_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		rate	= XPath.Path (context, "rate");
				
				if ((rate == null) || IsPositive (rate)) continue;
									
				errorHandler ("305", context,
						"The rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule29_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule29_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRate"), errorHandler));					
				
			return (
				  Rule29_OLD (name, nodeIndex.GetElementsByName ("currency1SideRate"), errorHandler)
				& Rule29_OLD (name, nodeIndex.GetElementsByName ("currency2SideRate"), errorHandler));
		}
		
		private static bool Rule29_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement 	forward = XPath.Path (context, "forwardPoints");
				XmlElement	spot	= XPath.Path (context, "spotRate");
				
				if (!((forward != null) && (spot == null))) continue;
				
				errorHandler ("305", context,
						"If forwardPoints exists then spotRate should also exist.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule30_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule30_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRate"), errorHandler));					
				
			return (
					  Rule30_OLD (name, nodeIndex.GetElementsByName ("currency1SideRate"), errorHandler)
					& Rule30_OLD (name, nodeIndex.GetElementsByName ("currency2SideRate"), errorHandler));
		}
		
		private static bool Rule30_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement 	forward = XPath.Path (context, "forwardPoints");
				XmlElement	spot	= XPath.Path (context, "spotRate");
				XmlElement	rate	= XPath.Path (context, "rate");
				
				if ((rate == null) || (forward == null) || (spot == null)) continue;
				
				if (ToDecimal (rate).Equals(ToDecimal (spot) + ToDecimal (forward)))
					continue;
				
				errorHandler ("305", context,
						"Sum of spotRate and forwardPoints does not equal rate.",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule30 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule30 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "CrossRate"), errorHandler));					
				
			return (
					  Rule30 (name, nodeIndex.GetElementsByName ("crossRate"), errorHandler));
		}
		
		private static bool Rule30 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement 	forward = XPath.Path (context, "forwardPoints");
				XmlElement	spot	= XPath.Path (context, "spotRate");
				XmlElement	rate	= XPath.Path (context, "rate");
				
				if ((rate == null) || (forward == null) || (spot == null)) continue;
				
				if (ToDecimal (rate).Equals(ToDecimal (spot) + ToDecimal (forward)))
					continue;
				
				errorHandler ("305", context,
						"Sum of spotRate and forwardPoints does not equal rate.",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule31_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule31_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRates"), errorHandler));					
				
			return (
					  Rule31_OLD (name, nodeIndex.GetElementsByName ("sideRates"), errorHandler));
		}
		
		private static bool Rule31_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement 	baseCcy	= XPath.Path (context, "baseCurrency");
				XmlElement	ccy1	= XPath.Path (context, "currency1SideRate", "currency");
				XmlElement	ccy2	= XPath.Path (context, "currency2SideRate", "currency");
				
				if ((baseCcy == null) || (ccy1 == null) || (ccy2 == null) ||
					(!IsSameCurrency (baseCcy, ccy1) && !IsSameCurrency (baseCcy, ccy2))) continue;
				
				errorHandler ("305", context,
						"The base currency must be different from the side rate currencies.",
						name, ToToken (baseCcy));
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule32_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule32_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
				
			return (
					  Rule32_OLD (name, nodeIndex.GetElementsByName ("termDeposit"), errorHandler));
		}
		
		private static bool Rule32_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	payer	 = XPath.Path (context, "initialPayerReference");
				XmlElement	receiver = XPath.Path (context, "initialReceiverReference");
				
				if ((payer == null) || (receiver == null)) continue;

				if (NotEqual (payer.GetAttribute ("href"),
							  receiver.GetAttribute ("href"))) continue;
				
				errorHandler ("305", context,
						"The initial payer and receiver must be different",
						name, payer.GetAttribute ("href"));
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule32 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule32 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
				
			return (
					  Rule32 (name, nodeIndex.GetElementsByName ("termDeposit"), errorHandler));
		}
		
		private static bool Rule32 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	payerParty	  = XPath.Path (context, "payerPartyReference");
				XmlElement	receiverParty = XPath.Path (context, "receiverPartyReference");
				
			    if ((payerParty == null) || (receiverParty == null) ||
                        NotEqual (payerParty.GetAttribute ("href"),
							    receiverParty.GetAttribute ("href"))) continue;
				
				XmlElement	payerAccount	 = XPath.Path (context, "payerPartyReference");
				XmlElement	receiverAccount  = XPath.Path (context, "receiverPartyReference");

                if ((payerAccount != null) && (receiverAccount != null) &&
                        NotEqual (payerAccount.GetAttribute ("href"),
                                receiverAccount.GetAttribute ("href"))) continue;
                
                errorHandler ("305", context,
						"The payer and receiver must be different",
						name, payerParty.GetAttribute ("href"));
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule33 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule33 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
				
			return (
					  Rule33 (name, nodeIndex.GetElementsByName ("termDeposit"), errorHandler));
		}
		
		private static bool Rule33 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	start	 = XPath.Path (context, "startDate");
				XmlElement	maturity = XPath.Path (context, "maturityDate");
				
				if ((start == null) || (maturity == null) ||
					Less (ToDate (start), ToDate (maturity))) continue;
				
				errorHandler ("305", context,
						"The maturity date must be after the start date",
						name, ToToken (maturity));
				
				result = false;
			}
			
			return (result);
		}
				
		// --------------------------------------------------------------------

		private static bool Rule34_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule34_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
				
			return (
					  Rule34_OLD (name, nodeIndex.GetElementsByName ("termDeposit"), errorHandler));
		}
		
		private static bool Rule34_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	amount	= XPath.Path (context, "principal", "amount");
				
				if ((amount == null) || IsPositive (amount)) continue;

				errorHandler ("305", context,
						"The principal amount must be positive",
						name, ToToken (amount));
				
				result = false;
			}
			
			return (result);
		}
				
		// --------------------------------------------------------------------

		private static bool Rule35_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule35_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
				
			return (
					  Rule35_OLD (name, nodeIndex.GetElementsByName ("termDeposit"), errorHandler));
		}
		
		private static bool Rule35_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement	context in list) {
				XmlElement	rate	= XPath.Path (context, "fixedRate");
				
				if ((rate == null) || IsPositive (rate)) continue;

				errorHandler ("305", context,
						"The fixed rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}
		
		// --------------------------------------------------------------------

		private static bool Rule36_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule36_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule36_OLD (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule36_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate	 = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement	expiryDate	 = XPath.Path (context, "fxAverageRateOption", "expiryDateTime", "expiryDate");
				
				if ((tradeDate == null) || (expiryDate == null)) continue;
				
				if (Less (ToDate (tradeDate), ToDate (expiryDate))) continue;
									
				errorHandler ("305", context,
						"Expiry date must be after trade date.",
						name, ToToken (expiryDate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule36 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule36 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule36 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule36 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate	 = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement	expiryDate	 = XPath.Path (context, "fxOption", "expiryDateTime", "expiryDate");
				
				if ((tradeDate == null) || (expiryDate == null)) continue;
				
				if (Less (ToDate (tradeDate), ToDate (expiryDate))) continue;
									
				errorHandler ("305", context,
						"Expiry date must be after trade date.",
						name, ToToken (expiryDate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule36B_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule36B_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule36B_OLD (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule36B_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool result = true;

			foreach (XmlElement context in list) {
				XmlElement tradeDate = XPath.Path (context, "header", "contractDate");
				XmlElement expiryDate = XPath.Path (context, "fxAverageRateOption", "expiryDateTime", "expiryDate");

				if ((tradeDate == null) || (expiryDate == null))
					continue;

				if (Less (ToDate (tradeDate), ToDate (expiryDate)))
					continue;

				errorHandler ("305", context,
						"Expiry date must be after contract trade date.",
						name, ToToken (expiryDate));

				result = false;
			}

			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule37_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule37_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule37_OLD (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule37_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate	 = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement	expiryDate	 = XPath.Path (context, "fxBarrierOption", "expiryDateTime", "expiryDate");
				
				if ((tradeDate == null) || (expiryDate == null)) continue;
				
				if (Less (ToDate (tradeDate), ToDate (expiryDate))) continue;
									
				errorHandler ("305", context,
						"Expiry date must be after trade date.",
						name, ToToken (expiryDate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule37B_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule37B_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule37B_OLD (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule37B_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool result = true;

			foreach (XmlElement context in list) {
				XmlElement tradeDate = XPath.Path (context, "header", "contractDate");
				XmlElement expiryDate = XPath.Path (context, "fxBarrierOption", "expiryDateTime", "expiryDate");

				if ((tradeDate == null) || (expiryDate == null))
					continue;

				if (Less (ToDate (tradeDate), ToDate (expiryDate)))
					continue;

				errorHandler ("305", context,
						"Expiry date must be after contract trade date.",
						name, ToToken (expiryDate));

				result = false;
			}

			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule38_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule38_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule38_OLD (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule38_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate	 = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement	expiryDate	 = XPath.Path (context, "fxDigitalOption", "expiryDateTime", "expiryDate");
				
				if ((tradeDate == null) || (expiryDate == null)) continue;
				
				if (Less (ToDate (tradeDate), ToDate (expiryDate))) continue;
									
				errorHandler ("305", context,
						"Expiry date must be after trade date.",
						name, ToToken (expiryDate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule38 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule38 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule38 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule38 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate	 = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement	expiryDate	 = XPath.Path (context, "fxDigitalOption", "europeanExercise", "expiryDate");
				
				if ((tradeDate == null) || (expiryDate == null)) continue;
				
				if (Less (ToDate (tradeDate), ToDate (expiryDate))) continue;
									
				errorHandler ("305", context,
						"Expiry date must be after trade date.",
						name, ToToken (expiryDate));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule38B_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule38B_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule38B_OLD (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule38B_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool result = true;

			foreach (XmlElement context in list) {
				XmlElement tradeDate = XPath.Path (context, "header", "contractDate");
				XmlElement expiryDate = XPath.Path (context, "fxDigitalOption", "expiryDateTime", "expiryDate");

				if ((tradeDate == null) || (expiryDate == null))
					continue;

				if (Less (ToDate (tradeDate), ToDate (expiryDate)))
					continue;

				errorHandler ("305", context,
						"Expiry date must be after contract trade date.",
						name, ToToken (expiryDate));

				result = false;
			}

			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule39 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule39 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule39 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule39 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement	valueDate = XPath.Path (context, "fxSingleLeg", "valueDate");
				XmlElement	value1Date = XPath.Path (context, "fxSingleLeg", "currency1ValueDate");
				XmlElement	value2Date = XPath.Path (context, "fxSingleLeg", "currency2ValueDate");
				
				if (tradeDate != null) {
					if (valueDate != null) {
						if (Less (ToDate (tradeDate), ToDate (valueDate))) continue;
						
						errorHandler ("305", context,
								"value date must be after trade date.",
								name, ToToken (valueDate));
						
						result = false;
					}
					
					if (value1Date != null) {
						if (Less (ToDate (tradeDate), ToDate (value1Date))) continue;
						
						errorHandler ("305", context,
								"value1date must be after trade date.",
								name, ToToken (value1Date));
						
						result = false;
					}

					if (value2Date != null) {
						if (Less (ToDate (tradeDate), ToDate (value2Date))) continue;
						
						errorHandler ("305", context,
								"value2date must be after trade date.",
								name, ToToken (value2Date));
						
						result = false;
					}
				}
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule39B_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule39B_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule39B_OLD (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule39B_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool result = true;

			foreach (XmlElement context in list) {
				XmlElement tradeDate = XPath.Path (context, "header", "contractDate");
				XmlElement valueDate = XPath.Path (context, "fxSingleLeg", "valueDate");
				XmlElement value1Date = XPath.Path (context, "fxSingleLeg", "currency1ValueDate");
				XmlElement value2Date = XPath.Path (context, "fxSingleLeg", "currency2ValueDate");

				if (tradeDate != null) {
					if (valueDate != null) {
						if (Less (ToDate (tradeDate), ToDate (valueDate)))
							continue;

						errorHandler ("305", context,
								"value date must be after contract trade date.",
								name, ToToken (valueDate));

						result = false;
					}

					if (value1Date != null) {
						if (Less (ToDate (tradeDate), ToDate (value1Date)))
							continue;

						errorHandler ("305", context,
								"value1date must be after contract trade date.",
								name, ToToken (value1Date));

						result = false;
					}

					if (value2Date != null) {
						if (Less (ToDate (tradeDate), ToDate (value2Date)))
							continue;

						errorHandler ("305", context,
								"value2date must be after contract trade date.",
								name, ToToken (value2Date));

						result = false;
					}
				}
			}

			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule40_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule40_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule40_OLD (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule40_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate 	= XPath.Path (context, "tradeHeader", "tradeDate");
				XmlNodeList	legs	  	= XPath.Paths (context, "fxSwap", "fxSingleLeg");
				
				foreach (XmlElement leg in legs) {
					XmlElement	valueDate 	= XPath.Path (leg, "valueDate");
					XmlElement	value1Date 	= XPath.Path (leg, "currency1ValueDate");
					XmlElement	value2Date 	= XPath.Path (leg, "currency2ValueDate");
					
					if (tradeDate != null) {
						if (valueDate != null) {
							if (Less (ToDate (tradeDate), ToDate (valueDate))) continue;
							
							errorHandler ("305", leg,
									"value date must be after trade date.",
									name, ToToken (valueDate));
							
							result = false;
						}
						
						if (value1Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value1Date))) continue;
							
							errorHandler ("305", leg,
									"value1date must be after trade date.",
									name, ToToken (value1Date));
							
							result = false;
						}

						if (value2Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value2Date))) continue;
							
							errorHandler ("305", leg,
									"value2date must be after trade date.",
									name, ToToken (value2Date));
							
							result = false;
						}
					}
				}
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule40 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule40 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule40 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule40 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	tradeDate 	= XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement	nearLeg	  	= XPath.Path (context, "fxSwap", "nearLeg");
				XmlElement	farLeg	  	= XPath.Path (context, "fxSwap", "farLeg");
				
				{
					XmlElement	valueDate 	= XPath.Path (nearLeg, "valueDate");
					XmlElement	value1Date 	= XPath.Path (nearLeg, "currency1ValueDate");
					XmlElement	value2Date 	= XPath.Path (nearLeg, "currency2ValueDate");
					
					if (tradeDate != null) {
						if (valueDate != null) {
							if (Less (ToDate (tradeDate), ToDate (valueDate))) continue;
							
							errorHandler ("305", nearLeg,
									"value date must be after trade date.",
									name, ToToken (valueDate));
							
							result = false;
						}
						
						if (value1Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value1Date))) continue;
							
							errorHandler ("305", nearLeg,
									"value1date must be after trade date.",
									name, ToToken (value1Date));
							
							result = false;
						}

						if (value2Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value2Date))) continue;
							
							errorHandler ("305", nearLeg,
									"value2date must be after trade date.",
									name, ToToken (value2Date));
							
							result = false;
						}
					}
                }

				{
					XmlElement	valueDate 	= XPath.Path (farLeg, "valueDate");
					XmlElement	value1Date 	= XPath.Path (farLeg, "currency1ValueDate");
					XmlElement	value2Date 	= XPath.Path (farLeg, "currency2ValueDate");
					
					if (tradeDate != null) {
						if (valueDate != null) {
							if (Less (ToDate (tradeDate), ToDate (valueDate))) continue;
							
							errorHandler ("305", farLeg,
									"value date must be after trade date.",
									name, ToToken (valueDate));
							
							result = false;
						}
						
						if (value1Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value1Date))) continue;
							
							errorHandler ("305", farLeg,
									"value1date must be after trade date.",
									name, ToToken (value1Date));
							
							result = false;
						}

						if (value2Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value2Date))) continue;
							
							errorHandler ("305", farLeg,
									"value2date must be after trade date.",
									name, ToToken (value2Date));
							
							result = false;
						}
					}
                }
			}
			
			return (result);
		}

        // --------------------------------------------------------------------

		private static bool Rule40B_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule40B_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule40B_OLD (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule40B_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool result = true;

			foreach (XmlElement context in list) {
				XmlElement tradeDate = XPath.Path (context, "header", "contractDate");
				XmlNodeList legs = XPath.Paths (context, "fxSwap", "fxSingleLeg");

				foreach (XmlElement leg in legs) {
					XmlElement valueDate = XPath.Path (leg, "valueDate");
					XmlElement value1Date = XPath.Path (leg, "currency1ValueDate");
					XmlElement value2Date = XPath.Path (leg, "currency2ValueDate");

					if (tradeDate != null) {
						if (valueDate != null) {
							if (Less (ToDate (tradeDate), ToDate (valueDate)))
								continue;

							errorHandler ("305", leg,
									"value date must be after contract trade date.",
									name, ToToken (valueDate));

							result = false;
						}

						if (value1Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value1Date)))
								continue;

							errorHandler ("305", leg,
									"value1date must be after contract trade date.",
									name, ToToken (value1Date));

							result = false;
						}

						if (value2Date != null) {
							if (Less (ToDate (tradeDate), ToDate (value2Date)))
								continue;

							errorHandler ("305", leg,
									"value2date must be after contract trade date.",
									name, ToToken (value2Date));

							result = false;
						}
					}
				}
			}

			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule41_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule41_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXBarrier"), errorHandler)
					& Rule41_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrier"), errorHandler));					
				
			return (
				  Rule41_OLD (name, nodeIndex.GetElementsByName ("fxBarrier"), errorHandler));
		}
		
		private static bool Rule41_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	rate	= XPath.Path (context, "triggerRate");
				
				if ((rate == null) || IsPositive (rate)) continue;
									
				errorHandler ("305", context,
						"The trigger rate must be positive",
						name, ToToken (rate));
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule42_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule42_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule42_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule42_OLD (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule42_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	nodes	= XPath.Paths (context, "averageRateObservationDate", "observationDate");
				
				int			limit	= nodes.Count;
				Date []		dates	= new Date [limit];
				
				for (int count = 0; count < limit; ++count)
					dates [count] = ToDate (nodes [count]);					
				
				for (int outer = 0; outer < (limit - 1); ++outer) {
					for (int inner = outer + 1; inner < limit; ++inner) {
						if (Equal (dates [outer], dates [inner]))
							errorHandler ("305", nodes [inner],
									"Duplicate observation date",
									name, ToToken (nodes [inner]));
						
						result = false;
					}
				}
				dates = null;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule43_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule43_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule43_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule43_OLD (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule43_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy1	= XPath.Path (context, "putCurrencyAmount", "currency");
				XmlElement	ccy2	= XPath.Path (context, "callCurrencyAmount", "currency");
				
				if ((ccy1 == null) || (ccy2 == null) || !IsSameCurrency (ccy1, ccy2)) continue;
				
				errorHandler ("305", context,
						"Put and call currencies must be different.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule44_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule44_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule44_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule44_OLD (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule44_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	buyer	 = XPath.Path (context, "buyerPartyReference");
				XmlElement	seller	 = XPath.Path (context, "sellerPartyReference");
				XmlElement	payer	 = XPath.Path (context, "fxOptionPremium", "payerPartyReference");
				XmlElement	receiver = XPath.Path (context, "fxOptionPremium", "receiverPartyReference");
				
				if ((buyer == null) || (seller == null) ||
					(payer == null) || (receiver == null)) continue;
				
				if (Equal (buyer.GetAttribute ("href"), payer.GetAttribute ("href")) &&
					Equal (seller.GetAttribute ("href"), receiver.GetAttribute ("href"))) continue;
									
				errorHandler ("305", context,
						"Premium payer and receiver don't match with option buyer and seller.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule45_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule45_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXDigitalOption"), errorHandler)
					& Rule45_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxDigitalOption"), errorHandler));					
				
			return (
				  Rule45_OLD (name, nodeIndex.GetElementsByName ("fxDigitalOption"), errorHandler));
		}
		
		private static bool Rule45_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	buyer	 = XPath.Path (context, "buyerPartyReference");
				XmlElement	seller	 = XPath.Path (context, "sellerPartyReference");
				XmlElement	payer	 = XPath.Path (context, "fxOptionPremium", "payerPartyReference");
				XmlElement	receiver = XPath.Path (context, "fxOptionPremium", "receiverPartyReference");
				
				if ((buyer == null) || (seller == null) ||
					(payer == null) || (receiver == null)) continue;
				
				if (Equal (buyer.GetAttribute("href"), payer.GetAttribute("href")) &&
					Equal (seller.GetAttribute("href"), receiver.GetAttribute("href"))) continue;
									
				errorHandler ("305", context,
						"Premium payer and receiver don't match with option buyer and seller.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
			
		// --------------------------------------------------------------------

		private static bool Rule45 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule45 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxDigitalOption"), errorHandler));					
				
			return (
				  Rule45 (name, nodeIndex.GetElementsByName ("fxDigitalOption"), errorHandler));
		}
		
		private static bool Rule45 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	buyerParty	 = XPath.Path (context, "buyerPartyReference");
				XmlElement	sellerParty	 = XPath.Path (context, "sellerPartyReference");
				XmlElement	payerParty	 = XPath.Path (context, "premium", "payerPartyReference");
				XmlElement	receiverParty = XPath.Path (context, "premium", "receiverPartyReference");
				
				if ((buyerParty == null) || (sellerParty == null) ||
					(payerParty == null) || (receiverParty == null)) continue;
				
				if (Equal (buyerParty.GetAttribute("href"), sellerParty.GetAttribute("href"))) {
				    XmlElement	buyerAccount	 = XPath.Path (context, "buyerAccountReference");
				    XmlElement	sellerAccount	 = XPath.Path (context, "sellerAccountReference");
				    XmlElement	payerAccount	 = XPath.Path (context, "premium", "payerAccountReference");
				    XmlElement	receiverAccount  = XPath.Path (context, "premium", "receiverAccountReference");

                    if ((buyerAccount != null) && (sellerAccount != null) &&
                        (payerAccount != null) && (receiverAccount != null) &&
                        Equal (buyerParty.GetAttribute("href"), payerParty.GetAttribute("href")) &&
					    Equal (sellerParty.GetAttribute("href"), receiverParty.GetAttribute("href")) &&
                        Equal (buyerAccount.GetAttribute("href"), payerAccount.GetAttribute("href")) &&
					    Equal (sellerAccount.GetAttribute("href"), receiverAccount.GetAttribute("href"))) continue;
                }
                else
                    if (Equal (buyerParty.GetAttribute("href"), payerParty.GetAttribute("href")) &&
					    Equal (sellerParty.GetAttribute("href"), receiverParty.GetAttribute("href"))) continue;
									
				errorHandler ("305", context,
						"Premium payer and receiver don't match with option buyer and seller.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}
		// --------------------------------------------------------------------

		private static bool Rule46_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule46_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRates"), errorHandler));					
				
			return (
					  Rule46_OLD (name, nodeIndex.GetElementsByName ("sideRates"), errorHandler));
		}
		
		private static bool Rule46_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	basis	= XPath.Path (context, "currency1SideRate", "sideRateBasis");
				
				if ((basis == null) ||
					ToToken (basis).ToUpper ().Equals ("CURRENCY1PERBASECURRENCY") || 
					ToToken (basis).ToUpper ().Equals ("BASECURRENCYPERCURRENCY1")) continue;
				
				errorHandler ("305", context,
						"Side rate basis for currency1 should not be expressed in terms of currency2.",
						name, ToToken (basis));
				
				result = false;
			}
			
			return (result);
		}
				
		// --------------------------------------------------------------------

		private static bool Rule47_OLD (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule47_OLD (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRates"), errorHandler));					
				
			return (
					  Rule47_OLD (name, nodeIndex.GetElementsByName ("sideRates"), errorHandler));
		}
		
		private static bool Rule47_OLD (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	basis	= XPath.Path (context, "currency2SideRate", "sideRateBasis");
				
				if ((basis == null) ||
					ToToken (basis).ToUpper ().Equals ("CURRENCY2PERBASECURRENCY") || 
					ToToken (basis).ToUpper ().Equals ("BASECURRENCYPERCURRENCY2")) continue;
				
				errorHandler ("305", context,
						"Side rate basis for currency2 should not be expressed in terms of currency1.",
						name, ToToken (basis));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule48 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule48 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAsianFeature"), errorHandler));					
				
			return (
					  Rule48 (name, nodeIndex.GetElementsByName ("asian"), errorHandler));
		}
		
		private static bool Rule48 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	rates	= XPath.Paths (context, "rateObservation", "rate");
				XmlElement	basis	= XPath.Path (context, "rateObservationQuoteBasis");
				
				if ((rates.Count == 0) ||
					(rates.Count > 0) && (basis != null)) continue;
				
				errorHandler ("305", context,
						"If one rateObservation/rate exists, then rateObservationQuoteBasis must exist.",
						name, null);
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule49 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule49 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwapLeg"), errorHandler));					
				
			return (
					  Rule49 (name, nodeIndex.GetElementsByName ("nearLeg"), errorHandler)
                    & Rule49 (name, nodeIndex.GetElementsByName ("farLeg"), errorHandler));
		}
		
		private static bool Rule49 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement	ccy1	= XPath.Path (context, "exchangedCurrency1", "paymentAmount", "currency");
				XmlElement	ccy2	= XPath.Path (context, "exchangedCurrency2", "paymentAmount", "currency");
				
				if ((ccy1 == null) || (ccy2 == null) ||
                    NotEqual (ccy1.GetAttribute ("currencyScheme"), ccy2.GetAttribute ("currencyScheme")) ||
                    NotEqual (ccy1, ccy2)) continue;
				
				errorHandler ("305", context,
						"The two currencies must be different",
						name, ToToken (ccy1));
				
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule50 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule50 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwapLeg"), errorHandler));					
				
			return (
					  Rule50 (name, nodeIndex.GetElementsByName ("nearLeg"), errorHandler)
                    & Rule50 (name, nodeIndex.GetElementsByName ("farLeg"), errorHandler));
		}
		
		private static bool Rule50 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		date1	= XPath.Path (context, "currency1ValueDate");
				XmlElement		date2	= XPath.Path (context, "currency2ValueDate");
				
				if ((date1 == null) || (date2 == null) ||
					NotEqual (ToDate (date1), ToDate (date2))) continue;
				
				errorHandler ("305", context,
						"currency1ValueDate and currency2ValueDate must be different.",
						name, ToToken (date1));
			
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule51 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule51 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwapLeg"), errorHandler));					
				
			return (
					  Rule51 (name, nodeIndex.GetElementsByName ("nearLeg"), errorHandler)
                    & Rule51 (name, nodeIndex.GetElementsByName ("farLeg"), errorHandler));
		}
		
		private static bool Rule51 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		cash	= XPath.Path (context, "nonDeliverableSettlement");
				XmlElement		forward	= XPath.Path (context, "exchangeRate", "forwardPoints");
				
				if ((cash == null) || (forward != null)) continue;
				
				errorHandler ("305", context,
						"If nonDeliverableSettlement is specified then forwardPoints must be present.",
						name, null);
			
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule52 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule52 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (Rule52 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule52 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		tradeDate	 = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement		expiryDate	 = XPath.Path (context, "fxOption", "americanExercise", "expiryDate");
				
				if ((tradeDate == null) || (expiryDate == null) ||
                    Less (ToDate (tradeDate), ToDate (expiryDate))) continue;
									
				errorHandler ("305", context,
						"Expiry date must be after trade date.",
						name, ToToken (expiryDate));
			
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		private static bool Rule53 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule53 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (Rule53 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule53 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlElement		tradeDate	 = XPath.Path (context, "tradeHeader", "tradeDate");
				XmlElement		expiryDate	 = XPath.Path (context, "fxDigitalOption", "americanExercise", "expiryDate");
				
				if ((tradeDate == null) || (expiryDate == null) ||
                    Less (ToDate (tradeDate), ToDate (expiryDate))) continue;
									
				errorHandler ("305", context,
						"Expiry date must be after trade date.",
						name, ToToken (expiryDate));
			
				result = false;
			}
			
			return (result);
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Generates a set of dates according to schedule defined by a start date,
		/// an end date, an interval, roll convention and a calendar.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="end">The end date.</param>
		/// <param name="frequency">The frequency of the schedule (e.g. 6M).</param>
		/// <param name="roll">The date roll convention or <c>null</c>.</param>
		/// <param name="calendar">The holiday calendar or <c>null</c>.</param>
		/// <returns>An array of calculated and adjusted dates.</returns>
		private static Date [] GenerateSchedule (Date start, Date end,
			Interval frequency, DateRoll roll, Calendar calendar)
		{
			Date		current = start;
			ArrayList	found	= new ArrayList ();
			Date []		dates;
			
			while (Less (current, end)) {
				Date		adjusted;
				
				if (roll != null)
					adjusted = roll.Adjust (calendar, current);
				else
					adjusted = current;
				
				if (!found.Contains (adjusted))
					found.Add (adjusted);
				
				if (frequency.Period == Period.TERM) {
					if (Equal (current, start))
						current = end;
					else
						break;				
				}
				else
					current = current.Plus (frequency);
			}
			
			found.CopyTo (dates  = new Date [found.Count]);
			return (dates);
		}
	}
}