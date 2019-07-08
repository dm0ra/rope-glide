using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HingeManager : MonoBehaviour
{
    public ArrayList Hinges;
    
    // Start is called before the first frame update
    void Start()
    {
        Hinges = new ArrayList();
    }

    public void addHinge(Rope H)
    {
        Hinges.Add(H);

    }

}
