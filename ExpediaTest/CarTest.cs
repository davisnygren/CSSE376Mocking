using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestThatCarLocationWorks()
        {
            targetCar = new Car(24);
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            targetCar.Database = mockDB;
            String loc = "Panama City Beach";
            String loc2 = "Neverland";

            Expect.Call(mockDB.getCarLocation(24)).Return(loc);
            Expect.Call(mockDB.getCarLocation(1024)).Return(loc2);

            mocks.ReplayAll();

            String result = targetCar.getCarLocation(24);
            Assert.AreEqual(loc, result);

            targetCar = new Car(1024);
            targetCar.Database = mockDB;
            result = targetCar.getCarLocation(1024);
            Assert.AreEqual(loc2, result);
        }

        [TestMethod]
        public void TestThatCarMileageWorks()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            int mileage = 1295;
            int mileage2 = 12358;

            targetCar = new Car(10);
            targetCar.Database = mockDatabase;

            Expect.Call(targetCar.Mileage).Return(mileage);

            mocks.ReplayAll();

            Assert.AreEqual(targetCar.Mileage, mileage);

            targetCar = new Car(1024);
            targetCar.Database = mockDatabase;

            mockDatabase.Expect(x => x.Miles).Return(mileage2);

            Assert.AreEqual(mileage2, targetCar.Mileage);

            mocks.VerifyAll();

        }
	}
}
