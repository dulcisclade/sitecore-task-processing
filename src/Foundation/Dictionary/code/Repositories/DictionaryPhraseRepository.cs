﻿using System;
using System.Web;
using Foundation.Dictionary.Services;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Foundation.Dictionary.Repositories
{
    public class DictionaryPhraseRepository : IDictionaryPhraseRepository
  {
    public Models.Dictionary Dictionary { get; set; }

    public DictionaryPhraseRepository(Models.Dictionary dictionary)
    {
      this.Dictionary = dictionary;
    }

    public static IDictionaryPhraseRepository Current => GetCurrentFromCacheOrCreate();

    private static IDictionaryPhraseRepository GetCurrentFromCacheOrCreate()
    {
      if (HttpContext.Current != null)
      {
        var repository = HttpContext.Current.Items["DictionaryPhraseRepository.Current"] as IDictionaryPhraseRepository;
        if (repository != null)
        {
          return repository;
        }
      }
      var returnValue = new DictionaryPhraseRepository(DictionaryRepository.Current);

      if (HttpContext.Current != null)
      {
        HttpContext.Current.Items.Add("DictionaryPhraseRepository.Current", returnValue);
      }
      return returnValue;
    }

    public string Get([NotNull] string relativePath, string defaultValue)
    {
      if (relativePath == null)
      {
        throw new ArgumentNullException(nameof(relativePath));
      }
      if (Context.Database == null)
      {
        return defaultValue;
      }

      var dictionaryItem = this.GetOrAutoCreateItem(relativePath, defaultValue);
      if (dictionaryItem == null)
      {
        return defaultValue;
      }

      return dictionaryItem.Fields[Templates.DictionaryEntry.Fields.Phrase].Value ?? defaultValue;
    }

    public Item GetItem([NotNull] string relativePath, string defaultValue = "")
    {
      if (relativePath == null)
      {
        throw new ArgumentNullException(nameof(relativePath));
      }

      var item = this.GetOrAutoCreateItem(relativePath, defaultValue);
      if (item == null)
      {
        Log.Debug($"Could not find the dictionary item for the site '{this.Dictionary.Site.Name}' with the path '{relativePath}'", this);
      }
      return item;
    }

    private Item GetOrAutoCreateItem([NotNull]string relativePath, [CanBeNull]string defaultValue)
    {
      relativePath = AssertRelativePath(relativePath);

      var item = this.Dictionary.Root.Axes.GetItem(relativePath);
      if (item != null)
        return item;

      if (!this.Dictionary.AutoCreate || defaultValue == null)
        return null;
      try
      {
        return CreateDictionaryEntryService.CreateDictionaryEntry(this.Dictionary, relativePath, defaultValue);
      }
      catch (Exception ex)
      {
        Log.Error($"Failed to get or create {relativePath} from the dictionary in site {this.Dictionary.Site.Name}", ex, this);
        return null;
      }
    }

    private static string AssertRelativePath(string relativePath)
    {
      if (relativePath == null)
      {
        throw new ArgumentNullException(nameof(relativePath));
      }

      if (relativePath.StartsWith("/"))
      {
        relativePath = relativePath.Substring(1);
      }
      if (string.IsNullOrWhiteSpace(relativePath))
      {
        throw new ArgumentException("the path is not a valid relative path", nameof(relativePath));
      }
      return relativePath;
    }
  }
}