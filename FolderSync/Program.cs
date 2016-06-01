// ***********************************************************************
// Assembly         : FolderSync
// Component        : Program.cs
// Created          : 05-31-2016
// 
// Version          : 1.0.0
// Last Modified On : 05-31-2016
// ***********************************************************************
// <copyright file="Program.cs" company="">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Runs the program.
// </summary>
//
// Changelog: 
//            - 1.0.0 (05-31-2016) - Initial version created.
// ***********************************************************************
using System;
using System.Windows.Forms;

namespace FolderSync
{
    /// <summary>
    /// Class Program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
