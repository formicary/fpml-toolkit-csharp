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
using System.Xml;

namespace HandCoded.Xml
{
	/// <summary>
	/// The <b>Browser</b> class tracks a position of interest within a
	/// DOM document which can be moved around as the document is explored.
	/// <para>During traversal the <b>Browser</b> automatically skips
	/// over any non-element nodes in the underlying <see cref="XmlDocument"/>.
	/// </para>
	/// </summary>
	public class Browser
	{
		/// <summary>
		/// Constructs a <b>Browser</b> that is attached to the root element of the
		/// <see cref="XmlDocument"/> indicated by <paramref name="document"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to explore.</param>
		public Browser (XmlDocument document)
			: this (document, document.DocumentElement)
		{ }

		/// <summary>
		/// Constructs a <b>Browser</b> that is attached to a specific 
		/// <see cref="XmlElement"/> of the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to explore.</param>
		/// <param name="context">The initial context <see cref="XmlElement"/>.</param>
		public Browser (XmlDocument document, XmlElement context)
		{
			if (context.OwnerDocument != document)
				throw new ArgumentException ("Invalid context element", "context");

			this.document = document;
			this.context  = context;
		}

		/// <summary>
		/// Contains the underlying <see cref="XmlDocument"/> instance.
		/// </summary>
		public XmlDocument Document {
			get {
				return (document);
			}
		}

		/// <summary>
		/// Contains the current context <see cref="XmlElement"/> instance.
		/// </summary>
		public XmlElement Context {
			get {
				return (context);
			}
		}

		/// <summary>
		/// Returns the value of the indicated attribute on the context element.
		/// </summary>
		/// <param name="name">The name of the required attribute.</param>
		/// <returns>The value of the attribute.</returns>
		public virtual string GetAttribute (string name)
		{
			return (context.GetAttribute (name));
		}

		/// <summary>
		/// Returns the value of the indicated attribute on the context element.
		/// </summary>
		/// <param name="name">The name of the required attribute.</param>
		/// <param name="uri">The attribute's owning namespace.</param>
		/// <returns>The value of the attribute.</returns>
		public string GetAttribute (string name, string uri)
		{
			return (context.GetAttribute (name, uri));
		}

		/// <summary>
		/// Determines if the context <see cref="XmlElement"/> has at least one
		/// child element.
		/// </summary>
		/// <returns><c>true</c> if the context <see cref="XmlElement"/> has at least
		/// one child <see cref="XmlElement"/>, <c>false</c> otherwise.</returns>
		public bool HasChildNodes ()
		{
			return (DOM.HasChildNodes (context));
		}

		/// <summary>
		/// Moves the context to the first child <see cref="XmlElement"/> if one
		/// exists.
		/// </summary>
		/// <returns><c>true</c> if the context <see cref="XmlElement"/> was
		/// changed, <c>false</c> otherwise.</returns>
		public bool MoveToFirstChild ()
		{
			XmlElement	child = DOM.GetFirstChild (context);
	
			if (child != null) {
				context = child;
				return (true);
			}
			return (false);
		}

		/// <summary>
		/// Moves the context to the last child <see cref="XmlElement"/> if one
		/// exists.
		/// </summary>
		/// <returns><c>true</c> if the context <see cref="XmlElement"/> was
		/// changed, <c>false</c> otherwise.</returns>
		public bool MoveToLastChild ()
		{
			XmlElement		child = DOM.GetLastChild (context);
	
			if (child != null) {
				context = child;
				return (true);
			}
			return (false);
		}

		/// <summary>
		/// Moves the context to the next sibling <see cref="XmlElement"/> if
		/// one exists.
		/// </summary>
		/// <returns><c>true</c> if the context <see cref="XmlElement"/> was
		/// changed, <c>false</c> otherwise.</returns>
		public bool MoveToNextSibling ()
		{
			XmlElement	element = DOM.GetNextSibling (context);

			if (element != null) {
				context = element;
				return (true);
			}
			return (false);
		}

		/// <summary>
		/// Moves the context to the previous sibling <see cref="XmlElement"/>
		/// if one exists.
		/// </summary>
		/// <returns><c>true</c> if the context <c>XmlElement</c> was changed,
		/// <c>false</c> otherwise.</returns>
		public bool MoveToPreviousSibling ()
		{
			XmlElement	element = DOM.GetPreviousSibling (context);

			if (element != null) {
				context = element;
				return (true);
			}
			return (false);
		}

		/// <summary>
		/// Moves the context to the parent <see cref="XmlElement"/> if one exists.
		/// </summary>
		/// <returns><c>true</c> if the context <see cref="XmlElement"/> was changed,
		/// <c>false</c> otherwise.</returns>
		public bool MoveToParent ()
		{
			XmlElement	parent = DOM.GetParent (context);

			if (parent != null){
				context = parent;
				return (true);
			}
			return (false);
		}

		/// <summary>
		/// The underlying <see cref="XmlDocument"/>.
		/// </summary>
		protected XmlDocument		document;

		/// <summary>
		/// The context <see cref="XmlElement"/>.
		/// </summary>
		protected XmlElement		context;
	}
}