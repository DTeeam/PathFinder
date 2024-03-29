﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Database
    {
        SQLiteAsyncConnection dbConnection;

        public Database()
        {
            dbConnection = DependencyService.Get<IDBInterface>().CreateConnection();
        }

        public Task<List<Point>> GetPointsAsync()
        {
            return dbConnection.Table<Point>().ToListAsync();
        }
        
    }
}
