using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

using static System.Reflection.Emit.GenericTypeParameterBuilder;

namespace BlazorWebApp.Shared.Components.Generic_Cross_Ref_Form
{
    public class GenericCrossRefFormBase<T> : ComponentBase
    //,
    //IGenericCrossRefFormGetDataActions<EntityName>
    {
        public HttpClient _httpClient { get; set; }
        public IHttpClientFactory _clientFactory { get; set; }
        public IConfiguration _config { get; set; }

        [Parameter] public string EntityName { get; set; } = string.Empty;

        public GenericCrossRefFormBase() { }

        #region RETRIEVE LIST OF LEASES

        //[Parameter] public string API_RouteAddress { get; set; } = string.Empty;
        //[Parameter] public string Title { get; set; } = string.Empty;
        //[Parameter] public List<T> TableData { get; set; } = new List<T>();
        //public async Task<List<T>> PopulateData
        //    (
        //        string API_RouteAddress
        //        //, string Title
        //        , List<T> TableData
        //    )
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get,
        //    _config["API_URL"] + $"{API_RouteAddress}");
        //    var client = _clientFactory.CreateClient();
        //    HttpResponseMessage response = await client.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        using var responseStream = await response.Content.ReadAsStreamAsync();
        //        TableData = await JsonSerializer.DeserializeAsync<List<T>>(responseStream);

        //        //errorString = null;
        //    }
        //    else
        //    {
        //        //errorString = $"There was an error getting Prospects Data: {response.ReasonPhrase}";
        //    }

        //    return TableData;
        //}

        #endregion RETRIEVE LIST OF TRACTS

        //[Parameter] public string EditModalHrefName { get; set; }
        //[Parameter] public string DeleteModalHrefName { get; set; }

        //protected override async Task OnInitializedAsync()
        //{
        //    await PopulateData(API_RouteAddress, TableData);
        //}
    }
}
