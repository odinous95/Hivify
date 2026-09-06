using Association.Application;
using Association.Infrastructure;
using BuildingBlocks.Infrastructure;
using Complaints.Application;
using Complaints.Infrastructure;
using DocumentsMgmt.Application;
using Feeds.Application;
using Feeds.Infrastructure;
using Houses.Application;
using Houses.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserMgmt.Application;
using UserMgmt.Application.Contracts;
using UserMgmt.Infrastructure.Identity;
using UserMgmt.Infrastructure.Presistence;

var builder = WebApplication.CreateBuilder(args);

#region API

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
#endregion


#region Database

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");



builder.Services.AddDbContextFactory<UserManagementDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

#endregion




#region Authentication & Authorization

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

builder.Services.AddAuthorization();

#endregion


#region Identity

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;

        options.Stores.SchemaVersion =
            IdentitySchemaVersions.Version3;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<UserManagementDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserDirectory, UserDirectory>();

builder.Services.AddSingleton<
    IEmailSender<ApplicationUser>,
    IdentityNoOpEmailSender>();

#endregion


#region Building Blocks
builder.Services.AddBuildingBlocks(builder.Configuration);

#endregion


#region Application


builder.Services.AddUserMgmtServices();
builder.Services.AddDocumentServices();

//Complaints
builder.Services.AddComplaintServices();
builder.Services.AddComplaintsInfrastructure(connectionString);

// Houses
builder.Services.AddHouseServices();
builder.Services.AddHousesInfrastructure(connectionString);

// Feed
builder.Services.AddFeedServices();
builder.Services.AddFeedInfrastructure(connectionString);
// Association
builder.Services.AddAssociationServices();
builder.Services.AddAssociationInfrastructure(connectionString);

#endregion




#region AI

builder.Services.AddHivifyAIServices();

#endregion


var app = builder.Build();


#region HTTP Pipeline

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion


app.Run();
