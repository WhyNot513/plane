using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] float explosionRadius;
    [SerializeField] LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    public void PickUp()
    {
        foreach (var item in Physics2D.OverlapCircleAll(transform.position, explosionRadius, mask))
        {
            item.GetComponent<Dropsthing>().setTarget(this.gameObject);
        }


    }
}
