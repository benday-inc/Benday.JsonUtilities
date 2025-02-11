using System;
using System.Linq;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace Benday.JsonUtilities;



public static class JsonExtensionMethods
{
    public static string GetString(this JsonNode? node, string propertyName)
    {
        if (node == null)
        {
            return string.Empty;
        }
        else
        {
            var match = node[propertyName];

            if (match == null)
            {
                return string.Empty;
            }
            else
            {
                return match.ToString();
            }
        }
    }

    public static int GetInt32(this JsonNode? node, string propertyName)
    {
        if (node == null)
        {
            return 0;
        }
        else
        {
            var match = node[propertyName];

            if (match == null)
            {
                return 0;
            }
            else
            {
                var valueAsString = match.ToString();

                if (int.TryParse(valueAsString, out int result) == true)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public static JsonNode? FirstOrDefaultWithPropertyName(
        this JsonArray? array,
        string propertyName)
    {
        if (array == null)
        {
            return null;
        }

        foreach (var item in array)
        {
            if (item == null)
            {
                continue;
            }
            else if (item[propertyName] != null)
            {
                return item[propertyName];
            }
        }

        return null;
    }

    public static JsonArray? GetArray(
        this JsonNode? node,
        string propertyName)
    {
        if (node == null)
        {
            return null;
        }

        // get reference to array property
        var array = node[propertyName];

        if (array == null)
        {
            return null;
        }
        else
        {
            if (array is JsonArray valueAsArray)
            {
                return valueAsArray;
            }
            else
            {
                return null;
            }
        }
    }

    public static JsonNode? GetArrayItem(
        this JsonNode? node,
        string arrayPropertyName,
        string searchPropertyName, string searchPropertyValue)
    {
        if (node == null)
        {
            return null;
        }

        var array = node.GetArray(arrayPropertyName);

        if (array == null)
        {
            return null;
        }
        else
        {
            foreach (var item in array)
            {
                if (item == null)
                {
                    continue;
                }
                else if (item[searchPropertyName] != null &&
                    item[searchPropertyName]!.ToString() == searchPropertyValue)
                {
                    return item;
                }
            }

            return null;
        }
    }
}