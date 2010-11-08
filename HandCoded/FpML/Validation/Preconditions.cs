// Copyright (C),2005-2010 HandCoded Software Ltd.
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

using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>Preconditions</b> class contains a set of <see cref="Precondition"/>
	/// instances used by the rules to define thier applicability.
	/// </summary>
	public sealed class Preconditions
	{
		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 1-0 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R1_0
			= new VersionPrecondition (Releases.R1_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 2-0 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R2_0
			= new VersionPrecondition (Releases.R2_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 3-0 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R3_0
			= new VersionPrecondition (Releases.R3_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 4-0 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R4_0
			= new VersionPrecondition (Releases.R4_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 4-1 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R4_1
			= new VersionPrecondition (Releases.R4_1);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 4-2 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R4_2
			= new VersionPrecondition (Releases.R4_2);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 4-3 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R4_3
			= new VersionPrecondition (Releases.R4_3);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 4-4 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R4_4
			= new VersionPrecondition (Releases.R4_4);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 4-5 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R4_5
			= new VersionPrecondition (Releases.R4_5);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 4-6 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R4_6
			= new VersionPrecondition (Releases.R4_6);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects FpML 4-7 compatible
        /// documents.
        /// </summary>
        public static readonly Precondition R4_7
            = new VersionPrecondition (Releases.R4_7);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects FpML 4-8 compatible
        /// documents.
        /// </summary>
        public static readonly Precondition R4_8
            = new VersionPrecondition (Releases.R4_8);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects FpML 4-9 compatible
        /// documents.
        /// </summary>
        public static readonly Precondition R4_9
            = new VersionPrecondition (Releases.R4_9);

        /// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 5-0 confirmation
		/// compatible documents.
		/// </summary>
		public static readonly Precondition	R5_0_CONFIRMATION
			= new VersionPrecondition (Releases.R5_0_CONFIRMATION);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 5-0 reporting
		/// compatible documents.
		/// </summary>
		public static readonly Precondition	R5_0_REPORTING
			= new VersionPrecondition (Releases.R5_0_REPORTING);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 5-0 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R5_0
			= Precondition.Or (R5_0_CONFIRMATION, R5_0_REPORTING);

        /// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 5-1 confirmation
		/// compatible documents.
		/// </summary>
		public static readonly Precondition	R5_1_CONFIRMATION
			= new VersionPrecondition (Releases.R5_1_CONFIRMATION);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 5-1 reporting
		/// compatible documents.
		/// </summary>
		public static readonly Precondition	R5_1_REPORTING
			= new VersionPrecondition (Releases.R5_1_REPORTING);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML 5-1 compatible
		/// documents.
		/// </summary>
		public static readonly Precondition	R5_1
			= Precondition.Or (R5_1_CONFIRMATION, R5_1_REPORTING);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions that use
		/// XPointer syntax for intra-document links.
		/// </summary>
		public static readonly Precondition	R1_0__R2_0
			= new VersionRangePrecondition (Releases.R1_0, Releases.R2_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects either FpML 1-0,
		/// 2-0 or 3-0 compatible documents.
		/// </summary>
		public static readonly Precondition	R1_0__R3_0
			= new VersionRangePrecondition (Releases.R1_0, Releases.R3_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML version 2-0 and
		/// 3-0.
		/// </summary>
		public static readonly Precondition	R2_0__R3_0
			= new VersionRangePrecondition (Releases.R2_0, Releases.R3_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML version 3-0 and
		/// 4-0.
		/// </summary>
		public static readonly Precondition	R3_0__R4_0
			= new VersionRangePrecondition (Releases.R3_0, Releases.R4_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 5-1 and
		/// later.
		/// </summary>
		public static readonly Precondition	R5_1__LATER
			= new VersionRangePrecondition (Releases.R5_1_CONFIRMATION, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 5-0 and
		/// later.
		/// </summary>
		public static readonly Precondition	R5_0__LATER
			= new VersionRangePrecondition (Releases.R5_0_CONFIRMATION, null);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects FpML versions 4-9 and
        /// later.
        /// </summary>
        public static readonly Precondition R4_9__LATER
            = new VersionRangePrecondition (Releases.R4_9, null);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects FpML versions 4-8 and
        /// later.
        /// </summary>
        public static readonly Precondition R4_8__LATER
            = new VersionRangePrecondition (Releases.R4_8, null);

        /// <summary>
        /// A <see cref="Precondition"/> instance that detects FpML versions 4-7 and
        /// later.
        /// </summary>
        public static readonly Precondition R4_7__LATER
            = new VersionRangePrecondition (Releases.R4_7, null);

        /// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 4-6 and
		/// later.
		/// </summary>
		public static readonly Precondition	R4_6__LATER
			= new VersionRangePrecondition (Releases.R4_6, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 4-5 and
		/// later.
		/// </summary>
		public static readonly Precondition	R4_5__LATER
			= new VersionRangePrecondition (Releases.R4_5, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 4-4 and
		/// later.
		/// </summary>
		public static readonly Precondition	R4_4__LATER
			= new VersionRangePrecondition (Releases.R4_4, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 4-3 and
		/// later.
		/// </summary>
		public static readonly Precondition	R4_3__LATER
			= new VersionRangePrecondition (Releases.R4_3, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 4-2 and
		/// later.
		/// </summary>
		public static readonly Precondition	R4_2__LATER
			= new VersionRangePrecondition (Releases.R4_2, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 4-1 and
		/// later.
		/// </summary>
		public static readonly Precondition	R4_1__LATER
			= new VersionRangePrecondition (Releases.R4_1, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML versions 4-0 and
		/// later.
		/// </summary>
		public static readonly Precondition	R4_0__LATER
			= new VersionRangePrecondition (Releases.R4_0, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML version 3-0 and
		/// later.
		/// </summary>
		public static readonly Precondition	R3_0__LATER
			= new VersionRangePrecondition (Releases.R3_0, null);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects FpML version 2-0 and
		/// later.
		/// </summary>
		public static readonly Precondition	R2_0__LATER
			= new VersionRangePrecondition (Releases.R2_0, Releases.R3_0);

		/// <summary>
		/// A <see cref="Precondition"/> instance that detects all FpML versions 
		/// except 4-0.
		/// </summary>
		public static readonly Precondition	NOT_R4_0
			= Precondition.Not (R4_0);

		/// <summary>
		/// Ensures that no instances can be created.
		/// </summary>
		private Preconditions()
		{ }
	}
}