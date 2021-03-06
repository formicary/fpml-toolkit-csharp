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
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using HandCoded.FpML;
using HandCoded.FpML.Validation;
using HandCoded.Framework;
using HandCoded.Meta;
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

		    // Initialise the default catalog
		    string		catalogPath = ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.XmlCatalog"];

		    if (catalogOption.Present) {
			    if (catalogOption.Value != null)
				    catalogPath = catalogOption.Value;
			    else
				    log.Error ("Missing argument for -catalog option");
		    }

			if (repeatOption.Present) {
				repeat = Int32.Parse (repeatOption.Value);
				if (repeat <= 0) {
					log.Error ("The repeat count must be >= 1");
					Environment.Exit (1);
				}
			}

			schemaOnly	= schemaOnlyOption.Present;
			random		= randomOption.Present;

			if (Arguments.Length == 0) {
				log.Error ("No files are present on the command line");
				Environment.Exit (1);
			}

		    if (reportOption.Present) {
			    Console.WriteLine ("<?xml version=\"1.0\"?>");
                Console.WriteLine ("<report>");			
		    }

            try {
			    XmlUtility.DefaultCatalog = CatalogManager.Find (PathTo (catalogPath));
            }
            catch (Exception error) {
                log.Error ("Failed to parse XML catalog", error);
				Environment.Exit (1);
            }

			// Activate the FpML Schemas
		    foreach (Release release in Releases.FPML.Releases) {
			    if (release is SchemaRelease)
				    XmlUtility.DefaultSchemaSet.Add (release as SchemaRelease);	
		    }

		    XmlUtility.DefaultSchemaSet.XmlSchemaSet.Compile ();
	    }

        /// <summary>
        /// Completes any close down actions.
        /// </summary>
        protected override void CleanUp ()
        {
            base.CleanUp ();

            if (reportOption.Present) {
                Console.WriteLine ("</report>");
            }

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
					String	        location = directory.ToString ();
					string			target	 = Arguments [index];

					while (target.StartsWith (@"..\")) {
                        location = location.Substring (0, location.LastIndexOf (Path.DirectorySeparatorChar));
						target = target.Substring (3);
					}
					FindFiles (files, Path.Combine (location, target));
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

					    if (reportOption.Present)
                            Console.WriteLine ("\t<file name=\"" + (files [which] as FileInfo).Name + "\">");
					    else
                            Console.WriteLine (">> " + (files [which] as FileInfo).Name);

						FileStream	stream	= File.OpenRead ((files [which] as FileInfo).FullName);

						FpMLUtility.ParseAndValidate (schemaOnly, stream, rules,
								new ValidationEventHandler (SyntaxError),
								new ValidationErrorHandler (SemanticError));
				
						stream.Close ();

                        if (reportOption.Present)
                            Console.WriteLine ("\t</file>");

                        ++count;
					}
				}

				DateTime		end		= DateTime.Now;
				TimeSpan		span	= end.Subtract (start);

                if (reportOption.Present)
                    Console.WriteLine ("\t<statistics count=\"" + count
                            + "\" time=\"" + span.TotalMilliseconds
                            + "\" rate=\"" + ((1000.0 * count) / span.TotalMilliseconds)
                            + "\" rules=\"" + rules.Size + "\"/>");
                else {
                    Console.WriteLine ("== Processed " + count + " files in "
                        + span.TotalMilliseconds + " milliseconds");
                    Console.WriteLine ("== " + ((1000.0 * count) / span.TotalMilliseconds)
                        + " files/sec checking " + rules.Size + " rules");
                }
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
		/// The <see cref="Option"/> instance used to detect <b>-strict</b>.
		/// </summary>
		private Option			strictOption
			= new Option ("-strict", "Use only FpML defined rules (no extensions)");
		
		/// <summary>
		/// The <see cref="Option"/> instance used to detect <b>-schemaOnly</b>.
		/// </summary>
		private Option			schemaOnlyOption
			= new Option ("-schemaOnly", "Only support XML Schema based documents");
		
		/// <summary>
		/// The <see cref="Option"/> instance used to detect <b>-catalog</b>.
		/// </summary>
	    private Option			catalogOption
		    = new Option ("-catalog", "Use url to create an XML catalog for parsing", "url");

        /// <summary>
        /// The <see cref="Option"/> instance use to detect <b>-report</b>
        /// </summary>
	    private Option			reportOption
		    = new Option ("-report", "Generate an XML report of the results");
    	
		/// <summary>
		/// A counter for the number of time to reprocess the files. 
		/// </summary>
		private int				repeat = 1;
		
		/// <summary>
		/// A flag indicating whether to randomise the file list.
		/// </summary>
		private bool			random = false;

		/// <summary>
		/// Defines the type(s) of grammar supported.
		/// </summary>
		private bool			schemaOnly;

		/// <summary>
		/// Constructs a <b>Validate</b> instance.
		/// </summary>
		private Validate ()
		{ }

		/// <summary>
		/// Creates a list of files to be processed by expanding a path and handling
		/// wildcards.
		/// </summary>
		/// <param name="files">The set of files to be processed.</param>
		/// <param name="path">The path to be processed.</param>
		private void FindFiles (ArrayList files, string path)
		{
			if (Directory.Exists (path)) {
				foreach (string subdir in Directory.GetDirectories (path)) {
					if ((new DirectoryInfo (subdir).Attributes & FileAttributes.Hidden) == 0)
						FindFiles (files, subdir);
				}

				foreach (string file in Directory.GetFiles (path, "*.xml")) {
					FileInfo	info = new FileInfo (file);

					if ((info.Attributes & FileAttributes.Hidden) == 0)
						files.Add (info);
				}
			}
			else {
                string directory = Path.GetDirectoryName (path);
                string pattern = Path.GetFileName (path);

				foreach (string file in Directory.GetFiles (directory, pattern)) {
					FileInfo	info = new FileInfo (file);

					if ((info.Attributes & FileAttributes.Hidden) == 0)
						files.Add (info);
				}
			}
		}

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
			if (reportOption.Present) {
                Console.WriteLine ("\t\t<validationError rule=\"" + rule
						+ "\" context=\"" + XPath.ForNode (context)
						+ "\"" + ((data != null) ? (" additionalData=\"" + data + "\"") : "")
						+ ">" + description + "</validationError>");
			}
			else {
			    if (data != null)
				    Console.WriteLine (rule + " " + XPath.ForNode (context) + " " + description + " [" + data + "]");
			    else
				    Console.WriteLine (rule + " " + XPath.ForNode (context) + " " + description);
            }
		}
	}
}
