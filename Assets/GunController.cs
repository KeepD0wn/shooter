using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject[] weapons = new GameObject[0];
    public AllWeapon[] all = new AllWeapon[1];
    Ak47 ak;
    GameObject firstSlot;
    GameObject secondSlot;

    // Start is called before the first frame update
    void Start()
    {
        all[0] = ak;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Alpha1) && !weapons[0].gameObject.GetComponent<Ak47>().IsDropped)
        {
            HideAll();
            weapons[0].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HideAll();
            weapons[1].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,3f))
            {
                string str = hit.transform.gameObject.tag;
                switch (str)
                {
                    case "Ak47":
                        PickUpGun(0,hit);
                        weapons[0].gameObject.GetComponent<Ak47>().IsDropped = false;                        
                        break;
                    case "M16A4":
                        PickUpGun(2,hit);
                        weapons[2].gameObject.GetComponent<M16A4>().IsDropped = false;
                        break;
                }
                    
            }
        }
       
    }

    void PickUpGun(int x,RaycastHit hit)
    {
        Destroy(hit.transform.gameObject);
        HideAll();
        weapons[x].SetActive(true);
    }

    void HideAll()
    {
        foreach (GameObject current in weapons)
        {
            current.SetActive(false);
        }
    }
}
