using Iesi.Collections.Generic;

namespace NHibernate.Test.NHSpecificTest.ANY
{
	public class Order
	{
		public long Id { get; set; }
		public IPayment Payment { get; set; }
	}

	public interface IPayment
	{
		int Amount { get; set; }
		long Id { get; set; }
		bool IsSuccessful { get; set; }
		ISet<Order> Orders { get; set; }
	}

	internal class CreditCardPayment : IPayment
	{
		public int Amount { get; set; }
		public string CardNumber { get; set; }
		public long Id { get; set; }
		public bool IsSuccessful { get; set; }
		public ISet<Order> Orders { get; set; }
	}

	internal class WirePayment : IPayment
	{
		public int Amount { get; set; }
		public string BankAccountNumber { get; set; }
		public long Id { get; set; }
		public bool IsSuccessful { get; set; }
		public ISet<Order> Orders { get; set; }
	}
}