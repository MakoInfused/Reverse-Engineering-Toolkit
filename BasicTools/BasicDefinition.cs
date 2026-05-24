using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BasicTools
{
    public class IgnoreCollectionIndentingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsArray || objectType.GetInterfaces()
                            .Any(x => x.IsGenericType &&
                                x.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        public override bool CanRead => false;
        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (var item in value as IEnumerable)
            {
                var json = JsonConvert.SerializeObject(item, Formatting.Indented);
                json = Regex.Replace(json, @"(\s)+", (Match match) =>
                {
                    return " ";
                });
                writer.WriteRawValue(json);
            }
            writer.WriteEndArray();
        }
    }

    public static class JsonConvertEx
    {
        public static string SerializeObject<T>(T value, JsonSerializerSettings settings)
        {
            StringBuilder sb = new StringBuilder(256);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);

            var jsonSerializer = JsonSerializer.CreateDefault(settings);
            using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.IndentChar = ' ';
                jsonWriter.Indentation = 4;
                jsonSerializer.Serialize(jsonWriter, value, typeof(T));
            }

            return sw.ToString();
        }
    }

    public class BasicJsonDefinition<T> : IBasicDefinition<T>
    {
        public T Definition { get; set; }

        private Encoding _Encoding;
        public Encoding Encoding
        {
            get
            {
                if(_Encoding == null)
                {
                    _Encoding = Encoding.UTF8;
                }
                return _Encoding;
            }
            private set
            {
                _Encoding = value;
            }
        }

        public BasicJsonDefinition(byte[] data, Encoding encoding = null)
        {
            Encoding = encoding;
            string json = Encoding.GetString(data);

            LoadDefinition(json);
        }

        public BasicJsonDefinition(string json)
        {
            LoadDefinition(json);
        }

        private void LoadDefinition(string json)
        {
            try
            {
                Definition = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                throw new Exception("Unable to load Definitions file.");
            }
        }

        public void FromDefinition(IBasicDefinition<T> otherDefinition)
        {
            LoadDefinition(ToJson(otherDefinition.Definition));
        }

        public byte[] ToBytes()
        {
            return Encoding.GetBytes(ToJson());
        }

        public IBasicDefinition<T> Clone()
        {
            return new BasicJsonDefinition<T>(ToJson());
        }

        private string ToJson(T definition)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };
            settings.Converters.Add(new IgnoreCollectionIndentingConverter());

            return JsonConvertEx.SerializeObject(definition, settings);
        }

        private string ToJson()
        {
            return ToJson(Definition);
        }
    }

    public class BasicDefinitionContainer<T> : IBasicDefinitionContainer
    {
        public IBasicDefinition<T> Current { get; }
        public IBasicDefinition<T> Template { get; }
        public object Default => Template.Definition;

        public BasicDefinitionContainer(IBasicDefinition<T> template, IBasicDefinition<T> current = null)
        {
            Template = template;
            Current = current != null ? current : template.Clone();
        }

        public void Reload(IBasicDefinition<T> data = null)
        {
            Current.FromDefinition(data ?? Template);
        }
    }
}
