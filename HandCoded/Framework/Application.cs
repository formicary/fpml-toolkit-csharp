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
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace HandCoded.Framework
{
	/// <summary>
	/// The <b>Application</b> class provides a basic application framework.
	/// Derived classes extend its functionality and specialise it to a
	/// particular task.
	/// </summary>
	public abstract class Application : Process
	{
		/// <summary>
		/// Contains the current <b>Application instance.</b>
		/// </summary>
		public static Application CurrentApplication {
			get {
				return (application);
			}
		}

        /// <summary>
        /// Contains the path to the application data directory.
        /// </summary>
        public static string DataDirectory {
            get {
                string dataDirectory = ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.DataDirectory"];

                if (dataDirectory.Equals ("."))
                    return (AppDomain.CurrentDomain.BaseDirectory);
                else
                    return (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, dataDirectory));
            }
        }

        /// <summary>
        /// Derives the path to the indicated location relative to the applications
        /// data directory.
        /// </summary>
        /// <param name="where">The target location.</param>
        /// <returns>The resolved path to the target location.</returns>
        public static string PathTo (string where)
        {
            return (Path.Combine (DataDirectory, where));
        }

		/// <summary>
		/// Causes the <b>Application</b> to process it's command line arguments
		/// and begin the execution cycle.
		/// </summary>
		/// <param name="arguments">The array of command line arguments.</param>
		public void Run (string [] arguments)
		{
			this.arguments = Option.ProcessArguments (arguments);

			base.Run ();
		}

		/// <summary>
		/// Converts the instance data members to a <see cref="string"/>
		/// representation that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
		public override string ToString ()
		{
			return (GetType ().FullName + "[" + ToDebug () + "]");
		}

		/// <summary>
		/// Contains the command line arguments after any option processing
		/// has been applied.
		/// </summary>
		protected string [] Arguments {
			get {
				return (arguments);
			}
		}

		/// <summary>
		/// Returns a list of all the assemblies that are referenced by the
		/// executing application.
		/// </summary>
		/// <returns>A list of <cref class="Assembly"/> instances.</returns>
		public static List<Assembly> GetAllAssemblies ()
		{
			if (assemblies == null) {
				List<Assembly> result = new List<Assembly> ();

				result.Add (Assembly.GetEntryAssembly ());
				foreach (AssemblyName name in Assembly.GetEntryAssembly ().GetReferencedAssemblies ())
					result.Add (Assembly.Load (name));

				assemblies = result;
			}
			return (assemblies);
		}

		/// <summary>
		/// Searches all of the assemblies that comprise the current application to
		/// locate a <see cref="Type"/> instance for the indicated type name.
		/// </summary>
		/// <param name="name">The qualified name of the required type.</param>
		/// <returns>The corresponding <see cref="Type"/> instance or <c>null</c> if the
		/// type could not be found.</returns>
		public static Type GetType (string name)
		{
			Type type			= null;
			
			foreach (Assembly assembly in GetAllAssemblies ())
				if ((type = assembly.GetType (name)) != null) break;
			
			return (type);
		}

		/// <summary>
		/// Constructs an <b>Application</b> instance and records it.
		/// </summary>
		protected Application()
		{
			application = this;
		}

		/// <summary>
		/// Provides an <b>Application</b> with a chance to perform any
		/// initialisation. This implementation checks for the -help option.
		/// Derived classes may extend the functionality.
		/// </summary>
		protected override void StartUp ()
		{
			if (helpOption.Present) {
				System.Console.Error.WriteLine ("Usage:\n");
				// TODO get application name.
				System.Console.Error.WriteLine ("    " + Option.ListOptions () + DescribeArguments ());
				System.Console.Error.WriteLine ("Options:");
				Option.DescribeOptions ();
				Environment.Exit (1);
			}
		}

		/// <summary>
		/// Provides a text description of the arguments expected after the options
		/// (if any), for example "file ...". This method should be overridedn in a
		/// derived class requiring an non-empty argument list.
		/// </summary>
		/// <returns>A description of the expected application arguments.</returns>
		protected virtual string DescribeArguments ()
		{
			return ("");
		}

		/// <summary>
		/// Converts the instance's member values to <see cref="string"/>
		/// representations and concatenates them all together. This function is
		/// used by ToString and my be overwritten in derived classes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
		protected virtual string ToDebug ()
		{
			StringBuilder	builder	= new StringBuilder ();

			builder.Append ("arguments=");
			if (arguments != null) {
				builder.Append ('[');
				for (int index = 0; index < arguments.Length; ++index) {
					if (index != 0) builder.Append (',');

					if (arguments [index] != null)
						builder.Append ("\"" + arguments [index] + "\"");
					else
						builder.Append ("null");
				}
			}
			else
				builder.Append ("null");

			builder.Append (",finished=" + Finished);

			return (builder.ToString ());
		}

		/// <summary>
		/// The set of assemblies referenced by the current application.
		/// </summary>
		private static List<Assembly> assemblies = null;

		/// <summary>
		/// The one and only <b>Application</b> instance.
		/// </summary>
		private static Application	application	= null;

		/// <summary>
		/// The <see cref="Option"/> instance used to detect <b>-help</b>.
		/// </summary>
		private Option				helpOption
			= new Option ("-help", "Displays help information");

		/// <summary>
		/// The command line argumenrs after processing.
		/// </summary>
		private string []			arguments	= null;
	}
}