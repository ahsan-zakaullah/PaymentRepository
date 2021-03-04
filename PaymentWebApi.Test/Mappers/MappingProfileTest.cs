using System;
using AutoMapper;
using FluentAssertions;
using Payment.Models.Common;
using Payment.Models.Domain_Models;
using Payment.Models.Dto_s;
using PaymentApplication.WebApi.Mappers;
using Xunit;

namespace PaymentWebApi.Test.Mappers
{
    public class MappingProfileTests
    {
        private readonly PaymentDto _createPaymentModel;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _createPaymentModel = new PaymentDto
            {
                Id = 1,
                CreditCardNumber = "5105105105105100",
                CardHolder = "Ahsan Raza",
                ExpirationDate = DateTime.Now,
                SecurityCode = 123,
                Amount = 300,
                PaymentStateDto = new PaymentStateDto{PaymentStatus =(int) StatusEnum.Processed}
            };
        }

        [Fact]
        public void Map_Payment_Payment_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<PaymentModel, PaymentModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreatePaymentModel_Payment()
        {
            var payment = _mapper.Map<PaymentModel>(_createPaymentModel);
            payment.CreditCardNumber.Should().Be(_createPaymentModel.CreditCardNumber);
            payment.CardHolder.Should().Be(_createPaymentModel.CardHolder);
            payment.ExpirationDate.Should().Be(_createPaymentModel.ExpirationDate);
            payment.SecurityCode.Should().Be(_createPaymentModel.SecurityCode);
            payment.Amount.Should().Be(_createPaymentModel.Amount);
        }
    }
}