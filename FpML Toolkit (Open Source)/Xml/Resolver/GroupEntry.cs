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
	/// The <b>GroupEntry</b> class provides a container for other
	/// catalog components.
	/// </summary>
	class GroupEntry : RelativeEntry
	{
		/// <summary>
		/// Contains the the value of the <b>prefer</b> attribute,
		/// possibly obtained from a containing element.
		/// </summary>
		public String Prefer {
			get {
				if (prefer != null)
					return (prefer);
				else if (Parent != null)
					return (Parent.Prefer);
				else
					return (null); 
			}
		}

		/// <summary>
		/// Constructs a <b>GroupEntry</b> given its containing entry
		/// and attribute values.
		/// </summary>
		/// <param name="parent">The containing element.</param>
		/// <param name="prefer">Optional <b>prefer</b> value.</param>
		/// <param name="xmlbase">Optional <b>xml:base</b> value.</param>
		public GroupEntry (GroupEntry parent, String prefer, String xmlbase)
			: base (parent, xmlbase)
		{
			this.prefer = prefer;
		}

		/// <summary>
		/// Adds a rule to the catalog directing the given public identifier to
		/// the indicated URI.
		/// </summary>
		/// <param name="publicId">The public identifier to be mapped.</param>
		/// <param name="uri">The corresponding URI.</param>
		/// <param name="xmlbase">Optional <b>xml:base</b> value.</param>
		/// <returns>The constructed <see cref="PublicEntry"/> instance.</returns>
		public PublicEntry AddPublic (String publicId, String uri, String xmlbase)
		{
			PublicEntry		result = new PublicEntry (this, publicId, uri, xmlbase);
			
			rules.Add (result);
			return (result);
		}
		
		/// <summary>
		/// Adds a rule to the catalog directing the given public identifier to
		/// the indicated URI.
		/// </summary>
		/// <param name="publicId">The public identifier to be mapped.</param>
		/// <param name="uri">The corresponding URI.</param>
		/// <returns>The constructed <see cref="PublicEntry"/> instance.</returns>
		public PublicEntry AddPublic (String publicId, String uri)
		{
			return (AddPublic (publicId, uri, null));
		}

		/// <summary>
		/// Adds a rule to the catalog directing the given system identifier to
		/// the indicated URI.
		/// </summary>
		/// <param name="systemId">The system identifier to be mapped.</param>
		/// <param name="uri">The corresponding URI.</param>
		/// <param name="xmlbase">Optional xml:base value.</param>
		/// <returns>The constructed <see cref="SystemEntry"/> instance.</returns>
		public SystemEntry AddSystem (String systemId, String uri, String xmlbase)
		{
			SystemEntry		result = new SystemEntry (this, systemId, uri, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/// <summary>
		/// Adds a rule to the catalog directing the given system identifier to
		/// the indicated URI.
		/// </summary>
		/// <param name="systemId">The system identifier to be mapped.</param>
		/// <param name="uri">The corresponding URI.</param>
		/// <returns>The constructed <see cref="SystemEntry"/> instance.</returns>
		public SystemEntry AddSystem (String systemId, String uri)
		{
			return (AddSystem (systemId, uri, null));
		}

		/// <summary>
		/// Adds a rule to the catalog which will cause a matching system identifier
		/// to its starting prefix replaced.
		/// </summary>
		/// <param name="startString">The system identifier prefix to match.</param>
		/// <param name="rewritePrefix">The new prefix to replace with.</param>
		/// <returns>The constructed <see cref="RewriteSystemEntry"/> instance.</returns>
		public RewriteSystemEntry AddRewriteSystem (String startString, String rewritePrefix)
		{
			RewriteSystemEntry	result = new RewriteSystemEntry (this, startString, rewritePrefix);
			
			rules.Add (result);
			return (result);
		}

		/// <summary>
		/// Adds a rule to the catalog which will direct the resolution for any
		/// system identifier that starts with the given prefix to a sub-catalog
		/// file.
		/// </summary>
		/// <param name="startString">The system identifier prefix to match</param>
		/// <param name="catalog">The URI of the sub-catalog.</param>
		/// <param name="xmlbase">Optional xml:base value.</param>
		/// <returns>The constructed <see cref="DelegateSystemEntry"/> instance.</returns>
		public DelegateSystemEntry AddDelegateSystem (String startString, String catalog,
				String xmlbase)
		{
			DelegateSystemEntry	result  = new DelegateSystemEntry (this, startString, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/// <summary>
		/// Adds a rule to the catalog which will direct the resolution for any
		/// system identifier that starts with the given prefix to a sub-catalog
		/// file.
		/// </summary>
		/// <param name="startString">The system identifier prefix to match</param>
		/// <param name="catalog">The URI of the sub-catalog.</param>
		/// <returns>The constructed <see cref="DelegateSystemEntry"/> instance.</returns>
		public DelegateSystemEntry AddDelegateSystem (String startString, String catalog)
		{
			return (AddDelegateSystem (startString, catalog, null));
		}

		/// <summary>
		/// Adds a rule to the catalog which will direct the resolution for any
		/// public identifier that starts with the given prefix to a sub-catalog
		/// file.
		/// </summary>
		/// <param name="startString">The public identifier prefix to match</param>
		/// <param name="catalog">The URI of the sub-catalog.</param>
		/// <param name="xmlbase">The optional xml:base URI</param>
		/// <returns>The constructed <see cref="DelegatePublicEntry"/> instance.</returns>
		public DelegatePublicEntry AddDelegatePublic (String startString, String catalog,
				String xmlbase)
		{
			DelegatePublicEntry	result  = new DelegatePublicEntry (this, startString, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/// <summary>
		/// Adds a rule to the catalog which will direct the resolution for any
		/// public identifier that starts with the given prefix to a sub-catalog
		/// file.
		/// </summary>
		/// <param name="startString">The public identifier prefix to match</param>
		/// <param name="catalog">The URI of the sub-catalog.</param>
		/// <returns>The constructed <see cref="DelegatePublicEntry"/> instance.</returns>
		public DelegatePublicEntry AddDelegatePublic (String startString, String catalog)
		{
			return (AddDelegatePublic (startString, catalog, null));
		}
		
		/// <summary>
		/// Adds a rule to the catalog which performs URI matching.
		/// </summary>
		/// <param name="name">The URI to match with.</param>
		/// <param name="uri">The URI to replace with.</param>
		/// <param name="xmlbase">The optional xml:base URI</param>
		/// <returns>The constructed <see cref="UriEntry"/> instance.</returns>
		public UriEntry AddUri (String name, String uri, String xmlbase)
		{
			UriEntry	result  = new UriEntry (this, name, uri, xmlbase);
			
			rules.Add (result);
			return (result);
		}
		
		/// <summary>
		/// Adds a rule to the catalog which performs URI matching.
		/// </summary>
		/// <param name="name">The URI to match with.</param>
		/// <param name="uri">The URI to replace with.</param>
		/// <returns>The constructed <see cref="UriEntry"/> instance.</returns>
		public UriEntry AddUri (String name, String uri)
		{
			return (AddUri (name, uri, null));
		}
		
		/// <summary>
		/// Adds a rule to the catalog which perform URI rewriting.
		/// </summary>
		/// <param name="startString">The prefix string to match with.</param>
		/// <param name="rewritePrefix">The string to replace matches with.</param>
		/// <returns>The constructed <see cref="RewriteUriEntry"/> instance.</returns>
		public RewriteUriEntry AddRewriteUri (String startString, String rewritePrefix)
		{
			RewriteUriEntry		result = new RewriteUriEntry (this, startString, rewritePrefix);
			
			rules.Add (result);
			return (result);
		}
		
		/// <summary>
		/// Adds a rule with delegates the processing for matched URIs to another
		/// catalog.
		/// </summary>
		/// <param name="startString">The prefix string to match against.</param>
		/// <param name="catalog">The catalog to delegate to.</param>
		/// <param name="xmlbase">The optional xml:base URI</param>
		/// <returns>The constructed <see cref="DelegateUriEntry"/> instance.</returns>
		public DelegateUriEntry AddDelegateUri (String startString, String catalog, String xmlbase)
		{
			DelegateUriEntry	result = new DelegateUriEntry (this, startString, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}
		
		/// <summary>
		/// Adds a rule with delegates the processing for matched URIs to another
		/// catalog.
		/// </summary>
		/// <param name="startString">The prefix string to match against.</param>
		/// <param name="catalog">The catalog to delegate to.</param>
		/// <returns>The constructed <see cref="DelegateUriEntry"/> instance.</returns>
		public DelegateUriEntry AddDelegateUri (String startString, String catalog)
		{
			return (AddDelegateUri (startString, catalog, null));
		}
		
		/// <summary>
		/// Adds a rule which will cause resolution to chain to another catalog
		/// if no match can be found in this one.
		/// </summary>
		/// <param name="catalog">The URI of the catalog to chain to.</param>
		/// <param name="xmlbase">The optional xml:base URI</param>
		/// <returns>The constructed <see cref="NextCatalogEntry"/> instance.</returns>
		public NextCatalogEntry AddNextCatalog (String catalog, String xmlbase)
		{
			NextCatalogEntry	result = new NextCatalogEntry (this, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/// <summary>
		/// Adds a rule which will cause resolution to chain to another catalog
		/// if no match can be found in this one.
		/// </summary>
		/// <param name="catalog">The URI of the catalog to chain to.</param>
		/// <returns>The constructed <see cref="NextCatalogEntry"/> instance.</returns>
		public NextCatalogEntry AddNextCatalog (String catalog)
		{
			return (AddNextCatalog (catalog, null));
		}

		/// <summary>
		/// The list of <see cref="CatalogComponent"/> that make up the group.
		/// </summary>
		protected List<CatalogComponent>	rules = new List<CatalogComponent> ();

		/// <summary>
		/// Implements the OASIS search rules for entity resources.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		protected internal String ApplyRules (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			String				result = null;

			if (catalogs.Contains (this))
				throw new Exception ("Circular dependency in the XML Catalogs");

			catalogs.Push (this);

			// If a system identifier is provided then try to match it explicitly
			// or through a rewriting rule or delegation.
			if ((systemId != null) && (systemId.Length > 0)) {
				if ((result = ApplySystemEntries (publicId, systemId, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
				
				if ((result = ApplyRewriteSystemEntries (publicId, systemId, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
				
				if ((result = ApplyDelegateSystemEntries (publicId, systemId, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
			}

			// If a public identifier is provided then try to match it explicity
			// or through delegation.
			if ((publicId != null) && (publicId.Length > 0)) {
				if ((result = ApplyPublicEntries (publicId, systemId, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
				
				if ((result = ApplyDelegatePublicEntries (publicId, systemId, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
			}

			// Finally try any other chained catalogs
			if ((result = ApplyNextCatalogEntries (publicId, systemId, catalogs)) != null) {
				catalogs.Pop ();
				return (result);
			}

			catalogs.Pop ();
			return (null);
		}

		/// <summary>
		/// Implements OASIS search rules for URI based resources.
		/// </summary>
		/// <param name="uri">The URI of the required resource.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		///
		protected internal String ApplyRules (String uri, Stack<GroupEntry> catalogs)
		{
			String				result = null;

			if (catalogs.Contains (this))
				throw new Exception ("Circular dependency in the XML Catalogs");

			catalogs.Push (this);

			if ((uri != null) && (uri.Length > 0)) {
				if ((result = ApplyUriEntries (uri, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
				
				if ((result = ApplyRewriteUriEntries (uri, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
				
				if ((result = ApplyDelegateUriEntries (uri, catalogs)) != null) {
					catalogs.Pop ();
					return (result);
				}
			}

			// Finally try any other chained catalogs
			if ((result = ApplyNextCatalogEntries (uri, catalogs)) != null) {
				catalogs.Pop ();
				return (result);
			}

			catalogs.Pop ();
			return (null);
		}

		/// <summary>
		/// The value of the prefer attribute.
		/// </summary>
		private readonly String		prefer;

		/// <summary>
		/// Applies all the <see cref="SystemEntry"/> rules in the current
		/// catalog recursing into <b>Group</b> definitions.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplySystemEntries (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is SystemEntry) {
					if ((result = ((IEntityRule) rule)
							.ApplyTo (publicId, systemId, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplySystemEntries (publicId, systemId, catalogs)) != null)
						break;
				}
			}
			return (result);
		}

		/// <summary>
		/// Applies all the <see cref="RewriteSystemEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyRewriteSystemEntries (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is RewriteSystemEntry) {
					if ((result = ((IEntityRule) rule)
							.ApplyTo (publicId, systemId, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyRewriteSystemEntries (publicId, systemId, catalogs)) != null)
						break;
				}
			}
			return (result);
		}

		/// <summary>
		/// Applies all the <see cref="DelegateSystemEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyDelegateSystemEntries (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is DelegateSystemEntry) {
					if ((result = ((IEntityRule) rule)
							.ApplyTo (publicId, systemId, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyDelegateSystemEntries (publicId, systemId, catalogs)) != null)
						break;
				}
			}
			return (result);
		}

		/// <summary>
		/// Applies all the <see cref="PublicEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyPublicEntries (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is PublicEntry) {
					if ((result = ((IEntityRule) rule)
							.ApplyTo (publicId, systemId, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyPublicEntries (publicId, systemId, catalogs)) != null)
						break;
				}
			}
			return (result);
		}

		/// <summary>
		/// Applies all the <see cref="DelegatePublicEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyDelegatePublicEntries (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is DelegatePublicEntry) {
					if ((result = ((IEntityRule) rule)
							.ApplyTo (publicId, systemId, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyDelegatePublicEntries (publicId, systemId, catalogs)) != null)
						break;
				}
			}
			return (result);
		}

		/// <summary>
		/// Applies all the <see cref="NextCatalogEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="publicId">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="systemId">The system identifier of the external entity
		/// being referenced.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyNextCatalogEntries (String publicId, String systemId, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is NextCatalogEntry) {
					if ((result = ((IEntityRule) rule)
							.ApplyTo (publicId, systemId, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyNextCatalogEntries (publicId, systemId, catalogs)) != null)
						break;
				}
			}
			return (result);
		}

		/// <summary>
		/// Applies all the <see cref="UriEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="uri">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyUriEntries (String uri, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is UriEntry) {
					if ((result = ((IUriRule) rule)
							.ApplyTo (uri, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyUriEntries (uri, catalogs)) != null)
						break;
				}
			}
			return (result);
		}
		
		/// <summary>
		/// Applies all the <see cref="RewriteUriEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="uri">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyRewriteUriEntries (String uri, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is RewriteUriEntry) {
					if ((result = ((IUriRule) rule)
							.ApplyTo (uri, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyRewriteUriEntries (uri, catalogs)) != null)
						break;
				}
			}
			return (result);
		}
		
		/// <summary>
		/// Applies all the <see cref="DelegateUriEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="uri">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyDelegateUriEntries (String uri, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is DelegateUriEntry) {
					if ((result = ((IUriRule) rule)
							.ApplyTo (uri, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyDelegateUriEntries (uri, catalogs)) != null)
						break;
				}
			}
			return (result);
		}

		/// <summary>
		/// Applies all the <see cref="NextCatalogEntry"/> rules in the current
		/// catalog recursing into <b>GroupEntry</b> definitions.
		/// </summary>
		/// <param name="uri">The public identifier of the external entity
		/// being referenced, or null if none was supplied.</param>
		/// <param name="catalogs">A stack of catalogs being processed used to
		/// detect circular dependency.</param>
		/// <returns>The URI of the resolved entity or <b>null</b>.</returns>
		private String ApplyNextCatalogEntries (String uri, Stack<GroupEntry> catalogs)
		{
			String		result = null;
			
			foreach (CatalogComponent rule in rules) {
				if (rule is NextCatalogEntry) {
					if ((result = ((IUriRule) rule)
							.ApplyTo (uri, catalogs)) != null)
						break;
				}
				
				if (rule is GroupEntry) {
					if ((result = ((GroupEntry) rule)
							.ApplyNextCatalogEntries (uri, catalogs)) != null)
						break;
				}
			}
			return (result);
		}
	}
}