using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class RandomIntNode : Node
    {
        int startPoint;
        int endPoint;
        public RandomIntNode(int startPoint, int endPoint)
        {
            //check endPoint > startpoint
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        public override States Evaluate()
        {
            int randomValue = Random.Range(startPoint, endPoint);
            return randomValue == endPoint ? States.SUCCESS : States.FAILED;
        }
    }
}
