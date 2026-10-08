using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Models;
using TodoManager.Application.Services;

namespace TodoManager.Domain.Tests
{
    public class TodoItemTests
    {
        [Fact]
        public void Constructor_EmptyTitle_ThrowsArgumentException()
        {
            // Arrange
            string title = "";
            string? description = null;
            DateTime dueDate = DateTime.Today.AddDays(1);

            // Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                TodoItem item = new TodoItem(title, description, dueDate);
            });
        }

        [Fact]
        public void Constructor_WhitespaceTitle_ThrowsArgumentException()
        {
            // Arrange
            string title = "   ";
            string? description = "Some description";
            DateTime dueDate = DateTime.Today.AddDays(1);

            // Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                TodoItem item = new TodoItem(title, description, dueDate);
            });
        }

        [Fact]
        public void Constructor_NullDescription_DoesNotThrow()
        {
            // Arrange
            string title = "Buy milk";
            string? description = null;
            DateTime dueDate = DateTime.Today.AddDays(1);

            // Act
            TodoItem item = new TodoItem(title, description, dueDate);

            // Assert
            Assert.Null(item.Description);
            Assert.Equal("Buy milk", item.Title);
        }

        [Fact]
        public void MarkAsCompleted_SetsIsCompletedAndCompletedAt()
        {
            // Arrange
            TodoItem item = new TodoItem("Study C#", null, DateTime.Today.AddDays(3));

            // Act
            item.MarkAsCompleted();

            // Assert
            Assert.True(item.IsCompleted);
            Assert.NotNull(item.CompletedAt);
        }

        [Fact]
        public void AddTodo_DuplicateTitle_ThrowsInvalidOperationException()
        {
            // Arrange
            TodoService service = new TodoService();

            service.AddTodo("Buy milk", null, DateTime.Today.AddDays(1));

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                service.AddTodo("  BUY MILK  ", "Duplicate", DateTime.Today.AddDays(2));
            });
        }


    }
}
