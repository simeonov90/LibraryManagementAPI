using AutoMapper;
using LibraryManagement.Application.Services.Authors.Dtos;
using LibraryManagement.Application.Services.Books.Dtos;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.DateOfBirth,
                opt => opt.MapFrom(src =>
                    src.DateOfBirth.HasValue
                        ? DateOnly.FromDateTime(src.DateOfBirth.Value)
                        : (DateOnly?)null));

            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
