// Copyright (C),2005-2007 HandCoded Software Ltd.
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

using HandCoded.Classification;
using HandCoded.FpML;
using HandCoded.Framework;
using HandCoded.Xml;

using log4net;
using log4net.Config;

namespace Classify
{
	/// <summary>
	/// This application demonstrates the classification components being used to
	/// identify the type of product within an FpML document based on its structure.
	/// </summary>
	sealed class Classify : Application
	{
		/// <summary>
		/// Creates an application instance and invokes its <see cref="Run"/>
		/// method passing the command line arguments.
		/// </summary>
		/// <param name="arguments">The command line arguments.</param>
		[STAThread]
		static void Main (string [] arguments)
		{
			log4net.Config.DOMConfigurator.Configure ();

			new Classify ().Run (arguments);
		}

		/// <summary>
		/// Processes the command line options and gets ready to start file
		/// processing.
		/// </summary>
		protected override void StartUp ()
		{
			base.StartUp ();

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

			XmlDocument		document;
			NodeIndex		nodeIndex;

			try {
				for (int index = 0; index < files.Count; ++index) {
					string filename = (files [index] as FileInfo).FullName;
					FileStream	stream	= File.OpenRead (filename);

					document = XmlUtility.NonValidatingParse (stream);
					nodeIndex = new NodeIndex (document);

					System.Console.WriteLine (filename + ":");
					DoClassify (nodeIndex.GetElementsByName ("trade"), "Trade");
					DoClassify (nodeIndex.GetElementsByName ("contract"), "Contract");
			
					stream.Close ();
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
			= LogManager.GetLogger (typeof (Classify));

		/// <summary>
		/// Constructs a <b>Classify</b> instance.
		/// </summary>
		private Classify ()
		{ }

		/// <summary>
		/// Uses the predefined FpML product types to attempt to classify a
		/// product within the document.
		/// </summary>
		/// <param name="list">A set of context elements to analyze.</param>
		/// <param name="container">The type of product container for display.</param>
		private void DoClassify (XmlNodeList list, string container)
		{
			foreach (XmlElement element in list) {
				Category	category = ProductType.Classify (element);

				System.Console.Write ("> " + container + "(");
				System.Console.Write ((category != null) ? category.ToString () : "UNKNOWN");
				System.Console.WriteLine (")");
			}
		}
	}
}