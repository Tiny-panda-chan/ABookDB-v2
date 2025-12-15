using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IFileRepository
    {
        bool? Add(FileModel model);
        void Delete(FileModel model);
    }
}
