// Project: XnaGraphicEngine, File: EnumHelper.cs
// Namespace: XnaGraphicEngine.Helpers, Class: EnumHelper
// Path: C:\code\XnaGraphicEngine\Helpers, Author: Abi
// Code lines: 11, Size of file: 161 Bytes
// Creation date: 24.11.2005 12:46
// Last modified: 02.12.2005 19:33
// Generated with Commenter by abi.exDream.com

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
#endregion

namespace XnaGraphicEngine.Helpers
{
	/// <summary>
	/// Enum helper
	/// </summary>
	class EnumHelper
	{
		#region Enum enumerator class
		/// <summary>
		/// Enum enumerator helper for GetEnumerator,
		/// this allow us to enumerate enums just like collections.
		/// </summary>
		public class EnumEnumerator : IEnumerator, IEnumerable
		{
			/// <summary>
			/// The enum we use
			/// </summary>
			public System.Type enumType;
			/// <summary>
			/// Own index
			/// </summary>
			public int index;
			/// <summary>
			/// Length of enum
			/// </summary>
			public int enumLength;

			/// <summary>
			/// Create enum enumerator
			/// </summary>
			public EnumEnumerator(System.Type setEnumType)
			{
				enumType = setEnumType;
				index = -1;
				enumLength = GetSize(enumType);
			} // EnumEnumerator(setEnumType)

			/// <summary>
			/// Own
			/// </summary>
			public object Current
			{
				get
				{
					if (index >= 0 &&
						index < enumLength)
						return Enum.GetValues(enumType).GetValue(new int[] { index });
					else
						// Just return first entry if index is invalid
						return Enum.GetValues(enumType).GetValue(new int[] { 0 });
				} // get
			} // Current

			/// <summary>
			/// Move next
			/// </summary>
			public bool MoveNext()
			{
				index++;

				// Finished?
				return index < enumLength;
			} // MoveNext()

			/// <summary>
			/// Reset
			/// </summary>
			public void Reset()
			{
				index = -1;
			} // Reset()

			/// <summary>
			/// Get enumerator
			/// </summary>
			public IEnumerator GetEnumerator()
			{
				return this;
			} // GetEnumerator()
		} // class EnumEnumerator
		#endregion

		#region Get size
		/// <summary>
		/// Get number of elements of a enum (accessing enum by type)
		/// </summary>
		public static int GetSize(System.Type enumType)
		{
			return Enum.GetNames(enumType).Length;
		} // GetSize(enumType)
		#endregion

		#region Get enumerator
		/// <summary>
		/// Get enumerator
		/// </summary>
		public static EnumEnumerator GetEnumerator(System.Type enumType)
		{
			return new EnumEnumerator(enumType);
		} // GetEnumerator(enumType)
		#endregion

		#region Search enumerator
		/// <summary>
		/// Search enumerator
		/// </summary>
		/// <param name="type">Type</param>
		/// <param name="name">Name</param>
		/// <returns>Object</returns>
		public static object SearchEnumerator(Type type, string name)
		{
			foreach (object objEnum in GetEnumerator(type))
				if (StringHelper.Compare(objEnum.ToString(), name))
					return objEnum;

			// Else not found, just return first!
			return 0;
		} // SearchEnumerator(type, name)
		#endregion

		#region Get all enum names
		/// <summary>
		/// Get all names from an enum.
		/// E.g. If we have an enum with 3 values (A, B and C),
		/// then return "A, B, C".
		/// </summary>
		/// <param name="type">Enum type, will be passed to
		/// GetEnumerator</param>
		/// <returns>String with all enum names</returns>
		public static string GetAllEnumNames(Type type)
		{
			// Simplified version
			return StringHelper.WriteArrayData(GetEnumerator(type));

			/*same code, but more complicated:
			List<string> returnList = new List<string>();
			foreach (Enum enumValue in GetEnumerator(type))
				returnList.Add(enumValue.ToString());
			return StringHelper.WriteArrayData(returnList);
			 */
		} // GetAllEnumNames(type)
		#endregion

		#region Unit Testing
#if DEBUG
		/*include NUnit.Framework for this
		/// <summary>
		/// EnumHelper tests to find out if all methods work properly.
		/// </summary>
		[TestFixture]
		public class EnumHelperTests
		{
			/// <summary>
			/// Test GetAllEnumNames method, which does return all names
			/// from the enum. We test the RocketCommanderForm MenuButtons.
			/// </summary>
			[Test]
			public void TestGetAllEnumNames()
			{
				Assert.AreEqual(
					"Missions, Highscore, Credits, Help, Options, Exit, Back",
					EnumHelper.GetAllEnumNames(typeof(MenuButton)));
			} // TestGetAllEnumNames()
		} // class EnumHelperTests
		 */
#endif
		#endregion
	} // class EnumHelper
} // namespace XnaGraphicEngine.Helpers
