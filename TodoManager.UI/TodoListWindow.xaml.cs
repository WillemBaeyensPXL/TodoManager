using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TodoManager.Application.Services;
using TodoManager.Domain.Models;

namespace TodoManager.WPF
{
    /// <summary>
    /// Interaction logic for TodoListWindow.xaml
    /// </summary>
    public partial class TodoListWindow : Window
    {
        private readonly TodoService _todoService;
        public TodoListWindow()
        {
            InitializeComponent();

            _todoService = new TodoService();
            RefreshTodos();
        }

        private void RefreshTodos()
        {
            todosListBox.Items.Clear();

            List<TodoItem> todos = _todoService.GetTodos();

            foreach (TodoItem todo in todos)
            {
                todosListBox.Items.Add(todo); // relies on TodoItem.ToString()
            }

            ClearDetails();
        }

        private void ClearDetails()
        {
            titleTextBlock.Text = "";
            dueDateTextBlock.Text = "";
            completedTextBlock.Text = "";
            completedAtTextBlock.Text = "";
            descriptionTextBlock.Text = "";
        }

        private void TodosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(todosListBox.SelectedItem is TodoItem todoItem)
            {
                titleTextBlock.Text = todoItem.Title;
                dueDateTextBlock.Text = todoItem.DueDate.ToLongDateString();
                completedTextBlock.Text = todoItem.IsCompleted ? "yes" : "no";
                completedAtTextBlock.Text = todoItem.CompletedAt.ToString();
                descriptionTextBlock.Text = todoItem.Description;
            }
            else
            {
                ClearDetails();
                return;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTodoWindow addWindow = new AddTodoWindow();
            addWindow.Owner = this;

            bool? result = addWindow.ShowDialog();
            if (result != true)
            {
                return;
            }

            // TODO: try-catch
            // TODO: _todoService.AddTodo(addWindow.TodoTitle, addWindow.TodoDescription, addWindow.TodoDueDate);
            // TODO: RefreshTodos();
            try
            {
                _todoService.AddTodo(addWindow.TodoTitle, addWindow.TodoDescription, addWindow.TodoDueDate);
                RefreshTodos();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            TodoItem selectedTodo = todosListBox.SelectedItem as TodoItem;
            if (selectedTodo is null)
            {
                MessageBox.Show("Geen todo geselecteerd.");
                return;
            }

            try
            {
                _todoService.CompleteTodo(selectedTodo);
                RefreshTodos();
                todosListBox.SelectedItem = selectedTodo;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TodoItem selectedTodo = todosListBox.SelectedItem as TodoItem;
            if (selectedTodo is null)
            {
                MessageBox.Show("Geen todo geselecteerd.");
                return;
            }

            try
            {
                _todoService.DeleteTodo(selectedTodo);
                RefreshTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshTodos();
        }
    }
}
