﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.BussinesObject.Application
{
    public class Contactos
    {
        private int _Ide_Contact;

        public int Ide_Contact
        {
            get { return _Ide_Contact; }
            set { _Ide_Contact = value; }
        }

        private int _Ide_User;

        public int Ide_User
        {
            get { return _Ide_User; }
            set { _Ide_User = value; }
        }

        private string _Nom_Name;

        public string Nom_Name
        {
            get { return _Nom_Name; }
            set { _Nom_Name = value; }
        }

        private string _Nom_LastName;

        public string Nom_LastName
        {
            get { return _Nom_LastName; }
            set { _Nom_LastName = value; }
        }

        private string _Nom_GroupName;

        public string Nom_GroupName
        {
            get { return _Nom_GroupName; }
            set { _Nom_GroupName = value; }
        }

        private string _Vlr_PhoneNumber;

        public string Vlr_PhoneNumber
        {
            get { return _Vlr_PhoneNumber; }
            set { _Vlr_PhoneNumber = value; }
        }

        private int _Ide_PhoneType;

        public int Ide_PhoneType
        {
            get { return _Ide_PhoneType; }
            set { _Ide_PhoneType = value; }
        }

        private string _Num_Extension;

        public string Num_Extension
        {
            get { return _Num_Extension; }
            set { _Num_Extension = value; }
        }

        private string _Nom_Organization;

        public string Nom_Organization
        {
            get { return _Nom_Organization; }
            set { _Nom_Organization = value; }
        }

        private string _Des_Comments;

        public string Des_Comments
        {
            get { return _Des_Comments; }
            set { _Des_Comments = value; }
        }

        private int _Ide_RecentUser;

        public int Ide_RecentUser
        {
            get { return _Ide_RecentUser; }
            set { _Ide_RecentUser = value; }
        }
    }
}
