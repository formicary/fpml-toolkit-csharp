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
using System.Collections.Generic;

namespace HandCoded.Classification
{
	/// <summary>
	/// The <b>Category</b> class represents a possible classification of an
	/// <see cref="Object"/>. <b>Category</b> instances can be linked to
	/// each other to create graphs of inter-related items, such as multiple
	/// inheritance based structures.
	/// </summary>
	public abstract class Category
	{
		/// <summary>
		/// Contains the name of this <b>Category</b>.
		/// </summary>
		public string Name {
			get {
				return (name);
			}
		}

		/// <summary>
		/// Contains an <see cref="IEnumerable"/> of super-categories.
		/// </summary>
		public IEnumerable<Category> SuperCategories {
			get {
				return (superCategories);
			}
		}

		/// <summary>
		/// Contains an <see cref="IEnumerable"/> of sub-categories.
		/// </summary>
		public IEnumerable<Category> SubCategories {
			get {
				return (subCategories);
			}
		}

		/// <summary>
		/// Converts the <b>Specification</b> to a string for debugging.
		/// </summary>
		/// <returns>A text description of the instance.</returns>
		public override string ToString ()
		{
			return (name);
		}

		/// <summary>
		/// Returns a hash code for this instance based on its name.
		/// </summary>
		/// <returns>The instance hash code.</returns>
		public override int GetHashCode ()
		{
			return (name.GetHashCode ());
		}

		/// <summary>
		/// Determines if this <b>Category</b> is the same as or is a
		/// subcategory of another <b>Category</b> (e.g. a Swaption
		/// is-a Option).
		/// </summary>
		/// <param name="superCategory">The super category.</param>
		/// <returns><b>true</b> if there is a 'is-a' relationship
		/// between the two categories.</returns>
		public bool IsA (Category superCategory)
		{
			if (this == superCategory) return (true);

			foreach (Category parent in superCategories)
				if (parent.IsA (superCategory)) return (true);

			return (false);
		}

		/// <summary>
		/// Determine if the given <see cref="Object"/> can be classified by the
		/// graph of <b>Category</b> instances related to this entry point.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be classified.</param>
		/// <returns>The matching <b>Category</b> for the <see cref="Object"/> or
		/// <c>null</c> if none could be determined.</returns>
		public Category Classify (Object value)
		{
			return (Classify (value, new Hashtable ()));
		}

		/// <summary>
		/// <b>Category</b> instances that reference this instance.
		/// </summary>
		protected internal List<Category> superCategories
            = new List<Category> ();

		/// <summary>
		/// <b>Category</b> instances referenced by this instance.
		/// </summary>
		protected internal List<Category> subCategories
            = new List<Category> ();

		/// <summary>
		/// Construct a <b>Category</b> with the given name.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>Category</b>.</param>
		protected Category (Classification classification, string name)
		{
			this.name = name;

            classification.Add (this);
		}

		/// <summary>
		/// Construct a <b>Category</b> that is a sub-classification of another
		/// <b>Category</b>.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>Category</b>.</param>
		/// <param name="parent">The parent <b>Category</b>.</param>
		protected Category (Classification classification, string name, Category parent)
			: this (classification, name)
		{
			this.superCategories.Add (parent);
			parent.subCategories.Add (this);
		}

		/// <summary>
		/// Construct a <b>Category</b> that is a sub-classification of other
		/// <b>Category</b> instances.
		/// </summary>
        /// <param name="classification">The owning <see cref="Classification"/>.</param>
		/// <param name="name">The name of this <b>Category</b>.</param>
		/// <param name="parents">The parent <b>Category</b> instances.</param>
		protected Category (Classification classification, string name, Category [] parents)
			: this (classification, name)
		{
			foreach (Category parent in parents) {
				this.superCategories.Add (parent);
				parent.subCategories.Add (this);
			}
		}

		/// <summary>
		/// Determine if the given <see cref="Object"/> can be classified by the
		/// graph of <b>Category</b> instances related to this entry point.
		/// </summary>
		/// <param name="value">The <see cref="Object"/> to be classified.</param>
		/// <param name="visited">A <see cref="Hashtable"/> used to track visited nodes.</param>
		/// <returns>The matching <b>Category</b> for the <see cref="Object"/> or
		/// <c>null</c> if none could be determined.</returns>
		protected internal abstract Category Classify (Object value, Hashtable visited);

		/// <summary>
		/// The name of this <b>Category</b>.
		/// </summary>
		private readonly string		name;
	}
}