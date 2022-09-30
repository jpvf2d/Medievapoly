using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoardSpaces : MonoBehaviour
{
    public Transform[] boardSpaces;
    [SerializeField] private float moveSpeed = 1f;
    [HideInInspector] public int boardSpaceIndex = 0;
    public bool move = false;


    // Start is called before the first frame update
    private void Start()
    {
        transform.position = boardSpaces[boardSpaceIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if(move)
            Move();
    }

    private void Move()
    {
        if(boardSpaceIndex <= boardSpaces.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, boardSpaces[boardSpaceIndex].transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == boardSpaces[boardSpaceIndex].transform.position)
            {
                boardSpaceIndex += 1;
            }
        }
        
    }
}
