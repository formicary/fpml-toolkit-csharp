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

namespace HandCoded.Xml.Writer
{
	/// <summary>
	/// The <b>NestedWriter</b> produces a formated XML document that uses
	/// indentation to show the structure of the information. If an element has
	/// attributes these are put on their own line to make them easy to read
	/// whilst a leaf element is formatted to enclose its value. For example:
	/// <code>
	/// &lt;?XML version="1.0" encoding="UTF-8"?&gt;
	/// &lt;root&gt;
	///   &lt;element
	///    attr="value"&gt;
	///     &lt;leaf&gt;text&lt;/leaf;&gt;
	///     &lt;leaf/&gt;
	///   &lt;/element&gt;
	/// &lt;/root&gt;
	/// &lt;/?XML&gt;
	/// </code>
	/// </summary>
	public class NestedWriter : XmlWriter
	{
		/// <summary>
		/// Constructs a <b>NestedWriter</b>.
		/// </summary>
		/// <param name="writer">The <see cref="TextWriter"/> to write to.</param>
		public NestedWriter (TextWriter writer)
			: base (writer)
		{ }

		/// <summary>
		/// Constructs a <b>NestedWriter</b>.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		public NestedWriter (Stream stream, Encoding encoding)
			: base (new StreamWriter (stream, encoding))
		{ }

		/// <summary>
		/// Formats and writes the indicated <see cref="XmlDocument"/> to the
		/// output stream using the style implemented by the class instance.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be formatted.</param>
		public override void Write (XmlDocument document)
		{
			XmlDocumentType		doctype = document.DocumentType;
	
			writer.WriteLine ("<?xml version=\"1.0\" encoding=\"" + Encoding.WebName + "\"?>");

			if (doctype != null) {
				writer.Write ("<!DOCTYPE " + doctype.Name);

				if (doctype.PublicId != null) {
					writer.Write (" PUBLIC \"" + doctype.PublicId + "\"");
					writer.Write (" \"" + doctype.SystemId + "\"");
				}
				else if (doctype.SystemId != null)
					writer.Write (" SYSTEM \"" + doctype.SystemId + "\"");

				if ((doctype.InternalSubset != null) && (doctype.InternalSubset.Length > 0)) {
					writer.WriteLine (" [");
					writer.Write (doctype.InternalSubset);
					writer.Write (']');
				}
				writer.WriteLine ('>');
			}
			Write (document.DocumentElement, 0);
			writer.Flush ();
		}

		/// <summary>
		/// Converts a <see cref="XmlDocument"/> to a compact string
		/// representation.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be converted.</param>
		/// <returns>The document's string representation.</returns>
		public static string ToString (XmlDocument document)
		{
			StringBuilder	xml		= new StringBuilder ();
			TextWriter		writer	= new StringWriter (xml);

			new NestedWriter (writer).Write (document);
			writer.Close ();

			return (xml.ToString ());
		}

		/// <summary>
		/// Formats and writes the indicated <see cref="XmlNode"/> to the
		/// output stream using the style implemented by the class instance.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be formatted.</param>
		/// <param name="level">The current recursion depth.</param>
		private void Write (XmlNode node, int level)
		{
			switch (node.NodeType) {
			case XmlNodeType.Element:
				{
					XmlElement		element = node as XmlElement;

					for (int count = 0; count < level; ++count)
						writer.Write ("  ");

					writer.Write ('<');
					writer.Write (element.Name);

					foreach (XmlAttribute attr in element.Attributes) {
						if (attr.Specified) {
							writer.WriteLine ();
							for (int count = 0; count < level; ++count)
								writer.Write ("  ");

							writer.Write (' ');
							writer.Write (attr.Name);
							writer.Write ("=\"");
							Escape (attr.Value, true);
							writer.Write ('"');
						}
					}

					if (element.HasChildNodes) {
						bool	startLine = false;
						writer.Write ('>');

						foreach (XmlNode child in element.ChildNodes) {
							if (child.NodeType == XmlNodeType.Text) {
								Escape ((child as XmlText).Data.Trim (), false);
								startLine = false;
							}
							else {
								writer.WriteLine ();
								Write (child, level + 1);
								startLine =true;
							}
						}

						if (startLine) {
							writer.WriteLine ();
							for (int count = 0; count < level; ++count)
								writer.Write ("  ");
						}
						writer.Write ("</");
						writer.Write (element.Name);
						writer.Write ('>');
					}
					else
						writer.Write ("/>");

					break;
				}

			case XmlNodeType.Comment:
				{
					for (int count = 0; count < level; ++count)
						writer.Write ("  ");

					writer.Write ("<!-- ");
					writer.Write ((node as XmlComment).Data.Trim ());
					writer.WriteLine (" -->");
					break;
				}

			case XmlNodeType.Text:
				{
					writer.Write ((node as XmlText).Data.Trim ());
					break;
				}

			case XmlNodeType.CDATA:
				{
					writer.Write ("<![CDATA[");
					writer.Write ((node as XmlCDataSection).Data.Trim ());
					writer.Write ("]]>");
					break;
				}

			case XmlNodeType.EntityReference:
				{
					writer.Write ('&');
					writer.Write ((node as XmlEntityReference).Value);
					writer.Write (';');
					break;
				}

			case XmlNodeType.ProcessingInstruction:
				{
					writer.Write ("<?");
					writer.Write ((node as XmlProcessingInstruction).Name);

					string value = (node as XmlProcessingInstruction).Value;
					if ((value != null) && (value.Length > 0)) {
						writer.Write (' ');
						writer.Write (value);
					}
					writer.Write ("?>");
					break;
				}
			}
		}
	}
}
