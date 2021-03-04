using AutoMapper;
using Payment.Models.Domain_Models;
using Payment.Models.Dto_s;

namespace PaymentApplication.WebApi.Mappers
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            // Default mapping when property names are same
            CreateMap<PaymentModel, PaymentDto>()
            .ReverseMap();
            // Default mapping when property names are same
            CreateMap<PaymentState, PaymentStateDto>()
                .ReverseMap();
        }
    }
}
