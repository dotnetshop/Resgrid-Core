﻿using Resgrid.Model.Queue;

namespace Resgrid.Model.Providers
{
	public interface IRabbitOutboundQueueProvider
	{
		bool EnqueueCall(CallQueueItem callQueue);
		bool EnqueueMessage(MessageQueueItem messageQueue);
		bool EnqueueDistributionList(DistributionListQueueItem distributionListQueue);
		bool EnqueueNotification(NotificationItem notificationQueue);
		bool EnqueueShiftNotification(ShiftQueueItem shiftQueueItem);
		bool EnqueueCqrsEvent(CqrsEvent cqrsEvent);
		bool VerifyAndCreateClients();
	}
}
