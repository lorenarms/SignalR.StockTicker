using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(StockTicker.Startup1cs))]

namespace StockTicker
{
	public class Startup1cs
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
		}
	}
}
