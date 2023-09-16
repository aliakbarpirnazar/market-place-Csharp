using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.ChatViewModels
{
    public class ChatMessageViewModel
    {
        public string Sender { get; set; }

        public string Message { get; set; }

        public Guid ChatRoomID { get; set; }

    }
}
