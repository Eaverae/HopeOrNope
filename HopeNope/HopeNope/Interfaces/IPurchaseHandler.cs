using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HopeNope.Interfaces
{
	public interface IPurchaseHandler
	{
		string ProductId { get; }
		Task<bool> MakePurchase();

		Task<bool> WasItemPurchased();
	}
}
