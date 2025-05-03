using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
    public JetAgent_Child_2 jetagent;
    public Transform rightTarget, leftTarget;
    //
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "handType")
        {
            HandType hand = collision.gameObject.GetComponent<HandType>();
            if (hand.handsType == HandType.HandsType.Right)
            {
                jetagent.agentMother_Transform = rightTarget.gameObject;
            }
            else if (hand.handsType == HandType.HandsType.Left)
            {
                jetagent.agentMother_Transform = leftTarget.gameObject;
            }
            Debug.Log("The target changed!");
        }
    }
}
