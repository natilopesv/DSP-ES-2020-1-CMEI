using System;

namespace _3_Domain
{
    public class Post
    {
        public string id { get; set; }

        public string idCmei { get; set; }

        public DateTime createdAt { get; set; }

        public string createdBy { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public string imageId { get; set; }
    }
}