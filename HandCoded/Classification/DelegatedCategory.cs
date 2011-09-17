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
using System.Collections;

namespace HandCoded.Classification
{
	/// <summary>
	/// Delegate used to classify an <see cref="Object"/>. 
	/// </summary>
	public delegate Category CategoryDelegate (Object value);

	/// <summary>
	/// The <b>DelegatedCategory</b> class adapts <see cref="Category"/> to use
	/// delegate functions.
	/// </summary>
	public sealed class DelegatedCategory : Category
	{
		/// <summary>
		/// Construct a <b>DelegatedCategory</b> with the given name.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>DelegatedCategory</b>.</param>
		/// <param name="function">The delegate to use for classification.</param>
		public DelegatedCategory (Classification classification, string name, CategoryDelegate function)
			: base (classification, name)
		{
			this.function = function;
		}

		/// <summary>
		/// Construct a <b>DelegatedCategory</b> that is a sub-classification
		/// of another <see cref="Category"/>.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>DelegatedCategory</b>.</param>
		/// <param name="parent">The parent <see cref="Category"/>.</param>
		/// <param name="function">The delegate to use for classification.</param>
		public DelegatedCategory (Classification classification, string name, Category parent, CategoryDelegate function)
			: base (classification, name, parent)
		{
			this.function = function;
		}

		/// <summary>
		/// Construct a <b>DelegatedCategory</b> that is a sub-classification
		/// of other <see cref="Category"/> instances.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>DelegatedCategory</b>.</param>
		/// <param name="parents">The parent <see cref="Category"/> instances.</param>
		/// <param name="function">The delegate to use for classification.</param>
		public DelegatedCategory (Classification classification, string name, Category [] parents, CategoryDelegate function)
			: base (classification, name, parents)
		{
			this.function = function;
		}

		/// <summary>
		/// Determine if the given <see cref="Object"/> can be classified by the
		/// graph of <b>Category</b> instances related to this entry point.
		/// </summary>
		/// <remarks>Uses the delegate provided at construction.</remarks>
		/// <param name="value">The <see cref="Object"/> to be classified.</param>
        /// <param name="visited">A <see cref="Hashtable"/> used to track visited nodes.</param>
        /// <returns>The matching <b>Category</b> for the <see cref="Object"/> or
		/// <c>null</c> if none could be determined.</returns>
        protected internal override Category Classify (Object value, Hashtable visited)
		{
            visited [this] = true;
			return (function (value));
		}

		/// <summary>
		/// The delegate function for classification.
		/// </summary>
		private readonly CategoryDelegate	function;
	}
}