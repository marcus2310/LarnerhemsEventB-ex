using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LarnerhemsEvent.Models.ViewModels
{
    public class VMPackages
    {
        public List<package> Tent { get; set; }
        public List<package> Golv { get; set; }
        public List<package> Ljud { get; set; }
        public List<package> Ljus { get; set; }
        public List<package> Tillbehör { get; set; }

    }
}