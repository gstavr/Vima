using Vimachem.Models.Domain;

namespace Vimachem.Data
{
    public static class BookStore
    {

        public static List<Book> bookList =
        [
            new () {Id = 1, Name = "Book One", CategoryId = 1},
            new () {Id = 2, Name = "Book Two", CategoryId = 2},
            new () {Id = 3, Name = "Book Three", CategoryId = 3},
            new () {Id = 4, Name = "Book Four", CategoryId = 4},
            new () {Id = 5, Name = "Book Five", CategoryId = 5}
        ];
    }
}
