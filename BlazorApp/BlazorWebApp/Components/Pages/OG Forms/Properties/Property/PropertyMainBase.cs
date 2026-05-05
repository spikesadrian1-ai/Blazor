using ApplicationLayer.Entities.Locations.Prospects;
using ApplicationLayer.Entities.Property;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Property;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Components.Pages.OG_Forms.Properties.Property
{
    public class PropertyMainBase : ComponentBase
    {
        #region INJECTIONS
        [Inject]
        public IPropertyDropdowns _propertyDropdowns { get; set; }

        #endregion INJECTIONS

        #region CONSTRUCTORS

        public PropertyMainBase() { }

        public PropertyMainBase(
              IPropertyDropdowns propertyDropdowns
        )
        {            
            this._propertyDropdowns = propertyDropdowns;
        }

        #endregion CONSTRUCTORS

        public PropertyMainForm propertyMain { get; set; }
            = new PropertyMainForm();
        public async Task OnInitializedAsync(ProspectMainForm prospectMain)
        {
            if (string.IsNullOrWhiteSpace(propertyMain.Id.ToString()))
            {
                await PropertyMainPagePopulateDropdowns();
                //PropertyMorePagePopulateDropdowns();
            }
            else
            {
                await PopulateForm();
            }
        }

        #region PROPERTY MAIN PAGE DROPDOWNS
        public async Task<List<string>> PropertyMainPagePopulateDropdowns()
        {
            var getPropertyTypesTask = RetrieveAllPropertyTypes(); 
            var getGrantorsTask = RetrieveAllGrantors(); 
            var getGranteesTask = RetrieveAllGrantees(); 
            // soldTo
            // status

            await Task.WhenAll(
                               getPropertyTypesTask
                             , getGrantorsTask
                             , getGranteesTask
            // soldTo
            // status

                            );

            var report =
                         $"Property_Types: {getPropertyTypesTask.Result},"
                         +
                         $"Grantors: {getGrantorsTask.Result},"
                         + 
                         $"Grantees: {getGranteesTask.Result},"
            // soldTo
            // status
                         ;

            return new List<string> { report };
        }

        public List<string> propertyTypeList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllPropertyTypes() =>
           propertyTypeList = await _propertyDropdowns
           .RetrieveAllPropertyTypes();

        public List<string> grantorList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllGrantors() =>
            grantorList = await _propertyDropdowns
            .RetrieveAllGrantors();

        public List<string> granteeList { get; set; } = new List<string>();
        public async Task<List<string>> RetrieveAllGrantees() =>
            granteeList = await _propertyDropdowns
            .RetrieveAllGrantees();


        // soldTo


        // status

        #endregion PROPERTY MAIN PAGE DROPDOWNS

        #region PROPERTY MORE PAGE DROPDOWNS
        public async Task<List<string>> PropertyMorePagePopulateDropdowns()
        {
            var getPropertyTypesTask = RetrieveAllPropertyTypes();
            var getGrantorsTask = RetrieveAllGrantors();
            // soldTo
            // status

            await Task.WhenAll(
                               getPropertyTypesTask
                             , getGrantorsTask
            // soldTo
            // status

                            );

            var report =
                         $"Property_Types: {getPropertyTypesTask.Result},"
                         +
                         $"Grantors: {getGrantorsTask.Result},"
            // soldTo
            // status
                         ;

            return new List<string> { report };
        }

        //public List<string> propertyTypeList { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllPropertyTypes() =>
        //   propertyTypeList = await _propertyDropdowns
        //   .RetrieveAllPropertyTypes();

        //public List<string> grantorList { get; set; } = new List<string>();
        //public async Task<List<string>> RetrieveAllGrantors() =>
        //    grantorList = await _propertyDropdowns
        //    .RetrieveAllGrantors();

        // soldTo


        // status

        #endregion PROPERTY MORE PAGE DROPDOWNS

        private async Task<PropertyMainForm> PopulateForm()
        {
            throw new NotImplementedException();
        }
    }
}
