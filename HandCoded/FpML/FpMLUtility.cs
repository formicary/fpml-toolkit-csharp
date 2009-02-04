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
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using HandCoded.FpML.Validation;
using HandCoded.Validation;
using HandCoded.Xml;

namespace HandCoded.FpML
{
	/// <summary>
	/// The <b>FpMLUtility</b> class contains a set of methods for performing 
	/// common operations on FpML document instances.
	/// </summary>
	public sealed class FpMLUtility
	{
		/// <summary>
		/// Parses the XML string provided passing any validation problems to
		/// the indicated <see cref="ValidationEventHandler"/>.
		/// </summary>
		/// <param name="xml">The XML string to be parsed.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <returns>A <see cref="XmlDocument"/> instance constructed from the XML document.</returns>
		public static XmlDocument Parse (string xml, ValidationEventHandler eventHandler)
		{
			return (Parse (false, xml, eventHandler));
		}

		/// <summary>
		/// Parses an XML document from the given <see cref="Stream"/> passing
		/// any reported errors to the <see cref="ValidationEventHandler"/> instance.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to process XML from.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <returns>A <see cref="XmlDocument"/> instance constructed from the XML document.</returns>
		public static XmlDocument Parse (Stream stream, ValidationEventHandler eventHandler)
		{
			return (Parse (false, stream, eventHandler));
		}

		/// <summary>
		/// Parses the XML string provided passing any validation problems to
		/// the indicated <see cref="ValidationEventHandler"/>.
		/// </summary>
		/// <param name="schemaOnly">Indicates only schema based documents to be processed.</param>
		/// <param name="xml">The XML string to be parsed.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <returns>A <see cref="XmlDocument"/> instance constructed from the XML document.</returns>
		public static XmlDocument Parse (bool schemaOnly, string xml, ValidationEventHandler eventHandler)
		{
			return (XmlUtility.ValidatingParse (
				(schemaOnly ? XmlUtility.SCHEMA_ONLY : XmlUtility.DTD_OR_SCHEMA),
				xml, XmlUtility.DefaultSchemaSet.XmlSchemaSet, XmlUtility.DefaultCatalog, eventHandler));
		}

		/// <summary>
		/// Parses an XML document from the given <see cref="Stream"/> passing
		/// any reported errors to the <see cref="ValidationEventHandler"/> instance.
		/// </summary>
		/// <param name="schemaOnly">Indicates only schema based documents to be processed.</param>
		/// <param name="stream">The <see cref="Stream"/> to process XML from.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <returns>A <see cref="XmlDocument"/> instance constructed from the XML document.</returns>
		public static XmlDocument Parse (bool schemaOnly, Stream stream, ValidationEventHandler eventHandler)
		{
			return (XmlUtility.ValidatingParse (
				(schemaOnly ? XmlUtility.SCHEMA_ONLY : XmlUtility.DTD_OR_SCHEMA),
				stream, XmlUtility.DefaultSchemaSet.XmlSchemaSet, XmlUtility.DefaultCatalog, eventHandler));
		}

		/// <summary>
		/// Uses the indicated <see cref="RuleSet"/> to perform a semantic validation of
		/// the <see cref="XmlDocument"/> and reports errors (if any). 
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be validated.</param>
		/// <param name="rules">The <see cref="RuleSet"/> to use.</param>
		/// <param name="errorHandler">The <see cref="ValidationErrorHandler"/> used to report issues.</param>
		/// <returns><b>true</b> if the <see cref="XmlDocument"/> successfully passed all
		/// applicable rules, <b>false</b> if one or more rules failed.</returns>
		public static bool Validate (XmlDocument document, RuleSet rules, ValidationErrorHandler errorHandler)
		{
			return (rules.Validate (document, errorHandler));
		}

		/// <summary>
		/// Uses the default FpML <see cref="RuleSet"/> to perform a semantic validation of
		/// the <see cref="XmlDocument"/> and reports errors (if any). 
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be validated.</param>
		/// <param name="errorHandler">The <see cref="ValidationErrorHandler"/> used to report issues.</param>
		/// <returns><b>true</b> if the <see cref="XmlDocument"/> successfully passed all
		/// applicable rules, <b>false</b> if one or more rules failed.</returns>
		public static bool Validate (XmlDocument document, ValidationErrorHandler errorHandler)
		{
			return (Validate (document, AllRules.Rules, errorHandler));
		}

		/// <summary>
		/// Attempts to parse an XML document from a string and then pass it through
		/// the specified validation rule set.
		/// </summary>
		/// <param name="xml">The XML string to be parsed.</param>
		/// <param name="rules">The <see cref="RuleSet"/> used for validation.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (string xml, RuleSet rules, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			XmlDocument		document = Parse (xml, eventHandler);

			return ((document != null) ? Validate (document, rules, errorHandler) : false);
		}

		/// <summary>
		/// Attempts to parse an XML document from a <see cref="Stream"/> and then
		/// pass it though the specified validation rule set.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to process XML from.</param>
		/// <param name="rules">The <see cref="RuleSet"/> used for validation.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (Stream stream, RuleSet rules, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			XmlDocument		document = Parse (stream, eventHandler);

			return ((document != null) ? Validate (document, errorHandler) : false);
		}

		/// <summary>
		/// Attempts to parse an XML document from a string and then pass it through
		/// the default FpML validation rule set.
		/// </summary>
		/// <param name="xml">The XML string to be parsed.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (string xml, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			return (ParseAndValidate (xml, AllRules.Rules, eventHandler, errorHandler));
		}

		/// <summary>
		/// Attempts to parse an XML document from a <see cref="Stream"/> and then
		/// pass it though the default FpML validation rule set.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to process XML from.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (Stream stream, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			return (ParseAndValidate (stream, AllRules.Rules, eventHandler, errorHandler));
		}

		/// <summary>
		/// Attempts to parse an XML document from a string and then pass it through
		/// the specified validation rule set.
		/// </summary>
		/// <param name="schemaOnly">Indicates only schema based documents to be processed.</param>
		/// <param name="xml">The XML string to be parsed.</param>
		/// <param name="rules">The <see cref="RuleSet"/> used for validation.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (bool schemaOnly, string xml, RuleSet rules, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			XmlDocument		document = Parse (schemaOnly, xml, eventHandler);

			return ((document != null) ? Validate (document, rules, errorHandler) : false);
		}

		/// <summary>
		/// Attempts to parse an XML document from a <see cref="Stream"/> and then
		/// pass it though the specified validation rule set.
		/// </summary>
		/// <param name="schemaOnly">Indicates only schema based documents to be processed.</param>
		/// <param name="stream">The <see cref="Stream"/> to process XML from.</param>
		/// <param name="rules">The <see cref="RuleSet"/> used for validation.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (bool schemaOnly, Stream stream, RuleSet rules, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			XmlDocument		document = Parse (schemaOnly, stream, eventHandler);

			return ((document != null) ? Validate (document, errorHandler) : false);
		}

		/// <summary>
		/// Attempts to parse an XML document from a string and then pass it through
		/// the default FpML validation rule set.
		/// </summary>
		/// <param name="schemaOnly">Indicates only schema based documents to be processed.</param>
		/// <param name="xml">The XML string to be parsed.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (bool schemaOnly, string xml, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			return (ParseAndValidate (schemaOnly, xml, AllRules.Rules, eventHandler, errorHandler));
		}

		/// <summary>
		/// Attempts to parse an XML document from a <see cref="Stream"/> and then
		/// pass it though the default FpML validation rule set.
		/// </summary>
		/// <param name="schemaOnly">Indicates only schema based documents to be processed.</param>
		/// <param name="stream">The <see cref="Stream"/> to process XML from.</param>
		/// <param name="eventHandler">A <see cref="ValidationEventHandler"/> used to report parser errors.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> used to report validation errors.</param>
		/// <returns></returns>
		public static bool ParseAndValidate (bool schemaOnly, Stream stream, ValidationEventHandler eventHandler, ValidationErrorHandler errorHandler)
		{
			return (ParseAndValidate (schemaOnly, stream, AllRules.Rules, eventHandler, errorHandler));
		}
			
		/// <summary>
		/// Ensures no instances can be created.
		/// </summary>
		private FpMLUtility()
		{ }
	}
}