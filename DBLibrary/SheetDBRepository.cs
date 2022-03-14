using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class SheetDBRepository : ISheetDBRepository
    {
        private SQLiteDBContext db;
        private Mapper mapper;

        public SheetDBRepository(SQLiteDBContext context)
        {
            db = context;
            MapperConfiguration mapperConfiguration = new(cfg => cfg.CreateMap<SheetDTO, Sheet>());
            mapper = new Mapper(mapperConfiguration);
        }

        public async Task Approve(int id, decimal amount)
        {
            var sheet = Get(id).Result;
            sheet.Approve(amount);
            await db.SaveChangesAsync();
        }

        public async Task Create(SheetDTO sheetDTO)
        {
            var sheet = mapper.Map<Sheet>(sheetDTO);
            sheet.Id = db.Sheets.Count() + 1;
            db.Add(sheet);
            await db.SaveChangesAsync();
        }

        public async Task<Sheet> Get(int id)
        {
            return await db.FindAsync<Sheet>(id);
        }

        public async Task Reopen(int id)
        {
            var sheet = Get(id).Result;
            sheet.Reopen();
            await db.SaveChangesAsync();
        }
    }
}
