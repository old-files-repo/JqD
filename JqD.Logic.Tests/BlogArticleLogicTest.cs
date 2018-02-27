using System;
using FluentAssertions;
using JqD.Command.Work;
using JqD.Common;
using JqD.Data.CodeSection;
using JqD.Data.ShareModels;
using JqD.Data.UnitOfWork;
using JqD.Entities;
using JqD.Infrustruct.Enums;
using JqD.IRepository;
using Moq;
using NUnit.Framework;

namespace JqD.Logic.Tests
{
    [TestFixture]
    public class BlogArticleLogicTest
    {
        [Test]
        public void Add_blogarticle_when_title_is_having_and_content_is_having_should_add_a_record_blogarticle()
        {
            //Arrange
            var articleRepositoryMocker = new Mock<IBlogArticleRepository>();
            var currentTimeProviderMocker = new Mock<ICurrentTimeProvider>();
            var unitOfWorkFactoryMocker = new Mock<IUnitOfWorkFactory>();
            var work = new AddWorkCommand
            {
                Title="1",
                Category= Enums.Category.Web前段,
                Content="2"
            };

            LoginUserSection.Start(new LoginUserInformationForCodeSection
            {
                LoginName ="yzuhao"
            });

            var datetime = new DateTime(2018, 01, 01);
            currentTimeProviderMocker.Setup(x => x.CurrentTime()).Returns(datetime);

            var blogArticleLogic = new BlogArticleLogic(articleRepositoryMocker.Object,
                currentTimeProviderMocker.Object, unitOfWorkFactoryMocker.Object);

            //Act
            blogArticleLogic.Add(work);

            //Assert
            articleRepositoryMocker.Verify(x=>x.Add(It.IsAny<BlogArticle>()),Times.Once);
        }

        [Test]
        public void Add_blogarticle_when_title_is_none_and_content_is_having_should_throw_exception()
        {
            //Arrange
            var articleRepositoryMocker = new Mock<IBlogArticleRepository>();
            var currentTimeProviderMocker = new Mock<ICurrentTimeProvider>();
            var unitOfWorkFactoryMocker = new Mock<IUnitOfWorkFactory>();
            var work = new AddWorkCommand
            {
                Title = "",
                Category = Enums.Category.Web前段,
                Content = "2"
            };

            var blogArticleLogic = new BlogArticleLogic(articleRepositoryMocker.Object,
                currentTimeProviderMocker.Object, unitOfWorkFactoryMocker.Object);

            //Act
            //Assert
            Assert.Throws<LogicException>(() => blogArticleLogic.Add(work));
        }

        [Test]
        public void Add_blogarticle_when_title_is_having_and_content_is_none_should_throw_exception()
        {
            //Arrange
            var articleRepositoryMocker = new Mock<IBlogArticleRepository>();
            var currentTimeProviderMocker = new Mock<ICurrentTimeProvider>();
            var unitOfWorkFactoryMocker = new Mock<IUnitOfWorkFactory>();
            var work = new AddWorkCommand
            {
                Title = "1",
                Category = Enums.Category.Web前段,
                Content = ""
            };

            var blogArticleLogic = new BlogArticleLogic(articleRepositoryMocker.Object,
                currentTimeProviderMocker.Object, unitOfWorkFactoryMocker.Object);

            //Act
            //Assert
            Assert.Throws<LogicException>(() => blogArticleLogic.Add(work));
        }

        [Test]
        public void LogicDelete_blogarticle_when_need_not_should_delete_this_record_by_id()
        {
            //Arrange
            var articleRepositoryMocker = new Mock<IBlogArticleRepository>();
            var currentTimeProviderMocker = new Mock<ICurrentTimeProvider>();
            var unitOfWorkFactoryMocker = new Mock<IUnitOfWorkFactory>();
            const int id = 6;

            LoginUserSection.Start(new LoginUserInformationForCodeSection
            {
                LoginName = "yzuhao"
            });

            var datetime = new DateTime(2018, 01, 01);
            currentTimeProviderMocker.Setup(x => x.CurrentTime()).Returns(datetime);

            var info = new BlogArticle
            {
                Title = "1",
                Category = Enums.Category.Web前段,
                Content = "2",
                CreateUser = "123",
                CreateDate = datetime
            };
            articleRepositoryMocker.Setup(x => x.Get(It.IsAny<int>())).Returns(info);

            unitOfWorkFactoryMocker.Setup(x => x.GetCurrentUnitOfWork().Commit());

            var blogArticleLogic = new BlogArticleLogic(articleRepositoryMocker.Object,
                currentTimeProviderMocker.Object, unitOfWorkFactoryMocker.Object);

            //Act
            blogArticleLogic.LogicDelete(id);

            //Assert
            info.Title.Should().Be("1");
            info.Status.Should().Be(Enums.Status.Delete);
            info.EditUser.Should().Be("yzuhao");
            info.EditDate.Should().Be(datetime);
        }

        [Test]
        public void Update_blogarticle_when_title_is_having_and_content_is_having_should_update_blogarticle()
        {
            //Arrange
            var articleRepositoryMocker = new Mock<IBlogArticleRepository>();
            var currentTimeProviderMocker = new Mock<ICurrentTimeProvider>();
            var unitOfWorkFactoryMocker = new Mock<IUnitOfWorkFactory>();
            var work = new UpdateWorkCommand
            {
                Id=6,
                Title = "3",
                Category = Enums.Category.Web前段,
                Content = "3"
            };

            LoginUserSection.Start(new LoginUserInformationForCodeSection
            {
                LoginName = "yzuhao"
            });

            var datetime = new DateTime(2018, 01, 01);
            currentTimeProviderMocker.Setup(x => x.CurrentTime()).Returns(datetime);

            var info = new BlogArticle
            {
                Id = 6,
                Title = "1",
                Category = Enums.Category.Web前段,
                Content = "2",
                CreateUser = "123",
                CreateDate = datetime
            };
            articleRepositoryMocker.Setup(x => x.Get(It.IsAny<int>())).Returns(info);

            var blogArticleLogic = new BlogArticleLogic(articleRepositoryMocker.Object,
                currentTimeProviderMocker.Object,unitOfWorkFactoryMocker.Object);

            //Act
            blogArticleLogic.Update(work);

            //Assert
            info.Title.Should().Be("3");
            info.Category.Should().Be(Enums.Category.Web前段);
            info.Content.Should().Be("3");
        }

        [Test]
        public void Update_blogarticle_when_title_is_none_and_content_is_having_should_throw_exception()
        {
            //Arrange
            var articleRepositoryMocker = new Mock<IBlogArticleRepository>();
            var currentTimeProviderMocker = new Mock<ICurrentTimeProvider>();
            var unitOfWorkFactoryMocker = new Mock<IUnitOfWorkFactory>();
            var work = new UpdateWorkCommand
            {
                Id = 6,
                Title = "",
                Category = Enums.Category.Web前段,
                Content = "3"
            };

            var blogArticleLogic = new BlogArticleLogic(articleRepositoryMocker.Object,
                currentTimeProviderMocker.Object,unitOfWorkFactoryMocker.Object);

            //Act
            //Assert
            Assert.Throws<LogicException>(() => blogArticleLogic.Update(work));
        }

        [Test]
        public void Update_blogarticle_when_title_is_having_and_content_is_none_should_throw_exception()
        {
            //Arrange
            var articleRepositoryMocker = new Mock<IBlogArticleRepository>();
            var currentTimeProviderMocker = new Mock<ICurrentTimeProvider>();
            var unitOfWorkFactoryMocker = new Mock<IUnitOfWorkFactory>();
            var work = new UpdateWorkCommand
            {
                Id = 6,
                Title = "3",
                Category = Enums.Category.Web前段,
                Content = ""
            };

            var blogArticleLogic = new BlogArticleLogic(articleRepositoryMocker.Object,
                currentTimeProviderMocker.Object,unitOfWorkFactoryMocker.Object);

            //Act
            //Assert
            Assert.Throws<LogicException>(() => blogArticleLogic.Update(work));
        }
    }
}
