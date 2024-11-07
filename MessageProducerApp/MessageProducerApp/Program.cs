// See https://aka.ms/new-console-template for more information

using MessageProducerApp;

MessageProducer producer = new();
int i = 500;
while (i < int.MaxValue)
{
    i++;
    Thread.Sleep(3000);
    await producer.ProduceAsync($"Hello my queue {i}");
}