using Sitecore.Data;

namespace Learn.Foundation.Blogs
{
    public struct Templates
    {
        public struct BlogAPIConfigurations
        {
            public static readonly ID ID = new ID("{0AB1DA55-0874-4CB9-9C2A-FA2A4F1E1A4D}");


            public struct Fields
            {
                public static readonly ID ConnectingUrl = new ID("{D7FC60C0-D20F-4FBD-A911-55C0A9DC7DD6}");
                public static readonly ID ItemsPath = new ID("{8A30E2DB-3F9A-4837-A94A-3B86B497704C}");
                public static readonly ID Parent = new ID("{C522D3E1-900B-4119-A305-036412C66D35}");
                public static readonly ID TemplatePath = new ID("{EC4713B4-9529-4FE7-82B3-5D22F543B685}");
                public static readonly ID OtherProperties = new ID("{11FE7341-55C1-4095-9EE1-D5CB55C840D8}");

            }
        }

        public struct Blog
        {
            public static readonly ID ID = new ID("{C3514240-8897-40E4-B8C1-FB463E1DC350}");


            public struct Fields
            {
                public static readonly ID Image = new ID("{4FC1355F-8B30-4FEF-866C-E80B04C6080F}");
                public static readonly ID Description = new ID("{34478891-D0E4-4300-ACCA-36DA6040EDCD}");
                public static readonly ID Name = new ID("{226C29E0-4C84-4D0C-B0DB-764F50A7B2CB}");
                public static readonly ID Author = new ID("{C763D4F9-A323-4516-BBA1-93D62A4DCA5D}");
                public static readonly ID Title = new ID("{26BAB0BB-4F8D-4623-AA2E-99488C4CC865}");
            }
        }
    }
}