#if TST_DISABLED
// Project: XnaGraphicEngine, File: DisposeHelper.cs
// Namespace: XnaGraphicEngine.Helpers, Class: DisposeHelper
// Path: C:\code\XnaGraphicEngine\Helpers, Author: Abi
// Code lines: 314, Size of file: 7,87 KB
// Creation date: 11.09.2006 08:16
// Last modified: 16.10.2006 11:47
// Generated with Commenter by abi.exDream.com

#region Using directives
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using XnaGraphicEngine.Graphics;
using XnaGraphicEngine.Landscapes;
using XnaGraphicEngine.Shaders;
using XnaGraphicEngine.Tracks;
using Model = XnaGraphicEngine.Graphics.Model;
using Texture = XnaGraphicEngine.Graphics.Texture;
#endregion

namespace XnaGraphicEngine.Helpers
{
	/// <summary>
	/// Helper class to dispose stuff. I really hate writing 3 lines
	/// just to dispose something, this helper makes it 1 line!
	/// A big problem with this is the fact that we can't just use
	/// ref IDisposable because casting does not work this way.
	/// For that reason there are many overloads in this helper class.
	/// </summary>
	class DisposeHelper
	{
		#region Dispose
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref IDisposable someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref Effect someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref SpriteBatch someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref XnaGraphicEngine.Graphics.Texture someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref XnaGraphicEngine.Graphics.TextureFont someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref Texture2D someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref TextureCube someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		
		/*not supported anymore in xna drop 6
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref Surface someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		 */

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref RenderTarget2D someObject)
		{
			if (someObject != null)
			{
				try
				{
					someObject.Dispose();
				} // try
				catch {} // ignore
			} // if (someObject)
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref ShaderEffect someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref PostScreenMenu someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref PostScreenGlow someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref PreScreenSkyCubeMapping someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref ShadowMapShader someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref XnaGraphicEngine.Graphics.LensFlare someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref RenderToTexture someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref SoundBank someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref WaveBank someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref AudioEngine someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref Model someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref Landscape someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref Track someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref Material someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref VertexBuffer someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="someObject">Some object</param>
		public static void Dispose(ref IndexBuffer someObject)
		{
			if (someObject != null)
				someObject.Dispose();
			someObject = null;
		} // Dispose(someObject)
		#endregion
	} // class DisposeHelper
} // namespace XnaGraphicEngine.Helpers
#endif