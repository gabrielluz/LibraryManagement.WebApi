using AutoMapper;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;

namespace LibraryManager.Api.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReviewInputDto, Review>()
                .ForPath(e => e.User.Id, opt => opt.MapFrom(d => d.UserId))
                .ForPath(e => e.Id, opt => opt.Ignore())
                .ForPath(e => e.Book, opt => opt.Ignore());

            CreateMap<Review, ReviewOutputDto>()
                .ForPath(e => e.UserId, opt => opt.MapFrom(e => e.User.Id));
            
            CreateMap<BookInputDto, Book>()
                .ForPath(e => e.Id, opt => opt.Ignore());

            CreateMap<Book, BookOutputDto>();

            CreateMap<AddRentalInputDto, Rental>()
                .ForPath(e => e.Id, opt => opt.Ignore())
                .ForPath(e => e.Returned, opt => opt.Ignore())
                .ForPath(e => e.Book.Id, opt => opt.MapFrom(dto => dto.BookId))
                .ForPath(e => e.User.Id, opt => opt.MapFrom(dto => dto.UserId));
            
            CreateMap<Rental, RentalOutputDto>()
                .ForPath(dto => dto.UserId, opt => opt.MapFrom(entity => entity.User.Id))
                .ForPath(dto => dto.BookId, opt => opt.MapFrom(entity => entity.Book.Id));
        
            CreateMap<UserInputDto, User>()
                .ForPath(e => e.Id, opt => opt.Ignore());

            CreateMap<User, UserOutputDto>();
        }
    }
}