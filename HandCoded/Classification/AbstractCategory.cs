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
	/// An <b>AbstractCategory</b> is used to relate a set of sub-category
	/// instances. 
	/// </summary>
	public class AbstractCategory : Category
	{
		/// <summary>
		/// Construct an <b>AbstractCategory</b> with the given name.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>AbstractCategory</b>.</param>
		public AbstractCategory (Classification classification, string name)
			: base (classification, name)
		{ }

		/// <summary>
		/// Construct an <b>AbstractCategory</b> that is a sub-classification of another
		/// <see cref="Category"/>.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>AbstractCategory</b>.</param>
		/// <param name="parent">The parent <see cref="Category"/>.</param>
		public AbstractCategory (Classification classification, string name, Category parent)
			: base (classification, name, parent)
		{ }

		/// <summary>
		/// Construct an <b>AbstractCategory</b> that is a sub-classification of other
		/// <see cref="Category"/> instances.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>AbstractCategory</b>.</param>
		/// <param name="parents">The parent <see cref="Category"/> instances.</param>
		public AbstractCategory (Classification classification, string name, Category [] parents)
			: base (classification, name, parents)
		{ }

		/// <summary>
		/// Determine if the given <see cref="Object"/> can be classified by the
		/// graph of <b>Category</b> instances related to this entry point.
		/// </summary>
		/// <remarks>For an <b>AbstractCategory</b> this is determined by
		/// recursively checking each sub-category to see if it matches.</remarks>
		/// <param name="value">The <see cref="Object"/> to be classified.</param>
		/// <param name="visited">A <see cref="Hashtable"/> used to track visited nodes.</param>
		/// <returns>The matching <b>Category</b> for the <see cref="Object"/> or
		/// <c>null</c> if none could be determined.</returns>
		/// <exception cref="Exception">If two or more <see cref="Category"/>
		/// different instances claim to match the <see cref="Object"/>. This
		/// indicates an error in the construction of the graph and/or its
		/// tests.</exception>
		protected internal override Category Classify (Object value, Hashtable visited)
		{
			Category			result	= null;

			visited [this] = true;
			foreach (Category category in subCategories) {
				Category		match;

				if (!visited.ContainsKey (category) && ((match = category.Classify (value, visited)) != null)) {
					if ((result != null) && (result != match)) {
						if (result.IsA (match)) continue;

						throw new Exception ("Object cannot be unambiguously classified("
												+ result + " & " + match + ")");
					}
					result = match;
				}
			}
			return (result);
		}
	}
}