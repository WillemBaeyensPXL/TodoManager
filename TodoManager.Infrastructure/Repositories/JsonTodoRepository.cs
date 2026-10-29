using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TodoManager.Domain.Models;

namespace TodoManager.Infrastructure.Repositories
{
    public class JsonTodoRepository
    {
        private const string FILENAME = "Todos.json";
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        private int _nextId = 0;
        public JsonTodoRepository()
        {
            if(!File.Exists(FILENAME))
            {
                File.WriteAllText(FILENAME,"[]");
            }    
        }

        public List<TodoItem> GetAll()
        {
            string jsonText = File.ReadAllText(FILENAME);
            List<TodoItem>? todoList = JsonSerializer.Deserialize<List<TodoItem>>(jsonText);
            return todoList ?? new List<TodoItem>();
        }

        public TodoItem Get(int id)
        {
            List<TodoItem> todos = GetAll();

            TodoItem? existing = todos.Find(todo => todo.Id == id);
            if (existing is null)
                throw new ArgumentException($"Item met id {id} bestaat niet.");

            return existing;
        }

        public void Add(TodoItem todo)
        {
            List<TodoItem> todos = GetAll();
            todo.Id = _nextId;
            _nextId++;
            todos.Add(todo);

            string json = JsonSerializer.Serialize(todos, jsonOptions);
            File.WriteAllText(FILENAME, json);
        }

        public bool Remove(TodoItem todo)
        {
            List<TodoItem> todos = GetAll();
            int result = todos.RemoveAll(ti => ti.Id == todo.Id);

            if(result > 0)
            {
                string json = JsonSerializer.Serialize(todos, jsonOptions);
                File.WriteAllText(FILENAME, json);
            }

            return result > 0;
        }

        public void Update(TodoItem todo)
        {
            List<TodoItem> todos = GetAll();

            todos.RemoveAll(ti => ti.Id == todo.Id);
            todos.Add(todo);

            string json = JsonSerializer.Serialize(todos, jsonOptions);
            File.WriteAllText(FILENAME, json);
        }
    }
}
