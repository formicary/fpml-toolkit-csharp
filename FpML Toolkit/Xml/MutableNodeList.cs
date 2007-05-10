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
	/// The <b>MutableNodeList</b> class implements a <see cref="XmlNodeList"/>
	/// that can have its contents changed.
	/// </summary>
	public sealed class MutableNodeList : XmlNodeList
	{
		/// <summary>
		/// An empty <see cref="XmlNodeList"/>
		/// </summary>
		public static readonly XmlNodeList	EMPTY
			= new MutableNodeList ();

		/// <summary>
		/// Constructs an empty <b>MutableNodeList</b> instance.
		/// </summary>
		public MutableNodeList()
		{ }

		/// <summary>
		/// Contains the number of <see cref="XmlNode"/> instance in the list.
		/// </summary>
		public override int Count {
			get {
				return (nodes.Count);
			}
		}

		/// <summary>
		/// Adds the indicated <see cref="XmlNode"/> instance to the current
		/// <b>MutableNodeList</b>.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be added.</param>
		public void Add (XmlNode node)
		{
			if (!nodes.Contains (node)) nodes.Add (node);
		}

		/// <summary>
		/// Adds the contents of the given <see cref="XmlNodeList"/> into the
		/// current <b>MutableNodeList</b>.
		/// </summary>
		/// <param name="list">The <see cref="XmlNodeList"/> to be added.</param>
		public void AddAll (XmlNodeList list)
		{
			foreach (XmlNode node in list)
				if (!nodes.Contains (node)) nodes.Add (node);
		}

		/// <summary>
		/// Removes in indicated <see cref="XmlNode"/> from the collection held
		/// within the <b>MutableNodeList</b>.
		/// </summary>
		/// <param name="node">The <see cref="XmlNode"/> to be removed.</param>
		public void Remove (XmlNode node)
		{
			nodes.Remove (node);
		}

		/// <summary>
		/// Removes each of the <see cref="XmlNode"/> instances in the given
		/// <see cref="XmlNodeList"/> from the collection held within the
		/// <b>MutableNodeList</b>.
		/// </summary>
		/// <param name="list">The <see cref="XmlNodeList"/> of instance to remove.</param>
		public void RemoveAll (XmlNodeList list)
		{
			foreach (XmlNode node in list)
				nodes.Remove (node);
		}

		/// <summary>
		/// Removes all <see cref="XmlNode"/> instances from the underlying
		/// collection.
		/// </summary>
		public void Clear ()
		{
			nodes.Clear ();
		}

		/// <summary>
		/// Returns the indicated item from the underlying collection.
		/// </summary>
		/// <param name="index">The position of the required item.</param>
		/// <returns>The <see cref="XmlNode"/> in the indicated position.</returns>
		public override XmlNode Item (int index)
		{
			return (nodes [index] as XmlNode);
		}

		/// <summary>
		/// Returns an <see cref="IEnumerator"/> than can be used to iterate
		/// through the underlying collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/> to access the list.</returns>
		public override IEnumerator GetEnumerator ()
		{
			return (nodes.GetEnumerator ());
		}

		/// <summary>
		/// The underlying collection used to hold <see cref="XmlNode"/> instances.
		/// </summary>
		private ArrayList			nodes	= new ArrayList ();
	}
}