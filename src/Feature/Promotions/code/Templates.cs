using Sitecore.Data;

namespace Learn.Feature.Promotions
{
    public struct Templates
    {
        public struct Carousel
        {
            public static readonly ID ID = new ID("{3EA0BC99-CD31-4D17-B47E-06F93493D93F}");


            public struct Fields
            {
                public static readonly ID Image = new ID("{E66FCB8E-42E9-4115-892B-E1BD89FE55AF}");
                public static readonly ID Title = new ID("{89EA0638-2040-41A4-BFD7-6036433B919B}");
                public static readonly ID Description = new ID("{3D594FA6-78E0-44D2-86EB-1836EDD53D89}");
                public static readonly ID Link = new ID("{9D8D60F8-343A-4F66-B699-8CCF3E80F1F5}");
            }
        }
    }
}