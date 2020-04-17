using System.Collections;
using System.Collections.Generic;

namespace BT
{
    // it's final leaf with action
    public class ActionNode : Node
    {
        // Method signature for the action.
        public delegate States ActionNodeDelegate();

        // pointer to action-function
        private ActionNodeDelegate action;

        public ActionNode(ActionNodeDelegate actionMethod)
        {
#if DEBUG_MODE
            //UnityEngine.Debug.Log($"ActionNode Constructor {actionMethod.GetType().Name}");
#endif
            this.action = actionMethod;
        }

        public ActionNode(ActionNodeDelegate actionMethod, string name)
        {
#if DEBUG_MODE
            //UnityEngine.Debug.Log($"ActionNode Constructor {actionMethod.GetType().Name}");
#endif
            this.action = actionMethod;
            this.Name = name;
        }

        public override States Evaluate()
        {
#if DEBUG_MODE
            //UnityEngine.Debug.Log($"ActionNode Evaluate {this.Name}");
#endif
            //without switch
            /*
             * try {
             * _state = this.action();
             * return state;
             * }
             * catch (System.Exception e) {
             *  _state = States.FAILED;
             *  return state;
             * }
             */
            switch (this.action())
            {
                case States.SUCCESS:
                    _state = States.SUCCESS;
                    return state;
                case States.FAILED:
#if DEBUG_MODE
                    UnityEngine.Debug.Log($"ActionNode FAILED {this.Name}");
#endif
                    _state = States.FAILED;
                    return state;
                case States.EXECUTED:
                    _state = States.EXECUTED;
                    return state;
                default:
#if DEBUG_MODE
                    UnityEngine.Debug.Log($"ActionNode DEFAULT {this.Name}");
#endif
                    _state = States.FAILED;
                    return state;
            }
        }
    }
}
 
 
 