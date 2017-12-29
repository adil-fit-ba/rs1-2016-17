using System;
using CondorExtreme3.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CondorExtreme3.DAL;
using System.Data.Entity;
using CondorExtreme3.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace CondorExtreme3Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string id = "1";
           
            Console.WriteLine(Guid.NewGuid());
            Console.WriteLine(Guid.NewGuid());
            Console.ReadKey();
        }
    }
}
