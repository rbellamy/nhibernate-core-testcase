using NHibernate.Dialect;

using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.ANY
{
	[TestFixture]
	public class Test : BugTestCase
	{
		[Test]
		public void PaymentTypeShouldBeCorrectWithCorrectData()
		{
			using (ISession session = this.OpenSession())
			{
				Order entity = session.Get<Order>(1L);
				Assert.That(entity.Payment, Is.InstanceOf<CreditCardPayment>());
				IPayment payment = entity.Payment;
				Assert.That(payment.IsSuccessful, Is.True);
				Assert.That(payment.Amount, Is.EqualTo(5));
				Assert.That(payment.Orders.Contains(entity), Is.True);
			}
		}

		protected override bool AppliesTo(Dialect.Dialect dialect)
		{
			return dialect as MsSql2005Dialect != null;
		}

		protected override void OnSetUp()
		{
			base.OnSetUp();
			using (ISession session = this.OpenSession())
			{
				Order entity = new Order
				{
					Payment = new CreditCardPayment
					{
						Amount = 5,
						CardNumber = "1234",
						IsSuccessful = true
					}
				};

				session.Save(entity);
				session.Flush();
			}
		}

		protected override void OnTearDown()
		{
			base.OnTearDown();
			using (ISession session = this.OpenSession())
			{
				string hql = "from System.Object";
				session.Delete(hql);
				session.Flush();
			}
		}
	}
}