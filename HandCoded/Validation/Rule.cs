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

using HandCoded.Xml;

namespace HandCoded.Validation
{
	/// <summary>
	/// A <b>Rule</b> instance encapsulates a validation rule that can be
	/// tested against a <see cref="XmlDocument"/>.
	/// </summary>
	public abstract class Rule : Validator
	{		
		/// <summary>
		/// Contains the <see cref="Precondition"/> for this rule.
		/// </summary>
		public Precondition Precondition {
			get {
				return (precondition);
			}
		}

		/// <summary>
		/// Contains the unique name of the rule.
		/// </summary>
		public string Name {
			get {
				return (name);
			}
		}

        /// <summary>
        /// Returns a reference to the named <c>Rule</c> instance if it exists.
        /// </summary>
        /// <param name="name">The name of the required <c>Rule</c>.</param>
        /// <returns>The corresponding <c>Rule</c> instance or <c>null</c>.</returns>
        public static Rule ForName (string name)
        {
            return (extent.ContainsKey (name) ? extent [name] : null);
        }

		/// <summary>
		/// Determines if the <b>Rule</b> applies to a document but evaluating
		/// its <see cref="Precondition"/>.
		/// </summary>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> of the document.</param>
		/// <returns><c>true</c> if the <b>Rule</b> applies, <c>false</c> otherwise.</returns>
		public bool AppliesTo (NodeIndex nodeIndex)
		{
			return (precondition.Evaluate (nodeIndex, new Dictionary<Precondition,bool> ()));
		}

		/// <summary>
		/// Constructs a <c>Rule</c> with the given name and that applies in
		/// the circumstances defines by its <see cref="Precondition"/>.
		/// </summary>
		/// <param name="precondition">A <see cref="Precondition"/> instance.</param>
		/// <param name="name">The unique name for the rule.</param>
		protected Rule (Precondition precondition, string name)
		{
			this.precondition = precondition;
			this.name		  = name;

            extent [name] = this;
		}

		/// <summary>
		/// Constructs a <c>Rule</c> that applies to any document. 
		/// </summary>
		/// <param name="name">The unique name for the rule.</param>
		protected Rule (string name)
			: this (Precondition.ALWAYS, name)
		{ }

        /// <summary>
        /// The set of all <c>Rule</c> instances indexed by name.
        /// </summary>
        private static Dictionary<string, Rule> extent = new Dictionary<string, Rule> ();

		/// <summary>
		/// The <see cref="Precondition"/> for this rule.
		/// </summary>
		private readonly Precondition	precondition;

		/// <summary>
		/// The unique name of this rule.
		/// </summary>
		private readonly string		name;
	}
}