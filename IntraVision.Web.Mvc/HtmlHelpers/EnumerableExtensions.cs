using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    /// <summary>
    /// Extension methods on IEnumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts the source sequence into an IEnumerable of SelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the SelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the SelectList items</param>
        /// <returns>IEnumerable of SelectListItem</returns>
        public static IEnumerable<SelectListItem> ToSelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector)
        {
            return items.ToSelectList(valueSelector, nameSelector, x => false,null);
        }

        /// <summary>
        /// Converts the source sequence into an IEnumerable of SelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the SelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the SelectList items</param>
        /// <param name="selectedItems">Those items that should be selected</param>
        /// <returns>IEnumerable of SelectListItem</returns>
        public static IEnumerable<SelectListItem> ToSelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector, IEnumerable<TValue> selectedItems)
        {
            return items.ToSelectList(valueSelector, nameSelector, x => selectedItems != null && selectedItems.Contains(valueSelector(x)), null);
        }

        /// <summary>
        /// Converts the source sequence into an IEnumerable of SelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the SelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the SelectList items</param>
        /// <param name="selectedValueSelector">Lambda that specifies whether the item should be selected</param>
        /// <returns>IEnumerable of SelectListItem</returns>
        public static IEnumerable<SelectListItem> ToSelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector, Func<TItem, bool> selectedValueSelector)
        {
            return items.ToSelectList(valueSelector, nameSelector, selectedValueSelector, null);
        }

        /// <summary>
        /// Converts the source sequence into an IEnumerable of SelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the SelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the SelectList items</param>
        /// <param name="selectedValueSelector">Lambda that specifies whether the item should be selected</param>
        /// <returns>IEnumerable of SelectListItem</returns>
        public static IEnumerable<SelectListItem> ToSelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector, Func<TItem, bool> selectedValueSelector, string emptyText)
        {
            if (!string.IsNullOrEmpty(emptyText))
                yield return new SelectListItem { Text = emptyText };

            foreach (var item in items)
            {
                var value = valueSelector(item);

                yield return new SelectListItem
                {
                    Text = nameSelector(item),
                    Value = value.ToString(),
                    Selected = selectedValueSelector(item)
                };
            }
        }

        /// <summary>
        /// Converts the source sequence into an IEnumerable of CategorySelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the CategorySelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the CategorySelectList items</param>
        /// <returns>IEnumerable of CategorySelectListItem</returns>
        public static IEnumerable<CategorySelectListItem> ToCategorySelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector, Func<TItem, string> categorySelector)
        {
            return items.ToCategorySelectList(valueSelector, nameSelector, categorySelector, x => false, null);
        }

        /// <summary>
        /// Converts the source sequence into an IEnumerable of CategorySelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the CategorySelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the CategorySelectList items</param>
        /// <param name="selectedItems">Those items that should be selected</param>
        /// <returns>IEnumerable of CategorySelectListItem</returns>
        public static IEnumerable<CategorySelectListItem> ToCategorySelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector, Func<TItem, string> categorySelector, IEnumerable<TValue> selectedItems)
        {
            return items.ToCategorySelectList(valueSelector, nameSelector, categorySelector, x => selectedItems != null && selectedItems.Contains(valueSelector(x)), null);
        }

        /// <summary>
        /// Converts the source sequence into an IEnumerable of CategorySelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the CategorySelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the CategorySelectList items</param>
        /// <param name="selectedValueSelector">Lambda that specifies whether the item should be selected</param>
        /// <returns>IEnumerable of CategorySelectListItem</returns>
        public static IEnumerable<CategorySelectListItem> ToCategorySelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector, Func<TItem, string> categorySelector, Func<TItem, bool> selectedValueSelector)
        {
            return items.ToCategorySelectList(valueSelector, nameSelector, categorySelector, selectedValueSelector, null);
        }

        /// <summary>
        /// Converts the source sequence into an IEnumerable of CategorySelectListItem
        /// </summary>
        /// <param name="items">Source sequence</param>
        /// <param name="nameSelector">Lambda that specifies the name for the CategorySelectList items</param>
        /// <param name="valueSelector">Lambda that specifies the value for the CategorySelectList items</param>
        /// <param name="selectedValueSelector">Lambda that specifies whether the item should be selected</param>
        /// <returns>IEnumerable of CategorySelectListItem</returns>
        public static IEnumerable<CategorySelectListItem> ToCategorySelectList<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> valueSelector, Func<TItem, string> nameSelector, Func<TItem,string> categorySelector, Func<TItem, bool> selectedValueSelector, string emptyText)
        {
            if (!string.IsNullOrEmpty(emptyText))
                yield return new CategorySelectListItem { Text = emptyText };

            foreach (var item in items)
            {
                var value = valueSelector(item);

                yield return new CategorySelectListItem
                {
                    Text = nameSelector(item),
                    Value = value.ToString(),
                    Category = categorySelector(item),
                    Selected = selectedValueSelector(item)
                };
            }
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}