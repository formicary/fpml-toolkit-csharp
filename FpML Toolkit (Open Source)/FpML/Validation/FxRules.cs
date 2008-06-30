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
using System.Xml;

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
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = RuleSet.ForName ("FxRules");

		/// <summary>
		/// Contains the <see cref="RuleSet"/>.
		/// </summary>
		public static RuleSet Rules {
			get {
				return (rules);
			}
		}

		/// <summary>
	    /// A <see cref="Rule"/> that ensures that the rate is positive.
		/// </summary>
        /// <remarks>Applies to FpML 3.0 and later.</remarks>
		public static readonly Rule	RULE01
			= new DelegatedRule (Preconditions.R3_0__LATER, "fx-1", new RuleDelegate (Rule01));


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
                        name, Types.ToToken (rate));
				
				result = false;
			}

            return (result);
        }


#if false	
	/**
	 * A <CODE>Rule</CODE> that ensures that if forwardPoints exists then
	 * spotRate should also exist.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE02
		= new Rule (Preconditions.R3_0__LATER, "fx-2")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("exchangeRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element 	forward = XPath.path (context, "forwardPoints");
					Element		spot	= XPath.path (context, "spotRate");
					
					if (!((forward != null) && (spot == null))) continue;
					
					errorHandler.error ("305", context,
							"If forwardPoints exists then spotRate should also exist.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures that if both forwardPoints and spotRate
	 * exist then rate equals 'spotRate + forwardRate'.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE03
		= new Rule (Preconditions.R3_0__LATER, "fx-3")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("exchangeRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element 	forward = XPath.path (context, "forwardPoints");
					Element		spot	= XPath.path (context, "spotRate");
					Element		rate	= XPath.path (context, "rate");
					
					if ((rate == null) || (forward == null) || (spot == null)) continue;
					
					if (toDecimal (rate).equals(toDecimal (spot).add (toDecimal (forward))))
						continue;
					
					errorHandler.error ("305", context,
							"Sum of spotRate and forwardPoints does not equal rate.",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};

	// 4 5 6

	/**
	 * A <CODE>Rule</CODE> that ensures sideRates/baseCurrency must be neither
	 * quotedCurrencyPair/currency1 nor quotedCurrencyPair/currency2.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE04
		= new Rule (Preconditions.R3_0__LATER, "fx-4")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("exchangeRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element 	baseCcy = XPath.path (context, "sideRates", "baseCurrency");
					Element		ccy1	= XPath.path (context, "quotedCurrencyPair", "currency1");
					Element		ccy2	= XPath.path (context, "quotedCurrencyPair", "currency2");
					
					if ((baseCcy == null) || (ccy2 == null) || (ccy2 == null)) continue;
					
					if (equal (baseCcy, ccy1) || equal (baseCcy, ccy2)) {
						errorHandler.error ("305", context,
								"The side rate base currency must not be one of the trade currencies.",
								getName (), Types.toString (baseCcy));
					
						result = false;
					}
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures sideRates/currency1SideRate/currency
	 * must be the same as quotedCurrencyPair/currency1.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE05
		= new Rule (Preconditions.R3_0__LATER, "fx-5")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("exchangeRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ccy		= XPath.path (context, "quotedCurrencyPair", "currency1");
					Element 	ccy1 	= XPath.path (context, "sideRates", "currency1SideRate", "currency");
					
					if ((ccy == null) || (ccy1 == null) || equal (ccy, ccy1)) continue;
					
					errorHandler.error ("305", context,
							"The side rate currency1 '" + Types.toString(ccy1) +
							"' must be the same as trade currency1 '" + Types.toString(ccy) + "'.",
							getName (), null);
				
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures sideRates/currency2SideRate/currency
	 * must be the same as quotedCurrencyPair/currency2.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE06
		= new Rule (Preconditions.R3_0__LATER, "fx-6")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "ExchangeRate"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("exchangeRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ccy		= XPath.path (context, "quotedCurrencyPair", "currency2");
					Element 	ccy1 	= XPath.path (context, "sideRates", "currency2SideRate", "currency");
					
					if ((ccy == null) || (ccy1 == null) || equal (ccy, ccy1)) continue;
					
					errorHandler.error ("305", context,
							"The side rate currency2 '" + Types.toString(ccy1) +
							"' must be the same as trade currency2 '" + Types.toString(ccy) + "'.",
							getName (), null);
				
					result = false;
				}
				
				return (result);
			}
		};					

	/**
	 * A <CODE>Rule</CODE> that ensures triggerRate is positive.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE07
		= new Rule (Preconditions.R3_0__LATER, "fx-7")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAmericanTrigger"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAmericanTrigger"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAmericanTrigger"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "triggerRate");
					
					if ((rate == null) || isPositive (rate)) continue;
										
					errorHandler.error ("305", context,
							"The trigger rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that observationStartDate and observationEndDate
	 * both exist then observationStartDate <= observationEndDate.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE08
		= new Rule (Preconditions.R3_0__LATER, "fx-8")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAmericanTrigger"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAmericanTrigger"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAmericanTrigger"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		start	= XPath.path (context, "observationStartDate");
					Element		end		= XPath.path (context, "observationEndDate");
					
					if ((start == null) || (end == null) || 
						lessOrEqual (toDate (start), toDate (end))) continue;
										
					errorHandler.error ("305", context,
							"The observationStartDate must not be after the observationEndDate",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures observationStartDate and observationEndDate
	 * both exist then observationStartDate <= observationEndDate.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE09
		= new Rule (Preconditions.R3_0__LATER, "fx-9")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateObservationSchedule"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("averageRateObservationSchedule"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		start	= XPath.path (context, "observationStartDate");
					Element		end		= XPath.path (context, "observationEndDate");
					
					if ((start == null) || (end == null) || 
						lessOrEqual (toDate (start), toDate (end))) continue;
										
					errorHandler.error ("305", context,
							"The observationStartDate must not be after the observationEndDate",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures the observation period defined by
	 * observationStartDate and observationEndDate should be an integer
	 * multiple of the calculationPeriodFrequency.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE10
		= new Rule (Preconditions.R3_0__LATER, "fx-10")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateObservationSchedule"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateObservationSchedule"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("averageRateObservationSchedule"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		start	= XPath.path (context, "observationStartDate");
					Element		end		= XPath.path (context, "observationEndDate");
					Element		period	= XPath.path (context, "calculationPeriodFrequency");
					
					if ((start == null) || (end == null) || (period == null) ||
							toInterval (period).dividesDates(toDate (start), toDate (end))) continue;
								
					errorHandler.error ("305", context,
							"The observation period is not a multiple of the calculationPeriodFrequency",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures each observationDate is unique.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE11
		= new Rule (Preconditions.R3_0__LATER, "fx-11")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAverageRateOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					NodeList	nodes	= XPath.paths (context, "observedRates", "observationDate");
					
					int			limit	= nodes.getLength ();
					Date []		dates	= new Date [limit];
					
					for (int count = 0; count < limit; ++count)
						dates [count] = toDate (nodes.item(count));					
					
					for (int outer = 0; outer < (limit - 1); ++outer) {
						for (int inner = outer + 1; inner < limit; ++inner) {
							if (equal (dates [outer], dates [inner]))
								errorHandler.error ("305", nodes.item (inner),
										"Duplicate observation date",
										getName (), Types.toString(nodes.item (inner)));
							
							result = false;
						}
					}
					dates = null;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures each observationDate matches one defined
	 * by the schedule.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE12
		= new Rule (Preconditions.R3_0__LATER, "fx-12")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAverageRateOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context 	= (Element) list.item (index);
					Element		schedule	= XPath.path (context, "averageRateObservationSchedule");
					
					if (schedule == null) continue;
					
					Element		start	= XPath.path (schedule, "observationStartDate");
					Element		end		= XPath.path (schedule, "observationEndDate");
					Element		freq	= XPath.path (schedule, "calculationPeriodFrequency");
					Element		roll	= XPath.path (freq, "rollConvention");
					
					if ((start == null) || (end == null) || (freq == null) || (roll == null)) continue;
					
					Date [] 	dates	= generateSchedule (toDate (start), toDate (end),
							toInterval (freq), DateRoll.forName (Types.toString (roll)), null);
					
					NodeList	nodes	= XPath.paths (context, "observedRates", "observationDate");
										
					for (int count = 0; count < nodes.getLength(); ++count) {
						Element 	observed = (Element) nodes.item (count);
						Date		date 	 = toDate (observed);
						
						boolean		found = false;
						for (int match = 0; match < dates.length; ++match) {
							if (equal (date, dates [match])) {
								found = true;
								break;
							}
						}
						
						if (!found) {
							errorHandler.error ("305", observed,
									"Observation date '" + Types.toString (observed) +
									"' does not match with the schedule.",
									getName (), Types.toString(observed));
							
							result = false;
						}
					}
					dates = null;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures each observationDate is unique.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE13
		= new Rule (Preconditions.R3_0__LATER, "fx-13")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAverageRateOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context 	= (Element) list.item (index);
					NodeList	schedule	= XPath.paths (context, "averageRateObservationDate", "observationDate");
					int			limit		= (schedule != null) ? schedule.getLength () : 0;
					
					if (limit == 0) continue;
					
					Date []		dates	= new Date [limit];
					
					for (int count = 0; count < limit; ++count)
						dates [count] = toDate (schedule.item (count));					
					
					NodeList	nodes	= XPath.paths (context, "observedRates", "observationDate");
										
					for (int count = 0; count < nodes.getLength(); ++count) {
						Element 	observed = (Element) nodes.item (count);
						Date		date 	 = toDate (observed);
						
						boolean		found = false;
						for (int match = 0; match < dates.length; ++match) {
							if (equal (date, dates [match])) {
								found = true;
								break;
							}
						}
						
						if (!found) {
							errorHandler.error ("305", observed,
									"Observation date '" + Types.toString (observed) +
									"' does not match with a defined observationDate.",
									getName (), Types.toString(observed));
							
							result = false;
						}
					}
					dates = null;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures observationStartDate and observationEndDate
	 * both exist then observationStartDate <= observationEndDate.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE14
		= new Rule (Preconditions.R3_0__LATER, "fx-14")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXBarrier"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxBarrier"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxBarrier"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		start	= XPath.path (context, "observationStartDate");
					Element		end		= XPath.path (context, "observationEndDate");
					
					if ((start == null) || (end == null) || 
						lessOrEqual (toDate (start), toDate (end))) continue;
										
					errorHandler.error ("305", context,
							"The observationStartDate must not be after the observationEndDate",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures spotRate is positive if it exists.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE15
		= new Rule (Preconditions.R3_0__LATER, "fx-15")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXBarrierOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxBarrierOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxBarrierOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "spotRate");
					
					if ((rate == null) || isPositive (rate)) continue;
										
					errorHandler.error ("305", context,
							"The spot rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};
				
	/**
	 * Context: FxDigitalOption (Complex Type)
	 * <P>
	 * A <CODE>Rule</CODE> that ensures spotRate is positive if it exists.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE16
		= new Rule (Preconditions.R3_0__LATER, "fx-16")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXDigitalOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxDigitalOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxDigitalOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "spotRate");
					
					if ((rate == null) || isPositive (rate)) continue;
										
					errorHandler.error ("305", context,
							"The spot rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures triggerRate is positive.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE17
		= new Rule (Preconditions.R3_0__LATER, "fx-17")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXEuropeanTrigger"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxEuropeanTrigger"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxEuropeanTrigger"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "triggerRate");
					
					if ((rate == null) || isPositive (rate)) continue;
										
					errorHandler.error ("305", context,
							"The trigger rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * Context>: FxLeg (Complex Type)
	 * <P>
	 * A <CODE>Rule</CODE> that ensures payer and reciever are correct.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE18
		= new Rule (Preconditions.R3_0__LATER, "fx-18")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXLeg"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxLeg"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSingleLeg"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ccy1Pay	= XPath.path (context, "exchangedCurrency1", "payerPartyReference");
					Element		ccy1Rec	= XPath.path (context, "exchangedCurrency1", "receiverPartyReference");
					Element		ccy2Pay	= XPath.path (context, "exchangedCurrency2", "payerPartyReference");
					Element		ccy2Rec	= XPath.path (context, "exchangedCurrency2", "receiverPartyReference");
					
					if ((ccy1Pay == null) || (ccy1Rec == null) ||
						(ccy2Pay == null) || (ccy2Rec == null)) continue;
					
					if (equal (DOM.getAttribute(ccy1Pay, "href"), DOM.getAttribute(ccy2Rec, "href")) &&
						equal (DOM.getAttribute(ccy2Pay, "href"), DOM.getAttribute(ccy1Rec, "href"))) continue;
										
					errorHandler.error ("305", context,
							"Exchanged currency payers and receivers don't match.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
	
	/**
	 * Context: FxLeg (Complex Type)
	 * <P>
	 * A <CODE>Rule</CODE> that ensures exchanged currencies are different.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE19
		= new Rule (Preconditions.R3_0__LATER, "fx-19")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXLeg"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxLeg"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSingleLeg"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ccy1	= XPath.path (context, "exchangedCurrency1", "paymentAmount", "currency");
					Element		ccy2	= XPath.path (context, "exchangedCurrency2", "paymentAmount", "currency");
					
					if ((ccy1 == null) || (ccy2 == null)) continue;
					
					// TODO check currency scheme as well
					if (notEqual (ccy1, ccy2)) continue;
										
					errorHandler.error ("305", context,
							"Exchanged currencies must be different.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
		
	/**
	 * A <CODE>Rule</CODE> that ensures split settlement dates are different.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE20
		= new Rule (Preconditions.R3_0__LATER, "fx-20")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXLeg"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxLeg"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSingleLeg"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		date1	= XPath.path (context, "currency1ValueDate");
					Element		date2	= XPath.path (context, "currency2ValueDate");
					
					if ((date1 == null) || (date2 == null)) continue;
					
					if (notEqual (toDate (date1), toDate (date2))) continue;
										
					errorHandler.error ("305", context,
							"currency1ValueDate and currency2ValueDate must be different.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures non-deliverable forwards contains
	 * a specification of the forward points.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE21
		= new Rule (Preconditions.R3_0__LATER, "fx-21")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXLeg"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxLeg"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSingleLeg"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ndf		= XPath.path (context, "nonDeliverableForward");
					Element		fwd		= XPath.path (context, "exchangeRate", "forwardPoints");
					
					if ((ndf == null) || (fwd != null)) continue;
					
					errorHandler.error ("305", context,
							"Non-deliverable forward does not specify forwardPoints.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures buyer, seller, payer and reciever are correct.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE22
		= new Rule (Preconditions.R3_0__LATER, "fx-22")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXOptionLeg"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxOptionLeg"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSimpleOption"), errorHandler)
					& validate (nodeIndex.getElementsByName ("fxBarrierOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  = (Element) list.item (index);
					Element		buyer	 = XPath.path (context, "buyerPartyReference");
					Element		seller	 = XPath.path (context, "sellerPartyReference");
					Element		payer	 = XPath.path (context, "fxOptionPremium", "payerPartyReference");
					Element		receiver = XPath.path (context, "fxOptionPremium", "receiverPartyReference");
					
					if ((buyer == null) || (seller == null) ||
						(payer == null) || (receiver == null)) continue;
					
					if (equal (DOM.getAttribute(buyer, "href"), DOM.getAttribute(payer, "href")) &&
						equal (DOM.getAttribute(seller, "href"), DOM.getAttribute(receiver, "href"))) continue;
										
					errorHandler.error ("305", context,
							"Premium payer and receiver don't match with option buyer and seller.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
		
	/**
	 * A <CODE>Rule</CODE> that ensures the put and call currencies are different.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE23
		= new Rule (Preconditions.R3_0__LATER, "fx-23")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXOptionLeg"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxOptionLeg"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSimpleOption"), errorHandler)
					& validate (nodeIndex.getElementsByName ("fxBarrierOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ccy1	= XPath.path (context, "putCurrencyAmount", "currency");
					Element		ccy2	= XPath.path (context, "callCurrencyAmount", "currency");
					
					if ((ccy1 == null) || (ccy2 == null)) continue;
					
					// TODO check currency scheme as well
					if (notEqual (ccy1, ccy2)) continue;
										
					errorHandler.error ("305", context,
							"Put and call currencies must be different.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures rate is positive.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE24
		= new Rule (Preconditions.R3_0__LATER, "fx-24")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXStrikePrice"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxStrikePrice"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxStrikePrice"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "rate");
					
					if ((rate == null) || isPositive (rate)) continue;
										
					errorHandler.error ("305", context,
							"The rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * Context: FxSwap (Complex Type)
	 * <P>
	 * A <CODE>Rule</CODE> that ensures two or more legs are present.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE25
		= new Rule (Preconditions.R3_0__LATER, "fx-25")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXSwap"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxSwap"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSwap"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					NodeList	legs	= XPath.paths (context, "fxSingleLeg");
					
					if (count (legs) >= 2) continue;
										
					errorHandler.error ("305", context,
							"FX swaps must have at least two legs.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures if two legs are present they must have
	 * different value dates.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE26
		= new Rule (Preconditions.R3_0__LATER, "fx-26")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXSwap"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxSwap"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxSwap"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					NodeList	legs	= XPath.paths (context, "fxSingleLeg");
					
					if (count (legs) != 2) continue;
					
					Element 	date1	= XPath.path ((Element) legs.item (0), "valueDate");
					Element 	date2	= XPath.path ((Element) legs.item (1), "valueDate");
										
					if (notEqual (toDate (date1), toDate (date2))) continue;
					
					errorHandler.error ("305", context,
							"FX swaps legs must settle on different days.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures two different currencies are specified.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE27
		= new Rule (Preconditions.R3_0__LATER, "fx-27")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "QuotedCurrencyPair"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("quotedCurrencyPair"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ccy1	= XPath.path (context, "currency1");
					Element		ccy2	= XPath.path (context, "currency2");
					
					if ((ccy1 == null) || (ccy2 == null)) continue;
					
					// TODO check currency scheme as well
					if (notEqual (ccy1, ccy2)) continue;
										
					errorHandler.error ("305", context,
							"Currencies must be different.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
				
	/**
	 * A <CODE>Rule</CODE> that ensures triggerRate is positive.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE28
		= new Rule (Preconditions.R3_0__LATER, "fx-28")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "SideRate"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("currency1SideRate"), errorHandler)
					& validate (nodeIndex.getElementsByName ("currency2SideRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "rate");
					
					if ((rate == null) || isPositive (rate)) continue;
										
					errorHandler.error ("305", context,
							"The rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures that if forwardPoints exists then
	 * spotRate should also exist.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE29
		= new Rule (Preconditions.R3_0__LATER, "fx-29")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "SideRate"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("currency1SideRate"), errorHandler)
					& validate (nodeIndex.getElementsByName ("currency2SideRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element 	forward = XPath.path (context, "forwardPoints");
					Element		spot	= XPath.path (context, "spotRate");
					
					if (!((forward != null) && (spot == null))) continue;
					
					errorHandler.error ("305", context,
							"If forwardPoints exists then spotRate should also exist.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
				
	/**
	 * A <CODE>Rule</CODE> that ensures that if both forwardPoints and spotRate
	 * exist then rate equals 'spotRate + forwardRate'.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE30
		= new Rule (Preconditions.R3_0__LATER, "fx-30")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "SideRate"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("currency1SideRate"), errorHandler)
						& validate (nodeIndex.getElementsByName ("currency2SideRate"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element 	forward = XPath.path (context, "forwardPoints");
					Element		spot	= XPath.path (context, "spotRate");
					Element		rate	= XPath.path (context, "rate");
					
					if ((rate == null) || (forward == null) || (spot == null)) continue;
					
					if (toDecimal (rate).equals(toDecimal (spot).add (toDecimal (forward))))
						continue;
					
					errorHandler.error ("305", context,
							"Sum of spotRate and forwardPoints does not equal rate.",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures that side rates are obtained relative
	 * to another currency.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE31
		= new Rule (Preconditions.R3_0__LATER, "fx-31")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "SideRates"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("sideRates"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element 	base 	= XPath.path (context, "baseCurrency");
					Element		ccy1	= XPath.path (context, "currency1SideRate", "currency");
					Element		ccy2	= XPath.path (context, "currency2SideRate", "currency");
					
					if ((base == null) || (ccy1 == null) || (ccy2 == null)) continue;
					
					// TODO handle currency scheme
					if (notEqual (base, ccy1) && notEqual (base, ccy2)) continue;
					
					errorHandler.error ("305", context,
							"The base currency must be different from the side rate currencies.",
							getName (), Types.toString (base));
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures the initial payer and reciever
	 * are different.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE33
		= new Rule (Preconditions.R4_0__LATER, "fx-33")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("termDeposit"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		payer	 = XPath.path (context, "initialPayerReference");
					Element		receiver = XPath.path (context, "initialReceiverReference");
					
					if ((payer == null) || (receiver == null)) continue;

					if (notEqual (DOM.getAttribute(payer, "href"),
								  DOM.getAttribute(receiver, "href"))) continue;
					
					errorHandler.error ("305", context,
							"The initial payer and receiver must be different",
							getName (), DOM.getAttribute (payer, "href"));
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures the maturity date is after the start date.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE34
		= new Rule (Preconditions.R4_0__LATER, "fx-34")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("termDeposit"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		start	 = XPath.path (context, "startDate");
					Element		maturity = XPath.path (context, "maturityDate");
					
					if ((start == null) || (maturity == null) ||
						less (toDate (start), toDate (maturity))) continue;
					
					errorHandler.error ("305", context,
							"The maturity date must be after the start date",
							getName (), Types.toString (maturity));
					
					result = false;
				}
				
				return (result);
			}
		};
				
	/**
	 * A <CODE>Rule</CODE> that ensures the principal amount is positive.
	 * are different.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE35
		= new Rule (Preconditions.R4_0__LATER, "fx-35")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("termDeposit"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		amount	= XPath.path (context, "principal", "amount");
					
					if ((amount == null) || isPositive (amount)) continue;

					errorHandler.error ("305", context,
							"The principal amount must be positive",
							getName (), Types.toString (amount));
					
					result = false;
				}
				
				return (result);
			}
		};
				
	/**
	 * A <CODE>Rule</CODE> that ensures the fixed rate is positive.
	 * are different.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE36
		= new Rule (Preconditions.R4_0__LATER, "fx-36")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "TermDeposit"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("termDeposit"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "fixedRate");
					
					if ((rate == null) || isPositive (rate)) continue;

					errorHandler.error ("305", context,
							"The fixed rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};
		
	/**
	 * A <CODE>Rule</CODE> that ensures expiry date is after trade date.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE37
		= new Rule (Preconditions.R3_0__LATER, "fx-37")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "Trade"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("trade"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  = (Element) list.item (index);
					Element		tradeDate	 = XPath.path (context, "tradeHeader", "tradeDate");
					Element		expiryDate	 = XPath.path (context, "fxAverageRateOption", "expiryDateTime", "expiryDate");
					
					if ((tradeDate == null) || (expiryDate == null)) continue;
					
					if (less (tradeDate, expiryDate)) continue;
										
					errorHandler.error ("305", context,
							"Expiry date must be after trade date.",
							getName (), Types.toString (expiryDate));
					
					result = false;
				}
				
				return (result);
			}
		};
					
	/**
	 * A <CODE>Rule</CODE> that ensures expiry date is after trade date.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE38
		= new Rule (Preconditions.R3_0__LATER, "fx-38")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "Trade"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("trade"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  = (Element) list.item (index);
					Element		tradeDate	 = XPath.path (context, "tradeHeader", "tradeDate");
					Element		expiryDate	 = XPath.path (context, "fxBarrierOption", "expiryDateTime", "expiryDate");
					
					if ((tradeDate == null) || (expiryDate == null)) continue;
					
					if (less (tradeDate, expiryDate)) continue;
										
					errorHandler.error ("305", context,
							"Expiry date must be after trade date.",
							getName (), Types.toString (expiryDate));
					
					result = false;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures expiry date is after trade date.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE39
		= new Rule (Preconditions.R3_0__LATER, "fx-39")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "Trade"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("trade"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  = (Element) list.item (index);
					Element		tradeDate	 = XPath.path (context, "tradeHeader", "tradeDate");
					Element		expiryDate	 = XPath.path (context, "fxDigitalOption", "expiryDateTime", "expiryDate");
					
					if ((tradeDate == null) || (expiryDate == null)) continue;
					
					if (less (tradeDate, expiryDate)) continue;
										
					errorHandler.error ("305", context,
							"Expiry date must be after trade date.",
							getName (), Types.toString (expiryDate));
					
					result = false;
				}
				
				return (result);
			}
		};
						
	/**
	 * A <CODE>Rule</CODE> that ensures value date is after trade date.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE40
		= new Rule (Preconditions.R3_0__LATER, "fx-40")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "Trade"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("trade"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  = (Element) list.item (index);
					Element		tradeDate = XPath.path (context, "tradeHeader", "tradeDate");
					Element		valueDate = XPath.path (context, "fxSingleLeg", "valueDate");
					Element		value1Date = XPath.path (context, "fxSingleLeg", "currency1ValueDate");
					Element		value2Date = XPath.path (context, "fxSingleLeg", "currency2ValueDate");
					
					if (tradeDate != null) {
						if (valueDate != null) {
							if (less (toDate (tradeDate), toDate (valueDate))) continue;
							
							errorHandler.error ("305", context,
									"value date must be after trade date.",
									getName (), Types.toString (valueDate));
							
							result = false;
						}
						
						if (value1Date != null) {
							if (less (toDate (tradeDate), toDate (value1Date))) continue;
							
							errorHandler.error ("305", context,
									"value date must be after trade date.",
									getName (), Types.toString (value1Date));
							
							result = false;
						}

						if (value2Date != null) {
							if (less (toDate (tradeDate), toDate (value2Date))) continue;
							
							errorHandler.error ("305", context,
									"value date must be after trade date.",
									getName (), Types.toString (value2Date));
							
							result = false;
						}
					}
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures all FX swap value dates are after the
	 * trade date.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE41
		= new Rule (Preconditions.R3_0__LATER, "fx-41")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "Trade"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("trade"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  	= (Element) list.item (index);
					Element		tradeDate 	= XPath.path (context, "tradeHeader", "tradeDate");
					NodeList	legs	  	= XPath.paths (context, "fxSwap", "fxSingleLeg");
					
					for (int count = 0; count < legs.getLength(); ++count) {
						Element		leg			= (Element) legs.item (count);
						Element		valueDate 	= XPath.path (leg, "valueDate");
						Element		value1Date 	= XPath.path (leg, "currency1ValueDate");
						Element		value2Date 	= XPath.path (leg, "currency2ValueDate");
						
						if (tradeDate != null) {
							if (valueDate != null) {
								if (less (toDate (tradeDate), toDate (valueDate))) continue;
								
								errorHandler.error ("305", leg,
										"value date must be after trade date.",
										getName (), Types.toString (valueDate));
								
								result = false;
							}
							
							if (value1Date != null) {
								if (less (toDate (tradeDate), toDate (value1Date))) continue;
								
								errorHandler.error ("305", leg,
										"value date must be after trade date.",
										getName (), Types.toString (value1Date));
								
								result = false;
							}

							if (value2Date != null) {
								if (less (toDate (tradeDate), toDate (value2Date))) continue;
								
								errorHandler.error ("305", leg,
										"value date must be after trade date.",
										getName (), Types.toString (value2Date));
								
								result = false;
							}
						}
					}
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures triggerRate is positive.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE42
		= new Rule (Preconditions.R3_0__LATER, "fx-42")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXBarrier"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxBarrier"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxBarrier"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		rate	= XPath.path (context, "triggerRate");
					
					if ((rate == null) || isPositive (rate)) continue;
										
					errorHandler.error ("305", context,
							"The trigger rate must be positive",
							getName (), Types.toString (rate));
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures each averageRateObservationDate/observationDate
	 * is unique.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE43
		= new Rule (Preconditions.R3_0__LATER, "fx-43")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAverageRateOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					NodeList	nodes	= XPath.paths (context, "averageRateObservationDate", "observationDate");
					
					int			limit	= nodes.getLength ();
					Date []		dates	= new Date [limit];
					
					for (int count = 0; count < limit; ++count)
						dates [count] = toDate (nodes.item(count));					
					
					for (int outer = 0; outer < (limit - 1); ++outer) {
						for (int inner = outer + 1; inner < limit; ++inner) {
							if (equal (dates [outer], dates [inner]))
								errorHandler.error ("305", nodes.item (inner),
										"Duplicate observation date",
										getName (), Types.toString(nodes.item (inner)));
							
							result = false;
						}
					}
					dates = null;
				}
				
				return (result);
			}
		};

	/**
	 * A <CODE>Rule</CODE> that ensures the put and call currencies are different.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE44
		= new Rule (Preconditions.R3_0__LATER, "fx-44")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAverageRateOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		ccy1	= XPath.path (context, "putCurrencyAmount", "currency");
					Element		ccy2	= XPath.path (context, "callCurrencyAmount", "currency");
					
					if ((ccy1 == null) || (ccy2 == null)) continue;
					
					// TODO check currency scheme as well
					if (notEqual (ccy1, ccy2)) continue;
										
					errorHandler.error ("305", context,
							"Put and call currencies must be different.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures buyer, seller, payer and reciever are correct.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE45
		= new Rule (Preconditions.R3_0__LATER, "fx-45")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXAverageRateOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxAverageRateOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxAverageRateOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  = (Element) list.item (index);
					Element		buyer	 = XPath.path (context, "buyerPartyReference");
					Element		seller	 = XPath.path (context, "sellerPartyReference");
					Element		payer	 = XPath.path (context, "fxOptionPremium", "payerPartyReference");
					Element		receiver = XPath.path (context, "fxOptionPremium", "receiverPartyReference");
					
					if ((buyer == null) || (seller == null) ||
						(payer == null) || (receiver == null)) continue;
					
					if (equal (DOM.getAttribute(buyer, "href"), DOM.getAttribute(payer, "href")) &&
						equal (DOM.getAttribute(seller, "href"), DOM.getAttribute(receiver, "href"))) continue;
										
					errorHandler.error ("305", context,
							"Premium payer and receiver don't match with option buyer and seller.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures buyer, seller, payer and reciever are correct.
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule	RULE46
		= new Rule (Preconditions.R3_0__LATER, "fx-46")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (
						  validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FXDigitalOption"), errorHandler)
						& validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "FxDigitalOption"), errorHandler));					
					
				return (
					  validate (nodeIndex.getElementsByName ("fxDigitalOption"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context  = (Element) list.item (index);
					Element		buyer	 = XPath.path (context, "buyerPartyReference");
					Element		seller	 = XPath.path (context, "sellerPartyReference");
					Element		payer	 = XPath.path (context, "fxOptionPremium", "payerPartyReference");
					Element		receiver = XPath.path (context, "fxOptionPremium", "receiverPartyReference");
					
					if ((buyer == null) || (seller == null) ||
						(payer == null) || (receiver == null)) continue;
					
					if (equal (DOM.getAttribute(buyer, "href"), DOM.getAttribute(payer, "href")) &&
						equal (DOM.getAttribute(seller, "href"), DOM.getAttribute(receiver, "href"))) continue;
										
					errorHandler.error ("305", context,
							"Premium payer and receiver don't match with option buyer and seller.",
							getName (), null);
					
					result = false;
				}
				
				return (result);
			}
		};
			
	/**
	 * A <CODE>Rule</CODE> that ensures the side rates definition for currency1
	 * uses an appropriate basis. 
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE47
		= new Rule (Preconditions.R3_0__LATER, "fx-47")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "SideRates"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("sideRates"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		basis	= XPath.path (context, "currency1SideRate", "sideRateBasis");
					
					if ((basis == null) ||
						Types.toString (basis).equalsIgnoreCase ("Currency1perBaseCurrency") || 
						Types.toString (basis).equalsIgnoreCase ("BaseCurrencyPerCurrency1")) continue;
					
					errorHandler.error ("305", context,
							"Side rate basis for currency1 should not be expressed in terms of currency2.",
							getName (), Types.toString (basis));
					
					result = false;
				}
				
				return (result);
			}
		};
				
	/**
	 * A <CODE>Rule</CODE> that ensures the side rates definition for currency2
	 * uses an appropriate basis. 
	 * <P>
	 * Applies to FpML 3.0 and later.
	 * @since	TFP 1.2
	 */
	public static final Rule 	RULE48
		= new Rule (Preconditions.R3_0__LATER, "fx-48")
		{
			public boolean validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
			{
				if (nodeIndex.hasTypeInformation()) 
					return (validate (nodeIndex.getElementsByType (determineNamespace (nodeIndex), "SideRates"), errorHandler));					
					
				return (
						  validate (nodeIndex.getElementsByName ("sideRates"), errorHandler));
			}
			
			private boolean validate (NodeList list, ValidationErrorHandler errorHandler)
			{
				boolean		result	= true;
				
				for (int index = 0; index < list.getLength(); ++index) {
					Element		context = (Element) list.item (index);
					Element		basis	= XPath.path (context, "currency2SideRate", "sideRateBasis");
					
					if ((basis == null) ||
						Types.toString (basis).equalsIgnoreCase ("Currency2perBaseCurrency") || 
						Types.toString (basis).equalsIgnoreCase ("BaseCurrencyPerCurrency2")) continue;
					
					errorHandler.error ("305", context,
							"Side rate basis for currency2 should not be expressed in terms of currency1.",
							getName (), Types.toString (basis));
					
					result = false;
				}
				
				return (result);
			}
		};
					
	/**
	 * Provides access to the FX validation rule set.
	 *
	 * @return	The FX validation rule set.
	 * @since	TFP 1.2
	 */
	public static RuleSet getRules ()
	{
		return (rules);
	}

	/**
	 * A <CODE>RuleSet</CODE> containing all the standard FpML defined
	 * validation rules for interest rate products.
	 * @since	TFP 1.2
	 */
	private static final RuleSet	rules = new RuleSet ();
	
	/**
	 * Generates a set of dates according to schedule defined by a start date,
	 * an end date, an interval, roll convention and a calendar.
	 * 
	 * @param	start		The start date.
	 * @param	end			The end date.
	 * @param	frequency	The frequency of the schedule (e.g. 6M).
	 * @param	roll		The date roll convention or <CODE>null</CODE>.
	 * @param	calendar	The holiday calendar or <CODE>null</CODE>.
	 * @return	An array of calculated and adjusted dates.
	 * @since	TFP 1.2
	 */
	protected static Date [] generateSchedule (final Date start, final Date end,
			final Interval frequency, final DateRoll roll, final Calendar calendar)
	{
		Date		current = start;
		Vector		found	= new Vector ();
		Date []		dates;
		
		while (less (current, end)) {
			Date		adjusted;
			
			if (roll != null)
				adjusted = roll.adjust(calendar, current);
			else
				adjusted = current;
			
			if (!found.contains (adjusted))
				found.add (adjusted);
			
			if (frequency.getPeriod () == Period.TERM) {
				if (equal (current, start))
					current = end;
				else
					break;				
			}
			else
				current = current.plus (frequency);
		}
		
		found.copyInto (dates  = new Date [found.size ()]);
		return (dates);
	}
#endif
#if false
		/// <summary>
		/// Initialises the <see cref="RuleSet"/> with copies of all the FpML
		/// defined <see cref="Rule"/> instances for Interest Rate Derivatives.
		/// </summary>
		static FxRules ()
		{
			Rules.Add (RULE01);
	/*		Rules.Add (RULE02);
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
			Rules.Add (RULE25); */
		}
#endif
	}
}