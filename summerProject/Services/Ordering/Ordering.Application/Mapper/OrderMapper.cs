using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ordering.Application.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper() {

            CreateMap<OrderId, Guid>().ConvertUsing(src => src.Value);
            CreateMap<CustomerId, Guid>().ConvertUsing(src => src.Value);
            CreateMap<ProductId, Guid>().ConvertUsing(src => src.Value);

            CreateMap<Order, OrderDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(des => des.CustomerId, opt => opt.MapFrom(src => src.CustomerId.Value));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(des => des.ProductId, opt => opt.MapFrom(src => src.ProductId.Value))
                .ForMember(des => des.OrderId, opt => opt.MapFrom(src => src.OrderId.Value));

            CreateMap<OrderDto, Order>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => OrderId.Of(src.Id)))
                .ForMember(des => des.CustomerId, opt => opt.MapFrom(src => CustomerId.Of(src.CustomerId)))
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore()); ;

            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(des => des.ProductId, opt => opt.MapFrom(src => ProductId.Of(src.ProductId)))
                .ForMember(des => des.OrderId, opt => opt.MapFrom(src => OrderId.Of(src.OrderId)));
                

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();

        }
    }
}
