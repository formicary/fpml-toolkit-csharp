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
using System.Configuration;
using System.Text;

using HandCoded.Classification;
using HandCoded.Classification.Xml;

namespace HandCoded.FpML.Classification
{
    /// <summary>
    /// The <b>ISDATaxonomy</b> class provides easy access the to the
    /// categories defined in the taxonomy provided by the ISDA product
    /// working groups initiated in response to the Dodd-Frank Act.
    /// </summary>
    public sealed class ISDATaxonomy
    {
        /// <summary>
        /// The default ISDA Product classification.
        /// </summary>
	    private static HandCoded.Classification.Classification	defaultClassification
		    = ClassificationLoader.Load (
                ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.ISDATaxonomy"]);

        /// <summary>
        /// A <see cref="Category"/> representing all product types.
        /// </summary>
	    public static readonly Category	ISDA
		    = defaultClassification.GetCategoryByName ("{ISDA}");

        /// <summary>
        /// Prevents an instance being created.
        /// </summary>
	    private ISDATaxonomy ()
	    { }
    }
}