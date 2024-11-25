using System.ComponentModel;
using System.Reflection;

namespace Zaczytani.Domain.DescriptionEnumConver;

public static class EnumHelper
{
    public static List<string> GetEnumDescriptions<T>() where T : Enum
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Select(field => field.GetCustomAttribute<DescriptionAttribute>()?.Description ?? field.Name)
                        .ToList();
    }
}