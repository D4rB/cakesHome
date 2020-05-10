using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class algaeAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture[] textures;
    public float changeInterval = 0.33F;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (textures.Length == 0)
            return;

        int index = Mathf.FloorToInt(Time.time / changeInterval);
        index = index % textures.Length;
        rend.material.mainTexture = textures[index];
    }
}
