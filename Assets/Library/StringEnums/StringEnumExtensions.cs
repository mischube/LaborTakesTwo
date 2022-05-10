using System;
using Global;
using UnityEngine.SceneManagement;

namespace Library.StringEnums
{
    public static class StringEnumExtensions
    {
        public static string GetStringValue(this Scenes scenes)
        {
            var type = scenes.GetType();
            var fieldInfo = type.GetField(scenes.ToString());

            var attribs = fieldInfo.GetCustomAttributes(
                typeof(StringEnum), false) as StringEnum[];

            if (attribs is null || attribs.Length == 0)
            {
                return scenes.ToString();
            }

            return attribs[0].StringValue;
        }

        public static Scenes GetEnumValue(this Scene scene)
        {
            var success = Enum.TryParse<Scenes>(scene.name, out var scenesEnumValue);
            return success ? scenesEnumValue : throw new InvalidCastException();
        }
    }
}