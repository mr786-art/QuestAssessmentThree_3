using Note_TakingConsoleApp.Note_Model;
using Note_TakingConsoleApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_TakingConsoleApp
{
    internal class Program
    {
        private static string connectionString = "Server=MUDASIR-DELL\\SQLEXPRESS01;Database=NoteTakingApp;Trusted_Connection=True;"; 
        private static INoteRepository noteRepository = new NoteRepository(connectionString);

        public Program()
        {
            noteRepository = new NoteRepository(connectionString);
        }

        public void Operations()
        {
            while (true)
            {
                Console.WriteLine("1. Create a new note");
                Console.WriteLine("2. View all notes");
                Console.WriteLine("3. Update an existing note");
                Console.WriteLine("4. Delete a note");
                Console.WriteLine("5. Exit");
                var options = Console.ReadLine();

                switch (options)
                {
                    case "1":
                        CreateNote();
                        break;
                    case "2":
                        ViewAllNotes();
                        break;
                    case "3":
                        UpdateNote();
                        break;
                    case "4":
                        DeleteNote();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.Please try again.");
                        break;
                }
            }
        }
        private void CreateNote()
        {
            Console.Write("Enter note title: ");
            var title = Console.ReadLine();
            Console.Write("Enter note content: ");
            var content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Title and content cannot be empty.");
                return;
            }

            var note = new Note { Title = title, Content = content };
            noteRepository.Create(note);
            Console.WriteLine("Note created successfully.");
        }
        private void ViewAllNotes()
        {
            var notes = noteRepository.GetAll();
            foreach (var note in notes)
            {
                Console.WriteLine($"ID: {note.Id}, Title: {note.Title}, Created At: {note.CreatedAt}");
            }
        }
        private void UpdateNote()
        {
            Console.Write("Enter note ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var note = noteRepository.GetById(id);
                if (note == null)
                {
                    Console.WriteLine("Note not found.");
                    return;
                }

                Console.WriteLine($"Current Title: {note.Title}");
                Console.Write("Enter new title (leave empty to keep current): ");
                var newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    note.Title = newTitle;
                }
                Console.WriteLine($"Current Content: {note.Content}");
                Console.Write("Enter new content (leave empty to keep current): ");
                var newContent = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newContent))
                {
                    note.Content = newContent;
                }

                noteRepository.Update(note);
                Console.WriteLine("Note updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
        private void DeleteNote()
        {
            Console.Write("Enter note ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                noteRepository.Delete(id);
                Console.WriteLine("Note deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Operations();

        }
    }
}
