// Copyright (C),2005-2011 HandCoded Software Ltd.
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
using System.Text;

namespace HandCoded.FpML.Util
{
    /// <summary>
    /// Instances of the <b>Version</b> class hold a version number expressed
    /// as two major and minor component values. Instances can be compared to
    /// determine equality and relative ordering.
    /// </summary>
    public sealed class Version : IComparable
    {
        /// <summary>
        /// Contains the major number.
        /// </summary>
        public int Major {
            get {
                return (major);
            }
        }
        
        /// <summary>
        /// Contains the minor number.
        /// </summary>
        public int Minor {
            get {
                return (minor);
            }
        }

        /// <summary>
        /// Parse the FpML version number given as an argument and return a
        /// corresponding <b>Version</b> instance. A hash table is used to
        /// lookup previously converted strings.
        /// </summary>
        /// <param name="version">The version number string.</param>
        /// <returns>A <b>Version</b> instance containing the decoded major and
        /// minor values.</returns>
        public static Version Parse (string version)
	    {
		    if (!extent.ContainsKey (version))
			    extent [version] = new Version (version);
    		
		    return (extent [version]);
	    }

		/// <summary>
		/// Determines if this <b>Version</b> instance and another hold the same
		/// version number.
		/// </summary>
		/// <param name="other">The <see cref="object"/> instance to compare with.</param>
		/// <returns><c>true</c> if both instance represent the same version number,
		/// <c>false</c> otherwise.</returns>
		public override bool Equals (object other)
		{
			return ((other is Version) && Equals (other as Version));
		}

        /// <summary>
        /// Compares two <b>Version</b> instances and determines if they
	    /// contain the same version number.
        /// </summary>
        /// <param name="other">The <b>Version</b> instance to compare with.</param>
        /// <returns><c>true</c> if both instances contain the same major and
	    ///	minor component values.</returns>
        public bool Equals (Version other)
        {
            return ((major == other.major) && (minor == other.minor));
        }

        /// <summary>
		/// Returns the result of comparing this instance to another <see cref="Object"/>.
		/// </summary>
		/// <param name="other">The <see cref="Object"/> instance to compare with.</param>
		/// <returns>An integer value indicating the relative ordering.</returns>
		/// <exception cref="InvalidCastException">If the argument is not a
		/// <c>Date</c> instance.</exception>
		public int CompareTo (Object other)
		{
			return (CompareTo (other as Version));
		}

        /// <summary>
        /// Compares two <b>Version</b> instances to determine thier relative
        /// ordering.
        /// </summary>
        /// <param name="other">The <b>Version</b> instance to compare with.</param>
        /// <returns>A negative value if this instance is less than the other, a
	    ///	positive value if it is greater and zero if both are the same.</returns>
        public int CompareTo (Version other)
        {
		    if (major == other.major) {
			    if (minor == other.minor)
				    return (0);
			    else
				    return ((minor < other.minor) ? -1 : 1);
		    }
		    else
			    return ((major < other.major) ? -1 : 1);
        }

        /// <summary>
		/// Returns the hash value of the date for hash based data structures and
		/// algorithms. 
		/// </summary>
		/// <returns>The hash value of the date.</returns>
		public override int GetHashCode()
        {
            return (ToString ().GetHashCode ());
        }

		/// <summary>
		/// Converts the instance data members to a <see cref="string"/> representation
		/// that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
        public override string ToString()
        {
            return (major + "-" + minor);
        }

        /// <summary>
        /// The extent set of all <b>Version</b> instances.
        /// </summary>
        private static Dictionary<string, Version>  extent
            = new Dictionary<string, Version> ();

        /// <summary>
        /// The major component of the version number.
        /// </summary>
	    private readonly int		major;

    	/// <summary>
    	/// The minor component of the version number.
    	/// </summary>
	    private readonly int		minor;

        /// <summary>
        /// Constructs a <b>Version</b> instance from a 'major-minor' format
	    /// string value.
        /// </summary>
        /// <param name="version">The version number string.</param>
        private Version (string version)
            : this (version.Split ('-'))
        { }

        /// <summary>
        /// Constructs a <b>Version</b> instance from an array of two
        /// strings containing the component values.</summary>
        /// <param name="parts">The version number parts.</param>
        private Version (string [] parts)
            : this (Int32.Parse (parts [0]), Int32.Parse (parts [1]))
        { }

        /// <summary>
        /// Constructs a <b>Version</b> instance from major and minor
	    /// component values.
        /// </summary>
        /// <param name="major">The major number component.</param>
        /// <param name="minor">The minor number component.</param>
        private Version (int major, int minor)
        {
            this.major = major;
            this.minor = minor;
        }
    }
}