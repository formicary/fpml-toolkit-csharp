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
using System.Collections.Generic;
using System.Xml;

using log4net;

namespace HandCoded.FpML.Schemes
{
	/// <summary>
	/// The <b>SchemeCollection</b> class maintains a set of <see cref="Scheme"/>
	/// instances that are used to validate data within an FpML document.
	/// </summary>
	public sealed class SchemeCollection
	{
		/// <summary>
		/// Constructs an empty <b>SchemeCollection</b>
		/// </summary>
		public SchemeCollection ()
		{ }

		/// <summary>
		/// Parses the XML data file indicated by the URI to extract default sets
		/// of closed schemes.
		/// </summary>
		/// <param name="uri">The URI of the source XML document.</param>
		public void Parse (string uri)
		{
			CachedScheme	scheme		= null;
			string			code		= null;
			string			source		= null;
			string			description	= null;
			string			text		= null;
			XmlReader		reader		= new XmlTextReader (uri);

			while (reader.Read ()) {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					{
						if (reader.LocalName.Equals ("scheme")) {
							if (reader ["uri"] != null) {
								if (reader ["canonicalUri"] != null)
									Add (scheme = new ClosedScheme (reader ["uri"], reader ["canonicalUri"]));
								else	
									Add (scheme = new ClosedScheme (reader ["uri"]));

								log.Debug ("Added scheme " + reader ["uri"]);
							}
							else {
								log.Warn ("uri attribute missing from scheme in bootstrap definitions");
								scheme = null;
							}
						}
						else if (reader.LocalName.Equals ("schemeValue")) {
							if (reader ["name"] != null)
								code = reader ["name"];
							else {
								log.Warn ("name attribute missing from schemeValue in bootstrap definitions");
								code = null;
							}

							source = reader ["schemeValueSource"];
							description = null;

							if (reader.IsEmptyElement) {
								if ((scheme != null) && (code != null)) {
									scheme.Add (new Value (code, source, description));
								}
							}
						}
						else if (reader.LocalName.Equals ("paragraph"))
							text = null;
						break;
					}

				case XmlNodeType.EndElement:
					{
						if (reader.LocalName.Equals ("schemeValue")) {
							if ((scheme != null) && (code != null)) {
								scheme.Add (new Value (code, source, description));
							}
						}
						else if (reader.LocalName.Equals ("paragraph")) {
							if (description == null)
								description = text.Trim ();
						}

						break;
					}

				case XmlNodeType.Text:
					{
						text = reader.Value;
						break;
					}
				}
			}
			reader.Close ();
		}

		/// <summary>
		/// Adds the given <see cref="Scheme"/> instance to the extent set. If
		/// the <see cref="Scheme"/> has the same URI as an existing entry then
		/// it will replace the old definition.
		/// </summary>
		/// <param name="scheme">The <see cref="Scheme"/> instance to be added.</param>
		/// <returns>A reference to the old <see cref="Scheme"/> object replaced
		/// by this action, <c>null</c> otherwise.</returns>
		public Scheme Add (Scheme scheme)
		{
			Scheme		resultA	= schemes.ContainsKey (scheme.Uri) ? schemes [scheme.Uri] : null;
			Scheme		resultB	= null;
			
			if (scheme.CanonicalUri != null) {
				resultB = schemes.ContainsKey (scheme.CanonicalUri) ? schemes [scheme.CanonicalUri] : null;

				schemes [scheme.CanonicalUri] = scheme;
			}
			schemes [scheme.Uri] = scheme;
			
			return ((resultA != null) ? resultA : resultB);
		}

		/// <summary>
		/// Removes the indicated <see cref="Scheme"/> instance from the extent
		/// set.
		/// </summary>
		/// <param name="scheme">The <see cref="Scheme"/> to be removed.</param>
		/// <returns>A reference to the <see cref="Scheme"/> if it was in the
		/// extent set, <c>null</c> if it was not.</returns>
		public Scheme Remove (Scheme scheme)
		{
			Scheme		resultA	= null;
			Scheme		resultB = null;

			if (schemes.ContainsKey (scheme.Uri))
				schemes.Remove ((resultA = scheme).Uri);

			if (scheme.CanonicalUri != null)
				if (schemes.ContainsKey (scheme.CanonicalUri))
					schemes.Remove ((resultB = scheme).CanonicalUri);
 
			return ((resultA != null) ? resultA : resultB);
		}

		/// <summary>
		/// Removes the <see cref="Scheme"/> identified by the given URI if it
		/// is contained within the extent set.
		/// </summary>
		/// <param name="uri">The URI of the target <see cref="Scheme"/>.</param>
		/// <returns>A reference to the <see cref="Scheme"/> if it was in the
		/// extent set, <c>null</c> if it was not.</returns>
		public Scheme Remove (string uri)
		{
			Scheme		scheme	= FindSchemeForUri (uri);

			if (scheme != null)
				return (Remove (scheme));

			return (null);
		}

		/// <summary>
		/// Attempts to locate the <see cref="Scheme"/> with the given URI in the
		/// cache.
		/// </summary>
		/// <param name="uri">The URI of the required <see cref="Scheme"/>.</param>
		/// <returns>The corresponding <see cref="Scheme"/> instance or
		/// <c>null</c> if it was not in the cache.</returns>
		public Scheme FindSchemeForUri (string uri)
		{
			return (schemes.ContainsKey (uri) ? schemes [uri] : null);
		}

		/// <summary>
		/// <see cref="ILog"/> instance used to record problems.
		/// </summary>
		private static ILog			log
			= LogManager.GetLogger (typeof (SchemeCollection));

		/// <summary>
		/// The extent set of all <see cref="Scheme"/> instances.
		/// </summary>
		private Dictionary<string, Scheme>  schemes
            = new Dictionary<string, Scheme> ();
	}
}