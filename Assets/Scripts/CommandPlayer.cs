using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPlayer : MonoBehaviour
{
    private Queue<Command> _commandsWaitList;
    private Stack<Command> _usedCommands;

    private void Awake()
    {
        _commandsWaitList = new Queue<Command>();
        _usedCommands = new Stack<Command>();
    }

    void Update()
    {
        ReadCommands();
        ExecuteCommands();
    }

    public void ReadCommands()
    {
        if(Input.GetKeyDown(KeyCode.W))
            _commandsWaitList.Enqueue(new MoveCommand(transform , new Vector2(0,1)));
        
        if(Input.GetKeyDown(KeyCode.A))
            _commandsWaitList.Enqueue(new MoveCommand(transform , new Vector2(-1,0)));
        
        if(Input.GetKeyDown(KeyCode.S))
            _commandsWaitList.Enqueue(new MoveCommand(transform , new Vector2(0,-1)));
        
        if(Input.GetKeyDown(KeyCode.D))
            _commandsWaitList.Enqueue(new MoveCommand(transform , new Vector2(1,0)));
    }

    public void ExecuteCommands()
    {
        if (_commandsWaitList.Count > 0)
        {
          Command tmp =  _commandsWaitList.Dequeue();
          _usedCommands.Push(tmp);
          
          tmp.Execute();
        }
    }

    public void Undo()
    {
        if(_usedCommands.Count > 0)
            _usedCommands.Pop().UnDo();
    }
}

public abstract class Command
{
    public abstract void Execute();
    public abstract void UnDo();
}

public class MoveCommand : Command
{
    private Vector3 _direction;
    private Transform _player;
    
    public MoveCommand(Transform player , Vector3 moveDirection)
    {
       _player = player;
       _direction = moveDirection;
    }
    public override void Execute()
    {
        _player.transform.position += _direction;
    }

    public override void UnDo()
    {
        _player.transform.position -= _direction;
    }
}
