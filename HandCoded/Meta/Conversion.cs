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
using System.Xml;

namespace HandCoded.Meta
{
	/// <summary>
	/// A <b>Conversion</b> instance encapsulates the knowledge of how to
	/// transform an XML document from one <see cref="Release"/> of a
	/// <see cref="Specification"/> to another.
	/// </summary>
	/// <remarks>
	/// It is expected that most conversions will be between different releases of
	/// the same specification but the code allows for the discovery of
	/// inter-specification conversions.
	/// </remarks>
	public abstract class Conversion
	{
		/// <summary>
		/// Attempts to find a <b>Conversion</b> that will transform a
		/// <see cref="XmlDocument"/> between the two specified releases. The releases
		/// must be different, null transformations are not allowed.
		/// </summary>
		/// <param name="source">The source <see cref="Release"/> to convert from.</param>
		/// <param name="target">The target <see cref="Release"/> to convert to.</param>
		/// <returns>A <b>Conversion</b> instance that implements the
		/// transformation or <c>null</c> if one could not be found.</returns>
		public static Conversion ConversionFor (Release source, Release target)
		{
			return ((source != target) ? DepthFirstSearch (source, target, new Stack ()) : null);
		}
	
		/// <summary>
		/// Contains the <see cref="Release"/> that a <b>Conversion</b> converts from.
		/// </summary>
		public abstract Release SourceRelease {
			get;
		}

		/// <summary>
		/// Contains the <see cref="Release"/> that a <b>Conversion</b> converts to.
		/// </summary>
		public abstract Release TargetRelease {
			get;
		}

		/// <summary>
		/// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
		/// to create a new <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be converted.</param>
		/// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
		/// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
		public abstract XmlDocument Convert (XmlDocument document, IHelper helper);

		/// <summary>
		/// Constructs a <b>Conversion</b> instance.
		/// </summary>
		protected Conversion()
		{ }

		/// <summary>
		/// Returns a count of the number of stages in the conversion.
		/// </summary>
		/// <remarks>This is used to pick the shorter of two possible
		/// conversion paths when searching the release graph.</remarks>
		internal abstract int Complexity {
			get;
		}

		/// <summary>
		/// Recursively explores the <see cref="Release"/> definitions to determine
		/// the shortest conversion path between two releases.
		/// </summary>
		/// <param name="source">The source <see cref="Release"/> for the search.</param>
		/// <param name="target">The target <see cref="Release"/> for the search.</param>
		/// <param name="stack">A <see cref="Stack"/> used to detect cycles.</param>
		/// <returns>A <B>Conversion</B> that transforms between the source and
		/// target releases or <c>null</c> if no conversion is possible.</returns>
		private static Conversion DepthFirstSearch (Release source, Release target, Stack stack)
		{
			Conversion	best = null;

			if (!stack.Contains (source)) {
				stack.Push (source);

				foreach (Conversion first in source.SourceConversions) {
					Release		release = first.TargetRelease;
					Conversion	result	= null;

					if (release == target)
						result = first;
					else {
						Conversion second = DepthFirstSearch (release, target, stack);
							if (second != null)
						result = new IndirectConversion (first, second);
					}
				
					if (result != null) {
						if ((best == null)||(result.Complexity < best.Complexity))
							best = result;
					}
				}
				stack.Pop ();
			}
			return (best);
		}
	}
}