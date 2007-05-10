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
	/// The <b>Validator</b> class defines a standard API for requesting
	/// the validation of the business data content of an XML instance document.
	/// </summary>
	public abstract class Validator
	{

		/// <summary>
		/// Determines if the given <see cref="XmlDocument"/> instance's business
		/// data content passes a validation test. If errors are detected these will
		/// be reported through the <see cref="ValidationErrorHandler"/> instance passed as an
		/// argument.
		/// </summary>
		/// <remarks>Note that test returns <c>true</c> if it does not fail,
		/// including circumstances when the test is inapplicable to the
		/// <see cref="XmlDocument"/> under examination.
		/// </remarks>
		/// <param name="document">The <see cref="XmlDocument"/> instance to examine.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> delegate used to report
		/// validation failures.</param>
		/// <returns><c>false</c> if the validation test failed, <c>true</c> otherwise.
		/// </returns>
		public bool Validate (XmlDocument document, ValidationErrorHandler errorHandler)
		{
			return (Validate (new NodeIndex (document), errorHandler));
		}

		/// <summary>
		/// Constructs a <b>Validator</b> instance.
		/// </summary>
		protected Validator ()
		{ }

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
		protected abstract bool Validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler);

		internal bool PerformValidation (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			return (Validate (nodeIndex, errorHandler));
		}
	}
}