using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move1 : MonoBehaviour {
    public static float Speed { get; set; }
    private const float jump_speed_initial = 3.0F;
    private const float gravity = -9.8F;
    private float jump_speed { get; set; }
    private Vector3 move_direction;
    private bool playing{ get; set; }
    public static bool Movable {  get; set;  }
    private static GameObject p1Bubble { get; set; }
    private static GameObject p2Bubble { get; set; }
    private static TextMesh p1Speech, p2Speech;
    private static GameObject p1Gui, p2Gui;
    public static string P1Tips { get; set; }
    public static string P2Tips { get; set; }
    public static List<Dialog> DialogList { get; set; }
    // Use this for initialization
    void Start () 
    {
        Speed = SavedVars.Instance.speed;
        jump_speed = 0;
        move_direction = Vector3.zero;
        Movable = true;
        playing = false;
        InvokeRepeating("WalkingSound", 0, .5F);
        DialogList = new List<Dialog>();
        p1Bubble = GameObject.Find("P1Speech");
        p1Speech = p1Bubble.GetComponent<TextMesh>();
        p1Bubble.SetActive(false);
        p2Bubble = GameObject.Find("P2Speech");
        p2Speech = p2Bubble.GetComponent<TextMesh>();
        p2Bubble.SetActive(false);   
        p1Gui = GameObject.Find("p1Gui");
        p2Gui = GameObject.Find("p2Gui");
        //StartCoroutine(Talk());
    }
	
    // Update is called once per frame
    void Update () 
    {
        if (Speed > 3)
        {
            Speed -= Time.deltaTime/ 100;
        }
        CharacterController fig1 = GetComponent<CharacterController>();
        if (Movable)
        {
            move_direction = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime, 0, Input.GetAxis("Vertical") * Time.deltaTime);
            move_direction = transform.TransformDirection(move_direction);
            move_direction *= Speed;
            p1Gui.guiText.text = P1Tips;
            p2Gui.guiText.text = P2Tips;
            if (fig1.isGrounded)
            {

                //if (Input.GetButton("Jump"))
                //#{
                //jump_speed += jump_speed_initial;
                //}
            }
            else
            {
                jump_speed += gravity * Time.deltaTime;
            }
            move_direction.y = jump_speed;
            fig1.Move(move_direction);
            var model = GameObject.FindGameObjectWithTag("Char1Model");
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

    public static void AddDialog(int player, string text)
    {
        DialogList.Add(new Dialog(player.ToString(), text));
    }
    
    public static IEnumerator Talk()
    {
        Movable = false;
        Move2.movable = false;
        print("in talk");
        foreach(var d in DialogList)
        {
            int words = 0;
            print (d.text);
            if(d.player == "1")
            {
                p1Bubble.SetActive(true);
                p2Bubble.SetActive(false);
                p2Speech.text = "";
                p1Speech.text = "";
                foreach(var s in d.text.Split(' '))
                {
                    words += 1;
                    if(words > 4)
                    {
                        p1Speech.text += "\n";
                        words = 0;
                    }
                        
                    foreach(var c in s)
                    {
                        p1Speech.text += c.ToString();
                        yield return new WaitForSeconds (.01f);
                    }
                    p1Speech.text += " ";
                    yield return new WaitForSeconds (.05f);
                }
            
                yield return new WaitForSeconds(1);
                words = 0;
            }
            
            else if(d.player == "2")
            {
                p2Bubble.SetActive(true);
                p1Bubble.SetActive(false);
                 p1Speech.text = "";
                p2Speech.text = "";
                foreach(var s in d.text.Split(' '))
                {
                    words += 1;
                    if(words > 4)
                    {
                        p2Speech.text += "\n";
                        words = 0;
                    }
                        
                    foreach(var c in s)
                    {
                        p2Speech.text += c.ToString();
                        yield return new WaitForSeconds (.01f);
                    }
                    p2Speech.text += " ";
                    yield return new WaitForSeconds (.05f);
                }
            }
            words = 0;
            yield return new WaitForSeconds(1);
        }
        DialogList = new List<Dialog>();
        p1Bubble.SetActive(false);
        p2Bubble.SetActive(false);
        Movable = true;
        Move2.movable = true;
        yield return 0;
    }

    /*
    public static IEnumerator P1Msg()
    {
        p1Gui.guiText.text = "";
        yield return WaitForSeconds(4);
        p1Gui.guiText.text = "";
        yield return 0;
    }
    
    public static IEnumerator P2Msg(string message)
    {
        p2Gui.guiText.text = message;
        yield return WaitForSeconds(4);
        p2Gui.guiText.text = "";
        yield return 0;
    }
(*/
    void WalkingSound()
    {

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal") )
        {
            var walking_gravel = gameObject.GetComponent<AudioSource>().clip;
            audio.Play();
        }
        else
        {
            audio.Stop();
        }
    }
    public static IEnumerator Die()
    {
        yield return new WaitForSeconds(2.5f);
        Application.LoadLevel(Application.loadedLevel); 
        yield return 0;
    }
    public static void Death()
    {
        SavedVars.Instance.StartCoroutine(Move1.Die());
    }
}
 
public class Dialog
{
    public string player, text;
    public Dialog(string player, string text)
    {
        this.player = player;
        this.text = text;
    }
}






