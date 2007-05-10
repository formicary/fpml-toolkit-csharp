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
using System.Collections;

namespace HandCoded.FpML.Schemes
{
	/// <summary>
	/// The <b>CachedScheme</b> class provides in-memory storage for the codes
	/// defined for its domain and thier associated descriptions.
	/// </summary>
	public class CachedScheme : Scheme
	{
		/// <summary>
		/// Constructs a <b>CachedScheme</b> instance for the indicated scheme
		/// URI
		/// </summary>
		/// <param name="uri">The URI used to reference the scheme.</param>
		public CachedScheme (string uri)
			: base (uri)
		{ }

		/// <summary>
		/// Determines if the given code value is valid within the scheme.
		/// </summary>
		/// <param name="code">The code value to be validated.</param>
		/// <returns><c>true</c> if the code value is valid, <c>false</c>
		/// otherwise.</returns>
		public override bool IsValid (string code)
		{
			return (values.ContainsKey (code));
		}

		/// <summary>
		/// Provides the underlying storage for the code values.
		/// </summary>
		protected Hashtable		values = new Hashtable ();

		/// <summary>
		/// Adds a <see cref="Value"/> instance to the extent set manange by
		/// this instance. If the new value has the same code as an existing
		/// entry it will replace it and a reference to the old instance will
		/// be returned to the caller.
		/// </summary>
		/// <param name="value">The scheme <see cref="Value"/> to be added.</param>
		/// <returns>The old <see cref="Value"/> instance having the same code
		/// as the new one, otherwise <c>null</c>.</returns>
		protected internal Value Add (Value value)
		{
			Value		result = values [value.Code] as Value;

			values [value.Code] = value;
			return (result);
		}
	}
}