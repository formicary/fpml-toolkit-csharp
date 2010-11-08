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
	/// The <b>XPath</b> class contains utility functions for creating XPath
	/// location strings and applying simple XPath like operations to a DOM
	/// tree.
	/// </summary>
	public sealed class XPath
	{
		/// <summary>
		/// Constructs an XPath expression that describes the location of the
		/// given <see cref="XmlNode"/> within its document tree.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be described.</param>
		/// <returns>An XPath expression describing the location of the
		/// <see cref="XmlNode"/>.</returns>
		public static string ForNode (XmlNode node)
		{
			if (node != null) 
			{
				switch (node.NodeType) 
				{
					case XmlNodeType.Attribute:
						return (ForNode ((node as XmlAttribute).OwnerElement) + "/@" + node.Name);
	
					case XmlNodeType.Element:
					{
						int			succ = 0;
						int			pred = 0;

						for (XmlElement temp = DOM.GetPreviousSibling (node as XmlElement); temp != null;) 
						{
							if (!temp.Name.Equals (node.Name)) break;

							temp = DOM.GetPreviousSibling (temp);
							++succ;
						}

						for (XmlElement temp = DOM.GetNextSibling (node as XmlElement); temp != null;) 
						{
							if (!temp.Name.Equals (node.Name)) break;

							temp = DOM.GetNextSibling (temp);
							++pred;
						}

						if ((succ + pred) > 0)
							return (ForNode (node.ParentNode) +
								"/" + node.LocalName + "[" + (succ + 1) + "]");
						else
							return (ForNode (node.ParentNode) +
								"/" + node.LocalName);
					}
				}
			}
			return ("");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Determines the common ancestor of two <see cref="XmlElement"/> instances.
		/// </summary>
		/// <param name="elementA">The first element.</param>
		/// <param name="elementB">The second element.</param>
		/// <returns>The common ancestor <see cref="XmlNode"/> or <b>null</b> if the
		/// two <see cref="XmlElement"/> arguments are in different documents.</returns>
		public static XmlNode CommonAncestor (XmlElement elementA, XmlElement elementB)
		{
			if ((elementA != null) && (elementB != null)) {
				if (elementA.OwnerDocument == elementB.OwnerDocument) {
					for (XmlNode nodeA = elementA; nodeA != null; nodeA = nodeA.ParentNode)
						for (XmlNode nodeB = elementB; nodeB != null; nodeB = nodeB.ParentNode)
							if (nodeA == nodeB) return (nodeA);
				}
			}
			return (null);
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named child element. The '.' and '..' specifiers are
		/// supported.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name">The name of the required child.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name)
		{
			if ((name.Length == 1) && name.Equals ("."))
				return (context);
			else if ((name.Length == 2) && name.Equals (".."))
				return (context.ParentNode as XmlElement);
			else {
                if (context != null) {
                    foreach (XmlNode node in context.ChildNodes) {
                        if ((node is XmlElement) && (node as XmlElement).LocalName.Equals (name))
                            return (node as XmlElement);
                    }
                }
                return (null);
            }
		}

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named grandchild element.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name1, string name2)
		{
			return (Path (Path (context, name1), name2));
		}

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named great-grandchild element.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name1, string name2, string name3)
		{
			return (Path (Path (Path (context, name1), name2), name3));
		}

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named great-great-grandchild element.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name1, string name2, string name3, string name4)
		{
			return (Path (Path (Path (Path (context, name1), name2), name3), name4));
		}

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named great-great-great-grandchild element.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name1, string name2, string name3, string name4, string name5)
		{
			return (Path (Path (Path (Path (Path (context, name1), name2), name3), name4), name5));
		}

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named great-great-great-grandchild element.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <param name="name6">The name of the required great-great-great-great-grandchild.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name1, string name2, string name3, string name4, string name5, string name6)
		{
			return (Path (Path (Path (Path (Path (Path (context, name1), name2), name3), name4), name5), name6));
		}

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named great-great-great-grandchild element.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <param name="name6">The name of the required great-great-great-great-grandchild.</param>
		/// <param name="name7">The name of the required great-great-great-great-great-grandchild.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name1, string name2, string name3, string name4, string name5, string name6, string name7)
		{
			return (Path (Path (Path (Path (Path (Path (Path (context, name1), name2), name3), name4), name5), name6), name7));
		}

		/// <summary>
		/// Evaluates a simple single valued path access from the given context
		/// node to the named great-great-great-grandchild element.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <param name="name6">The name of the required great-great-great-great-grandchild.</param>
		/// <param name="name7">The name of the required great-great-great-great-great-grandchild.</param>
		/// <param name="name8">The name of the required great-great-great-great-great-great-grandchild.</param>
		/// <returns>The child <see cref="XmlElement"/> or <b>null</b> if no
		/// matching element exists.</returns>
		public static XmlElement Path (XmlElement context, string name1, string name2, string name3, string name4, string name5, string name6, string name7, string name8)
		{
			return (Path (Path (Path (Path (Path (Path (Path (Path (context, name1), name2), name3), name4), name5), name6), name7), name8));
		}

		//---------------------------------------------------------------------------

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named child elements. The '*', '.' and '..' specifiers are
		/// supported.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name">The name of the required child.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// child elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name)
		{
            if (context != null) {
			    if (name.Equals ("*"))
				    return ((context != null) ? DOM.GetChildElements (context) : MutableNodeList.EMPTY);
			    else if (name.Equals (".")) {
				    if (context != null) {
					    MutableNodeList	list = new MutableNodeList ();

					    list.Add (context);
					    return (list);
				    }
				    else
					    return (MutableNodeList.EMPTY);
			    }
			    else if (name.Equals ("..")) {
				    if ((context != null) && (context.ParentNode != null)) {
					    MutableNodeList	list = new MutableNodeList ();

					    list.Add (context.ParentNode);
					    return (list);
				    }
				    else
					    return (MutableNodeList.EMPTY);
			    }
			    
				return (DOM.GetElementsByLocalName (context, name));
            }
            return (MutableNodeList.EMPTY);
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named child elements.
		/// </summary>
		/// <param name="contexts">The context <see cref="XmlElement"/>.</param>
		/// <param name="name">The name of the required child.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// child elements.</returns>
		public static XmlNodeList Paths (XmlNodeList contexts, string name)
		{
			MutableNodeList		list = new MutableNodeList ();

			foreach (XmlElement context in contexts)
				list.AddAll (Paths (context, name));

			return (list);
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named grandchild elements.
		/// </summary>
		/// <param name="contexts">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// child elements.</returns>
		public static XmlNodeList Paths (XmlNodeList contexts, string name1, string name2)
		{
			MutableNodeList		list = new MutableNodeList ();

			foreach (XmlElement context in contexts)
				list.AddAll (Paths (Paths (context, name1), name2));

			return (list);
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named grandchild elements.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// grandchild elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name1, string name2)
		{
			return (Paths (Paths (context, name1), name2));
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named great-grandchild elements.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// great-grandchild elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name1, string name2, string name3)
		{
			return (Paths (Paths (Paths (context, name1), name2), name3));
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named great-great-grandchild elements.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// great-great-grandchild elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name1, string name2, string name3, string name4)
		{
			return (Paths (Paths (Paths (Paths (context, name1), name2), name3), name4));
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named great-great-great-grandchild elements.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// great-great-great-grandchild elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name1, string name2, string name3, string name4, string name5)
		{
			return (Paths (Paths (Paths (Paths (Paths (context, name1), name2), name3), name4), name5));
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named great-great-great-great-grandchild elements.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <param name="name6">The name of the required great-great-great-great-grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// great-great-great-great-grandchild elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name1, string name2, string name3, string name4, string name5, string name6)
		{
			return (Paths (Paths (Paths (Paths (Paths (Paths (context, name1), name2), name3), name4), name5), name6));
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named great-great-great-great-grandchild elements.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <param name="name6">The name of the required great-great-great-great-grandchild.</param>
		/// <param name="name7">The name of the required great-great-great-great-great-grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// great-great-great-great-grandchild elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name1, string name2, string name3, string name4, string name5, string name6, string name7)
		{
			return (Paths (Paths (Paths (Paths (Paths (Paths (Paths (context, name1), name2), name3), name4), name5), name6), name7));
		}

		/// <summary>
		/// Evaluates a simple multiple valued path access from the given context
		/// node to the named great-great-great-great-grandchild elements.
		/// </summary>
		/// <param name="context">The context <see cref="XmlElement"/>.</param>
		/// <param name="name1">The name of the required child.</param>
		/// <param name="name2">The name of the required grandchild.</param>
		/// <param name="name3">The name of the required great-grandchild.</param>
		/// <param name="name4">The name of the required great-great-grandchild.</param>
		/// <param name="name5">The name of the required great-great-great-grandchild.</param>
		/// <param name="name6">The name of the required great-great-great-great-grandchild.</param>
		/// <param name="name7">The name of the required great-great-great-great-great-grandchild.</param>
		/// <param name="name8">The name of the required great-great-great-great-great-great-grandchild.</param>
		/// <returns>A possibly empty <see cref="XmlNodeList"/> of matching
		/// great-great-great-great-grandchild elements.</returns>
		public static XmlNodeList Paths (XmlElement context, string name1, string name2, string name3, string name4, string name5, string name6, string name7, string name8)
		{
			return (Paths (Paths (Paths (Paths (Paths (Paths (Paths (Paths (context, name1), name2), name3), name4), name5), name6), name7), name8));
		}

		/// <summary>
		/// Ensures no instances can be constructed.
		/// </summary>
		private XPath ()
		{ }
	}
}