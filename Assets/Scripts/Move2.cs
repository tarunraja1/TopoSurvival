using UnityEngine;
using System.Collections;


public class Move2 : MonoBehaviour {
    private float speed = 6.0F;
    private float jump_speed_initial = 3.0F;
    private float gravity = -9.8F;
    private float jump_speed = 0.0F;
    private Vector3 move_direction = Vector3.zero;
    public static bool movable {  get; set;  }        
	// Use this for initialization
    void Start () 
    {
        InvokeRepeating("WalkingSound", 0, 0.5F);
        movable = true;
    }
	
	// Update is called once per frame
    void Update () 
    {
        CharacterController fig2 = GetComponent<CharacterController>();
        speed = Move1.Speed;
        if(movable)
        {
            move_direction = new Vector3(Input.GetAxis("Horizontal2") * Time.deltaTime, 0, Input.GetAxis("Vertical2") * Time.deltaTime);
            move_direction = transform.TransformDirection(move_direction);
            move_direction *= speed;
            if (fig2.isGrounded)
            {

                //if (Input.GetButton("Jump"))
                //{
                //jump_speed += jump_speed_initial;
                //}
            }
            else
            {
                jump_speed += gravity * Time.deltaTime;;
            }
            move_direction.y = jump_speed;
            fig2.Move(move_direction);
            var model = GameObject.FindGameObjectWithTag("Char2Model");
            move_direction.y = 0;
            if (move_direction != Vector3.zero)
            {
                model.transform.rotation = Quaternion.Slerp(
                                                            model.transform.rotation,
                                                            Quaternion.LookRotation(move_direction),
                                                            Time.deltaTime* 5
                                                            );

            }
        }
    }
	
    void WalkingSound()
    {

        if (Input.GetButton("Vertical2") || Input.GetButton("Horizontal2") )
        {
            var walking_gravel = gameObject.GetComponent<AudioSource>().clip;
            audio.Play();
        }
        else
        {
            audio.Stop();
        }
    }
}
