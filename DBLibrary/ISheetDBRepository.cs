using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public interface ISheetDBRepository
    {
        Task Create(SheetDTO sheetDTO);
        Task<Sheet> Get(int id);
        Task Approve(int id, decimal amount);
        Task Reopen(int id);
    }
}
