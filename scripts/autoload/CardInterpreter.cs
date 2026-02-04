using System.Linq;
using Godot;

public static class CardInterpreter
{
    public static object ExecuteNode(CardGraphNode graphNode) //По идее это должен быть метод ноды
    {
        GraphEdit graphEdit = graphNode.GetParent<GraphEdit>();
        object[] inputValues = new object[graphNode.GetInputPortCount()];

        GD.Print($"Collecting input values for {graphNode.Title} {graphNode}");
        foreach (Godot.Collections.Dictionary connection in graphEdit.GetConnectionListFromNode(graphNode.Name))
        {
            if ((StringName)connection["from_node"] == graphNode.Name) //Если это выходное соединение
            {
                if ((graphNode.GetOutputPortType((int)connection["from_port"]) == 0) && (graphNode.GetOutputPortSlot((int)connection["from_port"]) == 0)) //Если выходное соединение на нулевой слот с типом 0 (поток выполнения)
                {
                    graphNode.NextNode = graphEdit.GetNode<CardGraphNode>(connection["to_node"].ToString());
                    GD.Print($"Set NextNode {graphNode.Title} -> {graphNode.NextNode.Title}");
                }
                continue;
            }
            if ((graphNode.GetInputPortType((int)connection["from_port"]) == 0) && (graphNode.GetInputPortSlot((int)connection["to_port"]) == 0)) { continue; } //Если входное соединение на нулевой слот с типом 0 (поток выполнения), пропускаем
            inputValues.Append(ExecuteNode(graphEdit.GetNode<CardGraphNode>(connection["from_node"].ToString()))); //Нужно добавить кэширование, которое может всё сломать
        }
        
        GD.Print($"Executing {graphNode.Title} {graphNode}");
        object returnValue = inputValues.Length; //Тут должно вычисляться выходное значение
        GD.Print($"Completed {graphNode.Title} {graphNode}, returning {returnValue}");

        return returnValue;
    }

    public static void StartSequence(CardGraphNode startNode)
    {
        ExecuteNode(startNode);
        if (startNode.NextNode is null)
        {
            GD.Print($"Executing completed");
            return;
        }
        StartSequence(startNode.NextNode);
    }
}