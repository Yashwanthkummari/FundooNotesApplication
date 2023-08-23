using System;
using System.Collections.Generic;
using System.Text;

namespace FundooSubscriber.Interface
{
    public interface IRabbitMQSubscriber
    {
        void ConsumeMessages();
    }
}
