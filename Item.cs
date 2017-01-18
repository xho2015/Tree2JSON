using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree2Json
{
    public class Item 
    {
        public const string LB = "{";   //left brace
        public const string RB = "}";   //right brace
        public const string COMMA = ",";    
        public const string COLON = ":";
        public const string OB = "[";   //open bracket
        public const string CB = "]";   //close bracket
        public const string Q = "\"";   //quote

        public string Category { set; get; }

        public int Id { set; get; }

        public int ParentId { set; get; }

        public string Name { set; get; }

        public string Value { set; get; }

        public string ToJson()
        {
            return new StringBuilder(Q).Append("Id").Append(Q).Append(COLON).Append(Q).Append(Id).Append(Q).Append(COMMA)
                .Append(Q).Append("ParentId").Append(Q).Append(COLON).Append(Q).Append(ParentId).Append(Q).Append(COMMA)
                .Append(Q).Append("Name").Append(Q).Append(COLON).Append(Q).Append(Name).Append(Q).Append(COMMA)
                .Append(Q).Append("Value").Append(Q).Append(COLON).Append(Q).Append(Value).Append(Q).Append(COMMA).ToString();
        }
    }

}
