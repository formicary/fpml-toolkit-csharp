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

namespace HandCoded.FpML.Schemes
{
	/// <summary>
	/// The <b>Value</b> class is used to represent code values within thier
	/// corresponding <see cref="Scheme"/> instance's extent set.
	/// </summary>
	public sealed class Value
	{
		/// <summary>
		/// Constructs a <b>Value</b> given a code, its source (e.g. who defines
		/// it - FpML or someone else) and a description.
		/// </summary>
		/// <param name="code">The code string</param>
		/// <param name="source">The source identifier</param>
		/// <param name="description">Some descriptive text.</param>
		public Value (string code, string source, string description)
		{
			this.code		 = code;
			this.source		 = source;
			this.description = description;
		}

		/// <summary>
		/// Contains the code value.
		/// </summary>
		public string Code {
			get {
				return (code);
			}
		}

		/// <summary>
		/// Contains the source identifier.
		/// </summary>
		public string Source {
			get {
				return (source);
			}
		}

		/// <summary>
		/// Contains a description.
		/// </summary>
		public string Description {
			get {
				return (description);
			}
		}

		/// <summary>
		/// Returns a hash code for the instance based on the value of the code
		/// string.
		/// </summary>
		/// <returns>The hash code value.</returns>
		public override int GetHashCode ()
		{
			return (code.GetHashCode ());
		}

		/// <summary>
		/// Produces a debugging string describing the object.
		/// </summary>
		/// <returns>The debugging information string.</returns>
		public override string ToString ()
		{
			return (GetType ().FullName + " [" + ToDebug () + "]");
		}

		/// <summary>
		/// The code string value.
		/// </summary>
		private readonly string		code;

		/// <summary>
		/// The source identifier value.
		/// </summary>
		private readonly string		source;

		/// <summary>
		/// The description string.
		/// </summary>
		private readonly string		description;

		/// <summary>
		/// Produces a debugging string based on the instance members.
		/// </summary>
		/// <returns>The debugging information string.</returns>
		private string ToDebug ()
		{
			return ("code=\"" + code + "\",source=\"" + source + "\",description=\"" + description +"\"");
		}
	}
}