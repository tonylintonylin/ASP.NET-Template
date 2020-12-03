using ASP.NET.TEMPLATE.Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NET.TEMPLATE
{
    #region Interface

    public interface ICache
    {
        Dictionary<string, MetaType> MetaTypes { get; }

        string ThingAIcon { get; }
        string ThingBIcon { get; }
        string ThingCIcon { get; }
        string ThingDIcon { get; }
        string ThingEIcon { get; }
        string AdminIcon { get; }
        string UserIcon { get; }
        string PeopleIcon { get; }

        Dictionary<int, ThingA> ThingsA { get; }
        Dictionary<int, ThingB> ThingsB { get; }
        Dictionary<int, ThingC> ThingsC { get; }
        Dictionary<int, ThingD> ThingsD { get; }
        Dictionary<int, ThingE> ThingsE { get; }
        Dictionary<int, User> Users { get; }

        void ClearThingsA();
        void MergeThingA(ThingA thingA);
        void DeleteThingA(ThingA thingA);

        void ClearThingsB();
        void MergeThingB(ThingB thingA);
        void DeleteThingB(ThingB thingA);

        void ClearThingsC();
        void MergeThingC(ThingC thingA);
        void DeleteThingC(ThingC thingA);

        void ClearThingsD();
        void MergeThingD(ThingD thingA);
        void DeleteThingD(ThingD thingA);

        void ClearThingsE();
        void MergeThingE(ThingE thingA);
        void DeleteThingE(ThingE thingA);

        void ClearUsers();
        void MergeUser(User user);
        void DeleteUser(User user);

        void Clear();
    }

    #endregion

    public class Cache : ICache
    {
        #region Dependency Injection

        private readonly ICurrentUser _currentUser;
        private readonly IMemoryCache _memoryCache;
        private readonly UltraContext _db;

        public Cache(ICurrentUser currentUser, IMemoryCache memoryCache, UltraContext db)
        {
            _currentUser = currentUser;
            _memoryCache = memoryCache;
            _db = db;
        }

        #endregion

        #region Cache management

        private static object locker = new object();
        private static object mocker = new object(); // modifying locker

        private const string ThingsAKey = nameof(ThingsAKey);
        private const string ThingsBKey = nameof(ThingsBKey);
        private const string ThingsCKey = nameof(ThingsCKey);
        private const string ThingsDKey = nameof(ThingsDKey);
        private const string ThingsEKey = nameof(ThingsEKey);
        private const string UsersKey = nameof(UsersKey);
        private const string MetaTypesKey = nameof(MetaTypesKey);

        // Keeps track of keys used
        private static readonly HashSet<string> UsedKeys = new HashSet<string>();

        #endregion

        #region MetaTypes

        public Dictionary<string, MetaType> MetaTypes
        {
            get
            {
                // ** Lazy load pattern 

                var dictionary = _memoryCache.Get(MetaTypesKey) as Dictionary<string, MetaType>;
                if (dictionary == null)
                {
                    lock (locker)
                    {
                        dictionary = new Dictionary<string, MetaType>();
                        dictionary.Add("ThingA", new MetaType { Name = "ThingA", Icon = "icon-star icon-square icon-thinga", Url="/thingsa" });
                        dictionary.Add("ThingB", new MetaType { Name = "ThingB", Icon = "icon-user icon-square icon-thingb", Url="/thingsb" });
                        dictionary.Add("ThingC", new MetaType { Name = "ThingC", Icon = "icon-calendar icon-square icon-thingc", Url="/thingsc" });
                        dictionary.Add("ThingD", new MetaType { Name = "ThingD", Icon = "icon-globe icon-square icon-thingd", Url="/thingsd" });
                        dictionary.Add("ThingE", new MetaType { Name = "ThingE", Icon = "icon-people icon-square icon-thinge", Url= "/thingse" });
                        dictionary.Add("People", new MetaType { Name = "People", Icon = "icon-emotsmile icon-square icon-persons", Url = "/people" });
                        dictionary.Add("Admin", new MetaType { Name = "Admin", Icon = "icon-settings icon-square icon-admin", Url = "/admin" });
                        dictionary.Add("Owner", new MetaType { Name = "Owner", Icon = "icon-user icon-square icon-owner", Url = "/people" });
                        dictionary.Add("User", new MetaType { Name = "User", Icon = "icon-user icon-square icon-owner", Url = "/admin/users" });

                        Add(MetaTypesKey, dictionary, DateTime.Now.AddHours(24));
                    }
                }

                return dictionary;
            }
        }
        public string ThingAIcon { get { return MetaTypes["ThingA"].Icon; } }
        public string ThingBIcon { get { return MetaTypes["ThingB"].Icon; } }
        public string ThingCIcon { get { return MetaTypes["ThingC"].Icon; } }
        public string ThingDIcon { get { return MetaTypes["ThingD"].Icon; } }
        public string ThingEIcon { get { return MetaTypes["ThingE"].Icon; } }
        public string AdminIcon { get { return MetaTypes["Admin"].Icon; } }
        public string UserIcon { get { return MetaTypes["User"].Icon; } }
        public string PeopleIcon { get { return MetaTypes["People"].Icon; } }

        #endregion

        #region ThingAs

        public Dictionary<int, ThingA> ThingsA
        {
            get
            {
                // ** Lazy load pattern 

                if (!(_memoryCache.Get(ThingsAKey) is Dictionary<int, ThingA> dictionary))
                {
                    lock (locker)
                    {
                        dictionary = _db.ThingA.ToDictionary(c => c.Id);
                        Add(ThingsAKey, dictionary, DateTime.Now.AddHours(4));
                    }
                }

                return dictionary;
            }
        }

        // Clear thingA cache

        public void ClearThingsA()
        {
            lock (locker)
            {
                Clear(ThingsAKey);
            }
        }

        public void MergeThingA(ThingA thingA)
        {
            lock (mocker)
            {
                if (thingA != null)
                    ThingsA[thingA.Id] = thingA;
            }
        }

        public void DeleteThingA(ThingA thingA)
        {
            lock (mocker)
            {
                if (thingA != null)
                    ThingsA.Remove(thingA.Id);
            }
        }

        #endregion

        #region ThingBs

        public Dictionary<int, ThingB> ThingsB
        {
            get
            {
                // ** Lazy load pattern 

                if (!(_memoryCache.Get(ThingsBKey) is Dictionary<int, ThingB> dictionary))
                {
                    lock (locker)
                    {
                        dictionary = _db.ThingB.ToDictionary(c => c.Id);
                        Add(ThingsBKey, dictionary, DateTime.Now.AddHours(4));
                    }
                }

                return dictionary;
            }
        }

        // Clear ThingB cache

        public void ClearThingsB()
        {
            lock (locker)
            {
                Clear(ThingsBKey);
            }
        }

        public void MergeThingB(ThingB ThingB)
        {
            lock (mocker)
            {
                if (ThingB != null)
                    ThingsB[ThingB.Id] = ThingB;
            }
        }

        public void DeleteThingB(ThingB ThingB)
        {
            lock (mocker)
            {
                if (ThingB != null)
                    ThingsB.Remove(ThingB.Id);
            }
        }

        #endregion

        #region ThingCs

        public Dictionary<int, ThingC> ThingsC
        {
            get
            {
                // ** Lazy load pattern 

                if (!(_memoryCache.Get(ThingsCKey) is Dictionary<int, ThingC> dictionary))
                {
                    lock (locker)
                    {
                        dictionary = _db.ThingC.ToDictionary(c => c.Id);
                        Add(ThingsCKey, dictionary, DateTime.Now.AddHours(4));
                    }
                }

                return dictionary;
            }
        }

        // Clear ThingC cache

        public void ClearThingsC()
        {
            lock (locker)
            {
                Clear(ThingsCKey);
            }
        }

        public void MergeThingC(ThingC ThingC)
        {
            lock (mocker)
            {
                if (ThingC != null)
                    ThingsC[ThingC.Id] = ThingC;
            }
        }

        public void DeleteThingC(ThingC ThingC)
        {
            lock (mocker)
            {
                if (ThingC != null)
                    ThingsC.Remove(ThingC.Id);
            }
        }

        #endregion

        #region ThingDs

        public Dictionary<int, ThingD> ThingsD
        {
            get
            {
                // ** Lazy load pattern 

                if (!(_memoryCache.Get(ThingsDKey) is Dictionary<int, ThingD> dictionary))
                {
                    lock (locker)
                    {
                        dictionary = _db.ThingD.ToDictionary(c => c.Id);
                        Add(ThingsDKey, dictionary, DateTime.Now.AddHours(4));
                    }
                }

                return dictionary;
            }
        }

        // Clear ThingD cache

        public void ClearThingsD()
        {
            lock (locker)
            {
                Clear(ThingsDKey);
            }
        }

        public void MergeThingD(ThingD ThingD)
        {
            lock (mocker)
            {
                if (ThingD != null)
                    ThingsD[ThingD.Id] = ThingD;
            }
        }

        public void DeleteThingD(ThingD ThingD)
        {
            lock (mocker)
            {
                if (ThingD != null)
                    ThingsD.Remove(ThingD.Id);
            }
        }

        #endregion

        #region ThingEs

        public Dictionary<int, ThingE> ThingsE
        {
            get
            {
                // ** Lazy load pattern 

                if (!(_memoryCache.Get(ThingsEKey) is Dictionary<int, ThingE> dictionary))
                {
                    lock (locker)
                    {
                        dictionary = _db.ThingE.ToDictionary(c => c.Id);
                        Add(ThingsEKey, dictionary, DateTime.Now.AddHours(4));
                    }
                }

                return dictionary;
            }
        }

        // Clear ThingE cache

        public void ClearThingsE()
        {
            lock (locker)
            {
                Clear(ThingsEKey);
            }
        }

        public void MergeThingE(ThingE ThingE)
        {
            lock (mocker)
            {
                if (ThingE != null)
                    ThingsE[ThingE.Id] = ThingE;
            }
        }

        public void DeleteThingE(ThingE ThingE)
        {
            lock (mocker)
            {
                if (ThingE != null)
                    ThingsE.Remove(ThingE.Id);
            }
        }

        #endregion

        #region Users

        public Dictionary<int, User> Users
        {
            get
            {
                // ** Lazy load pattern 

                if (!(_memoryCache.Get(UsersKey) is Dictionary<int, User> dictionary))
                {
                    lock (locker)
                    {
                        dictionary = _db.User.OrderBy(u => u.LastName).ToDictionary(c => c.Id);
                        Add(UsersKey, dictionary, DateTime.Now.AddHours(4));
                    }
                }

                return dictionary;
            }
        }

        // Clear users cache

        public void ClearUsers()
        {
            lock (locker)
            {
                Clear(UsersKey);
            }
        }

        public void MergeUser(User user)
        {
            lock (mocker)
            {
                Users[user.Id] = user;
            }
        }

        public void DeleteUser(User user)
        {
            lock (mocker)
            {
                // No hard deletes
                Users[user.Id] = user;
            }
        }

        #endregion

        #region Cache Helpers

        // clears single cache entry

        private void Clear(string key)
        {
            lock (locker)
            {
                _memoryCache.Remove(key);

                UsedKeys.Remove(key);
            }
        }

        // clears entire cache

        public void Clear()
        {
            // only host is allowed to clear entire cache

            if (!_currentUser.IsAdmin) return;

            lock (locker)
            {
                foreach (var usedKey in UsedKeys)
                    _memoryCache.Remove(usedKey);

                UsedKeys.Clear();
            }
        }

        // add to cache 

        private void Add(string key, object value, DateTimeOffset expiration)
        {
            _memoryCache.Set(key, value,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiration));

            UsedKeys.Add(key);
        }

        #endregion
    }

    public class MetaType
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
    }
}
