using System;

namespace RabbitMQ.Interfaces
{
    public interface IRabbitMQService
    { 
        void Publish<T>(T message, string queueName);
        void Subscribe(string queueName, Action<string> handleMessage);
    }
}
