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
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

using HandCoded.Xml;

using log4net;

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
		public Specification (string name)
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
		public static Specification ForName (string name)
		{
            return (extent [name]);
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
		public string Name {
			get {
				return (name);
			}
		}

		/// <summary>
		/// Contains all the currently defined <see cref="Release"/> instances associated
		/// with this <b>Specification</b>.
		/// </summary>
		public IEnumerable<Release> Releases {
			get {
				return (releases);
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
			foreach (Release release in releases) {
				if (release.IsInstance (document)) return (release);
			}
			return (null);
		}

		/// <summary>
		/// Attempts to locate a <see cref="Release"/> associated with this
		/// <b>Specification</b> with the indicated version identifier.
		/// </summary>
		/// <param name="version">The target version identifier.</param>
		/// <returns>The corresponding <see cref="Release"/> instance or <c>null</c>
		/// if none exists.</returns>
		public Release GetReleaseForVersion (string version)
		{
            foreach (Release release in releases)
                if (release.Version.Equals (version)) return (release);

		    return (null);
		}

		/// <summary>
		/// Attempts to locate a <see cref="SchemaRelease"/> associated with this
		/// <b>Specification</b> with the indicated version identifier and
        /// namespace URI.
		/// </summary>
		/// <param name="version">The target version identifier.</param>
        /// <param name="namespaceUri">The target namespace URI.</param>
		/// <returns>The corresponding <see cref="SchemaRelease"/> instance or <c>null</c>
		/// if none exists.</returns>
		public SchemaRelease GetReleaseForVersionAndNamespace (string version, string namespaceUri)
		{
            foreach (Release release in releases) {
                if (release is SchemaRelease) {
                    SchemaRelease schemaRelease = release as SchemaRelease;
                    if (schemaRelease.Version.Equals (version) &&
                        schemaRelease.NamespaceUri.Equals (namespaceUri)) return (schemaRelease);
                }
            }

		    return (null);
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

			releases.Add (release);
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

			releases.Remove (release);
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
		/// A <see cref="ILog"/> instance used to report run-time problems.
		/// </summary>
		private static ILog			log
			= LogManager.GetLogger (typeof (Specification));

        /// <summary>
		/// The extent set of all <b>Specification</b> instances.
		/// </summary>
		private static Dictionary<string, Specification> extent
            = new Dictionary<string, Specification> ();

		/// <summary>
		/// The unique name of this <b>Specification</b>.
		/// </summary>
		private readonly string		name;

		/// <summary>
		/// The set of <see cref="Release"/> instances associated with this
		/// <b>Specification</b>.
		/// </summary>
		private List<Release> releases
            = new List<Release> ();

        /// <summary>
        /// If the releases file defines a custom class loader to be used the process
        /// the data block identified by the context element then return its name,
        /// otherwise return the indicated default class name.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/>.</param>
        /// <param name="defaultClass">The name of the default class loader if not overridden.</param>
        /// <returns>The name of the class loader to be instantiated.</returns>
	    private static string GetClassLoader (XmlElement context, string defaultClass)
	    {
		    foreach (XmlElement element in XPath.Paths (context, "classLoader")) {
			    string platform = DOM.GetAttribute (element, "platform");
    			
			    if ((platform != null) && platform.Equals (".Net"))
				    return (DOM.GetAttribute (element, "class"));
		    }
		    return (defaultClass);
	    }

        /// <summary>
        /// Creates an <see cref="IReleaseLoader"/> that can process a DTD meta
        /// definition.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/>.</param>
        /// <returns>An instance of the <see cref="IReleaseLoader"/> interface.</returns>
	    private static IReleaseLoader GetDtdReleaseLoader (XmlElement context)
	    {
		    string targetName = GetClassLoader (context, "HandCoded.Meta.DefaultDTDReleaseLoader");
    		
		    try {
                Type targetClass = Type.GetType (targetName);
    		
			    return (targetClass.GetConstructor (Type.EmptyTypes).Invoke (null) as IReleaseLoader); 
		    }
		    catch (Exception error) {
			    log.Fatal ("Failed to get class loader type: " + targetName, error);
			    throw error;
		    }
	    }

        /// <summary>
        /// Creates an <see cref="IReleaseLoader"/> that can process a schema meta
        /// definition.
        /// </summary>
        /// <param name="context">The context <see cref="XmlElement"/>.</param>
        /// <returns>An instance of the <see cref="IReleaseLoader"/> interface.</returns>
	    private static IReleaseLoader GetSchemaReleaseLoader (XmlElement context)
	    {
		    string targetName = GetClassLoader (context, "HandCoded.Meta.DefaultSchemaReleaseLoader");
    		
		    try {
                Type targetClass = Type.GetType (targetName);
    		
			    return (targetClass.GetConstructor (Type.EmptyTypes).Invoke (null) as IReleaseLoader); 
		    }
		    catch (Exception error) {
			    log.Fatal ("Failed to get class loader type: " + targetName, error);
			    throw error;
		    }
	    }

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

        /// <summary>
        /// Bootstrap the entire collection of specifications by processing the
	    /// contents of the 'files/releases.xml' file.
        /// </summary>
        static Specification ()
        {
            Dictionary<string, SchemaRelease>   loadedSchemas
                = new Dictionary<string,SchemaRelease> ();

    		log.Debug ("Bootstrapping Specifications");

		    try {
                FileStream  stream = new FileStream (
                    ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.Releases"],
                    FileMode.Open);

			    XmlDocument document = XmlUtility.NonValidatingParse (stream);
    				
			    foreach (XmlElement context in XPath.Paths (document.DocumentElement, "specification")) {
				    XmlElement name = XPath.Path (context, "name");
    				
				    Specification specification = new Specification (Types.ToToken (name));
    				
				    foreach (XmlElement node in XPath.Paths (context, "dtdRelease"))
					    GetDtdReleaseLoader (node).LoadData (specification, node, loadedSchemas);
    				
				    foreach (XmlElement node in XPath.Paths (context, "schemaRelease"))
					    GetSchemaReleaseLoader (node).LoadData (specification, node, loadedSchemas);
			    }
                stream.Close ();
		    }
		    catch (Exception error) {
			    log.Fatal ("Unable to load specifications", error);
		    }

            log.Debug ("Completed");
        }
	}
}