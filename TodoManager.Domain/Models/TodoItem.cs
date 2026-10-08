using System;
using System.Collections.Generic;
using System.Text;

namespace TodoManager.Domain.Models
{
    public class TodoItem
    {
        public TodoItem(string title, string? description, DateTime dueDate)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("empty title not allowed");
            }

            Title = title;
            Description = description;
            DueDate = dueDate;

            IsCompleted = false;
            CompletedAt = null;

        }

        public void MarkAsCompleted()
        {
            if (IsCompleted) return;//do nothing if already completed
            IsCompleted = true;
            CompletedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; }
        public string? Description { get; }
        public DateTime DueDate { get; }
        public bool IsCompleted { get; private set; }
        public DateTime? CompletedAt { get; private set; }


        public override string ToString()
        {
            string status = IsCompleted ? "[Done]" : "[Open]";
            return $"{status} {Title} (due {DueDate:dd/MM/yyyy})";
        }
    }
}
