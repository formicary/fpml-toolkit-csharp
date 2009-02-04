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

using HandCoded.Finance;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>FpMLRuleSet</b> is the abstract base class used by all other
	/// FpML rule sets.
	/// </summary>
	public abstract class FpMLRuleSet : Logic
	{
		/// <summary>
		/// Extracts an <see cref="Interval"/> from the data stored below the
		/// given context node.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <returns>An <see cref="Interval"/> constructed from the stored data.</returns>
		protected static Interval ToInterval (XmlElement context)
		{
			if (context != null) {
				try {
					return (new Interval (
						ToInteger (XPath.Path (context, "periodMultiplier")),
						Period.ForCode (ToToken (XPath.Path (context, "period")))));
				}
				catch (Exception) {
					return (null);
				}
			}
			return (null);
		}

		/// <summary>
		/// Determine if two <see cref="XmlElement"/> structures containing
		/// <b>currency</b> instances have the same currency codes belonging to
		/// the same currency code scheme.
		/// </summary>
		/// <param name="ccy1">The first currency <see cref="XmlElement"/>.</param>
		/// <param name="ccy2">The second currency <see cref="XmlElement"/>.</param>
		/// <returns><c>true</c> if both <b>currency</b> structures have the same code.</returns>
 		protected static bool IsSameCurrency (XmlElement ccy1, XmlElement ccy2)
		{
			string		uri1	= DOM.GetAttribute (ccy1, "currencyScheme");
			string		uri2	= DOM.GetAttribute (ccy2, "currencyScheme");
			
			if ((uri1 != null) && (uri2 != null) && uri1.Equals (uri2))
				return (Equal (ccy1, ccy2));
			
			return (false);
		}
	
        /// <summary>
        /// Determines the namespace URI of the FpML document.
        /// </summary>
        /// <param name="nodeIndex">A <see cref="NodeIndex"/> of the entire document.</param>
        /// <returns>A <see cref="String"/> containing the namespace URI.</returns>
 	    protected internal static String DetermineNamespace (NodeIndex nodeIndex)
	    {
		    return (nodeIndex.Document.DocumentElement.NamespaceURI);
	    }
	}
}