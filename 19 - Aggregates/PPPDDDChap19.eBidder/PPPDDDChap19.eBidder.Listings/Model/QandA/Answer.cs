using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.QandA
{
    public class Answer
    {
        public Answer(DateTime dateAnswered, string text)
        {
            DateAnswered = dateAnswered;
            Text = text;
        }

        public DateTime DateAnswered { get; private set; }
        public string Text { get; private set; }
    }
}
