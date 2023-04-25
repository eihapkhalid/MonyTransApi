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
        #region dependency injection region
            private readonly TransController _controller;
            private readonly Mock<IBusinessLayer<TbUser>> _mockUsersBusinessLayer;
            private readonly Mock<IBusinessLayer<TbFinancialTransaction>> _mockFinancialTransactionBusinessLayer;
            private readonly Mock<IBusinessLayer<VwTransWacountDetail>> _mockVwTransWacountDetailBusinessLayer;

            public TransControllerTests()
            {
                _mockUsersBusinessLayer = new Mock<IBusinessLayer<TbUser>>();
                _mockFinancialTransactionBusinessLayer = new Mock<IBusinessLayer<TbFinancialTransaction>>();
                _mockVwTransWacountDetailBusinessLayer = new Mock<IBusinessLayer<VwTransWacountDetail>>();
                _controller = new TransController
                    (
                        _mockUsersBusinessLayer.Object,
                        _mockFinancialTransactionBusinessLayer.Object,
                        _mockVwTransWacountDetailBusinessLayer.Object
                    );
            }
        #endregion

        [TestMethod]
        #region GetAllUsers
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<TbUser>
                    {
                        new TbUser { UserId = 1, Username = "User1", Password="123", Email="eihap@gmail.com", CurrentState = 1 },
                        new TbUser { UserId = 2, Username = "User2", Password="777", Email="omerf@gmail.com", CurrentState = 1 }
                    };
            _mockUsersBusinessLayer.Setup(b => b.GetAll()).Returns(expectedUsers);
            // Act
            var result = _controller.Get();

            // Assert
            Assert.AreEqual(expectedUsers, result);
        } 
        #endregion

        [TestMethod]
        #region GetUserById
        public void GetUserById_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new TbUser { UserId = userId, Username = "User1", Password = "123", Email = "eihap@gmail.com", CurrentState = 1 };
            _mockUsersBusinessLayer.Setup(b => b.GetById(userId)).Returns(expectedUser);

            // Act
            var result = _controller.Get(userId);

            // Assert
            Assert.AreEqual(expectedUser, result);
        } 
        #endregion

        [TestMethod]
        #region GetAllTrans
        public void GetAllTrans_ReturnsListOfTransactions()
        {
            // Arrange
            var expectedTransactions = new List<VwTransWacountDetail>
                    {
                        new VwTransWacountDetail { SuserId = 1, RuserId= 2, TransactionId = 3, SaccountBalance = 100, SenderAccountNumber = "125412514254", ReceiverAccountNumber="564874", TransactionAmount=500, TransactionDate= DateTime.Now, SaccountNumber = "125412514254", RaccountNumber="564874", RaccountBalance=120 },
                        new VwTransWacountDetail { SuserId = 1, RuserId= 5, TransactionId = 20, SaccountBalance = 10, SenderAccountNumber = "425857527575", ReceiverAccountNumber="757864", TransactionAmount=300, TransactionDate= DateTime.Now, SaccountNumber = "125412514254", RaccountNumber="564874", RaccountBalance=120  }
                    };
            _mockVwTransWacountDetailBusinessLayer.Setup(b => b.GetAll()).Returns(expectedTransactions);

            // Act
            var result = _controller.GetAllTrans();

            // Assert
            Assert.AreEqual(expectedTransactions, result);
        } 
        #endregion

        [TestMethod]
        #region GetTransByID
        public void GetTransByID_ReturnsTransaction()
        {
            // Arrange
            var transactionId = 1;
            var expectedTransaction = new TbFinancialTransaction { TransactionId = transactionId, TransactionAmount = 100 };
            _mockFinancialTransactionBusinessLayer.Setup(b => b.GetById(transactionId)).Returns(expectedTransaction);

            // Act
            var result = _controller.GetTransID(transactionId);

            // Assert
            Assert.AreEqual(expectedTransaction, result);
        } 
        #endregion

        [TestMethod]
        #region AddsNewUser
        public void Post_AddsNewUser()
        {
            // Arrange
            var newUser = new TbUser { UserId = 1, Username = "User1", Password = "123", Email = "eihap@gmail.com", CurrentState = 1 };

            // Act
            _controller.Post(newUser);

            // Assert
            _mockUsersBusinessLayer.Verify(b => b.Save(newUser), Times.Once);
        } 
        #endregion

    }
}
