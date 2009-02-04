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

using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>ReferenceRule</b> class validates an intra-document reference
	/// recorded using 'ID' and 'IDREF' based attributes. When possible type
	/// information is used to locate possible matching elements otherwise element
	/// names are used instead.
	/// </summary>
	public class ReferenceRule : Rule
	{
		/// <summary>
		/// Construct a <b>ReferenceRule</b> instance that will locate
		/// context and target elements based on the data provided.
		/// </summary>
		/// <param name="precondition">A <see cref="Precondition"/> instance.</param>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="contextType">The schema type for the context element.</param>
		/// <param name="contextElements">An array of potential context element names.</param>
		/// <param name="targetType">The schema type for the target element.</param>
		/// <param name="targetElements">An array of potential target element names.</param>
		public ReferenceRule (Precondition precondition, string name,
			string contextType, string [] contextElements,
			string targetType, string [] targetElements)
				: base (precondition, name)
		{
			this.contextType 		= contextType;
			this.contextElements 	= contextElements;
			this.targetType			= targetType;
			this.targetElements 	= targetElements;
		}

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
			if (nodeIndex.HasTypeInformation) {
				string ns = FpMLRuleSet.DetermineNamespace (nodeIndex);
				
				return (Validate (
							nodeIndex.GetElementsByType (ns, contextType),
							nodeIndex.GetElementsByType (ns, targetType), errorHandler));
			}
		
			return (Validate (
						nodeIndex.GetElementsByName (contextElements),
						nodeIndex.GetElementsByName (targetElements), errorHandler));
		}

		/// <summary>
		/// Checks the elements in context <see cref="XmlNodeList"/> to see if they
		/// reference elements in the target <see cref="XmlNodeList"/>.
		/// </summary>
		/// <param name="contexts">A <see cref="XmlNodeList"/> of context <see cref="XmlElement"/> instances.</param>
		/// <param name="targets">A <see cref="XmlNodeList"/> of target <see cref="XmlElement"/> instances.</param>
		/// <param name="errorHandler">The <see cref="ValidationErrorHandler"/> used to report errors.</param>
		/// <returns><c>true</c> if the validation succeeded.</returns>
		protected bool Validate (XmlNodeList contexts, XmlNodeList targets, ValidationErrorHandler errorHandler)
		{
			bool		result = true;
			
			foreach (XmlElement context in contexts) {
				XmlAttribute	href	= context.GetAttributeNode ("href");
				
				if (href == null) continue;
				
				string		hrefValue = href.Value;
				bool		found	= false;
				
				foreach (XmlElement target in targets) {
					XmlAttribute	id	   = target.GetAttributeNode ("id");
					
					if (id == null)  continue;
					
					if (id.Value.Equals (hrefValue)) {
						found = true;
						break;
					}
				}
				
				if (found) continue;
				
				errorHandler ("305", context,
						"The @href attribute of a '" + contextType + "' element should " +
						"match with an @id attribute on a '" + targetType + "' element.",
						Name, hrefValue);
				
				result = false;
			}
			
			return (result);
		}

		/// <summary>
		/// Contains the name of context element's schema type.
		/// </summary>
		private readonly string		contextType;
	
		/// <summary>
		/// Contains an array of potential context element names.
		/// </summary>
		private readonly string []	contextElements;
	
		/// <summary>
		/// Contains the name of target element's schema type.
		/// </summary>
		private readonly string		targetType;
	
		/// <summary>
		/// Contains an array of potential target element names.
		/// </summary>
		private readonly string []	targetElements;
	}
}