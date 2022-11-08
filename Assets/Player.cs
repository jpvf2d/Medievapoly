using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] boardSpaces;
    [SerializeField] public float moveSpeed = 1f;
    [HideInInspector] public float tmpMoveSpeed = 0f;
    [HideInInspector] public int boardSpaceIndex = 0;
    public bool move = false;
    public bool stuckInPlace = false; //Determine if player stuck in jail or not 
    public float money = 0;
    public List<int> property; //Use list to easily add purchaed properties 
    public string name;
    public int playersIndex; 
    public bool jailFreeCard = false;
    [HideInInspector] public bool relocated = false;
    [HideInInspector] public int communityFund = 0;
    [HideInInspector] public int numUtilities = 0;
    [HideInInspector] public int numRailroads = 0;
    public Dictionary<string, int> fullSetTracker = new Dictionary<string, int>(); // see 'LoadFSTDictionary'
    private Animator anim;
    public bool justLost = false;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool justWon = false;
    [HideInInspector] public bool isWinner = false;
    public SpriteRenderer spriteRenderer;
    public Sprite burnt;
    public GameObject WinPanel;
    [SerializeField] public bool autoPlayEnabled = false;



    // Start is called before the first frame update
    private void Start()
    {
        // moveSpeed = 40f; //Remove after testing
        money = 1500;
        transform.position = boardSpaces[boardSpaceIndex].transform.position;
        List<int> property = new List<int>();
        anim = GetComponent<Animator>();        
        //WinPanel = GameObject.Find("WinPanel");
        WinPanel.SetActive(false);
        LoadFSTDictionary(); 
    }

    // Update is called once per frame
    private void Update()
    {
        if(move)
            Move();

        if(money < 0 && isDead == false){
            justLost = true;
        }

        if(justLost)
        {
            KillPlayer();
        }

        if(justWon){
            DeclareWinner();
        }
    }

    private void Move()
    {
        anim.SetBool("Walk", true);
        float speed = moveSpeed;
        if(this.tmpMoveSpeed > 1)
        {
            speed = tmpMoveSpeed;
        }

        if(boardSpaceIndex <= boardSpaces.Length - 1 && transform.position != boardSpaces[boardSpaceIndex].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, boardSpaces[boardSpaceIndex].transform.position, speed * Time.deltaTime);        
        }
        else
        {
            this.tmpMoveSpeed = 0f;
            move = false;
            //Player finishes moving and interacts with space
            //Do not interact with 'Jail' space if player was sent via landing on 'Go to Jail'
            if(!GoToJailSpace.sentToJail)  
                SpaceLogic.coroutine_sl = true; 
           
            else
                GoToJailSpace.sentToJail = false;
            anim.SetBool("Walk", false);
        }
    }

    private void KillPlayer(){
        isDead = true;
        money = 0;
        GameplaySystem.alivePlayers -= 1;

        // Makes cards available again
        GameObject boardSpacesIndexed = GameObject.Find("BoardSpaces");
        BoardSpace[] boardSpacesArray = boardSpacesIndexed.GetComponent<SpaceLogic>().allBoardSpaces;
        foreach (int index in property){
            boardSpacesArray[index].GetComponent<PropertySpace>().owned = false;
        }

        // Kill visual
        anim.enabled = false;
        spriteRenderer.sprite = burnt;
        Invoke("Rotate", 1);
        justLost = false;
        Debug.Log("Player has been killed!");
    }

    private void Rotate(){
        transform.Rotate (Vector3.forward * -90);
    }

    private void DeclareWinner(){
        isWinner = true;
        WinPanel.SetActive(true);
        justWon = false;
    }

    private void LoadFSTDictionary()
    {
        this.fullSetTracker.Add("Purple", 0);
        this.fullSetTracker.Add("Red", 0);
        this.fullSetTracker.Add("Pink", 0);
        this.fullSetTracker.Add("LightB", 0);
        this.fullSetTracker.Add("Yellow", 0);
        this.fullSetTracker.Add("Orange", 0);
        this.fullSetTracker.Add("Green", 0);
        this.fullSetTracker.Add("Blue", 0);
    }
}
