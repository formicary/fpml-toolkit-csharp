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

namespace HandCoded.DSig
{
	/// <summary>
	/// The <b>Releases</b> class contains a set of static objects describing
	/// the W3C Digital Signature specification.
	/// </summary>
	public sealed class Releases
	{
		/// <summary>
		/// A <see cref="Specification"/> instance representing the Digital Signatures
		/// specification as a whole.
		/// </summary>
		public static readonly Specification DSIG
			= new Specification ("DSig");
	
		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for the
		/// DSIG recommendation.
		/// </summary>
		public static readonly SchemaRelease R1_0
			= new SchemaRelease (DSIG, "1-0",
				"http://www.w3.org/2000/09/xmldsig#", "xmldsig-core-schema.xsd",
				"dsig", null, "");
	
		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private Releases ()
		{ }
	}
}