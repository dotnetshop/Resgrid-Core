﻿using System.Threading;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;

namespace Resgrid.Model.Services
{
	public interface IPaymentProviderService
	{
		/// <summary>
		/// Saves the event asynchronous.
		/// </summary>
		/// <param name="providerEvent">The provider event.</param>
		/// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Task&lt;PaymentProviderEvent&gt;.</returns>
		Task<PaymentProviderEvent> SaveEventAsync(PaymentProviderEvent providerEvent,
			CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Processes the stripe payment asynchronous.
		/// </summary>
		/// <param name="charge">The charge.</param>
		/// <returns>Task&lt;Payment&gt;.</returns>
		Task<Payment> ProcessStripePaymentAsync(Charge charge);

		/// <summary>
		/// Processes the stripe subscription update asynchronous.
		/// </summary>
		/// <param name="stripeSubscription">The stripe subscription.</param>
		/// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Task&lt;Payment&gt;.</returns>
		Task<Payment> ProcessStripeSubscriptionUpdateAsync(Subscription stripeSubscription,
			CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Processes the stripe subscription cancellation asynchronous.
		/// </summary>
		/// <param name="stripeSubscription">The stripe subscription.</param>
		/// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Task&lt;Payment&gt;.</returns>
		Task<Payment> ProcessStripeSubscriptionCancellationAsync(Subscription stripeSubscription,
			CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Processes the stripe subscription refund asynchronous.
		/// </summary>
		/// <param name="stripeCharge">The stripe charge.</param>
		/// <returns>Task&lt;Payment&gt;.</returns>
		Task<Payment> ProcessStripeSubscriptionRefundAsync(Charge stripeCharge);

		/// <summary>
		/// Processes the stripe charge failed asynchronous.
		/// </summary>
		/// <param name="stripeCharge">The stripe charge.</param>
		/// <returns>Task&lt;Payment&gt;.</returns>
		Task<Payment> ProcessStripeChargeFailedAsync(Charge stripeCharge);

		/// <summary>
		/// Creates the stripe session for sub.
		/// </summary>
		/// <param name="departmentId">The department identifier.</param>
		/// <param name="stripeCustomerId">The stripe customer identifier.</param>
		/// <param name="stripePlanId">The stripe plan identifier.</param>
		/// <param name="planId">The plan identifier.</param>
		/// <param name="emailAddress">The email address.</param>
		/// <param name="departmentName">Name of the department.</param>
		/// <returns>Session.</returns>
		Task<Session> CreateStripeSessionForSub(int departmentId, string stripeCustomerId, string stripePlanId, int planId,
			string emailAddress, string departmentName);

		/// <summary>
		/// Creates the stripe session for update.
		/// </summary>
		/// <param name="departmentId">The department identifier.</param>
		/// <param name="stripeCustomerId">The stripe customer identifier.</param>
		/// <param name="emailAddress">The email address.</param>
		/// <param name="departmentName">Name of the department.</param>
		/// <returns>Session.</returns>
		Task<Session> CreateStripeSessionForUpdate(int departmentId, string stripeCustomerId, string emailAddress,
			string departmentName);

		/// <summary>
		/// Processes the stripe checkout completed asynchronous.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Task&lt;Payment&gt;.</returns>
		Task<Payment> ProcessStripeCheckoutCompletedAsync(Session session,
			CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Gets the active stripe subscription.
		/// </summary>
		/// <param name="stripeCustomerId">The stripe customer identifier.</param>
		/// <returns>Subscription.</returns>
		Subscription GetActiveStripeSubscription(string stripeCustomerId);

		/// <summary>
		/// Changes the active subscription.
		/// </summary>
		/// <param name="stripeCustomerId">The stripe customer identifier.</param>
		/// <param name="stripePlanId">The stripe plan identifier.</param>
		/// <returns>Invoice.</returns>
		Invoice ChangeActiveSubscription(string stripeCustomerId, string stripePlanId);

		/// <summary>
		/// Processes the stripe checkout update.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Task&lt;Customer&gt;.</returns>
		Task<Customer> ProcessStripeCheckoutUpdateAsync(Session session,
			CancellationToken cancellationToken = default(CancellationToken));
	}
}
