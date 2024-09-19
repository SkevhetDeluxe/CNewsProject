using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    public class ExchangeRateController : Controller
    {
        private readonly TableClient _tableClient;

        public ExchangeRateController(TableServiceClient tableServiceClient)
        {
            _tableClient = tableServiceClient.GetTableClient("ExchangeRateArchived");
        }

        public async Task<IActionResult> History()
        {
            var exchangeRateHistory = new List<ExchangeRateHistoryViewModel>();

            await foreach (var entity in _tableClient.QueryAsync<TableEntity>())
            {
                exchangeRateHistory.Add(new ExchangeRateHistoryViewModel
                {
                    Date = DateTime.Parse(entity.RowKey),  // Assuming the RowKey is the date
                    USD = (decimal)(entity.GetDouble("USD") ?? 0),  // Convert nullable double to decimal
                    EUR = (decimal)(entity.GetDouble("EUR") ?? 0),
                    GBP = (decimal)(entity.GetDouble("GBP") ?? 0)
                });
            }

            return View(exchangeRateHistory);
        }
    }
}
