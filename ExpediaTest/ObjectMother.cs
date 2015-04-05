using Expedia;
using System;
using Expedia.Car;


namespace ExpediaTest
{
	public class ObjectMother
	{
		
	}

    public static Car Saab()
    {
        return new Car(7) { Name = "Saab 9-5 Sports Sedan" };
    }

    public static Car BMW()
    {
        return new Car(12) { Name = "1979 BMW 323i German Import" };
    }
}
