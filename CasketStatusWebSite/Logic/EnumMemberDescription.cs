using System;

namespace CasketStatusWebSite.Logic
{
    // Средства для получения human readable строк из значений перечислений (enums)

    // Human readable текст для значений enum
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)] //< К сожалению нет ограничения только на значения enum-а
    public class HumanReadableTextAttribute : Attribute
    {
        // заданный текст
        public string Text { get; set; }
        public string DetailedText { get; set; }

        public HumanReadableTextAttribute(string text)
        {
            Text = text;
        }
    }

    public static class EnumExtensions
    {
        // Метод расширения, для получения human readable текста из значения перечисления
        public static string GetHumanReadableText(this Enum enumeration, bool detailed = false)
        {
            // Юзаем reflection для поиска атрибута HumanReadableAttribute

            var memberInfo = enumeration.GetType().GetMember(enumeration.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var humanReadableAttributes = memberInfo[0].GetCustomAttributes(typeof(HumanReadableTextAttribute), false);
                if (humanReadableAttributes != null && humanReadableAttributes.Length > 0)
                    // Возвращаем human readable текст из найденного атрибута
                    return detailed ? ((HumanReadableTextAttribute)humanReadableAttributes[0]).DetailedText : ((HumanReadableTextAttribute)humanReadableAttributes[0]).Text;
            }

            // В случае, если атрибута нет, берем просто строковое представление значения перечисления
            return enumeration.ToString();
        }
    }
}
