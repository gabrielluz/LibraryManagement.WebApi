using AutoMapper;
using LibraryManagerApi.Models.Dto;
using LibraryManagerApi.Models.Entities;

namespace LibraryManagerApi.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReviewInputDto, Review>()
                .ForPath(e => e.User, opt => opt.Ignore())
                .ForPath(e => e.Book, opt => opt.Ignore());
            CreateMap<Review, ReviewOutputDto>();
            CreateMap<Book, BookInputDto>();
            CreateMap<Book, BookOutputDto>();
            CreateMap<Rental, RentalInputDto>();
            CreateMap<Rental, RentalOutputDto>()
                .ForPath(dto => dto.UserId, opt => opt.MapFrom(entity => entity.User.Id));
        }
    }
}