using System;
using BT;
using Character;

namespace BaseAI
{
    public abstract class BasePlayerAiClass : IBaseAI
    {
        public CharacterModel cModel;

        public BT.States isLowHealth()
        {
            //TODO: Implement!
#if DEBUG_MODE
#if UNITY_2018_1_OR_NEWER
            UnityEngine.Debug.Log($"isLowHealth {cModel?.isLowHealth()}");
#else
            UnityEngine.Debug.Log("isLowHealth " + cModel.isLowHealth());
#endif
#endif
            return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
        }

        public BT.States isUnderAttack()
        {
            //TODO: Implement!
#if DEBUG_MODE
#if UNITY_2017_4_OR_NEWER
			UnityEngine.Debug.Log($"isUnderAttack {cModel.isUnderAttack()}");
#else
            UnityEngine.Debug.Log("isUnderAttack " + cModel.isUnderAttack());
#endif
#endif
            return cModel.isUnderAttack() ? States.SUCCESS : States.FAILED;
        }

        public BT.States madeCounterAttack()
        {
            //TODO: Implement!
#if DEBUG_MODE
            UnityEngine.Debug.Log("madeCounterAttack TRUE");
#endif
            return States.SUCCESS;
        }

        public BT.States isNotUnderAttack()
        {
            //TODO: Implement!
#if DEBUG_MODE
#if UNITY_2017_4_OR_NEWER
			UnityEngine.Debug.Log($"isNotUnderAttack {cModel.isUnderAttack()}");
#else
            UnityEngine.Debug.Log("isNotUnderAttack " + cModel.isUnderAttack());            
#endif
#endif
            return cModel.isUnderAttack() ? States.FAILED : States.SUCCESS;
        }


        public BT.States moveToExitAction()
        {
            //TODO: Implement!
#if DEBUG_MODE
#if UNITY_2017_4_OR_NEWER
			UnityEngine.Debug.Log($"moveToExitAction {cModel.isUnderAttack()}");
#else
            UnityEngine.Debug.Log("moveToExitAction " + cModel.isUnderAttack());
#endif
#endif
            return cModel.isUnderAttack() ? States.FAILED : States.SUCCESS;
        }

        public BT.States canMoveAction()
        {
            //TODO: Implement!
#if DEBUG_MODE
#if UNITY_2017_4_OR_NEWER
			UnityEngine.Debug.Log($"canMoveAction {cModel.canMove}");
#else
            UnityEngine.Debug.Log("canMoveAction " + cModel.canMove);            
#endif
#endif
            return cModel.canMove ? States.SUCCESS : States.FAILED;
        }

        public abstract void InitActions();
        public abstract int StartAI();
        public void Start()
        {
            InitObjects();
        }
        public void InitObjects()
        {

        }

        public abstract AiStates GetNextAction(States state);
    }
}