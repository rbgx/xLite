using System;
using System.Collections.Generic;
using System.Text;

namespace xLite.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Items
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
