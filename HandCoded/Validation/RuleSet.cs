// Copyright (C),2005-2009 HandCoded Software Ltd.
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
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

using log4net;

using HandCoded.Framework;
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
        /// Contains the name of the <c>RuleSet</c> or <c>null</c> if unnamed.
        /// </summary>
        public string Name {
            get {
                return (name);
            }
        }

        /// <summary>
		/// Contains the rule names.
		/// </summary>
		public ICollection<string> Keys {
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
		/// Constructs an unnamed empty <B>RuleSet</B>.
		/// </summary>
		public RuleSet()
            : this (null)
		{ }

        /// <summary>
        /// Constructs a named empty <B>RuleSet</B>.
        /// </summary>
        public RuleSet (string name)
        {
            if ((this.name = name) != null) extent [name] = this;
        }

        /// <summary>
        /// Returns a reference to the named <c>RuleSet</c> instance if it exists.
        /// </summary>
        /// <param name="name">The name of the required <c>RuleSet</c>.</param>
        /// <returns>The corresponding <c>RuleSet</c> instance.</returns>
        public static RuleSet ForName (string name)
        {
            lock (extent) {
                return (extent.ContainsKey (name) ? extent [name] : new RuleSet (name));
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
            if (rules.ContainsKey (name)) {
                Rule result = rules [name];

                rules.Remove (name);
                return (result);
            }
            return (null);
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
		/// A <see cref="ILog"/> instance used to report run-time problems.
		/// </summary>
		private static ILog			logger
			= LogManager.GetLogger (typeof (RuleSet));

		/// <summary>
		/// A static object used to represent a true outcome.
		/// </summary>
		private static readonly	object	trueObject	= new object ();

		/// <summary>
		/// A static object used to represent a false outcome.
		/// </summary>
		private static readonly object	falseObject	= new object ();

        /// <summary>
        /// The set of all named <c>RuleSet</c> instances.
        /// </summary>
        private static Dictionary<string, RuleSet> extent
            = new Dictionary<string, RuleSet> ();

        /// <summary>
        /// The name of the <c>RuleSet</c> or <c>null</c> if not named
	    /// (for backwards compatibility).
        /// </summary>
        private readonly string         name;

		/// <summary>
		/// The underlying collection of rules indexed by name.
		/// </summary>
		private Dictionary<string, Rule>    rules
            = new Dictionary<string, Rule> ();

        /// <summary>
        /// Parse the <b>RuleSet</b> definitions in the configuration file
        /// indicated by the URI.
        /// </summary>
        /// <param name="uri">The URI of the business rule configuration file.</param>
        private static void ParseRuleSets (string uri)
        {
            RuleSet         ruleSet     = null;
			XmlReader		reader		= XmlReader.Create (uri);

			while (reader.Read ()) {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					{
                        if (reader.LocalName.Equals ("forceLoad")) {
                            string platform = reader ["platform"];
                            string implementation = reader ["class"];

                            if (platform.Equals (".Net")) {
                                try {
									Type		type = Application.GetType (implementation);

									if (type != null) {
										// Access each member to ensure it has a chance to initialise
										MemberInfo [] members = type.GetMembers ();
										if (members != null) {
											foreach (MemberInfo member in members) {
												if (member.MemberType == MemberTypes.Field) {
													type.InvokeMember (member.Name, BindingFlags.GetField, null, null, null);
												}
											}
										}
										break;
									}
									else
										logger.Error ("Could not find load rule class '" + implementation + "'");
                                }
                                catch (Exception error) {
                                    logger.Error ("Could not force load rule class '" + implementation + "'", error);
                                }
                            }
                        } else	if (reader.LocalName.Equals ("ruleSet")) {
							string		name	= reader ["name"];

                            ruleSet = new RuleSet (name);
                        }
						else if (reader.LocalName.Equals ("addRule")) {
							string		name	= reader ["name"];
                            Rule        rule    = Rule.ForName (name);

                            if (rule != null) {
                                if (ruleSet != null)
                                    ruleSet.Add (rule);
                                else
                                    logger.Error ("Syntax error in rule file - addRule outside of RuleSet");
                            }
                            else
                                logger.Error ("Reference to undefined rule '" + name + "' in addRule");
                        }
                        else if (reader.LocalName.Equals ("removeRule")) {
                            string name = reader ["name"];
                            Rule rule = Rule.ForName (name);

                            if (rule != null) {
                                if (ruleSet != null)
                                    ruleSet.Remove (rule);
                                else
                                    logger.Error ("Syntax error in rule file - removeRule outside of RuleSet");
                            }
                            else
                                logger.Error ("Reference to undefined rule '" + name + "' in addRule");
                        }
                        else if (reader.LocalName.Equals ("addRuleSet")) {
                            string name = reader ["name"];
                            RuleSet rules = ForName (name);

                            if (rules != null) {
                                if (ruleSet != null) {
                                    if (rules != ruleSet)
                                        ruleSet.Add (rules);
                                    else
                                        logger.Error ("Attempt to recursively define ruleset '" + name + "'");
                                }
                                else
                                    logger.Error ("Syntax error in rule file - addRuleSet outside of RuleSet");
                            }
                            else
                                logger.Error ("Reference to undefined rule '" + name + "' in addRule");
                        }
						break;
					}
                case XmlNodeType.EndElement:
                    {
                        if (reader.LocalName.Equals ("ruleSet"))
                            ruleSet = null;
     
                        break;
                    }
				}
			}
			reader.Close ();
        }

        /// <summary>
        /// Causes the <b>RuleSet</b> class to try and bootstrap the business
	    /// rules from a configuration file.
        /// </summary>
        static RuleSet ()
        {
            logger.Debug ("Bootstrapping");

			try {
				ParseRuleSets (
					Path.Combine (AppDomain.CurrentDomain.BaseDirectory,
						ConfigurationManager.AppSettings ["HandCoded.FpML Toolkit.BusinessRules"]));
			}
			catch (Exception error) {
				logger.Error ("Unable to load standard rule set definitions", error);
			}

			logger.Debug ("Completed");
        }
	}
}