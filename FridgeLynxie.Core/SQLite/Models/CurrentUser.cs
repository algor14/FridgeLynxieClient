using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeLynxie.Core.SQLite.Models
{
    public class CurrentUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // its a local id

        public int UserId { get; set; } // this id math with user id in global DB
        public string Name { get; set; }
        public int? NetworkId { get; set; }
    }
}
