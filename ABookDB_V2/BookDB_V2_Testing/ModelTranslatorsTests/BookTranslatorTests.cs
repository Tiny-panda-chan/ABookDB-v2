using FakeItEasy;
using FluentAssertions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Models.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.ModelTranslator;
using WebProject.ViewModels.Book;

namespace BookDB_V2_Testing.ModelTranslatorsTests
{
    public class BookTranslatorTests
    {
        private readonly ModelTranslatorBook _mtb;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUrlRepository _urlRepository;

        public BookTranslatorTests()
        {
            _bookRepository = A.Fake<IBookRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _categoryRepository = A.Fake<ICategoryRepository>();
            _authorRepository = A.Fake<IAuthorRepository>();
            _urlRepository = A.Fake<IUrlRepository>();

            _mtb = new ModelTranslatorBook(
                A.Fake<IHttpContextAccessor>(),
                _bookRepository,
                _userRepository,
                _categoryRepository,
                _authorRepository,
                _urlRepository
                );
        }


        [Fact]
        public async Task Index_FillObjectAsync_ReturnsObjAsync()
        {
            //arrange
            var books = A.CollectionOfFake<BookModel>(10);//new List<BookModel>();//A.Fake<IEnumerable<BookModel>>();
            var tstBM = new BookModel()
            {
                Id = 999,
                Name = "Test",
                Description = "Test",
            };
            books.Add(tstBM);
            A.CallTo(() => _bookRepository.GetAllAsync()).Returns(books);

            var cats = A.Fake<IEnumerable<CategoryModel>>();
            A.CallTo(() => _categoryRepository.GetAllAsync()).Returns(cats);
            //act
            var result = await _mtb.FillObjectAsync(new IndexVM());
            //assert
            result.Should().NotBeNull();
            result.BookList.Should().HaveSameCount(books);
            result.BookList.Should().Contain(bl => (bl.Id == tstBM.Id && bl.Title == tstBM.Name));

            result.Categories.Should().HaveSameCount(cats);
            //result.Should().Subject.Should().BeEquivalentTo(arrObj);
        }

        [Fact]
        public async Task Detail_FillObjectAsync_ReturnsObjAsync()
        {
            //arrange
            var fBm = A.Fake<BookModel>();
            A.CallTo(() => _bookRepository.GetByIdAsync(A<int>._)).Returns(fBm);
            //act
            var result = await _mtb.FillObjectAsync(new DetailVM(1));
            //assert
            result.Should().NotBeNull();
            result.Name.Should().Be(fBm.Name);
            result.Description.Should().Be(fBm.Description);
            result.TotalPages.Should().Be(fBm.TotalPages);
            result.CreatedDate.Should().Be(fBm.CreatedOn);

        }

        [Fact]
        public async Task Detail_FillObjectAsync_ReturnsEmptyObjAsync()
        {
            //arrange
            var inp = new DetailVM(0);
            A.CallTo(() => _bookRepository.GetByIdAsync(A<int>._)).Returns(new BookModel());
            //act
            var result = await _mtb.FillObjectAsync(inp);
            //assert
            result.Should().BeEquivalentTo(inp);
            result._id.Should().Be(inp._id);

        }


        [Fact]
        public async Task Create_SaveObjectAsync_ReturnsObj()
        {
            //arrange
            var fBC = A.Fake<CreateVM>();
            fBC.CategoryCreateVM.SelectedCategories = new List<string>(5) { "sdlfkj", "sdioweurj", "OEWIRUJDF", "xcovuo", "qopeipqwpo"};
            fBC.Urls = new List<string>(2) {"sldkfjoewi", "pqpqpqpowucvxi"};

            //act
            var result = await _mtb.SaveObjectAsync(fBC);
            //assert
            A.CallTo(() => _categoryRepository.GetByNameAsync(A<string>._)).MustHaveHappened(fBC.CategoryCreateVM.SelectedCategories.Count, Times.Exactly);
            A.CallTo(() => _urlRepository.Add(A<UrlModel>._)).MustHaveHappened(fBC.Urls.Count, Times.Exactly);
        }
    }
}
