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

        public Task<List<points>> GetPointsAsync()
        {
            return dbConnection.Table<points>().ToListAsync();
        }
        public Task<List<achievments>> GetAchievmentsAsync(int id)
        {
            return dbConnection.Table<achievments>().ToListAsync();
            //return dbConnection.QueryAsync<achievments>("SELECT * FROM achievments WHERE id = " + id + ";");
        }
    }
}
