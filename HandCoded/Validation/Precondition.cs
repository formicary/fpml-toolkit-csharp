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
using System.Xml;

using HandCoded.Xml;

namespace HandCoded.Validation
{
	/// <summary>
	/// A <b>Precondition</b> instance is used to determine the applicability
	/// of a <see cref="Rule"/> to a <see cref="XmlDocument"/>.
	/// </summary>
	public abstract class Precondition
	{
		/// <summary>
		/// A <b>Precondition</b> instance that is always <c>true</c>.
		/// </summary>
		public static readonly	Precondition	ALWAYS	= new Precondition.Always ();

		/// <summary>
		/// A <b>Precondition</b> instance that is always <c>false</c>.
		/// </summary>
		public static readonly  Precondition	NEVER	= new Precondition.Never ();

		/// <summary>
		/// Creates a new <b>Precondition</b> that is the logical NOT of the
		/// given arguments.
		/// </summary>
		/// <param name="pre">The <b>Precondition</b> to be inverted.</param>
		/// <returns>A new <b>Precondition</b> derived from the argument.</returns>
		public static Precondition Not (Precondition pre)
		{
			return (new NotPrecondition (pre));
		}

		/// <summary>
		/// Creates a new <b>Precondition</b> that is the logical AND of the
		/// given arguments.
		/// </summary>
		/// <param name="lhs">The left hand side <b>Precondition</b>.</param>
		/// <param name="rhs">The right hand side <b>Precondition</b>.</param>
		/// <returns>A new <b>Precondition</b> derived from the arguments.</returns>
		public static Precondition And (Precondition lhs, Precondition rhs)
		{
			return (new AndPrecondition (lhs, rhs));
		}

		/// <summary>
		/// Creates a new <b>Precondition</b> that is the logical OR of the
		/// given arguments.
		/// </summary>
		/// <param name="lhs">The left hand side <b>Precondition</b>.</param>
		/// <param name="rhs">The right hand side <b>Precondition</b>.</param>
		/// <returns>A new <b>Precondition</b> derived from the arguments.</returns>
		public static Precondition Or (Precondition lhs, Precondition rhs)
		{
			return (new OrPrecondition (lhs, rhs));
		}

		/// <summary>
		/// Evaluates this <b>Precondition</b> against the contents of the
		/// indicated <see cref="NodeIndex"/>.
		/// </summary>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
		/// <returns>A <c>bool</c> value indicating the applicability of this
		/// <b>Precondition</b> to the <see cref="XmlDocument"/>.</returns>
		public abstract bool Evaluate (NodeIndex nodeIndex);

		/// <summary>
		/// The <b>UnaryPrecondition</b> class holds the reference to the
		/// underlying <see cref="Precondition"/> for unary logic operators.
		/// </summary>
		protected abstract class UnaryPrecondition : Precondition
		{
			/// <summary>
			/// The underlying <see cref="Precondition"/>.
			/// </summary>
			protected readonly Precondition		pre;

			/// <summary>
			/// Constructs a <b>UnaryPrecondition</b> instance.
			/// </summary>
			/// <param name="pre">The underlying <see  cref="Precondition"/>.</param>
			protected UnaryPrecondition (Precondition pre)
			{
				this.pre = pre;
			}
		}

		/// <summary>
		/// The <b>BinaryPrecondition</b> class holds the references to the
		/// underlying <see cref="Precondition"/> instances for binary logic operators.
		/// </summary>
		protected abstract class BinaryPrecondition : Precondition
		{
			/// <summary>
			/// The underlying left hand side <see cref="Precondition"/>.
			/// </summary>
			protected readonly Precondition		lhs;

			/// <summary>
			/// The underlying right hand side <see cref="Precondition"/>.
			/// </summary>
			protected readonly Precondition		rhs;

			/// <summary>
			/// Constructs a <c>BinaryPrecondition</c> instance.
			/// </summary>
			/// <param name="lhs">The left hand side <see cref="Precondition"/>.</param>
			/// <param name="rhs">The right hand side <see cref="Precondition"/>.</param>
			protected BinaryPrecondition (Precondition lhs, Precondition rhs)
			{
				this.lhs = lhs;
				this.rhs = rhs;
			}
		}

		/// <summary>
		/// The <b>NotPrecondition</b> class implements the unary logical NOT
		/// operation.
		/// </summary>
		protected sealed class NotPrecondition : UnaryPrecondition
		{
			/// <summary>
			/// Constructs a <b>NotPrecondition</b> which will invert some
			/// underlying <see cref="Precondition"/>.
			/// </summary>
			/// <param name="pre">The underlying <see cref="Precondition"/>.</param>
			public NotPrecondition (Precondition pre)
				: base (pre)
			{ }

			/// <summary>
			/// Evaluates this <b>Precondition</b> against the contents of the
			/// indicated <see cref="NodeIndex"/>.
			/// </summary>
			/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
			/// <returns>A <c>bool</c> value indicating the applicability of this
			/// <b>Precondition</b> to the <see cref="XmlDocument"/>.</returns>
			public override bool Evaluate (NodeIndex nodeIndex)
			{
				return (!pre.Evaluate (nodeIndex));
			}

			/// <summary>
			/// Returns a <see cref="string"/> that represents the current
			/// <see cref="Object"/>.
			/// </summary>
			/// <returns>A <see cref="string"/> that represents the current
			/// <see cref="Object"/>.</returns>
			public override string ToString ()
			{
				return ("!" + pre.ToString ());
			}
		}

		/// <summary>
		/// The <b>AndPrecondition</b> class implements a binary logical AND
		/// operation on two <see cref="Precondition"/> instances.
		/// </summary>
		protected sealed class AndPrecondition : BinaryPrecondition
		{
			/// <summary>
			/// Constructs a <b>AndPrecondition</b> which will derive its
			/// value from two underlying <see cref="Precondition"/> instances.
			/// </summary>
			/// <param name="lhs">The left hand <see cref="Precondition"/>.</param>
			/// <param name="rhs">The right hand <see cref="Precondition"/>.</param>
			public AndPrecondition (Precondition lhs, Precondition rhs)
				: base (lhs, rhs)
			{ }

			/// <summary>
			/// Evaluates this <b>Precondition</b> against the contents of the
			/// indicated <see cref="NodeIndex"/>.
			/// </summary>
			/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
			/// <returns>A <c>bool</c> value indicating the applicability of this
			/// <b>Precondition</b> to the <see cref="XmlDocument"/>.</returns>
			public override bool Evaluate (NodeIndex nodeIndex)
			{
				return (lhs.Evaluate (nodeIndex) && rhs.Evaluate (nodeIndex));
			}

			/// <summary>
			/// Returns a <see cref="string"/> that represents the current
			/// <see cref="Object"/>.
			/// </summary>
			/// <returns>A <see cref="string"/> that represents the current
			/// <see cref="Object"/>.</returns>
			public override string ToString ()
			{
				return ("(" + lhs.ToString () + "&&" + rhs.ToString () + ")");
			}
		}

		/// <summary>
		/// The <b>OrPrecondition</b> class implements a binary logical OR
		/// operation on two <see cref="Precondition"/> instances.
		/// </summary>
		protected sealed class OrPrecondition : BinaryPrecondition
		{
			/// <summary>
			/// Constructs a <b>OrPrecondition</b> which will derive its
			/// value from two underlying <see cref="Precondition"/> instances.
			/// </summary>
			/// <param name="lhs">The left hand <see cref="Precondition"/>.</param>
			/// <param name="rhs">The right hand <see cref="Precondition"/>.</param>
			public OrPrecondition (Precondition lhs, Precondition rhs)
				: base (lhs, rhs)
			{ }

			/// <summary>
			/// Evaluates this <b>Precondition</b> against the contents of the
			/// indicated <see cref="NodeIndex"/>.
			/// </summary>
			/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
			/// <returns>A <c>bool</c> value indicating the applicability of this
			/// <b>Precondition</b> to the <see cref="XmlDocument"/>.</returns>
			public override bool Evaluate (NodeIndex nodeIndex)
			{
				return (lhs.Evaluate (nodeIndex) || rhs.Evaluate (nodeIndex));
			}

			/// <summary>
			/// Returns a <see cref="string"/> that represents the current
			/// <see cref="Object"/>.
			/// </summary>
			/// <returns>A <see cref="string"/> that represents the current
			/// <see cref="Object"/>.</returns>
			public override string ToString ()
			{
				return ("(" + lhs.ToString () + "||" + rhs.ToString () + ")");
			}
		}
 
		/// <summary>
		/// Constructs a <b>Precondition</b> instance.
		/// </summary>
		protected Precondition()
		{ }

		/// <summary>
		/// The <b>Always</b> class implements a <see cref="Precondition"/> that
		/// is always <c>true</c>.
		/// </summary>
		private class Always : Precondition
		{
			/// <summary>
			/// Evaluates this <b>Precondition</b> against the contents of the
			/// indicated <see cref="NodeIndex"/>.
			/// </summary>
			/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
			/// <returns>A <c>bool</c> value indicating the applicability of this
			/// <b>Precondition</b> to the <see cref="XmlDocument"/>.</returns>
			public override bool Evaluate (NodeIndex nodeIndex)
			{
				return (true);
			}

			/// <summary>
			/// Returns a <see cref="string"/> that represents the current
			/// <see cref="Object"/>.
			/// </summary>
			/// <returns>A <see cref="string"/> that represents the current
			/// <see cref="Object"/>.</returns>
			public override string ToString ()
			{
				return ("true");
			}
		}

		/// <summary>
		/// The <b>Never</b> class implements a <see cref="Precondition"/> that
		/// is always <c>false</c>.
		/// </summary>
		private class Never : Precondition
		{
			/// <summary>
			/// Evaluates this <b>Precondition</b> against the contents of the
			/// indicated <see cref="NodeIndex"/>.
			/// </summary>
			/// <param name="nodeIndex">The <see cref="NodeIndex"/> of a <see cref="XmlDocument"/>.</param>
			/// <returns>A <c>bool</c> value indicating the applicability of this
			/// <b>Precondition</b> to the <see cref="XmlDocument"/>.</returns>
			public override bool Evaluate (NodeIndex nodeIndex)
			{
				return (false);
			}

			/// <summary>
			/// Returns a <see cref="string"/> that represents the current
			/// <see cref="Object"/>.
			/// </summary>
			/// <returns>A <see cref="string"/> that represents the current
			/// <see cref="Object"/>.</returns>
			public override string ToString ()
			{
				return ("false");
			}
		}	
	}
}