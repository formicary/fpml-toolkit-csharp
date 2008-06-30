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
            return (elementsByName.ContainsKey (name) ? elementsByName [name] : MutableNodeList.EMPTY);
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

            foreach (string name in names) {
                if (elementsByName.ContainsKey (name))
                    list.AddAll (elementsByName [name]);
            }
			return (list);
		}

        /// <summary>
        /// Creates a (possibly empty) <see cref="XmlNodeList"/> containing all the
	    /// element nodes of a given type (or a derived subtype).
        /// </summary>
        /// <param name="ns">The required namespace URI.</param>
        /// <param name="type">The required type.</param>
        /// <returns>A <see cref="XmlNodeList"/> of corresponding nodes.</returns>
		public XmlNodeList GetElementsByType (string ns, string type)
		{
			List<XmlSchemaType>	matches;
			
			if (!compatibleTypes.ContainsKey (type)) {
				compatibleTypes.Add (type, matches = new List<XmlSchemaType> ());
				
	//			System.err.println ("%% Looking for " + ns + ":" + type);
				
				foreach (string key in typesByName.Keys) {
					List<XmlSchemaType> types = typesByName [key];
					
					foreach (XmlSchemaType info in types) {
						if (type.Equals (info.Name) || IsDerived (new XmlQualifiedName (type, ns), info)) {
							matches.Add (info);
	//						System.err.println ("%% Found: " + info.getTypeName ());
						}
					}
				}
			}
            else
                matches = compatibleTypes [type];
			
			MutableNodeList		result = new MutableNodeList ();
			
			foreach (XmlSchemaType info in matches) {
     //			System.err.println ("-- Matching elements of type: " + info.getTypeName ());
                if (elementsByType.ContainsKey (info.Name)) {
                    foreach (XmlElement element in elementsByType [info.Name]) {
                        XmlSchemaType typeInfo = element.SchemaInfo.SchemaType;

                        if (typeInfo.QualifiedName.Equals (info.QualifiedName)) {
                            result.Add (element);

     //					    System.err.println ("-- Matched: " + element.getLocalName ());
                        }
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
			return (elementsById.ContainsKey (id) ? elementsById [id] : null);
		}

		/// <summary>
		/// The <see cref="XmlDocument"/> instance that was indexed.
		/// </summary>
		private XmlDocument			document;

		/// <summary>
        /// A collection containing <see cref="MutableNodeList"/> instances
		/// indexed by element name.
		/// </summary>
		private Dictionary<string, MutableNodeList>	elementsByName
            = new Dictionary<string, MutableNodeList> ();

        /// <summary>
        /// A collection containing <see cref="MutableNodeList"/> instances
        /// indexed by type name.
        /// </summary>
        private Dictionary<string, MutableNodeList> elementsByType
            = new Dictionary<string, MutableNodeList> ();

		/// <summary>
		/// A collection containing <see cref="XmlElement"/> instances
		/// indexed by thier id attribute value.
		/// </summary>
        private Dictionary<string, XmlElement> elementsById
            = new Dictionary<string, XmlElement> ();

        /// <summary>
        /// A collection containing <see cref="XmlSchemaType"/>
        /// instances indexed by name.
        /// </summary>
        private Dictionary<string, List<XmlSchemaType>> typesByName
            = new Dictionary<string, List<XmlSchemaType>> ();
	
        /// <summary>
        /// A collection containing a list for each explored type
        /// containing related types defined by extension or restriction.
        /// </summary>
		private Dictionary<string, List<XmlSchemaType>>	compatibleTypes
            = new Dictionary<string, List<XmlSchemaType>> ();

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
						MutableNodeList	list;

						if (!elementsByName.ContainsKey (name))
							elementsByName.Add (name, list = new MutableNodeList ());
                        else
                            list = elementsByName [name];

						list.Add (node);

						XmlSchemaType typeInfo = (node as XmlElement).SchemaInfo.SchemaType;
						if ((typeInfo != null) && ((name = typeInfo.Name) != null)) {
                            List<XmlSchemaType> types;
                            int		            index;
							
                            if (!typesByName.ContainsKey (name))
                                typesByName.Add (name, types = new List<XmlSchemaType> ());
                            else
                                types =  typesByName [name];

							for (index = 0; index < types.Count; ++index) {
								XmlSchemaType info = types [index];
								
								if (typeInfo.QualifiedName.Equals (info.QualifiedName)) break;
							}
							if (index == types.Count) types.Add (typeInfo);
							
							if (!elementsByType.ContainsKey (name))
								elementsByType.Add (name, list = new MutableNodeList ());
                            else
                                list = elementsByType [name];
								
							list.Add (node);
						}

						XmlAttribute	id	  = (node as XmlElement).GetAttributeNode ("id");

						if (id != null) elementsById [id.Value] = node as XmlElement;

						foreach (XmlNode child in (node as XmlElement).ChildNodes)
							if (child.NodeType == XmlNodeType.Element)
								IndexNodes (child);
						break;
					}
			}
		}	

        /// <summary>
        /// Walks up the type inheritance structure to determine if the indicated
        /// <see cref="XmlSchemaType"/> is derived from the named type.
        /// </summary>
        /// <param name="name">The target parent type.</param>
        /// <param name="type">The type being tested.</param>
        /// <returns><c>true</c> if the types are related, <c>false</c> otherwise.</returns>
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