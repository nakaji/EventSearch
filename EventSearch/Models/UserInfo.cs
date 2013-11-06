using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace EventSearch.Models
{
    //[Serializable]
    public class UserInfo
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string GivenName { get; private set; }
        public string FamilyName { get; private set; }
        public string Picture { get; private set; }

        public UserInfo(string id, string name, string givenName, string familyName, string picture)
        {
            Id = id;
            Name = name;
            GivenName = givenName;
            FamilyName = familyName;
            Picture = picture;
        }
    }
}