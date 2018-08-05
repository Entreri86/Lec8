using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lec8.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace Lec8.Persistence
{
    class SQLiteDb : ISQLiteDb
    {
        
        public SQLiteAsyncConnection GetSQLiteAsyncConnection()
        {
            var DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(DocumentsPath, "MySQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}
