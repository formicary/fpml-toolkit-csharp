// Copyright (C),2005-2008 HandCoded Software Ltd.
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
using System.Collections.Generic;
using System.Xml;

using HandCoded.Meta;

namespace HandCoded.FpML
{
	/// <summary>
	/// Summary description for Conversions.
	/// </summary>
	public class Conversions
	{
		/// <summary>
		/// The <b>R1_0__R2_0</b> class implements a conversion from FpML 1.0
		/// to FpML 2.0. The specific changes needed (other than basic DOCTYPE
		/// changes) are:
		/// <UL>
		/// <LI>The &lt;product&gt; container element was removed.</LI>
		/// <LI>Superfluous <c>type</c> and <c>base</c> attributes are removed.</LI>
		/// </UL> 
		/// </summary>
		public sealed class R1_0__R2_0 : HandCoded.Meta.DirectConversion
		{
			/// <summary>
			/// Constructs a <b>CR1_0__R2_0</b> instance.
			/// </summary>
			public R1_0__R2_0 ()
				: base (Releases.R1_0, Releases.R2_0)
			{ }

			/// <summary>
			/// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
			/// to create a new <see cref="XmlDocument"/>.
			/// </summary>
			/// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
			/// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
			/// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
			public override XmlDocument Convert (XmlDocument source, HandCoded.Meta.IHelper helper)
			{
				XmlDocument		target 	= TargetRelease.NewInstance ("FpML");
				XmlElement		oldRoot = source.DocumentElement;
				XmlElement		newRoot	= target.DocumentElement;
				XmlDocumentType doctype = target.DocumentType;
			
				// Temporarily remove the <!DOCTYPE> from the new document
				target.RemoveChild (doctype);

				// Transfer the scheme default attributes
				foreach (XmlAttribute attr in oldRoot.Attributes) {
					if (attr.Name.EndsWith ("SchemeDefault")) {
						string name  = attr.Name;
						string value = attr.Value;
						
						if (Releases.R1_0.SchemeDefaults.GetDefaultUriForAttribute (name).Equals (value))
							value = Releases.R2_0.SchemeDefaults.GetDefaultUriForAttribute (name);
						
						if (value != null) newRoot.SetAttribute (name, null, value);
					}
				}

				// Transcribe each of the first level child elements
				foreach (XmlNode node in oldRoot.ChildNodes)
					Transcribe (node, target, newRoot);

				// Replace the <!DOCTYPE>
				target.InsertBefore (doctype, target.DocumentElement);

				return (target);
			}
			
			/// <summary>
			/// Recursively copies the structure of the old FpML 1-0 document
			/// into a new FpML 2-0 document adjusting the elements and attributes
			/// as necessary.
			/// </summary>
			/// <param name="context">The <see cref="XmlNode"/> to be copied.</param>
			/// <param name="document">The new <see cref="XmlDocument"/> instance.</param>
			/// <param name="parent">The new parent <see cref="XmlNode"/>.</param>
			private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent)
			{
				switch (context.NodeType) {
				case XmlNodeType.Element:
					{
						XmlElement		element = context as XmlElement;
	
						if (context.LocalName.Equals ("product")) {
							// Replace this element with a copy of its children
							foreach (XmlNode node in element.ChildNodes)
								Transcribe (node, document, parent);
						}
						else {
							XmlElement 		clone = document.CreateElement (element.LocalName);
							
							parent.AppendChild (clone);

							// Transfer and update attributes
							foreach (XmlAttribute attr in element.Attributes) {
								string name = attr.Name;
								
								if (!(name.Equals ("type") || name.Equals ("base"))) {
									string value  = attr.Value;
									
									if (name.EndsWith ("Scheme")) {
										string oldDefault = Releases.R1_0.SchemeDefaults.GetDefaultAttributeForScheme (name);
										string newDefault = Releases.R2_0.SchemeDefaults.GetDefaultAttributeForScheme (name);
										
										if (oldDefault != null && newDefault != null) {
											string 	defaultUri = Releases.R1_0.SchemeDefaults.GetDefaultUriForAttribute (oldDefault); 
											if ((defaultUri != null) && defaultUri.Equals (value))
												value = Releases.R2_0.SchemeDefaults.GetDefaultUriForAttribute (newDefault);
										}
									}
									
									if (value != null) clone.SetAttribute (name, value);
								}
							}
							
							// Recursively copy the child node across
							foreach (XmlNode node in element.ChildNodes)
								Transcribe (node, document, clone);
						}
						break;
					}
					
				default:
					parent.AppendChild (document.ImportNode (context, false));
					break;
				}
			}
		}

		/// <summary>
		/// The <b>R2_0__TR3_0</b> class implements a conversion from FpML 2-0
		/// to FpML 3-0. The specific changes need (other than basic DOCTYPE
		/// substitutions are:
		/// <ul>
		/// <li><c>href</c> attributes must be converted to <c>IDREF</c> values
		/// rather than XLink expressions.</li>
		/// <li>Superfluous <b>type</b> and <b>base</b> attributes are removed.</li>
		/// </ul>
		/// </summary>
		public class R2_0__R3_0 : HandCoded.Meta.DirectConversion
		{
			/// <summary>
			/// Constructs a <b>R2_0__TR3_0</b> instance.
			/// </summary>
			public R2_0__R3_0 ()
				: base (Releases.R2_0, Releases.R3_0)
			{ }

			/// <summary>
			/// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
			/// to create a new <see cref="XmlDocument"/>.
			/// </summary>
			/// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
			/// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
			/// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
			public override XmlDocument Convert(XmlDocument source, HandCoded.Meta.IHelper helper)
			{
				XmlDocument		target 	= TargetRelease.NewInstance ("FpML");
				XmlElement		oldRoot = source.DocumentElement;
				XmlElement		newRoot	= target.DocumentElement;
				XmlDocumentType doctype = target.DocumentType;
			
				// Temporarily remove the <!DOCTYPE> from the new document
				target.RemoveChild (doctype);
				
				// Transfer the scheme default attributes
				foreach (XmlAttribute attr in oldRoot.Attributes) {
					if (attr.Name.EndsWith ("SchemeDefault")) {
						string name  = attr.Name;
						string value = attr.Value;
						
						if (Releases.R2_0.SchemeDefaults.GetDefaultUriForAttribute (name).Equals (value))
							value = Releases.R3_0.SchemeDefaults.GetDefaultUriForAttribute (name);
						
						if (value != null) newRoot.SetAttribute (name, null, value);
					}
				}
				
				// Transcribe each of the first level child elements
				List<XmlElement>	parties = new List<XmlElement> ();
				foreach (XmlNode node in oldRoot.ChildNodes)
					Transcribe (node, target, newRoot, parties);

				// Then append the saved party elements
				foreach (XmlElement party in parties)
					Transcribe (party, target, newRoot, null);

				// Replace the <!DOCTYPE>
				target.InsertBefore (doctype, target.DocumentElement);

				return (target);
			}

			private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent, List<XmlElement> parties)
			{
				switch (context.NodeType) {
				case XmlNodeType.Element:
					{
						XmlElement		element = context as XmlElement;
	
						// If this is the first pass thru the tree then save
						// party elements instead of processing them.
						if ((parties != null) && (element.Name.Equals ("party"))) {
							parties.Add (element);
							return;
						}
	
						XmlElement	clone = document.CreateElement (element.LocalName);
						parent.AppendChild (clone);

						foreach (XmlAttribute attr in element.Attributes) {
							string		name = attr.Name;

							if (!(name.Equals ("type") || name.Equals ("base"))) {
								string		value = attr.Value;

								if (name.Equals ("href")) {
									if (value.StartsWith ("#"))
										value = value.Substring (1);
								}
								else if (name.EndsWith ("Scheme")) {
									string oldDefault = Releases.R2_0.SchemeDefaults.GetDefaultAttributeForScheme (name);
									string newDefault = Releases.R3_0.SchemeDefaults.GetDefaultAttributeForScheme (name);
									
									if (oldDefault != null && newDefault != null) {
										string 	defaultUri = Releases.R2_0.SchemeDefaults.GetDefaultUriForAttribute (oldDefault); 
										if ((defaultUri != null) && defaultUri.Equals (value))
											value = Releases.R3_0.SchemeDefaults.GetDefaultUriForAttribute (newDefault);
									}
								}
	
								if (value != null) clone.SetAttribute (name, value);
							}
						}

						// Recursively copy the child node across
						foreach (XmlNode node in element.ChildNodes)
							Transcribe (node, document, clone, parties);

						break;
					}
					
				default:
					parent.AppendChild (document.ImportNode (context, false));
					break;
				}
			}
		}

		/// <summary>
		/// The <b>R3_0__R4_0</b> class implements a conversion from FpML 3-0
		/// to FpML 4-0. The specific changes need (other than basic DOCTYPE
		/// substitutions are:
		/// <ul>
		/// <li>The document is becomes XML schema referencing.</li>
		/// <li>Legacy documents become FpML DataDocument instances.</li>
		/// <li>The <b>dateRelativeTo</b> referencing mechanism is changed.</li>
		/// <li>The value set for &lt;fraDiscounting&gt; was modified.</li>
		/// <li>The element &lt;calculationAgentPartyReference&gt; was moved from
		/// the trade header into the trade structure.</li>
		/// <li>The &lt;informationSource&gt; element is renamed &lt;primaryRateSource&gt;
		/// within &lt;fxSpotRateSource&gt; elements.</li>
		/// <li>The structure of the <b>equityOption</b> element is changed.</li>
		/// <li>SchemeDefaults are removed and non-defaulted schemes appear
		/// on referencing elements,</li>
		/// </ul>
		/// </summary>
		public class R3_0__R4_0 : HandCoded.Meta.DirectConversion
		{
			/// <summary>
			/// Constructs a <b>TR3_0__R4_0</b> instance.
			/// </summary>
			public R3_0__R4_0 ()
				: base (Releases.R3_0, Releases.R4_0)
			{ }

			/// <summary>
			/// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
			/// to create a new <see cref="XmlDocument"/>.
			/// </summary>
			/// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
			/// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
			/// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
			public override XmlDocument Convert (XmlDocument source, HandCoded.Meta.IHelper helper)
			{
				XmlDocument		target 	= TargetRelease.NewInstance ("FpML");
				XmlElement		oldRoot = source.DocumentElement;
				XmlElement		newRoot	= target.DocumentElement;
				Dictionary<string, XmlElement>	cache = new Dictionary<string, XmlElement> ();

				newRoot.SetAttribute ("xsi:type", "DataDocument");

				// Recursively copy the child node across
				foreach (XmlNode node in oldRoot.ChildNodes)
					Transcribe (node, target, newRoot, cache, true);

				cache.Clear ();
				return (target);
			}

			private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent,
                Dictionary<string, XmlElement> cache, bool caching)
			{
				switch (context.NodeType) {
				case XmlNodeType.Element:
					{
						XmlElement		element = context as XmlElement;
						XmlElement		clone;

						// Cache the <calculationAgentPartyReference> element
						if (caching &&
							element.Name.Equals ("calculationAgentPartyReference") &&
							element.ParentNode.Name.Equals ("tradeHeader")) {
							cache ["calculationAgentPartyReference"] = element;
							return;
						}

						// Handle the restructuring of the equityOption element components
						if (element.LocalName.Equals ("buyerParty")) {
							clone = document.CreateElement ("buyerPartyReference");
							
							clone.SetAttribute ("href", element ["partyReference"].GetAttribute ("href"));
							parent.AppendChild (clone);
							break;
						}
						if (element.LocalName.Equals ("sellerParty")) {
							clone = document.CreateElement ("sellerPartyReference");
								
							clone.SetAttribute ("href", element ["partyReference"].GetAttribute ("href"));
							parent.AppendChild (clone);
							break;
						}
						if (element.LocalName.Equals ("underlying")) {
							clone = document.CreateElement ("underlyer");
							XmlElement singleUnderlyer = document.CreateElement ("singleUnderlyer");
							XmlElement underlyingAsset;

							if (element.GetElementsByTagName ("extraordinaryEvents").Count == 0)
								underlyingAsset = document.CreateElement ("index");
							else
								underlyingAsset = document.CreateElement ("equity");
			
							foreach (XmlElement instrumentId in element.GetElementsByTagName ("instrumentId"))
								Transcribe (instrumentId, document, underlyingAsset, cache, caching);

							XmlElement description = document.CreateElement ("description");
							description.InnerText = element ["description"].InnerText;
							underlyingAsset.AppendChild (description);

							XmlElement optional;

							if ((optional = element ["currency"]) != null)
								Transcribe (optional, document, underlyingAsset, cache, caching);

							if ((optional = element ["exchangeId"]) != null)
								Transcribe (optional, document, underlyingAsset, cache, caching);

							if ((optional = element ["clearanceSystem"]) != null)
								Transcribe (optional, document, underlyingAsset, cache, caching);

							singleUnderlyer.AppendChild (underlyingAsset);
							clone.AppendChild (singleUnderlyer);
							parent.AppendChild (clone);
							break;
						}
						if (element.LocalName.Equals ("settlementDate")) {
							clone = document.CreateElement ("settlementDate");
							XmlElement relativeDate = document.CreateElement ("relativeDate");

							foreach (XmlNode child in element.ChildNodes)
								Transcribe (child, document, relativeDate, cache, caching);

							clone.AppendChild (relativeDate);
							parent.AppendChild (clone);
							break;
						}
						
						// Handle restructuring of FX components
						if (element.LocalName.Equals ("fixing")) {
							clone = document.CreateElement ("fixing");

							XmlElement target;

							if ((target = element ["primaryRateSource"]) != null) 
								Transcribe (target, document, clone, cache, caching);
							if ((target = element ["secondaryRateSource"]) != null)
								Transcribe (target, document, clone, cache, caching);
							if ((target = element ["fixingTime"]) != null)
								Transcribe (target, document, clone, cache, caching);
							if ((target = element ["quotedCurrencyPair"]) != null)
								Transcribe (target, document, clone, cache, caching);
							if ((target = element ["fixingDate"]) != null)
								Transcribe (target, document, clone, cache, caching);

							parent.AppendChild (clone);
							break;
						}

						// Handle elements that get renamed
						if (element.LocalName.Equals ("informationSource") &&
							element.ParentNode.LocalName.Equals ("fxSpotRateSource"))
							clone = document.CreateElement ("primaryRateSource");
						else
							clone = document.CreateElement (element.LocalName);

						// Generate the <calculationAgentPartyReference> before any peer element
						if (element.LocalName.Equals ("calculationAgentBusinessCenter") ||
							element.LocalName.Equals ("governingLaw") ||
							element.LocalName.Equals ("documentation")) {
							XmlElement agent = cache ["calculationAgentPartyReference"] as XmlElement;

							if (agent != null) {
								XmlElement container = document.CreateElement ("calculationAgent");

								clone.AppendChild (container);
								Transcribe (agent, document, parent, cache, false);
								cache.Remove ("calculationAgentPartyReference");
							}
						}

						parent.AppendChild (clone);

						// Change the data value of fraDiscounting
						if (element.LocalName.Equals ("fraDiscounting")) {
							if (element.InnerText.Trim ().Equals ("true"))
								clone.InnerText = "ISDA";
							else
								clone.InnerText = "NONE";
							
							break;
						}

						// Handle scheme values which changed capitalisation
						if (element.LocalName.Equals ("quoteBasis")) {
							string	value	= element.InnerText.Trim ().ToUpper ();

							if (value.Equals ("CURRENCY1PERCURRENCY2"))
								clone.InnerText = "Currency1PerCurrency2";
							else if (value.Equals ("CURRENCY2PERCURRENCY1"))
								clone.InnerText = "Currency2PerCurrency1";
							else
								clone.InnerText = element.InnerText.Trim ();
							
							break;
						}
						if (element.LocalName.Equals ("sideRateBasis")) {
							string	value	= element.InnerText.Trim ().ToUpper ();

							if (value.Equals ("CURRENCY1PERBASECURRENCY"))
								clone.InnerText = "Currency1PerBaseCurrency";
							else if (value.Equals ("BASECURRENCYPERCURRENCY1"))
								clone.InnerText = "BaseCurrencyPerCurrency1";
							else if (value.Equals ("CURRENCY2PERBASECURRENCY"))
								clone.InnerText = "Currency2PerBaseCurrency";
							else if (value.Equals ("BASECURRENCYPERCURRENCY2"))
								clone.InnerText = "BaseCurrencyPerCurrency2";
							else
								clone.InnerText = element.InnerText.Trim ();
							
							break;
						}
						if (element.LocalName.Equals ("premiumQuoteBasis")) {
							string	value	= element.InnerText.Trim ().ToUpper ();

							if (value.Equals ("PERCENTAGEOFCALLCURRENCYAMOUNT"))
								clone.InnerText = "PercentageOfCallCurrencyAmount";
							else if (value.Equals ("PERCENTAGEOFPUTCURRENCYAMOUNT"))
								clone.InnerText = "PercentageOfPutCurrencyAmount";
							else if (value.Equals ("CALLCURRENCYPERPUTCURRENCY"))
								clone.InnerText = "CallCurrencyPerPutCurrency";
							else if (value.Equals ("PUTCURRENCYPERCALLCURRENCY"))
								clone.InnerText = "PutCurrencyPerCallCurrency";
							else if (value.Equals ("EXPLICIT"))
								clone.InnerText = "Explicit";
							else
								clone.InnerText = element.InnerText.Trim ();

							break;
						}
						if (element.LocalName.Equals ("strikeQuoteBasis") ||
							element.LocalName.Equals ("averageRateQuoteBasis")) {
							string value	= element.InnerText.Trim ().ToUpper ();

							if (value.Equals ("CALLCURRENCYPERPUTCURRENCY"))
								clone.InnerText = "CallCurrencyPerPutCurrency";
							else if (value.Equals ("PUTCURRENCYPERCALLCURRENCY"))
								clone.InnerText = "PutCurrencyPerCallCurrency";
							else
								clone.InnerText = element.InnerText.Trim ();
							
							break;
						}
						if (element.LocalName.Equals ("fxBarrierType")) {
							string	value	= element.InnerText.Trim ().ToUpper ();

							if (value.Equals ("KNOCKIN"))
								clone.InnerText = "Knockin";
							else if (value.Equals ("KNOCKOUT"))
								clone.InnerText = "Knockout";
							else if (value.Equals ("REVERSEKNOCKIN"))
								clone.InnerText = "ReverseKnockin";
							else if (value.Equals ("REVERSEKNOCKOUT"))
								clone.InnerText = "ReverseKnockout";
							else
								clone.InnerText = element.InnerText.Trim ();

							break;
						}

						// Handle elements which changed from schemes to enumerations
						if (element.LocalName.Equals ("optionType")) {
							clone.InnerText = element.InnerText;
							break;
						}
						if (element.LocalName.Equals ("nationalisationOrInsolvency")) {
							clone.InnerText = element.InnerText;
							break;
						}
						if (element.LocalName.Equals ("delisting")) {
							clone.InnerText = element.InnerText;
							break;
						}

						foreach (XmlAttribute attr in element.Attributes) {
							string		name = attr.Name;

							if (!(name.Equals ("type") || name.Equals ("base")))
								clone.SetAttribute (name, attr.Value);
						}

						// Ignore text string under dateRelativeTo
						if (element.LocalName.Equals ("dateRelativeTo")) break;

						// Fix up id and href on cash settlement dateRelativeTo definitions
						if (element.LocalName.Equals ("cashSettlementPaymentDate") &&
							element.ParentNode.ParentNode.LocalName.Equals ("optionalEarlyTermination")) {
							string		id;

							for (int count = 1;;) {
								id = "AutoRef" + (count++);
								if (document.GetElementById (id) == null) break;
							}

							clone.SetAttribute ("id", id);
							
							parent ["cashSettlementValuationDate"]["dateRelativeTo"].SetAttribute ("href", id);
						}
						
						// Recursively copy the child node across
						foreach (XmlNode node in element.ChildNodes)
							Transcribe (node, document, clone, cache, caching);

						// Generate <calculationAgentPartyReference> at the end of trade
						// if no peer element.
						if (element.Name.Equals ("trade")) {
							XmlElement agent = cache ["calculationAgentPartyReference"] as XmlElement;

							if (agent != null) {
								XmlElement container = document.CreateElement ("calculationAgent");

								clone.AppendChild (container);
								Transcribe (agent, document, container, cache, false);
								cache.Remove ("calculationAgentPartyReference");
							}
						}
						break;
					}
			
				default:
					parent.AppendChild (document.ImportNode (context, false));
					break;
				}
			}
		}

		/// <summary>
		/// The <b>CR4_0__R4_1</b> class implements a conversion from FpML 4-0
		/// to FpML 4-1. The specific changes need (other than basic DOCTYPE
		/// substitutions are:
		/// <ul>
		/// </ul>
		/// </summary>
		public class R4_0__R4_1 : HandCoded.Meta.DirectConversion
		{
			/// <summary>
			/// Extended <see cref="HandCoded.Meta.IHelper"/> interface used to
			/// obtain additional information.
			/// </summary>
			public interface IHelper : HandCoded.Meta.IHelper
			{
				/// <summary>
				/// Uses the context information provided to determine the reference
				/// currency of the trade or throws a <see cref="ConversionException"/>.
				/// </summary>
				/// <param name="context">The <see cref="XmlElement"/> of the fxFeature</param>
				/// <returns>The reference currency code value (e.g. GBP).</returns>
				string GetReferenceCurrency (XmlElement context);

				/// <summary>
				/// Uses the context information provided to determine the first quanto
				/// currency of the trade or throws a <see cref="ConversionException"/>.
				/// </summary>
				/// <param name="context">The <see cref="XmlElement"/> of the fxFeature</param>
				/// <returns>The reference currency code value (e.g. GBP).</returns>
				string GetQuantoCurrency1 (XmlElement context);

				/// <summary>
				/// Uses the context information provided to determine the second quanto
				/// currency of the trade or throws a <see cref="ConversionException"/>.
				/// </summary>
				/// <param name="context">The <see cref="XmlElement"/> of the fxFeature</param>
				/// <returns>The reference currency code value (e.g. GBP).</returns>
				string GetQuantoCurrency2 (XmlElement context);
			}

			/// <summary>
			/// Constructs a <b>R4_0__R4_1</b> instance.
			/// </summary>
			public R4_0__R4_1 ()
				: base (Releases.R4_0, Releases.R4_1)
			{ }

			/// <summary>
			/// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
			/// to create a new <see cref="XmlDocument"/>.
			/// </summary>
			/// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
			/// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
			/// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
			public override XmlDocument Convert (XmlDocument source, HandCoded.Meta.IHelper helper)
			{
				XmlDocument		target 	= TargetRelease.NewInstance ("FpML");
				XmlElement		oldRoot = source.DocumentElement;
				XmlElement		newRoot	= target.DocumentElement;

				// Transfer the attributes
				newRoot.SetAttribute ("xsi:type", oldRoot.GetAttribute ("xsi:type"));

				// Recursively copy the child node across
				foreach (XmlNode node in oldRoot.ChildNodes)
					Transcribe (node, target, newRoot, helper);

				return (target);
			}

			private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent, HandCoded.Meta.IHelper helper)
			{
				switch (context.NodeType) {
				case XmlNodeType.Element:
					{
						XmlElement		element = context as XmlElement;
						XmlElement		clone;

						// Ignore failureToDeliverApplication
						if (element.LocalName.Equals ("failureToDeliverApplicable"))
							break;
					
						// Handle elements that get renamed.
						if (element.LocalName.Equals ("equityOptionFeatures"))
							clone = document.CreateElement ("equityFeatures");
						else if (element.LocalName.Equals ("automaticExerciseApplicable"))
							clone = document.CreateElement ("automaticExercise");
						else if (element.LocalName.Equals ("equityBermudanExercise"))
							clone = document.CreateElement ("equityBermudaExercise");
						else if (element.LocalName.Equals ("bermudanExerciseDates"))
							clone = document.CreateElement ("bermudaExerciseDates");
						else if (element.LocalName.Equals ("fxSource") ||
								 element.LocalName.Equals ("fxDetermination"))
							clone = document.CreateElement ("fxSpotRateSource");
						else if (element.LocalName.Equals ("futuresPriceValuationApplicable"))
							clone = document.CreateElement ("futuresPriceValuation");
						else if (element.LocalName.Equals ("equityValuationDate"))
							clone = document.CreateElement ("valuationDate");
						else if (element.LocalName.Equals ("equityValuationDates"))
							clone = document.CreateElement ("valuationDates");
						else if (element.LocalName.Equals ("fxTerms"))
							clone = document.CreateElement ("fxFeature");
						else
							clone = document.CreateElement (element.LocalName);

						parent.AppendChild (clone);

						// Handle renaming for clearanceSystemIdScheme attribute
						if (element.LocalName.Equals ("clearanceSystem")) {
							clone.SetAttribute ("clearanceSystemScheme", element.GetAttribute ("clearanceSystemIdScheme"));
							clone.InnerText = element.InnerText;
							break;
						}

						// Handle renaming for routingIdScheme attribute
						if (element.LocalName.Equals ("routingId")) {
							clone.SetAttribute ("routingIdCodeScheme", element.GetAttribute ("routingIdScheme"));
							clone.InnerText = element.InnerText;
							break;
						}

						foreach (XmlAttribute attr in element.Attributes)
							clone.SetAttribute (attr.Name, attr.Value);
						
						// Handle the restructuring of equityOption
						if (element.LocalName.Equals ("equityOption")) {
							XmlElement target;

							XmlElement premium	= document.CreateElement ("equityPremium");
							XmlElement payer	= document.CreateElement ("payerPartyReference");
							XmlElement receiver	= document.CreateElement ("receiverPartyReference");

							if ((target = element ["buyerPartyReference"]) != null) {
								Transcribe (target, document, clone, helper);
								payer.SetAttribute ("href", target.GetAttribute ("href"));
							}
							if ((target = element ["sellerPartyReference"]) != null) {
								Transcribe (target, document, clone, helper);
								receiver.SetAttribute ("href", target.GetAttribute ("href"));
							}
							if ((target = element ["optionType"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["equityEffectiveDate"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["underlyer"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["notional"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["equityExercise"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["fxFeature"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["methodOfAdjustment"]) != null)
								Transcribe (target, document, clone, helper);

							if ((target = element ["extraordinaryEvents"]) != null)
								Transcribe (target, document, clone, helper);
							else {
								XmlElement	child = document.CreateElement ("extraordinaryEvents");
								XmlElement	failure = document.CreateElement ("failureToDeliver");
							
								if ((target = element ["equityExercise"]["failureToDeliverApplicable"]) != null)
									failure.InnerText = target.InnerText;
								else
									failure.InnerText = "false";

								child.AppendChild (failure);
								clone.AppendChild (child);
							}
							
							if ((target = element ["equityOptionFeatures"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["strike"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["spot"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["numberOfOptions"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["optionEntitlement"]) != null)
								Transcribe (target, document, clone, helper);

							premium.AppendChild (payer);
							premium.AppendChild (receiver);			
							clone.AppendChild (premium);

							break;
						}

						// Handle restructuring of swaption
						if (element.LocalName.Equals ("swaption")) {
							XmlElement target;
							XmlElement agent = document.CreateElement ("calculationAgent");

							if ((target = element ["buyerPartyReference"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["sellerPartyReference"]) != null)
								Transcribe (target, document, clone, helper);

							foreach (XmlElement child in element.GetElementsByTagName ("premium"))
								Transcribe (child, document, clone, helper);

							if ((target = element ["americanExercise"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["bermudaExercise"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["europeanExercise"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["exerciseProcedure"]) != null)
								Transcribe (target, document, clone, helper);
		
							clone.AppendChild (agent);

							foreach (XmlElement child in element.GetElementsByTagName ("calculationAgentPartyReference"))
								Transcribe (child, document, agent, helper);

							if ((target = element ["cashSettlement"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["swaptionStraddle"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["swaptionAdjustedDates"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["swap"]) != null)
								Transcribe (target, document, clone, helper);

							break;
						}

						// Handle restructuring of fxFeature
						if (element.LocalName.Equals ("fxFeature")) {
							XmlElement	child;
							XmlElement	target;
							XmlElement	rccy	= document.CreateElement ("referenceCurrency");

							if (helper is IHelper) {
								rccy.InnerText = (helper as IHelper).GetReferenceCurrency (element);
								clone.AppendChild (rccy);
							}
							else
								throw new ConversionException ("Cannot determine the fxFeature reference currency");

							if (element ["fxFeatureType"].InnerText.Trim ().ToUpper ().Equals ("COMPOSITE")) {
								child = document.CreateElement ("composite");
	
								if ((target = element ["fxSource"]) != null)
									Transcribe (target, document, child, helper);
							}
							else {
								child = document.CreateElement ("quanto");

								XmlComment	note	= document.CreateComment ("Note: Manual enrichment required here");

								XmlElement	pair	= document.CreateElement ("quotedCurrencyPair");
								XmlElement	ccy1	= document.CreateElement ("currency1");
								XmlElement	ccy2	= document.CreateElement ("currency2");
								XmlElement	basis	= document.CreateElement ("quoteBasis");
								XmlElement	rate	= document.CreateElement ("fxRate");
								XmlElement	value	= document.CreateElement ("rate");

								if (helper is IHelper) {
									ccy1.InnerText = (helper as IHelper).GetQuantoCurrency1 (element);
									ccy2.InnerText = (helper as IHelper).GetQuantoCurrency2 (element);
								}
								else
									throw new ConversionException ("Cannot determine fxFeature quanto currencies");

								basis.InnerText = "Currency1PerCurrency2";

								pair.AppendChild (ccy1);
								pair.AppendChild (ccy2);
								pair.AppendChild (basis);

								if ((target = element ["fxRate"]) != null)
									value.InnerText = target.InnerText;
								else
									value.InnerText = "0.0000";

								rate.AppendChild (pair);
								rate.AppendChild (value);

								child.AppendChild (note);
								child.AppendChild (rate);

								if ((target = element ["fxSource"]) != null)
									Transcribe (target, document, child, helper);
							}
							clone.AppendChild (child);
					
							break;
						}

						// Handle restucturing of fxTerms
						if (element.LocalName.Equals ("fxTerms")) {
							XmlElement	kind;
							XmlElement	child;

							if ((kind = element ["quanto"]) != null) {
								Transcribe (kind ["referenceCurrency"], document, clone, helper);

								child = document.CreateElement ("quanto");

								foreach (XmlElement target in kind.GetElementsByTagName ("fxRate"))
									Transcribe (target, document, child, helper);

								clone.AppendChild (child);
							}
							if ((kind = element ["compositeFx"]) != null) {
								XmlElement	target;

								Transcribe (kind ["referenceCurrency"], document, clone, helper);

								child = document.CreateElement ("composite");

								if ((target = kind ["determinationMethod"]) != null)
									Transcribe (target, document, child, helper);
								if ((target = kind ["relativeDate"]) != null)
									Transcribe (target, document, child, helper);
								if ((target = kind ["fxDetermination"]) != null)
									Transcribe (target, document, child, helper);

								clone.AppendChild (child);
							}
							break;
						}

						// Handle new buyer/seller party references in equity swap
						if (element.LocalName.Equals ("equitySwap")) {
							XmlElement	target;

							if ((target = element ["productType"]) != null)
								Transcribe (target, document, clone, helper);
		
							foreach (XmlElement child in element.GetElementsByTagName ("productId"))
								Transcribe (child, document, clone, helper);

							XmlElement	firstLeg = element.GetElementsByTagName ("equityLeg")[0] as XmlElement;

							XmlElement	buyer	= document.CreateElement ("buyerPartyReference");
							XmlElement	seller	= document.CreateElement ("sellerPartyReference");

							buyer.SetAttribute ("href", firstLeg ["payerPartyReference"].GetAttribute ("href"));
							seller.SetAttribute ("href", firstLeg ["receiverPartyReference"].GetAttribute ("href"));

							clone.AppendChild (buyer);
							clone.AppendChild (seller);

							foreach (XmlElement child in element.GetElementsByTagName ("equityLeg"))
								Transcribe (child, document, clone, helper);
							foreach (XmlElement child in element.GetElementsByTagName ("interestLeg"))
								Transcribe (child, document, clone, helper);

							if ((target = element ["principalExchangeFeatures"]) != null)
								Transcribe (target, document, clone, helper);

							foreach (XmlElement child in element.GetElementsByTagName ("additionalPayment"))
								Transcribe (child, document, clone, helper);
							foreach (XmlElement child in element.GetElementsByTagName ("earlyTermination"))
								Transcribe (child, document, clone, helper);

							break;
						}

						// Handle restructuring of initialPrice and valuationPriceFinal
						if (element.LocalName.Equals ("initialPrice") ||
							element.LocalName.Equals ("valuationPriceFinal")) {
							XmlElement	target;

							if ((target = element ["commission"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["determinationMethod"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["amountRelativeTo"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["grossPrice"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["netPrice"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["accruedInterestPrice"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["fxConversion"]) != null)
								Transcribe (target, document, clone, helper);

							XmlElement	valuation = document.CreateElement ("equityValuation");

							if ((target = element ["equityValuationDate"]) != null)
								Transcribe (target, document, valuation, helper);
							if ((target = element ["valuationTimeType"]) != null)
								Transcribe (target, document, valuation, helper);
							if ((target = element ["valuationTime"]) != null)
								Transcribe (target, document, valuation, helper);

							clone.AppendChild (valuation);
							break;
						}

						// Handle restructuring of valuationPriceInterim
						if (element.LocalName.Equals ("valuationPriceInterim")) {
							XmlElement	target;

							if ((target = element ["commission"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["determinationMethod"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["amountRelativeTo"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["grossPrice"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["netPrice"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["accruedInterestPrice"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["fxConversion"]) != null)
								Transcribe (target, document, clone, helper);

							XmlElement	valuation = document.CreateElement ("equityValuation");

							if ((target = element ["equityValuationDates"]) != null)
								Transcribe (target, document, valuation, helper);
							if ((target = element ["valuationTimeType"]) != null)
								Transcribe (target, document, valuation, helper);
							if ((target = element ["valuationTime"]) != null)
								Transcribe (target, document, valuation, helper);

							clone.AppendChild (valuation);
							break;
						}
						// Handle new optionality in constituentWeight
						if (element.LocalName.Equals ("constituentWeight")) {
							XmlElement	target = element ["basketPercentage"];

							if (target != null)
								Transcribe (target, document, clone, helper);
							else
								Transcribe (element ["openUnits"], document, clone, helper);

							break;
						}

						// Handle transfer of failure to deliver into extraordinary events
						if (element.LocalName.Equals ("extraordinaryEvents")) {
							XmlElement	target;

							if ((target = element ["mergerEvents"]) != null)
								Transcribe (target, document, clone, helper);
							
							XmlElement	failure = document.CreateElement ("failureToDeliver");
							
							if ((target = (element.ParentNode as XmlElement)["equityExercise"]["failureToDeliverApplicable"]) != null)
								failure.InnerText = target.InnerText;
							else
								failure.InnerText = "false";

							clone.AppendChild (failure);
							
							if ((target = element ["nationalisationOrInsolvency"]) != null)
								Transcribe (target, document, clone, helper);
							if ((target = element ["delisting"]) != null)
								Transcribe (target, document, clone, helper);

							break;
						}

						// Recursively copy the child node across
						foreach (XmlNode node in element.ChildNodes)
							Transcribe (node, document, clone, helper);

						break;
					}
					
				default:
					parent.AppendChild (document.ImportNode (context, false));
					break;
				}
			}
		}

		/// <summary>
		/// The <b>R4_1__R4_2</b> class implements a conversion from FpML 4-1
		/// to FpML 4-2.
		/// </summary>
		public class R4_1__R4_2 : HandCoded.Meta.DirectConversion
		{
			/// <summary>
			/// Constructs a <b>R4_1__TR4_2</b> instance.
			/// </summary>
			public R4_1__R4_2 ()
				: base (Releases.R4_1, Releases.R4_2)
			{ }

			/// <summary>
			/// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
			/// to create a new <see cref="XmlDocument"/>.
			/// </summary>
			/// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
			/// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
			/// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
			public override XmlDocument Convert(XmlDocument source, HandCoded.Meta.IHelper helper)
			{
				XmlDocument		target 	= TargetRelease.NewInstance ("FpML");
				XmlElement		oldRoot = source.DocumentElement;
				XmlElement		newRoot	= target.DocumentElement;

				// Transfer the attributes
				newRoot.SetAttribute ("xsi:type", oldRoot.GetAttribute ("xsi:type"));

				// Recursively copy the child node across
				foreach (XmlNode node in oldRoot.ChildNodes)
					Transcribe (node, target, newRoot);

				return (target);
			}

			private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent)
			{
				switch (context.NodeType) {
				case XmlNodeType.Element:
					{
						XmlElement		element = context as XmlElement;
						XmlElement		clone;

						clone = document.CreateElement (element.LocalName);

						parent.AppendChild (clone);

						foreach (XmlAttribute attr in element.Attributes)
							clone.SetAttribute (attr.Name, attr.Value);
						
						// Recursively copy the child node across
						foreach (XmlNode node in element.ChildNodes)
							Transcribe (node, document, clone);

						break;
					}
					
				default:
					parent.AppendChild (document.ImportNode (context, false));
					break;
				}
			}
		}

        /// <summary>
        /// The <b>R4_2__R4_3</b> class implements a conversion from FpML 4-2
        /// to FpML 4-3.
        /// </summary>
        public class R4_2__R4_3 : HandCoded.Meta.DirectConversion
        {
            /// <summary>
            /// Constructs a <b>R4_2__TR4_3</b> instance.
            /// </summary>
            public R4_2__R4_3 ()
                : base (Releases.R4_2, Releases.R4_3)
            {
            }

            /// <summary>
            /// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
            /// to create a new <see cref="XmlDocument"/>.
            /// </summary>
            /// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
            /// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
            /// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
            public override XmlDocument Convert (XmlDocument source, HandCoded.Meta.IHelper helper)
            {
                XmlDocument target = TargetRelease.NewInstance ("FpML");
                XmlElement oldRoot = source.DocumentElement;
                XmlElement newRoot = target.DocumentElement;

                // Transfer the attributes
                newRoot.SetAttribute ("xsi:type", oldRoot.GetAttribute ("xsi:type"));

                // Recursively copy the child node across
                foreach (XmlNode node in oldRoot.ChildNodes)
                    Transcribe (node, target, newRoot);

                return (target);
            }

            private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent)
            {
                switch (context.NodeType) {
                case XmlNodeType.Element: {
                        XmlElement element = context as XmlElement;
                        XmlElement clone;

                        clone = document.CreateElement (element.LocalName);

                        parent.AppendChild (clone);

                        foreach (XmlAttribute attr in element.Attributes)
                            clone.SetAttribute (attr.Name, attr.Value);

                        // Recursively copy the child node across
                        foreach (XmlNode node in element.ChildNodes)
                            Transcribe (node, document, clone);

                        break;
                    }

                default:
                    parent.AppendChild (document.ImportNode (context, false));
                    break;
                }
            }
        }

        /// <summary>
        /// The <b>R4_3__R4_4</b> class implements a conversion from FpML 4-3
        /// to FpML 4-4.
        /// </summary>
        public class R4_3__R4_4 : HandCoded.Meta.DirectConversion
        {
            /// <summary>
            /// Constructs a <b>R4_3__TR4_4</b> instance.
            /// </summary>
            public R4_3__R4_4 ()
                : base (Releases.R4_3, Releases.R4_4)
            {
            }

            /// <summary>
            /// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
            /// to create a new <see cref="XmlDocument"/>.
            /// </summary>
            /// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
            /// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
            /// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
            public override XmlDocument Convert (XmlDocument source, HandCoded.Meta.IHelper helper)
            {
                XmlDocument target = TargetRelease.NewInstance ("FpML");
                XmlElement oldRoot = source.DocumentElement;
                XmlElement newRoot = target.DocumentElement;

                // Transfer the attributes
                newRoot.SetAttribute ("xsi:type", oldRoot.GetAttribute ("xsi:type"));

                // Recursively copy the child node across
                foreach (XmlNode node in oldRoot.ChildNodes)
                    Transcribe (node, target, newRoot);

                return (target);
            }

            private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent)
            {
                switch (context.NodeType) {
                case XmlNodeType.Element: {
                        XmlElement element = context as XmlElement;
                        XmlElement clone;

                        clone = document.CreateElement (element.LocalName);

                        parent.AppendChild (clone);

                        foreach (XmlAttribute attr in element.Attributes)
                            clone.SetAttribute (attr.Name, attr.Value);

                        // Recursively copy the child node across
                        foreach (XmlNode node in element.ChildNodes)
                            Transcribe (node, document, clone);

                        break;
                    }

                default:
                    parent.AppendChild (document.ImportNode (context, false));
                    break;
                }
            }
        }

        /// <summary>
        /// The <b>R4_4__R4_5</b> class implements a conversion from FpML 4-4
        /// to FpML 4-5.
        /// </summary>
        public class R4_4__R4_5 : HandCoded.Meta.DirectConversion
        {
            /// <summary>
            /// Constructs a <b>R4_4__TR4_5</b> instance.
            /// </summary>
            public R4_4__R4_5 ()
                : base (Releases.R4_4, Releases.R4_5)
            {
            }

            /// <summary>
            /// Applies the <b>Conversion</b> to a <see cref="XmlDocument"/> instance
            /// to create a new <see cref="XmlDocument"/>.
            /// </summary>
            /// <param name="source">The <see cref="XmlDocument"/> to be converted.</param>
            /// <param name="helper">A <see cref="IHelper"/> used to guide conversion.</param>
            /// <returns>A new <see cref="XmlDocument"/> containing the transformed data.</returns>
            public override XmlDocument Convert (XmlDocument source, HandCoded.Meta.IHelper helper)
            {
                XmlDocument target = TargetRelease.NewInstance ("FpML");
                XmlElement oldRoot = source.DocumentElement;
                XmlElement newRoot = target.DocumentElement;

                // Transfer the attributes
                newRoot.SetAttribute ("xsi:type", oldRoot.GetAttribute ("xsi:type"));

                // Recursively copy the child node across
                foreach (XmlNode node in oldRoot.ChildNodes)
                    Transcribe (node, target, newRoot);

                return (target);
            }

            private void Transcribe (XmlNode context, XmlDocument document, XmlNode parent)
            {
                switch (context.NodeType) {
                case XmlNodeType.Element: {
                        XmlElement element = context as XmlElement;
                        XmlElement clone;

                        clone = document.CreateElement (element.LocalName);

                        parent.AppendChild (clone);

                        foreach (XmlAttribute attr in element.Attributes)
                            clone.SetAttribute (attr.Name, attr.Value);

                        // Recursively copy the child node across
                        foreach (XmlNode node in element.ChildNodes)
                            Transcribe (node, document, clone);

                        break;
                    }

                default:
                    parent.AppendChild (document.ImportNode (context, false));
                    break;
                }
            }
        }

		/// <summary>
		/// Ensures no instances can be constructed.
		/// </summary>
		private Conversions()
		{ }
	}
}