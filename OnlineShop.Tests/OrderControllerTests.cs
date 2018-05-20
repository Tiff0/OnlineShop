﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Controllers;
using OnlineShop.Models;
using Xunit;

namespace OnlineShop.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange - create a mock repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            // Arrange - create an empty cart
            Cart cart = new Cart();

            // Arrange - create an order
            Order order = new Order();

            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            ViewResult result = target.Checkout(order) as ViewResult;

            // Assert - check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check that I'm passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_Shipping_Details()
        {
            // Arrange - create a mock order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Arrange - add an error ot the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // Assert - check that the model hasn't been passed stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // Assert - check that I'm passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Check_And_Submit_Order()
        {
            // Arrange - create a mock order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Act - try to checkout
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            // Assert - check that the order has been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            // Assert - check that the method is redirecting to the completed action
            Assert.Equal("Completed", result.ActionName);
        }
    }
}