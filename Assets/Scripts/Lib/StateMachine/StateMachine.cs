using System;
using System.Collections.Generic;

public class StateMachine
{
    private readonly Dictionary<string, System.Object> _blackboard = new Dictionary<string, object>(100);
    private readonly Dictionary<string, IStateNode> _nodes = new Dictionary<string, IStateNode>(100);
    private IStateNode _curNode;
    private IStateNode _preNode;

    /// <summary>
    /// ״̬��������
    /// </summary>
    public System.Object Owner { private set; get; }

    /// <summary>
    /// ��ǰ���еĽڵ�����
    /// </summary>
    public string CurrentNode
    {
        get { return _curNode != null ? _curNode.GetType().FullName : string.Empty; }
    }

    /// <summary>
    /// ֮ǰ���еĽڵ�����
    /// </summary>
    public string PreviousNode
    {
        get { return _preNode != null ? _preNode.GetType().FullName : string.Empty; }
    }


    private StateMachine() { }
    public StateMachine(System.Object owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// ����״̬��
    /// </summary>
    public void Update()
    {
        if (_curNode != null)
            _curNode.OnUpdate();
    }

    /// <summary>
    /// ����״̬��
    /// </summary>
    public void Run<TNode>() where TNode : IStateNode
    {
        var nodeType = typeof(TNode);
        var nodeName = nodeType.FullName;
        Run(nodeName);
    }
    public void Run(Type entryNode)
    {
        var nodeName = entryNode.FullName;
        Run(nodeName);
    }
    public void Run(string entryNode)
    {
        _curNode = TryGetNode(entryNode);
        _preNode = _curNode;

        if (_curNode == null)
            throw new Exception($"Not found entry node: {entryNode}");

        _curNode.OnEnter();
    }

    /// <summary>
    /// ����һ���ڵ�
    /// </summary>
    public void AddNode<TNode>() where TNode : IStateNode
    {
        var nodeType = typeof(TNode);
        var stateNode = Activator.CreateInstance(nodeType) as IStateNode;
        AddNode(stateNode);
    }
    public void AddNode(IStateNode stateNode)
    {
        if (stateNode == null)
            throw new ArgumentNullException();

        var nodeType = stateNode.GetType();
        var nodeName = nodeType.FullName;

        if (_nodes.ContainsKey(nodeName) == false)
        {
            stateNode.OnCreate(this);
            _nodes.Add(nodeName, stateNode);
        }
        else
        {
            UnityEngine.Debug.LogError($"State node already existed : {nodeName}");
        }
    }

    /// <summary>
    /// ת��״̬�ڵ�
    /// </summary>
    public void ChangeState<TNode>() where TNode : IStateNode
    {
        var nodeType = typeof(TNode);
        var nodeName = nodeType.FullName;
        ChangeState(nodeName);
    }
    public void ChangeState(Type nodeType)
    {
        var nodeName = nodeType.FullName;
        ChangeState(nodeName);
    }
    public void ChangeState(string nodeName)
    {
        if (string.IsNullOrEmpty(nodeName))
            throw new ArgumentNullException();

        IStateNode node = TryGetNode(nodeName);
        if (node == null)
        {
            UnityEngine.Debug.LogError($"Can not found state node : {nodeName}");
            return;
        }

        UnityEngine.Debug.Log($"{_curNode.GetType().FullName} --> {node.GetType().FullName}");
        _preNode = _curNode;
        _curNode.OnExit();
        _curNode = node;
        _curNode.OnEnter();
    }

    /// <summary>
    /// ���úڰ�����
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetBlackboardValue(string key, System.Object value)
    {
        if (_blackboard.ContainsKey(key) == false)
            _blackboard.Add(key, value);
        else
            _blackboard[key] = value;
    }

    /// <summary>
    /// ��ȡ�ڰ�����
    /// </summary>
    public System.Object GetBlackboardValue(string key)
    {
        if (_blackboard.TryGetValue(key, out System.Object value))
        {
            return value;
        }
        else
        {
            UnityEngine.Debug.LogWarning($"Not found blackboard value : {key}");
            return null;
        }
    }

    private IStateNode TryGetNode(string nodeName)
    {
        _nodes.TryGetValue(nodeName, out IStateNode result);
        return result;
    }
}