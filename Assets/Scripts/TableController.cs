using UnityEngine;

public class TableCollider : MonoBehaviour
{
    private double diceCountOnTable = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            diceCountOnTable += 0.5;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            diceCountOnTable -= 0.5;
        }
    }
}
