using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] boardSpaces;
    [SerializeField] public float moveSpeed = 1f;
    [HideInInspector] public int boardSpaceIndex = 0;
    public bool move = false;
    public bool stuckInPlace = false; //Determine if player stuck in jail or not 
    public float money = 0;
    public List<int> property; //Use list to easily add purchaed properties 
    public string name;
    public int playersIndex; 


    // Start is called before the first frame update
    private void Start()
    {
        money = 3000; //Change to whatever
        transform.position = boardSpaces[boardSpaceIndex].transform.position;
        List<int> property = new List<int>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(move)
            Move();

    }

    private void Move()
    {
        if(boardSpaceIndex <= boardSpaces.Length - 1 && transform.position != boardSpaces[boardSpaceIndex].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, boardSpaces[boardSpaceIndex].transform.position, moveSpeed * Time.deltaTime);        
        }
        else
        {
            move = false;
            //Player finishes moving and interacts with space
            //Do not interact with 'Jail' space if player was sent via landing on 'Go to Jail'
            if(!GoToJailSpace.sentToJail)  
                SpaceLogic.coroutine_sl = true; 
           
            else
                GoToJailSpace.sentToJail = false; 
        }
    }
}
