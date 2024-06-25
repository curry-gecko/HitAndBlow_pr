
using System;
using System.ComponentModel;
using System.Reflection;

public enum SuitType
{
    [Description("Spade")]
    Spade,
    [Description("Club")]
    Club,
    [Description("Diamond")]
    Diamond,
    [Description("Heart")]
    Heart
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }
}

