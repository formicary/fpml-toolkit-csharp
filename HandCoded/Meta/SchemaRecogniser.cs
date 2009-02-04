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

using System.Xml;

namespace HandCoded.Meta
{
    /// <summary>
    /// An instance implementing the <b>SchemaRecognise</b> interface is used
    /// to compare an <see cref="XmlDocument"/> to a <see cref="SchemaRelease"/> to
    /// see if it could be an instance of it.
    /// </summary>
	public abstract class SchemaRecogniser
	{
        /// <summary>
        /// Determines if the <see cref="XmlDocument"/> could be an instance of the
        /// indicated <see cref="SchemaRelease"/>.
        /// </summary>
        /// <param name="release">The potential <see cref="SchemaRelease"/>.</param>
        /// <param name="document">The <see cref="XmlDocument"/> to be tested.</param>
        /// <returns><b>true</b> if the <see cref="XmlDocument"/> could be an
        ///	instance of the indicated <see cref="SchemaRelease"/>.</returns>
        public abstract bool Recognises (SchemaRelease release, XmlDocument document);

		/// <summary>
		/// Constructs an <b>SchemaRecogniser</b> instance.
		/// </summary>
		protected SchemaRecogniser ()
		{ }
	}
}