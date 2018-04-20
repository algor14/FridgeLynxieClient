using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FridgeLynxie.Core.SQLite
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
