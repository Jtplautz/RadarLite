using NUnit.Framework;
using Moq;
using RadarLite.Interfaces;
using Microsoft.Extensions.Logging;
using RadarLite.NationalWeatherService.EndPoints;
using System.Collections;
using RadarLite.Database.Models.Entities;
using System;
using System.Threading.Tasks;

namespace RadarLite.NationalWeatherService.Test.Unit;

[NUnit.Framework.TestFixture]
public class NationalWeatherServiceTest 
{
    private Mock<ILocationService> locationService;


    [SetUp]
    public void Init() 
    {
        locationService = new Mock<ILocationService>();
    }

    [Test]
    [TestCaseSource(typeof(LocationEndpointsTestCredential), "LocationEndpointsTestCases")]
    public void testLocationEndpoints(int zip, Location testLocation)
    {

        //ARRANGE
        locationService.Setup(x => x.GetLocationAsync(zip)).Returns(Task.FromResult(testLocation));

        //ACT
        var result = locationService.Object.GetLocationAsync(zip).Result;

        //ASSERT
        Assert.IsNotNull(result);
        Assert.AreEqual(testLocation, result);
        Assert.AreNotEqual("", result.Name);
        Assert.IsTrue(result.Name.Equals("Unhealthy") || result.Name.Equals("Healthy"));

    }
}

public class LocationEndpointsTestCredential 
{
    public static IEnumerable LocationEndpointsTestCases
    {
        get 
        {
            Guid locationEndpointTestGuid1 = Guid.NewGuid();
            int zipcode1 = 12345;
            var locationModelTest1 = new Location()
            {
                Id = locationEndpointTestGuid1,
                Name = "Healthy"
            };

            yield return new TestCaseData(zipcode1, locationModelTest1).SetDescription("Valid Zipcode");

            Guid locationEndpointTestGuid2 = Guid.NewGuid();
            int zipcode2 = 0;
            var locationModelTest2 = new Location()
            {
                Id = locationEndpointTestGuid1,
                Name = "Unhealthy"
            };

            yield return new TestCaseData(zipcode2, locationModelTest2).SetDescription("Invalid Zipcode");
        }
        
    }
}