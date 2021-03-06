﻿using System;
using System.Collections.Generic;

namespace Ziggle.ProductDatabase
{
    public partial class ProductCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
