// Copyright (C),2005-2007 HandCoded Software Ltd.
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

namespace HandCoded.Xml.Resolver
{
	/// <summary>
	/// The <b>RewriteUriEntry</b> class implements URI rewriting.
	/// </summary>
	sealed class RewriteUriEntry : CatalogComponent, IUriRule
	{
		/// <summary>
		/// Constructs a <b>RewriteUriEntry</b>.
		/// </summary>
		/// <param name="parent">parent</param>
		/// <param name="startString">startString</param>
		/// <param name="rewritePrefix">rewritePrefix</param>
		public RewriteUriEntry (GroupEntry parent, String startString, String rewritePrefix)
			: base (parent)
		{
			this.startString   = startString;
			this.rewritePrefix = rewritePrefix;
		}
		
		/// <summary>
		/// Applys the rule instance to the public or system identifier in an
		/// attempt to locate the URI of a resource with can provide the required
		///	information.
		/// </summary>
		/// <param name="uri">The uri needing to be resolved.</param>
		/// <param name="catalogs">The stack of catalogs being processed.</param>
		/// <returns>A new URI if the rule was successfully applied, otherwise
		///	<b>null</b>.</returns>
		public String ApplyTo (String uri, Stack<GroupEntry> catalogs)
		{
			if (uri.StartsWith (startString))
				return (rewritePrefix + uri.Substring (startString.Length));

			return (null);
		}

		/// <summary>
		/// Converts the instance's member values to <see cref="String"/> representations
		/// and concatenates them all together. This function is used by ToString and
		/// may be overriden in derived classes.
		/// </summary>
		/// <returns>The object's <see cref="String"/> representation.</returns>
		protected override String ToDebug ()
		{
			return ("startString=\"" + startString + "\",rewritePrefix=\"" + rewritePrefix + "\"");
		}

		/// <summary>
		/// The prefix to match with.
		/// </summary>
		private readonly String		startString;

		/// <summary>
		/// The replacement prefix.
		/// </summary>
		private readonly String		rewritePrefix;
	}
}