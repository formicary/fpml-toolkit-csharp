// Copyright (C),2005-2008 HandCoded Software Ltd.
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
using System.IO;
using System.Net;
using System.Text;

namespace HandCoded.Xml.Resolver
{
	/// <summary>
	/// The <b>PublicEntry</b> class implements simple public identifier
	/// matching.
	/// </summary>
	sealed class PublicEntry : RelativeEntry, IEntityRule
	{
		/// <summary>
		/// Constructs a <b>PublicEntry</b> instance that will replace
		/// a public identifier with a URI.
		/// </summary>
		/// <param name="parent">The containing element.</param>
		/// <param name="publicId">The public identifier to be matched</param>
		/// <param name="uri">The replacement URI.</param>
		/// <param name="xmlbase">The optional xml:base URI</param>
		public PublicEntry (GroupEntry parent, String publicId,
				String uri, String xmlbase)
			: base (parent, xmlbase)
		{
			this.publicId = publicId;
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
			Uri					targetUri;
			String				publicUri;
			
			// Convert the target PublicId value to a URI
			try {
				if (publicId.StartsWith ("file:"))
					targetUri = new Uri (Path.GetFullPath (publicId.Substring (5)));
				else
					targetUri = new Uri (Unwrap (publicId));
			}
			catch (UriFormatException error) {
				throw new Exception ("Failed to normalise targetId", error);
			}
		
			// Convert the catalog PublicId value to a URI
			try {
				publicUri = new Uri (BaseAsUri (), new Uri (Unwrap (this.publicId))).ToString ();
			}
			catch (UriFormatException error) {
				throw new Exception ("Failed to normalise publicId", error);
			}
			
			// If they match then replace with the catalog URI
			if (publicUri.ToString ().Equals (targetUri.ToString ())) {
				try {
					return (new Uri (XmlBase + "/" + uri).ToString ());
				}
				catch (UriFormatException error) {
					throw new Exception ("Failed to resolve target URI", error);
				}
			}

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
			return ("publicId=\"" + publicId + "\",uri=\"" + uri + "\"," + base.ToDebug ());
		}

		/// <summary>
		/// The publicId to match against.
		/// </summary>
		private readonly String	publicId;

		/// <summary>
		/// The URI to replace with.
		/// </summary>
		private readonly String	uri;

		/// <summary>
		/// If the supplied name is a public name then it is 'unwrapped' according
		/// to the rules defined in RFC 3151.
		/// </summary>
		/// <remarks>Public names for DTDs (such as those used by DocBook and FpML
		/// 1-0 thru 3-0) are not valid URIs and must undergo a number of character
		/// replacements. This routine detects a public name by looking for '//'
		/// anywhere in the string.</remarks>
		/// <param name="name">The public name to be wrapped.</param>
		/// <returns>A valid URI string, either the original input unmodified or
		///	a new URI constructed by the unwrapping process.</returns>
		private String Unwrap (String name)
		{
			if (name.IndexOf ("//") != -1) {
				StringBuilder buffer = new StringBuilder ();
				
				int		length = name.Length;
				char	ch;
				
				buffer.Append ("urn:publicid:");
				for (int index = 0; index < length; ++index) {
					switch (ch = name [index]) {
					case ' ':
					case '\t':
					case '\r':
					case '\n':
						{
							int			buflen = buffer.Length;
							
							if ((buflen == 0) || (buffer [buflen - 1] != '+'))
								buffer.Append ('+');
							break;
						}
						
					case '/':
						{
							if (((index + 1) < length) && (name [index + 1] == '/')) {
								buffer.Append (':');
								++index;
							}
							else
								buffer.Append ("%2F");
							break;
						}
					
					case ':':
					{
						if (((index + 1) < length) && (name [index + 1] == ':')) {
							buffer.Append (';');
							++index;
						}
						else
							buffer.Append ("%3A");
						break;
					}
					
					case '+':
						buffer.Append ("%2B");
						break;
						
					case ';':
						buffer.Append ("%3B");
						break;
						
					case '\'':
						buffer.Append ("%27");
						break;
					
					case '?':
						buffer.Append ("%3F");
						break;
						
					case '#':
						buffer.Append ("%23");
						break;
						
					case '%':
						buffer.Append ("%25");
						break;
						
					default:
						buffer.Append (ch);
						break;
					}
				}
				
				return (buffer.ToString ());
			}
				
			return (name);
		}
	}
}