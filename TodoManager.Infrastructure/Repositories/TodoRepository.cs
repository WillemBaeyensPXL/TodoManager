using System;
using System.Collections.Generic;
using System.Text;
using TodoManager.Domain.Models;

namespace TodoManager.Infrastructure.Repositories
{
    public class TodoRepository
    {
        private readonly List<TodoItem> _todos;

        public TodoRepository()
        {
            _todos = new List<TodoItem>();
        }

        public List<TodoItem> GetAll()
        {
            return new List<TodoItem>(_todos);
        }

        public TodoItem Get(int id)
        {
            return _todos.Find(todo => todo.Id == id);
        }

        public void Add(TodoItem item)
        {
            int newId = _todos.Count;
            item.Id = newId;
            _todos.Add(item);
        }

        public bool Remove(TodoItem item)
        {
            return _todos.Remove(item);
        }
    }
}
