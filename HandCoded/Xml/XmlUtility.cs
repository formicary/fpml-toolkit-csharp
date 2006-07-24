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
using System.IO;
using System.Text;
using System.Xml;

using HandCoded.Xml.Writer;

namespace HandCoded.Xml
{
	/// <summary>
	/// A collection of utility functions for traversing the structure of
	/// a DOM tree represented by a <see cref="XmlDocument"/> and its
	/// constitutent <see cref="XmlNode"/> instances.
	/// </summary>
	public sealed class XmlUtility
	{
		/// <summary>
		/// Produces a detailed dump of the given <see cref="XmlNode"/> instance
		/// and its descendents on the console output stream.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be dumped.</param>
		public static void DumpNode (XmlNode node)
		{
			DumpNode (node, System.Console.Out, 0);
		}
	
		/// <summary>
		/// Produces a detailed dump of the given <see cref="XmlNode"/> instance
		/// and its descendents via the indicated <see cref="TextWriter"/>.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be dumped.</param>
		/// <param name="writer">The <see cref="TextWriter"/> to use for output.</param>	
		public static void DumpNode (XmlNode node, TextWriter writer)
		{
			DumpNode (node, writer, 0);
		}

		/// <summary>
		/// Ensures no instances can be constructed.
		/// </summary>
		private XmlUtility()
		{ }

		/// <summary>
		/// Recursively dumps out an <see cref="XmlNode"/> and its descendents
		/// to the indicated <see cref="TextWriter"/> in a simple indented style.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be dumped.</param>
		/// <param name="writer">The <see cref="TextWriter"/> to use for output.</param>
		/// <param name="indent">The current level of indentation.</param>
		private static void DumpNode (XmlNode node, TextWriter writer, int indent)
		{
			if (node != null) {
				for (int i = 0; i < indent; ++i)
					writer.Write ("  ");

				switch (node.NodeType) {
				case XmlNodeType.Document:
					{
						writer.WriteLine ("[Document]");
						DumpNode ((node as XmlDocument).DocumentType, writer, indent + 1);
						DumpNode ((node as XmlDocument).DocumentElement, writer, indent + 1);
						break;
					}

				case XmlNodeType.Element:
					{
						writer.Write ("[Element]");
						writer.Write ((node as XmlElement).Name);
						writer.WriteLine (" {" + node.NamespaceURI + "}");
						
						foreach (XmlNode attr in (node as XmlElement).Attributes)
							DumpNode (attr, writer, indent + 1);

						foreach (XmlNode child in (node as XmlElement).ChildNodes)
							DumpNode (child, writer, indent + 1);

						break;
					}

				case XmlNodeType.Attribute:
					{
						writer.Write ("[Attribute]");
						writer.Write ((node as XmlAttribute).Name);	
						writer.Write ("=" + (node as XmlAttribute).Value);
						writer.WriteLine (" {" + node.NamespaceURI + "}");
						break;
					}

				case XmlNodeType.Text:
					{
						writer.Write ("[Text]");
						writer.WriteLine ((node as XmlText).Value);
						break;
					}	
				}
			}
		}
	}
}