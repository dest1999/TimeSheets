using DBLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SheetsController : ControllerBase
    {
        private ISheetDBRepository dB;

        public SheetsController(ISheetDBRepository dBRepository)
        {
            dB = dBRepository;
        }

        [HttpPost]
        public void AddNewSheet([FromQuery] SheetDTO sheet)
        {
            dB.Create(sheet);
        }

        [HttpGet("{id}")]
        public Task<Sheet> Get([FromRoute, Required, Range(1, int.MaxValue)] int id)
        {
            return dB.Get(id);
        }

        [HttpPut("approve")]
        public void Approve([FromQuery, Required, Range(1, int.MaxValue)] int id, decimal amount)
        {
            dB.Approve(id, amount);
        }

        [HttpPut("reopen")]
        public void Reopen([FromQuery, Required, Range(1, int.MaxValue)] int id)
        {
            dB.Reopen(id);
        }

    }
}
