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
	/// The <b>DOM</b> class contains utility functions that operate on a DOM
	/// tree, often combining several simpler operations or returning a more
	/// useful type.  
	/// </summary>
	public sealed class DOM
	{
		/// <summary>
		/// Finds the first child element of the given <see cref="XmlElement"/>
		/// skipping any intermediate non-element <see cref="XmlNode"/> instances.
		/// </summary>
		/// <param name="parent">The parent <see cref="XmlElement"/>.</param>
		/// <returns>The first child <see cref="XmlElement"/> or <c>null</c> if
		/// none could be found.</returns>
		public static XmlElement GetFirstChild (XmlElement parent)
		{
			XmlNode		node = parent.FirstChild;

			while ((node != null) && (node.NodeType != XmlNodeType.Element))
				node = node.NextSibling;

			return (node as XmlElement);
		}

		/// <summary>
		/// Finds the last child element of the given <see cref="XmlElement"/>
		/// skipping any intermediate non-element <see cref="XmlNode"/> instances.
		/// </summary>
		/// <param name="parent">The parent <see cref="XmlElement"/>.</param>
		/// <returns>The last child <see cref="XmlElement"/> or <c>null</c> if none
		/// could be found.</returns>
		public static XmlElement GetLastChild (XmlElement parent)
		{
			XmlNode		node = parent.LastChild;

			while ((node != null) && (node.NodeType != XmlNodeType.Element))
				node = node.PreviousSibling;

			return (node as XmlElement);
		}

		/// <summary>
		/// Finds the next sibling element of the given <see cref="XmlElement"/>
		/// skipping any intermediate non-element <see cref="XmlNode"/> instances.
		/// </summary>
		/// <param name="element">The context <see cref="XmlElement"/>.</param>
		/// <returns>The next sibling <see cref="XmlElement"/> or <c>null</c> if none
		/// could be found.</returns>
		public static XmlElement GetNextSibling (XmlElement element)
		{
			XmlNode		node = element.NextSibling;

			while ((node != null) && (node.NodeType != XmlNodeType.Element))
				node = node.NextSibling;

			return (node as XmlElement);
		}

		/// <summary>
		/// Finds the previous sibling element of the given <see cref="XmlElement"/>
		/// skipping any intermediate non-element <see cref="XmlNode"/> instances.
		/// </summary>
		/// <param name="element">The context <see cref="XmlElement"/>.</param>
		/// <returns>The previous sibling <see cref="XmlElement"/> or <c>null</c>
		/// if none could be found.</returns>
		public static XmlElement GetPreviousSibling (XmlElement element)
		{
			XmlNode		node = element.PreviousSibling;
 
			while ((node != null) && (node.NodeType != XmlNodeType.Element))
				node = node.PreviousSibling;

			return (node as XmlElement);
		}

		/// <summary>
		/// Finds the parent <see cref="XmlElement"/> of the given node.
		/// </summary>
		/// <param name="element">The context <see cref="XmlElement"/>.</param>
		/// <returns>The parent <see cref="XmlElement"/> or <c>null</c> if the
		/// context node was the root element.</returns>
		public static XmlElement GetParent (XmlElement element)
		{
			return ((element != null) ? element.ParentNode as XmlElement : null);
		}

		/// <summary>
		/// Determines if the given <see cref="XmlElement"/> has at least one
		/// child element.
		/// </summary>
		/// <param name="element">The context <see cref="XmlElement"/>.</param>
		/// <returns><c>true</c> if the context <see cref="XmlElement"/> has at least
		/// one child <see cref="XmlElement"/>, <c>false</c> otherwise.</returns>
		public static bool HasChildNodes (XmlElement element)
		{
			for (XmlNode node = element.FirstChild; node != null;) 
			{
				if (node.NodeType == XmlNodeType.Element) return (true);
				node = node.NextSibling;
			}
			return (false);
		}

		/// <summary>
		/// Locates the first child <see cref="XmlElement"/> of the indicated
		/// parent that matches the given local name string, ignoring prefixes
		/// and namespaces.
		/// </summary>
		/// <param name="parent">The parent <see cref="XmlElement"/>.</param>
		/// <param name="localName">The required element local name string.</param>
		/// <returns>The first matching <see cref="XmlElement"/> or <b>null</b>
		/// if none.</returns>
		public static XmlElement GetElementByLocalName (XmlElement parent, string localName)
		{
			foreach (XmlNode node in parent.ChildNodes) {
				if ((node.NodeType == XmlNodeType.Element) && node.LocalName.Equals (localName))
					return (node as XmlElement);
			}
			return (null);
		}

		/// <summary>
		/// Locates all the child <see cref="XmlElement"/> instances of the indicated
		/// parent that match the given local name string, ignoring prefixes
		/// and namespaces.
		/// </summary>
		/// <param name="parent">The parent <see cref="XmlElement"/>.</param>
		/// <param name="localName">The required element local name string.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// <see cref="XmlElement"/> instances.</returns>
		public static XmlNodeList GetElementsByLocalName (XmlElement parent, string localName)
		{
			MutableNodeList	list	= new MutableNodeList ();

			foreach (XmlNode node in parent.ChildNodes) {
				if ((node.NodeType == XmlNodeType.Element) && node.LocalName.Equals (localName))
					list.Add (node);
			}
			return (list);
		}

		/// <summary>
		/// Locates all the child elements of the given parent <see cref="XmlElement"/>
		/// and returns them in a (possibly empty) <see cref="XmlNodeList"/>.
		/// </summary>
		/// <param name="parent">The parent <see cref="XmlElement"/>.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of child elements.</returns>
		public static XmlNodeList GetChildElements (XmlElement parent)
		{
			MutableNodeList	list	= new MutableNodeList ();

			if (parent != null) {
				foreach (XmlNode node in parent.ChildNodes)
					if (node.NodeType == XmlNodeType.Element) list.Add (node);
			}
			return (list);
		}

		/// <summary>
		/// Gets the value of the specified attribute on the indicated context
		/// <see cref="XmlElement"/>. Note that this method returns <c>null</c>
		/// if the attribute is not present compared to the DOM function which
		/// returns a empty string.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name">The attribute name.</param>
		/// <returns>The value of the attribute or <c>null</c> if it was not present.</returns>
		public static string GetAttribute (XmlElement context, string name)
		{
			XmlAttribute	attr	= context.GetAttributeNode (name);

			return ((attr != null) ? attr.Value : null);
		}

		/// <summary>
		/// Sets the value of specified attribute on the indicated context
		/// <see cref="XmlElement"/>. If the value is <c>null</c> then the
		/// attribute is removed.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name">The attribute name.</param>
		/// <param name="value">The new value or <c>null</c>.</param>
		public static void SetAttribute (XmlElement context, string name, string value)
		{
			if (value == null) {
				context.RemoveAttribute (name);
			}
			else
				context.SetAttribute (name, value);
		}
				
		/// <summary>
		/// Ensures no instances can be constructed.
		/// </summary>
		private DOM ()
		{ }
	}
}