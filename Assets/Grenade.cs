using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Detonate d = new Detonate();
    float speed = 15f;
    float grenadeLeft = 100;
    public GameObject grenadePref;
    Quaternion fol = new Quaternion();

    // Start is called before the first frame update
    void Start()
    {
        fol.x = 90f;
        fol.y = 90f;
        fol.z = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && CanThrow())
        {
            ThrowGranade();
        }        
    }

    bool CanThrow()
    {
        if (grenadeLeft > 0)
            return true;
        else return false;
    }

    void ThrowGranade()
    {
        grenadeLeft--;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        fol.w = Random.Range(-60f, 60f);
        GameObject gren= Instantiate(grenadePref,transform.position,fol) as GameObject;
        gren.GetComponent<Rigidbody>().velocity =  transform.right* speed;        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 25), "Grenades / " + grenadeLeft);
    }
}
