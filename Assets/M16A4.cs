using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M16A4 : AllWeapon
{
    public GameObject m4Pref;
    private static bool isDropped = false;

    int currenPatrons;
    int patronsAtAll = 25;
    public GameObject decal;

    float timeCD = 0.15f;
    float currentCD;

    public Transform cam;
    float deltaShake = 0.5f;

    float offSetY = 0;
    float offSetX = 0;

    AudioSource audio;

    public AudioClip reloadAudio;
    public AudioClip shootAudio;

    public override int CurrenPatrons
    {
        get
        {
            return currenPatrons;
        }
        set
        {
            if (value < 0)
                value = 0;
            currenPatrons = value;
        }
    }
    public override bool IsDropped { get => isDropped; set => isDropped = value; }

    public override bool CanShootCoolDown()
    {
        if (currentCD <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ForceHit(RaycastHit hit)
    {
        Rigidbody r = hit.transform.gameObject.GetComponent<Rigidbody>();
        if (r != null)
        {
            r.AddForceAtPosition(-hit.normal * 2000f, hit.point);
        }
    }

    public override void RandomOffset()
    {
        int x = Random.Range(0, 2);
        if (x == 0)
        {
            offSetX += 7;
        }
        else
        {
            offSetX -= 7;
        }

        offSetY += 7f;
        StartCoroutine(ShakeCamera());
    }

    public override IEnumerator Reload()
    {
        audio.clip = reloadAudio;
        audio.Play();
        offSetY = 0;
        offSetX = 0;
        yield return null;
        CurrenPatrons = patronsAtAll;
    }

    public override void ReturnRay()
    {
        if (offSetY > 0)
        {
            offSetY -= 0.8f; //больше - быстрее возвращается y в 0
        }
        if (offSetX > -0.4f && 0.4f > offSetX)
        {
            offSetX = 0;
        }
        else if (offSetX > 0)
        {
            offSetX -= 0.6f;//больше - быстрее возвращается x в 0  
        }
        else
        {
            offSetX += 0.6f;
        }
    }

    public override void SetDecal(RaycastHit hit)
    {
        if (hit.transform.gameObject.tag != "NonDecal")
        {
            GameObject go = Instantiate(decal) as GameObject;
            go.transform.rotation = Quaternion.LookRotation(-hit.normal);
            go.transform.position = hit.point + hit.normal * 0.01f;
            go.transform.SetParent(hit.transform);
            Destroy(go, 5);
        }
    }

    public override IEnumerator ShakeCamera()
    {
        cam.Rotate(new Vector3(-deltaShake, 0, 0));
        yield return new WaitForSeconds(0.02f);
        cam.Rotate(new Vector3(deltaShake, 0, 0));
    }

    public override void Shoot()
    {
        CurrenPatrons--;
        StartCoroutine(SoundShoot());

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(offSetX, offSetY, 0)); // луч+ отдача
        RandomOffset();
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            SetDecal(hit);
            ForceHit(hit);

            if (hit.transform.gameObject.tag == "Target")
            {
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        currentCD = timeCD;
    }

    public override IEnumerator SoundShoot()
    {
        audio.clip = shootAudio;
        audio.Play();
        yield return new WaitForSeconds(0.13f);
        audio.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        CurrenPatrons = patronsAtAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && CanShootCoolDown() == true && CurrenPatrons > 0)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DropGun();
        }
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.R) && CurrenPatrons < 30)
        {
            StartCoroutine(Reload());
        }
    }

    private void FixedUpdate()
    {
        ReturnRay();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 25), "M4 patrons / " + CurrenPatrons);
    }

    public override void DropGun()
    {
        GameObject m4 = Instantiate(m4Pref, transform.position, Quaternion.identity);
        m4.GetComponent<Rigidbody>().velocity = -transform.forward * 3f;
        IsDropped = true;
        gameObject.SetActive(false);
    }
}
