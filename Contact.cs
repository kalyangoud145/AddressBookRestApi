﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookRestApi
{
    public class Contact
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string contactType { get; set; }
    }
}
