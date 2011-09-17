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

namespace HandCoded.Classification
{
	/// <summary>
	/// Delegate used to determine applicability.
	/// </summary>
	public delegate bool ApplicableDelegate (Object value);

	/// <summary>
	/// The <b>DelegatedRefinableCategory</b> class adapts a
	/// <see cref="RefinableCategory"/> to use delegate functions.
	/// </summary>
	public sealed class DelegatedRefinableCategory : RefinableCategory
	{
		/// <summary>
		/// Construct a <b>DelegatedRefinableCategory</b> with the given name.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>DelegatedRefinableCategory</b>.</param>
		/// <param name="function">The delegate to use for classification.</param>
		public DelegatedRefinableCategory (Classification classification, string name, ApplicableDelegate function)
			: base (classification, name)
		{
			this.function = function;
		}

		/// <summary>
		/// Construct a <b>DelegatedRefinableCategory</b> that is a sub-classification
		/// of another <see cref="Category"/>.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>DelegatedRefinableCategory</b>.</param>
		/// <param name="parent">The parent <see cref="Category"/>.</param>
		/// <param name="function">The delegate to use for classification.</param>
		public DelegatedRefinableCategory (Classification classification, string name, Category parent, ApplicableDelegate function)
			: base (classification, name, parent)
		{
			this.function = function;
		}

		/// <summary>
		/// Construct a <b>DelegatedRefinableCategory</b> that is a sub-classification
		/// of other <see cref="Category"/> instances.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>DelegatedRefinableCategory</b>.</param>
		/// <param name="parents">The parent <see cref="Category"/> instances.</param>
		/// <param name="function">The delegate to use for classification.</param>
		public DelegatedRefinableCategory (Classification classification, string name, Category [] parents, ApplicableDelegate function)
			: base (classification, name, parents)
		{
			this.function = function;
		}

		/// <summary>
		/// Determines if this <b>RefinableCategory</b> (and its sub-categories)
		/// is applicable to the given <see cref="Object"/>.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns><c>true</c> if the <see cref="Object"/> is applicable,
		/// <c>false</c> otherwise.</returns>
		protected override bool IsApplicable(Object value)
		{
			return (function (value));
		}

		/// <summary>
		/// Delegate used to determine applicability.
		/// </summary>
		private readonly ApplicableDelegate	function;
	}
}