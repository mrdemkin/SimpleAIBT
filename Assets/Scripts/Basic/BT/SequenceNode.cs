using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class SequenceNode : Node
    {
        protected List<Node> cNodes = new List<Node>();

        public SequenceNode(List<Node> childNodes)
        {
            this.cNodes = childNodes;
        }

        public SequenceNode(List<Node> childNodes, string name)
        {
            this.cNodes = childNodes;
            this._nodeName = name;
        }

        //if any of node failed - failed. If all nodes is success - success
        public override States Evaluate()
        {
            bool anyExecuted = false;
#if DEBUG_MODE
            //UnityEngine.Debug.Log($"Sequence node 0 {cNodes.Count}");
			UnityEngine.Debug.Log("Sequence node 0 " +cNodes.Count);
#endif
            foreach (Node node in cNodes)
            {
#if DEBUG_MODE
                UnityEngine.Debug.Log("Sequence node " +node.Name);
#endif
                switch (node.Evaluate())
                {
                    case States.FAILED:
                        _state = States.FAILED;
                        return _state;
                    case States.SUCCESS:
                        continue;
                    case States.EXECUTED:
                        anyExecuted = true;
                        continue;
                    default:
                        _state = States.SUCCESS;
                        return _state;
                }
            }
            _state = anyExecuted ? States.EXECUTED : States.SUCCESS;
            return _state;

        }
    }
}
