﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Benday.JsonUtilities;
public class JsonEditorV2
{
    private readonly JsonNode _rootNode;
    public JsonEditorV2(string filePath) : this(File.ReadAllText(filePath), true)
    {

    }

    public JsonEditorV2(string json, bool loadFromString)
    {
        if (loadFromString == false)
        {
            throw new InvalidOperationException("Argument not valid on this constructor.");
        }

        if (string.IsNullOrEmpty(json))
            throw new ArgumentException($"{nameof(json)} is null or empty.", nameof(json));

        var temp = JsonObject.Parse(json);

        if (temp != null)
        {
            _rootNode = temp;
        }
        else
        {
            throw new InvalidOperationException($"Could not parse JsonNode from json.");
        }
    }

    public string? GetValue(params string[] nodes)
    {
        if (nodes == null || nodes.Length == 0)
            throw new ArgumentException(
            $"{nameof(nodes)} is null or empty.", nameof(nodes));

        var node = GetNode(nodes);

        if (node == null)
        {
            return null;
        }
        else
        {
            return node.ToString();
        }
    }

    private JsonNode? GetNode(params string[] nodes)
    {
        if (nodes == null || nodes.Length == 0)
            throw new ArgumentException(
            $"{nameof(nodes)} is null or empty.", nameof(nodes));

        var parent = _rootNode;

        if (parent == null)
        {
            throw new InvalidOperationException($"Root node was null.");
        }

        JsonNode? node;
        bool success = false;

        for (int index = 0; index < nodes.Length; index++)
        {
            node = parent![nodes[index]];

            if (node == null)
            {
                success = false;
            }
            else
            {
                success = true;
            }

            if (success == false)
            {
                return null;
            }
            else if (index == nodes.Length - 1)
            {
                // found what we want
                return node;
            }
            else
            {
                // keep searching
                parent = node;
            }
        }

        return null;
    }

    /*
    private void CreateNodeStructure(string[] nodes)
    {
        if (nodes == null || nodes.Length == 0)
            throw new ArgumentException($"{nameof(nodes)} is null or empty.", nameof(nodes));

        JObject parent = null;

        for (var i = 0; i < nodes.Length; i++)
        {
            var current = GetJToken(_json,
                GetJsonQueryForNodes(nodes.Take(i + 1).ToArray()));

            if (current == null)
            {
                if ((nodes.Length - i) > 1)
                {
                    // node is somewhere in the middle of structure
                    var tempContainer = new JObject();
                    var temp = new JProperty(nodes[i], tempContainer);

                    if (parent == null)
                    {
                        _json.Add(temp);
                    }
                    else
                    {
                        parent.Add(temp);
                    }

                    parent = tempContainer;
                }
                else
                {
                    // end of node structure
                    var temp = new JProperty(nodes[i], string.Empty);

                    if (parent == null)
                    {
                        _json.Add(temp);
                    }
                    else
                    {
                        parent.Add(temp);
                    }
                }
            }
            else
            {
                parent = (JObject)current;
            }
        }
    }
    */

    public void SetValue(string nodeValue, params string[] nodes)
    {
        if (string.IsNullOrEmpty(nodeValue))
            throw new ArgumentException($"{nameof(nodeValue)} is null or empty.", nameof(nodeValue));
        if (nodes == null || nodes.Length == 0)
            throw new ArgumentException(
            $"{nameof(nodes)} is null or empty.", nameof(nodes));

        var match = GetNode(nodes);

        if (match == null)
        {
            CreateNodeStructureAndSetValue(nodeValue, nodes);
        }
        else
        {
            var propertyName = nodes.Last();

            var parent = match.Parent;

            if (parent == null)
            {
                throw new InvalidOperationException($"Parent is null");
            }

            parent[propertyName] = nodeValue;
        }
    }

    private void CreateNodeStructureAndSetValue(string nodeValue, params string[] nodes)
    {
        if (string.IsNullOrEmpty(nodeValue))
            throw new ArgumentException($"{nameof(nodeValue)} is null or empty.", nameof(nodeValue));
        if (nodes == null || nodes.Length == 0)
            throw new ArgumentException(
            $"{nameof(nodes)} is null or empty.", nameof(nodes));

        var parent = _rootNode;

        JsonNode? node;
        bool success = false;

        for (int index = 0; index < nodes.Length; index++)
        {
            node = parent![nodes[index]];

            if (node != null)
            {
                parent = node;
            }
            else
            {
                if (index == nodes.Length - 1)
                {
                    // set the value
                    parent[nodes[index]] = nodeValue;
                }
                else
                {
                    node = new JsonObject();

                    parent[nodes[index]] = node;

                    parent = node;
                }
            }            
        }
    }

    public string? GetSiblingValue(SiblingValueArguments args)
    {
        var parentNode = FindParentNodeBySiblingValue(args);

        if (parentNode == null)
        {
            return null;
        }
        else
        {
            var match = parentNode[args.DesiredNodeKey];

            if (match == null)
            {
                return null;
            }
            else
            {
                return match.ToString();                
            }
        }
    }

    public void SetSiblingValue(SiblingValueArguments args)
    {
        throw new NotImplementedException();

        //var parentNode = FindParentNodeBySiblingValue(args);

        //if (parentNode == null)
        //{
        //    return;
        //}
        //else
        //{
        //    parentNode[args.DesiredNodeKey] = args.DesiredNodeValue;
        //}
    }
    
    private JsonNode? FindParentNodeBySiblingValue(SiblingValueArguments args)
    {
        var collectionMatch = GetNode(args.PathArguments);

        if (collectionMatch == null)
        {
            return null;
        }
        else if (collectionMatch is JsonArray)
        {
            var matches = (JsonArray) collectionMatch;

            foreach (var item in matches)
            {
                if (item == null)
                {
                    continue;
                }
                else if (item[args.SiblingSearchKey] != null &&
                    item[args.SiblingSearchKey]!.ToString() == args.SiblingSearchValue)
                {
                    return item;
                }
            }

            return null;
        }        
        else
        {
            return null;
        }

        //collectionMatch.

        //var matches = collectionMatch.Children().ToList();

        //if (matches == null || matches.Count == 0)
        //{
        //    return null;
        //}
        //else
        //{
        //    foreach (var item in matches)
        //    {
        //        if (item.HasValues == true)
        //        {
        //            if (item[args.SiblingSearchKey] != null &&
        //                item[args.SiblingSearchKey].Value<string>() == args.SiblingSearchValue)
        //            {
        //                return item;
        //            }
        //        }
        //    }

        //    return null;
        //}
    }
}
