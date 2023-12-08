using Flower_Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class MementoPattern
    {
        private readonly IMemoryCache _memoryCache;

        public MementoPattern(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Memento StorePost;

        public void BackUpToCache(Memento storedPost)
        {
            _memoryCache.Set("Memento", storedPost, TimeSpan.FromDays(30));
        }
    }
}
