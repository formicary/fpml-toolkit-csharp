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
using System.Xml;

using HandCoded.Classification;
using HandCoded.Xml;

namespace HandCoded.FpML
{
	/// <summary>
	/// The <b>ProductType</b> class contains a set of <see cref="Category"/>
	/// instances configured to classify standard FpML product types.
	/// </summary>
	public sealed class ProductType
	{
		/// <summary>
		/// A <see cref="Category"/> representing all short form documents.
		/// </summary>
		private static readonly Category SHORT_FORM
			= new AbstractCategory ("(SHORT FORM)");

		/// <summary>
		/// A <see cref="Category"/> representing all product types.
		/// </summary>
		private static readonly Category PRODUCT_TYPE
			= new AbstractCategory ("(PRODUCT TYPE)");

		/// <summary>
		/// A <see cref="Category"/> representing all structured products.
		/// </summary>
        private static readonly Category STRUCTURED_PRODUCT
            = new DelegatedRefinableCategory ("(STRUCTURE PRODUCT)", PRODUCT_TYPE,
                    new ApplicableDelegate (IsStructuredProduct));

		/// <summary>
		/// A <see cref="Category"/> representing all swaps.
		/// </summary>
		public static readonly Category	SWAP
			= new AbstractCategory ("(SWAP)", PRODUCT_TYPE);

		/// <summary>
		/// A <see cref="Category"/> representing all options.
		/// </summary>
		public static readonly Category	OPTION
			= new AbstractCategory ("(OPTION)", PRODUCT_TYPE);

		/// <summary>
		/// A <see cref="Category"/> representing all forwards.
		/// </summary>
		public static readonly Category	FORWARD
			= new AbstractCategory ("(FORWARD)", PRODUCT_TYPE);
		
		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange products.
		/// </summary>
		public static readonly Category	FOREIGN_EXCHANGE
			= new AbstractCategory ("(FOREIGN EXCHANGE)", PRODUCT_TYPE);

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange spot/forward
		/// deals.
		/// </summary>
		public static readonly Category	FX_SPOT_FORWARD
			= new DelegatedRefinableCategory ("FX SPOT/FORWARD", FOREIGN_EXCHANGE,
					new ApplicableDelegate (IsFxSpotForward));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange spot deals.
		/// </summary>
		public static readonly Category	FX_SPOT
			= new DelegatedRefinableCategory ("FX SPOT", FX_SPOT_FORWARD,
					new ApplicableDelegate (IsFxSpot));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange forward
		/// deals.
		/// </summary>
		public static readonly Category	FX_FORWARD
			= new DelegatedRefinableCategory ("FX FORWARD",
					new Category [] { FX_SPOT_FORWARD, FORWARD },
					new ApplicableDelegate (IsFxForward));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange swap
		/// deals.
		/// </summary>
		public static readonly Category	FX_SWAP
			= new DelegatedRefinableCategory ("FX SWAP",
					new Category [] { FX_SPOT_FORWARD, SWAP },
					new ApplicableDelegate (IsFxSwap));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange options
		/// </summary>
		public static readonly Category	FX_OPTION
			= new AbstractCategory ("FX OPTION",
					new Category [] { FOREIGN_EXCHANGE, OPTION });

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange barrier
		/// options
		/// </summary>
		public static readonly Category	FX_SIMPLE_OPTION
			= new DelegatedRefinableCategory ("FX SIMPLE OPTION", FX_OPTION,
					new ApplicableDelegate (IsFxSimpleOption));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange barrier
		/// options
		/// </summary>
		public static readonly Category	FX_BARRIER_OPTION
			= new DelegatedRefinableCategory ("FX BARRIER OPTION", FX_OPTION,
					new ApplicableDelegate (IsFxBarrierOption));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange digital
		/// options
		/// </summary>
		public static readonly Category	FX_DIGITAL_OPTION
			= new DelegatedRefinableCategory ("FX DIGITAL OPTION", FX_OPTION,
					new ApplicableDelegate (IsFxDigitalOption));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange average rate
		/// options
		/// </summary>
		public static readonly Category	FX_AVERAGE_RATE_OPTION
			= new DelegatedRefinableCategory ("FX AVERAGE RATE OPTION", FX_OPTION,
					new ApplicableDelegate (IsFxAverageRateOption));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange option
		/// strategies.
		/// </summary>
		public static readonly Category	FX_OPTION_STRATEGY
			= new DelegatedRefinableCategory ("FX OPTION STRATEGY",
                    new Category [] { FOREIGN_EXCHANGE, STRUCTURED_PRODUCT },
					new ApplicableDelegate (IsFxOptionStrategy));

		/// <summary>
		/// A <see cref="Category"/> representing all bullet payments.
		/// </summary>
		public static readonly Category	BULLET_PAYMENT
			= new DelegatedRefinableCategory ("BULLET PAYMENT", PRODUCT_TYPE,
					new ApplicableDelegate (IsBulletPayment));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate derivatives.
		/// </summary>
		public static readonly Category	INTEREST_RATE_DERIVATIVE
			= new AbstractCategory ("(INTEREST RATE DERIVATIVE)", PRODUCT_TYPE);

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate swaps.
		/// </summary>
		public static readonly Category	FORWARD_RATE_AGREEMENT
			= new DelegatedRefinableCategory ("FORWARD RATE AGREEMENT", INTEREST_RATE_DERIVATIVE,
					new ApplicableDelegate (IsForwardRateAgreement));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate swaps.
		/// </summary>
		public static readonly Category	INTEREST_RATE_SWAP
			= new DelegatedRefinableCategory ("INTEREST RATE SWAP",
					new Category [] { INTEREST_RATE_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsInterestRateSwap));

		/// <summary>
		/// A <see cref="Category"/> representing all cross currency interest
		/// rate swaps.
		/// </summary>
		public static readonly Category	CROSS_CURRENCY_SWAP
			= new DelegatedRefinableCategory ("CROSS CURRENCY SWAP", INTEREST_RATE_SWAP,
					new ApplicableDelegate (IsCrossCurrencySwap));

		/// <summary>
		/// A <see cref="Category"/> representing all cross currency interest
		/// rate swaps.
		/// </summary>
		public static readonly Category	INFLATION_SWAP
			= new DelegatedRefinableCategory ("INFLATION SWAP", INTEREST_RATE_SWAP,
					new ApplicableDelegate (IsInflationSwap));

		/// <summary>
		/// A <see cref="Category"/> representing all cross currency interest
		/// rate swaps.
		/// </summary>
		public static readonly Category	TERM_DEPOSIT
			= new DelegatedRefinableCategory ("TERM DEPOSIT", PRODUCT_TYPE,
					new ApplicableDelegate (IsTermDeposit));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate caps and floors.
		/// </summary>
		public static readonly Category	INTEREST_RATE_CAP_FLOOR
			= new DelegatedRefinableCategory ("INTEREST RATE CAP/FLOOR", INTEREST_RATE_DERIVATIVE,
					new ApplicableDelegate (IsInterestRateCapOrFloor));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate caps.
		/// </summary>
		public static readonly Category	INTEREST_RATE_CAP
			= new DelegatedRefinableCategory ("INTEREST RATE CAP", INTEREST_RATE_CAP_FLOOR,
					new ApplicableDelegate (IsInterestRateCap));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate floors.
		/// </summary>
		public static readonly Category	INTEREST_RATE_FLOOR
			= new DelegatedRefinableCategory ("INTEREST RATE FLOOR", INTEREST_RATE_CAP_FLOOR,
					new ApplicableDelegate (IsInterestRateFloor));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate collars.
		/// </summary>
		public static readonly Category	INTEREST_RATE_COLLAR
			= new DelegatedRefinableCategory ("INTEREST RATE FLOOR",
                    new Category [] { INTEREST_RATE_CAP, INTEREST_RATE_FLOOR },
					new ApplicableDelegate (IsInterestRateCollar));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate swaptions.
		/// </summary>
		public static readonly Category	INTEREST_RATE_SWAPTION
			= new DelegatedRefinableCategory ("INTEREST RATE SWAPTION", 
					new Category [] { INTEREST_RATE_DERIVATIVE, OPTION },
					new ApplicableDelegate (IsInterestRateSwaption));

		/// <summary>
		/// A <see cref="Category"/> representing all equity derivatives.
		/// </summary>
		public static readonly Category	EQUITY_DERIVATIVE
			= new AbstractCategory ("(EQUITY DERIVATIVE)", PRODUCT_TYPE);

		/// <summary>
		/// A <see cref="Category"/> representing all equity forward.
		/// </summary>
		public static readonly Category	EQUITY_FORWARD
			= new DelegatedRefinableCategory ("EQUITY FORWARD", 
					new Category [] { EQUITY_DERIVATIVE, FORWARD },
					new ApplicableDelegate (IsEquityForward));

		/// <summary>
		/// A <see cref="Category"/> representing all equity options.
		/// </summary>
		public static readonly Category	EQUITY_OPTION
			= new AbstractCategory ("(EQUITY OPTION)", 
                    new Category [] { EQUITY_DERIVATIVE, OPTION });

		/// <summary>
		/// A <see cref="Category"/> representing all equity options.
		/// </summary>
		public static readonly Category	EQUITY_SIMPLE_OPTION
			= new DelegatedRefinableCategory ("EQUITY SIMPLE_OPTION", EQUITY_OPTION,
					new ApplicableDelegate (IsEquityOption));

		/// <summary>
		/// A <see cref="Category"/> representing all equity short form options.
		/// </summary>
		public static readonly Category	EQUITY_OPTION_SHORT_FORM
            = new DelegatedRefinableCategory ("EQUITY OPTION SHORT FORM",
                    new Category [] { EQUITY_OPTION, SHORT_FORM },
					new ApplicableDelegate (IsEquityOptionShortForm));

		/// <summary>
		/// A <see cref="Category"/> representing all equity option transaction supplements.
		/// </summary>
		public static readonly Category	EQUITY_OPTION_TRANSACTION_SUPPLEMENT
            = new DelegatedRefinableCategory ("EQUITY OPTION TRANSACTION SUPPLEMENT", EQUITY_OPTION,
					new ApplicableDelegate (IsEquityOptionTransactionSupplement));

		/// <summary>
		/// A <see cref="Category"/> representing all equity correlation swaps.
		/// </summary>
		public static readonly Category	EQUITY_CORRELATION_SWAP
            = new DelegatedRefinableCategory ("EQUITY CORRELATION SWAP",
                    new Category [] { EQUITY_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsEquityCorrelationSwap));

		/// <summary>
		/// A <see cref="Category"/> representing all equity dividend swaps.
		/// </summary>
		public static readonly Category	EQUITY_DIVIDEND_SWAP
            = new DelegatedRefinableCategory ("EQUITY DIVIDEND SWAP",
                    new Category [] { EQUITY_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsEquityDividendSwap));

		/// <summary>
		/// A <see cref="Category"/> representing all equity variance options.
		/// </summary>
		public static readonly Category	EQUITY_VARIANCE_OPTION
            = new DelegatedRefinableCategory ("EQUITY VARIANCE OPTION", EQUITY_OPTION,
					new ApplicableDelegate (IsEquityVarianceOption));

		/// <summary>
		/// A <see cref="Category"/> representing all equity variance swaps.
		/// </summary>
		public static readonly Category	EQUITY_VARIANCE_SWAP
            = new DelegatedRefinableCategory ("EQUITY VARIANCE SWAP",
                    new Category [] { EQUITY_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsEquityVarianceSwap));

		/// <summary>
		/// A <see cref="Category"/> representing all equity variance swaps.
		/// </summary>
		public static readonly Category	EQUITY_VARIANCE_SWAP_TRANSACTION_SUPPLEMENT
            = new DelegatedRefinableCategory ("EQUITY VARIANCE SWAP TRANSACTION SUPPLEMENT",
                    new Category [] { EQUITY_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsEquityVarianceSwapTransactionSupplement));

		/// <summary>
		/// A <see cref="Category"/> representing all equity variance swaps.
		/// </summary>
		public static readonly Category	EQUITY_TOTAL_RETURN_SWAP
            = new DelegatedRefinableCategory ("EQUITY TOTAL RETURN SWAP",
                    new Category [] { EQUITY_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsEquityTotalReturnSwap));

		/// <summary>
		/// A <see cref="Category"/> representing all equity variance swaps.
		/// </summary>
		public static readonly Category	EQUITY_SWAP_TRANSACTION_SUPPLEMENT
            = new DelegatedRefinableCategory ("EQUITY SWAP TRANSACTION SUPPLEMENT",
                    new Category [] { EQUITY_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsEquitySwapTransactionSupplement));

		/// <summary>
		/// A <see cref="Category"/> representing all fixed income products.
		/// </summary>
		public static readonly Category	FIXED_INCOME
			= new AbstractCategory ("(FIXED INCOME)", PRODUCT_TYPE);

        /// <summary>
		/// A <see cref="Category"/> representing all bond options.
		/// </summary>
		public static readonly Category	BOND_OPTION
            = new DelegatedRefinableCategory ("BOND OPTION",
                    new Category [] { FIXED_INCOME, OPTION },
					new ApplicableDelegate (IsBondOption));

		/// <summary>
		/// A <see cref="Category"/> representing all credit derivatives.
		/// </summary>
		public static readonly Category	CREDIT_DERIVATIVE
			= new AbstractCategory ("(CREDIT DERIVATIVE)", PRODUCT_TYPE);
		
        /// <summary>
		/// A <see cref="Category"/> representing all bond options.
		/// </summary>
		public static readonly Category	CREDIT_DEFAULT_SWAP
            = new DelegatedRefinableCategory ("CREDIT DEFAULT SWAP",
                    new Category [] { CREDIT_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsCreditDefaultSwap));

        /// <summary>
		/// A <see cref="Category"/> representing all bond options.
		/// </summary>
		public static readonly Category	CREDIT_DEFAULT_SWAPTION
            = new DelegatedRefinableCategory ("CREDIT DEFAULT SWAPTION",
                    new Category [] { CREDIT_DERIVATIVE, OPTION },
					new ApplicableDelegate (IsCreditDefaultSwaption));

		/// <summary>
		/// A <see cref="Category"/> representing all commodity derivatives.
		/// </summary>
		public static readonly Category	COMMODITY_DERIVATIVE
			= new AbstractCategory ("(COMMODITY DERIVATIVE)", PRODUCT_TYPE);
		
        /// <summary>
		/// A <see cref="Category"/> representing all commodity swaps.
		/// </summary>
		public static readonly Category	COMMODITY_SWAP
            = new DelegatedRefinableCategory ("COMMODITY SWAP",
                    new Category [] { COMMODITY_DERIVATIVE, SWAP },
					new ApplicableDelegate (IsCommoditySwap));

        /// <summary>
		/// A <see cref="Category"/> representing all commodity forward.
		/// </summary>
		public static readonly Category	COMMODITY_FORWARD
            = new DelegatedRefinableCategory ("COMMODITY FORWARD",
                    new Category [] { CREDIT_DERIVATIVE, FORWARD },
					new ApplicableDelegate (IsCommodityForward));

        /// <summary>
		/// A <see cref="Category"/> representing all commodity options.
		/// </summary>
		public static readonly Category	COMMODITY_OPTION
            = new DelegatedRefinableCategory ("COMMODITY OPTION",
                    new Category [] { CREDIT_DERIVATIVE, OPTION },
					new ApplicableDelegate (IsCommodityOption));

		/// <summary>
		/// Attempts to determine the type of product used in an XML document.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static Category Classify (XmlElement parent)
		{
			return (PRODUCT_TYPE.Classify (parent));
		}

		/// <summary>
		/// Ensures that no instance can be constructed.
		/// </summary>
		private ProductType ()
		{ }

		/// <summary>
		/// Test function used to detect structured products.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
        private static bool IsStructuredProduct (Object value)
        {
			XmlDocument	document = ((XmlElement) value).OwnerDocument;
			
			if (Releases.FPML.GetReleaseForDocument (document) == Releases.R1_0)
				return (XPath.Path ((XmlElement) value, "product", "strategy") != null);
			else
				return (XPath.Path ((XmlElement) value, "strategy") != null);
        }

		/// <summary>
		/// Test function used to detect foreign exchange spots and forwards.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxSpotForward (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fxSingleLeg") != null);
		}

		/// <summary>
		/// Test function used to detect foreign exchange spots.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxSpot (Object value)
		{
			return (XPath.Path ((XmlElement) value, "fxSingleLeg", "exchangeRate", "forwardPoints") == null);
		}

		/// <summary>
		/// Test function used to detect foreign exchange forwards.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxForward (Object value)
		{
			return (XPath.Path ((XmlElement) value, "fxSingleLeg", "exchangeRate", "forwardPoints") != null);
		}

		/// <summary>
		/// Test function used to detect foreign exchange swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxSwap (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fxSwap") != null);
		}

		/// <summary>
		/// Test function used to detect simple foreign exchange options.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxSimpleOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fxSimpleOption") != null);
		}

		/// <summary>
		/// Test function used to detect foreign exchange barrier options.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxBarrierOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fxBarrierOption") != null);
		}

		/// <summary>
		/// Test function used to detect foreign exchange digital options.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxDigitalOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fxDigitalOption") != null);
		}

		/// <summary>
		/// Test function used to detect foreign exchange average rate options.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxAverageRateOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fxAverageRateOption") != null);
		}

		/// <summary>
		/// Test function used to detect foreign exchange option strategies.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsFxOptionStrategy (Object value)
		{
			XmlNodeList	nodes = XPath.Paths ((XmlElement) value, "strategy", "*");
			
			if (nodes.Count > 0) {
				foreach (XmlElement node in nodes) {
					String localName = node.LocalName;

					if (localName.Equals ("productType")) continue;
					if (localName.Equals ("productId")) continue;
					
					if (localName.Equals ("fxSingleLeg")) continue;
					if (localName.Equals ("fxSimpleOption")) continue;
					if (localName.Equals ("fxBarrierOption")) continue;
					if (localName.Equals ("fxDigitalOption")) continue;
					if (localName.Equals ("fxAverageRateOption")) continue;
					if (localName.Equals ("termDeposit")) continue;
					
					return (false);
				}
				return (true);
			}
			return (false);
		}

		/// <summary>
		/// Test function used to detect bullet payments.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsBulletPayment (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "bulletPayment") != null);
		}

		/// <summary>
		/// Test function used to detect forward rate agreements.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsForwardRateAgreement (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fra") != null);
		}

		/// <summary>
		/// Test function used to detect interest rate swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsInterestRateSwap (Object value)
		{
			XmlDocument	document = ((XmlElement) value).OwnerDocument;

			if (Releases.FPML.GetReleaseForDocument (document) == Releases.R1_0)
				return (XPath.Path (value as XmlElement, "product", "swap") != null);
			else
				return (XPath.Path (value as XmlElement, "swap") != null);
		}

		/// <summary>
		/// Test function used to detect interest rate cross currency swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsCrossCurrencySwap (Object value)
		{
			XmlDocument	document = ((XmlElement) value).OwnerDocument;
			XmlNodeList	currencies;
			bool		different	= false;

			if (Releases.FPML.GetReleaseForDocument (document) == Releases.R1_0)
				currencies = XPath.Paths (value as XmlElement,
						"product", "swap", "swapStream", "calculationPeriodAmount", "calculation",
						"notionalSchedule", "notionalStepSchedule",	"currency");
			else
				currencies = XPath.Paths (value as XmlElement,
						"swap", "swapStream", "calculationPeriodAmount", "calculation",
						"notionalSchedule", "notionalStepSchedule",	"currency");

			for (int index = 1; index < currencies.Count; ++index) {
				XmlElement	ccy1	= (XmlElement) currencies.Item (index - 1);
				XmlElement	ccy2	= (XmlElement) currencies.Item (index);

				if (!ccy1.InnerText.Trim ().Equals (ccy2.InnerText.Trim ())) {
					different = true;
					break;
				}
			}
			return (different);
		}

		/// <summary>
		/// Test function used to detect inflation swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
        private static bool IsInflationSwap (Object value)
        {
			XmlNodeList 	calcs = XPath.Paths((XmlElement) value,
					"swap", "swapStream", "calculationPeriodAmount",
					"calculation", "inflationRateCalculation");
			
			return (calcs.Count > 0);
        }

		/// <summary>
		/// Test function used to detect term deposits.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsTermDeposit (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "termDeposit") != null);
		}

		/// <summary>
		/// Test function used to detect caps and floors.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsInterestRateCapOrFloor (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "capFloor") != null);
		}

		/// <summary>
		/// Test function used to detect caps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsInterestRateCap (Object value)
		{
			XmlElement floatingRateCalculation = XPath.Path ((XmlElement) value, "capFloor", "capFloorStream", "calculationPeriodAmount", "calculation", "floatingRateCalculation");
			XmlElement capRateSchedule = XPath.Path (floatingRateCalculation, "capRateSchedule");
			return (capRateSchedule != null);
		}

		/// <summary>
		/// Test function used to detect floors.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsInterestRateFloor (Object value)
		{
			XmlElement floatingRateCalculation = XPath.Path ((XmlElement) value, "capFloor", "capFloorStream", "calculationPeriodAmount", "calculation", "floatingRateCalculation");
			XmlElement floorRateSchedule = XPath.Path (floatingRateCalculation, "floorRateSchedule");
			return (floorRateSchedule != null);
		}

		/// <summary>
		/// Test function used to detect collar.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsInterestRateCollar (Object value)
		{
			XmlElement floatingRateCalculation = XPath.Path ((XmlElement) value, "capFloor", "capFloorStream", "calculationPeriodAmount", "calculation", "floatingRateCalculation");
			XmlElement capRateSchedule = XPath.Path (floatingRateCalculation, "capRateSchedule");
			XmlElement floorRateSchedule = XPath.Path (floatingRateCalculation, "floorRateSchedule");
			return ((capRateSchedule != null) && (floorRateSchedule != null));
		}

		/// <summary>
		/// Test function used to detect interest rate swaptions.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsInterestRateSwaption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "swaption") != null);
		}

    	/// <summary>
		/// Test function used to detect equity forwards.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityForward(Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "equityForward") != null);
		}

    	/// <summary>
		/// Test function used to detect equity options.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "equityOption") != null);
		}

    	/// <summary>
		/// Test function used to detect short form equity options.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityOptionShortForm (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "brokerEquityOption") != null);
		}

    	/// <summary>
		/// Test function used to detect equity options transaction supplements.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityOptionTransactionSupplement (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "equityOptionTransactionSupplement") != null);
		}

    	/// <summary>
		/// Test function used to detect equity correlation swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityCorrelationSwap (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "correlationSwap") != null);
		}

    	/// <summary>
		/// Test function used to detect equity dividend swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityDividendSwap (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "dividendSwapTransactionSupplement") != null);
		}

    	/// <summary>
		/// Test function used to detect equity forwards.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityVarianceOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "varianceOptionTransactionSupplement") != null);
		}

    	/// <summary>
		/// Test function used to detect equity variance swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityVarianceSwap (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "varianceSwap") != null);
		}

    	/// <summary>
		/// Test function used to detect equity variance swap transaction supplements.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityVarianceSwapTransactionSupplement (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "varianceSwapTransactionSupplement") != null);
		}

    	/// <summary>
		/// Test function used to detect equity total return swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquityTotalReturnSwap (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "returnSwap") != null);
		}

    	/// <summary>
		/// Test function used to detect equity swap transaction supplements.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsEquitySwapTransactionSupplement (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "equitySwapTransactionSupplement") != null);
		}

        /// <summary>
		/// Test function used to detect bond options.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsBondOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "bondOption") != null);
		}

        /// <summary>
		/// Test function used to detect credit default swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsCreditDefaultSwap (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "creditDefaultSwap") != null);
		}

        /// <summary>
		/// Test function used to detect credit default swaptions.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsCreditDefaultSwaption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "creditDefaultSwapOption") != null);
		}

        /// <summary>
		/// Test function used to detect commodity swaps.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsCommoditySwap (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "commoditySwap") != null);
		}

        /// <summary>
		/// Test function used to detect commodity forwards.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsCommodityForward (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "commodityForward") != null);
		}

        /// <summary>
		/// Test function used to detect commodityOptions.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsCommodityOption (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "commodityOption") != null);
		}
    }
}