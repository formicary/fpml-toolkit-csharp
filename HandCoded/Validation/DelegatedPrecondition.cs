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
	/// The <b>PreconditionDelegate</b> delegate is used to access a function
	/// that will carry out a specific evaluation.
	/// </summary>
	public delegate bool PreconditionDelegate (NodeIndex nodeIndex, Dictionary<Precondition, bool> cache);

	/// <summary>
	/// The <b>DelegatedPrecondition</b> class provides an implementation of the
	/// abstract <see cref="Precondition"/> class that allows the use of delegates.
	/// </summary>
	public sealed class DelegatedPrecondition : Precondition
	{
		/// <summary>
		/// Constructs a <b>DelegatedPrecondition</b> that will use the given
		/// delegate for evaluation.
		/// </summary>
		/// <param name="function">The delegate evaluation function.</param>
		public DelegatedPrecondition(PreconditionDelegate function)
		{
			this.function = function;
		}

		/// <summary>
		/// Evaluates this <b>Precondition</b> against the contents of the
		/// indicated <see cref="NodeIndex"/>.
		/// </summary>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
        /// <param name="cache">A cache of previously evaluated precondition results.</param>
		/// <returns>A <c>bool</c> value indicating the applicability of this
		/// <b>Precondition</b> to the <see cref="XmlDocument"/>.</returns>
		public override bool Evaluate (NodeIndex nodeIndex, Dictionary<Precondition, Boolean> cache)
		{
			return (function (nodeIndex, cache));
		}

		/// <summary>
		/// The delegate function that handles evalulation.
		/// </summary>
		private readonly PreconditionDelegate	function;
	}
}