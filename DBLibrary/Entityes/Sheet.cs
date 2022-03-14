using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class Sheet
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Closed { get; set; }
        public bool IsApproved { get; set; }
        public int User { get; set; }
        public int Employee { get; set; }
        public decimal Amount { get; set; }

        public Sheet()
        {

        }

        private Sheet(int user, int employee, string description)
        {
            Created = DateTime.Now;
            User = user;
            Employee = employee;
            Description = description;
        }

        public static Sheet Create(int userOwner, int employee, string description)
        {
            return new Sheet(userOwner, employee, description);
        }

        public void Approve(decimal amount)
        {
            Amount = amount;
            Closed = DateTime.Now;
            IsApproved = true;
        }

        public void Reopen()
        {
            Amount = 0;
            Closed = null;
            IsApproved = false;
        }

    }
}
