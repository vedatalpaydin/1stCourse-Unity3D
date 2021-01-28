using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] private Canvas impact;
    [SerializeField] private float impactTime = .3f;
    
    // Start is called before the first frame update
    void Start()
    {
        impact.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        impact.enabled = true;
        yield return new WaitForSeconds(impactTime);
        impact.enabled = false;

    }
}
