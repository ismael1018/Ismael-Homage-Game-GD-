using UnityEngine;

public class GoalZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>() != null)
            ScoreManager.Instance.GoalScored();
    }
}
