using Microsoft.AspNetCore.Components;

namespace Demo.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;
        private int increment = 3;

        [Parameter]
        public int? Increment { get; set; }

        protected override void OnParametersSet()
        {
            if (Increment.HasValue)
                increment = Increment.Value;
        }

        private void IncrementCount()
        {
            currentCount += increment;
        }
    }
}