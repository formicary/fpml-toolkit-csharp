// Copyright (C),2005-2010 HandCoded Software Ltd.
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

 	    private static readonly Precondition	FX_PRODUCT
		    = new ContentPrecondition (
				new string [] { "fxSingleLeg", "fxSwap", "fxSimpleOption",
						"fxAverageRateOption", "fxBarrierOption", "fxDigitalOption",
						"termDeposit" },
				new string [] { "FxSingleLeg", "FXSingleLeg", "FxSwap", "FXSwap",
						"FxSimpleOption", "FXSimpleOption", "FxOption",
						"FxAveragerateOption", "FXAverageRateOption",
						"FxBarrierOption", "FXBarrierOption",
						"FxDigitalOption", "FXDigitalOption", "TermDeposit" }
				);

	    private static readonly Precondition 	R3_0__LATER
		    = Precondition.And (FX_PRODUCT, Preconditions.R3_0__LATER);
	
	    private static readonly Precondition 	R4_0__LATER
		    = Precondition.And (FX_PRODUCT, Preconditions.R4_0__LATER);
	
	    private static readonly Precondition 	R4_2__LATER
		    = Precondition.And (FX_PRODUCT, Preconditions.R4_2__LATER);
    	
	    private static readonly Precondition 	R3_0__R4_X
		    = Precondition.And (FX_PRODUCT,
				    Precondition.Or (Preconditions.R3_0, Preconditions.R4_X));
    	
	    private static readonly Precondition 	R4_0__R4_X
		    = Precondition.And (FX_PRODUCT, Preconditions.R4_X);
    	
	    private static readonly Precondition 	R5_1__LATER
		    = Precondition.And (FX_PRODUCT, Preconditions.R5_1__LATER);
        
        /// <summary>
	    /// A <see cref="Rule"/> that ensures that the rate is positive.
		/// </summary>
        /// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE01
			= new DelegatedRule (R3_0__R4_X, "fx-1", new RuleDelegate (Rule01));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if forwardPoints exists then
		/// spotRate should also exist.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE02
			= new DelegatedRule (R3_0__R4_X, "fx-2", new RuleDelegate (Rule02));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if both forwardPoints and spotRate
		/// exist then rate equals 'spotRate + forwardRate'.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE03
			= new DelegatedRule (R3_0__LATER, "fx-3", new RuleDelegate (Rule03));

		/// <summary>
		/// A <see cref="Rule"/> that ensures sideRates/baseCurrency must be neither
		/// quotedCurrencyPair/currency1 nor quotedCurrencyPair/currency2.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE04
			= new DelegatedRule (R3_0__R4_X, "fx-4", new RuleDelegate (Rule04));

		/// <summary>
		/// A <see cref="Rule"/> that ensures sideRates/currency1SideRate/currency
		/// must be the same as quotedCurrencyPair/currency1.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE05
			= new DelegatedRule (R3_0__R4_X, "fx-5", new RuleDelegate (Rule05));

		/// <summary>
		/// A <see cref="Rule"/> that ensures sideRates/currency2SideRate/currency
		/// must be the same as quotedCurrencyPair/currency2.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE06
			= new DelegatedRule (R3_0__R4_X, "fx-6", new RuleDelegate (Rule06));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE07
			= new DelegatedRule (R3_0__R4_X, "fx-7", new RuleDelegate (Rule07));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE08_A
			= new DelegatedRule (R3_0__R4_X, "fx-8[A]", new RuleDelegate (Rule08_A));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE08_B
			= new DelegatedRule (R5_1__LATER, "fx-8[B]", new RuleDelegate (Rule08_B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE09
			= new DelegatedRule (R3_0__LATER, "fx-9", new RuleDelegate (Rule09));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the observation period defined by
		/// observationStartDate and observationEndDate should be an integer
		/// multiple of the calculationPeriodFrequency.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE10
			= new DelegatedRule (R3_0__LATER, "fx-10", new RuleDelegate (Rule10));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each observationDate is unique.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE11_A
			= new DelegatedRule (R3_0__R4_X, "fx-11[A]", new RuleDelegate (Rule11_A));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each observationDate is unique.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE11_B
			= new DelegatedRule (R5_1__LATER, "fx-11[B]", new RuleDelegate (Rule11_B));

		/// <summary>
		/// A <see cref="Rule"/> that each observationDate matches one defined
		/// by the schedule.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE12
			= new DelegatedRule (R3_0__LATER, "fx-12", new RuleDelegate (Rule12));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each observationDate is unique.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE13_A
			= new DelegatedRule (R3_0__R4_X, "fx-13[A]", new RuleDelegate (Rule13_A));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each observationDate is unique.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE13_B
			= new DelegatedRule (R5_1__LATER, "fx-13[B]", new RuleDelegate (Rule13_B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 up to 5.0.</remarks>
		public static readonly Rule	RULE14_A
			= new DelegatedRule (R3_0__R4_X, "fx-14[A]", new RuleDelegate (Rule14_A));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if observationStartDate and observationEndDate
		/// both exist then observationStartDate &lt;= observationEndDate.
		/// </summary>
		/// <remarks>Applies to FpML 5.1 and later.</remarks>
		public static readonly Rule	RULE14_B
			= new DelegatedRule (R5_1__LATER, "fx-14[B]", new RuleDelegate (Rule14_B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures spotRate is positive if it exists.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE15
			= new DelegatedRule (R3_0__LATER, "fx-15", new RuleDelegate (Rule15));

		/// <summary>
		/// A <see cref="Rule"/> that ensures spotRate is positive if it exists.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE16
			= new DelegatedRule (R3_0__LATER, "fx-16", new RuleDelegate (Rule16));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE17
			= new DelegatedRule (R3_0__LATER, "fx-17", new RuleDelegate (Rule17));

		/// <summary>
		/// A <see cref="Rule"/> that ensures payer and reciever of an FxLeg are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE18
			= new DelegatedRule (R3_0__LATER, "fx-18", new RuleDelegate (Rule18));

		/// <summary>
		/// A <see cref="Rule"/> that ensures exchanged currencies in an FxLeg are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE19
			= new DelegatedRule (R3_0__LATER, "fx-19", new RuleDelegate (Rule19));

		/// <summary>
		/// A <see cref="Rule"/> that ensures split settlement dates are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE20
			= new DelegatedRule (R3_0__LATER, "fx-20", new RuleDelegate (Rule20));

		/// <summary>
		/// A <see cref="Rule"/> that ensures non-deliverable forwards contains
		/// a specification of the forward points.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE21
			= new DelegatedRule (R3_0__LATER, "fx-21", new RuleDelegate (Rule21));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE22
			= new DelegatedRule (R3_0__LATER, "fx-22", new RuleDelegate (Rule22));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the put and call currencies are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE23
			= new DelegatedRule (R3_0__LATER, "fx-23", new RuleDelegate (Rule23));

		/// <summary>
		/// A <see cref="Rule"/> that ensures rate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE24
			= new DelegatedRule (R3_0__LATER, "fx-24", new RuleDelegate (Rule24));

		/// <summary>
		/// A <see cref="Rule"/> that ensures two or more legs are present.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE25
			= new DelegatedRule (R3_0__LATER, "fx-25", new RuleDelegate (Rule25));

		/// <summary>
		/// A <see cref="Rule"/> that ensures if two legs are present they must have
		/// different value dates.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE26
			= new DelegatedRule (R3_0__LATER, "fx-26", new RuleDelegate (Rule26));

		/// <summary>
		/// A <see cref="Rule"/> that ensures two different currencies are specified.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE27
			= new DelegatedRule (R3_0__LATER, "fx-27", new RuleDelegate (Rule27));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE28
			= new DelegatedRule (R3_0__LATER, "fx-28", new RuleDelegate (Rule28));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if forwardPoints exists then
		/// spotRate should also exist.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE29
			= new DelegatedRule (R3_0__LATER, "fx-29", new RuleDelegate (Rule29));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that if both forwardPoints and spotRate
		/// exist then rate equals 'spotRate + forwardRate'.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE30
			= new DelegatedRule (R3_0__LATER, "fx-30", new RuleDelegate (Rule30));

		/// <summary>
		/// A <see cref="Rule"/> that ensures that side rates are obtained relative
		/// to another currency.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE31
			= new DelegatedRule (R3_0__LATER, "fx-31", new RuleDelegate (Rule31));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the initial payer and reciever
		/// are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE32
			= new DelegatedRule (R3_0__LATER, "fx-32", new RuleDelegate (Rule32));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the maturity date is after the start date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE33
			= new DelegatedRule (R3_0__LATER, "fx-33", new RuleDelegate (Rule33));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the principal amount is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE34
			= new DelegatedRule (R3_0__LATER, "fx-34", new RuleDelegate (Rule34));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the fixed rate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE35
			= new DelegatedRule (R3_0__LATER, "fx-35", new RuleDelegate (Rule35));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE36
			= new DelegatedRule (R3_0__LATER, "fx-36", new RuleDelegate (Rule36));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule RULE36B
			= new DelegatedRule (R4_2__LATER, "fx-36b", new RuleDelegate (Rule36B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE37
			= new DelegatedRule (R3_0__LATER, "fx-37", new RuleDelegate (Rule37));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule RULE37B
			= new DelegatedRule (R4_2__LATER, "fx-37b", new RuleDelegate (Rule37B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE38
			= new DelegatedRule (R3_0__LATER, "fx-38", new RuleDelegate (Rule38));

		/// <summary>
		/// A <see cref="Rule"/> that ensures expiry date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule RULE38B
			= new DelegatedRule (R4_2__LATER, "fx-38b", new RuleDelegate (Rule38B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures value date is after trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE39
			= new DelegatedRule (R3_0__LATER, "fx-39", new RuleDelegate (Rule39));

		/// <summary>
		/// A <see cref="Rule"/> that ensures value date is after contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule RULE39B
			= new DelegatedRule (R4_2__LATER, "fx-39b", new RuleDelegate (Rule39B));

		/// <summary>
		/// A <see cref="Rule"/> that all FX swap value dates are after the
		/// trade date.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE40
			= new DelegatedRule (R3_0__LATER, "fx-40", new RuleDelegate (Rule40));

		/// <summary>
		/// A <see cref="Rule"/> that all FX swap value dates are after the
		/// contract trade date.
		/// </summary>
		/// <remarks>Applies to FpML 4.2 and later.</remarks>
		public static readonly Rule RULE40B
			= new DelegatedRule (R4_2__LATER, "fx-40b", new RuleDelegate (Rule40B));

		/// <summary>
		/// A <see cref="Rule"/> that ensures triggerRate is positive.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE41
			= new DelegatedRule (R3_0__LATER, "fx-41", new RuleDelegate (Rule41));

		/// <summary>
		/// A <see cref="Rule"/> that ensures each averageRateObservationDate/observationDate
		/// is unique.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE42
			= new DelegatedRule (R3_0__LATER, "fx-42", new RuleDelegate (Rule42));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the put and call currencies are different.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE43
			= new DelegatedRule (R3_0__LATER, "fx-43", new RuleDelegate (Rule43));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE44
			= new DelegatedRule (R3_0__LATER, "fx-44", new RuleDelegate (Rule44));

		/// <summary>
		/// A <see cref="Rule"/> that ensures buyer, seller, payer and reciever are correct.
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE45
			= new DelegatedRule (R3_0__LATER, "fx-45", new RuleDelegate (Rule45));

		/// <summary>
		/// A <see cref="Rule"/> that ensures the side rates definition for currency1
		/// uses an appropriate basis. 
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE46
			= new DelegatedRule (R3_0__LATER, "fx-46", new RuleDelegate (Rule46));

		/// <summary>
		/// A <see cref="Rule"/> that
		/// </summary>
		/// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE47
			= new DelegatedRule (R3_0__LATER, "fx-47", new RuleDelegate (Rule47));

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

        private static bool Rule01 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule01 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule01 (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
        }

        private static bool Rule01 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule02 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule02 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule02 (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule02 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule04 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule04 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule04 (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule04 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule05 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule05 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule05 (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule05 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule06 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule06 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
				
			return (
				  Rule06 (name, nodeIndex.GetElementsByName ("exchangeRate"), errorHandler));
		}
		
		private static bool Rule06 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule07 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule07 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAmericanTrigger"), errorHandler)
					& Rule07 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAmericanTrigger"), errorHandler));					
				
			return (
				  Rule07 (name, nodeIndex.GetElementsByName ("fxAmericanTrigger"), errorHandler));
		}
		
		private static bool Rule07 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule08_A (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule08_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAmericanTrigger"), errorHandler)
					& Rule08_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAmericanTrigger"), errorHandler));					
				
			return (
				  Rule08_A (name, nodeIndex.GetElementsByName ("fxAmericanTrigger"), errorHandler));
		}
		
		private static bool Rule08_A (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule08_B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule08_B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxTouch"), errorHandler));					
				
			return (
				  Rule08_B (name, nodeIndex.GetElementsByName ("touch"), errorHandler));
		}
		
		private static bool Rule08_B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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
					  Rule09 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateObservationSchedule"), errorHandler)
					& Rule09 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
				
			return (
				  Rule09 (name, nodeIndex.GetElementsByName ("averageRateObservationSchedule"), errorHandler));
		}
		
		private static bool Rule09 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule10 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule10 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateObservationSchedule"), errorHandler)
					& Rule10 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
				
			return (
				  Rule10 (name, nodeIndex.GetElementsByName ("averageRateObservationSchedule"), errorHandler));
		}
		
		private static bool Rule10 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule11_A (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule11_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule11_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule11_A (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule11_A (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule11_B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule11_B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAsianFeature"), errorHandler));					
				
			return (
				  Rule11_B (name, nodeIndex.GetElementsByName ("asian"), errorHandler));
		}
		
		private static bool Rule11_B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	nodes	= XPath.Paths (context, "observedRate", "date");
				
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

		private static bool Rule12 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule12 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule12 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule12 (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule12 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule13_A (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule13_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule13_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule13_A (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule13_A (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule13_B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule13_B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAsianFeature"), errorHandler));					
				
			return (
				  Rule13_B (name, nodeIndex.GetElementsByName ("asian"), errorHandler));
		}
		
		private static bool Rule13_B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;
			
			foreach (XmlElement context in list) {
				XmlNodeList	schedule	= XPath.Paths (context, "averageRateObservationDate", "date");
				int			limit		= (schedule != null) ? schedule.Count : 0;
				
				if (limit == 0) continue;
				
				Date []		dates	= new Date [limit];
				
				for (int count = 0; count < limit; ++count)
					dates [count] = ToDate (schedule [count]);					
				
				XmlNodeList	nodes	= XPath.Paths (context, "observedRate", "date");
									
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

		private static bool Rule14_A (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule14_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXBarrier"), errorHandler)
					& Rule14_A (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrier"), errorHandler));					
				
			return (
				  Rule14_A (name, nodeIndex.GetElementsByName ("fxBarrier"), errorHandler));
		}
		
		private static bool Rule14_A (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule14_B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule14_B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrierFeature"), errorHandler));					
				
			return (
				  Rule14_B (name, nodeIndex.GetElementsByName ("barrier"), errorHandler));
		}
		
		private static bool Rule14_B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule15 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule15 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXBarrierOption"), errorHandler)
					& Rule15 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrierOption"), errorHandler));					
				
			return (
				  Rule15 (name, nodeIndex.GetElementsByName ("fxBarrierOption"), errorHandler));
		}
		
		private static bool Rule15 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule16 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule16 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXDigitalOption"), errorHandler)
					& Rule16 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxDigitalOption"), errorHandler));					
				
			return (
				  Rule16 (name, nodeIndex.GetElementsByName ("fxDigitalOption"), errorHandler));
		}
		
		private static bool Rule16 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule17 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule17 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXEuropeanTrigger"), errorHandler)
					& Rule17 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxEuropeanTrigger"), errorHandler));					
				
			return (
				  Rule17 (name, nodeIndex.GetElementsByName ("fxEuropeanTrigger"), errorHandler));
		}
		
		private static bool Rule17 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule18 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule18 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule18 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
			return (
				  Rule18 (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule18 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule19 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule19 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule19 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
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
		
		private static bool Rule20 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule20 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule20 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
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

		private static bool Rule21 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule21 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXLeg"), errorHandler)
					& Rule21 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxLeg"), errorHandler));					
				
			return (
				  Rule21 (name, nodeIndex.GetElementsByName ("fxSingleLeg"), errorHandler));
		}
		
		private static bool Rule21 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule22 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule22 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXOptionLeg"), errorHandler)
					& Rule22 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxOptionLeg"), errorHandler));					
				
			return (
				  Rule22 (name, nodeIndex.GetElementsByName ("fxSimpleOption"), errorHandler)
				& Rule22 (name, nodeIndex.GetElementsByName ("fxBarrierOption"), errorHandler));
		}
		
		private static bool Rule22 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule23 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule23 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXOptionLeg"), errorHandler)
					& Rule23 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxOptionLeg"), errorHandler));					
				
			return (
				  Rule23 (name, nodeIndex.GetElementsByName ("fxSimpleOption"), errorHandler)
				& Rule23 (name, nodeIndex.GetElementsByName ("fxBarrierOption"), errorHandler));
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

		private static bool Rule24 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule24 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXStrikePrice"), errorHandler)
					& Rule24 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxStrikePrice"), errorHandler));					
				
			return (
				  Rule24 (name, nodeIndex.GetElementsByName ("fxStrikePrice"), errorHandler));
		}
		
		private static bool Rule24 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule25 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule25 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXSwap"), errorHandler)
					& Rule25 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwap"), errorHandler));					
				
			return (
				  Rule25 (name, nodeIndex.GetElementsByName ("fxSwap"), errorHandler));
		}
		
		private static bool Rule25 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule26 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule26 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXSwap"), errorHandler)
					& Rule26 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxSwap"), errorHandler));					
				
			return (
				  Rule26 (name, nodeIndex.GetElementsByName ("fxSwap"), errorHandler));
		}
		
		private static bool Rule26 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule28 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule28 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRate"), errorHandler));					
				
			return (
				  Rule28 (name, nodeIndex.GetElementsByName ("currency1SideRate"), errorHandler)
				& Rule28 (name, nodeIndex.GetElementsByName ("currency2SideRate"), errorHandler));
		}
		
		private static bool Rule28 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule29 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule29 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRate"), errorHandler));					
				
			return (
				  Rule29 (name, nodeIndex.GetElementsByName ("currency1SideRate"), errorHandler)
				& Rule29 (name, nodeIndex.GetElementsByName ("currency2SideRate"), errorHandler));
		}
		
		private static bool Rule29 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule30 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule30 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRate"), errorHandler));					
				
			return (
					  Rule30 (name, nodeIndex.GetElementsByName ("currency1SideRate"), errorHandler)
					& Rule30 (name, nodeIndex.GetElementsByName ("currency2SideRate"), errorHandler));
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

		private static bool Rule31 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule31 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRates"), errorHandler));					
				
			return (
					  Rule31 (name, nodeIndex.GetElementsByName ("sideRates"), errorHandler));
		}
		
		private static bool Rule31 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule34 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule34 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
				
			return (
					  Rule34 (name, nodeIndex.GetElementsByName ("termDeposit"), errorHandler));
		}
		
		private static bool Rule34 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule35 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule35 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
				
			return (
					  Rule35 (name, nodeIndex.GetElementsByName ("termDeposit"), errorHandler));
		}
		
		private static bool Rule35 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule36B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule36B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule36B (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule36B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule37 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule37 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Trade"), errorHandler));					
				
			return (
				  Rule37 (name, nodeIndex.GetElementsByName ("trade"), errorHandler));
		}
		
		private static bool Rule37 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule37B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule37B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule37B (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule37B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule38B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule38B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule38B (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule38B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule39B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule39B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule39B (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule39B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule40B (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation)
				return (Rule40B (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "Contract"), errorHandler));

			return (
				  Rule40B (name, nodeIndex.GetElementsByName ("contract"), errorHandler));
		}

		private static bool Rule40B (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule41 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule41 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXBarrier"), errorHandler)
					& Rule41 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxBarrier"), errorHandler));					
				
			return (
				  Rule41 (name, nodeIndex.GetElementsByName ("fxBarrier"), errorHandler));
		}
		
		private static bool Rule41 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule42 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule42 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule42 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule42 (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule42 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule43 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule43 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule43 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule43 (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule43 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule44 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule44 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
					& Rule44 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
				
			return (
				  Rule44 (name, nodeIndex.GetElementsByName ("fxAverageRateOption"), errorHandler));
		}
		
		private static bool Rule44 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule45 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (
					  Rule45 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FXDigitalOption"), errorHandler)
					& Rule45 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "FxDigitalOption"), errorHandler));					
				
			return (
				  Rule45 (name, nodeIndex.GetElementsByName ("fxDigitalOption"), errorHandler));
		}
		
		private static bool Rule45 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule46 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule46 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRates"), errorHandler));					
				
			return (
					  Rule46 (name, nodeIndex.GetElementsByName ("sideRates"), errorHandler));
		}
		
		private static bool Rule46 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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

		private static bool Rule47 (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			if (nodeIndex.HasTypeInformation) 
				return (Rule47 (name, nodeIndex.GetElementsByType (DetermineNamespace (nodeIndex), "SideRates"), errorHandler));					
				
			return (
					  Rule47 (name, nodeIndex.GetElementsByName ("sideRates"), errorHandler));
		}
		
		private static bool Rule47 (string name, XmlNodeList list, ValidationErrorHandler errorHandler)
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