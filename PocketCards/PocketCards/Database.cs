using PocketCards.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PocketCards.Services
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Card>().Wait();
        }

        public Task<List<Card>> GetCardsAsync()
        {
            return _database.Table<Card>().ToListAsync();
        }

        public Task<int> AddCardAsync(Card card)
        {
            return _database.InsertAsync(card);
        }

        public Task<int> UpdateCardAsync(Card card)
        {
            return _database.UpdateAsync(card);
        }
    }
}
