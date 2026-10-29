using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Models;
using TodoManager.Infrastructure.Repositories;

namespace TodoManager.Application.Services
{
    public class TodoService
    {
        private readonly JsonTodoRepository _repository;

        public TodoService()
        {
            _repository = new JsonTodoRepository();
        }

        public List<TodoItem> GetTodos()
        {
            return _repository.GetAll();
        }

        public void AddTodo(string title, string? description, DateTime dueDate)
        {
            TodoItem? duplicate = _repository.GetAll().Find(todo => todo.Title.Trim().Equals(title.Trim(), StringComparison.OrdinalIgnoreCase));
            if(duplicate != null)
                throw new InvalidOperationException();

            TodoItem todo = new TodoItem(title, description, dueDate);
            _repository.Add(todo);
        }

        public void CompleteTodo(TodoItem todo)
        {
            todo.MarkAsCompleted();
           _repository.Update(todo);
        }

        public void DeleteTodo(TodoItem todo)
        {
            if (todo.DueDate.Date < DateTime.Today)
            {
                throw new InvalidOperationException("due date cannot be in the past");
            }

            if (todo.IsCompleted)
            {
                throw new InvalidOperationException("completed todos may not be deleted");
            }

            _repository.Remove(todo);
        }
    }
}
