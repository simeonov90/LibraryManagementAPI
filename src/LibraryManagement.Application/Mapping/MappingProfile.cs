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
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();
        }
    }
}
