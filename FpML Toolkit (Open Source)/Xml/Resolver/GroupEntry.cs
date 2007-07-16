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

		/**
		 * Adds a rule to the catalog which will cause a matching system identifier
		 * to its starting prefix replaced.
		 *
		 * @param	startString		The system identifier prefix to match.
		 * @param	rewritePrefix	The new prefix to replace with.
		 * @since	TFP 1.0
		 */
		public RewriteSystemEntry AddRewriteSystem (String startString, String rewritePrefix)
		{
			RewriteSystemEntry	result = new RewriteSystemEntry (this, startString, rewritePrefix);
			
			rules.Add (result);
			return (result);
		}

		/**
		 * Adds a rule to the catalog which will direct the resolution for any
		 * system identifier that starts with the given prefix to a sub-catalog
		 * file.
		 *
		 * @param	startString		The system identifier prefix to match
		 * @param	catalog			The URI of the sub-catalog.
		 * @param	xmlbase			Optional xml:base value.
		 * @since	TFP 1.0
		 */
		public DelegateSystemEntry AddDelegateSystem (String startString, String catalog,
				String xmlbase)
		{
			DelegateSystemEntry	result  = new DelegateSystemEntry (this, startString, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/**
		 * Adds a rule to the catalog which will direct the resolution for any
		 * system identifier that starts with the given prefix to a sub-catalog
		 * file.
		 *
		 * @param	startString		The system identifier prefix to match
		 * @param	catalog			The URI of the sub-catalog.
		 * @since	TFP 1.0
		 */
		public DelegateSystemEntry AddDelegateSystem (String startString, String catalog)
		{
			return (AddDelegateSystem (startString, catalog, null));
		}

		/**
		 * Adds a rule to the catalog which will direct the resolution for any
		 * public identifier that starts with the given prefix to a sub-catalog
		 * file.
		 *
		 * @param	startString		The public identifier prefix to match
		 * @param	catalog			The URI of the sub-catalog.
		 * @param	xmlbase			The optional xml:base URI
		 * @since	TFP 1.0
		 */
		public DelegatePublicEntry AddDelegatePublic (String startString, String catalog,
				String xmlbase)
		{
			DelegatePublicEntry	result  = new DelegatePublicEntry (this, startString, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/**
		 * Adds a rule to the catalog which will direct the resolution for any
		 * public identifier that starts with the given prefix to a sub-catalog
		 * file.
		 *
		 * @param	startString		The public identifier prefix to match
		 * @param	catalog			The URI of the sub-catalog.
		 * @since	TFP 1.0
		 */
		public DelegatePublicEntry AddDelegatePublic (String startString, String catalog)
		{
			return (AddDelegatePublic (startString, catalog, null));
		}
		
		public UriEntry AddUri (String name, String uri, String xmlbase)
		{
			UriEntry	result  = new UriEntry (this, name, uri, xmlbase);
			
			rules.Add (result);
			return (result);
		}
		
		public UriEntry AddUri (String name, String uri)
		{
			return (AddUri (name, uri, null));
		}
		
		public RewriteUriEntry AddRewriteUri (String startString, String rewritePrefix)
		{
			RewriteUriEntry		result = new RewriteUriEntry (this, startString, rewritePrefix);
			
			rules.Add (result);
			return (result);
		}
		
		public DelegateUriEntry AddDelegateUri (String startString, String catalog, String xmlbase)
		{
			DelegateUriEntry	result = new DelegateUriEntry (this, startString, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}
		
		public DelegateUriEntry AddDelegateUri (String startString, String catalog)
		{
			return (AddDelegateUri (startString, catalog, null));
		}
		
		/**
		 * Adds a rule which will cause resolution to chain to another catalog
		 * if no match can be found in this one.
		 * <P>
		 * This method is only available to the <CODE>XmlCatalogManager</CODE>
		 * class.
		 *
		 * @param	catalog			The URI of the catalog to chain to.
		 * @param	xmlbase			The optional xml:base URI
		 * @since	TFP 1.0
		 */
		public NextCatalogEntry AddNextCatalog (String catalog, String xmlbase)
		{
			NextCatalogEntry	result = new NextCatalogEntry (this, catalog, xmlbase);
			
			rules.Add (result);
			return (result);
		}

		/**
		 * Adds a rule which will cause resolution to chain to another catalog
		 * if no match can be found in this one.
		 * <P>
		 * This method is only available to the <CODE>CatalogManager</CODE>
		 * class.
		 *
		 * @param	catalog			The URI of the catalog to chain to.
		 * @since	TFP 1.0
		 */
		public NextCatalogEntry AddNextCatalog (String catalog)
		{
			return (AddNextCatalog (catalog, null));
		}

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

		/**
		 * Applies all the <CODE>RewriteSystemEntry</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param	publicId		The public identifier of the external entity
		 *							being referenced, or null if none was supplied.
		 * @param	systemId		The system identifier of the external entity
		 *							being referenced.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws 	SAXException	If an occur was detected during processing.
		 * @since	TFP 1.0 
		 */
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

		/**
		 * Applies all the <CODE>DelegateSystemEntry</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param	publicId		The public identifier of the external entity
		 *							being referenced, or null if none was supplied.
		 * @param	systemId		The system identifier of the external entity
		 *							being referenced.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws 	SAXException	If an occur was detected during processing.
		 * @since	TFP 1.0 
		 */
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

		/**
		 * Applies all the <CODE>PublicEntry</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param	publicId		The public identifier of the external entity
		 *							being referenced, or null if none was supplied.
		 * @param	systemId		The system identifier of the external entity
		 *							being referenced.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws 	SAXException	If an occur was detected during processing.
		 * @since	TFP 1.0 
		 */
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

		/**
		 * Applies all the <CODE>DelegatePublicEntry</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param	publicId		The public identifier of the external entity
		 *							being referenced, or null if none was supplied.
		 * @param	systemId		The system identifier of the external entity
		 *							being referenced.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws 	SAXException	If an occur was detected during processing.
		 * @since	TFP 1.0 
		 */
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

		/**
		 * Applies all the <CODE>NextCatalog</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param	publicId		The public identifier of the external entity
		 *							being referenced, or null if none was supplied.
		 * @param	systemId		The system identifier of the external entity
		 *							being referenced.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws 	SAXException	If an occur was detected during processing.
		 * @since	TFP 1.0 
		 */
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

		/**
		 * Applies all the <CODE>UriEntry</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param 	uri				The URI of the required resource.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws	SAXException If an error occurs during processing.
		 * @since	TFP 1.0 
		 */
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
		
		/**
		 * Applies all the <CODE>RewriteUriEntry</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param 	uri				The URI of the required resource.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws	SAXException If an error occurs during processing.
		 * @since	TFP 1.0 
		 */
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
		
		/**
		 * Applies all the <CODE>DelegateUriEntry</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param 	uri				The URI of the required resource.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws	SAXException If an error occurs during processing.
		 * @since	TFP 1.0 
		 */
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

		/**
		 * Applies all the <CODE>NextCatalog</CODE> rules in the current
		 * catalog recursing into <CODE>Group</CODE> definitions.
		 * 
		 * @param 	uri				The URI of the required resource.
		 * @param	catalogs		A stack of catalogs being processed used to
		 *							detect circular dependency.
		 * @return  The URI of the resolved entity or <CODE>null</CODE>.
		 * @throws	SAXException If an error occurs during processing.
		 * @since	TFP 1.0 
		 */
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