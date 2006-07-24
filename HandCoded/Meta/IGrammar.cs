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

namespace HandCoded.Meta
{
	/// <summary>
	/// The <b>IGrammar</b> interface defines a standard way of creating and
	/// identifying instances of an XML grammar. 
	/// </summary>
	public interface IGrammar
	{
		/// <summary>
		/// Contains a list of possible root element names for this <b>IGrammar</b>.
		/// </summary>
		string [] RootElements {
			get;
		}

		/// <summary>
		/// Determines if the given <see cref="XmlDocument"/> is an instance of the XML
		/// grammar represented by this instance.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be tested.</param>
		/// <returns><c>true</c> if the <see cref="XmlDocument"/> is an instance of this
		/// <b>IGrammar</b>, <c>false</c> otherwise.</returns>
		bool IsInstance (XmlDocument document);

		/// <summary>
		/// Creates a new instance the XML grammar represented by this instance
		/// using the indicated element name as the root element for the document.
		/// </summary>
		/// <param name="rootElement">The name of the root element.</param>
		/// <returns>A new <see cref="XmlDocument"/> instance.</returns>
		XmlDocument NewInstance (string rootElement);
	}
}