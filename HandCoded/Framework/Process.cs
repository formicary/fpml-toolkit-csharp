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
	/// The <b>Process</b> class provides a basic process framework.
	/// Derived classes extend its functionality and specialise it to a
	/// particular task.
	/// </summary>
	public abstract class Process
	{
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
		/// Causes the <b>Process</b> to process to begin execution.
		/// </summary>
		public void Run ()
		{
			StartUp ();
			while (!Finished)
				Execute ();
			CleanUp ();
		}

		/// <summary>
		/// Constructs a <b>Process</b> instance.
		/// </summary>
		protected Process ()
		{ }

		/// <summary>
		/// Provides a <b>Process</b> with a chance to perform any
		/// initialisation. This implementation does nothing. Derived classes
		/// may extend the functionality.
		/// </summary>
		protected virtual void StartUp ()
		{ }

		/// <summary>
		/// The <b>Execute</b> method should perform one program execution
		/// cycle. The method is called repeatedly until the finished flag is set.
		/// </summary>
		protected abstract void Execute ();

		/// <summary>
		/// Provides a <b>Process</b> with a change to perform any
		/// closing actions. This implementation does nothing. Derived classes
		/// may extend the functionality.
		/// </summary>
		protected virtual void CleanUp ()
		{ }

		/// <summary>
		/// A <b>bool</b> flag to indicate that we are done.
		/// </summary>
		private bool				finished	= false;
	}
}