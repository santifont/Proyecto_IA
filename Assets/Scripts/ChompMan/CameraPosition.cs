using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private GameObject chompMan;
    private float posZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chompMan = GameObject.Find("Chomp");
    }

    // Update is called once per frame
    void Update()
    {
        // 14 ~ 28 Z
        posZ = chompMan.transform.position.z;
        transform.position = new Vector3(0.0f, 35.0f, posZ);

        if (transform.position.z < -14.0f)
        {
            transform.position = new Vector3(0.0f, 35.0f, -14.0f);
        }

        if (transform.position.z > 28.0f)
        {
            transform.position = new Vector3(0.0f, 35.0f, 28.0f);
        }

        transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    }
}
