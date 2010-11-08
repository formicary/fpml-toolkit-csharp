// Copyright (C),2005-2010 HandCoded Software Ltd.
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
	/// The <b>Builder</b> class extends <see cref="Browser"/> and adds methods
	/// that allow the underlying <see cref="XmlDocument"/> to be modified.
	/// </summary>
	public class Builder : Browser
	{
		/// <summary>
		/// Constructs a <b>Builder</b> attached to the root <see cref="XmlElement"/>
		/// of the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to attach to.</param>
		public Builder (XmlDocument document)
			: base (document)
		{ }

		/// <summary>
		/// Constructs a <b>Builder</b> attached to the specified <see cref="XmlElement"/>
		/// of the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to attach to.</param>
		/// <param name="context">The initial context <see cref="XmlElement"/>.</param>
		public Builder (XmlDocument document, XmlElement context)
			: base (document, context)
		{ }

		/// <summary>
		/// Creates and appends a new <see cref="XmlElement"/> with the name
		/// indicated by the parameter <paramref name="name"/> to the current
		/// context element.
		/// </summary>
		/// <param name="name">The name of the new <see cref="XmlElement"/>.</param>
        /// <returns>The <see cref="XmlElement"/> of the new element.</returns>
		public virtual XmlElement AppendElement (string name)
		{
			return (AppendElement (null, name));
		}

		/// <summary>
		/// Creates a new <see cref="XmlElement"/> with the given name and namespace
		/// URI and appends it as a child of context.
		/// </summary>
		/// <param name="uri">The namespace URI or <c>null</c> if none.</param>
		/// <param name="name">The name of the new <see cref="XmlElement"/>.</param>
        /// <returns>The <see cref="XmlElement"/> of the new element.</returns>
		public XmlElement AppendElement (string uri, string name)
		{
			XmlElement	element = document.CreateElement (name, uri);

			context.AppendChild (element);
			return (context = element);
		}

		/// <summary>
		/// Makes the parent <see cref="XmlElement"/> of the context the new
		/// context for future operations.
		/// </summary>
		public void CloseElement ()
		{
			MoveToParent ();
		}

		/// <summary>
		/// Sets the value of the named attribute to the given value.
		/// </summary>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The new value of the attribute.</param>
		public virtual void SetAttribute (string name, string value)
		{
			context.SetAttribute (name, value);
		}

		/// <summary>
		/// Sets the value of the named attribute in the indicated namespace,
		/// to the given value.
		/// </summary>
		/// <param name="uri">The namespace URI of the attribute.</param>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The new value of the attribute.</param>
		public void SetAttribute (string uri, string name, string value)
		{
			context.SetAttribute (name, uri, value);
		}

		/// <summary>
		/// Creates a new <see cref="XmlText"/> node and appends it to the
		/// children of the context node.
		/// </summary>
		/// <param name="text">The value of the new <see cref="XmlText"/> node.</param>
		public void AppendText (string text)
		{
			context.AppendChild (document.CreateTextNode (text));
		}

		/// <summary>
		/// Creates a new <see cref="XmlCDataSection"/> node and appends it to the
		/// children of the context node.
		/// </summary>
		/// <param name="data">The value of the new <see cref="XmlCDataSection"/> node.</param>
		public void AppendCData (string data)
		{
			context.AppendChild (document.CreateCDataSection (data));
		}

		/// <summary>
		/// Creates a new <see cref="XmlComment"/> node and appends it to the
		/// children of the context node.
		/// </summary>
		/// <param name="comment">The value of the new <see cref="XmlComment"/> node.</param>
		public void AppendComment (string comment)
		{
			context.AppendChild (document.CreateComment (comment));
		}

		/// <summary>
		/// Creates a new leaf element containing the specified text string and
		/// appends it to the children of the context node.
		/// </summary>
		/// <param name="name">The name of the new <see cref="XmlElement"/>.</param>
		/// <param name="text">The value of the contained <see cref="XmlText"/> node.</param>
        /// <returns>The <see cref="XmlElement"/> of the new element.</returns>
		public virtual XmlElement AppendElementAndText (string name, string text)
		{
			return (AppendElementAndText (null, name, text));
		}

		/// <summary>
		/// Creates a new leaf element containing the specified text string and
		/// appends it to the children of the context node.
		/// </summary>
		/// <param name="uri">The namespace URI of the new element or <c>null</c>.</param>
		/// <param name="name">The name of the new <see cref="XmlElement"/>.</param>
		/// <param name="text">The value of the contained <see cref="XmlText"/> node.</param>
        /// <returns>The <see cref="XmlElement"/> of the new element.</returns>
		public XmlElement AppendElementAndText (string uri, string name, string text)
		{
			XmlElement  result;
            
            result = AppendElement (uri, name);
			AppendText (text);
			CloseElement ();
            return (result);
		}

        /// <summary>
        /// Appends a copy of the document fragment at the end of the element
        /// referenced by the context node.
        /// </summary>
        /// <param name="fragment">The <see cref="XmlDocument"/> containing the fragment.</param>
        public void AppendDocument (XmlDocument fragment)
        {
            context.AppendChild (fragment.DocumentElement.CloneNode (true));
        }
	}
}