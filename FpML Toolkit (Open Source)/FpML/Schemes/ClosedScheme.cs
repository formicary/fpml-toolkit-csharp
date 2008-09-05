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
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HandCoded.FpML.Schemes
{
	/// <summary>
	/// The <b>ClosedScheme</b> class provides an extened implementation of
	/// <see cref="CachedScheme"/> that implements the <see cref="IEnumerable"/>
	/// and <see cref="IMatchable"/> interfaces.
	/// </summary>
	public class ClosedScheme : CachedScheme, IEnumerable, IMatchable
	{
		/// <summary>
		/// Constructs a <b>ClosedScheme</b> instance for the indicated scheme
		/// URI.
		/// </summary>
		/// <param name="uri">The URI used to reference the scheme.</param>
		public ClosedScheme (string uri)
			: base (uri)
		{ }

		/// <summary>
		/// Constructs a <b>ClosedScheme</b> instance for the indicated scheme
		/// URI and canonical URI.
		/// </summary>
		/// <param name="uri">The URI used to reference the scheme.</param>
		/// <param name="canonicalUri">The canonical URI or <c>null</c>.</param>
		public ClosedScheme (string uri, string canonicalUri)
			: base (uri, canonicalUri)
		{ }

		/// <summary>
		/// Contains an array of all the possible <see cref="Value"/> instances
		/// supported by the <see cref="Scheme"/> in no particular order.
		/// </summary>
		public Value [] Values {
			get {
				Value []		result  = new Value [values.Count];

				values.Values.CopyTo (result, 0);
				return (result);
			}
		}

		/// <summary>
		/// Contains the number of values held in the <see cref="Scheme"/>.
		/// </summary>
		public int Count {
			get {
				return (values.Count);
			}
		}
		/// <summary>
		/// Finds the subset of scheme values that match the provided regular
		/// expression. No particular ordering of the values is enforced.
		/// </summary>
		/// <param name="pattern">A regular expression.</param>
		/// <returns>An array containing the matching <see cref="Value"/> instances.</returns>
		public Value [] Like (string pattern)
		{
			ArrayList		matches = new ArrayList ();
			Regex			regex	= new Regex (pattern);
			Value []		result;

			foreach (Value value in values.Values)
				if (regex.IsMatch (value.Code)) matches.Add (value);

			result = new Value [matches.Count];
			matches.CopyTo (result, 0);

			return (result);
		}
	}
}