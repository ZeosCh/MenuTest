using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace MenuTest.Resources.model
{
    public class Station
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public int Capacity { get; set; }
        public int Availability { get; set; }
        public String State { get; set; }
    }
}