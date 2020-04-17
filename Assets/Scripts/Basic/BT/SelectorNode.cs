using System.Collections;
using System.Collections.Generic;

namespace BT
{
    //regula node
    public class SelectorNode : Node
    {
        //child for selector
        protected List<Node> cNodes = new List<Node>();

		public int Count {
			get {
				return cNodes.Count;
			}
		}

        public SelectorNode(List<Node> childNodes)
        {
            this.cNodes = childNodes;
        }

        public SelectorNode(List<Node> childNodes, string name)
        {
            this.cNodes = childNodes;
            this.Name = name;
        }

        //any of nodes is success - success. If ALL nodes is failed - return fail
        public override States Evaluate()
        {
#if DEBUG_MODE
			UnityEngine.Debug.Log("SelectorNode 0");
#endif
            foreach (Node node in cNodes)
            {
#if DEBUG_MODE
              //  UnityEngine.Debug.Log($"SelectorNode 1");
				UnityEngine.Debug.Log("SelectorNode 1");
#endif
                switch (node.Evaluate())
                {
                    case States.FAILED:
#if DEBUG_MODE
                        //UnityEngine.Debug.Log($"SelectorNode {this.Name} FAILED on {node.Name}");
						UnityEngine.Debug.Log("SelectorNode " + this.Name+ " FAILED on " + node.Name);
#endif
                        //continue;
						break;
                    case States.SUCCESS:
#if DEBUG_MODE
                        //UnityEngine.Debug.Log($"SelectorNode {this.Name} SUCCESS on {node.Name}");
					UnityEngine.Debug.Log("SelectorNode " + this.Name + "SUCCESS on " +node.Name);
#endif
                        _state = States.SUCCESS;
                        return _state;
                    case States.EXECUTED:
#if DEBUG_MODE
                        //UnityEngine.Debug.Log($"SelectorNode {this.Name} EXECUTED on {node.Name}");
						UnityEngine.Debug.Log("SelectorNode " +this.Name+ " EXECUTED on "+node.Name);
#endif
                        _state = States.EXECUTED;
                        return _state;
                    default:
					#if DEBUG_MODE
					//UnityEngine.Debug.Log($"SelectorNode {this.Name} EXECUTED on {node.Name}");
					UnityEngine.Debug.Log("PIAZDETC");
					#endif
                        //continue;
							break;
                }
            }
#if DEBUG_MODE
            UnityEngine.Debug.Log("SelectorNode FAILED");
#endif
            _state = States.FAILED;
            return _state;
        }
    }
}