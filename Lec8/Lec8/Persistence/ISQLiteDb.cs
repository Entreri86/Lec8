using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lec8.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetSQLiteAsyncConnection();
        

    }
}
