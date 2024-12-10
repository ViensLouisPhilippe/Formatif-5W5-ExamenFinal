using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
  
    [TestMethod]
    public void ReserveSeat()
    {
        // Arrange
        var expectedSeat = new Seat { Id = 1, Number = 1 };
        var userId = "1";
        var seatId = 1;

        var seatServiceMock = new Mock<SeatsService>();
        var seatController = new Mock<SeatsController>(seatServiceMock.Object) { CallBase = true };
       
        seatServiceMock
            .Setup(service => service.ReserveSeat(userId, seatId))
            .Returns(expectedSeat); 

        seatController.Setup(t => t.UserId).Returns("1");

       
        var actionResult = seatController.Object.ReserveSeat(seatId).Result;
        var result = actionResult as OkObjectResult;

       
       
        Assert.AreSame(expectedSeat, result.Value);
    }
    [TestMethod]
    public void unauthorizedReserveSeat()
    {
        var seatUser1 = new Seat { Id = 1, Number = 1, ExamenUserId = "1" };
        var userId1 = "1";
        var userId2 = "2";

        var seatServiceMock = new Mock<SeatsService>();
        var seatController = new Mock<SeatsController>(seatServiceMock.Object) { CallBase = true };

        seatServiceMock
            .Setup(service => service.ReserveSeat(userId1 , seatUser1.Id))
            .Returns(seatUser1);


        seatController.Setup(t => t.UserId).Returns("2");
        
        seatServiceMock
            .Setup(service => service.ReserveSeat(userId2, seatUser1.Id))
            .Throws(new SeatAlreadyTakenException());

        var actionResult = seatController.Object.ReserveSeat(seatUser1.Id).Result;
        
        Assert.IsInstanceOfType(actionResult, typeof(UnauthorizedResult));

    }

    [TestMethod]
    public void limitSeats()
    {
       
        var userId1 = "1";
        int seat101 = 101;

        var seatServiceMock = new Mock<SeatsService>();
        var seatController = new Mock<SeatsController>(seatServiceMock.Object) { CallBase = true };


        seatServiceMock
           .Setup(service => service.ReserveSeat(userId1, seat101))
           .Throws(new SeatOutOfBoundsException());

        seatController.Setup(t => t.UserId).Returns("1");
        var actionResult = seatController.Object.ReserveSeat(seat101).Result as ObjectResult;
        Assert.IsNotNull(actionResult);
        Assert.AreEqual("Could not find " + seat101, actionResult.Value);

    }
    [TestMethod]
    public void alreadyHasASeat()
    {

        var userId1 = "1";
        int seat101 = 101;

        var seatServiceMock = new Mock<SeatsService>();
        var seatController = new Mock<SeatsController>(seatServiceMock.Object) { CallBase = true };


        seatServiceMock
           .Setup(service => service.ReserveSeat(userId1, seat101))
           .Throws(new BadRequest());

        seatController.Setup(t => t.UserId).Returns("1");
        var actionResult = seatController.Object.ReserveSeat(seat101).Result as ObjectResult;

        Assert.AreEqual("Could not find " + seat101, actionResult.Value);

    }


}
