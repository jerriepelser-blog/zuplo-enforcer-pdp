using Rsk.Enforcer;
using Rsk.Enforcer.AuthZen;
using Rsk.Enforcer.PEP;
using ZuploEnforcerPdp;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddLogging(b => { b.AddConsole(); })
    .AddEnforcer("AcmeCorp.Global",
        o =>
        {
            o.Licensee = "DEMO";
            o.LicenseKey = "...";
        })
    .AddPolicyEnforcementPoint(o => o.Bias = PepBias.Deny)
    .AddPolicyAttributeProvider<SubjectRecordProvider>()
    .AddEmbeddedPolicyStore("ZuploEnforcerPdp.Policies");

var app = builder.Build();

app.UseEnforcerAuthZen();

app.Run();