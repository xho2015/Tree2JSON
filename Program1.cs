using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACMachine
{
    class Program1{

        private static int trieTreeSpace = 0;

        private static string src1 = @"<script>alert('中文')</script>";

        static void Main(string[] args)
        {
            AcMachine(args);
            //TestCnStringToChar();
            Console.ReadKey();
        }


        static void TestCnStringToChar()
        {
            char[] cnChars = src1.ToCharArray();
            foreach (char i in cnChars)
                Console.WriteLine(i);
        }


        static void AcMachine(string[] args)
        {
            String[] word={"say","she","shr","he","her","OnClick"};
            String src="saykkkkasherhsOnClicksay";
            for (int i = 0; i < word.Length; i++) {
                Insert(word[i]);
            }
            BuildAcMachine(root);
            Console.WriteLine(Query(src));
            Console.WriteLine("Total Tire tree space is " + trieTreeSpace);
        }

        private static Node root = new Node();

        private static Queue<Node> queue = new Queue<Node>();

        /// <summary>
        /// multiple key words matching in target string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>the number of matching</returns>
        public static int Query(string s)
        {
            //HXY: queue which temprely cache leaf nodes
            Queue<Node> leafNodes = new Queue<Node>(); 

            int count = 0;
            Node p = root;
            char[] str = s.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                int index = str[i] - 'A';
                while (p.child[index] == null && p != root)
                {
                    p = p.fail;
                }
                p = p.child[index];
                p = (p == null) ? root : p;
                Node temp = p;
                while (temp != root && temp.count != -1)
                {
                    //HXY: when temp.count > 1, a matching found here. 
                    if (temp.count > 0)
                    {
                        Console.WriteLine("matching found: {0}", temp.nChars);
                        //HXY: since leaf node will be reset, keep track of them in a queue =>.
                        leafNodes.Enqueue(temp);
                    }

                    count += temp.count;                 
                    temp.count = -1; //Why reset count to -1? 
                    temp = temp.fail;
                }
                //<=and revert it back to original status after while loop
                Console.WriteLine("after while loop, leafNode.Count: {0}", leafNodes.Count);
                        
                for (int k = 0; k < leafNodes.Count; k++ )
                {
                    Node n = leafNodes.Dequeue();
                    n.count = 1;
                }
            }
            return count;
        }


        public static void BuildAcMachine(Node root)
        {
            root.fail = null;
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node temp = queue.Dequeue();
                Node p = null;
                
                for (int i = 0; i < 122 - 65 + 1; i++)
                {
                    if (temp.child[i] != null)
                    {
                        if (temp == root)
                        {
                            temp.child[i].fail = root;
                        }
                        else
                        {
                            p = temp.fail;
                            while (p != null)
                            {
                                if (p.child[i] != null)
                                {
                                    temp.child[i].fail = p.child[i];
                                    break;
                                }
                                p = p.fail;
                            }
                            if (p == null)
                            {
                                temp.child[i].fail = root;
                            }
                        }
                        queue.Enqueue(temp.child[i]);
                    }
                }
            }
        }


        public static void Insert(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            char[] charArray = str.ToCharArray();
            Node cNode = root;

            for (int i=0; i <str.Length; i++)
            {
                int index = charArray[i] - 'A';
                if (cNode.child[index] == null)
                {
                    Node pNode = new Node();
                    cNode.child[index] = pNode;
                }
                cNode = cNode.child[index];   
            }
            cNode.count = 1;
            //HXY: assign the char array (keyword) to tail node                   
            cNode.nChars = str;
        }

        /// <summary>
        /// Char ASCII table (partial A ~ z)
        /// 65        A         41
        /// 90        Z         5A
        /// -----------------------
        /// 91        [         5B
        /// 92        \         5C
        /// 93        ]         5D
        /// 94        ^         5E
        /// 95        _         5F
        /// 96        `         60
        /// -------------------------
        /// 97        a         61
        /// 122       z         7A
        /// </summary>
        public class Node
        {
            public int count {set; get;}
            public Node fail { set; get; }
            public Node[] child { set; get; }
            public string nChars { set; get; }

            public Node()
            {
                fail = null;
                count = 0;
                //child = new Node[26]; //26 means slots for a~y, how about A~Y ?
                child = new Node[122-65+1]; //extend it to A ~ z, but included 6 unwanted slots
                trieTreeSpace += 1;
            }
        }
    }
}
