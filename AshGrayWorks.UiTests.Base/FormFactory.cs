using System;
using System.Linq;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace AshGrayWorks.UiTests.Base
{
    public class FormFactory
    {
        public static U CreateForm<U>(Window wnd)
            where U : FlowForm, new()
        {
            var form = new U { Wnd = wnd };

            InstantiateControls(form, wnd);

            return form;
        }

        private static void InstantiateControls<U>(U form, Window wnd)
        {
            typeof(U).GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(ByAutomationIdAttribute)))
                .Select(p => new
                {
                    Type = p,
                    SearchCriteria = p.GetCustomAttributes(typeof(ByAutomationIdAttribute), true)
                        .Cast<ByAutomationIdAttribute>()
                        .Select(a => SearchCriteria.ByAutomationId(a.Id))
                        .Single()
                })
                .ToList()
                .ForEach(
                    p =>
                    {
                        var propertyType = p.Type.PropertyType;

                        if (propertyType == typeof(TextBox))
                        {
                            p.Type.SetValue(form, wnd.Get<TextBox>(p.SearchCriteria), null);
                        } 
                        else if (propertyType == typeof(Button))
                        {
                            p.Type.SetValue(form, wnd.Get<Button>(p.SearchCriteria), null);
                        }
                        else
                        {
                            throw new NotImplementedException(String.Format("There is no factory for type {0}", propertyType.ToString()));
                        }
                    });
        }
    }
}