﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MiDEWPF.Models
{
    public class NewButton : Button
    {
        private int bid;

        public int Bid { get; set; }
        
        public NewButton()
        {
            Bid = bid;
        }

        public int GetBid()
        {
            return Bid;
        }
       
    }
}
