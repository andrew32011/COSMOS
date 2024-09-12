using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwnObj : MonoBehaviour
{
    public int Cost;
    [SerializeField] private float PlusPower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");

        if (collision.gameObject.tag == "Player")
        {
            
            Debug.Log("FInal");
            GravityController contr = collision.gameObject.GetComponent<GravityController>();
            if (contr.JumpForse < 1)
            {
                contr.PlusJump(PlusPower);
            }
            

        }
    }
}
