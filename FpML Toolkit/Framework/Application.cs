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
using System.Text;

namespace HandCoded.Framework
{
	/// <summary>
	/// The <b>Application</b> class provides a basic application framework.
	/// Derived classes extend its functionality and specialise it to a
	/// particular task.
	/// </summary>
	public abstract class Application
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
		/// Contains a flag that indicates when execution is finished.
		/// </summary>
		public bool Finished {
			get {
				return (finished);
			}
			set {
				finished = value;
			}
		}

		/// <summary>
		/// Causes the <b>Application</b> to process it's command line arguments
		/// and begin the execution cycle.
		/// </summary>
		/// <param name="arguments">The array of command line arguments.</param>
		public void Run (string [] arguments)
		{
			this.arguments = Option.ProcessArguments (arguments);

			StartUp ();
			while (!finished)
				Execute ();
			CleanUp ();
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
		protected virtual void StartUp ()
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
		/// The <b>Execute</b> method should perform one program execution
		/// cycle. The method is called repeatedly until the finished flag is set.
		/// </summary>
		protected abstract void Execute ();

		/// <summary>
		/// Provides an <b>Application</b> with a change to perform any
		/// closing actions. This implementation does nothing. Derived classes
		/// may extend the functionality.
		/// </summary>
		protected virtual void CleanUp ()
		{ }

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

			builder.Append (",finished=" + finished);

			return (builder.ToString ());
		}

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

		/// <summary>
		/// A <b>bool</b> flag to indicate that we are done.
		/// </summary>
		private bool				finished	= false;
	}
}