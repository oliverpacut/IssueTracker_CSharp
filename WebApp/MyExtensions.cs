using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp
{
    public static class MyExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = (value.Equals(selectedValue))
                };

            return htmlHelper.EnumDropDownList
                (
                name,
                items
                );
        }

        public static IDictionary<char, string> ParseArgs(string arguments)
        {
            var returnDict = new Dictionary<char, string>();

            foreach(var item in arguments.Split(';'))
            {
                returnDict.Add(item.Split('=').ToArray()[0].First(), item.Split('=').ToArray()[1]);
            }

            arguments.Split(';').ToList().ForEach(y => returnDict.Add(y.Split('=')[0].First(), y.Split('=')[1]));

            return returnDict;
        }
    }
}