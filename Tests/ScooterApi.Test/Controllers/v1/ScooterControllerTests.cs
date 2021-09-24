using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScooterApi.Controllers.v1;
using ScooterApi.Domain.Entities;
using ScooterApi.Models.v1;
using ScooterApi.Service.v1.Command;
using ScooterApi.Service.v1.Query;
using Xunit;

namespace ScooterApi.Test.Controllers.v1
{
    public class ScooterControllerTests
    {
        private readonly IMediator _mediator;
        private readonly ScooterController _testee;
        private readonly DataFromScooterModel _dataFromScooterModel;
        private readonly int _id = 10;

        public ScooterControllerTests()
        {
            var mapper = A.Fake<IMapper>();
            _mediator = A.Fake<IMediator>();
            _testee = new ScooterController(mapper, _mediator);

            _dataFromScooterModel = new DataFromScooterModel
            {
                Coordinate = new Coordinate(){X = 60, Y = 65},
                Time = DateTime.Now,
                ChargePercent = 30,
                InUse = false,
                ScooterId = _id
            };
            var scooters = new List<Scooter>
            {
                new()
                {
                    Id = _id,
                    Time = DateTime.Now,
                    ChargePercent = 50,
                    CoordinateX = 50,
                    CoordinateY = 55,
                    InUse = false,
                    ScooterId = 101
                },
                new()
                {
                    Id = 11,
                    Time = DateTime.Now,
                    ChargePercent = 60,
                    CoordinateX = 60,
                    CoordinateY = 60,
                    InUse = false,
                    ScooterId = 102
                }
            };

            A.CallTo(() => mapper.Map<Scooter>(A<Scooter>._)).Returns(scooters.First());
            A.CallTo(() => _mediator.Send(A<CreateScooterCommand>._, default)).Returns(scooters.First());
            A.CallTo(() => _mediator.Send(A<UpdateScooterCommand>._, default)).Returns(scooters.First());
            A.CallTo(() => _mediator.Send(A<GetScootersQuery>._, default)).Returns(scooters);
        }

        [Theory]
        [InlineData("CreateScooterAsync: scooter is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateScooterCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Scooter(_dataFromScooterModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }
        

        [Fact]
        public async void Get_ShouldReturnScooters()
        {
            var result = await _testee.Scooters();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<Scooter>>();
            result.Value.Count.Should().Be(2);
        }

        [Theory]
        [InlineData("Scooters could not be loaded")]
        public async void Get_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<GetScootersQuery>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.Scooters();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Post_ShouldReturnScooter()
        {
            var result = await _testee.Scooter(_dataFromScooterModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.OK);
            result.Value.Should().BeOfType<Scooter>();
            result.Value.Id.Should().Be(_id);
        }
    }
}