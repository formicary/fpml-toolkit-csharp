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

using HandCoded.Meta;

namespace HandCoded.Acme
{
	/// <summary>
	/// The <b>Releases</b> class contains a set of static objects describing
	/// the HandCoded Acme extension schemas.
	/// </summary>
	public sealed class Releases
	{
		/// <summary>
		/// A <see cref="Specification"/> instance representing the Acme extensions
		/// specification as a whole.
		/// </summary>
		public static readonly Specification ACME
			= new Specification ("Acme");

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for the
		/// Acme 1-0 extension.
		/// </summary>
		public static readonly SchemaRelease R1_0
			= new SchemaRelease (ACME, "1-0",
					"http://www.handcoded.com/spec/2005/Acme-1-0", "acme-1-0.xsd",
					"acme", null, "");

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private Releases()
		{ }
	}
}