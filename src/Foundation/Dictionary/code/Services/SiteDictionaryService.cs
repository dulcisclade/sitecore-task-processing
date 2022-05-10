using Foundation.DependencyInjection;
using Foundation.Dictionary.Models;
using Foundation.Dictionary.Repositories;
using Foundation.SitecoreExtensions.Extensions;
using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.Dictionary.Services
{
    [Service]
    public class SiteDictionaryService
    {
        public PhraseDictionary GetCurrentSiteDictionary()
        {
            return GetPhraseDictionary(DictionaryRepository.Current.Root);
        }

        private PhraseDictionary GetPhraseDictionary(Item folderItem, PhraseDictionary parentDictionary = null)
        {
            if (!folderItem?.IsDerived(Templates.DictionaryFolder.ID) ?? true)
                return null;

            var childPhraseItems = folderItem.Children.Where(item => item.IsDerived(Templates.DictionaryEntry.ID));
            var childFolderItems = folderItem.Children.Where(item => item.IsDerived(Templates.DictionaryFolder.ID));

            var dictionary = new PhraseDictionary
            {
                Key = folderItem.Key,
                Values = GeneratePhraseCollection(childPhraseItems)
            };

            childFolderItems.Each(folder => GetPhraseDictionary(folder, dictionary));

            parentDictionary?.Children.Add(dictionary);

            return parentDictionary ?? dictionary;
        }



        private SortedDictionary<string, string> GeneratePhraseCollection(IEnumerable<Item> children)
        {
            var dictionary = new SortedDictionary<string, string>();

            foreach (var child in children)
            {
                if (!dictionary.ContainsKey(child.Key))
                    dictionary.Add(child.Key, child.Fields[Templates.DictionaryEntry.Fields.Phrase].Value);
            }

            return dictionary;
        }
    }
}