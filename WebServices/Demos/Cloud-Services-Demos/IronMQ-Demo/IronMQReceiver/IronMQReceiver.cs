using System;
using io.iron.ironmq;
using io.iron.ironmq.Data;
using System.Threading;

class IronMQReceiver
{
	static void Main()
	{
		Client client = new Client(
			"4fcf860768a0197d11019380",
			"hQObnl0BZpVBxVbLODXCEHgYI4s");
		Queue queue = client.queue("NakovChatQueue");
		Console.WriteLine("Listening for new messages from IronMQ server:");
		while (true)
		{
			Message msg = queue.get();
			if (msg != null)
			{
				Console.WriteLine(msg.Body);
                queue.deleteMessage(msg);
			}
			Thread.Sleep(100);
		}
	}
}
