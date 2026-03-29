using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Hubs
{
    public class CommunityHub : Hub
    {
        public void SendPost()
        {
            Clients.All.updatePosts();
        }

        public void SendComment(int postId)
        {
            Clients.All.updateComments(postId);
        }
    }
}