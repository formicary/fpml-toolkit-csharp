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
using System.IO;
using System.Xml;
using System.Xml.Schema;

using HandCoded.FpML;
using HandCoded.FpML.Validation;
using HandCoded.Framework;
using HandCoded.Validation;
using HandCoded.Xml;
using HandCoded.Xml.Resolver;

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
		static void Main (string[] arguments)
		{
			log4net.Config.DOMConfigurator.Configure ();

			new Validate ().Run (arguments);
		}

		/// <summary>
		/// Processes the command line options and gets ready to start file
		/// processing.
		/// </summary>
		protected override void StartUp ()
		{
			base.StartUp ();

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

			XmlUtility.DefaultCatalog = CatalogManager.Find ("files/catalog.xml");

			// Activate the FpML Schemas
			XmlUtility.DefaultSchemaSet.Add (Releases.R4_0);
			XmlUtility.DefaultSchemaSet.Add (Releases.R4_1);
			XmlUtility.DefaultSchemaSet.Add (Releases.R4_2);
			XmlUtility.DefaultSchemaSet.Add (Releases.R4_3);

			XmlUtility.DefaultSchemaSet.XmlSchemaSet.Compile ();
		}

		/// <summary>
		/// Perform the file processing while timing the operation.
		/// </summary>
		protected override void Execute ()
		{
			DirectoryInfo	directory = new DirectoryInfo (Environment.CurrentDirectory);
			ArrayList		files	= new ArrayList ();

			try {
				for (int index = 0; index < Arguments.Length; ++index) {
					DirectoryInfo	location = directory;
					string			target	 = Arguments [index];

					while (target.StartsWith (@"..\")) {
						location = location.Parent;
						target = target.Substring (3);
					}
					FileInfo []		info = location.GetFiles (target);

					foreach (FileInfo file in info) files.Add (file); 
				}
			}
			catch (Exception) {
				log.Fatal ("Invalid command line argument");

				Finished = true;
				return;
			}

			RuleSet			rules	= strictOption.Present ? FpMLRules.Rules : AllRules.Rules;
			Random			rng		= new Random ();
			DateTime		start	= DateTime.Now;
			int				count	= 0;

			try {
				while (repeat-- > 0) {
					for (int index = 0; index < files.Count; ++index) {
						int		which = random ? rng.Next (files.Count) : index;

						Console.WriteLine (">> " + (files [which] as FileInfo).Name);

						FileStream	stream	= File.OpenRead ((files [which] as FileInfo).FullName);

						FpMLUtility.ParseAndValidate (stream, rules,
								new ValidationEventHandler (SyntaxError),
								new ValidationErrorHandler (SemanticError));
				
						stream.Close ();
						++count;
					}
				}

				DateTime		end		= DateTime.Now;
				TimeSpan		span	= end.Subtract (start);

				Console.WriteLine ("== Processed " + count + " files in "
					+ span.TotalMilliseconds + " milliseconds");
				Console.WriteLine ("== " + ((1000.0 * count) / span.TotalMilliseconds)
					+ " files/sec checking " + rules.Size + " rules");
			}
			catch (Exception error) {
				log.Fatal ("Unexpected exception during processing", error);
			}

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
		/// The <see cref="Option"/> instance used to detect <b>-repeat count</b>.
		/// </summary>
		private Option			repeatOption
			= new Option ("-repeat", "Number of times to processes the files", "count");
		
		/// <summary>
		/// The <see cref="Option"/> instance used to detect <b>-random</b>.
		/// </summary>
		private Option			randomOption
			= new Option ("-random", "Pick files at random for processing");

		/// <summary>
		/// The <see cref="Option"/> instance used to detct <b>-strict</b>.
		/// </summary>
		private Option			strictOption
			= new Option ("-strict", "Use only FpML defined rules (no extensions)");
		
		/// <summary>
		/// A counter for the number of time to reprocess the files. 
		/// </summary>
		private int				repeat = 1;
		
		/// <summary>
		/// A flag indicating whether to randomise the file list.
		/// </summary>
		private bool			random = false;

		/// <summary>
		/// Constructs a <b>Validate</b> instance.
		/// </summary>
		private Validate ()
		{ }

		/// <summary>
		/// Report XML parser errors.
		/// </summary>
		/// <param name="sender">The object raising the error.</param>
		/// <param name="args">A description of the parser error.</param>
		private void SyntaxError (object sender, ValidationEventArgs args)
		{
			Console.WriteLine (args.Message);
		}

		/// <summary>
		/// Report FpML semantic errors.
		/// </summary>
		/// <param name="code">The error code.</param>
		/// <param name="context">The context element.</param>
		/// <param name="description">A dxescription of the problem.</param>
		/// <param name="rule">The rule identifier.</param>
		/// <param name="data">Any additional data.</param>
		private void SemanticError (string code, XmlNode context, string description, string rule, string data)
		{
			if (data != null)
				Console.WriteLine (rule + " " + XPath.ForNode (context) + " " + description + " [" + data + "]");
			else
				Console.WriteLine (rule + " " + XPath.ForNode (context) + " " + description);
		}
	}
}
