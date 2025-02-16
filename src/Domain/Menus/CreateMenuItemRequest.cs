﻿namespace Konso.Clients.Cms.Domain.Menus
{
    public class CreateMenuItemRequest<TKey> : IPublishable<TKey> where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }
        public TKey SiteId { get; set; }

        public TKey MenuId { get; set; }

        public string Link { get; set; }

        public string Text { get; set; }

        public string Rel { get; set; }

        public string Target { get; set; }

        public string Alt { get; set; }

        public string ImagePath { get; set; }

        public string ImagePathAlt { get; set; }

        public TKey? PageId { get; set; }
        public bool Publish { get; set; }
        public long? PublishedOn { get; set; }

        public byte Order { get; set; }
    }

}
