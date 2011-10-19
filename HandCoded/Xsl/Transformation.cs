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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

using HandCoded.Framework;

namespace HandCoded.Xsl
{
    /// <summary>
    /// The <b>Transformation</b> class is a wrapper around the XSL APIs that
    /// allows a <see cref="XslTransform"/> to be created from a file and
    /// applied to DOM Document.
    /// </summary>
    public sealed class Transformation
    {
        /// <summary>
        /// Constructs a <b>Transformation</b> instance using the XSL source
	    /// in the specified file.
        /// </summary>
        /// <param name="filename">The XSL filename.</param>
        public Transformation (string filename)
        {
            transformer.Load (Application.PathTo (filename));
        }

        /// <summary>
        /// Applies the XSL transformation to the indicated DOM <see cref="XmlNode"/>
	    /// and returns the resulting document.
        /// </summary>
        /// <param name="node">The source DOM <see cref="XmlNode"/>.</param>
        /// <returns>A new DOM <see cref="XmlDocument"/> containing the result of the
	    ///	XSL transformation.</returns>
        public XmlDocument Transform (XmlNode node)
        {
            lock (transformer) {
                return (new XmlDocument ().Load (transformer.Transform (node, null)));
            }
        }

        /// <summary>
        /// The underlying <see cref="XslTransform"/> that will be applied to
	    /// a DOM instance.
        /// </summary>
        private XslTransform    transformer = new XslTransform (); 
    }
}