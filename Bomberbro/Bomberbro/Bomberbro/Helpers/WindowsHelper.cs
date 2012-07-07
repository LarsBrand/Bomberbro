// Project: XnaGraphicEngine, File: WindowsHelper.cs
// Namespace: XnaGraphicEngine.Helpers, Class: WindowsHelper
// Path: C:\code\XnaGraphicEngine\Helpers, Author: Abi
// Code lines: 11, Size of file: 205 Bytes
// Creation date: 01.11.2005 19:43
// Last modified: 11.12.2005 14:11
// Generated with Commenter by abi.exDream.com

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#if !XBOX360
using System.Security.Principal;
#endif
using System.Globalization;
#endregion

namespace Bomberbro.Helpers
{
	/// <summary>
	/// Windows helper
	/// </summary>
	class WindowsHelper
	{
		#region NativeMethods helper class (required for FxCop checks)
#if DEBUG && !XBOX360
//*not possible on XBox
		/// <summary>
		/// NativeMethods helper class (required for FxCop checks)
		/// </summary>
		internal class NativeMethods
		{
			/// <summary>
			/// Don't allow instantiating this class.
			/// </summary>
			private NativeMethods()
			{
			} // NativeMethods()

			/// <summary>
			/// Win32 function: Get foreground window handle (hwnd)
			/// </summary>
			[DllImport("User32.dll")]
			internal static extern int GetForegroundWindow();

			/// <summary>
			/// Win32 function: Set foreground window handle
			/// </summary>
			[DllImport("User32.dll")]
			internal static extern int SetForegroundWindow(int hWnd);

			/// <summary>
			/// Win 32 function: Get window thread process id
			/// </summary>
			/// <param name="window">Window handle</param>
			/// <param name="processId">Process id</param>
			/// <returns>Thread id</returns>
			[DllImport("user32.dll")]
			internal static extern int GetWindowThreadProcessId(
				int window, int processId);

			/// <summary>
			/// Win32 function: Attaches input to a thread for controlling windows
			/// </summary>
			[DllImport("User32.dll")]
			internal static extern int AttachThreadInput(
				int idAttach, int idAttachTo, int fAttach);

			/// <summary>
			/// Win32 function: Checks if a window is iconic (on the task bar)
			/// </summary>
			[DllImport("User32.dll")]
			internal static extern int IsIconic(int hWnd);
			/// <summary>
			/// Win32 function: Show window with nCmdShow parameters.
			/// </summary>
			[DllImport("User32.dll")]
			internal static extern int ShowWindow(int hWnd, int nCmdShow);

			/// <summary>
			/// Win32 function: Get device context of window handle (hwnd).
			/// </summary>
			[DllImport("User32.dll")]
			internal static extern IntPtr GetWindowDC(IntPtr hwnd);

			/// <summary>
			/// Win32 function: Release a device context of a window handle.
			/// </summary>
			[DllImport("User32.dll")]
			internal static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

			/// <summary>
			/// Win32 function: Bit blt an image the GDI way ..
			/// </summary>
			[DllImport("gdi32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool BitBlt(IntPtr hdcDst,
				int xDst, int yDst, int cx, int cy,
				IntPtr hdcSrc, int xSrc, int ySrc, uint ulRop);

			/// <summary>
			/// Query performance (high resolution) timer frequency
			/// </summary>
			/// <param name="lpFrequency">current frequency</param>
			[System.Security.SuppressUnmanagedCodeSecurity]
			[DllImport("Kernel32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool QueryPerformanceFrequency(
				out long lpFrequency);

			/// <summary>
			/// Query performance (high resolution) timer counter
			/// </summary>
			/// <param name="lpCounter">current counter value</param>
			[System.Security.SuppressUnmanagedCodeSecurity]
			[DllImport("Kernel32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool QueryPerformanceCounter(
				out long lpCounter);
		} // class NativeMethods
 //*/
#endif
		#endregion

		#region Constructor
		/// <summary>
		/// Don't allow instantiating this class.
		/// </summary>
		private WindowsHelper()
		{
		} // WindowsHelper()
		#endregion

		#region Get and set foreground window
#if DEBUG && !XBOX360
		// Only supported in debug mode and in windows

		/// <summary>
		/// Win32 constants
		/// </summary>
		public const int
			SwShow = 5,
			SwRestore = 9;

		/// <summary>
		/// Helper function to force a window into foreground, will
		/// even work if our process is not the same as the other windows.
		/// We will try to get the others thread input process and
		/// attach it to our thread for setting the window to the foreground.
		/// </summary>
		public static bool ForceForegroundWindow(int hWnd)
		{
			int foregroundWnd = NativeMethods.GetForegroundWindow();
			// Do nothing if already in foreground.
			if (hWnd == foregroundWnd)
				return true;

			// First need to get the thread responsible for this window,
			// and the thread for the foreground window.
			int ret = 0,
				threadID1 = NativeMethods.GetWindowThreadProcessId(foregroundWnd, 0),
				threadID2 = NativeMethods.GetWindowThreadProcessId(hWnd, 0);

			// By sharing input state, threads share their concept of
			// the active window.
			if (threadID1 != threadID2)
			{
				NativeMethods.AttachThreadInput(threadID1, threadID2, 1);//true
				ret = NativeMethods.SetForegroundWindow(hWnd);
				NativeMethods.AttachThreadInput(threadID1, threadID2, 0);//false
			} // if (threadID1)
			else
				ret = NativeMethods.SetForegroundWindow(hWnd);

			// Restore and repaint
			if (NativeMethods.IsIconic(hWnd) != 0)
				NativeMethods.ShowWindow(hWnd, SwRestore);
			else
				NativeMethods.ShowWindow(hWnd, SwShow);

			// Succeeded
			return ret != 0;
		} // ForceForegroundWindow(hWnd)
#endif
		/*
		/// <summary>
		/// Force foreground window. Do a little trick to get foreground access to
		/// form. Usually NUnit will keep VS in the foreground and our newly
		/// created form is created in the background. form.Focus, form.TopMost,
		/// form.SendToFront, etc. will all not work because VS will still have
		/// the input focus. So we first put the form at topmost, try to give
		/// focus to it, then force it to be in the foreground and finally disable
		/// the topmost mode again, voila, works nice :)
		/// </summary>
		/// <param name="form">Form</param>
		public static void ForceForegroundWindow(Form form)
		{
			if (form == null)
				throw new ArgumentNullException("form",
					"You need a valid form to perform ForceForegroundWindow");

			// Note for debugging: Don't step into here, VS may crash,
			// just set a breakpoint somewhere after the last line here.
			bool wasAlreadyTopMost = form.TopMost;

			// Bring top most and try to give focus
			form.TopMost = true;
			form.Focus();

			// Now force it to the foreground
			WindowsHelper.ForceForegroundWindow(form.Handle.ToInt32());

			// And restore top most state if necessary
			if (wasAlreadyTopMost == false)
				form.TopMost = false;
		} // ForceForegroundWindow(form)
 */
		#endregion

		#region Performance counters and getting ns time
/*not allowed on Xbox
		/// <summary>
		/// Get current performance timer frequency
		/// (using QueryPerformanceFrequency)
		/// </summary>
		public static long GetPerformanceFrequency()
		{
			long l;
			NativeMethods.QueryPerformanceFrequency(out l);
			return l;
		} // GetPerformanceFrequency()

		/// <summary>
		/// Get current performance timer counter value
		/// (using QueryPerformanceCounter)
		/// </summary>
		public static long GetPerformanceCounter()
		{
			long l;
			NativeMethods.QueryPerformanceCounter(out l);
			return l;
		} // GetPerformanceCounter()

		/// <summary>
		/// Remember the frequency
		/// </summary>
		public static long performanceFrequency = GetPerformanceFrequency();

		/// <summary>
		/// Convert performance counter value to ns.
		/// </summary>
		/// <param name="perfCounter">Counter difference from 2 values</param>
		static public long ConvertToNs(long perfCounter)
		{
			return perfCounter * 1000000 / performanceFrequency;
		} // ConvertToNs(perfCounter)
		
		/*now back in ElapsedTime.cs
		/// <summary>
		/// For frame per seconds calculations: Start tick in ns
		/// (when start counting frames)
		/// </summary>
		private static long startTimeNs =
			ConvertToNs(WindowsHelper.GetPerformanceCounter());

		/// <summary>
		/// Get total elapsed time in ms
		/// </summary>
		public static int MsTotal
		{
			get
			{
				return (int)((ConvertToNs(WindowsHelper.GetPerformanceCounter()) -
					startTimeNs) / 1000);
			} // get
		} // MsTotal
		*/
		#endregion
		
		#region Get default player name
		/// <summary>
		/// Get default player name from windows identity, and if
		/// this failes use computer name!
		/// </summary>
		static public string GetDefaultPlayerName()
		{
			string defaultPlayerName = "bomberManGuy";
#if !XBOX360
			try
			{
				defaultPlayerName = WindowsIdentity.GetCurrent().Name;

				if (String.IsNullOrEmpty(defaultPlayerName))
				{
					defaultPlayerName = "bomberManGuy";
				} // if (defaultPlayerName)
				else
				{
					// Windows will return name in format <computername>\<username>,
					// we just want the username!
					string[] nameInfo = defaultPlayerName.Split(
						new char[] { '\\' }, 2);
					if (nameInfo.Length >= 2)
					{
						defaultPlayerName = nameInfo[1];
					} // if
				} // else
			} // try
			catch { } // Ignore any error
#endif

			return defaultPlayerName;
		} // GetDefaultPlayerName()
		#endregion
	} // class WindowsHelper
} // namespace XnaGraphicEngine.Helpers
