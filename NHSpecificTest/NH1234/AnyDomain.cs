namespace NHibernate.Test.NHSpecificTest.NH1234
{
	public class Order
	{
		public long Id { get; set; }
		public IPayment Payment { get; set; }
	}

	public interface IPayment
	{
		long Id { get; set; }
		bool IsSuccessful { get; set; }
		int Amount { get; set; }
	}

	class CreditCardPayment : IPayment
	{
		public long Id { get; set; }
		public bool IsSuccessful { get; set; }
		public int Amount { get; set; }
		public string CardNumber { get; set; }
	}

	class WirePayment : IPayment
	{
		public long Id { get; set; }
		public bool IsSuccessful { get; set; }
		public int Amount { get; set; }
		public string BankAccountNumber { get; set; }
	}
}