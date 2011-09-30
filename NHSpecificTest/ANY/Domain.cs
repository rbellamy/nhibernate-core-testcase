using System;
using System.Collections.Generic;
using System.Linq;

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
		Order Order { get; set; }
	}

	internal abstract class Payment : IPayment
	{
		public abstract int Amount { get; set; }
		public abstract long Id { get; set; }
		public abstract bool IsSuccessful { get; set; }
		public abstract Order Order { get; set; }

		protected void SetSingleValue<TEntity, TItem>(
			TEntity entity, TItem newItem, ICollection<TItem> collection, Action<TItem, TEntity> setOwner)
			where TItem : class
			where TEntity : class
		{
			if (newItem != null && collection.Contains(newItem))
			{
				return;
			}

			foreach (TItem item in collection)
			{
				setOwner(item, null);
			}

			collection.Clear();

			if (newItem != null)
			{
				setOwner(newItem, entity);
				collection.Add(newItem);
			}
		}
	}

	internal class CreditCardPayment : Payment
	{
		private Iesi.Collections.Generic.ISet<Order> orders = new HashedSet<Order>();
		public override int Amount { get; set; }
		public string CardNumber { get; set; }
		public override long Id { get; set; }
		public override bool IsSuccessful { get; set; }

		public override Order Order
		{
			get { return this.orders.FirstOrDefault(); }
			set
			{
				this.SetSingleValue(
					this,
					value,
					this.orders,
					(order, payment) => order.Payment = payment);
			}
		}
	}

	internal class WirePayment : Payment
	{
		private Iesi.Collections.Generic.ISet<Order> orders = new HashedSet<Order>();
		public override int Amount { get; set; }
		public string BankAccountNumber { get; set; }
		public override long Id { get; set; }
		public override bool IsSuccessful { get; set; }

		public override Order Order
		{
			get { return this.orders.FirstOrDefault(); }
			set
			{
				this.SetSingleValue(
					this,
					value,
					this.orders,
					(order, payment) => order.Payment = payment);
			}
		}
	}
}