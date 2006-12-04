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
		/// Forces the cache of standard FpML schemas to be populated.
		/// </summary>
		public static void PreloadSchemas ()
		{
			GetSchemas ();
		}

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
				xml, schemaCollection, resolver, eventHandler));
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
				stream, schemaCollection, resolver, eventHandler));
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
		/// Returns a <see cref="XmlResolver"/> instance pre-configured for the FpML
		/// DTD releases.
		/// </summary>
		/// <returns>A pre-configured <see cref="XmlResolver"/> instance.</returns>
		public static XmlResolver GetResolver ()
		{
			if (resolver == null)
				resolver = new Resolver ();

			return (resolver);
		}

		/// <summary>
		/// Returns a <see cref="XmlSchemaCollection"/> instance pre-configured
		/// for the FpML schema releases.
		/// </summary>
		/// <returns>A pre-configured <see cref="XmlSchemaCollection"/> instance.
		/// </returns>
#if DOTNET2_0
		public static XmlSchemaSet GetSchemas ()
		{
			if (schemaCollection == null) {
				string baseDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
                    ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.SchemasDirectory"]);

				schemaCollection = new XmlSchemaSet ();
#else
		public static XmlSchemaCollection GetSchemas ()
		{
			if (schemaCollection == null) {
				string baseDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
                    ConfigurationSettings.AppSettings ["HandCoded.FpML Toolkit.SchemasDirectory"]);

				schemaCollection = new XmlSchemaCollection ();
#endif

				// Must add DSIG before FpML schemas
				schemaCollection.Add (HandCoded.DSig.Releases.R1_0.NamespaceUri,
					Path.Combine (baseDirectory, "dsig/" + HandCoded.DSig.Releases.R1_0.SchemaLocation));

				// FpML Schemas
				schemaCollection.Add (HandCoded.FpML.Releases.R4_0.NamespaceUri,
					Path.Combine (baseDirectory, "fpml4-0/" + HandCoded.FpML.Releases.R4_0.SchemaLocation));
				schemaCollection.Add (HandCoded.FpML.Releases.R4_1.NamespaceUri,
					Path.Combine (baseDirectory, "fpml4-1/" + HandCoded.FpML.Releases.R4_1.SchemaLocation));
				schemaCollection.Add (HandCoded.FpML.Releases.TR4_2.NamespaceUri,
					Path.Combine (baseDirectory, "fpml4-2/" + HandCoded.FpML.Releases.TR4_2.SchemaLocation));

				// Acme Schemas
				schemaCollection.Add (HandCoded.Acme.Releases.R1_0.NamespaceUri,
					Path.Combine (baseDirectory, "acme1-0/" + HandCoded.Acme.Releases.R1_0.SchemaLocation));
			}			
			return (schemaCollection);
		}

		/// <summary>
		/// The <see cref="XmlResolver"/> used to locate DTDs.
		/// </summary>
		private static XmlResolver			resolver			= null;

#if DOTNET2_0
		/// <summary>
		/// The <see cref="XmlSchemaSet"/> used to hold cached schemas.
		/// </summary>
		private static XmlSchemaSet			schemaCollection = null;
#else
		/// <summary>
		/// The <see cref="XmlSchemaCollection"/> used to hold cached schemas.
		/// </summary>
		private static XmlSchemaCollection	schemaCollection	= null;
#endif

		/// <summary>
		/// The <b>Resolver</b> class is a customised <see cref="XmlUrlResolver"/>
		/// configured to the public names used in FpML 1-0, 2-0 and 3-0 instances
		/// to the appropriate DTD file.
		/// </summary>
		private class Resolver : XmlUrlResolver
		{
			/// <summary>
			/// Constructs a <b>Resolver</b> and populates its URI to filename
			/// mapping table.
			/// </summary>
			public Resolver ()
			{
				string baseDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
#if DOTNET2_0
					ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.SchemasDirectory"]);
#else
                    ConfigurationSettings.AppSettings ["HandCoded.FpML Toolkit.SchemasDirectory"]);
#endif

				uriMap.Add (Releases.R1_0.PublicId, 
					Path.Combine (baseDirectory, "fpml1-0/" + Releases.R1_0.SystemId));
				uriMap.Add (Releases.R2_0.PublicId,
					Path.Combine (baseDirectory, "fpml2-0/" + Releases.R2_0.SystemId));
				uriMap.Add (Releases.TR3_0.PublicId,
					Path.Combine (baseDirectory, "fpml3-0/" + Releases.TR3_0.SystemId));
			}

			/// <summary>
			/// Uses the mapping table to return the appropriate local filename
			/// when a reference is made to an FpML DTD by means of its public
			/// name.
			/// </summary>
			/// <param name="baseUri">The base URI.</param>
			/// <param name="relativeUri">The relative URI.</param>
			/// <returns>The resolved URI.</returns>
			public override Uri ResolveUri(Uri baseUri, string relativeUri)
			{
				if (uriMap.ContainsKey (relativeUri))
					relativeUri = uriMap [relativeUri] as string;
				
				return (base.ResolveUri (baseUri, relativeUri));
			}

			/// <summary>
			/// The <see cref="Hashtable"/> used to record public name to system
			/// filename mappings.
			/// </summary>
			private Hashtable			uriMap	= new Hashtable ();
		}
			
		/// <summary>
		/// Ensures no instances can be created.
		/// </summary>
		private FpMLUtility()
		{ }
	}
}