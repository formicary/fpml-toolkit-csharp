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

namespace HandCoded.Xml
{
	/// <summary>
	/// The <b>NodeIndex</b> class builds an index of the elements that
	/// comprise a DOM tree to allow subsequent queries to be efficiently
	/// executed.
	/// </summary>
	/// <remarks>A typical DOM document instance performs an expensive in-order
	/// traversal of the DOM tree every time the <see cref="GetElementsByName"/>
	/// method is called. This class does a one off traversal on construction and
	/// then uses the cached results to return <see cref="XmlNodeList"/> instances.
	/// </remarks>
	public sealed class NodeIndex
	{
		/// <summary>
		/// Constructs a <b>NodeIndex</b> for the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be indexed.</param>
		public NodeIndex (XmlDocument document)
		{
			IndexNodes (this.document = document);
		}

		/// <summary>
		/// Contains the indexed <see cref="XmlDocument"/> instance.
		/// </summary>
		public XmlDocument Document {
			get {
				return (document);
			}
		}

		/// <summary>
		/// Creates a (possibly empty) <see cref="XmlNodeList"/> containing all the
		/// element nodes identified by the given name string.
		/// </summary>
		/// <param name="name">The name of the required elements.</param>
		/// <returns>A <see cref="XmlNodeList"/> of corresponding nodes.</returns>
		public XmlNodeList GetElementsByName (string name)
		{
			XmlNodeList		list = elements [name] as XmlNodeList;

			return ((list != null) ? list : MutableNodeList.EMPTY);
		}

		/// <summary>
		/// Creates a (possibly empty) <see cref="XmlNodeList"/> containing all the
		/// element nodes identified by the given name strings.
		/// </summary>
		/// <param name="names">The name of the required elements.</param>
		/// <returns>A <see cref="XmlNodeList"/> of corresponding nodes.</returns>
		public XmlNodeList GetElementsByName (string [] names)
		{
			MutableNodeList list = new MutableNodeList ();

			foreach (string name in names)
				list.AddAll (GetElementsByName (name));

			return (list);
		}

		/// <summary>
		/// Returns the <see cref="XmlElement"/> in the indexed document that has
		/// an id attribute with the given value. 
		/// </summary>
		/// <param name="id">The required id attribute value.</param>
		/// <returns>The matching <see cref="XmlElement"/> or <c>null</c> if 
		/// none.</returns>
		public XmlElement GetElementById (string id)
		{
			return (ids [id] as XmlElement);
		}

		/// <summary>
		/// The <see cref="XmlDocument"/> instance that was indexed.
		/// </summary>
		private XmlDocument			document;

		/// <summary>
		/// A <see cref="Hashtable"/> containing <see cref="XmlNodeList"/> instances
		/// indexed by element name.
		/// </summary>
		private Hashtable			elements	= new Hashtable ();

		/// <summary>
		/// A <see cref="Hashtable"/> containing <see cref="XmlElement"/> instances
		/// index by thier id attribute value.
		/// </summary>
		private Hashtable			ids			= new Hashtable ();

		/// <summary>
		/// Recursively walks a DOM tree creating an index of the elements by
		/// thier local name.
		/// </summary>
		/// <param name="node">The next <see cref="XmlNode"/> to be indexed.</param>
		private void IndexNodes (XmlNode node)
		{
			switch (node.NodeType) {
			case XmlNodeType.Document:
					IndexNodes ((node as XmlDocument).DocumentElement);
					break;

			case XmlNodeType.Element:
					{
						string			name  = (node as XmlElement).LocalName;
						MutableNodeList	list  = elements [name] as MutableNodeList;

						if (list == null)
							elements.Add (name, list = new MutableNodeList ());

						list.Add (node);

						XmlAttribute	id	  = (node as XmlElement).GetAttributeNode ("id");

						if (id != null) ids [id.Value] = node;

						foreach (XmlNode child in (node as XmlElement).ChildNodes)
							if (child.NodeType == XmlNodeType.Element)
								IndexNodes (child);
						break;
					}
			}
		}	
	}
}