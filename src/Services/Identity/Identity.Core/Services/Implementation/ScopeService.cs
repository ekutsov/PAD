using System.Collections.Immutable;
using OpenIddict.Abstractions;
using PAD.Identity.Core.Helpers;

namespace PAD.Identity.Core.Services;

public class ScopeService : IScopeService
{
    private readonly IOpenIddictScopeManager _scopeManager;

    public ScopeService(IOpenIddictScopeManager scopeManager)
    {
        _scopeManager = scopeManager;
    }

    public async Task<List<string>> ListResourcesAsync(ImmutableArray<string> scopes)
    {
        List<string> resources = await _scopeManager.ListResourcesAsync(scopes).ToListAsync();

        return resources;
    }
}