using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR.StockTicker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.StockTicker
{
	public class StockTicker
	{
		// Singleton instance
		private static readonly Lazy<StockTicker> _instance = new Lazy<StockTicker>(() => new StockTicker(GlobalHost.ConnectionManager.GetHubContext<StockTickerHub>().Clients));

		// Using concurrentDictionary negates the need to lock the dictionary when changing values
		private readonly ConcurrentDictionary<string, Stock> _stocks = new ConcurrentDictionary<string, Stock>();

		private readonly object _updateStockPricesLock = new object();

		//stock can go up or down by a percentage of this factor on each change
		private readonly double _rangePercent = .002;

		private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);
		private readonly Random _updateOrNotRandom = new Random();

		private readonly Timer _timer;
		private volatile bool _updatingStockPrices = false;

		private StockTicker(IHubConnectionContext<dynamic> clients)
		{
			Clients = clients;

			_stocks.Clear();
			var stocks = new List<Stock>
			{
				new Stock { Symbol = "MSFT", Price = 30.31m },
				new Stock { Symbol = "APPL", Price = 578.18m },
				new Stock { Symbol = "GOOG", Price = 570.30m }
			};
			stocks.ForEach(stock => _stocks.TryAdd(stock.Symbol, stock));

			_timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

		}

		public static StockTicker Instance => _instance.Value;

		private IHubConnectionContext<dynamic> Clients
		{
			get;
			set;
		}

		public IEnumerable<Stock> GetAllStocks()
		{
			return _stocks.Values;
		}

		private void UpdateStockPrices(object state)
		{
			lock (_updateStockPricesLock)
			{
				if (!_updatingStockPrices)
				{
					_updatingStockPrices = true;

					foreach (var stock in _stocks.Values)
					{
						if (TryUpdateStockPrice(stock))
						{
							BroadcastStockPrice(stock);
						}
					}

					_updatingStockPrices = false;
				}
			}
		}

		private bool TryUpdateStockPrice(Stock stock)
		{
			// Randomly choose whether to update this stock or not
			var r = _updateOrNotRandom.NextDouble();
			if (r > .1)
			{
				return false;
			}

			// Update the stock price by a random factor of the range percent

			// gets a random number that's smaller than the stock price
			var random = new Random((int)Math.Floor(stock.Price));
			// gets a randomly generated amount based on the reangePercent value set above
			var percentChange = random.NextDouble() * _rangePercent;
			// randomly determines if the change should be negative or positive
			var pos = random.NextDouble() > .51;
			// creates the amount to change by as 'change', using the stock price as seed
			var change = Math.Round(stock.Price * (decimal)percentChange, 2);
			// ternary to set 'change' to positive or negative
			change = pos ? change : -change;

			// add the change to the price
			stock.Price += change;
			return true;
		}

		private void BroadcastStockPrice(Stock stock)
		{
			Clients.All.updateStockPrice(stock);
		}
	}

}