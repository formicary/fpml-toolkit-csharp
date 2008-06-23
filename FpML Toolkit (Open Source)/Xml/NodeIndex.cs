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
using System.Xml.Schema;

namespace HandCoded.Xml
{
	/// <summary>
	/// The <b>NodeIndex</b> class builds an index of the elements that
	/// comprise a DOM tree to allow subsequent queries to be efficiently
	/// executed.
	/// </summary>
	/// <remarks>A typical DOM document instance performs an expensive in-order
	/// traversal of the DOM tree every time the <b>GetElementsByName</b>
	/// method is called. This class does a one off traversal on construction and
	/// then uses the cached results to return <see cref="XmlNodeList"/> instances.
	/// <p/>
	/// The <b>NodeIndex</b> also indexes 'id' attributes as the DOM has been
	/// found to be unreliable at indexing these during post parse schema validation.
	/// <p/>
	/// Since TFP 1.2 the <b>NodeIndex</b> also attempts to capture schema
	/// type information for the <see cref="XmlDocument"/> as it explores it.
	/// Derivation relationships between types are determined and cached as calls
	/// to <b>GetElementsByType</b> are made.
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
		/// Contains a flag indicating if the <b>NodeIndex</b> has managed to acquire
		/// type information during the indexing process.
		/// </summary>
		public bool HasTypeInformation {
			get {
				return (typesByName.Count != 0);
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
			XmlNodeList		list = elementsByName [name] as XmlNodeList;

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

		public XmlNodeList GetElementsByType (string ns, string type)
		{
			ArrayList	matches = compatibleTypes [type] as ArrayList;
			
			if (matches == null) {
				compatibleTypes.Add (type, matches = new ArrayList ());
				
	//			System.err.println ("%% Looking for " + ns + ":" + type);
				
				foreach (string key in typesByName.Keys) {
					ArrayList types = typesByName [key] as ArrayList;
					
					foreach (XmlSchemaType info in types) {
						if (type.Equals (info.Name) || IsDerived (new XmlQualifiedName (type, ns), info)) {
							matches.Add (info);
	//						System.err.println ("%% Found: " + info.getTypeName ());
						}
					}
				}
			}
			
			MutableNodeList		result = new MutableNodeList ();
			
			foreach (XmlSchemaType info in matches) {
				XmlNodeList nodes = elementsByType [info.Name] as XmlNodeList;
				
	//			System.err.println ("-- Matching elements of type: " + info.getTypeName ());
				
				foreach (XmlElement  element in nodes) {
					XmlSchemaType typeInfo = element.SchemaInfo.SchemaType;
					
					if (typeInfo.QualifiedName.Equals (info.QualifiedName)) {
						result.Add (element);
						
	//					System.err.println ("-- Matched: " + element.getLocalName ());
					}
				}
			}
			
			return (result);
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
			return (elementsById [id] as XmlElement);
		}

		/// <summary>
		/// The <see cref="XmlDocument"/> instance that was indexed.
		/// </summary>
		private XmlDocument			document;

		/// <summary>
		/// A <see cref="Hashtable"/> containing <see cref="XmlNodeList"/> instances
		/// indexed by element name.
		/// </summary>
		private Hashtable			elementsByName	= new Hashtable ();

		private Hashtable			elementsByType	= new Hashtable ();

		/// <summary>
		/// A <see cref="Hashtable"/> containing <see cref="XmlElement"/> instances
		/// index by thier id attribute value.
		/// </summary>
		private Hashtable			elementsById	= new Hashtable ();

		private Hashtable			typesByName		= new Hashtable ();
	
		private Hashtable			compatibleTypes	= new Hashtable ();

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
						MutableNodeList	list  = elementsByName [name] as MutableNodeList;

						if (list == null)
							elementsByName.Add (name, list = new MutableNodeList ());

						list.Add (node);

						XmlSchemaType typeInfo = (node as XmlElement).SchemaInfo.SchemaType;
						if ((typeInfo != null) && ((name = typeInfo.Name) != null)) {
							ArrayList 	types = typesByName [name] as ArrayList;
							int		index;
							
							if (types == null)
								typesByName.Add (name, types = new ArrayList ());
							
							for (index = 0; index < types.Count; ++index) {
								XmlSchemaType info = types [index] as XmlSchemaType;
								
								if (typeInfo.QualifiedName.Equals (info.QualifiedName)) break;
							}
							if (index == types.Count) types.Add (typeInfo);
							
							list = (MutableNodeList) elementsByType [name];
							
							if (list == null)
								elementsByType.Add(name, list = new MutableNodeList ());
								
							list.Add (node);
						}

						XmlAttribute	id	  = (node as XmlElement).GetAttributeNode ("id");

						if (id != null) elementsById [id.Value] = node;

						foreach (XmlNode child in (node as XmlElement).ChildNodes)
							if (child.NodeType == XmlNodeType.Element)
								IndexNodes (child);
						break;
					}
			}
		}	

		private bool IsDerived (XmlQualifiedName name, XmlSchemaType type)
		{
			while ((type = type.BaseXmlSchemaType) != null) {
				if (type.QualifiedName.Equals (name))
					return (true);
			}
			return (false);
		}
	}
}