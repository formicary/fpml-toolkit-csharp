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

using HandCoded.Validation;

namespace HandCoded.FpML.Validation
{
	/// <summary>
	/// The <b>FpMLRules</b> class contains a <see cref="RuleSet"/> that holds
	/// all of the FpML defined validation <see cref="Rule"/> instances.
	/// </summary>
	public class FpMLRules
	{
		/// <summary>
		/// Contains the <see cref="RuleSet"/>.
		/// </summary>
		public static RuleSet Rules {
			get {
				return (rules);
			}
		}

		/// <summary>
		/// The <see cref="RuleSet"/> used to hold the <see cref="Rule"/>
		/// instances.
		/// </summary>
		private static readonly RuleSet	rules = new RuleSet ();

		/// <summary>
		/// Ensures that no instances can be constructed.
		/// </summary>
		private FpMLRules ()
		{ }

		/// <summary>
		/// Initialises the <see cref="RuleSet"/> with copies of all the FpML
		/// defined <see cref="Rule"/> instances.
		/// </summary>
		static FpMLRules ()
		{
			rules.Add (SharedRules.Rules);
			rules.Add (IrdRules.Rules);
			rules.Add (CdsRules.Rules);
			rules.Add (EqdRules.Rules);
            rules.Add (FxRules.Rules);
        }
	}
}