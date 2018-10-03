// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Security;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Security.Permissions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyVersion("4.6.0.0")]
[assembly: AssemblyFileVersion("4.6.0.0")]
[assembly: AssemblyCopyright("© Component Factory Pty Ltd 2012. All rights reserved.")]
[assembly: AssemblyInformationalVersion("4.6.0.0")]
[assembly: AssemblyProduct("External Drag To Docking")]
[assembly: AssemblyDefaultAlias("ExternalDragToDocking.dll")]
[assembly: AssemblyTitle("External Drag To Docking")]
[assembly: AssemblyCompany("Component Factory")]
[assembly: AssemblyDescription("External Drag To Docking")]
[assembly: AssemblyConfiguration("Production")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: StringFreezing]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: AllowPartiallyTrustedCallers()]
[assembly: Dependency("System", LoadHint.Always)]
[assembly: Dependency("System.Drawing", LoadHint.Always)]
[assembly: Dependency("System.Windows.Forms", LoadHint.Always)]
[assembly: Dependency("ComponentFactory.Krypton.Toolkit", LoadHint.Always)]
[assembly: Dependency("ComponentFactory.Krypton.Navigator", LoadHint.Always)]
[assembly: Dependency("ComponentFactory.Krypton.Workspace", LoadHint.Always)]
[assembly: Dependency("ComponentFactory.Krypton.Docking", LoadHint.Always)]
