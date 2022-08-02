using CCTPAPP.Models.Praga;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCTPAPP.PlatformServices.Sqlite
{
    public interface IPragaTransactionDataRepository
    {
        Task<bool> AddTransactionDataAsync(PragaTransactionData pragaTransactionData);
        Task<bool> DeleteTransactionDataAsync(Guid UidPragaTransactionData);
        Task<bool> DeleteAllTransactionDataAsync();
        Task<List<PragaTransactionData>> GetTransactionsDatasAsync();
    }
}
