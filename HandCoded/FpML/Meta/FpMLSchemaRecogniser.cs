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
using System.Xml;

namespace HandCoded.FpML.Meta
{
	/// <summary>
	/// The <b>FpMLInstanceInitialiser</b> class provides additional
	/// initialisation logic for FpML schema based instances, in particular
	/// it ensures that the FpML version attribute is set to match the
	/// referenced schema.
	/// </summary>
	public sealed class FpMLSchemaRecogniser : HandCoded.Meta.DefaultSchemaRecogniser
	{
		/// <summary>
		/// Constructs a <b>FpMLSchemaRecogniser</b> that performs
		/// additional initialisation for FpML documents.
		/// </summary>
		public FpMLSchemaRecogniser ()
		{ }

		/// <summary>
        /// Determines if the <see cref="XmlDocument"/> could be an instance of the
	    /// indicated <see cref="SchemaRelease"/>. Also checks that the FpML version
		/// attribute matches the <see cref="SchemaRelease"/> instance.
        /// </summary>
        /// <param name="release">The potential <see cref="SchemaRelease"/>.</param>
        /// <param name="document">The <see cref="XmlDocument"/> to be tested.</param>
        /// <returns><b>true</b> if the <see cref="XmlDocument"/> could be an
	    ///	instance of the indicated <see cref="SchemaRelease"/>.</returns>
		public override bool Recognises (HandCoded.Meta.SchemaRelease release, XmlDocument document)
		{
			if (base.Recognises (release, document)) {
				int majorVersion = Int32.Parse (release.Version.Split('-')[0]);

				if (majorVersion <= 4) {
					if (document.DocumentElement.GetAttribute ("version").Equals (release.Version))
						return (true);
				}
				else {
					if (document.DocumentElement.GetAttribute ("fpmlVersion").Equals (release.Version))
						return (true);
				}
			}
			return (false);
		}
	}
}