using Zaczytani.Application.Book.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Book;

public class BookService(IBookRepository bookRepository)
{
    private readonly IBookRepository _bookRepository = bookRepository;
    //public async Task<IEnumerable<BookDto>> GetAll()
    //{
    //    var books = await _bookRepository.GetAll();

    //    return mapper.Map<IEnumerable<VehicleBrandDto>>(brands);
    //}
}
