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

using HandCoded.FpML.Meta;

namespace HandCoded.FpML.Xml
{
	/// <summary>
	/// The <b>Builder</b> class extends <see cref="HandCoded.Xml.Builder"/>
	/// with an understanding of FpML and its namespaces.
	/// </summary>
	public sealed class Builder : HandCoded.Xml.Builder
	{
		/// <summary>
		/// Constructs a <b>Builder</b> instance attached to the root element
		/// of the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to attach to.</param>
		public Builder (XmlDocument document)
			: base (document)
		{ }

		/// <summary>
		/// Constructs a <b>Builder</b> instance attached to the specified
		/// <see cref="XmlElement"/> of the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to attach to.</param>
		/// <param name="context">The initial context <see cref="XmlElement"/>.</param>
		public Builder (XmlDocument document, XmlElement context)
			: base (document, context)
		{ }

		/// <summary>
		/// Constructs a <b>Builder</b> instance attached the root element of
		/// a new FpML document.
		/// </summary>
		/// <param name="release">The FpML <see cref="SchemaRelease"/> to construct.</param>
		public Builder (SchemaRelease release)
			: base (release.NewInstance ("FpML"))
		{ }

		/// <summary>
		/// Constructs a <b>Builder</b> instance attached the root element of
		/// a new FpML document of the given <see cref="SchemaRelease"/>.
		/// </summary>
		/// <param name="release">The FpML <see cref="SchemaRelease"/> to construct.</param>
		/// <param name="type">The document or message type to construct.</param>
		public Builder (SchemaRelease release, string type)
			: base (release.NewInstance ("FpML"))
		{
			SetAttribute ("xsi:type", type);
		}
	}
}