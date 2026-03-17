using LocalStorage.Models;
using LocalStorage.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LocalStorage.Pages
{
    public partial class Home
    {   
        [Inject] IJSRuntime js { get; set; }
        [Inject] ILocalStorageService? localStorage { get; set; }
        private IJSObjectReference? module;
        private string? data;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await js.InvokeAsync<IJSObjectReference>
                        ("import", "./Pages/Home.razor.js");
            }
        }

        async Task SaveToLocalStorageAsync()
        {
            var dataInfo = new DataInfo()
            {
                Value = data,
                Length = data!.Length,
                Timestamp = DateTime.Now
            };
            await localStorage!.SetItemAsync<DataInfo?>( "localStorageData", dataInfo);
        }
        async Task ReadFromLocalStorageAsync()
        {
            if (module is not null)
            {
                DataInfo? savedData = await localStorage!.GetItemAsync<DataInfo>("localStorageData");
                string result = $"Data = {savedData!.Value}";
                await module.InvokeVoidAsync("showLocalStorage", result);
            }
        }

    }
}
