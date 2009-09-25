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
	/// The <b>DefaultHelper</b> class provides an implementation of all the
	/// interfaces defined by the conversion objects and can be used by
	/// applications that don't want to define thier own instance.
	/// </summary>
	public class DefaultHelper : Conversions.R4_0__R4_1.IHelper
	{
		/// <summary>
		/// Constructs a <b>DefaultHelper</b> instance.
		/// </summary>
		public DefaultHelper()
		{
		}

		/// <summary>
		/// Returns a dummy value for the reference currency (???).
		/// </summary>
		/// <param name="context">The <see cref="XmlElement"/> of the fxFeature</param>
		/// <returns>The reference currency code value (e.g. GBP).</returns>
		public string GetReferenceCurrency (XmlElement context)
		{
			return ("???");
		}

		/// <summary>
		/// Returns a dummy value for the first quanto currency (???).
		/// </summary>
		/// <param name="context">The <see cref="XmlElement"/> of the fxFeature</param>
		/// <returns>The reference currency code value (e.g. GBP).</returns>
		public string GetQuantoCurrency1 (XmlElement context)
		{
			return ("???");
		}

		/// <summary>
		/// Returns a dummy value for the second quanto currency (???).
		/// </summary>
		/// <param name="context">The <see cref="XmlElement"/> of the fxFeature</param>
		/// <returns>The reference currency code value (e.g. GBP).</returns>
		public string GetQuantoCurrency2 (XmlElement context)
		{
			return ("???");
		}

		/// <summary>
		/// Returns a dummy value for the quanto currency basis.
		/// </summary>
		/// <param name="context">The <see cref="XmlElement"/> of the fxFeature</param>
		/// <returns>The quanto currency basis.</returns>
		public string GetQuantoBasis (XmlElement context)
		{
			return ("???");
		}
	}
}