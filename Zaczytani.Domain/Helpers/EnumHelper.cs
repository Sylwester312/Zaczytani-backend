using System.ComponentModel;
using System.Reflection;

namespace Zaczytani.Domain.Helpers;

public static class EnumHelper
{
    public static List<string> GetEnumDescriptions<T>() where T : Enum
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Select(field => field.GetCustomAttribute<DescriptionAttribute>()?.Description ?? field.Name)
                        .ToList();
    }

    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
    }
}