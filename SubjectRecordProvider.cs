using Rsk.Enforcer.PIP;
using Rsk.Enforcer.PolicyModels;

namespace ZuploEnforcerPdp;

public class SubjectRecord
{
    [PolicyAttributeValue(PolicyAttributeCategories.Subject, "role")]
    public string[] Roles { get; init; } = [];
}

public class SubjectRecordProvider : RecordAttributeValueProvider<SubjectRecord>
{
    private static readonly PolicyAttribute SubjectIdentifier =
        new PolicyAttribute("id", PolicyValueType.String, PolicyAttributeCategories.Subject);

    private static readonly SubjectRecord Anonymous = new SubjectRecord();

    private static readonly Dictionary<string, SubjectRecord> SubjectRecordMap = new Dictionary<string, SubjectRecord>
    {
        ["jerrie-pelser"] = new SubjectRecord { Roles = ["manager"] },
        ["peter-parker"] = new SubjectRecord { Roles = [] }
    };

    protected override async Task<SubjectRecord> GetRecordValue(IAttributeResolver attributeResolver, CancellationToken ct)
    {
        var id = (await attributeResolver.Resolve<string>(SubjectIdentifier, ct))
            .Single();

        if (!SubjectRecordMap.TryGetValue(id, out var subject)) return Anonymous;

        return subject;
    }
}