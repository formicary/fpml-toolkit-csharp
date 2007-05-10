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

namespace HandCoded.Framework
{
	/// <summary>
	/// The <b>Option</b> class provide a basic command line processing
	/// capability. Instances of <b>Option</b> define the keywords to look
	/// for and the presence of associated parameters. The application should pass
	/// the entire set of command line strings recieved from <b>Main</b> to
	/// the processing function. The state of any option referenced by the strings
	/// is updated and any remaining strings are returned to the caller.
	/// </summary>
	public class Option
	{
		/// <summary>
		/// Contains a flag indicating the <b>Option</b> was present.
		/// </summary>
		public bool Present {
			get {
				return (present);
			}
		}

		/// <summary>
		/// Contains the value of the associated parameter.
		/// </summary>
		public string Value {
			get {
				return (value);
			}
		}

		/// <summary>
		/// Constructs a <b>Option</b> instance for an option that has an
		/// associated parameter value (e.g. -output &lt;file&gt;).
		/// </summary>
		/// <param name="name">The name of the option (e.g. -output).</param>
		/// <param name="description">A description of the options purpose.</param>
		/// <param name="parameter">A description of the required parameter or
		/// <b>null</b> if none allowed.</param>
		public Option (string name, string description, string parameter)
		{
			this.name		 = name;
			this.description = description;
			this.parameter	 = parameter;

			options.Add (this);
		}

		/// <summary>
		/// Constructs a <b>Option</b> instance for an option that does not
		/// have a parameter.
		/// </summary>
		/// <param name="name">The name of the option (e.g. -help).</param>
		/// <param name="description">A description of the options purpose.</param>
		public Option (string name, string description)
			: this (name, description, null)
		{ }


		/// <summary>
		/// Converts the instance data members to a <see cref="string"/> representation
		/// that can be displayed for debugging purposes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
		public override string ToString ()
		{
			return (GetType ().FullName + "[" + ToDebug () + "]");
		}

		/// <summary>
		/// Processes the command line arguments to extract options and
		/// parameter values. 
		/// </summary>
		/// <param name="arguments">The command line arguments pass to <b>Main</b></param>
		/// <returns>The remaining command line arguments after options have been
		/// removed.</returns>
		public static string [] ProcessArguments (string [] arguments)
		{
			int				index;
			string []		remainder;

			for (index = 0; index < arguments.Length; ++index) {
				bool			matched = false;

				foreach (Option option in options) {
					if (matched = arguments [index].Equals (option.name)) {
						option.present = true;

						if (option.parameter != null)
							option.value = arguments [++index];
						break;
					}
				}
				if (!matched) break;
			}

			// Copy the tail of the argument list to a new array
			remainder = new string [arguments.Length - index];
			for (int count = 0; index < arguments.Length;)
				remainder [count++] = arguments [index++];

			return (remainder);
		}

		/// <summary>
		/// Returns a string describing the available command line options.
		/// </summary>
		/// <returns>A string describing the command line options.</returns>
		public static string ListOptions ()
		{
			StringBuilder		buffer	= new StringBuilder ();

			foreach (Option option in options) {
				if (buffer.Length == 0) buffer.Append (' ');

				buffer.Append ('[');
				buffer.Append (option.name);
				if (option.parameter != null) {
					buffer.Append (' ');
					buffer.Append (option.parameter);
				}
				buffer.Append (']');
			}

			return (buffer.ToString ());
		}

		/// <summary>
		/// Prints out a description of the options and thier parameters.
		/// </summary>
		public static void DescribeOptions ()
		{
			string				spaces	= "                                            ";

			foreach (Option option in options) {
				if (option.parameter != null)
					System.Console.Out.WriteLine ("    "
						+ (option.name + " " + option.parameter + spaces).Substring (0, 16)
						+ " " + option.description);
				else
					System.Console.Out.WriteLine ("    "
						+ (option.name + spaces).Substring (0, 16)
						+ " " + option.description);
			}
		}

		/// <summary>
		/// Converts the instance's member values to <see cref="string"/>
		/// representations and concatenates them all together. This function is
		/// used by ToString and my be overwritten in derived classes.
		/// </summary>
		/// <returns>The object's <see cref="string"/> representation.</returns>
		protected string ToDebug ()
		{
			StringBuilder		builder	= new StringBuilder ();
	
			builder.Append ("name="
				+ ((name != null) ? name : "null"));
			builder.Append (",description="
				+ ((description != null) ? description : "null"));
			builder.Append (",parameter="
				+ ((parameter != null) ? parameter : "null"));
			builder.Append (",present=" + present);
			builder.Append (",value="
				+ ((value != null) ? value : "null"));

			return (builder.ToString ());
		}

		/// <summary>
		/// The set of all defined <b>Option</b> instances.
		/// </summary>
		private static ArrayList	options		= new ArrayList ();

		/// <summary>
		/// The name of the option (including any leading dash).
		/// </summary>
		private readonly string		name;

		/// <summary>
		/// A brief description of the purpose.
		/// </summary>
		private readonly string		description;

		/// <summary>
		/// A brief description of the parameter (if any).
		/// </summary>
		private readonly string		parameter;

		/// <summary>
		/// A flag to indicate that the option was present.
		/// </summary>
		private bool				present		= false;

		/// <summary>
		/// The actual value provided on the command line.
		/// </summary>
		private string				value		= null;
	}
}