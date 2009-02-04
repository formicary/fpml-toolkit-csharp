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
	/// The <b>Writer</b> class creates and manages a <see cref="TextWriter"/>
	/// used to output an XML document. The derived classes determine how
	/// the actual content is formatted on the stream.
	/// </summary>
	public abstract class XmlWriter
	{
		/// <summary>
		/// Returns the <see cref="Encoding"/> that will be applied to the
		/// characters writen to the output <see cref="TextWriter"/>. 
		/// </summary>
		public Encoding Encoding {
			get {
				return (writer.Encoding);
			}
		}

		/// <summary>
		/// Formats and writes the indicated <see cref="XmlDocument"/> to the
		/// output stream using the style implemented by the class instance.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be formatted.</param>
		public abstract void Write (XmlDocument document);

		/// <summary>
		/// The <see cref="TextWriter"/> used to record the generated XML.
		/// </summary>
		protected TextWriter		writer;

		/// <summary>
		/// Constructs a <c>Writer</c> that will output XML to the given
		/// <c>Stream</c> using UTF-8 character encoding.
		/// </summary>
		/// <param name="writer">The <c>TextWriter</c> to write to.</param>
		protected XmlWriter (TextWriter writer)
			: this (writer, Encoding.UTF8)
		{ }

		/// <summary>
		/// Constructs a <c>Writer</c> that will output XML to the given
		/// <c>Stream</c> using the specified character encoding.
		/// </summary>
		/// <param name="writer">The <c>TextWriter</c> to write to.</param>
		/// <param name="encoding">The required character encoding.</param>
		protected XmlWriter (TextWriter writer, Encoding encoding)
		{
			this.writer = writer;
		}

		/// <summary>
		/// Outputs a character string converting any characters used by XML
		/// for control purposes to their escaped format.
		/// </summary>
		/// <param name="text">The text to output.</param>
		/// <param name="isAttribute"><c>true</c> if the text is an attribute value.</param>
		protected void Escape (string text, bool isAttribute)
		{
			foreach (char ch in text) {
				switch (ch) {
				case '&':	writer.Write ("&amp;");		break;
				case '<':	writer.Write ("&lt;");		break;
				case '>':	writer.Write ("&gt;");		break;

				// Quotes must be escaped in attribute values
				case '"':
					{
						if (isAttribute)
							writer.Write ("&quot;");
						else
							writer.Write (ch);
						break;
					}

				default:	writer.Write (ch);			break;
				}
			}
		}
	}
}