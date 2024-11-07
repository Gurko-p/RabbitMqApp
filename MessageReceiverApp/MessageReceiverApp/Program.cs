
using MessageReceiverApp;

MessageReceiver receiver = new();
await receiver.StartListeningToQueue("hello");
