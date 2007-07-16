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
	/// The <b>SystemEntry</b> class implements simple system identifier
	/// matching.
	/// </summary>
	sealed class SystemEntry : RelativeEntry, IEntityRule
	{
		/// <summary>
		/// Constructs a <b>SystemEntry</b> instance that will replace
		/// a system identifier with a URI.
		/// </summary>
		/// <param name="parent">The containing element.</param>
		/// <param name="systemId">The system identifier to be matched.</param>
		/// <param name="uri">The replacement URI.</param>
		/// <param name="xmlbase">Optional <CODE>xml:base</CODE> value.</param>
		public SystemEntry (GroupEntry parent, String systemId,
				String uri, String xmlbase)
			: base (parent, xmlbase)
		{			
			this.systemId = systemId;
			this.uri      = uri;
		}
	
		/// <summary>
		/// Applys the rule instance to the public or system identifier in an
		/// attempt to locate the URI of a resource with can provide the required
		/// information.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">The stack of catalogs being processed.</param>
		/// <returns>A new URI if the rule was successfully applied, otherwise
		/// <b>null</b>.</returns>
		public String ApplyTo (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			Uri				targetUri = new Uri (systemId);
			Uri				systemUri = new Uri (BaseAsUri (), this.systemId);

			if (targetUri.Equals (systemUri))
				return (new Uri (BaseAsUri (), new Uri (uri)).ToString ());

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
			return ("systemId=\"" + systemId + "\",uri=\"" + uri + "\"," + base.ToDebug ());
		}

		/// <summary>
		/// The systemId to match against.
		/// </summary>
		private readonly String		systemId;

		/// <summary>
		/// The URI to replace with.
		/// </summary>
		private readonly String		uri;
	}
}