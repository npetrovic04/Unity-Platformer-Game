using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject player;   // to access the GameCtrl script (class)
    public float speed;         // the speed at which BG moves

    float offSetX;              // the secret of horizontal parallax
    Material mat;               // material assigned to the Quad renderer
    PlayerCtrl playerCtrl;
    
    void Start ()
    {
        mat = GetComponent<Renderer>().material;

        playerCtrl = player.GetComponent<PlayerCtrl>();
    }
	
	void Update ()
    {
        // offSetX += 0.005f;
        if (!playerCtrl.isStuck)
        {
            //showing parallax
            // for keyboard and joystick
            offSetX += Input.GetAxisRaw("Horizontal") * speed;

            // for mobile
            if (playerCtrl.leftPressed)
                offSetX += -speed;
            else if (playerCtrl.rightPressed)
                offSetX += speed;

            mat.SetTextureOffset("_MainTex", new Vector2(offSetX, 0));
        }
    }
}
