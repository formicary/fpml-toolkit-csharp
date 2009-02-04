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

namespace HandCoded.FpML.Meta
{
	/// <summary>
	/// The <b>SchemeDefaults</b> class provides a way to find out the URI
	/// associated with a particular type of coded data. An instance of
	/// <b>SchemeDefaults</b> exists for each supported release of FpML and is
	/// customised to return the appropriate URI for that version.
	/// </summary>
	public sealed class SchemeDefaults
	{
		/// <summary>
		/// Constructs a <b>SchemeDefaults</b> instance for an FpML 4-0 or
		/// later based instance in which scheme URI references can have schema
		/// defined default values.
		/// </summary>
		/// <param name="values">An array of scheme names and default URIs.</param>
		public SchemeDefaults (string [,] values)
		{
			for (int index = 0; index < (values.Length / 2); ++index)
				defaultValues.Add (values [index, 0], values [index, 1]);
		}
	
		/// <summary>
		/// Constructs a <b>SchemeDefaults</b> instance for a pre-FpML4-0
		/// release where schemes have global defaults on the FpML element.
		/// </summary>
		/// <param name="values">An array of scheme names and default URIs.</param>
		/// <param name="names">An array holding a scheme to default attribute map.</param>
		public SchemeDefaults (string [,] values, string [,] names)
			: this (values)
		{
			for (int index = 0; index < (names.Length / 2); ++index)
				defaultAttrs.Add (names [index, 0], names [index, 1]);
		}
		
		/// <summary>
		/// Returns the URI normally associated with the given scheme default
		/// attribute.
		/// </summary>
		/// <param name="name">The name of the scheme default attribute.</param>
		/// <returns>The default URI associated with this scheme default, or
		/// <c>null</c> if none.</returns>
		public string GetDefaultUriForAttribute (string name)
		{
            return (defaultValues.ContainsKey (name) ? defaultValues [name] : null);
		}
	
		/// <summary>
		/// Returns the name of the scheme default attribute that may provide
		/// the default value of a given scheme attribute. For example the
		/// <c>currencyScheme</c> attribute will default to the value of
		/// the <c>currencySchemeDefault</c> attribute.
		/// </summary>
		/// <param name="name">The name of the scheme attribute</param>
		/// <returns>The name of the scheme default attribute associated with
		/// this scheme attribute.</returns>
		public string GetDefaultAttributeForScheme (string name)
		{
			return (defaultAttrs.ContainsKey (name) ? defaultAttrs [name] : null);
		}
	
		/// <summary>
		/// A collection containing the scheme URI associated with each
		/// scheme default attribute.
		/// </summary>
		private Dictionary<string,string>	defaultValues
            = new Dictionary<string, string> ();

		/// <summary>
		/// A collection relating a scheme attribute name to its
		/// corresponding scheme default attribute (e.g. partyIdScheme uses
		/// partyIdSchemeDefault).
		/// </summary>
        private Dictionary<string, string>  defaultAttrs
            = new Dictionary<string, string> ();
	}
}