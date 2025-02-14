using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konso.Clients.Cms.Domain.Enums
{
    public enum ContentTypes
    {
        // system

        PageComponent = 1,
        InlineHTML = 2,
        InlineHTMLDraft = 3,
        InlineHTMLHistory = 4,
        EmailTemplate = 5,
        InlineMarkdown = 6,
        InlineMarkdownDraft = 7,
        InlineMarkdownHistory = 8,

        // media
        Media = 40,
        Image = 41,
        Video = 42,

        // common
        Review = 50,
        Articles = 51,
        ArticleImage = 52,
        Blog = 53,
        Service = 54,
        Help = 55,
        FAQ = 56,
        Testimonials = 57
    }

}
