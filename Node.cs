using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree2Json
{
    public class Node<T>
    {
        public List<Node<T>> Childs = new List<Node<T>>();

        public Node(T item)
        {
            this.Item = item;
        }

        public T Item { set; get; }

        public void Add(Node<T> n)
        {
            Childs.Add(n);
        }
    }
}
