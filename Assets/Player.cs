using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] boardSpaces;
    [SerializeField] private float moveSpeed = 1f;
    [HideInInspector] public int boardSpaceIndex = 0;
    public bool move = false;
    public float money = 0;
    public int[] property;
    public string name;


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
        if(boardSpaceIndex <= boardSpaces.Length - 1 && transform.position != boardSpaces[boardSpaceIndex].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, boardSpaces[boardSpaceIndex].transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            move = false;
			PropertySpace.runCoroutine = true; 
        }
    }
}
