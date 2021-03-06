﻿using System.Runtime.Serialization;
using EntityFX.Gdcame.Manager.Contract.SessionManager;

namespace EntityFX.Gdcame.Manager.Contract.UserManager
{
    [DataContract]
    public class UserData
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        public UserRole[] UserRoles { get; set; }
    }
}