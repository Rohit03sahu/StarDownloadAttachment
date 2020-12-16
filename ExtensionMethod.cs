using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ExtensionMethod
{

    public static int ToInt32(this string value)
    {
        if (IsNullOrEmpty(value))
            return default;
        int.TryParse(value, out int outValue);
        return outValue;
    }

    public static float ToFloat(this string value)
    {
        if (IsNullOrEmpty(value))
            return default(float);
        float.TryParse(value, out float outValue);
        return outValue;
    }

    public static bool IsNumeric(this string input)
    {
        return int.TryParse(input, out _);
    }
    public static string ToByteArrayToString(this byte[] value)
    {
        return System.Text.Encoding.UTF8.GetString(value);
    }
    public static double ToDouble(this string value)
    {
        if (IsNullOrEmpty(value))
            return default(double);
        return Convert.ToDouble(value);
    }

    public static decimal ToDecimal(this string value)
    {
        if (IsNullOrEmpty(value))
            return default(decimal);
        decimal.TryParse(value, out decimal outValue);
        return outValue;
    }


    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNotNullOrEmpty(this string value)
    {
        return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
    }

    public static bool IsValid<T>(this List<T> value)
    {
        return value != null && value.Count > 0 && value.Any();
    }

    public static bool IsValid<T>(this T value)
    {
        return value != null;
    }

    public static bool ConvertToBool(this string str)
    {
        bool.TryParse(str, out bool result);
        return result;
    }

    public static double ComparePercentage(this string str1, string str2)
    {
        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2) || str1.Trim() == "" || str2.Trim() == "")
            return 0;

        str1 = " " + str1.Replace(" ", string.Empty);
        str2 = " " + str2.Replace(" ", string.Empty);

        str1 = str1.ToLower();
        str2 = str2.ToLower();

        int possible = str1.Length + str2.Length - 2;
        int hits = 0;
        Task<int> Task1 = Task.Factory.StartNew<int>(() => GetHitCount(str1, str2));
        Task<int> Task2 = Task.Factory.StartNew<int>(() => GetHitCount(str1, str2));
        Task1.Wait();
        hits += Task1.Result;
        Task2.Wait();
        hits += Task2.Result;
        return Math.Round((double)((100 * hits) / possible), 2);
    }


    private static int GetHitCount(string str1, string str2)
    {
        int hits = 0;
        for (int i = 0; i <= str1.Length - 1; i++)
        {
            int length = (i == str1.Length - 1) ? 1 : 2;
            if (str2.IndexOf(str1.Substring(i, length)) != -1)
                hits++;
        }
        return hits;
    }

    public static string ToJson(this object model)
    {
        return JsonConvert.SerializeObject(model);
    }

    public static string ToJsonWithExpando(this object model)
    {
        return System.Text.Json.JsonSerializer.Serialize(model);
    }

    public static string SerializeObject(this object value)
    {
        return JsonConvert.SerializeObject(value);
    }

    public static T DeserializeObject<T>(this string value)
    {
        T response = default;
        try
        {
            response = JsonConvert.DeserializeObject<T>(value);
        }
        catch (Exception ex)
        {
        }
        return response;
    }
    public static bool HasRecords(this object value)
    {
        bool response = true;
        response = value != null;
        if (value is ICollection)
        {
            response = response && (value as ICollection).Count > 0;
        }
        return response;
    }
}
