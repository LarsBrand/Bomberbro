// Project: XnaGraphicEngine, File: Vector3Helper.cs
// Namespace: XnaGraphicEngine.Helpers, Class: Vector3Helper
// Path: C:\code\XnaGraphicEngine\Helpers, Author: Abi
// Code lines: 62, Size of file: 2,14 KB
// Creation date: 13.10.2006 23:14
// Last modified: 20.10.2006 08:29
// Generated with Commenter by abi.exDream.com

#region Using directives
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace XnaGraphicEngine.Helpers
{
	/// <summary>
	/// Vector 3 helper
	/// </summary>
	class Vector3Helper
	{
		#region Get angle between vectors
		/// <summary>
		/// Return angle between two vectors. Used for visbility testing and
		/// for checking angles between vectors for the road sign generation.
		/// </summary>
		/// <param name="vec1">Vector 1</param>
		/// <param name="vec2">Vector 2</param>
		/// <returns>Float</returns>
		public static float GetAngleBetweenVectors(Vector3 vec1, Vector3 vec2)
		{
			// See http://en.wikipedia.org/wiki/Vector_(spatial)
			// for help and check out the Dot Product section ^^
			// Both vectors are normalized so we can save deviding through the
			// lengths.
			return (float)Math.Acos(Vector3.Dot(vec1, vec2));
		} // GetAngleBetweenVectors(vec1, vec2)
		#endregion

		#region Distance to line
		/// <summary>
		/// Distance from our point to the line described by linePos1 and linePos2.
		/// </summary>
		/// <param name="point">Point</param>
		/// <param name="linePos1">Line position 1</param>
		/// <param name="linePos2">Line position 2</param>
		/// <returns>Float</returns>
		public static float DistanceToLine(Vector3 point,
			Vector3 linePos1, Vector3 linePos2)
		{
			// For help check out this article:
			// http://mathworld.wolfram.com/Point-LineDistance3-Dimensional.html
			Vector3 lineVec = linePos2-linePos1;
			Vector3 pointVec = linePos1-point;
			return Vector3.Cross(lineVec, pointVec).Length() / lineVec.Length();
		} // DistanceToLine(point, linePos1, linePos2)
		#endregion

		#region Signed distance to plane
		/// <summary>
		/// Signed distance to plane
		/// </summary>
		/// <param name="point">Point</param>
		/// <param name="planePosition">Plane position</param>
		/// <param name="planeNormal">Plane normal</param>
		/// <returns>Float</returns>
		public static float SignedDistanceToPlane(Vector3 point,
			Vector3 planePosition, Vector3 planeNormal)
		{
			Vector3 pointVec = planePosition - point;
			return Vector3.Dot(planeNormal, pointVec);
		} // SignedDistanceToPlane(point, planePosition, planeNormal)
		#endregion
	} // class Vector3Helper
} // namespace XnaGraphicEngine.Helpers
