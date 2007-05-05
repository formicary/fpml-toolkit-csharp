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

namespace HandCoded.Classification
{
	/// <summary>
	/// A <b>RefinableCategory</b> instance can be used to provide a 'catch-all'
	/// category should its sub-categories fail to isolate a specific variant.
	/// </summary>
	public abstract class RefinableCategory : AbstractCategory
	{
		/// <summary>
		/// Construct a <b>RefinableCategory</b> with the given name.
		/// </summary>
		/// <param name="name">The name of this <b>RefinableCategory</b>.</param>
		protected RefinableCategory (string name)
			: base (name)
		{ }

		/// <summary>
		/// Construct a <b>RefinableCategory</b> that is a sub-classification of another
		/// <see cref="Category"/>.
		/// </summary>
		/// <param name="name">The name of this <b>RefinableCategory</b>.</param>
		/// <param name="parent">The parent <see cref="Category"/>.</param>
		protected RefinableCategory (string name, Category parent)
			: base (name, parent)
		{ }

		/// <summary>
		/// Construct a <b>RefinableCategory</b> that is a sub-classification of other
		/// <see cref="Category"/> instances.
		/// </summary>
		/// <param name="name">The name of this <b>RefinableCategory</b>.</param>
		/// <param name="parents">The parent <see cref="Category"/> instances.</param>
		protected RefinableCategory (string name, Category [] parents)
			: base (name, parents)
		{ }

		/// <summary>
		/// Determine if the given <see cref="Object"/> can be classified by the
		/// graph of <b>Category</b> instances related to this entry point.
		/// </summary>
		/// <remarks>A <b>RefinableCategory</b> first determines if it (and its
		/// sub-categories) is applicable to the <see cref="Object"/> before
		/// attempting to classify it. If an applicable <see cref="Object"/> is
		/// not claimed by a sub-category then the <b>RefinableCategory</b> will
		/// 'generically' claim it.</remarks>
		/// <param name="value">The <see cref="Object"/> to be classified.</param>
		/// <param name="visited">A <see cref="Hashtable"/> used to track visited nodes.</param>
		/// <returns>The matching <b>Category</b> for the <see cref="Object"/> or
		/// <c>null</c> if none could be determined.</returns>
		protected internal override Category Classify (Object value, Hashtable visited)
		{
			Category		match	= null;

			visited [this] = true;
			if (IsApplicable (value)) {
				if ((match = base.Classify (value, visited)) == null)
					match = this;
			}
			return (match);
		}

		/// <summary>
		/// Determines if this <b>RefinableCategory</b> (and its sub-categories)
		/// is applicable to the given <see cref="Object"/>.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be tested.</param>
		/// <returns><c>true</c> if the <see cref="Object"/> is applicable,
		/// <c>false</c> otherwise.</returns>
		protected abstract bool IsApplicable (Object value);
	}
}