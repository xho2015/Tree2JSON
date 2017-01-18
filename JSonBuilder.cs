using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree2Json
{
    /// <summary>
    /// traverse the node tree by preorder and generate the json string
    /// </summary>
    public class JSonBuilder
    {      

        public static string BuildJson(Node<Item> node)
        {
            StringBuilder json = new StringBuilder().Append(Item.LB);

            json.Append(node.Item.ToJson());

            //populate childs
            json.Append(Item.Q).Append("childs").Append(Item.Q).Append(Item.COLON);
            if (node.Childs.Count > 0)
            {
                json.Append(Item.OB);
                int count = 0;
                foreach (var c in node.Childs)
                {
                    json.Append(BuildJson(c));
                    count++;
                    json.Append(count == node.Childs.Count ? string.Empty : Item.COMMA);
                }
                json.Append(Item.CB);
            }
            else
            {
                //nullable childs
                json.Append(Item.Q).Append("null").Append(Item.Q);
            }           
            return json.Append(Item.RB).ToString();
        }
    }
}
