using Note_TakingConsoleApp.Note_Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_TakingConsoleApp.Repository
{
    internal class NoteRepository : INoteRepository
    {
        private readonly string _connectionString;

        public NoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Note note)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Notes (Title, Content) VALUES (@Title, @Content)", connection);
                command.Parameters.AddWithValue("@Title", note.Title);
                command.Parameters.AddWithValue("@Content", note.Content);
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<Note> GetAll()
        {
            var notes = new List<Note>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notes", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notes.Add(new Note
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            Content = (string)reader["Content"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = (DateTime)reader["UpdatedAt"]
                        });
                    }
                }
            }
            return notes;
        }
        public Note GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notes WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Note
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            Content = (string)reader["Content"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = (DateTime)reader["UpdatedAt"]
                        };
                    }
                }
            }
            return null;
        }
        public void Update(Note note)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Notes SET Title = @Title, Content = @Content, UpdatedAt = GETDATE() WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", note.Id);
                command.Parameters.AddWithValue("@Title", note.Title);
                command.Parameters.AddWithValue("@Content", note.Content);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Notes WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

    }
}
