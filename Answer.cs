using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CombinationsTool
{
    public class Answer
    {
        public ConcurrentBag<List<string>> answerList { get; private set;}
        public long combinationsTime { get; set; }

        public Answer(ConcurrentBag<List<string>> answer, long time)
        {
            this.answerList = answer;
            this.combinationsTime = time;
        }
    }
}
