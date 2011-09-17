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
using System.Text;

namespace HandCoded.Classification
{
    /// <summary>
    /// A <b>Classification</b> instance provides a container for a set of
    /// related <see cref="Category"/> instances.
    /// </summary>
    public sealed class Classification
    {
        /// <summary>
        /// Constructs an empty <b>Classification</b> instance.
        /// </summary>
	    public Classification ()
	    { }
    	
        /// <summary>
        /// Locates a <see cref="Category"/> within the <b>Classification</b>
	    /// using its name string.
        /// </summary>
        /// <param name="name">The target name string.</param>
        /// <returns>The matching <see cref="Category"/> instance or <c>null</c>
        /// if no match was found.</returns>
	    public Category GetCategoryByName (String name)
	    {
		    return (extent [name]);
	    }

        /// <summary>
        /// Adds a <see cref="Category"/> instance to the extent set.
        /// </summary>
        /// <param name="category">The <see cref="Category"/> instance to add.</param>
	    internal void Add (Category category)
	    {
		    extent [category.Name] = category;
	    }
	
        /// <summary>
        /// The underlying hash table of categories indexed by name.
        /// </summary>
	    private Dictionary<String, Category>	extent
		    = new Dictionary<String, Category> ();
    }
}
