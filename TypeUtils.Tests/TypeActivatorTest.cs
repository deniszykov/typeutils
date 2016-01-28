﻿using System;
using System.Collections.Generic;
using Xunit;

namespace TypeUtils.Tests
{
	public class TypeActivatorTest
	{
		[Theory]
		[InlineData(default(byte))]
		[InlineData(default(short))]
		[InlineData(default(int))]
		[InlineData(default(long))]
		[InlineData(default(sbyte))]
		[InlineData(default(ushort))]
		[InlineData(default(uint))]
		[InlineData(default(ulong))]
		[InlineData(default(float))]
		[InlineData(default(double))]
		public void CreateBuildInTypes(object expectedValue)
		{
			var actualValue = TypeActivator.CreateInstance(expectedValue.GetType());
			Assert.Equal(expectedValue, actualValue);
		}

		[Theory]
		[InlineData(typeof(DateTime))] // struct
		[InlineData(typeof(decimal))] // struct
		[InlineData(typeof(Guid))] // struct
		[InlineData(typeof(ConsoleColor))] // enum
		[InlineData(typeof(EventArgs))] // class with Empty property
		[InlineData(typeof(string))] // class with Empty property
		[InlineData(typeof(SystemException))] // class
		[InlineData(typeof(int[]))] // array
		[InlineData(typeof(List<int>))] // class
		public void CreateType(Type expectedInstanceType)
		{
			var actualValue = TypeActivator.CreateInstance(expectedInstanceType);
			Assert.NotNull(actualValue);
			Assert.IsAssignableFrom(expectedInstanceType, actualValue);
		}

		[Fact]
		public void CreateInstanceWithArgs1()
		{
			var arr = new byte[0];
			var expected = new ArraySegment<byte>(arr);
			var actual = (ArraySegment<byte>)TypeActivator.CreateInstance(typeof(ArraySegment<byte>), arr);

			Assert.Equal(expected.Array, actual.Array);
			Assert.Equal(expected.Offset, actual.Offset);
			Assert.Equal(expected.Count, actual.Count);
		}
		[Fact]
		public void CreateInstanceWithArgs2()
		{
			var expected = new string('c', 10);
			var actual = (string)TypeActivator.CreateInstance(typeof(string), 'c', 10);

			Assert.Equal(expected, actual);
		}
		[Fact]
		public void CreateInstanceWithArgs3()
		{
			var arr = new byte[20];
			var expected = new ArraySegment<byte>(arr, 10, 10);
			var actual = (ArraySegment<byte>)TypeActivator.CreateInstance(typeof(ArraySegment<byte>), arr, 10, 10);

			Assert.Equal(expected.Array, actual.Array);
			Assert.Equal(expected.Offset, actual.Offset);
			Assert.Equal(expected.Count, actual.Count);
		}
	}
}
