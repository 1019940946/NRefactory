﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under MIT X11 license (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Linq;

namespace ICSharpCode.NRefactory.TypeSystem.Implementation
{
	/// <summary>
	/// Default implementation of <see cref="IAccessor"/>.
	/// </summary>
	public sealed class DefaultAccessor : AbstractFreezable, IAccessor
	{
		static readonly DefaultAccessor[] defaultAccessors = CreateDefaultAccessors();
		
		static DefaultAccessor[] CreateDefaultAccessors()
		{
			DefaultAccessor[] accessors = new DefaultAccessor[(int)Accessibility.ProtectedAndInternal + 1];
			for (int i = 0; i < accessors.Length; i++) {
				accessors[i] = new DefaultAccessor();
				accessors[i].accessibility = (Accessibility)i;
				accessors[i].Freeze();
			}
			return accessors;
		}
		
		/// <summary>
		/// Gets the default accessor with the specified accessibility (and without attributes or region).
		/// </summary>
		public static IAccessor GetFromAccessibility(Accessibility accessibility)
		{
			int index = (int)accessibility;
			if (index >= 0 && index < defaultAccessors.Length) {
				return defaultAccessors[index];
			} else {
				DefaultAccessor a = new DefaultAccessor();
				a.accessibility = accessibility;
				a.Freeze();
				return a;
			}
		}
		
		Accessibility accessibility;
		DomRegion region;
		IList<IAttribute> attributes;
		
		protected override void FreezeInternal()
		{
			base.FreezeInternal();
			this.attributes = FreezeList(this.attributes);
		}
		
		public Accessibility Accessibility {
			get { return accessibility; }
			set {
				CheckBeforeMutation();
				accessibility = value;
			}
		}
		
		public DomRegion Region {
			get { return region; }
			set {
				CheckBeforeMutation();
				region = value;
			}
		}
		
		public IList<IAttribute> Attributes {
			get {
				if (attributes == null)
					attributes = new List<IAttribute>();
				return attributes;
			}
		}
	}
}
