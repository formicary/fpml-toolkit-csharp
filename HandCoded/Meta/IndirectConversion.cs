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
	/// The <b>IndirectConversion</b> is used to chain multiple transformations
	/// together to create a multi-stage transformation. <b>IndirectConversion</b>
	/// instances are created during the search process to find a conversion
	/// path between two releases.
	/// </summary>
	public sealed class IndirectConversion : Conversion
	{
		/// <summary>
		/// Constructs a <b>IndirectConversion</b> instance that connects
		/// two other <see cref="Conversion"/> instances.
		/// </summary>
		/// <param name="first">The first <see cref="Conversion"/> to apply.</param>
		/// <param name="second">The second <see cref="Conversion"/> to apply.</param>
		public IndirectConversion (Conversion first, Conversion second)
		{
			this.first  = first;
			this.second = second;
		}

		/// <summary>
		/// Contains the <see cref="Release"/> that a <see cref="Conversion"/>
		/// converts from.
		/// </summary>
		public override Release SourceRelease {
			get {
				return (first.SourceRelease);
			}
		}

		/// <summary>
		/// Contains the <see cref="Release"/> that a <see cref="Conversion"/>
		/// converts to.
		/// </summary>
		public override Release TargetRelease {
			get {
				return (second.TargetRelease);
			}
		}

		/// <summary>
		/// Applies the <b>IndirectConversion</b> to a <see cref="XmlDocument"/> instance
		/// to create a new <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be converted.</param>
 		/// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
		/// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
		public override XmlDocument Convert (XmlDocument document, IHelper helper)
		{
			return (second.Convert (first.Convert (document, helper), helper));
		}

		/// <summary>
		/// Converts the state of the instance to a string.
		/// </summary>
		/// <returns>A debugging string.</returns>
		public override string ToString ()
		{
			return ("(" + first + "=>" + second + ")");
		}

		/// <summary>
		/// Returns a count of the number of stages in the conversion.
		/// </summary>
		/// <remarks>This is used to pick the shorter of two possible
		/// conversion paths when searching the release graph.</remarks>
		internal override int Complexity {
			get {
				return (first.Complexity + second.Complexity);
			}
		}

		/// <summary>
		/// The first <see cref="Conversion"/> instance.
		/// </summary>
		private readonly Conversion	first;

		/// <summary>
		/// The second <see cref="Conversion"/> instance.
		/// </summary>
		private readonly Conversion	second;
	}
}