using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moq;
using System.Xml.Linq;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
    [TestInitialize]
    public void init()
    {
        string seatBD = Guid.NewGuid().ToString();
        DbContextOptions<WebAPIContext> options = new DbContextOptionsBuilder<WebAPIContext>()
                .UseInMemoryDatabase(databaseName: seatBD)
                .UseLazyLoadingProxies(true)
                .Options;

    }


    [TestMethod]
    public void ReserveSeat()
    {

        var seatService = new Mock<SeatsService>();
        Mock<SeatsController> seatController = new Mock<SeatsController>() { CallBase = true };

    }
}
