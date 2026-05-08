using ApplicationLayer.Entities;
using ApplicationLayer.Entities.Burden;
using ApplicationLayer.Entities.Checks;
using ApplicationLayer.Entities.Contracts;
using ApplicationLayer.Entities.Drafts;
using ApplicationLayer.Entities.FilingInformation;
using ApplicationLayer.Entities.Grantor_Grantees;
using ApplicationLayer.Entities.Locations.Prospects.Data;
using ApplicationLayer.Entities.NewPayments;
using ApplicationLayer.Entities.Owners;
using ApplicationLayer.Entities.Payments;
using ApplicationLayer.Entities.SpecialObligation;
using ApplicationLayer.Entities.SpecialProvision;
using ApplicationLayer.Entities.TractOwnerships;
using ApplicationLayer.Entities.WorkingInterests;
using ApplicationLayer.Interfaces.ApiCommands._Generics;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Contract;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Leases;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Tracts;
using ApplicationLayer.Interfaces.ApiCommands.Assets.LandProperty.Well;
using ApplicationLayer.Interfaces.ApiCommands.Locations.Counties;
using ApplicationLayer.Interfaces.ApiCommands.RelatedProperties;
using ApplicationLayer.Interfaces.ApiCommands.RelatedProperties.TractMineralOwnership;
using ApplicationLayer.Interfaces.ApiQueries._Generics;
using ApplicationLayer.Interfaces.ApiQueries.Accounting.Check;
using ApplicationLayer.Interfaces.ApiQueries.Accounting.Draft;
using ApplicationLayer.Interfaces.ApiQueries.Accounting.Payments;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Contract;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Leases;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Leases.LeaseMain;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Property;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Tract.TractMain;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Wells;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Wells.AttachedTracts;
using ApplicationLayer.Interfaces.ApiQueries.Assets.Wells.WellCrossRefs;
using ApplicationLayer.Interfaces.ApiQueries.Owners.BusinessAssociates.OwnerMain;
using ApplicationLayer.Interfaces.ApiQueries.Owners.Filings;
using ApplicationLayer.Interfaces.ApiQueries.Owners.GrantorsGrantees;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.Burden;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.MineralOwnership;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.PaymentObligation;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.SpecialObligation;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.SpecialProvision;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.WorkingInterest;
using ApplicationLayer.Interfaces.ApiQueries.RelatedProperties.WorkingInterest.zWorkingInterestDetails;
using ApplicationLayer.Services.Accounting.Check;
using ApplicationLayer.Services.Accounting.Draft;
using ApplicationLayer.Services.Accounting.Payments;
using ApplicationLayer.Services.Assets.Leases;
using ApplicationLayer.Services.Assets.Leases.LeaseRelated;
using ApplicationLayer.Services.Assets.Tracts.Depths;
using ApplicationLayer.Services.Assets.Tracts.TractLeaseServices;
using ApplicationLayer.Services.Assets.Tracts.TractOwners;
using ApplicationLayer.Services.Assets.Tracts.TractStatus;
using ApplicationLayer.Services.Assets.Wells;
using ApplicationLayer.Services.DropdownLists;
using ApplicationLayer.Services.Locations.CountyServices;
using ApplicationLayer.Services.Owners.BusinessAssociateOwners.Associates;
using ApplicationLayer.Services.Owners.ChainOfTitles;
using ApplicationLayer.Services.Owners.Filings;
using ApplicationLayer.Services.Owners.GranteesGrantors;
using ApplicationLayer.Services.RelatedProperties.Burdens;
using ApplicationLayer.Services.RelatedProperties.PaymentObligation;
using ApplicationLayer.Services.RelatedProperties.SpecialObligation;
using ApplicationLayer.Services.RelatedProperties.SpecialProvision;
using ApplicationLayer.Services.RelatedProperties.TractMineralOwnership;
using ApplicationLayer.Services.RelatedProperties.WorkingInterest;
using ApplicationLayer.Services.RelatedProperties.WorkingInterest.WorkingInterestDetails;
using ApplicationLayer.UseCases.Assets.Leases.CheckIfLeaseExists;
using ApplicationLayer.UseCases.Assets.Leases.CreateEntity;
using ApplicationLayer.UseCases.Assets.Leases.DeleteEntity;
using ApplicationLayer.UseCases.Assets.Leases.UpdateEntity;
using ApplicationLayer.UseCases.Owners.Associates.Addresses.NewAddressCreation;
using ApplicationLayer.UseCases.Owners.Associates.CheckIfOwnerExists;
using ApplicationLayer.UseCases.Owners.Associates.CreateEntity;
using ApplicationLayer.UseCases.Owners.Associates.DeleteEntity;
using ApplicationLayer.UseCases.Owners.Associates.UpdateEntity;
using ApplicationLayer.UseCases.RelatedProperties.Burdens.CreateEntity;
using ApplicationLayer.UseCases.RelatedProperties.PaymentObligation.CreateEntity;
using ApplicationLayer.UseCases.RelatedProperties.SpecialObligation.CreateEntity;
using ApplicationLayer.UseCases.RelatedProperties.SpecialProvision.CreateEntity;
using ApplicationLayer.UseCases.RelatedProperties.TractMineralOwnership.CreateEntity;
using ApplicationLayer.UseCases.RelatedProperties.WorkingInterest.CreateEntity;
using ApplicationLayer.UseCases.RelatedProperties.WorkingInterest.zWorkingInterestDetails.CreateEntity;
using BlazorWebApp.Components;
using BlazorWebApp.Components.Account;
using BlazorWebApp.Components.Pages.OG_Forms.Properties.Wells.WellTabs.SubInternals.WellCrossRefs.RelatedComponents;
using BlazorWebApp.Components.Shared.Components.Generic_Cross_Ref_Form;
using BlazorWebApp.Data;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("UserDBConnection") ?? throw new InvalidOperationException("Connection string 'UserDBConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

#region AZURE AUTH

/// <summary>
/// ADDING AZURE AD AUTHENTICATION
/// TODO: FIX AZURE CONNECTION AFTER RENEWING ACCOUNT
/// </summary>
//builder.Services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
//    .AddAzureAD(options => Configuration.Bind("AzureAd", options));

#endregion AZURE AUTH

#region HTTP

#region ADDING HTTP CLIENT
/// <summary>
/// ADDING HTTP CLIENT
/// </summary>
//builder.Services.AddHttpClient();

//string Uri = Configuration.GetValue<string>("WebAPI"); MAY NOT NEED
builder.Services.AddHttpClient("WebAPI", c =>
{
    c.BaseAddress = new Uri("http://localhost:7000/api/");
    c.DefaultRequestHeaders.Add("Accept", "application/.json");

    /// OR
    //c.BaseAddress = new Uri(Configuration.GetValue<string>("WebAPI"));

    // OR
    //c.BaseAddress = new Uri(Configuration.GetConnectionString("API_URL"));

    //c.DefaultRequestHeaders.Accept.Clear();
    //c.DefaultRequestHeaders
    //.Accept
    //.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/.json"));




});

//services.AddHttpClient<IHttpClientBuilder,
//               IHttpClientBuilder>(client =>
// client.BaseAddress = new Uri(Configuration.GetSection("WebAPI").Value));

#endregion ADDING HTTP CLIENT

#region ADDING HTTP CONTENT

//services.AddScoped<IHttpContextAccessor, HttpContextAccessor>().AddOptions(
//    (
//        Options => options.
//    ));
//services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

#endregion ADDING HTTP CONTENT

#endregion HTTP


builder.Services.AddScoped(typeof(ApplicationLayer.DependancyInjection));
builder.Services.AddMediatR(typeof(ApplicationLayer.AssemblyReference).Assembly);


#region JSON

/// <summary>
/// JSON SERIALIZER
/// </summary>
//builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
////builder.Services.AddControllersWithViews().(options =>
//options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
//    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
//    = new DefaultContractResolver());



/// JSON SERIALIZER
///                
//builder.Services.AddControllersWithViews()
////    .AddJsonOptions
////    (options =>
////options.JsonSerializerOptions.DictionaryKeyPolicy = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
//    .AddJsonOptions
//    (options => options.JsonSerializerOptions.ReferenceHandler
//    = System.Text.Json.Serialization.ReferenceHandler.Preserve);


builder.Services.AddControllersWithViews()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;  // Pascal Case
    });

//builder.Services.AddHybridCache(options =>
//{
//    options.UseMemoryCache();
//    options.UseDistributedCache();
//});

#endregion JSON

#region SCOPED APPLICATION LAYER SERVICES

#region CONSTANTS

builder.Services.AddScoped<IDropdownQueryServiceTasks, DropdownQueryServiceTasks>();

#endregion CONSTANTS

#region ASSETS

#region CONTRACTS

builder.Services.AddScoped<IContractDropdowns, ContractDropdowns>();
builder.Services.AddScoped<ContractApiCommands>();
#endregion CONTRACTS

#region LEASES
builder.Services.AddScoped<IPopulateForm<LeaseMainForm2>, PopulateLeaseForm>();
builder.Services.AddScoped<LeaseApiCommands>();
builder.Services.AddScoped<LeaseMainServices>();
builder.Services.AddScoped<DoesLeaseExist>();
builder.Services.AddScoped<CreateLease>();
builder.Services.AddScoped<DeleteLease>();
builder.Services.AddScoped<UpdateLease>();
builder.Services.AddScoped<LeaseDataGridSearchs>();
builder.Services.AddScoped<LeaseRelatedPageServices>();
builder.Services.AddScoped</*ILeasePopulateDataGrids,*/ LeasePopulateDataGrids>();
#endregion LEASES

#region PROPERTY

#endregion PROPERTY

#region PROSPECTS
builder.Services.AddScoped<IPropertyDropdowns, PropertyDropdowns>();
builder.Services.AddScoped<TractsWithinProspect, TractsWithinProspect>();
#endregion PROSPECTS

#region TRACTS

builder.Services.AddScoped<IPopulateForm<TractMainForm>, PopulateTractForm>();
builder.Services.AddScoped<ISaveFormByEntityID<TractMainForm>, TractApiCommands>();
builder.Services.AddScoped<TractDepthsSearchServices>();
builder.Services.AddScoped<IStatusOfTracts, StatusOfTracts>();

#endregion TRACTS

#region WELLS
builder.Services.AddScoped<GenericCrossRefForm<ContractMainForm2>, RelatedContracts<ContractMainForm2>>();
builder.Services.AddScoped<IPopulateForm<WellMainForm2>, PopulateWellForm>();
builder.Services.AddScoped<IWellServices, WellServices>();
builder.Services.AddScoped<IWellAPiCommands, WellAPiCommands>();
builder.Services.AddScoped<WellCrossRefs>();
builder.Services.AddScoped</*IWellPopulateDatagrids,*/ WellPopulateDatagrids>();
#endregion WELLS

#endregion ASSETS

#region ACCOUNTING

#region CHECKS

builder.Services.AddScoped<IPopulateForm<CheckMasterMainForm>, PopulateCheckForm>();
builder.Services.AddScoped<ICheckServices, CheckServices>();

#endregion CHECKS

#region DRAFTS

builder.Services.AddScoped<IPopulateForm<DraftMasterMain>, PopulateDraftForm>();
builder.Services.AddScoped<IDraftServices, DraftServices>();

#endregion DRAFTS

#region PAYMENTS

builder.Services.AddScoped<IPopulateForm<NewPayment>, PopulatePaymentForm>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();

#endregion PAYMENTS

#endregion ACCOUNTING

#region LOCATIONS

#region COUNTIES

builder.Services.AddScoped<ICountyApiCommands, CountyApiCommands>();
builder.Services.AddScoped<ICountyQueryServiceTasks, CountyQueryServiceTasks>();

#endregion COUNTIES

#region DISTRICTS

#endregion DISTRICTS

#region REGIONS

#endregion REGIONS

#region STATES

#endregion STATES

#endregion LOCATIONS

#region OWNERS

#region BUSINESS / OWNERS / ASSOCIATES
builder.Services.AddScoped<IPopulateForm<OwnerAddressesMain>, PopulateOwnerForm>();

builder.Services.AddScoped<BusinessAssociates>();
builder.Services.AddScoped<DoesOwnerExist>();
builder.Services.AddScoped<CreateOwner>();
builder.Services.AddScoped<DeleteOwner>();
builder.Services.AddScoped<UpdateOwner>();
#endregion BUSINESS / OWNERS / ASSOCIATES

#region ADDRESSES
builder.Services.AddScoped<CreateNewAddress>();

#endregion ADDRESSES

#region FILING INFO
builder.Services.AddScoped<IPopulateForm<Recordings>, PopulateFilingForm>();
builder.Services.AddScoped<FilingInformationServices>();

#endregion FILING INFO

#region GRANTORS AND GRANTEES

builder.Services.AddScoped<IPopulateForm<GrantorsGrantees>, PopulateGrantorForm>();
builder.Services.AddScoped<IGranteesGrantorsServices, GranteesGrantorsServices>();

#endregion GRANTORS AND GRANTEES

#region CHAIN OF TITLES

builder.Services.AddScoped<IPopulateForm<GrantorsGrantees>, PopulateGrantorForm>();
builder.Services.AddScoped<ChainOfTitleServices>();

#endregion CHAIN OF TITLES

#endregion OWNERS

#region RELATED PROPERTIES

#region BURDENS
builder.Services.AddScoped<IPopulateForm<Burdens>, PopulateBurdenForm>();
builder.Services.AddScoped<BurdensApiCommands>();
builder.Services.AddScoped<IBurdenServices, BurdenServices>();
builder.Services.AddScoped<CreateBurden>();
#endregion BURDENS

#region PAYMENT OBLIGATIONS
builder.Services.AddScoped<IPopulateForm<PaymentObligations>, PopulatePaymentObligationForm>();
builder.Services.AddScoped<PaymentObligationApiCommands>();
builder.Services.AddScoped<IPaymentObligationServices, PaymentObligationServices>();
builder.Services.AddScoped<CreatePaymentObligation>();
#endregion PAYMENT OBLIGATIONS

#region SPECIAL OBLIGATIONS
builder.Services.AddScoped<IPopulateForm<SpecialObligations>, PopulateSpecialObligationForm>();
builder.Services.AddScoped<SpecialObligationApiCommands>();
builder.Services.AddScoped<ISpecialObligationServices, SpecialObligationServices>();
builder.Services.AddScoped<CreateSpecialObligation>();
#endregion SPECIAL OBLIGATIONS

#region SPECIAL PROVISIONS
builder.Services.AddScoped<IPopulateForm<SpecialProvisions>, PopulateSpecialProvisionForm>();
builder.Services.AddScoped<SpecialProvisionApiCommands>();
builder.Services.AddScoped<ISpecialProvisionServices, SpecialProvisionServices>();
builder.Services.AddScoped<CreateSpecialProvision>();
#endregion SPECIAL PROVISIONS

#region TRACT OWNERSHIP
builder.Services.AddScoped<IPopulateForm<TractOwnership>, PopulateMineralTractOwnershipForm>();
builder.Services.AddScoped<MineralOwnershipApiCommands>();
builder.Services.AddScoped<ITractMineralOwnershipServices, TractMineralOwnershipServices>();
builder.Services.AddScoped<TractLeaseServiceTasks>();
builder.Services.AddScoped<ITractOwnersQueryServiceTasks, TractOwnersQueryServiceTasks>();
builder.Services.AddScoped<CreateMineralOwnership>();
#endregion TRACT OWNERSHIP

#region WORKING INTEREST MAIN
builder.Services.AddScoped<IPopulateForm<WideckMaster>, PopulateWorkingInterestForm>();
builder.Services.AddScoped<WorkingInterestApiCommands>();
builder.Services.AddScoped<IWorkingInterestServices, WorkingInterestServices>();
builder.Services.AddScoped<CreateWorkingInterest>();
#endregion WORKING INTEREST MAIN 

#region WORKING INTEREST DETAILS
builder.Services.AddScoped<IPopulateForm<WideckDetails>, PopulateWorkingInterestsDetailsForm>();
builder.Services.AddScoped<WorkingInterestDetailApiCommands>();
builder.Services.AddScoped<IWorkingInterestDetailsServices, WorkingInterestDetailsServices>();
builder.Services.AddScoped<CreateWorkingInterestDetails>();
#endregion WORKING INTEREST DETAILS

#endregion RELATED PROPERTIES

#endregion SCOPED APPLICATION LAYER SERVICES


#region CORS

/// <summary>
/// ENABLE CORS
/// </summary>
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

#endregion CORS

var app = builder.Build();

// CORS
app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.Run();
