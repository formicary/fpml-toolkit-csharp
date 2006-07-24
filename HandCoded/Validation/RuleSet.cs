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
using System.Xml;

using HandCoded.Xml;

namespace HandCoded.Validation
{
	/// <summary>
	/// A <B>RuleSet</B> instance contains a collection of validation rules
	/// that can be tested against a  <see cref="XmlDocument"/> in a single
	/// operation. 
	/// </summary>
	public class RuleSet : Validator
	{
		/// <summary>
		/// Constructs an empty <B>RuleSet</B>.
		/// </summary>
		public RuleSet()
		{ }

		/// <summary>
		/// Contains the rule names.
		/// </summary>
		public ICollection Keys {
			get {
				return (rules.Keys);
			}
		}

		/// <summary>
		/// Contains the current number of rules in the <B>RuleSet</B>
		/// </summary>
		public int Size {
			get {
				return (rules.Count);
			}
		}

		/// <summary>
		/// Adds the indicated <see cref="Rule"/> instance to the <B>RuleSet</B>
		/// A <see cref="Rule"/> may be referenced by several <B>RuleSet</B>
		/// instances simultaneously.
		/// <para>If the <see cref="Rule"/> has the same name as a previously
		/// added one then it will replace it. This feature can be used to
		/// overwrite standard rules with customized ones.</para>
		/// </summary>
		/// <param name="rule">The <see cref="Rule"/> to be added.</param>
		public void Add (Rule rule)
		{
			rules [rule.Name] = rule;
		}

		/// <summary>
		/// Adds the <see cref="Rule"/> instances that comprise another
		/// <B>RuleSet</B> to this one.
		/// </summary>
		/// <param name="ruleSet">The <B>RuleSet</B> to be added.</param>
		public void Add (RuleSet ruleSet)
		{
			foreach (Rule rule in ruleSet.rules.Values)
				rules.Add (rule.Name, rule);
		}

		/// <summary>
		/// Attempts to remove a <see cref="Rule"/> with the given name from
		/// the collection held by the <B>RuleSet</B>.
		/// </summary>
		/// <param name="name">The name of the <see cref="Rule"/> to be removed.</param>
		/// <returns> The <see cref="Rule"/> instance removed from the set or
		/// <c>null</c> if there was no match.</returns>
		public Rule Remove (string name)
		{
			Rule		result = rules [name] as Rule;

			rules.Remove (name);
			return (result);
		}

		/// <summary>
		/// Attempts to remove a given <see cref="Rule"/> from the collection
		/// held by the <B>RuleSet</B>.
		/// </summary>
		/// <param name="rule">The <see cref="Rule"/> to be removed.</param>
		/// <returns> The <see cref="Rule"/> instance removed from the set or
		/// <c>null</c> if there was no match.</returns>
		public Rule Remove (Rule rule)
		{
			return (Remove (rule.Name));
		}

		/// <summary>
		/// Determines if the <see cref="XmlDocument"/> instance indexed by the
		/// provided <see cref="NodeIndex"/> has business data content that passes a
		/// validation test. If errors are detected they will be reported through
		/// the <see cref="ValidationErrorHandler"/> delegate passed as an argument.
		/// </summary>
		/// <remarks>Note that test returns <c>true</c> if it does not fail,
		/// including circumstances when the test is inapplicable to the
		/// <see cref="XmlDocument"/> under examination.
		/// </remarks>
		/// <param name="nodeIndex">The <see cref="NodeIndex"/> instance to examine.</param>
		/// <param name="errorHandler">An <see cref="ValidationErrorHandler"/> delegate used to report
		/// validation failures.</param>
		/// <returns><c>false</c> if the validation test failed, <c>true</c> otherwise.
		/// </returns>
		protected override bool Validate (NodeIndex nodeIndex, ValidationErrorHandler errorHandler)
		{
			bool		result = true;
			Hashtable	cache  = new Hashtable ();

			foreach (Rule rule in rules.Values) {
				Precondition		condition = rule.Precondition;
				bool				applies;

				// Determine if the precondition has be evaluated before 
				if (!cache.ContainsKey (condition)) {
					applies = condition.Evaluate (nodeIndex);
					cache [condition] = applies ? trueObject : falseObject;
				}
				else
					applies = (cache [condition] == trueObject);

				if (applies) result &= rule.PerformValidation (nodeIndex, errorHandler);
			}
			return (result);
		}

		/// <summary>
		/// A static object used to represent a true outcome.
		/// </summary>
		private static readonly	object	trueObject	= new object ();

		/// <summary>
		/// A static object used to represent a false outcome.
		/// </summary>
		private static readonly object	falseObject	= new object ();

		/// <summary>
		/// The underlying collection of rules indexed by name.
		/// </summary>
		private Hashtable			rules = new Hashtable ();
	}
}