﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Pages
{
    public interface IPage
    {

        void PrintHeader();
        void PrintMenu();
        bool Run();
        void PrintFooter();
       
    }
}
