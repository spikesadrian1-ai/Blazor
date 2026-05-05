using Microsoft.AspNetCore.Components;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationLayer.Entities.Locations.Counties;
using ApplicationLayer.Services.Locations.CountyServices;

namespace BlazorWebApp.Components.Pages.OG_Forms.Locations.Counties.CountyTabs
{
    public partial class CountyMoreBase : ComponentBase
    {
        #region INJECTIONS

        [Inject]
        public ICountyQueryServiceTasks CountyService { get; set; }

        #endregion INJECTIONS

        #region ON INITIALIZED ASYNC

        protected override async Task OnInitializedAsync()
        {
            /// PART OF THE LOADING INDICATOR
            System.Threading.Thread.Sleep(3000);

            await PopulateCounties();
        }

        #endregion ON INITIALIZED ASYNC

        #region DATAGRID

        public List<CountyMasterMainForm> ListAllCounties { get; set; } = new List<CountyMasterMainForm>();
        public string ErrorString;

        async Task<List<CountyMasterMainForm>> PopulateCounties()
        {

            if (ListAllCounties is null) { ErrorString = "There was an error getting Counties"; }

            /// CALLS METHOD FROM APPLICATION LAYER BUT UTILIZING THE APPLICATION DTO 
            /// INSTEAD OF THE DOMAIN LAYER TO AVOID ERROR WITH JSON CONVERSION ISSUES
            return ListAllCounties = await CountyService.RetrieveAllCountiesAsync(ListAllCounties);

        }

        #endregion DATAGRID
    }
}
