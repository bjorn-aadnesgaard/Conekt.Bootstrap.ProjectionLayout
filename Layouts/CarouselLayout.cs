using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Projections.Descriptors.Layout;
using Orchard.Projections.Models;
using Orchard.Projections.Services;

namespace Conekt.Bootstrap.ProjectionLayout.Layouts
{
    public class CarouselLayout : ILayoutProvider {
        private readonly IContentManager _contentManager;
        protected dynamic Shape { get; set; }
        
        public CarouselLayout(IShapeFactory shapeFactory, IContentManager contentManager) {
            _contentManager = contentManager;
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }
        
        public Localizer T { get; set; }

        public void Describe(DescribeLayoutContext describe) {
            describe.For("Html", T("Html"),T("Html Layouts"))
                .Element("Carousel", T("Carousel"), T("Renders markup for use with Bootstrap Carousel."),
                    DisplayLayout,
                    RenderLayout,
                    "CarouselLayout"
                );
        }

        public LocalizedString DisplayLayout(LayoutContext context) {
            return T("Renders markup for use with Bootstrap Carousel.");
        }

        public dynamic RenderLayout(LayoutContext context, IEnumerable<LayoutComponentResult> layoutComponentResults) {

			string outerDivId = context.State.OuterDivId;
			string outerDivClass = context.State.OuterDivClass;
            string innerDivClass = context.State.InnerDivClass;
            string firstItemClass = context.State.FirstItemClass;
            string itemClass = context.State.ItemClass;
			string interval = context.State.Interval;

            IEnumerable<dynamic> shapes =
               context.LayoutRecord.Display == (int)LayoutRecord.Displays.Content
                   ? layoutComponentResults.Select(x => _contentManager.BuildDisplay(x.ContentItem, context.LayoutRecord.DisplayType))
                   : layoutComponentResults.Select(x => x.Properties);

            return Shape.Carousel(
				Id: outerDivId,
				Items: shapes,
				OuterClasses: new[] { outerDivClass },
				InnerClasses: new[] { innerDivClass },
				FirstItemClasses: new[] { firstItemClass },
				ItemClasses: new[] { itemClass },
				Interval: interval
				);
        }
    }
}