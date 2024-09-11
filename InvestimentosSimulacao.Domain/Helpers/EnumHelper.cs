using System.ComponentModel;
using System.Reflection;

namespace InvestimentosSimulacao.Domain.Helpers;

public class EnumHelper
{
    public static string ObterDescricaoEnum<T>(T enumValue) where T : Enum
    {
        FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute == null ? enumValue.ToString() : attribute.Description;
    }
}