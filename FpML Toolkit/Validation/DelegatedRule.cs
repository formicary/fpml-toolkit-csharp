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
using System.Xml;

using HandCoded.Xml;

namespace HandCoded.Validation
{
	/// <summary>
	/// The <b>RuleDelegate</b> delegate is used to access a function that will
	/// carry out a specific validation.
	/// </summary>
	public delegate bool RuleDelegate (string name, NodeIndex nodeIndex, ValidationErrorHandler errorHandler);

	/// <summary>
	/// The <b>DelegatedRule</b> class provides a implementation of the
	/// abstract <see cref="Rule"/> class that allows the use of delegates.
	/// </summary>
	public sealed class DelegatedRule : Rule
	{
		/// <summary>
		/// Constructs a <c>DelegatedRule</c> that applies to any document. 
		/// </summary>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="function">The <see cref="RuleDelegate"/> delegate function.</param>
		public DelegatedRule (string name, RuleDelegate function)
			: base (name)
		{
			this.function = function;
		}

		/// <summary>
		/// Constructs a <c>DelegatedRule</c> with the given name and that
		/// applies in the circumstances defines by its <c>Precondition</c>.
		/// </summary>
		/// <param name="precondition">A <c>Precondition</c> instance.</param>
		/// <param name="name">The unique name for the rule.</param>
		/// <param name="function">The <see cref="RuleDelegate"/> delegate function.</param>
		public DelegatedRule (Precondition precondition, string name, RuleDelegate function)
			: base (precondition, name)
		{
			this.function = function;
		}

		/// <summary>
		/// Determines if the <see cref="XmlDocument"/> instance indexed by the
		/// provided <see cref="NodeIndex"/> has business data content that passes a
		/// validation test. If errors are detected they will be reported through
		/// the <see cref="ValidationErrorHandler"/> instance passed as an argument.
		/// </summary>
		/// <remarks>Note that test returns <c>true</c> if it does not fail,
		/// including circumstances when the test is inapplicable to the
		/// <see cref="XmlDocument"/> under examination.
		/// </remarks>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> instance to examine.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> instance used to report
		/// validation failures.</param>
		/// <returns><c>false</c> if the validation test failed, <c>true</c> otherwise.
		/// </returns>
		protected override bool Validate(NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (function (Name, nodeIndex, errorHandler));
		}

		/// <summary>
		/// The <see cref="RuleDelegate"/> used to perform validation.
		/// </summary>
		private readonly RuleDelegate	function;
	}
}