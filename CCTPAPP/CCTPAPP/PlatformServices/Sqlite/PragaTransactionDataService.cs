using CCTPAPP.Models.Praga;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCTPAPP.PlatformServices.Sqlite
{
    public class PragaTransactionDataService : IPragaTransactionDataRepository
    {
        public SQLiteAsyncConnection _database;
        public PragaTransactionDataService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<PragaTransactionData>().Wait();
        }


        public async Task<bool> AddTransactionDataAsync(PragaTransactionData pragaTransactionData)
        {
            if (pragaTransactionData.UidPragaTransactionData == null)
            {
                await _database.UpdateAsync(pragaTransactionData);
            }
            else
            {
                await _database.InsertAsync(pragaTransactionData);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteTransactionDataAsync(Guid UidPragaTransactionData)
        {
            await _database.DeleteAsync<PragaTransactionData>(UidPragaTransactionData);
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteAllTransactionDataAsync()
        {
            await _database.DeleteAllAsync<PragaTransactionData>();
            return await Task.FromResult(true);
        }
        public async Task<List<PragaTransactionData>> GetTransactionsDatasAsync()
        {
            return await Task.FromResult(await _database.Table<PragaTransactionData>().ToListAsync());
        }
    }
}
