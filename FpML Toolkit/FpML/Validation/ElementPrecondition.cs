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
using System.Text;
using System.Xml;

using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// Instances if the <b>ElementPrecondition</b> class check for the presence
	/// of specific elements in the source document.
	/// </summary>
	public class ElementPrecondition : Precondition
	{
		/// <summary>
		/// Constructs an <b>ElementPrecondition</b> that checks for multiple
		/// element names.
		/// </summary>
		/// <param name="elements">The names of the elements.</param>
		public ElementPrecondition (string [] elements)
		{
			this.elements = elements;
		}

		/// <summary>
		/// Constructs an <b>ElementPrecondition</b> that checks for a single
		/// element name.
		/// </summary>
		/// <param name="element">The name of the element.</param>
		public ElementPrecondition (string element)
			: this (new string [] { element })
		{ }

		/// <summary>
		/// Evaluates this <see cref="Precondition"/> against the contents of the
		/// indicated <see cref="NodeIndex"/>.
		/// </summary>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/></param>
		/// <returns>A <see cref="bool"/> value indicating the applicability of this
		/// <see cref="Precondition"/> to the <see cref="XmlDocument"/>.</returns>
		public override bool Evaluate (NodeIndex nodeIndex)
		{
			foreach (string element in elements) {
				if (nodeIndex.GetElementsByName (element).Count > 0)
					return (true);
			}
			return (false);
		}

		/// <summary>
		/// Creates debugging string describing the precondition rule.
		/// </summary>
		/// <returns>A debugging string.</returns>
		public override string ToString()
		{
			StringBuilder	builder = new StringBuilder ();

			foreach (string name in elements) {
				if (builder.Length > 0) builder.Append (", ");
				builder.Append (name);
			}
			return ("contains (" + builder + ")");
		}

		/// <summary>
		/// An array of element names.
		/// </summary>		
		private string []		elements;
	}
}