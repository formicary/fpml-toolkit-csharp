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
using System.Runtime.Serialization;

namespace HandCoded.Meta
{
	/// <summary>
	/// Summary description for ConversionException.
	/// </summary>
	public class ConversionException : ApplicationException
	{
		/// <summary>
		/// Initializes a new instance of the <b>ConversionException</b> class.
		/// </summary>
		public ConversionException ()
		{ }

		/// <summary>
		/// Initializes a new instance of the <b>ConversionException</b> class with a
		/// specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public ConversionException (string message)
			: base (message)
		{ }

		/// <summary>
		/// Initializes a new instance of the <b>ConversionException</b> class with
		/// serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		public ConversionException (SerializationInfo info, StreamingContext context)
			: base (info, context)
		{ }
	}
}