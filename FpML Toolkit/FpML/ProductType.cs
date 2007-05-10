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
		/// A <see cref="Category"/> representing all product types.
		/// </summary>
		private static readonly Category	PRODUCT_TYPE
			= new AbstractCategory ("(PRODUCT TYPE)");

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
			= new DelegatedRefinableCategory ("FX SPOT/FORWARD",
					new Category [] { FX_SPOT_FORWARD, FORWARD },
					new ApplicableDelegate (IsFxForward));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange options
		/// </summary>
		public static readonly Category	FX_OPTION
			= new DelegatedRefinableCategory ("FX OPTION",
					new Category [] { FOREIGN_EXCHANGE, OPTION },
					new ApplicableDelegate (IsFxOption));

		/// <summary>
		/// A <see cref="Category"/> representing all foreign exchange barrier
		/// options
		/// </summary>
		public static readonly Category	FX_BARRIER_OPTION
			= new DelegatedRefinableCategory ("FX BARRIER OPTION", FX_OPTION,
					new ApplicableDelegate (IsFxBarrierOption));

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
		/// A <see cref="Category"/> representing all bullet payments.
		/// </summary>
		public static readonly Category	CAP_FLOOR
			= new DelegatedRefinableCategory ("INTEREST RATE CAP/FLOOR", INTEREST_RATE_DERIVATIVE,
					new ApplicableDelegate (IsInterestRateCapOrFloor));

		/// <summary>
		/// A <see cref="Category"/> representing all interest rate swaptions.
		/// </summary>
		public static readonly Category	INTEREST_RATE_SWAPTION
			= new DelegatedRefinableCategory ("INTEREST RATE SWAPTION", 
					new Category [] { INTEREST_RATE_DERIVATIVE, OPTION },
					new ApplicableDelegate (IsInterestRateSwaption));

		/// <summary>
		/// A <see cref="Category"/> representing all forward rate agreements.
		/// </summary>
		public static readonly Category	FORWARD_RATE_AGREEMENT
			= new DelegatedRefinableCategory ("FORWARD RATE AGREEMENT", INTEREST_RATE_DERIVATIVE,
					new ApplicableDelegate (IsForwardRateAgreement));

		/// <summary>
		/// A <see cref="Category"/> representing all equity derivatives.
		/// </summary>
		public static readonly Category	EQUITY_DERIVATIVE
			= new AbstractCategory ("(EQUITY DERIVATIVE)", PRODUCT_TYPE);

		/// <summary>
		/// A <see cref="Category"/> representing all credit derivatives.
		/// </summary>
		public static readonly Category	CREDIT_DERIVATIVE
			= new AbstractCategory ("(CREDIT DERIVATIVE)", PRODUCT_TYPE);
		
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

		private static bool IsFxSpotForward (Object value)
		{
			return (XPath.Path ((XmlElement) value, "fxSingleLeg") != null);
		}

		private static bool IsFxSpot (Object value)
		{
			return (XPath.Path ((XmlElement) value, "fxSingleLeg", "exchangeRate", "forwardPoints") == null);
		}

		private static bool IsFxForward (Object value)
		{
			return (XPath.Path ((XmlElement) value, "fxSingleLeg", "exchangeRate", "forwardPoints") != null);
		}

		private static bool IsFxOption (Object value)
		{
			return ((XPath.Path ((XmlElement) value, "fxSimpleOption") != null) ||
					(XPath.Path ((XmlElement) value, "fxBarrierOption") != null));
		}

		private static bool IsFxBarrierOption (Object value)
		{
			return (XPath.Path ((XmlElement) value, "fxBarrierOption") != null);
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
		/// Test function used to detect forward rate agreements.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns>A <see cref="bool"/> value indicating if the test
		/// succeeded.</returns>
		private static bool IsForwardRateAgreement (Object value)
		{
			return (DOM.GetElementByLocalName (value as XmlElement, "fra") != null);
		}
	}
}