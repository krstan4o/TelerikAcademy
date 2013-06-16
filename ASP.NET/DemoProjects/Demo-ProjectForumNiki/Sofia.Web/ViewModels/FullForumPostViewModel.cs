using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sofia.Web.ViewModels
{
    public class FullForumPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime CreationTime { get; set; }
        public IEnumerable<ForumPostAnswerViewModel> Answers { get; set; }
    }
}