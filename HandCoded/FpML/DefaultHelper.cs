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

namespace HandCoded.FpML
{
	/// <summary>
	/// Summary description for DefaultHelper.
	/// </summary>
	public class DefaultHelper : Conversions.R4_0__R4_1.IHelper
	{
		public DefaultHelper()
		{
		}

		public string GetReferenceCurrency (XmlElement context)
		{
			return ("???");
		}

		public string GetQuantoCurrency1 (XmlElement context)
		{
			return ("???");
		}

		public string GetQuantoCurrency2 (XmlElement context)
		{
			return ("???");
		}
	}
}
