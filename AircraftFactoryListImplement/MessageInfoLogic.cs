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
            List<MessageInfoViewModel> result = source.MessageInfoes
            .Where(rec => model == null || rec.ClientId == model.ClientId)
            .Skip(model.Skip)
            .Take(model.Take)
            .Select(rec => new MessageInfoViewModel
            {
                MessageId = model.MessageId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            })
            .ToList();

            return result;
        }
    }
}
