using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.StockTicker
{
	[HubName("stockTickerMini")]
	public class StockTickerHub : Hub
	{
		private readonly global::SignalR.StockTicker.StockTicker _stockTicker;

		public StockTickerHub() : this(StockTicker.Instance)
		{
		}

		public StockTickerHub(global::SignalR.StockTicker.StockTicker stockTicker)
		{
			_stockTicker = stockTicker;
		}

		public IEnumerable<Stock> GetAllStocks() 
		{
			return _stockTicker.GetAllStocks();
		}
	}
}