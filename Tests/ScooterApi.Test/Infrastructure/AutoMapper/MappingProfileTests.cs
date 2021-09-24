using System;
using AutoMapper;
using FluentAssertions;
using ScooterApi.Domain.Entities;
using ScooterApi.Infrastructure.AutoMapper;
using ScooterApi.Models.v1;
using Xunit;

namespace ScooterApi.Test.Infrastructure.AutoMapper
{
    public class MappingProfileTests
    {
        private readonly DataFromScooterModel _dataFromScooterModel;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _dataFromScooterModel = new DataFromScooterModel
            {
                Time = DateTime.Now,
                ChargePercent = 60,
                Coordinate = new Coordinate(){X = 50, Y = 50},
                InUse = false,
                ScooterId = 102
            };
        }

        [Fact]
        public void Map_Scooter_Scooter_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Scooter, Scooter>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_DataFromScooterModel_Customer()
        {
            var customer = _mapper.Map<Scooter>(_dataFromScooterModel);

            customer.Time.Should().Be(_dataFromScooterModel.Time);
            customer.ChargePercent.Should().Be(_dataFromScooterModel.ChargePercent);
            customer.CoordinateX.Should().Be(_dataFromScooterModel.Coordinate.X);
            customer.CoordinateY.Should().Be(_dataFromScooterModel.Coordinate.Y);
        }
    }
}