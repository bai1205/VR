using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ConfigManager : MonoSingleton<ConfigManager>
{
    public Dictionary<int, SkillData> SkillDic { get; private set; } = new Dictionary<int, SkillData>();

    private readonly JsonSerializerSettings settings = new JsonSerializerSettings
    {
        Converters = new JsonConverter[]
        {
            new FloatArrayConverter(),
            new IntArrayConverter(),
            new StringArrayConverter(),
        }
    };

    public void Init()
    {
        SkillDic = Load<SkillData>("Config/SkillData");
    }

    public SkillData GetSkillData(int id)
    {
        if (!SkillDic.TryGetValue(id, out SkillData originalSkillData))
        {
            return null;
        }

        SkillData skillData = new SkillData();
        CopyFields(skillData, originalSkillData);
        return skillData;
    }

    private void CopyFields(object target, object source)
    {
        Type type = source.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            field.SetValue(target, field.GetValue(source));
        }
    }

    private Dictionary<int, T> Load<T>(string path)
    {
        string json = Resources.Load<TextAsset>(path).text;
        return JsonConvert.DeserializeObject<Dictionary<int, T>>(json, settings);
    }

    public class FloatArrayConverter : JsonConverter<float[]>
    {
        public override float[] ReadJson(JsonReader reader, Type objectType, float[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                string[] values = token.ToString().Replace("[", "").Replace("]", "").Split(',');
                float[] result = new float[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    float.TryParse(values[i], out result[i]);
                }
                return result;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, float[] value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class IntArrayConverter : JsonConverter<int[]>
    {
        public override int[] ReadJson(JsonReader reader, Type objectType, int[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                string[] values = token.ToString().Replace("[", "").Replace("]", "").Split(',');
                int[] result = new int[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    int.TryParse(values[i], out result[i]);
                }
                return result;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, int[] value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class StringArrayConverter : JsonConverter<string[]>
    {
        public override string[] ReadJson(JsonReader reader, Type objectType, string[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                return token.ToString().Replace("[", "").Replace("]", "").Split(',');
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, string[] value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
