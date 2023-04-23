using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoneyTransfer.ApiControllers;
using Bl;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
namespace UnitTestProject
{

    [TestClass]
    public class TransControllerTests
        {
            private readonly TransController _controller;
            private readonly Mock<IBusinessLayer<TbUser>> _mockUsersBusinessLayer;
            private readonly Mock<IBusinessLayer<TbFinancialTransaction>> _mockFinancialTransactionBusinessLayer;
            private readonly Mock<IBusinessLayer<VwTransWacountDetail>> _mockVwTransWacountDetailBusinessLayer;

            public TransControllerTests()
            {
                _mockUsersBusinessLayer = new Mock<IBusinessLayer<TbUser>>();
                _mockFinancialTransactionBusinessLayer = new Mock<IBusinessLayer<TbFinancialTransaction>>();
                _mockVwTransWacountDetailBusinessLayer = new Mock<IBusinessLayer<VwTransWacountDetail>>();
                _controller = new TransController(
                    _mockUsersBusinessLayer.Object,
                    _mockFinancialTransactionBusinessLayer.Object,
                    _mockVwTransWacountDetailBusinessLayer.Object);
            }

            [TestMethod]
            public void GetAllUsers_ReturnsListOfUsers()
            {
                // Arrange
                var expectedUsers = new List<TbUser>
                    {
                        new TbUser { UserId = 1, Username = "User1" },
                        new TbUser { UserId = 2, Username = "User2" }
                    };
                _mockUsersBusinessLayer.Setup(b => b.GetAll()).Returns(expectedUsers);

                // Act
                var result = _controller.Get();

                // Assert
                Assert.AreEqual(expectedUsers, result);
            }

            [TestMethod]
            public void GetUserById_ReturnsUser()
            {
                // Arrange
                var userId = 1;
                var expectedUser = new TbUser { UserId = userId, Username = "User1" };
                _mockUsersBusinessLayer.Setup(b => b.GetById(userId)).Returns(expectedUser);

                // Act
                var result = _controller.Get(userId);

                // Assert
                Assert.AreEqual(expectedUser, result);
            }

            [TestMethod]
            public void GetAllTrans_ReturnsListOfTransactions()
            {
                // Arrange
                var expectedTransactions = new List<VwTransWacountDetail>
                    {
                        new VwTransWacountDetail { SuserId = 1, SaccountBalance = 100 },
                        new VwTransWacountDetail { SuserId = 2, SaccountBalance = 200 }
                    };
                _mockVwTransWacountDetailBusinessLayer.Setup(b => b.GetAll()).Returns(expectedTransactions);

                // Act
                var result = _controller.GetAllTrans();

                // Assert
                Assert.AreEqual(expectedTransactions, result);
            }

            [TestMethod]
            public void GetTransByID_ReturnsTransaction()
            {
                // Arrange
                var transactionId = 1;
                var expectedTransaction = new VwTransWacountDetail { SuserId = transactionId, SaccountBalance = 100 };
                _mockVwTransWacountDetailBusinessLayer.Setup(b => b.GetById(transactionId)).Returns(expectedTransaction);

                // Act
                var result = _controller.GetTransID(transactionId);

                // Assert
                Assert.AreEqual(expectedTransaction, result);
            }

            [TestMethod]
            public void Post_AddsNewUser()
            {
                // Arrange
                var newUser = new TbUser { UserId = 1, Username = "User1" };

                // Act
                _controller.Post(newUser);

                // Assert
                _mockUsersBusinessLayer.Verify(b => b.Save(newUser), Times.Once);
            }

    }
}
