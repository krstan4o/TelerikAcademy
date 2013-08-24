using System;
using io.iron.ironmq;

class IronMQSender
{
	static void Main(string[] args)
	{
		Client client = new Client(
			"4fcf860768a0197d11019380",
			"hQObnl0BZpVBxVbLODXCEHgYI4s");
		Queue queue = client.queue("NakovChatQueue");
		Console.WriteLine("Enter messages to be sent to the IronMQ server:");
		while (true)
		{
			string msg = Console.ReadLine();
			queue.push(msg);
			Console.WriteLine("Message sent to the IronMQ server.");
		}
	}
}
