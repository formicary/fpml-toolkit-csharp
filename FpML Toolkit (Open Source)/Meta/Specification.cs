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
using System.Text;
using System.Xml;

namespace HandCoded.Meta
{
	/// <summary>
	/// Instances of the <b>Specification</b> class represent XML based
	/// data models such as those for the standards FpML and FixML.
	/// </summary>
	public sealed class Specification
	{
		/// <summary>
		/// Constructs a <b>Specification</b> with the given name.
		/// </summary>
		/// <param name="name">The unique name for the <b>Specification</b>.</param>
		public Specification (String name)
		{
			extent.Add (this.name = name, this);
		}

		/// <summary>
		/// Attempts to locate the <b>Specification</b> instance corresponding to
		/// the given name.
		/// </summary>
		/// <param name="name">The target <b>Specification</b> name.</param>
		/// <returns>The <b>Specification</b> instance corresponding to the
		/// name or <c>null</c> if it is not recognized.</returns>
		public static Specification ForName (String name)
		{
			return (extent [name] as Specification);
		}

		/// <summary>
		/// Attempts to locate the <b>Specification</b> instance corresponding
		/// to the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be examined.</param>
		/// <returns>The <b>Specification</b> instance corresponding to the
		/// <see cref="XmlDocument"/> or <c>null</c> if it is not recognized.</returns>
		public static Specification ForDocument (XmlDocument document)
		{
			Release		release = ReleaseForDocument (document);

			return ((release != null) ? release.Specification : null);
		}

		/// <summary>
		/// Attempts to locate the <see cref="Release"/> instance corresponding to
		/// the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be examined.</param>
		/// <returns>The <see cref="Release"/> instance corresponding to the
		/// <see cref="XmlDocument"/> or <c>null</c> if it is not recognized.</returns>
		public static Release ReleaseForDocument (XmlDocument document)
		{
			foreach (Specification specification in extent.Values) 
			{
				Release release = specification.GetReleaseForDocument (document);

				if (release != null) return (release);
			}
			return (null);
		}

		/// <summary>
		/// Contains the unique name of this <b>Specification</b>.
		/// </summary>
		public String Name {
			get {
				return (name);
			}
		}

		/// <summary>
		/// Contains all the currently defined <see cref="Release"/> instances associated
		/// with this <b>Specification</b>.
		/// </summary>
		public IEnumerable Releases {
			get {
				return (releases.Values);
			}
		}

		/// <summary>
		/// Determines if the given <see cref="XmlDocument"/> is an instance of some
		/// <see cref="Release"/> of this <b>Specification</b>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be examined.</param>
		/// <returns><c>true</c> if the <see cref="XmlDocument"/> is an instance of the
		/// <b>Specification</b>, <c>false</c> otherwise.</returns>
		public bool IsInstance (XmlDocument document)
		{
			return (GetReleaseForDocument (document) != null);
		}

		/// <summary>
		/// Attempts to locate a <see cref="Release"/> of the current <b>Specification</b>
		/// that is compatible with the given <see cref="XmlDocument"/>.
		/// </summary>
		/// <param name="document">The <see cref="XmlDocument"/> to be examined.</param>
		/// <returns>The corresponding <see cref="Release"/> or <c>null</c> if a match
		/// could not be found.</returns>
		public Release GetReleaseForDocument (XmlDocument document)
		{
			foreach (Release release in releases.Values) {
				if (release.IsInstance (document)) return (release);
			}
			return (null);
		}

		/// <summary>
		/// Attempts to locate a <see cref="Release"/> associated with this
		/// <b>Specification</b> with the indicated version identifier.
		/// </summary>
		/// <param name="version">The target version identifier</param>
		/// <returns>The corresponding <see cref="Release"/> instance or <c>null</c>
		/// if none exists.</returns>
		public Release GetReleaseForVersion (String version)
		{
		    return (releases [version] as Release);
		}

		/// <summary>
		/// Adds the indicated <see cref="Release"/> instance to the set managed
		/// by the <b>Specification</b>.
		/// </summary>
		/// <param name="release">The <see cref="Release"/> to be added.</param>
		/// <exception cref="ArgumentException">If the <see cref="Release"/> is associated
		/// with a different <b>Specification</b>.</exception>
		public void Add (Release release)
		{
			if (release.Specification != this)
				throw new ArgumentException ("The provided release is for a different specification", "release");

			releases [release.Version] = release;
		}

		/// <summary>
		/// Removes the indicated <see cref="Release"/> instance from the set managed
		/// by the <b>Specification</b>.
		/// </summary>
		/// <param name="release">The <see cref="Release"/> to be removed.</param>
		/// <exception cref="ArgumentException">If the <see cref="Release"/> is associated
		/// with a different <b>Specification</b>.</exception>
		public void Remove (Release release)
		{
			if (release.Specification != this)
				throw new ArgumentException ("The provided release is for a different specification", "release");

			releases.Remove (release.Version);
		}

		/// <summary>
		/// Returns a hash code for this instance based on its name.
		/// </summary>
		/// <returns>The instance hash code.</returns>
		public override int GetHashCode ()
		{
			return (name.GetHashCode ());
		}

		/// <summary>
		/// Converts the <b>Specification</b> to a string for debugging.
		/// </summary>
		/// <returns>A text description of the instance.</returns>
		public override string ToString ()
		{
			return (GetType ().FullName + " [" + ToDebug () + "]");
		}

		/// <summary>
		/// The extent set of all <b>Specification</b> instances.
		/// </summary>
		private static Hashtable	extent		= new Hashtable ();

		/// <summary>
		/// The unique name of this <b>Specification</b>.
		/// </summary>
		private readonly String		name;

		/// <summary>
		/// The set of <see cref="Release"/> instances associated with this
		/// <b>Specification</b>.
		/// </summary>
		private Hashtable			releases	= new Hashtable ();

		/// <summary>
		/// Produces a debugging string describing the state of the instance.
		/// </summary>
		/// <returns>The debugging string.</returns>
		private string ToDebug () {
			StringBuilder	buffer = new StringBuilder ();

			buffer.Append ("name=\"");
			buffer.Append (name);
			buffer.Append ("\", releases={");

			bool first = true;

			foreach (Release release in releases) {
				if (!first) buffer.Append (',');

				buffer.Append ('\"');
				buffer.Append (release.Version);
				buffer.Append ('\"');
				first = false;
			}
			buffer.Append ('}');

			return (buffer.ToString ());
		}
	}
}