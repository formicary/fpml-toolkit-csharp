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

namespace HandCoded.FpML.Schemes
{
	/// <summary>
	/// A <b>Scheme</b> instance holds a collection of values used to validate
	/// data fields that may only store a code value taken from the schemes
	/// domain.
	/// </summary>
	public abstract class Scheme
	{
		/// <summary>
		/// Contains the URI associated with the <b>Scheme</b>.
		/// </summary>
		public string Uri {
			get {
				return (uri);
			}
		}

		/// <summary>
		/// Contains the Canonical URI associated with the <b>Scheme</b>, <c>null</c> if none.
		/// </summary>
		public string CanonicalUri {
			get {
				return (canonicalUri);
			}
		}

		/// <summary>
		/// Determines if the given code value is valid within the scheme.
		/// </summary>
		/// <param name="code">The code value to be validated.</param>
		/// <returns><c>true</c> if the code value is valid, <c>false</c>
		/// otherwise.</returns>
		public abstract bool IsValid (string code);

		/// <summary>
		/// Constructs a <b>Scheme</b> for the given URI and canonical uri.
		/// </summary>
		/// <param name="uri">The associated URI.</param>
		/// <param name="canonicalUri">The associated canonical URI or <c>null</c>.</param>
		protected Scheme (string uri, string canonicalUri)
		{
			this.uri		  = uri;
			this.canonicalUri = canonicalUri;
		}

		/// <summary>
		/// Constructs a <b>Scheme</b> for the given URI.
		/// </summary>
		/// <param name="uri">The associated URI.</param>
		protected Scheme (string uri)
			: this (uri, null)
		{ }

		/// <summary>
		/// The associated URI for the <b>Scheme</b>.
		/// </summary>
		private readonly string		uri;

		/// <summary>
		/// The associated Canonical URI for the <b>Scheme</b>.
		/// </summary>
		private readonly string		canonicalUri;
	}
}