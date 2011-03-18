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
using System.Xml;

namespace HandCoded.Meta
{
	/// <summary>
	/// The <b>DirectConversion</b> class describes a transformation
	/// implemented by a derived class.
	/// </summary>
	public abstract class DirectConversion : Conversion
	{
		/// <summary>
		/// Contains the <see cref="Release"/> that a <see cref="Conversion"/> converts from.
		/// </summary>
		public override Release SourceRelease {
			get {
				return (sourceRelease);
			}
		}

		/// <summary>
		/// Contains the <see cref="Release"/> that a <see cref="Conversion"/> converts to.
		/// </summary>
		public override Release TargetRelease {
			get {
				return (targetRelease);
			}
		}

		/// <summary>
		/// Converts the state of the instance to a string.
		/// </summary>
		/// <returns>A debugging string.</returns>
		public override string ToString ()
		{
			return ("(" + sourceRelease.Version + "->" + targetRelease.Version + ")");
		}

		/// <summary>
		/// Constructs a <b>DirectConversion</b> that will transform between
		/// the specified releases. 
		/// </summary>
		/// <param name="sourceRelease">The <see cref="Release"/> to convert from.</param>
		/// <param name="targetRelease">The <see cerf="Release"/> to convert to.</param>
		protected DirectConversion (Release sourceRelease, Release targetRelease)
		{
		    this.sourceRelease = sourceRelease;
		    this.targetRelease = targetRelease;
    		
		    if ((sourceRelease != null) && (targetRelease != null)) {
			    sourceRelease.AddSourceConversion (this);
			    targetRelease.AddTargetConversion (this);
		    }
        }

		/// <summary>
		/// Returns a count of the number of stages in the conversion.
		/// <para>For a <b>DirectConversion</b> this will always be one.</para>
		/// </summary>
		/// <remarks>This is used to pick the shorter of two possible
		/// conversion paths when searching the release graph.</remarks>
		internal override int Complexity {
			get {
				return (1);
			}
		}

		/// <summary>
		/// The source <see cref="Release"/> instance.
		/// </summary>
		private readonly Release	sourceRelease;

		/// <summary>
		/// The target <see cref="Release"/> instance.
		/// </summary>
		private readonly Release	targetRelease;
	}
}