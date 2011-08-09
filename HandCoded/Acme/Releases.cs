// Copyright (C),2005-2011 HandCoded Software Ltd.
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
using System.Collections.Generic;
using System.Text;

using HandCoded.Meta;

namespace HandCoded.Acme
{
	/// <summary>
	/// The <b>Releases</b> class contains a set of static objects describing
	/// the Acme specification and its various releases.
	/// </summary>
    public sealed class Releases
    {
		/// <summary>
		/// A <see cref="Specification"/> instance representing the Acme extension
        /// schemas as a whole.
		/// </summary>
		public static readonly Specification ACME
			= Specification.ForName ("Acme");

		/// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for the
		/// Acme 1-0 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R1_0
			= ACME.GetReleaseForVersion ("1-0") as SchemaRelease;

        /// <summary>
		/// A <see cref="SchemaRelease"/> instance containing the details for the
		/// Acme 2-0 recommendation.
		/// </summary>
		public static readonly SchemaRelease	R2_0
			= ACME.GetReleaseForVersion ("2-0") as SchemaRelease;
    }
}