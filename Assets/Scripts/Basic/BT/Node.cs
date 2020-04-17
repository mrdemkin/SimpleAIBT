using System.Collections;
using System.Collections.Generic;

namespace BT
{
    public abstract class Node
    {
        private string _nodeName = string.Empty;
        public string Name
        {
            get
            {
                return _nodeName;
            }
            set
            {
                _nodeName = value;
            }
        }
        public delegate States NodeReturn();
        protected States _state;

        public States state
        {
            get { return _state; }
        }

        public Node() { }
        /* Implementing classes use this method to evaluate the desired set of conditions */
        public abstract States Evaluate();
    }
}