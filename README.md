# Krypton Suite of .NET WinForms Controls
The Krypton Suite of .NET WinForms controls are now freely available for use in personal or commerical projects.

I developed and sold them from my company Component Factory from 2006 until 2014, when the lack of sales meant selling the controls was no longer viable. So I decided to make them open source so that .NET developers, hobby developers in particular, had a good set of free controls to use in their projects. There is no point in all that hard work sitting on my hard drive when it could be useful to others. Full source code for all the controls and components is included along with Visual Studio 2015 projects and solution files.

# Getting Started #
**Bin** directory contains already compiled copies of all the example projects and the Krypton assemblies, so I recommend you start by running the **Krypton Explorer** application in this directory. It lists all the controls along with an example application used to show it in operation.

# Documentation #
**Help** directory contains a **KryptonHelp.chm** file that can be double clicked to open the documentation. I recommend you read this before developing using the Krypton controls.

# Using with Visual Studio #
 - Run the batch file **RegisterToGAC.bat** in the **Bin** directory as an admin so the assemblies are placed in the GAC
 - Start **Visual Studio** and create/open your Windows Forms project
 - Open the main Form of your application and show the **Toolbox**
 - Right click the **Toolbox** and **Add Tab**, give it the name **Krypton**
 - Right click inside the new tab and select **Choose Toolbox Items**
 - Use the **Browse** button and go to your **Bin** directory, select all the **ComponentFactory.Krypton...** assemblies
 - Ignore warning for **ComponentFactory.Krypton.Design.dll** as it does not have any controls in it
 - Select **OK** and now you have them all in the **Toolbox!**

# Source #
**Source** directory contains the full source code that you can view, modify and directly compile. The Krypton Components sub-directory contains all the actual controls, the other directories are for the myriad example projects.

# Krypton Toolkit
49 basic controls with full and consistent theming.

![](/Images/home_toolkit1.gif?raw=true)  ![](/Images/home_toolkit2.gif?raw=true)  ![](/Images/home_toolkit3.gif?raw=true)
![](/Images/home_toolkit4.gif?raw=true)  ![](/Images/home_toolkit5.gif?raw=true)  ![](/Images/home_toolkit6.gif?raw=true)

# Krypton Ribbon
Office style ribbon control.

![](/Images/p_ribbon1.gif?raw=true)  ![](/Images/p_ribbon2.gif?raw=true) 
![](/Images/p_ribbon3.gif?raw=true)  ![](/Images/p_ribbon4.gif?raw=true)


# Krypton Docking
Drag and drop just like Visual Studio.

![](/Images/KDocking.gif?raw=true)

# Krypton Navigator
A TabControl but so much better.

![](/Images/home_navigator1.gif?raw=true)  ![](/Images/home_navigator2.gif?raw=true)
![](/Images/home_navigator3.gif?raw=true)  ![](/Images/home_navigator4.gif?raw=true)

# Krypton Workspace
Organize the client area of your application.

![](/Images/KWSContext2.gif?raw=true)



