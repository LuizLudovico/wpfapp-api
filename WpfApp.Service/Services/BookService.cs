using WpfApp.Domain.Entities;
using WpfApp.Domain.Interfaces.Repository;
using WpfApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Service.Services
{
    public class BookService : IBookService
    {
        private IRepository<BookEntity> _repository;

        public BookService(IRepository<BookEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<BookEntity> Get(Guid id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<BookEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<BookEntity> Post(BookEntity book)
        {
            book.Titulo = book.Titulo.ToUpper();
            book.Editora = book.Editora.ToUpper();
            book.Autor = book.Autor.ToUpper();

            return await _repository.InsertAsync(book);
        }

        public async Task<BookEntity> Put(BookEntity book)
        {
            book.Titulo = book.Titulo.ToUpper();
            book.Editora = book.Editora.ToUpper();
            book.Autor = book.Autor.ToUpper();

            return await _repository.UpdateAsync(book);
        }
    }
}
