using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.LiveData;

namespace UnitTests
{
	[TestClass]
	public class LiveDataTests
	{
		[TestMethod]
		public void LiveData_Initial_Value()
		{
			var sut = new LiveData<int>(10);
			Assert.AreEqual(10, sut.Value);
			var value = 0;

			sut.Observe(v =>
			{
				value = v;
			});

			Assert.AreEqual(sut.Value, value);
			sut.Value = 20;
			Assert.AreEqual(sut.Value, value);
		}

		[TestMethod]
		public void LiveData_Default()
		{
			var sutInt = new LiveData<int>();
			Assert.AreEqual(0, sutInt.Value);
			var sutString = new LiveData<string>();
			Assert.IsNull(sutString.Value);
		}

		[TestMethod]
		public void LiveData_Source()
		{
			var source1 = new LiveData<string>();
			var source2 = new LiveData<string>();
			var mediatorLive = new LiveData<string>();
			Action<LiveData<string>, LiveData<string>> action = (v1, v2) => { mediatorLive.Value = String.Concat(v1.Value, ":", v2.Value); };

			mediatorLive.AddSource(source1, s => action(source1, source2));
			mediatorLive.AddSource(source2, s => action(source1, source2));

			source1.Value = "First";
			Assert.AreEqual("First:", mediatorLive.Value);
			source2.Value = "Second";
			Assert.AreEqual("First:Second", mediatorLive.Value);
		}
	}
}