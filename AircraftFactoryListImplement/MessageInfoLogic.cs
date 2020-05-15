using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryListImplement
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly DataListSingleton source;

        public MessageInfoLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Create(MessageInfoBindingModel model)
        {
            for (int i = 0; i < source.MessageInfoes.Count; ++i)
            {
                if (source.MessageInfoes[i].MessageId == model.MessageId)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }

            source.MessageInfoes.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            });
        }

        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();

            foreach (var message in source.MessageInfoes)
            {
                if (message.MessageId.Equals(model.MessageId))
                {
                    result.Add(new MessageInfoViewModel
                    {
                        MessageId = message.MessageId,
                        SenderName = message.SenderName,
                        DateDelivery = message.DateDelivery,
                        Subject = message.Subject,
                        Body = message.Body
                    });
                }
            }

            return result;
        }
    }
}
