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
        /// Determines the namespace URI of the FpML document.
        /// </summary>
        /// <param name="nodeIndex">A <see cref="NodeIndex"/> of the entire document.</param>
        /// <returns>A <see cref="String"/> containing the namespace URI.</returns>
 	    protected static String DetermineNamespace (NodeIndex nodeIndex)
	    {
		    return (nodeIndex.Document.DocumentElement.NamespaceURI);
	    }
	}
}