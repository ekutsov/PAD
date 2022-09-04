using System.Collections.Immutable;

namespace PAD.Identity.Core.Services;

public interface IScopeService
{
    Task<List<string>> ListResourcesAsync(ImmutableArray<string> scopes);
}