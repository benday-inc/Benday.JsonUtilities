using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Benday.JsonUtilities;
public class JsonEditorV2
{
    private readonly JsonElement _json;

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

        _json = JsonDocument.Parse(json).RootElement;
    }

    private JsonEditorV2(JsonElement fromObject)
    {
        _json = fromObject;
    }

    public string? GetValue(params string[] nodes)
    {
        if (nodes == null || nodes.Length == 0)
            throw new ArgumentException(
            $"{nameof(nodes)} is null or empty.", nameof(nodes));

        var element = GetElement(nodes);

        if (element == null || element.HasValue == false)
        {
            return null;
        }
        else
        {
            return element.Value.GetString();
        }

        /*
        var query = GetJsonQueryForNodes(nodes);

        return GetValueUsingQuery(query.ToString());
        */
    }

    private JsonElement? GetElement(params string[] nodes)
    {
        if (nodes == null || nodes.Length == 0)
            throw new ArgumentException(
            $"{nameof(nodes)} is null or empty.", nameof(nodes));

        var parent = _json;
        JsonElement element;
        bool success = false;

        for (int index = 0; index < nodes.Length; index++)
        {
            success = parent.TryGetProperty(nodes[index], out element);

            if (success == false)
            {
                return null;
            }
            else if (index == nodes.Length - 1)
            {
                // found what we want
                return element;
            }
            else
            {
                // keep searching
                parent = element;
            }
        }

        return null;
    }

    private string GetJsonQueryForNodes(params string[] nodes)
    {
        var needsPeriod = false;

        var query = new StringBuilder();

        foreach (var node in nodes)
        {
            if (needsPeriod == true)
            {
                query.Append(".");
            }

            query.Append(node);

            needsPeriod = true;
        }

        return query.ToString();
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
        throw new NotImplementedException();
        //if (string.IsNullOrEmpty(nodeValue))
        //    throw new ArgumentException($"{nameof(nodeValue)} is null or empty.", nameof(nodeValue));
        //if (nodes == null || nodes.Length == 0)
        //    throw new ArgumentException(
        //    $"{nameof(nodes)} is null or empty.", nameof(nodes));

        //var query = GetJsonQueryForNodes(nodes);

        //var match = GetJToken(_json, query);

        //if (match != null)
        //{
        //    match.Replace(new JValue(nodeValue));
        //}
        //else
        //{
        //    CreateNodeStructure(nodes);
        //    SetValue(nodeValue, nodes);
        //}

        //WriteJsonFile();
    }
    

    /*
    private JObject LoadJsonFromFile(string pathToFile)
    {
        AssertFileExists(pathToFile);

        var jsonText = File.ReadAllText(pathToFile);

        var json = JObject.Parse(jsonText);

        return json;
    }

    private void AssertFileExists(string pathToFile)
    {
        if (File.Exists(pathToFile) == false)
        {
            throw new FileNotFoundException("File not found.", pathToFile);
        }
    }
    */

    public string GetSiblingValue(SiblingValueArguments args)
    {
        throw new NotImplementedException();

        //var parentNode = FindParentNodeBySiblingValue(args);

        //if (parentNode == null)
        //{
        //    return null;
        //}
        //else
        //{
        //    var match = parentNode[args.DesiredNodeKey];

        //    if (match == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return match.Value<string>();
        //    }
        //}
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

    /*
    private JToken FindParentNodeBySiblingValue(SiblingValueArguments args)
    {
        var collectionMatch = GetJToken(
            _json, GetJsonQueryForNodes(args.PathArguments));

        if (collectionMatch == null)
        {
            return null;
        }

        var matches = collectionMatch.Children().ToList();

        if (matches == null || matches.Count == 0)
        {
            return null;
        }
        else
        {
            foreach (var item in matches)
            {
                if (item.HasValues == true)
                {
                    if (item[args.SiblingSearchKey] != null &&
                        item[args.SiblingSearchKey].Value<string>() == args.SiblingSearchValue)
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }
    */
}
