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

using HandCoded.FpML.Meta;
using HandCoded.FpML.Schemes;
using HandCoded.Meta;
using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>SchemeRule</b> class provides the logic to validate FpML scheme
	/// code values for all versions of FpML.
	/// </summary>
	public class SchemeRule : Rule
	{
		/// <summary>
		/// Constructs a <b>SchemeRule</b> with the given name and that applies in
		/// the circumstances defined by its <see cref="Precondition"/>.
		/// </summary>
		/// <param name="precondition">A <see cref="Precondition"/> instance.</param>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="parentNames">The local names of the parent elements or <b>null</b>.</param>
		/// <param name="elementNames">An array of element names using the same scheme type for validation.</param>
		/// <param name="attributeName">The name of the attribute containing the scheme URI.</param>
		public SchemeRule (Precondition precondition, string name, string [] parentNames,
				string [] elementNames, string attributeName)
			: base (precondition, name)
		{
			this.parentNames   = parentNames;
			this.elementNames  = elementNames;
			this.attributeName = attributeName;
		}

		/// <summary>
		/// Constructs a <c>SchemeRule</c> with the given name and that applies in
		/// the circumstances defined by its <see cref="Precondition"/>.
		/// </summary>
		/// <param name="precondition">A <see cref="Precondition"/> instance.</param>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="elementNames">An array of element names using the same scheme type for validation.</param>
		/// <param name="attributeName">The name of the attribute containing the scheme URI.</param>
		public SchemeRule (Precondition precondition, string name, string [] elementNames, string attributeName)
			: this (precondition, name, null, elementNames, attributeName)
		{ }

		/// <summary>
		/// Constructs a <c>SchemeRule</c> with the given name and that applies in
		/// the circumstances defined by its <see cref="Precondition"/>.
		/// </summary>
		/// <param name="precondition">A <see cref="Precondition"/> instance.</param>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="elementName">An element name that uses a scheme for validation.</param>
		/// <param name="attributeName">The name of the attribute containing the scheme URI.</param>
		public SchemeRule (Precondition precondition, string name, string elementName, string attributeName)
			: this (precondition, name, null, new string [] { elementName }, attributeName)
		{ }

		/// <summary>
		/// Constructs a <c>SchemeRule</c> with the given name and that applies in
		/// the circumstances defined by its <see cref="Precondition"/>.
		/// </summary>
		/// <param name="precondition">A <see cref="Precondition"/> instance.</param>
		/// <param name="name">The unique name for the rule.</param>
        /// <param name="parentName">The local name of the parent element or <b>null</b>.</param>
        /// <param name="elementName">An element name that uses a scheme for validation.</param>
		/// <param name="attributeName">The name of the attribute containing the scheme URI.</param>
		public SchemeRule (Precondition precondition, string name, string parentName, string elementName, string attributeName)
			: this (precondition, name, new string [] { parentName }, new string [] { elementName }, attributeName)
		{ }

		/// <summary>
		/// Constructs a <c>SchemeRule</c> that applies to any document. 
		/// </summary>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="elementNames">An array of element names using the same scheme type for validation.</param>
		/// <param name="attributeName">The name of the attribute containing the scheme URI.</param>
		public SchemeRule (string name, string [] elementNames, string attributeName)
			: this (Precondition.ALWAYS, name, null, elementNames, attributeName)
		{ }

		/// <summary>
		/// Constructs a <c>SchemeRule</c> that applies to any document. 
		/// </summary>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="elementName">An element name that uses a scheme for validation.</param>
		/// <param name="attributeName">The name of the attribute containing the scheme URI.</param>
		public SchemeRule (string name, string elementName, string attributeName)
			: this (Precondition.ALWAYS, name, null, new string [] { elementName }, attributeName)
		{ }

		/// <summary>
		/// Constructs a <c>SchemeRule</c> that applies to any document. 
		/// </summary>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="parentName">The local name of the parent <b>XmlElement</b>.</param>
		/// <param name="elementName">An element name that uses a scheme for validation.</param>
		/// <param name="attributeName">The name of the attribute containing the scheme URI.</param>
		public SchemeRule (string name, string parentName, string elementName, string attributeName)
			: this (Precondition.ALWAYS, name, new string [] { parentName}, new string [] { elementName }, attributeName)
		{ }

		/// <summary>
		/// Validates all the elements registered at construction using the
		/// <see cref="NodeIndex"/> to locate them as quickly as possible.
		/// </summary>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> of the test document.</param>
		/// <param name="errorHandler">The <see cref="ValidationErrorHandler"/> used to report issues.</param>
		/// <returns><c>true</c> if the code values pass the checks, <c>false</c>
		/// otherwise.</returns>
		protected override bool Validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			for (int index = 0; index < elementNames.Length; ++index) {
				XmlNodeList	list = nodeIndex.GetElementsByName (elementNames [index]);
				
				if (parentNames == null)
					result &= Validate (list, errorHandler);
				else {
					MutableNodeList		targets = new MutableNodeList ();
					
					foreach (XmlElement context in list) {
						XmlNode    parent  = context.ParentNode;
						
						if ((parent.NodeType == XmlNodeType.Element) &&
								parent.LocalName.Equals (parentNames [index]))
							targets.Add (context);
					}
					result &= Validate (targets, errorHandler);
				}
			}
			return (result);
		}

		/// <summary>
		/// Validates the data content of a set of elements by locating the scheme
		/// identified by the scheme attribute or a default on the root element
		/// for pre FpML-4-0 instances.
		/// </summary>
		/// <param name="list">An <see cref="XmlNodeList"/> of elements to check.</param>
		/// <param name="errorHandler">The <see cref="ValidationErrorHandler"/> used to report issues.</param>
		/// <returns><c>true</c> if the code values pass the checks, <c>false</c>
		/// otherwise.</returns>
		protected bool Validate (XmlNodeList list, ValidationErrorHandler errorHandler)
		{
			bool		result	= true;

			if (list.Count > 0) {
				XmlElement	fpml	= DOM.GetParent (list [0] as XmlElement);
				string		version	= null;

				// Find the FpML root node
				while (fpml != null) {
					if (fpml.LocalName.Equals ("FpML")) {
						version = fpml.GetAttribute ("version");
						break;
					}
					if (fpml.HasAttribute ("fpmlVersion")) {
						version = fpml.GetAttribute ("fpmlVersion");
						break;
					}
					fpml = DOM.GetParent (fpml);
				}

				SchemeCollection	schemes =
					(Releases.FPML.GetReleaseForVersion (version) as ISchemeAccess).SchemeCollection;

				foreach (XmlElement context in list) {
					// If there is no local override then look for a default on the FpML
					// element in pre 3-0 versions.
					string uri = context.GetAttribute (attributeName);
					if (((uri == null) || (uri.Length == 0)) && (version != null)) {
						string [] components = version.Split ('-');
						if ((components.Length > 1) && (components [0].CompareTo ("4") < 0)) {
							ISchemeAccess provider
								= Specification.ReleaseForDocument (context.OwnerDocument) as ISchemeAccess;

							string name = provider.SchemeDefaults.GetDefaultAttributeForScheme (attributeName);
							if (name != null) uri = fpml.GetAttribute (name);
						}
					}

					if ((uri == null) || (uri.Length == 0)) {
						errorHandler ("305", context,
							"A qualifying scheme URI has not been defined for this element",
							Name, context.LocalName);

						result = false;
						continue;
					}

					Scheme scheme = schemes.FindSchemeForUri (uri);
					if (scheme == null) {
						errorHandler ("305", context,
							"An unrecognized scheme URI has been used as a qualifier",
							Name, uri);

						result = false;
						continue;
					}
					
					string value = context.InnerText.Trim ();
					if (scheme.IsValid (value)) continue;

					errorHandler ("305", context,
						"The code value '" + value + "' is not valid in scheme '" + scheme.Uri + "'",
						Name, value);

					result = false;
				}
			}
			return (result);
		}

		/// <summary>
		/// A list of the local parent element names corresponding to the
		/// <b>elementNames</b>. If the array has a <b>null</b> value
		/// then no parent element qualification is performed.
		/// </summary>
		private readonly string []	parentNames;

		/// <summary>
		/// The set of element names to be validated.
		/// </summary>
		private readonly string	[]	elementNames;

		/// <summary>
		/// The name of attribute containing the scheme URI.
		/// </summary>
		private readonly string		attributeName;
	}
}