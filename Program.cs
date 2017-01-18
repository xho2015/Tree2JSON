using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree2Json
{
    class Program
    {
        /// <summary>
        /// Below scriptlet can be freely copy, modify or distrabute in your code in case you don't want a FAT/rich 3rd party library.
        /// 
        /// However, Please take below into consideration:
        /// 
        /// The runtime enginee for C# have method stack frame, meaning that the stack size are limited.
        //  Therefore, if the tree size are too huge, then it most likely exceed the stack size when
        //  traverse the whole nodes tree for generating JSON string.
        // 
        //  IF you find anything during your testing or any suggestion, please mail to houxuyong@hotmail.com
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //build up a testing table
            List<Item> table = BuildTable();
            
            //create root node
            var rootItem = table.Where(x => x.Category.Equals("M") && x.Name.Equals("root")).FirstOrDefault<Item>();
            Node<Item> rootNode = new Node<Item>(rootItem); 
            
            //create tree by records in table
            Table2Tree(table, rootNode);

            //calculate the tree size
            int tsize = TreeSize(rootNode) + 1;

            //build the JSON string
            string json = JSonBuilder.BuildJson(rootNode);

        }

        static List<Item> BuildTable()
        {
            List<Item> table = new List<Item>();

            //below 10 rows is in tree structured
            table.Add(new Item() { Category = "M", Id = 0, ParentId = -1, Name = "root"});
            table.Add(new Item() { Category = "M", Id = 1, ParentId = 0, Name = "KAM" });
            table.Add(new Item() { Category = "M", Id = 2, ParentId = 0, Name = "DBD" });
            table.Add(new Item() { Category = "M", Id = 3, ParentId = 1, Name = "K11", Value = "http://k11/ads.aspx?a=1" });
            table.Add(new Item() { Category = "M", Id = 4, ParentId = 1, Name = "K22", Value = "http://k22/ads.aspx?a=1" });
            table.Add(new Item() { Category = "M", Id = 5, ParentId = 2, Name = "D1" });
            table.Add(new Item() { Category = "M", Id = 6, ParentId = 2, Name = "D22" });
            table.Add(new Item() { Category = "M", Id = 7, ParentId = 5, Name = "D1ad", Value = "http://d11/ads.aspx?a=1" });
            table.Add(new Item() { Category = "M", Id = 8, ParentId = 5, Name = "D1ff", Value = "http://dff/ads.aspx?a=1" });
            table.Add(new Item() { Category = "M", Id = 9, ParentId = 5, Name = "D1zz", Value = "http://dszz/ads.aspx?a=1" });
            
            //this item is not relevant and should be filtered out
            table.Add(new Item() { Category = "C", Id = 10, ParentId = -1, Name = "CacheExpire", Value = "3600" });

            return table;
        }

        static void Table2Tree(List<Item> table, Node<Item> node)
        {
            List<Item> childs = table.Where(x => x.Category.Equals("M") && x.ParentId==node.Item.Id).ToList();
            foreach(var c in childs)
            {
                Node<Item> n = new Node<Item>(c);
                node.Add(n);

                Table2Tree(table, n);
            }            
        }

        /// <summary>
        /// traverse/count number of all childs
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static int TreeSize(Node<Item> node)
        {
            if (node.Childs.Count == 0)
                return 0;
            int count = node.Childs.Count;
            foreach (var n in node.Childs)
            {
                count += TreeSize(n);
            }
            return count;
        }
    }
}


