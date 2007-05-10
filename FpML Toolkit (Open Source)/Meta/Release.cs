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
using System.Collections;
using System.Xml;

namespace HandCoded.Meta
{
	/// <summary>
	/// A <b>Release</b> represents an identifiable version of a
	/// <see cref="Specification"/>.
	/// </summary>
	public abstract class Release : IGrammar
	{
		/// <summary>
		/// Contains the owning <see cref="Specification"/>.
		/// </summary>
		public Specification Specification
		{
			get {
				return (specification);
			}
		}

		/// <summary>
		/// Contains the version identifier.
		/// </summary>
		public string Version
		{
			get {
				return (version);
			}
		}

		/// <summary>
		/// Contains a list of possible root element names for this <see cref="IGrammar"/>.
		/// </summary>
		public string [] RootElements
		{
			get {
				return (rootElements);
			}
		}

		
		/// <summary>
		/// Indicates that the schema is a pure extension.
		/// </summary>
		public bool IsExtensionOnly {
			get {
				return (rootElements == null);
			}
		}

		/// <summary>
		/// Contains a list of conversions for which this <b>Release</b> is the
		/// source format.
		/// </summary>
		public IEnumerable SourceConversions {
			get {
				return (sourceConversions);
			}
		}

		/// <summary>
		/// Contains a list of conversions for which this <b>Release</b> is the
		/// target format.
		/// </summary>
		public IEnumerable TargetConversions {
			get {
				return (targetConversions);
			}
		}
		
		/// <summary>
		/// Determines if the given <see cref="XmlDocument"/> is an instance of the XML
		/// grammar represented by this instance.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be tested.</param>
		/// <returns><c>true</c> if the <see cref="XmlDocument"/> is an instance of this
		/// <see cref="IGrammar"/>, <c>false</c> otherwise.</returns>
		public abstract bool IsInstance (XmlDocument document);

		/// <summary>
		/// Creates a new instance the XML grammar represented by this instance
		/// using the indicated element name as the root element for the document.
		/// </summary>
		/// <param name="rootElement">The name of the root element.</param>
		/// <returns>A new <see cref="XmlDocument"/> instance.</returns>
		public abstract XmlDocument NewInstance (String rootElement);

		/// <summary>
		/// Adds the indicated <see cref="Conversion"/> to the set of conversions
		/// that take this <b>Release</b> as the source format.
		/// </summary>
		/// <param name="conversion">The <see cref="Conversion"/> to be added.</param>
		public void AddSourceConversion (Conversion conversion)
		{
			sourceConversions.Add (conversion);
		}

		/// <summary>
		/// Adds the indicated <see cref="Conversion"/> to the set of conversions
		/// that take this <b>Release</b> as the target format.
		/// </summary>
		/// <param name="conversion">The <see cref="Conversion"/> to be added.</param>
		public void AddTargetConversion (Conversion conversion)
		{
			targetConversions.Add (conversion);
		}

		/// <summary>
		/// Constructs a <b>Release</b> instance and associates it with the
		/// indicated <see cref="Specification"/>.
		/// </summary>
		/// <param name="specification">The owning <see cref="Specification"/>.</param>
		/// <param name="version">The version identifier for this release.</param>
		/// <param name="rootElements">The set of possible root element.</param>
		protected Release (Specification specification, string version, string [] rootElements) {
			this.specification = specification;
			this.version	   = version;
			this.rootElements  = rootElements;

			specification.Add (this);
		}

		/// <summary>
		/// The owning <see cref="Specification"/>.
		/// </summary>
		private Specification		specification;

		/// <summary>
		/// The version identifier.
		/// </summary>
		private string				version;

		/// <summary>
		/// The set of possible root elements.
		/// </summary>
		private string []			rootElements;

		/// <summary>
		/// The set of conversions for which this is the source format.
		/// </summary>
		private ArrayList			sourceConversions = new ArrayList ();

		/// <summary>
		/// The set of conversions for which this is the target format.
		/// </summary>
		private ArrayList			targetConversions = new ArrayList ();
	}
}