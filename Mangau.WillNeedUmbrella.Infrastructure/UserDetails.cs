using Mangau.WillNeedUmbrella.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public class UserDetails
    {
        public UserDetails()
        {
        }

        public UserDetails(User user, bool withPermissions = false)
        {
            Id = user.Id;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;

            if (withPermissions && user.GroupsUsers != null && user.GroupsUsers.Any())
            {
                List<string> permissions = new List<string>();

                foreach (var gu in user.GroupsUsers)
                {
                    foreach (var gp in gu.Group.GroupsPermissions)
                    {
                        permissions.Add(gp.Permission.Name.ToLowerInvariant());
                    }
                }

                Permissions = permissions;
            }
        }

        public long Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Token { get; set; }

        private List<string> _permisions = new List<string>();
        public IEnumerable<string> Permissions
        { 
            get => _permisions; 
            set
            {
                _permisions.Clear();

                if (value != null && value.Any())
                {
                    _permisions.AddRange(value.Distinct());
                }
            }
        }
    }
}
