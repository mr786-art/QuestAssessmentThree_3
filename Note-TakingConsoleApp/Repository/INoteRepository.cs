using Note_TakingConsoleApp.Note_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_TakingConsoleApp.Repository
{
    internal interface INoteRepository
    {
        void Create(Note note);
        IEnumerable<Note> GetAll();
        Note GetById(int id);
        void Update(Note note);
        void Delete(int id);
    }
}
