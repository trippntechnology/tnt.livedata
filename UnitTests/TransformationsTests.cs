using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TNT.LiveData;

namespace UnitTests
{
	[TestClass]
	public class TransformationsTests
	{
		[TestMethod]
		public void Tranformations_Map()
		{
			var liveData1 = new LiveData<int>(1);
			var liveData2 = liveData1.Map(value => { return value * 2; });
			var liveData3 = liveData2.Map(value => { return value * 2; });
			var liveData2Result = 0;
			var liveData3Result = 0;

			liveData2.Observe(v => { liveData2Result = v; });
			liveData3.Observe(v => { liveData3Result = v; });

			Assert.AreEqual(1, liveData1.Value);
			Assert.AreEqual(2, liveData2Result);
			Assert.AreEqual(4, liveData3Result);

			liveData1.Value = 7;
			Assert.AreEqual(14, liveData2Result);
			Assert.AreEqual(28, liveData3Result);
		}

		[TestMethod]
		public void Tranformations_SwitchMap()
		{
			var observedLive = new LiveData<int>(0);
			var liveDatas = new List<LiveData<string>>()
			{
				new LiveData<string>("zero"),
				new LiveData<string>("one"),
				new LiveData<string>("two"),
				new LiveData<string>("three"),
			};

			var switchedLive = observedLive.SwitchMap<int, string>(value => { return liveDatas[value]; });

			Assert.IsNull(switchedLive.Value);
			observedLive.Value = 0;
			Assert.AreEqual("zero", switchedLive.Value);
			liveDatas[0].Value = "z e r o";
			Assert.AreEqual("z e r o", switchedLive.Value);

			observedLive.Value = 1;
			Assert.AreEqual("one", switchedLive.Value);

			liveDatas[0].Value = "zero";
			Assert.AreEqual("one", switchedLive.Value);


		}
	}
}
