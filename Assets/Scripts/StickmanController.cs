using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    private Animator stickmanAnimator;
    private GravityController playerController;
    // Start is called before the first frame update
    private bool turned;
    void OnEnable()
    {
        stickmanAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<GravityController>();
    }

    public void hasChangedDirection(bool dir)
    {
        if (dir == false && turned == true)
        {
            gameObject.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            turned = false;
        }
        if (dir == true && turned == false)
        {
            gameObject.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            turned = true;
        }
    }
}
