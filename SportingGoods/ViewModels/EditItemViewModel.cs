﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.ViewModels
{
    public class EditItemViewModel : CreateItemViewModel
    {
        public int ID { get; set; }
        public string ExistingPicture { get; set; }
    }
}
