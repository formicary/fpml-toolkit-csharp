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
using System.Xml;

using HandCoded.Meta;
using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>VersionPrecondition</b> class checks that the FpML root
	/// element contains a specific version string.
	/// </summary>
	public sealed class VersionPrecondition : Precondition
	{
		/// <summary>
		/// Constructs a <b>VersionPrecondition</b> that detects a specific
		/// version number.
		/// </summary>
		/// <param name="release">The required FpML release.</param>
		public VersionPrecondition (Release release)
		{
			this.release = release;
		}

		/// <summary>
		/// Evaluates this <see cref="Precondition"/> against the contents of the
		/// indicated <see cref="NodeIndex"/>.
		/// </summary>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
        /// <param name="cache">A cache of previously evaluated precondition results.</param>
		/// <returns>A <see cref="bool"/> value indicating the applicability of this
		/// <see cref="Precondition"/> to the <see cref="XmlDocument"/>.</returns>
		public override bool Evaluate (NodeIndex nodeIndex, Dictionary<Precondition, bool> cache)
		{
			foreach (string rootElement in release.RootElements) {
				XmlNodeList list = nodeIndex.GetElementsByName (rootElement);
				
				if (list.Count == 1) {
					XmlElement	fpml = list [0] as XmlElement;
				
					if (fpml.LocalName.Equals("FpML"))
						return (fpml.GetAttribute ("version").Equals (release.Version));
					else
						return (fpml.GetAttribute ("fpmlVersion").Equals (release.Version));					
				}
			}
			return (false);
		}

		/// <summary>
		/// Creates debugging string describing the precondition rule.
		/// </summary>
		/// <returns>A debugging string.</returns>
		public override string ToString()
		{
			return ("version=" + release);
		}

		/// <summary>
		/// The specific FpML version to match against. 
		/// </summary>
		private readonly Release	release;
	}
}