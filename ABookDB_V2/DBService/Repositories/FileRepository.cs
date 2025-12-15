using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repositories
{
    public class FileRepository : Repository<FileModel>, Models.Interfaces.IFileRepository
    {
        public FileRepository(ABookDBContext context) : base(context)
        {
        }

    }
}
