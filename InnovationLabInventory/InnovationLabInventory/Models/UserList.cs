using System;

using Xamarin.Forms;

namespace InnovationLabInventory.Models
{
    public class UserList : ContentPage
    {
        //[PrimaryKey, AutoIncrement]
        public int assetID { get; set; }
        public string description { get; set; }
        public string location { get; set; }


    }
}

