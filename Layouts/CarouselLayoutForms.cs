using System;
using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;

namespace Conekt.Bootstrap.ProjectionLayout.Layouts
{

    public class CarouselLayoutForms : IFormProvider {
        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public CarouselLayoutForms(
            IShapeFactory shapeFactory) {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context) {
            Func<IShapeFactory, object> form =
                shape => {

                    var f = Shape.Form(
                        Id: "CarouselLayout",                        
                        _HtmlProperties: Shape.Fieldset(
                            Title: T("Html properties"), 
                            _ListId: Shape.TextBox(
                                Id: "outer-div-id", Name: "OuterDivId",
                                Title: T("Outer div id"),
                                Description: T("The id to provide on the div element."),
                                Classes: new[] { "textMedium", "tokenized" }
                                ),
                            _ListClass: Shape.TextBox(
                                Id: "outer-div-class", Name: "OuterDivClass",
                                Title: T("Outer div class"),
                                Description: T("The class to provide on the div element."),
                                Classes: new[] { "textMedium", "tokenized" }
                                ),
                            _InnerClass: Shape.TextBox(
                                Id: "inner-div-class", Name: "InnerDivClass",
                                Title: T("Inner div class"),
                                Description: T("The class to provide on the inner div element."),
                                Classes: new[] { "textMedium", "tokenized" }
                                ),
                             _FirstItemClass: Shape.TextBox(
                                Id: "first-item-class", Name: "FirstItemClass",
                                Title: T("First item class"),
                                Description: T("The class to provide on the first item element."),
                                Classes: new[] { "textMedium", "tokenized" }
                                ),
                            _ItemClass: Shape.TextBox(
                                Id: "item-class", Name: "ItemClass",
                                Title: T("Item class"),
                                Description: T("The class to provide on the item element."),
                                Classes: new[] { "textMedium", "tokenized" }
                                ),
							_Interval: Shape.TextBox(
								Id: "interval", Name: "Interval",
								Title: T("Interval"),
								Description: T("The amount of time to delay between automatically cycling an item. If false, carousel will not automatically cycle."),
								Classes: new[] { "textMedium", "tokenized" }
								)
                            )
                        );

                    return f;
                };

            context.Form("CarouselLayout", form);

        }
    }
}