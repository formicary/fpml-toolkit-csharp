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

using HandCoded.Framework;

using log4net;
using log4net.Config;
 
namespace Validate
{
	/// <summary>
	/// This application demonstrates the validation components being used to
	/// perform business level validation of an FpML document.
	/// </summary>
	sealed class Validate : Application
	{
		/// <summary>
		/// Creates an application instance and invokes its <see cref="Run"/>
		/// method passing the command line arguments.
		/// </summary>
		/// <param name="arguments">The command line arguments.</param>
		[STAThread]
		static void Main(string[] arguments)
		{
			log4net.Config.BasicConfigurator.Configure ();

			new Validate ().Run (arguments);
		}

		protected override void StartUp ()
		{
			if (repeatOption.Present) {
				repeat = Int32.Parse (repeatOption.Value);
				if (repeat <= 0) {
					log.Error ("The repeat count must be >= 1");
					Environment.Exit (1);
				}
			}
			random = randomOption.Present;

			if (Arguments.Length == 0) {
				log.Error ("No files are present on the command line");
				Environment.Exit (1);
			}
		}

		protected override void Execute ()
		{
			Finished = true;
		}

		/// <summary>
		/// Provides a text description of the expected arguments.
		/// </summary>
		/// <returns>A description of the expected application arguments.</returns>
		protected override string DescribeArguments ()
		{
			return (" files ...");
		}

		/// <summary>
		/// The <see cref="ILog"/> instance used to record problems.
		/// </summary>
		private static ILog		log
			= LogManager.GetLogger (typeof (Validate));

		/// <summary>
		/// The <see cref="Option"/> instance use to detect <b>-repeat count</b>
		/// </summary>
		private Option			repeatOption
			= new Option ("-repeat", "Number of times to processes the files", "count");
		
		/// <summary>
		/// The <see cref="Option"/> instance use to detect <b>-random</b>
		/// </summary>
		private Option			randomOption
			= new Option ("-random", "Pick files at random for processing");
		
		/// <summary>
		/// A counter for the number of time to reprocess the files. 
		/// </summary>
		private int				repeat = 1;
		
		/// <summary>
		/// A flag indicating whether to randomise the file list.
		/// </summary>
		private bool			random = false;

		private Validate ()
		{ }
	}
}